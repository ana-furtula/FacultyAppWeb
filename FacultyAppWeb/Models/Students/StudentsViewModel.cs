using Microsoft.AspNetCore.Mvc;
using FacultyAppWeb.Domains;

namespace FacultyAppWeb.Models.Students
{
    public class StudentsViewModel
    {
        public string SearchTerm { get; set; }
        public List<Student> Students { get; set; }
        [TempData]
        public string Message { get; set; }

    }
}
