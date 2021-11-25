using FacultyAppWeb.Domains;
using FacultyAppWeb.Models;
using FacultyAppWeb.Models.Students;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        [TempData]
        public string Message { get; set; }
        [TempData]
        public string MessageCreate { get; set; }


        public StudentsController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        [HttpGet("students")]
        public IActionResult Index(string searchTerm = null)
        {
            try
            {
                StudentsViewModel studentsViewModel = new StudentsViewModel()
                {
                    SearchTerm = searchTerm,
                    Students = studentRepository.GetStudentsByIndex(searchTerm).ToList(),
                    Message = this.Message
                };

                return View(studentsViewModel);

            } catch(Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return View(null);
        }

        [HttpGet("editStudent")]
        public IActionResult Edit(long id)
        {
            try
            {
                Student student = studentRepository.GetById(id);
                return View(student);
            } catch(Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return RedirectToAction(nameof(HomeController.Error));
        }

        [HttpPost("editStudent")]
        public IActionResult Edit(Student updated)
        {
            if (!ModelState.IsValid)
            {
                return View(updated);
            }
            try
            {
                studentRepository.Update(updated);
                TempData["Message"] = "Student successfully updated!";
                return RedirectToAction(nameof(StudentsController.Index));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return RedirectToAction(nameof(HomeController.Error));
            }
        }

        [HttpGet("newStudent")]
        public IActionResult Create()
        {
            return View(new CreateStudentViewModel()
            {
                MessageCreate = null,
                Student = new Student()
            });
           
        }

        [HttpPost("newStudent")]
        public IActionResult Create(CreateStudentViewModel newStudent)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (studentRepository.GetStudentsByIndex(newStudent.Student.Index).Any())
                {
                    return View(new CreateStudentViewModel()
                    {
                        MessageCreate = "Student with this index already exists.",
                        Student = newStudent.Student
                    });
                }
                if (studentRepository.GetStudentByJMBG(newStudent.Student.JMBG) != null)
                {
                    return View(new CreateStudentViewModel()
                    {
                        MessageCreate = "Student with this JMBG already exists.",
                        Student = newStudent.Student
                    });
                }

                studentRepository.Add(newStudent.Student);
                TempData["Message"] = "Student successfully saved!";

                return RedirectToAction(nameof(StudentsController.Index));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return RedirectToAction(nameof(HomeController.Error));
            }
            
        }

        [HttpGet("delete")]
        public IActionResult Delete(long id)
        {
            try
            {
                studentRepository.Delete(id);
                TempData["Message"] = "Student successfully deleted!";
                return RedirectToAction(nameof(StudentsController.Index));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return RedirectToAction(nameof(HomeController.Error));
            }
           
        }

    }
}
