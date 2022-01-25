using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FacultyAppWeb.Models.Students
{
    public class EditStudentViewModel
    {
        public long Id { get; set; }
        [Required]
        [Remote(action: "VerifyFirstName", controller: "Home")]
        public string FirstName { get; set; }
        [Required]
        [Remote(action: "VerifyLastName", controller: "Home")]
        public string LastName { get; set; }
        public string Index { get; set; }
        public string JMBG { get; set; }
        public string Email { get; set; }
    }
}
