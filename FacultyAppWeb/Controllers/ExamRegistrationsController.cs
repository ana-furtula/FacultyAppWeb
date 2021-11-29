using FacultyAppWeb.Domains;
using FacultyAppWeb.Models.ExamRegistrations;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacultyAppWeb.Controllers
{
    public class ExamRegistrationsController : Controller
    {
        private readonly IExamRegistrationRepository examRegistrationRepository;
        private readonly ILectureRepository lectureRepository;
        private readonly IProfessorRepository professorRepository;
        private readonly ISubjectRepository subjectRepository;
        private readonly IStudentRepository studentRepository;

        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }
        [TempData]
        public string MessageCreate { get; set; }

        public ExamRegistrationsController(IExamRegistrationRepository examRegistrationRepository, ILectureRepository lectureRepository, IProfessorRepository professorRepository, ISubjectRepository subjectRepository, IStudentRepository studentRepository)
        {
            this.examRegistrationRepository = examRegistrationRepository;
            this.lectureRepository = lectureRepository;
            this.professorRepository = professorRepository;
            this.subjectRepository = subjectRepository;
            this.studentRepository = studentRepository;
        }

        [HttpGet("examRegistrations")]
        public IActionResult Index(string searchTermStudent = null, string searchTermSubject = null)
        {
            try
            {
                var ers = examRegistrationRepository.GetAll();
                ExamRegistrationsViewModel ersViewModel = new()
                {
                    SearchTermStudent = searchTermStudent,
                    SearchTermSubject = searchTermSubject,
                    ExamRegistrations = ers.Any()?ers.Where(er => (searchTermStudent == null || er.Student.Index.ToLower().StartsWith(searchTermStudent)) && (searchTermSubject == null || er.Subject.Name.ToLower().StartsWith(searchTermSubject))).OrderBy(er=> er.IsLocked).ToList():null,
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
        public IActionResult Create()
        {
            try
            {
                var students = studentRepository.GetStudentsByIndex("");
                var selectListStudents = new List<SelectListItem>();
                foreach (var element in students)
                {
                    selectListStudents.Add(new SelectListItem
                    {
                        Value = element.Id.ToString(),
                        Text = "Indeks: " + element.Index + ", " + element.FirstName + " " + element.LastName
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

                return View(new CreateExamRegistrationViewModel()
                {
                    Students = selectListStudents,
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
        public IActionResult Create(CreateExamRegistrationViewModel newER)
        {
            try
            {
                if (!string.IsNullOrEmpty(newER.SelectedSubject) && !string.IsNullOrEmpty(newER.SelectedStudent))
                {
                    var student = studentRepository.GetById(long.Parse(newER.SelectedStudent));
                    var subject = subjectRepository.GetById(long.Parse(newER.SelectedSubject));
                    var er = new ExamRegistration()
                    {
                        Subject = subject,
                        Student = student,
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

                return RedirectToAction(nameof(ExamRegistrationsController.Index));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return RedirectToAction(nameof(HomeController.Error));
            }

        }

        [HttpGet("deleteER")]
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
        public IActionResult Edit(EditExamRegistrationViewModel updated)
        {
            if (updated.ExamRegistration.Grade>10 || updated.ExamRegistration.Grade<5)
            {
                MessageError = "Invalid input";
                return RedirectToAction(nameof(ExamRegistrationsController.Edit), new { id = updated.ExamRegistration.Id});
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
