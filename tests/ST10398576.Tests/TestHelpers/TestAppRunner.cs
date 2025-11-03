using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GiftOfTheGivers.Tests.TestHelpers
{
    public sealed class TestAppRunner : IDisposable
    {
        private Process? _process;
        private StringBuilder _stdOut = new();
        private StringBuilder _stdErr = new();

        public string BaseUrl { get; private set; } = string.Empty;

        private TestAppRunner() { }

        public static TestAppRunner Start(string? projectFilePath = null, int startupTimeoutSeconds = 30, System.Collections.Generic.IDictionary<string, string>? env = null)
        {
            var runner = new TestAppRunner();

            // Resolve default project path relative to test assembly if not provided
            if (string.IsNullOrWhiteSpace(projectFilePath))
            {
                var assemblyDir = AppContext.BaseDirectory!;
                projectFilePath = Path.GetFullPath(Path.Combine(assemblyDir, "..", "..", "..", "..", "ST10398576_Disaster Alleviation Foundation", "ST10398576_Disaster Alleviation Foundation.csproj"));
            }

            if (!File.Exists(projectFilePath))
                throw new FileNotFoundException($"Web project file not found at '{projectFilePath}'. Pass the path to Start() or set WEBAPP_PROJECT_PATH.", projectFilePath);

            var port = GetFreePort();
            runner.BaseUrl = $"http://127.0.0.1:{port}";

            // Prevent launchSettings.json from overriding environment and connection string values
            var args = $"run --project \"{projectFilePath}\" --no-launch-profile --urls {runner.BaseUrl}";
            var psi = new ProcessStartInfo("dotnet", args)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // ensure Testing environment
            psi.Environment["ASPNETCORE_ENVIRONMENT"] = "Testing";

            // apply additional env overrides (e.g. connection string)
            if (env != null)
            {
                foreach (var kv in env) psi.Environment[kv.Key] = kv.Value;
            }

            runner._process = Process.Start(psi) ?? throw new InvalidOperationException("Failed to start web app process.");

            // capture stdout/stderr for diagnostics
            runner._process.OutputDataReceived += (_, e) => { if (!string.IsNullOrEmpty(e.Data)) runner._stdOut.AppendLine(e.Data); };
            runner._process.ErrorDataReceived += (_, e) => { if (!string.IsNullOrEmpty(e.Data)) runner._stdErr.AppendLine(e.Data); };
            runner._process.BeginOutputReadLine();
            runner._process.BeginErrorReadLine();

            // wait until the host responds (accept 2xx, 3xx, 404 or 401 as "listening")
            var sw = Stopwatch.StartNew();
            using var client = new HttpClient();
            while (sw.Elapsed < TimeSpan.FromSeconds(startupTimeoutSeconds))
            {
                try
                {
                    var resp = client.GetAsync(runner.BaseUrl).GetAwaiter().GetResult();

                    // Accept success, redirects, NotFound (404) or Unauthorized (401)
                    var code = (int)resp.StatusCode;
                    if (resp.IsSuccessStatusCode
                        || resp.StatusCode == HttpStatusCode.NotFound
                        || resp.StatusCode == HttpStatusCode.Unauthorized
                        || (code >= 300 && code < 400))
                    {
                        return runner;
                    }
                }
                catch { /* not ready yet */ }

                if (runner._process.HasExited)
                {
                    var exit = runner._process.ExitCode;
                    var outText = runner._stdOut.ToString();
                    var errText = runner._stdErr.ToString();
                    runner.Dispose();
                    throw new InvalidOperationException($"Web app process exited prematurely (exit code {exit}).\nStdOut:\n{outText}\nStdErr:\n{errText}");
                }

                Thread.Sleep(200);
            }

            // timed out — include captured stdout/stderr to help diagnose startup hang
            var stdOutFinal = runner._stdOut.ToString();
            var stdErrFinal = runner._stdErr.ToString();
            runner.Dispose();
            throw new TimeoutException($"Timed out waiting for web app to start at {runner.BaseUrl}.\nStdOut:\n{stdOutFinal}\nStdErr:\n{stdErrFinal}");
        }

        private static int GetFreePort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        public void Dispose()
        {
            try
            {
                if (_process != null && !_process.HasExited)
                {
                    try { _process.Kill(true); } catch { _process.Kill(); }
                    _process.WaitForExit(2000);
                }
            }
            catch { }
            finally
            {
                _process?.Dispose();
                _process = null;
            }
        }
    }
}