using Microsoft.AspNetCore.Mvc;
using FacultyAppWeb.Domains;

namespace FacultyAppWeb.Models.Subjects
{
    public class SubjectsViewModel
    {
        public string SearchTerm { get; set; }
        public List<Subject> Subjects { get; set; }
        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }

    }
}
