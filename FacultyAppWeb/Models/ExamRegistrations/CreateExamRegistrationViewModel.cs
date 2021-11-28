using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacultyAppWeb.Models.ExamRegistrations
{
    public class CreateExamRegistrationViewModel
    {
        public List<SelectListItem> Students { get; set; }
        public string SelectedStudent { get; set; }
        public List<SelectListItem> Subjects { get; set; }
        public string SelectedSubject { get; set; }

        [TempData]
        public string MessageCreate { get; set; }
        
    }
}
