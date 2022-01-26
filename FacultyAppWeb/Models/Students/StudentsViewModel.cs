using Microsoft.AspNetCore.Mvc;
using FacultyAppWeb.Domains;

namespace FacultyAppWeb.Models.Students
{
    public class StudentsViewModel
    {
        public string SearchTerm { get; set; }

        public List<Student> Students { get; set; }

        public int TotalStudentsNumber { get; set; }

        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }
       
       




    }
}
