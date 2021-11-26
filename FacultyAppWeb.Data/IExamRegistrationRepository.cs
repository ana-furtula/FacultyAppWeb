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
        IEnumerable<ExamRegistration> GetExamRegistrationsBySubjectName(string subjectName);
        IEnumerable<ExamRegistration> GetExamRegistrationsByProfessorName(string professorName);
        IEnumerable<ExamRegistration> GetExamRegistrationsByStudentIndex(string studentIndex);
        IEnumerable<ExamRegistration> GetExamRegistrationsByProfessorId(string professorId);
        IEnumerable<ExamRegistration> GetExamRegistrationsBySubjectId(string subjectId);
        IEnumerable<ExamRegistration> GetExamRegistrationsByStudentId(string studentId);
        ExamRegistration Add(ExamRegistration examRegistration);
        ExamRegistration Delete(long id);
        ExamRegistration Update(ExamRegistration er);
        public void Lock(long id);
    }
}
