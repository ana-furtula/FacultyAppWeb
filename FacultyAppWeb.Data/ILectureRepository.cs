using FacultyAppWeb.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.RepositoryServices.Interfaces
{
    public interface ILectureRepository
    {
        bool Exists(Lecture lecture);
        Lecture GetById(long id);
        IEnumerable<Lecture> GetLecturesBySubjectName(string subjectName);
        PagedList<Lecture> GetLecturesBySubjectName(LectureParameters param, string index);
        IEnumerable<Lecture> GetLecturesByProfessorName(string professorName);
        Lecture Add(Lecture lecture);
        Lecture Delete(long id);
        IEnumerable<Professor> GetProfessorsForSubject(long subjectId);

        IEnumerable<Subject> GetSubjectsForProfessor(Professor professor)
        {
            return null;
        }
        int GetTotalLecturesNumber(string searchTerm);
    }
}
