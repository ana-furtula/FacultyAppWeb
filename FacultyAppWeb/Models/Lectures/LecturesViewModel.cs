using FacultyAppWeb.Domains;
using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Models.Lectures
{
    public class LecturesViewModel
    {
        public string SearchTerm { get; set; }
        public List<Lecture> Lectures { get; set; }
        public int TotalLecturesNumber { get; set; }
        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }
    }
}
