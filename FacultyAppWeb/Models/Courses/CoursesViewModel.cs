using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacultyAppWeb.Models.Courses
{
    public class CoursesViewModel
    {
        public List<Course> Courses { get; set; }
        public string SearchTerm { get; set; }
        public string SearchCategory { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
