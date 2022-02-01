using FacultyAppWeb.Models;
using FacultyAppWeb.Models.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public async Task<IActionResult> Index(CoursesViewModel model = null)
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

                    var coursesJSON = JsonSerializer.Deserialize<List<Course>>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    foreach (var course in coursesJSON)
                    {
                        courses.Add(course);
                    }

                    List<string> categories = new List<string>();

                    foreach (var course in courses)
                    {
                        foreach (var cat in course.Category)
                        {
                            if (!categories.Where(x => x.Equals(cat)).Any())
                            {
                                categories.Add(cat);
                            }
                        }
                    }

                    var selectListCategories = new List<SelectListItem>();
                    selectListCategories.Add(new SelectListItem()
                    {
                        Value = "All",
                        Text = "All"
                    });
                    foreach (var element in categories)
                    {
                        selectListCategories.Add(new SelectListItem
                        {
                            Value = element,
                            Text = element
                        });
                    }

                    if (model == null)
                        return View(new CoursesViewModel()
                        {
                            Courses = courses,
                            Categories = selectListCategories,
                            SearchCategory = null,
                            SearchTerm = null
                        });

                    //if category is undefined
                    if (model.SearchCategory == null || model.SearchCategory.Equals("All"))
                    {
                        if (string.IsNullOrEmpty(model.SearchTerm))
                            return View(new CoursesViewModel()
                            {
                                Courses = courses,
                                Categories = selectListCategories,
                                SearchCategory = null,
                                SearchTerm = null
                            });

                        else
                        {
                            List<Course> newList = new List<Course>();
                            foreach (var course in courses)
                            {
                                if (course.Title.ToLower().Contains(model.SearchTerm.ToLower()))
                                {
                                    newList.Add(course);
                                }
                            }
                            return View(new CoursesViewModel()
                            {
                                Courses = newList,
                                Categories = selectListCategories,
                                SearchCategory = model.SearchCategory,
                                SearchTerm = model.SearchTerm
                            });
                        }
                    }
                    //if category is defined
                    else
                    {
                        List<Course> newList = new List<Course>();
                        foreach (var course in courses)
                        {
                            if (course.Category.Contains(model.SearchCategory))
                            {
                                newList.Add(course);
                            }
                        }
                        if (string.IsNullOrEmpty(model.SearchTerm))
                        {
                            return View(new CoursesViewModel()
                            {
                                Courses = newList,
                                Categories = selectListCategories,
                                SearchCategory = model.SearchCategory,
                                SearchTerm = model.SearchTerm
                            }); ;
                        }

                        else
                        {
                            List<Course> newList2 = new List<Course>();
                            foreach (var course in newList)
                            {
                                if (course.Title.ToLower().Contains(model.SearchTerm.ToLower()))
                                {
                                    newList2.Add(course);
                                }
                            }
                            return View(new CoursesViewModel()
                            {
                                Courses = newList2,
                                Categories = selectListCategories,
                                SearchCategory = model.SearchCategory,
                                SearchTerm = model.SearchTerm
                            });
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
       