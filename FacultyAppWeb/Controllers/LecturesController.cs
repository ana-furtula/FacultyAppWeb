using FacultyAppWeb.Domains;
using FacultyAppWeb.Models.Lectures;
using FacultyAppWeb.Models.Subjects;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacultyAppWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LecturesController : Controller
    {
        private readonly ILectureRepository lectureRepository;
        private readonly IProfessorRepository professorRepository;
        private readonly ISubjectRepository subjectRepository;

        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }
        [TempData]
        public string MessageCreate { get; set; }

        public LecturesController(ILectureRepository lectureRepository, IProfessorRepository professorRepository, ISubjectRepository subjectRepository)
        {
            this.lectureRepository = lectureRepository;
            this.professorRepository = professorRepository;
            this.subjectRepository = subjectRepository;
        }

        [HttpGet("lectures")]
        public IActionResult Index(string searchTerm = null)
        {
            try
            {
                var lectures = lectureRepository.GetLecturesBySubjectName(searchTerm);
                LecturesViewModel lecturesViewModel = new()
                {
                    SearchTerm = searchTerm,
                    Lectures = (lectures!=null)? lectures.ToList():null,
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


        [HttpGet("deleteLecture")]
        public IActionResult Delete(long id)
        {
            try
            {
                lectureRepository.Delete(id);
                TempData["MessageSuccess"] = "Lecture successfully deleted!";
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                TempData["MessageError"] = "Lecture cannot be deleted!";
            }
            return RedirectToAction(nameof(LecturesController.Index));

        }

        [HttpGet("newLecture")]
        public IActionResult Create()
        {
            try
            {
                var professors = professorRepository.GetProfessorsByName("");
                var selectListProfessors = new List<SelectListItem>();
                foreach (var element in professors)
                {
                    selectListProfessors.Add(new SelectListItem
                    {
                        Value = element.Id.ToString(),
                        Text = element.FirstName + " " + element.LastName + ", JMBG: " + element.JMBG
                    });
                }

                var subjects = subjectRepository.GetSubjectsByName("");
                var selectListSubjects = new List<SelectListItem>();
                foreach (var element in subjects)
                {
                    selectListSubjects.Add(new SelectListItem
                    {
                        Value = element.Id.ToString(),
                        Text = element.Name + ", ESPB: " + element.ESPB + ", Semester: " + element.Semester
                    });
                }

                return View(new CreateLectureViewModel()
                {
                    Professors = selectListProfessors,
                    Subjects = selectListSubjects
                });
            } catch(Exception ex)
            {
                return View();
            }

        }

        [HttpPost("newLecture")]
        public IActionResult Create(CreateLectureViewModel newLecture)
        {
            try
            {
                if(!string.IsNullOrEmpty(newLecture.SelectedSubject) && !string.IsNullOrEmpty(newLecture.SelectedProfessor))
                {
                    var professor = professorRepository.GetById(long.Parse(newLecture.SelectedProfessor));
                    var subject = subjectRepository.GetById(long.Parse(newLecture.SelectedSubject));
                    var lecture = new Lecture()
                    {
                        Subject = subject,
                        Professor = professor
                    };

                    if (!lectureRepository.Exists(lecture))
                    {
                        lectureRepository.Add(lecture);
                        TempData["MessageSuccess"] = "Lecture successfully saved!";
                    }
                    else
                    {
                        TempData["MessageError"] = "Lecture already exists!";
                    }
                }
                else
                {
                    TempData["MessageError"] = "Lecture cannot be saved!";
                }
                
                return RedirectToAction(nameof(LecturesController.Index));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return RedirectToAction(nameof(HomeController.Error));
            }

        }

    }
}
