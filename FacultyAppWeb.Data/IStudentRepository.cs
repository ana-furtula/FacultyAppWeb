using FacultyAppWeb.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.RepositoryServices.Interfaces
{
    public interface IStudentRepository
    {
        Student GetById(long id);
        IEnumerable<Student> GetStudentsByIndex(string index);
        Student Update(Student updated);

        Student Add(Student student);
        Student GetStudentByJMBG(string JMBG);
        Student Delete(long id);
    }
}
