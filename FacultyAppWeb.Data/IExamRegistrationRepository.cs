using FacultyAppWeb.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.RepositoryServices.Interfaces
{
    public interface IExamRegistrationRepository
    {
        bool Exists(ExamRegistration examRegistration);
        ExamRegistration GetById(long id);
        public IEnumerable<ExamRegistration> GetAll();
        IEnumerable<ExamRegistration> GetAll(ExamRegistrationParameters param, string searchTermSubject, string searchTermStudent, out bool hasNext);
        IEnumerable<ExamRegistration> GetExamRegistrationsBySubjectName(string subjectName);
        PagedList<ExamRegistration> GetExamRegistrationsBySubjectName(ExamRegistrationParameters param, string subjectName);
        IEnumerable<ExamRegistration> GetExamRegistrationsByProfessorName(string professorName);
        IEnumerable<ExamRegistration> GetExamRegistrationsByStudentIndex(string studentIndex);
        PagedList<ExamRegistration> GetExamRegistrationsByStudentIndex(ExamRegistrationParameters param, string index);
        IEnumerable<ExamRegistration> GetExamRegistrationsByProfessorId(long professorId);
        IEnumerable<ExamRegistration> GetExamRegistrationsBySubjectId(long subjectId);
        IEnumerable<ExamRegistration> GetExamRegistrationsByStudentId(long studentId);
        ExamRegistration Add(ExamRegistration examRegistration);
        ExamRegistration Delete(long id);
        ExamRegistration Update(ExamRegistration er);
        public void Lock(long id);
        int GetTotalRegistrationNumber(string subjectName, string index);
        IEnumerable<ExamRegistration> GetAllForProfessor(ExamRegistrationParameters examParameters, string searchTermSubject, string searchTermStudent, List<Subject> subjects, out bool hasNext);
        IEnumerable<ExamRegistration> GetAllForStudent(ExamRegistrationParameters examParameters, string searchTermSubject, Student student, out bool hasNext);
    }
}
