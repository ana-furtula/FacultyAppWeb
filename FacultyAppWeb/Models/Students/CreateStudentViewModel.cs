using FacultyAppWeb.Domains;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FacultyAppWeb.Models.Students
{
    public class CreateStudentViewModel
    {
        [Required]
        [Remote(action: "VerifyIndex", controller: "Students")]
        [StringLength(9)]
        public string Index { get; set; }
        [Required]
        [Remote(action: "VerifyFirstName", controller: "Home")]
        public string FirstName { get; set; }
        [Required]
        [Remote(action: "VerifyLastName", controller: "Home")]
        public string LastName { get; set; }
        [Required]
        [Remote(action: "VerifyJMBG", controller: "Students")]
        [StringLength(13)]
        public string JMBG { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [TempData]
        public string? MessageCreate { get; set; } 
    }
}
