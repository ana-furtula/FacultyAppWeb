using FacultyAppWeb.Domains;
using FacultyAppWeb.Models.ExamRegistrations;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FacultyAppWeb.Controllers
{
    [Authorize]
    public class ExamRegistrationsController : Controller
    {
        private readonly IExamRegistrationRepository examRegistrationRepository;
        private readonly ILectureRepository lectureRepository;
        private readonly IProfessorRepository professorRepository;
        private readonly ISubjectRepository subjectRepository;
        private readonly IStudentRepository studentRepository;
        private readonly UserManager<IdentityUser> userManager;

        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }
        [TempData]
        public string MessageCreate { get; set; }

        public ExamRegistrationsController(IExamRegistrationRepository examRegistrationRepository, ILectureRepository lectureRepository, IProfessorRepository professorRepository, ISubjectRepository subjectRepository, IStudentRepository studentRepository, UserManager<IdentityUser> userManager)
        {
            this.examRegistrationRepository = examRegistrationRepository;
            this.lectureRepository = lectureRepository;
            this.professorRepository = professorRepository;
            this.subjectRepository = subjectRepository;
            this.studentRepository = studentRepository;
            this.userManager = userManager;
        }

        [HttpGet("examRegistrations")]
        public IActionResult Index([FromQuery] ExamRegistrationParameters examParameters, int pageNumber = 1, string searchTermStudent = null, string searchTermSubject = null)
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                Professor professor = null;
                Student student = null;
                List<Subject> subjects = null;
                examParameters.PageNumber = pageNumber;
                professor = professorRepository.GetProfessorsByName("").Where(p => p.Email.Equals(userEmail)).FirstOrDefault();
                IEnumerable<ExamRegistration> ers;
                bool hasNext;
                if (professor != null)
                {
                    var profSubjects = lectureRepository.GetSubjectsForProfessor(professor);
                    subjects = profSubjects != null ? profSubjects.ToList() : new List<Subject>();
                    ers = examRegistrationRepository.GetAllForProfessor(examParameters, searchTermSubject, searchTermStudent, subjects, out hasNext);
                }
                else
                {
                    student = studentRepository.GetStudentsByIndex("").Where(s => s.Email.Equals(userEmail)).FirstOrDefault();
                    if (student != null)
                    {
                        ers = examRegistrationRepository.GetAllForStudent(examParameters, searchTermSubject, student, out hasNext);
                    }
                    else
                    {
                        ers = examRegistrationRepository.GetAll(examParameters, searchTermSubject, searchTermStudent, out hasNext);
                    }
                }
                

                ExamRegistrationsViewModel ersViewModel = new()
                {
                    SearchTermStudent = searchTermStudent,
                    PageNumber = pageNumber,
                    SearchTermSubject = searchTermSubject,
                    ExamRegistrations = ers.Any() ? ers.ToList() : null,
                    CurrentUserEmail = userEmail,
                    Subjects = subjects,
                    HasNext = hasNext,
                    MessageSuccess = MessageSuccess,
                    MessageError = MessageError
                };

                return View(ersViewModel);

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return RedirectToAction(nameof(HomeController.Error));
        }

        [HttpGet("newER")]
        [Authorize(Roles = "Admin, Student")]
        public IActionResult Create(long id)
        {
            try
            {
                var ers = examRegistrationRepository.GetAll().Where(er => (er.Student.Id == id));

                var subjects = subjectRepository.GetSubjectsByName("");

                var selectListSubjects = new List<SelectListItem>();
                foreach (var element in subjects)
                {
                    if (!ers.Where(er => (er.Subject.Id == element.Id && er.Grade != null && er.Grade > 5)).Any())
                    {
                        selectListSubjects.Add(new SelectListItem
                        {
                            Value = element.Id.ToString(),
                            Text = element.Name + ", ESPB: " + element.ESPB + ", Semester: " + element.Semester
                        });
                    }
                }

                return View(new CreateExamRegistrationViewModel()
                {
                    Student = studentRepository.GetById(id),
                    Subjects = selectListSubjects
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return View();
            }

        }

        [HttpPost("newER")]
        [Authorize(Roles = "Admin, Student")]
        public IActionResult Create(CreateExamRegistrationViewModel newER)
        {
            try
            {
                if (!string.IsNullOrEmpty(newER.SelectedSubject))
                {
                    var subject = subjectRepository.GetById(long.Parse(newER.SelectedSubject));
                    var er = new ExamRegistration()
                    {
                        Subject = subject,
                        Student = studentRepository.GetById(newER.Student.Id),
                        RegistrationDate = DateTime.Now,
                        IsLocked = false,
                        Professor = null,
                        Grade = null,
                        ExamDate = null
                    };

                    if (!examRegistrationRepository.Exists(er))
                    {
                        examRegistrationRepository.Add(er);
                        TempData["MessageSuccess"] = "Exam registration successfully saved!";

                        return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Students", action = "Details", id = newER.Student.Id, messageSuccess = MessageSuccess }));
                    }
                    else
                    {
                        TempData["MessageError"] = "Exam registration already exists!";

                    }
                }
                else
                {
                    TempData["MessageError"] = "Exam registration cannot be saved!";

                }

                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Students", action = "Details", id = newER.Student.Id, messageSuccess = "", messageError = MessageError }));

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return RedirectToAction(nameof(HomeController.Error));
            }

        }

        [HttpGet("deleteER")]
        [Authorize(Roles = "Admin, Student")]
        public IActionResult Delete(long id)
        {
            try
            {
                examRegistrationRepository.Delete(id);
                TempData["MessageSuccess"] = "Exam registration successfully deleted!";
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                TempData["MessageError"] = "Exam registration cannot be deleted!";
            }
            return RedirectToAction(nameof(ExamRegistrationsController.Index));

        }

        [HttpGet("lockER")]
        [Authorize(Roles = "Admin, Professor")]
        public IActionResult Lock(long id)
        {
            try
            {
                examRegistrationRepository.Lock(id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                TempData["MessageError"] = "Exam registration cannot be locked!";
            }
            return RedirectToAction(nameof(ExamRegistrationsController.Index));

        }

        [HttpGet("editER")]
        [Authorize(Roles = "Admin, Professor")]
        public IActionResult Edit(long id)
        {
            try
            {
                ExamRegistration examRegistration = examRegistrationRepository.GetById(id);

                var professors = lectureRepository.GetProfessorsForSubject(examRegistration.Subject.Id);
                var selectListProfessors = new List<SelectListItem>();
                foreach (var element in professors)
                {
                    selectListProfessors.Add(new SelectListItem
                    {
                        Value = element.Id.ToString(),
                        Text = element.FirstName + " " + element.LastName + ", JMBG: " + element.JMBG,
                    });
                }
                return View(new EditExamRegistrationViewModel()
                {
                    ExamRegistration = examRegistration,
                    Professors = selectListProfessors,
                    MessageError = MessageError
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return RedirectToAction(nameof(HomeController.Error));
        }

        [HttpPost("editER")]
        [Authorize(Roles = "Admin, Professor")]
        public IActionResult Edit(EditExamRegistrationViewModel updated)
        {
            if (updated.ExamRegistration.Grade > 10 || updated.ExamRegistration.Grade < 5)
            {
                MessageError = "Invalid input";
                return RedirectToAction(nameof(ExamRegistrationsController.Edit), new { id = updated.ExamRegistration.Id });
            }
            try
            {
                var er = examRegistrationRepository.GetById(updated.ExamRegistration.Id);
                er.Grade = updated.ExamRegistration.Grade;
                er.IsLocked = updated.ExamRegistration.IsLocked;
                er.ExamDate = DateTime.Now;
                er.Professor = professorRepository.GetById(long.Parse(updated.SelectedProfessor));
                examRegistrationRepository.Update(er);
                TempData["MessageSuccess"] = "Exam registration successfully updated!";

                return RedirectToAction(nameof(ExamRegistrationsController.Index));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return RedirectToAction(nameof(HomeController.Error));
            }
        }

    }
}
