using FacultyAppWeb.Domains;
using FacultyAppWeb.RepositoryServices.Interfaces;
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
                var query = from er in dbContext.ExamRegistrations
                            join student in dbContext.Students
                                on er.Student.Id equals student.Id
                            join subject in dbContext.Subjects
                                on er.Subject.Id equals subject.Id
                            join professor in dbContext.Professors
                                on er.Professor equals professor into ep
                            from subprof in ep.DefaultIfEmpty()
                            where er.Id == id
                            select new ExamRegistration()
                            {
                                Id = er.Id,
                                Student = student,
                                Subject = subject,
                                Professor = subprof ?? null,
                                ExamDate = er.ExamDate ?? null,
                                Grade = er.Grade ?? null,
                                IsLocked = er.IsLocked,
                                RegistrationDate = er.RegistrationDate
                            };

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
                var query = from er in dbContext.ExamRegistrations
                            join student in dbContext.Students
                                on er.Student.Id equals student.Id
                            join subject in dbContext.Subjects
                                on er.Subject.Id equals subject.Id
                            where (student.Id == examRegistration.Student.Id && subject.Id == examRegistration.Subject.Id) && (er.Grade == null || er.Grade > 5 || er.IsLocked == false)
                            select er;
                return query.Any();
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
                var query = from er in dbContext.ExamRegistrations
                            join student in dbContext.Students
                                on er.Student.Id equals student.Id
                            join subject in dbContext.Subjects
                                on er.Subject.Id equals subject.Id
                            join professor in dbContext.Professors
                                on er.Professor equals professor into ep
                            from subprof in ep.DefaultIfEmpty()
                            select new ExamRegistration()
                            {
                                Id = er.Id,
                                Student = student,
                                Subject = subject,
                                Professor = subprof ?? null,
                                ExamDate = er.ExamDate ?? null,
                                Grade = er.Grade ?? null,
                                IsLocked = er.IsLocked,
                                RegistrationDate = er.RegistrationDate
                            };

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PagedList<ExamRegistration> GetAll(ExamRegistrationParameters param)
        {
           return PagedList<ExamRegistration>.ToPagedList((IQueryable<ExamRegistration>)GetAll(),
        param.PageNumber,
        param.PageSize);
        }

        public ExamRegistration GetById(long id)
        {
            try
            {
                var query = from er in dbContext.ExamRegistrations
                            join student in dbContext.Students
                                on er.Student.Id equals student.Id
                            join subject in dbContext.Subjects
                                on er.Subject.Id equals subject.Id
                            join professor in dbContext.Professors
                                on er.Professor equals professor into ep
                            from subprof in ep.DefaultIfEmpty()
                            where er.Id == id
                            select new ExamRegistration()
                            {
                                Id = er.Id,
                                Student = student,
                                Subject = subject,
                                Professor = subprof ?? null,
                                ExamDate = er.ExamDate ?? null,
                                Grade = er.Grade ?? null,
                                IsLocked = er.IsLocked,
                                RegistrationDate = er.RegistrationDate
                            };

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ExamRegistration> GetExamRegistrationsByProfessorId(long professorId)
        {
            try
            {
                var query = from er in dbContext.ExamRegistrations
                            join student in dbContext.Students
                                on er.Student.Id equals student.Id
                            join subject in dbContext.Subjects
                                on er.Subject.Id equals subject.Id
                            join professor in dbContext.Professors
                                on er.Professor equals professor into ep
                            from subprof in ep.DefaultIfEmpty()
                            where subprof!=null && subprof.Id == professorId
                            select new ExamRegistration()
                            {
                                Id = er.Id,
                                Student = student,
                                Subject = subject,
                                Professor = subprof,
                                ExamDate = er.ExamDate ?? null,
                                Grade = er.Grade ?? null,
                                IsLocked = er.IsLocked,
                                RegistrationDate = er.RegistrationDate
                            };

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
                var query = from er in dbContext.ExamRegistrations
                            join student in dbContext.Students
                                on er.Student.Id equals student.Id
                            join subject in dbContext.Subjects
                                on er.Subject.Id equals subject.Id
                            join professor in dbContext.Professors
                                on er.Professor equals professor into ep
                            from subprof in ep.DefaultIfEmpty()
                            where string.IsNullOrEmpty(professorName) || (subprof!=null && subprof.FirstName.StartsWith(professorName))
                            select new ExamRegistration()
                            {
                                Id = er.Id,
                                Student = student,
                                Subject = subject,
                                Professor = subprof ?? null,
                                ExamDate = er.ExamDate ?? null,
                                Grade = er.Grade ?? null,
                                IsLocked = er.IsLocked,
                                RegistrationDate = er.RegistrationDate
                            };

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
                var query = from er in dbContext.ExamRegistrations
                            join student in dbContext.Students
                                on er.Student.Id equals student.Id
                            join subject in dbContext.Subjects
                                on er.Subject.Id equals subject.Id
                            join professor in dbContext.Professors
                                on er.Professor equals professor into ep
                            from subprof in ep.DefaultIfEmpty()
                            where student.Id == studentId
                            select new ExamRegistration()
                            {
                                Id = er.Id,
                                Student = student,
                                Subject = subject,
                                Professor = subprof ?? null,
                                ExamDate = er.ExamDate ?? null,
                                Grade = er.Grade ?? null,
                                IsLocked = er.IsLocked,
                                RegistrationDate = er.RegistrationDate
                            };

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
                var query = from er in dbContext.ExamRegistrations
                            join student in dbContext.Students
                                on er.Student.Id equals student.Id
                            join subject in dbContext.Subjects
                                on er.Subject.Id equals subject.Id
                            join professor in dbContext.Professors
                                on er.Professor equals professor into ep
                            from subprof in ep.DefaultIfEmpty()
                            where string.IsNullOrEmpty(studentIndex) || student.Index.StartsWith(studentIndex)
                            select new ExamRegistration()
                            {
                                Id = er.Id,
                                Student = student,
                                Subject = subject,
                                Professor = subprof ?? null,
                                ExamDate = er.ExamDate ?? null,
                                Grade = er.Grade ?? null,
                                IsLocked = er.IsLocked,
                                RegistrationDate = er.RegistrationDate
                            };

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
                var query = from er in dbContext.ExamRegistrations
                            join student in dbContext.Students
                                on er.Student.Id equals student.Id
                            join subject in dbContext.Subjects
                                on er.Subject.Id equals subject.Id
                            join professor in dbContext.Professors
                                on er.Professor equals professor into ep
                            from subprof in ep.DefaultIfEmpty()
                            where string.IsNullOrEmpty(studentIndex) || student.Index.StartsWith(studentIndex)
                            select new ExamRegistration()
                            {
                                Id = er.Id,
                                Student = student,
                                Subject = subject,
                                Professor = subprof ?? null,
                                ExamDate = er.ExamDate ?? null,
                                Grade = er.Grade ?? null,
                                IsLocked = er.IsLocked,
                                RegistrationDate = er.RegistrationDate
                            };

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
                var query = from er in dbContext.ExamRegistrations
                            join student in dbContext.Students
                                on er.Student.Id equals student.Id
                            join subject in dbContext.Subjects
                                on er.Subject.Id equals subject.Id
                            join professor in dbContext.Professors
                                on er.Professor equals professor into ep
                            from subprof in ep.DefaultIfEmpty()
                            where subject.Id == subjectId
                            select new ExamRegistration()
                            {
                                Id = er.Id,
                                Student = student,
                                Subject = subject,
                                Professor = subprof ?? null,
                                ExamDate = er.ExamDate ?? null,
                                Grade = er.Grade ?? null,
                                IsLocked = er.IsLocked,
                                RegistrationDate = er.RegistrationDate
                            };

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
                var query = from er in dbContext.ExamRegistrations
                            join student in dbContext.Students
                                on er.Student.Id equals student.Id
                            join subject in dbContext.Subjects
                                on er.Subject.Id equals subject.Id
                            join professor in dbContext.Professors
                                on er.Professor equals professor into ep
                            from subprof in ep.DefaultIfEmpty()
                            where string.IsNullOrEmpty(subjectName) || subject.Name.StartsWith(subjectName)
                            select new ExamRegistration()
                            {
                                Id = er.Id,
                                Student = student,
                                Subject = subject,
                                Professor = subprof ?? null,
                                ExamDate = er.ExamDate ?? null,
                                Grade = er.Grade ?? null,
                                IsLocked = er.IsLocked,
                                RegistrationDate = er.RegistrationDate
                            };

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
                var query = from er in dbContext.ExamRegistrations
                            join student in dbContext.Students
                                on er.Student.Id equals student.Id
                            join subject in dbContext.Subjects
                                on er.Subject.Id equals subject.Id
                            join professor in dbContext.Professors
                                on er.Professor equals professor into ep
                            from subprof in ep.DefaultIfEmpty()
                            where string.IsNullOrEmpty(subjectName) || subject.Name.StartsWith(subjectName)
                            select new ExamRegistration()
                            {
                                Id = er.Id,
                                Student = student,
                                Subject = subject,
                                Professor = subprof ?? null,
                                ExamDate = er.ExamDate ?? null,
                                Grade = er.Grade ?? null,
                                IsLocked = er.IsLocked,
                                RegistrationDate = er.RegistrationDate
                            };

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
            var query = from er in dbContext.ExamRegistrations
                        join student in dbContext.Students
                            on er.Student.Id equals student.Id
                        join subject in dbContext.Subjects
                            on er.Subject.Id equals subject.Id
                        join professor in dbContext.Professors
                            on er.Professor equals professor into ep
                        from subprof in ep.DefaultIfEmpty()
                        where (string.IsNullOrEmpty(subjectName) || subject.Name.StartsWith(subjectName)) && (string.IsNullOrEmpty(index) || student.Index.StartsWith(index))
                        select new ExamRegistration()
                        {
                            Id = er.Id,
                            Student = student,
                            Subject = subject,
                            Professor = subprof ?? null,
                            ExamDate = er.ExamDate ?? null,
                            Grade = er.Grade ?? null,
                            IsLocked = er.IsLocked,
                            RegistrationDate = er.RegistrationDate
                        };
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
    }
}
