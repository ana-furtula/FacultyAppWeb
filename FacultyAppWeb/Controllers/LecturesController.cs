using FacultyAppWeb.Models.Lectures;
using FacultyAppWeb.Models.Subjects;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Controllers
{
    public class LecturesController : Controller
    {
        private readonly ILectureRepository lectureRepository;
        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }
        [TempData]
        public string MessageCreate { get; set; }

        public LecturesController(ILectureRepository lectureRepository)
        {
            this.lectureRepository = lectureRepository;
        }

        [HttpGet("lectures")]
        public IActionResult Index(string searchTerm = null)
        {
            try
            {
                LecturesViewModel lecturesViewModel = new()
                {
                    SearchTerm = searchTerm,
                    Lectures = lectureRepository.GetLecturesBySubjectName(searchTerm).ToList(),
                    MessageSuccess = MessageSuccess,
                    MessageError = MessageError
                };

                return View(lecturesViewModel);

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return RedirectToAction(nameof(HomeController.Error));
        }
    }
}
