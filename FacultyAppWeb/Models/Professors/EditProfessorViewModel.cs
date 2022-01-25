using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FacultyAppWeb.Models.Professors
{
    public class EditProfessorViewModel
    {
        public long Id { get; set; }
        public string JMBG { get; set; }
        public string Email { get; set; }

        [Required]
        [Remote(action: "VerifyFirstName", controller: "Home")]
        public string FirstName { get; set; }

        [Required]
        [Remote(action: "VerifyLastName", controller: "Home")]
        public string LastName { get; set; }
    }
}
