using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System.Diagnostics;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult About() => View();
    }
}
