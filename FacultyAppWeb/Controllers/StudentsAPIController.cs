using FacultyAppWeb.Domains;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace FacultyAppWeb.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsAPIController : ControllerBase
    {
        public IStudentRepository StudentRepository { get; }

        public StudentsAPIController(IStudentRepository studentRepository)
        {
            StudentRepository = studentRepository;
        }

        [HttpGet("{format}")]
        public string Get(string format)
        {
            try
            {
                var students = StudentRepository.GetStudentsByIndex(null);
                students = students != null ? students.ToList(): new List<Student>();

                if (format.Equals("xml"))
                {
                    string xml = "";
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                    using (var sww = new StringWriter())
                    {
                        using (XmlWriter writer = XmlWriter.Create(sww))
                        {
                            serializer.Serialize(writer, students);
                            xml = sww.ToString();
                        }
                    }
                    return xml;
                }

                return JsonSerializer.Serialize(students);
            }
            catch (Exception ex)
            {
                return "Could not get students.";
            }


        }

        [HttpGet("{format}/{jmbg}")]
        public string Get(string jmbg, string format)
        {
            try
            {
                var student = StudentRepository.GetStudentByJMBG(jmbg);
                if (student == null)
                {
                    return $"Student with JMBG: {jmbg} not found";
                }
                if (format.Equals("xml"))
                {
                    string xml = "";
                    XmlSerializer serializer = new XmlSerializer(typeof(Student));
                    using (var sww = new StringWriter())
                    {
                        using (XmlWriter writer = XmlWriter.Create(sww))
                        {
                            serializer.Serialize(writer, student);
                            xml = sww.ToString();
                        }
                    }
                    return xml;
                }

                return JsonSerializer.Serialize(student);
            }
            catch (Exception ex)
            {
                return "Could not get student.";
            }

        }

        [HttpPost]
        public string Post([FromBody] Student student)
        {
            try
            {
                StudentRepository.Add(student);
                return "Student successfully saved.";
            }
            catch (Exception)
            {
                return "Could not save student.";
            }
        }

        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Student student)
        {
            try
            {
                student.Id = id;
                StudentRepository.Update(student);
                return "Student updated successfully.";
            }
            catch (Exception)
            {
                return "Could not update student.";
            }

        }

        [HttpDelete("{id}")]
        public string Delete(long id)
        {
            try
            {
                StudentRepository.Delete(id);
                return "Student deleted successfully.";
            }
            catch (Exception)
            {
                return "Could not delete student.";
            }
        }

    }
}
