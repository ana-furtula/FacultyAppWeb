using FacultyAppWeb.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FacultyAppWeb.Models.ExamRegistrations
{
    public class EditExamRegistrationViewModel
    {
        public List<SelectListItem> Professors { get; set; }
        public string SelectedProfessor { get; set; }
        [Required]
        public ExamRegistration ExamRegistration { get; set; }
        [TempData]
        public string MessageError { get; set; }
    }
}
