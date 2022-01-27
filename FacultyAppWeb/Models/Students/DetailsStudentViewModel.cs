using FacultyAppWeb.Domains;
using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Models.Students
{
    public class DetailsStudentViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Index { get; set; }
        public string JMBG { get; set; }
        public string Email { get; set; }
        public List<ExamRegistration> PassedExams { get; set; }
        public List<ExamRegistration> FailedExams { get; set; }
        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }
    }
}
