using FacultyAppWeb.Domains;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FacultyAppWeb.Models.Professors
{
    public class CreateProfessorViewModel
    {
        [Required]
        [Remote(action: "VerifyFirstName", controller: "Home")]
        public string FirstName { get; set; }

        [Required]
        [Remote(action: "VerifyLastName", controller: "Home")]
        public string LastName { get; set; }

        [Required]
        [Remote(action: "VerifyJMBG", controller: "Professors")]
        [StringLength(13)]
        public string JMBG { get; set; }

        [TempData]
        public string? MessageCreate { get; set; }
    }
}
