using FacultyAppWeb.Domains;
using FacultyAppWeb.Models.Professors;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Controllers
{
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

        [HttpGet("professors")]
        public IActionResult Index(string searchTerm = null)
        {
            try
            {
                ProfessorsViewModel professorsViewModel = new ProfessorsViewModel()
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
                return View(professor);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return RedirectToAction(nameof(HomeController.Error));
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
                professorRepository.Update(updated);
                TempData["MessageSuccess"] = "Professor successfully updated!";
                return RedirectToAction(nameof(ProfessorsController.Index));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return RedirectToAction(nameof(HomeController.Error));
            }
        }

        [HttpGet("newProfessor")]
        public IActionResult Create()
        {
            return View(new CreateProfessorViewModel()
            {
                MessageCreate = null,
                Professor = new Professor()
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
                if (professorRepository.GetByJMBG(newProfessor.Professor.JMBG) != null)
                {
                    return View(new CreateProfessorViewModel()
                    {
                        MessageCreate = "Professor with this JMBG already exists.",
                        Professor = newProfessor.Professor
                    });
                }

                professorRepository.Add(newProfessor.Professor);
                TempData["MessageSuccess"] = "Professor successfully saved!";

                return RedirectToAction(nameof(ProfessorsController.Index));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return RedirectToAction(nameof(HomeController.Error));
            }

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
                Console.Error.WriteLine(ex);
                TempData["MessageError"] = "Professor cannot be deleted!";
            }
            return RedirectToAction(nameof(ProfessorsController.Index));

        }

    }
}
