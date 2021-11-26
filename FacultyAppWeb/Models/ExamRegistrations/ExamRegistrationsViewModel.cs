using FacultyAppWeb.Domains;
using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Models.ExamRegistrations
{
    public class ExamRegistrationsViewModel
    {
        public List<ExamRegistration> ExamRegistrations { get; set; }
        public String SearchTermSubject { get; set; }
        public String SearchTermStudent { get; set; }

        [TempData]
        public String MessageSuccess { get; set; }
        [TempData]
        public String MessageError { get; set; }

    }
}
