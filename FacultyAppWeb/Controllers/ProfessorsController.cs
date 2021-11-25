using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Controllers
{
    public class ProfessorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
