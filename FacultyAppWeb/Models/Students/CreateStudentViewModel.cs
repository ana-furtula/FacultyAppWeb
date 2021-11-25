using FacultyAppWeb.Domains;
using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Models.Students
{
    public class CreateStudentViewModel
    {
        public Student Student { get; set; }

        [TempData]
        public string? MessageCreate { get; set; } 
    }
}
