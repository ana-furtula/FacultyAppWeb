using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Controllers
{
    public class SubjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
