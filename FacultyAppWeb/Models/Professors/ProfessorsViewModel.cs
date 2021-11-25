using FacultyAppWeb.Domains;
using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Models.Professors
{
    public class ProfessorsViewModel
    {
        public string SearchTerm { get; set; }
        public List<Professor> Professors { get; set; }

        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }
    }
}
