using FacultyAppWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FacultyAppWeb.Controllers
{
    public class CoursesController: Controller
        
    {
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ILogger<CoursesController> logger)
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

        /* public IActionResult Index()
         {
             return View();
         }*/

        public async Task<IActionResult> Index()
        {
            List<Course> courses = new List<Course>();

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://udemy-courses-coupon-code.p.rapidapi.com/api/udemy_course/"),
                    Headers =
    {
        { "x-rapidapi-host", "udemy-courses-coupon-code.p.rapidapi.com" },
        { "x-rapidapi-key", "9ae3f1311emsh0ae100ce3b2cc37p1668a8jsn872b9b890c3d" },
    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
        
                    var coursesJSON = System.Text.Json.JsonSerializer.Deserialize<List<Course>>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    foreach (var course in coursesJSON)
                    {
                        courses.Add(course);
                    } 
                }

            }
            catch (Exception)

            {
                return View();
            }


            return View(courses);
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
