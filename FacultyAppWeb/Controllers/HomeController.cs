using FacultyAppWeb.Models;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace FacultyAppWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AcceptVerbs("GET")]
        public IActionResult VerifyFirstName(string firstName)
        {
            var rx = new Regex("[A-Z][a-z]+");
            
            if (string.IsNullOrEmpty(firstName) || !rx.IsMatch(firstName))
                return Json($"First name {firstName} is not valid.");

            return Json(true);
        }

        [AcceptVerbs("GET")]
        public IActionResult VerifyLastName(string lastName)
        {
            var rx = new Regex("[A-Z][a-z]+");

            if (string.IsNullOrEmpty(lastName) || !rx.IsMatch(lastName))
                return Json($"Last name {lastName} is not valid.");

            return Json(true);
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
/*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]*/
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}