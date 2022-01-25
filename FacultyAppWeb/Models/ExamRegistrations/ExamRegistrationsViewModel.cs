using FacultyAppWeb.Domains;
using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Models.ExamRegistrations
{
    public class ExamRegistrationsViewModel
    {
        public List<ExamRegistration> ExamRegistrations { get; set; }
        public string SearchTermSubject { get; set; }
        public string SearchTermStudent { get; set; }
        public string CurrentUserEmail { get; set; }
        public List<Lecture> Lectures { get; set; }

        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }

    }
}
