﻿using FacultyAppWeb.Domains;
using FacultyAppWeb.Models.Subjects;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacultyAppWeb.Controllers
{
    [Authorize]
    public class SubjectsController : Controller
    {
        private readonly ISubjectRepository subjectRepository;

        [TempData]
        public string MessageSuccess { get; set; }
        [TempData]
        public string MessageError { get; set; }
        [TempData]
        public string MessageCreate { get; set; }

        public SubjectsController(ISubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        [HttpGet]
        public JsonResult GetMethod()
        {
            var subjects = subjectRepository.GetSubjectsByName("").ToList();
            List<ChartData> data = new();
            foreach(var subject in subjects)
            {
                //int failed = subjectRepository.getNumberOfFailedExams(subject.Id);
                int passed = subjectRepository.getNumberOfPassedExams(subject.Id);
                int total = subjectRepository.getTotalNumberOfGradedExams(subject.Id);
                double percent;
                if (total != 0)
                {
                    percent = ((double)passed / total) * 100;
                }
                else
                {
                    percent = 0;
                }

                data.Add(new ChartData() {
                    Name = subject.Name,
                    PassedPercent = percent
                });
            }
            return Json(data.ToArray());
        }

        [HttpGet("subjects")]
        public IActionResult Index([FromQuery] SubjectParameters subjectParameters, int pageNumber = 1, string searchTerm = null)
        {
            try
            {   subjectParameters.PageNumber = pageNumber;
                SubjectsViewModel subjectsViewModel = new SubjectsViewModel()
                
                {
                    SearchTerm = searchTerm,
                    TotalSubjectNumber = subjectRepository.GetTotalSubjectNumber(searchTerm),
                    Subjects = subjectRepository.GetSubjectsByName(subjectParameters, searchTerm).ToList(),
                    MessageSuccess = MessageSuccess,
                    MessageError = MessageError
                };

                return View(subjectsViewModel);

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return RedirectToAction(nameof(HomeController.Error));
        }

        [HttpGet("editSubject")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(long id)
        {
            try
            {
                Subject subject = subjectRepository.GetById(id);
                return View(subject);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return RedirectToAction(nameof(HomeController.Error));
        }

        [HttpPost("editSubject")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Subject updated)
        {
            if (!ModelState.IsValid)
            {
                return View(updated);
            }
            try
            {
                if (subjectRepository.Exists(updated))
                {
                    TempData["MessageError"] = "Subject already exists!";
                }
                else
                {
                    subjectRepository.Update(updated);
                    TempData["MessageSuccess"] = "Subject successfully updated!";
                }
                return RedirectToAction(nameof(SubjectsController.Index));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return RedirectToAction(nameof(HomeController.Error));
            }
        }

        [HttpGet("newSubject")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new CreateSubjectViewModel()
            {
                MessageCreate = null,
                Subject = new Subject()
            });

        }

        [HttpPost("newSubject")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CreateSubjectViewModel newSubject)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var subjects = subjectRepository.GetSubjectsByName(newSubject.Subject.Name);
                foreach(var subject in subjects)
                {
                    if(subjectRepository.Exists(newSubject.Subject))
                    {
                        return View(new CreateSubjectViewModel()
                        {
                            MessageCreate = "Subject already exists.",
                            Subject = newSubject.Subject
                        });
                    }
                }

                subjectRepository.Add(newSubject.Subject);
                TempData["MessageSuccess"] = "Subject successfully saved!";

                return RedirectToAction(nameof(SubjectsController.Index));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return RedirectToAction(nameof(HomeController.Error));
            }

        }

        [HttpGet("deleteSubject")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(long id)
        {
            try
            {
                subjectRepository.Delete(id);
                TempData["MessageSuccess"] = "Subject successfully deleted!";
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                TempData["MessageError"] = "Subject cannot be deleted!";
            }
            return RedirectToAction(nameof(SubjectsController.Index));

        }

    }
}
