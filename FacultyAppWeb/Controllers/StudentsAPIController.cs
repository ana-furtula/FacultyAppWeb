using FacultyAppWeb.Domains;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace FacultyAppWeb.Controllers
{
    [Route("api/students.{format}")]
    [ApiController]
    public class StudentsAPIController : ControllerBase
    {
        public IStudentRepository StudentRepository { get; }

        public StudentsAPIController(IStudentRepository studentRepository)
        {
            StudentRepository = studentRepository;
        }

        [HttpGet]
        public string Get(string format)
        {
            
            var students = StudentRepository.GetStudentsByIndex(null);
            students = students ?? new List<Student>();

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

        [HttpGet("{jmbg}")]
        public string Get(string jmbg, string format)
        {
            var student = StudentRepository.GetStudentByJMBG(jmbg);
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

        [HttpPost]
        public void Post([FromBody] Student student)
        {
            StudentRepository.Add(student);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Student student)
        {

            StudentRepository.Update(student);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            StudentRepository.Delete(id);
        }
    }
}
