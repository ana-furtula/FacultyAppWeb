using FacultyAppWeb.Domains;
using FacultyAppWeb.Models.Professors;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FacultyAppWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProfessorsController : Controller
    {
        private readonly IProfessorRepository professorRepository;
        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }

        public ProfessorsController(IProfessorRepository professorRepository)
        {
            this.professorRepository = professorRepository;
        }

        [AcceptVerbs("GET")]
        public IActionResult VerifyJMBG(string jmbg)
        {

            var rx = new Regex("[0-9]{13}");

            if (!rx.IsMatch(jmbg))
                return Json($"JMBG {jmbg} is not valid.");
            if (professorRepository.GetByJMBG(jmbg) != null)
            {
                return Json($"Professor with JMBG {jmbg} already exists.");
            }

            return Json(true);
        }

        [HttpGet("professors")]
        public IActionResult Index(string searchTerm = null)
        {
            try
            {
                var professorsViewModel = new ProfessorsViewModel()
                {
                    SearchTerm = searchTerm,
                    Professors = professorRepository.GetProfessorsByName(searchTerm).ToList(),
                    MessageSuccess = MessageSuccess,
                    MessageError = MessageError
                };

                return View(professorsViewModel);

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return RedirectToAction(nameof(HomeController.Error));
        }

        [HttpGet("editProfessor")]
        public IActionResult Edit(long id)
        {
            try
            {
                Professor professor = professorRepository.GetById(id);
             /*   return View(new EditProfessorViewModel()
                {
                    FirstName = professor.FirstName,
                    LastName = professor.LastName,
                    Id = professor.Id
                });*/
             return View(professor);
            }
            catch (Exception ex)
            {
                //return RedirectToAction(nameof(HomeController.Error));
                TempData["MessageError"] = "Could not load professor";
                return RedirectToAction(nameof(ProfessorsController.Index));
            }
        }

        [HttpPost("editProfessor")]
        public IActionResult Edit(Professor updated)
        {
            if (!ModelState.IsValid)
            {
                return View(updated);
            }
            try
            {
                /*var prof = new Professor()
                {
                    Id = updated.Id,
                    FirstName = updated.FirstName,
                    LastName = updated.LastName
                };*/
                professorRepository.Update(updated);
                TempData["MessageSuccess"] = "Professor successfully updated!";
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = "Professor cannot be updated!";
            }
            return RedirectToAction(nameof(ProfessorsController.Index));
        }

        [HttpGet("newProfessor")]
        public IActionResult Create()
        {
            return View(new CreateProfessorViewModel()
            {
                MessageCreate = null,
                FirstName = "",
                LastName = "",
                JMBG = "",
                Email = ""
            });

        }

        [HttpPost("newProfessor")]
        public IActionResult Create(CreateProfessorViewModel newProfessor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var prof = new Professor()
                {
                    FirstName = newProfessor.FirstName,
                    LastName = newProfessor.LastName,
                    JMBG = newProfessor.JMBG,
                    Email = newProfessor.Email
                };
                professorRepository.Add(prof);

                TempData["MessageSuccess"] = "Professor successfully saved!";
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = "Professor cannot be saved!";
            }
            return RedirectToAction(nameof(ProfessorsController.Index));

        }

        [HttpGet("deleteProfessor")]
        public IActionResult Delete(long id)
        {
            try
            {
                professorRepository.Delete(id);
                TempData["MessageSuccess"] = "Professor successfully deleted!";
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = "Professor cannot be deleted!";
            }
            return RedirectToAction(nameof(ProfessorsController.Index));

        }

    }
}
