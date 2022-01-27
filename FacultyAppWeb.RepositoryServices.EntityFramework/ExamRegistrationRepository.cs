using FacultyAppWeb.Domains;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.RepositoryServices.EntityFramework
{
    public class ExamRegistrationRepository : IExamRegistrationRepository
    {
        private readonly FacultyContext dbContext;

        public ExamRegistrationRepository(FacultyContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public ExamRegistration Add(ExamRegistration examRegistration)
        {
            try
            {
                dbContext.Add(examRegistration);
                dbContext.SaveChanges();
                return examRegistration;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExamRegistration Delete(long id)
        {
            try
            {
                var query = dbContext.ExamRegistrations
                            .Include(x => x.Student)
                            .Include(x => x.Subject)
                            .Include(x => x.Professor != null ? x.Professor : null)
                            .Where(x => x.Id == id);

                var exam = query.FirstOrDefault();
                dbContext.Remove(exam);
                dbContext.SaveChanges();
                return exam;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(ExamRegistration examRegistration)
        {
            try
            {
                var ers = dbContext.ExamRegistrations
                          .Include(x => x.Student)
                          .Include(x => x.Subject)
                          .Where(x => x.Student.Id == examRegistration.Student.Id && x.Subject.Id == examRegistration.Subject.Id && (x.Grade == null || x.IsLocked == false || (x.IsLocked == true && x.Grade > 5)));

                return ers.Any();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ExamRegistration> GetAll()
        {
            try
            {
                var query = dbContext.ExamRegistrations
                            .Include(x => x.Student)
                            .Include(x => x.Subject)
                            .Include(x => x.Professor != null ? x.Professor : null);

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ExamRegistration> GetAll(ExamRegistrationParameters param, string searchTermSubject, string searchTermStudent, out bool hasNext)
        {
            try
            {
                var query = dbContext.ExamRegistrations
                            .Include(x => x.Student)
                            .Include(x => x.Subject)
                            .Include(x => x.Professor != null ? x.Professor : null)
                            .Where(x => (searchTermStudent == null || x.Student.Index.ToLower().StartsWith(searchTermStudent.ToLower())) && (searchTermSubject == null || x.Subject.Name.ToLower().StartsWith(searchTermSubject.ToLower())))
                            .Skip((param.PageNumber - 1) * param.PageSize)
                            .Take(param.PageSize);

                var queryNext = dbContext.ExamRegistrations
                            .Include(x => x.Student)
                            .Include(x => x.Subject)
                            .Include(x => x.Professor != null ? x.Professor : null)
                            .Where(x => (searchTermStudent == null || x.Student.Index.ToLower().StartsWith(searchTermStudent.ToLower())) && (searchTermSubject == null || x.Subject.Name.ToLower().StartsWith(searchTermSubject.ToLower())))
                            .Skip(((param.PageNumber - 1) * param.PageSize) + query.Count())
                            .Take(1);
                hasNext = queryNext.Any();
                
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExamRegistration GetById(long id)
        {
            try
            {
                var query = dbContext.ExamRegistrations
                            .Include(x => x.Student)
                            .Include(x => x.Subject)
                            .Include(x => x.Professor != null ? x.Professor : null)
                            .Where(x => x.Id == id);

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<ExamRegistration> GetExamRegistrationsByProfessorId(long professorId)
        {
            try
            {
                var query = dbContext.ExamRegistrations
                            .Include(x => x.Student)
                            .Include(x => x.Subject)
                            .Include(x => x.Professor)
                            .Where(x => x.Professor.Id == professorId);
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ExamRegistration> GetExamRegistrationsByProfessorName(string professorName)
        {
            try
            {
                if (string.IsNullOrEmpty(professorName))
                {
                    return GetAll();
                }

                var query = dbContext.ExamRegistrations
                        .Include(x => x.Student)
                        .Include(x => x.Subject)
                        .Include(x => x.Professor != null ? x.Professor : null)
                        .Where(x => x.Professor != null && x.Professor.FirstName.StartsWith(professorName));

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ExamRegistration> GetExamRegistrationsByStudentId(long studentId)
        {
            try
            {
                var query = dbContext.ExamRegistrations
                        .Include(x => x.Student)
                        .Include(x => x.Subject)
                        .Include(x => x.Professor != null ? x.Professor : null)
                        .Where(x => x.Student.Id == studentId);

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ExamRegistration> GetExamRegistrationsByStudentIndex(string studentIndex)
        {
            try
            {
                if (string.IsNullOrEmpty(studentIndex))
                {
                    return GetAll();
                }

                var query = dbContext.ExamRegistrations
                        .Include(x => x.Student)
                        .Include(x => x.Subject)
                        .Include(x => x.Professor != null ? x.Professor : null)
                        .Where(x => x.Student.Index.StartsWith(studentIndex));

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PagedList<ExamRegistration> GetExamRegistrationsByStudentIndex(ExamRegistrationParameters param, string studentIndex)
        {
            try
            {
                var query = dbContext.ExamRegistrations
                       .Include(x => x.Student)
                       .Include(x => x.Subject)
                       .Include(x => x.Professor != null ? x.Professor : null)
                       .Where(x => string.IsNullOrEmpty(studentIndex) || x.Student.Index.StartsWith(studentIndex));

                return PagedList<ExamRegistration>.ToPagedList(query,
        param.PageNumber,
        param.PageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ExamRegistration> GetExamRegistrationsBySubjectId(long subjectId)
        {
            try
            {
                var query = dbContext.ExamRegistrations
                        .Include(x => x.Student)
                        .Include(x => x.Subject)
                        .Include(x => x.Professor != null ? x.Professor : null)
                        .Where(x => x.Subject.Id == subjectId);

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ExamRegistration> GetExamRegistrationsBySubjectName(string subjectName)
        {
            try
            {
                if (string.IsNullOrEmpty(subjectName))
                {
                    return GetAll();
                }

                var query = dbContext.ExamRegistrations
                        .Include(x => x.Student)
                        .Include(x => x.Subject)
                        .Include(x => x.Professor != null ? x.Professor : null)
                        .Where(x => x.Subject.Name.StartsWith(subjectName));

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PagedList<ExamRegistration> GetExamRegistrationsBySubjectName(ExamRegistrationParameters param, string subjectName)
        {
            try
            {
                var query = dbContext.ExamRegistrations
                         .Include(x => x.Student)
                         .Include(x => x.Subject)
                         .Include(x => x.Professor != null ? x.Professor : null)
                         .Where(x => (string.IsNullOrEmpty(subjectName)) || x.Subject.Name.StartsWith(subjectName));

                return PagedList<ExamRegistration>.ToPagedList(query,
         param.PageNumber,
         param.PageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetTotalRegistrationNumber(string subjectName, string index)
        {

            var query = dbContext.ExamRegistrations
                        .Include(x => x.Student)
                        .Include(x => x.Subject)
                        .Include(x => x.Professor != null ? x.Professor : null)
                        .Where(x => (string.IsNullOrEmpty(subjectName) || x.Subject.Name.StartsWith(subjectName)) && (string.IsNullOrEmpty(index) || x.Student.Index.StartsWith(index)));

            return query.Count();
        }

        public void Lock(long id)
        {
            try
            {
                var er = dbContext.ExamRegistrations.Find(id);
                er.IsLocked = true;
                dbContext.Update(er);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExamRegistration Update(ExamRegistration er)
        {
            try
            {
                dbContext.Update(er);
                dbContext.SaveChanges();
                return er;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<ExamRegistration> GetAllForProfessor(ExamRegistrationParameters param, string searchTermSubject, string searchTermStudent, List<Subject> subjects, out bool hasNext)
        {
            try
            {
                var query = dbContext.ExamRegistrations
                            .Include(x => x.Student)
                            .Include(x => x.Subject)
                            .Include(x => x.Professor != null ? x.Professor : null)
                            .Where(x => subjects.Contains(x.Subject) && (searchTermStudent == null || x.Student.Index.ToLower().StartsWith(searchTermStudent.ToLower())) && (searchTermSubject == null || x.Subject.Name.ToLower().StartsWith(searchTermSubject.ToLower())))
                            .Skip((param.PageNumber - 1) * param.PageSize)
                            .Take(param.PageSize);

                var queryNext = dbContext.ExamRegistrations
                            .Include(x => x.Student)
                            .Include(x => x.Subject)
                            .Include(x => x.Professor != null ? x.Professor : null)
                            .Where(x => subjects.Contains(x.Subject) && (searchTermStudent == null || x.Student.Index.ToLower().StartsWith(searchTermStudent.ToLower())) && (searchTermSubject == null || x.Subject.Name.ToLower().StartsWith(searchTermSubject.ToLower())))
                            .Skip(((param.PageNumber - 1) * param.PageSize) + query.Count())
                            .Take(1);

                hasNext = queryNext.Any();

                return query;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<ExamRegistration> GetAllForStudent(ExamRegistrationParameters param, string searchTermSubject, Student student, out bool hasNext)
        {
            try
            {
                var query = dbContext.ExamRegistrations
                            .Include(x => x.Student)
                            .Include(x => x.Subject)
                            .Include(x => x.Professor != null ? x.Professor : null)
                            .Where(x => (x.Student.Id == student.Id) && (searchTermSubject == null || x.Subject.Name.ToLower().StartsWith(searchTermSubject.ToLower())))
                            .Skip((param.PageNumber - 1) * param.PageSize)
                            .Take(param.PageSize);

                var queryNext = dbContext.ExamRegistrations
                           .Include(x => x.Student)
                           .Include(x => x.Subject)
                           .Include(x => x.Professor != null ? x.Professor : null)
                           .Where(x => x.Student.Id == student.Id && (searchTermSubject == null || x.Subject.Name.ToLower().StartsWith(searchTermSubject.ToLower())))
                           .Skip(((param.PageNumber - 1) * param.PageSize) + query.Count())
                           .Take(1);

                hasNext = queryNext.Any();

                return query;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
