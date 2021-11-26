using FacultyAppWeb.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.RepositoryServices.Interfaces
{
    public interface ISubjectRepository
    {
        bool Exists(Subject subject);
        Subject GetById(long id);
        IEnumerable<Subject> GetSubjectsByName(string name);
        IEnumerable<Subject> GetSubjectsBySemester(int semester);
        IEnumerable<Subject> GetSubjectsByESPB(int espb);
        Subject Update(Subject updated);

        Subject Add(Subject subject);
        Subject Delete(long id);
    }
}
