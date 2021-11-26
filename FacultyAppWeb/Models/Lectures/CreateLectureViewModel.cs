using FacultyAppWeb.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacultyAppWeb.Models.Lectures
{
    public class CreateLectureViewModel
    {
        public List<SelectListItem> Professors { get; set; }
        public string SelectedProfessor { get; set; }
        public List<SelectListItem> Subjects { get; set; }
        public string SelectedSubject { get; set; }

        [TempData]
        public String MessageCreate { get; set; }

    }
}
