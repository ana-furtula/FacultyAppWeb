using FacultyAppWeb.Domains;
using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Models.Subjects
{
    public class CreateSubjectViewModel
    {
        public Subject Subject { get; set; }

        [TempData]
        public string? MessageCreate { get; set; }
    }
}
