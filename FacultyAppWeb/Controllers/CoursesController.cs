using FacultyAppWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FacultyAppWeb.Controllers
{
    public class CoursesController : Controller

    {
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ILogger<CoursesController> logger)
        {
            _logger = logger;
        }




        public async Task<IActionResult> Index(string searchTerm = null, string searchCategory = null)
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
                    //if category is undefined
                    if (searchCategory == null || searchCategory.Equals("All"))
                    {
                        if (string.IsNullOrEmpty(searchTerm))
                            return View(courses);
                    
                    else
                    {
                        List<Course> newList = new List<Course>();
                        foreach (var course in courses)
                        {
                            if (course.Title.ToLower().Contains(searchTerm.ToLower()))
                            {
                                newList.Add(course);
                            }
                        }
                        return View(newList);
                    }
                    }
                    //if category is defined
                    else
                    {
                        List<Course> newList = new List<Course>();
                        foreach (var course in courses)
                        {
                            if (course.Category.Contains(searchCategory))
                            {
                                newList.Add(course);
                            }
                        }
                        if (string.IsNullOrEmpty(searchTerm))
                        {
                         return View(newList);
                        }
                            
                        else
                        {
                            List<Course> newList2 = new List<Course>();
                            foreach (var course in newList)
                            {
                                if (course.Title.ToLower().Contains(searchTerm.ToLower()))
                                {
                                    newList2.Add(course);
                                }
                            }
                            return View(newList2);
                        }
                    }
                   


                }

            }
            catch (Exception)

            {
                return View();
            }


           
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
       