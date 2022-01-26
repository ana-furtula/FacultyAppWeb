using FacultyAppWeb.Domains;
using FacultyAppWeb.RepositoryServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.RepositoryServices.InMemory
{
    public class InMemoryStudentRepository : IStudentRepository
    {
        List<Student> students;

        public InMemoryStudentRepository()
        {
            students = new List<Student>()
            {
                new Student(1L,"Aleksandra","Furtula","2018/0175","0207999285019","email"),
                new Student(2L,"Filip","Furtula","2014/0155","2512995280020","email"),
                new Student(3L,"Matija","Furtula","2012/0100","2102994280021","email")
            };
        }

        public int Commit()
        {
            return 0;
        }

        public Student GetById(long id)
        {
            return students.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Student> GetStudentsByIndex(string index)
        {
            if (string.IsNullOrEmpty(index))
                return students;

            return from s in students
                   where s.Index.StartsWith(index)
                   orderby s.FirstName
                   select s;
        }

        public Student Update(Student updated)
        {
            Student student = students.SingleOrDefault(s => s.Id == updated.Id);
            if (student != null)
            {
                student.FirstName = updated.FirstName;
                student.LastName = updated.LastName;
            }
            return student;
        }

        public Student Add(Student student)
        {
            students.Add(student);
            student.Id = students.Max(s => s.Id) + 1;
            return student;
        }

        public Student GetStudentByJMBG(string JMBG)
        {
            return students.SingleOrDefault(s => s.JMBG.Equals(JMBG));
        }

        public Student Delete(long id)
        {
            Student student = students.SingleOrDefault(s => s.Id == id);

            if (student != null)
                students.Remove(student);

            return student;
        }

        public IEnumerable<Student> GetStudentsByIndex(StudentParameters param, string index)
        {
            throw new NotImplementedException();
        }

        PagedList<Student> IStudentRepository.GetStudentsByIndex(StudentParameters param, string index)
        {
            throw new NotImplementedException();
        }

        public int GetStudentsNumber(string index)
        {
            throw new NotImplementedException();
        }
    }
}
