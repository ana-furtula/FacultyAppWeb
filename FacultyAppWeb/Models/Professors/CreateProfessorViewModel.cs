using FacultyAppWeb.Domains;
using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Models.Professors
{
    public class CreateProfessorViewModel
    {
        public Professor Professor { get; set; }

        [TempData]
        public string? MessageCreate { get; set; }
    }
}
