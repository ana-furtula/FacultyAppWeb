using FacultyAppWeb.Domains;
using FacultyAppWeb.Models.Students;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FacultyAppWeb.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;

        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }
        [TempData]
        public string MessageCreate { get; set; }


        public StudentsController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        [AcceptVerbs("GET")]
        [Authorize(Roles = "Admin")]
        public IActionResult VerifyIndex(string index)
        {

            var rx = new Regex("[0-9]{4}/[0-9]{4}");

            if (!rx.IsMatch(index))
                return Json($"Index {index} is not valid.");
            if (studentRepository.GetStudentsByIndex(index).Any())
            {
                return Json($"Student with index {index} already exists.");
            }

            return Json(true);
        }

        [AcceptVerbs("GET")]
        [Authorize(Roles = "Admin")]
        public IActionResult VerifyJMBG(string jmbg)
        {

            var rx = new Regex("[0-9]{13}");

            if (!rx.IsMatch(jmbg))
                return Json($"JMBG {jmbg} is not valid.");

            if (studentRepository.GetStudentByJMBG(jmbg) != null)
            {
                return Json($"Student with JMBG {jmbg} already exists.");
            }

            return Json(true);
        }


        [HttpGet("students")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index(string searchTerm = null)
        {
            try
            {

                var studentsViewModel = new StudentsViewModel()
                {
                    SearchTerm = searchTerm,
                    Students = studentRepository.GetStudentsByIndex(searchTerm).ToList(),
                    MessageSuccess = MessageSuccess,
                    MessageError = MessageError
                };

                return View(studentsViewModel);

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return RedirectToAction(nameof(HomeController.Error));
        }

        [HttpGet("studentDetails")]
        public IActionResult Details(long id, string messageSuccess = null, string messageError = null, string email = null)
        {
            try
            {
                Student student = null;

                if (email != null)
                    student = studentRepository.GetStudentsByIndex("").Where(s => (s.Email.Equals(email))).FirstOrDefault();
               
                else if (id != 0)
                    student = studentRepository.GetById(id);

                if (student == null)
                    return RedirectToAction(nameof(HomeController.Error));

                return View(new DetailsStudentViewModel()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Index = student.Index,
                    JMBG = student.JMBG,
                    Email = student.Email,
                    MessageSuccess = MessageSuccess,
                    MessageError = MessageError
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return RedirectToAction(nameof(HomeController.Error));
        }


        [HttpGet("editStudent")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(long id)
        {
            try
            {
                Student student = studentRepository.GetById(id);
                return View(new EditStudentViewModel()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Index = student.Index,
                    JMBG = student.JMBG,
                    Email = student.Email
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return RedirectToAction(nameof(HomeController.Error));
        }

        [HttpPost("editStudent")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(EditStudentViewModel updated)
        {
            if (!ModelState.IsValid)
            {
                return View(updated);
            }
            try
            {
                var student = new Student()
                {
                    Id = updated.Id,
                    LastName = updated.LastName,
                    FirstName = updated.FirstName,
                    Index = updated.Index,
                    JMBG = updated.JMBG,
                    Email = updated.Email
                };
                studentRepository.Update(student);
                TempData["MessageSuccess"] = "Student successfully updated!";
                return RedirectToAction(nameof(StudentsController.Index));
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = "Student cannot be updated!";
                return RedirectToAction(nameof(StudentsController.Index));
            }
        }

        [HttpGet("newStudent")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            try
            {
                return View(new CreateStudentViewModel()
                {
                    MessageCreate = null,
                    Index = "",
                    FirstName = "",
                    JMBG = "",
                    LastName = "",
                    Email = ""
                });
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("newStudent")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CreateStudentViewModel newStudent)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                studentRepository.Add(new Student()
                {
                    Index = newStudent.Index,
                    FirstName = newStudent.FirstName,
                    JMBG = newStudent.JMBG,
                    LastName = newStudent.LastName,
                    Email = newStudent.Email
                });

                TempData["MessageSuccess"] = "Student successfully saved!";
                return RedirectToAction(nameof(StudentsController.Index));
            }
            catch (Exception ex)
            {
                TempData["MessageSuccess"] = "Student cannot be saved!";
                return RedirectToAction(nameof(StudentsController.Index));
            }

        }

        [HttpGet("delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(long id)
        {
            try
            {
                studentRepository.Delete(id);
                TempData["MessageSuccess"] = "Student successfully deleted!";
                return RedirectToAction(nameof(StudentsController.Index));
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = "Student cannot be deleted!";
                return RedirectToAction(nameof(StudentsController.Index));
            }

        }

    }
}
