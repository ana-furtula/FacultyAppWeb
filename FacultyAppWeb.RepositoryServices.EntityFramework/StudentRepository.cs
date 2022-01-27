using FacultyAppWeb.Domains;
using FacultyAppWeb.RepositoryServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FacultyAppWeb.RepositoryServices.EntityFramework
{
    public class StudentRepository : IStudentRepository
    {
        private readonly FacultyContext dbContext;

        public StudentRepository(FacultyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Student Add(Student student)
        {
            try
            {
                dbContext.Students.Add(student);
                dbContext.SaveChanges();
                return student;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Student Delete(long id)
        {
            try
            {
                Student student = dbContext.Students.SingleOrDefault(x => x.Id == id);
                dbContext.Students.Remove(student);
                dbContext.SaveChanges();
                return student;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Student GetById(long id)
        {
            try
            {
                Student student = dbContext.Students.SingleOrDefault(x => x.Id == id);
                return student;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Student GetStudentByJMBG(string JMBG)
        {
            try
            {
                Student student = dbContext.Students.SingleOrDefault(x => x.JMBG == JMBG);
                return student;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PagedList<Student> GetStudentsByIndex(StudentParameters studentParameters, string index)
        {
            try
            {
               
                if (string.IsNullOrEmpty(index))
                    return PagedList<Student>.ToPagedList(dbContext.Students,
        studentParameters.PageNumber,
        studentParameters.PageSize);
      

                return PagedList<Student>.ToPagedList(dbContext.Students.Where(x => x.Index.StartsWith(index)),
        studentParameters.PageNumber,
        studentParameters.PageSize);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Student> GetStudentsByIndex(string index)
        {

            try
            {
                if (string.IsNullOrEmpty(index))
                    return dbContext.Students.
                    ToList();

                return dbContext.Students.Where(x => x.Index.StartsWith(index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        public Student Update(Student updated)
        {
            try
            {
                dbContext.Students.Update(updated);
                dbContext.SaveChanges();
                return updated;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetStudentsNumber(string index)
        {
            try
            {
                if (string.IsNullOrEmpty(index))
                    return dbContext.Students.Count();


                return dbContext.Students.Where(x => x.Index.StartsWith(index)).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }

          
           
        }

        public IEnumerable<ExamRegistration> GetFailedExams(long id)
        {
            return dbContext.ExamRegistrations
                   .Include(x => x.Student)
                   .Include(x => x.Subject)
                   .Include(x => x.Professor)
                   .Where(x => x.Student.Id == id && x.IsLocked == true && x.Grade == 5);
        }

        public IEnumerable<ExamRegistration> GetPassedExams(long id)
        {
            return dbContext.ExamRegistrations
                   .Include(x => x.Student)
                   .Include(x => x.Subject)
                   .Include(x => x.Professor)
                   .Where(x => x.Student.Id == id && x.IsLocked == true && x.Grade > 5);
        }
    }
}
