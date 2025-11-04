using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using GiftOfTheGivers.Tests.TestHelpers;

namespace GiftOfTheGivers.Tests.UI
{
    [TestClass]
    public class RegisterLoginSeleniumTests
    {
        private static TestAppRunner? _runner;
        private static string? _dbFilePath;
        private IWebDriver? _driver;
        private WebDriverWait? _wait;

        [ClassInitialize]
        public static void ClassInit(TestContext _)
        {
            // create unique sqlite DB per test run and pass to the app process
            _dbFilePath = Path.Combine(Path.GetTempPath(), $"e2e_{Guid.NewGuid():N}.db");
            var env = new Dictionary<string, string>
            {
                ["ConnectionStrings__DefaultConnection"] = $"Data Source={_dbFilePath}"
            };

            // Explicitly resolve web project .csproj path for TestAppRunner.
            string? projectPath = Environment.GetEnvironmentVariable("WEBAPP_PROJECT_PATH");
            if (string.IsNullOrWhiteSpace(projectPath))
            {
                var baseDir = AppContext.BaseDirectory!;
                projectPath = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "ST10398576_Disaster Alleviation Foundation", "ST10398576_Disaster Alleviation Foundation.csproj"));

                // fallback attempt (one folder higher)
                if (!File.Exists(projectPath))
                {
                    var alt = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "..", "ST10398576_Disaster Alleviation Foundation", "ST10398576_Disaster Alleviation Foundation.csproj"));
                    if (File.Exists(alt)) projectPath = alt;
                }
            }

            if (string.IsNullOrWhiteSpace(projectPath) || !File.Exists(projectPath))
                throw new FileNotFoundException($"Web project .csproj not found at '{projectPath}'. Set environment variable WEBAPP_PROJECT_PATH or update the ClassInit logic.", projectPath);

            _runner = TestAppRunner.Start(projectFilePath: projectPath, startupTimeoutSeconds: 60, env: env);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _runner?.Dispose();
            try
            {
                if (!string.IsNullOrWhiteSpace(_dbFilePath) && File.Exists(_dbFilePath))
                    File.Delete(_dbFilePath);
            }
            catch { /* best-effort cleanup */ }
        }

        [TestInitialize]
        public void TestInit()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless=new");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");

            _driver = new ChromeDriver(chromeOptions);
            // increase wait to reduce flakiness during first-run migrations / page loads
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _driver = null;
            _wait = null;
        }

        [TestMethod]
        public void Login_Works()
        {
            var baseUrl = _runner!.BaseUrl;

            // use a unique email so repeated runs don't conflict
            var email = $"selenium_{Guid.NewGuid():N}@example.com";
            var password = "Pass123!";

            // --------------------
            // Register the account
            // --------------------
            _driver!.Navigate().GoToUrl($"{baseUrl}/Account/Register");
            _wait!.Until(d => d.FindElement(By.Name("FullName")));

            _driver.FindElement(By.Name("FullName")).SendKeys("Selenium User");
            _driver.FindElement(By.Name("Email")).SendKeys(email);
            _driver.FindElement(By.Name("Password")).SendKeys(password);

            // select role (exists in the view)
            var roleEl = _driver.FindElement(By.Name("Role"));
            var roleSelect = new SelectElement(roleEl);
            roleSelect.SelectByText("Donor");

            // submit
            var submitSelector = "form button[type=submit], form input[type=submit], form button:not([type])";
            _wait.Until(d => d.FindElements(By.CssSelector(submitSelector)).Count > 0);
            _driver.FindElement(By.CssSelector(submitSelector)).Click();

            _wait.Until(d => !d.Url.Contains("/Account/Register"));

            // ensure clean session and perform login
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Navigate().GoToUrl($"{baseUrl}/Account/Login");
            _wait.Until(d => d.FindElement(By.Name("Email")));

            _driver.FindElement(By.Name("Email")).Clear();
            _driver.FindElement(By.Name("Email")).SendKeys(email);
            _driver.FindElement(By.Name("Password")).Clear();
            _driver.FindElement(By.Name("Password")).SendKeys(password);

            _wait.Until(d => d.FindElements(By.CssSelector(submitSelector)).Count > 0);
            _driver.FindElement(By.CssSelector(submitSelector)).Click();

            _wait.Until(d => !d.Url.Contains("/Account/Login"));
            _driver.Url.Should().NotContain("/Account/Login");
            _driver.Url.Should().Contain("/");
        }
    }
}