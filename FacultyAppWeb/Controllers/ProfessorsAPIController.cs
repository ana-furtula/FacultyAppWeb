using FacultyAppWeb.Domains;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace FacultyAppWeb.Controllers
{
    [Route("api/professors")]
    [ApiController]
    public class ProfessorsAPIController : ControllerBase
    {
        private readonly IProfessorRepository professorRepository;

        public ProfessorsAPIController(IProfessorRepository professorRepository)
        {
            this.professorRepository = professorRepository;
        }

        [HttpGet("{format}")]
        public string Get(string format)
        {
            try
            {
                var profs = professorRepository.GetProfessorsByName(null);
                profs = profs != null ? profs.ToList() : new List<Professor>();

                if (format.Equals("xml"))
                {
                    string xml = "";
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Professor>));
                    using (var sww = new StringWriter())
                    {
                        using (XmlWriter writer = XmlWriter.Create(sww))
                        {
                            serializer.Serialize(writer, profs);
                            xml = sww.ToString();
                        }
                    }
                    return xml;
                }

                return JsonSerializer.Serialize(profs);
            }
            catch (Exception ex)
            {
                return "Could not get professors.";
            }


        }

        [HttpGet("{format}/{id}")]
        public string Get(long id, string format)
        {
            try
            {
                var prof = professorRepository.GetById(id);
                if (prof == null)
                {
                    return $"Professor with ID: {id} not found";
                }
                if (format.Equals("xml"))
                {
                    string xml = "";
                    XmlSerializer serializer = new XmlSerializer(typeof(Professor));
                    using (var sww = new StringWriter())
                    {
                        using (XmlWriter writer = XmlWriter.Create(sww))
                        {
                            serializer.Serialize(writer, prof);
                            xml = sww.ToString();
                        }
                    }
                    return xml;
                }

                return JsonSerializer.Serialize(prof);
            }
            catch (Exception ex)
            {
                return "Could not get student.";
            }

        }

        [HttpPost]
        public string Post([FromBody] Professor professor)
        {
            try
            {
                professorRepository.Add(professor);
                return "Professor successfully saved.";
            }
            catch (Exception)
            {
                return "Could not save professor.";
            }
        }

        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Professor professor)
        {
            try
            {
                professor.Id = id;
                professorRepository.Update(professor);
                return "Professor updated successfully.";
            }
            catch (Exception)
            {
                return "Could not update professor.";
            }

        }

        [HttpDelete("{id}")]
        public string Delete(long id)
        {
            try
            {
                professorRepository.Delete(id);
                return "Professor deleted successfully.";
            }
            catch (Exception)
            {
                return "Could not delete professor.";
            }
        }

    }
}
