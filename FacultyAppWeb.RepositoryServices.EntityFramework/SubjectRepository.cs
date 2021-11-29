using FacultyAppWeb.Domains;
using FacultyAppWeb.RepositoryServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.RepositoryServices.EntityFramework
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly FacultyContext dbContext;

        public SubjectRepository(FacultyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Subject Add(Subject subject)
        {
            try
            {
                dbContext.Subjects.Add(subject);
                dbContext.SaveChanges();
                return subject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Subject Delete(long id)
        {
            try
            {
                Subject subject = dbContext.Subjects.Find(id);
                dbContext.Subjects.Remove(subject);
                dbContext.SaveChanges();
                return subject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Subject subject)
        {
           return dbContext.Subjects.Where(x=>x.Name.Equals(subject.Name) && x.Semester==subject.Semester && x.ESPB==subject.ESPB).Any();
        }

        public Subject GetById(long id)
        {
            try
            {
                return dbContext.Subjects.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Subject> GetSubjectsByESPB(int espb)
        {
            return dbContext.Subjects.Where(s => s.ESPB == espb);
        }

        public IEnumerable<Subject> GetSubjectsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return dbContext.Subjects;
            return dbContext.Subjects.Where(s => s.Name.StartsWith(name));
        }

        public IEnumerable<Subject> GetSubjectsBySemester(int semester)
        {
            return dbContext.Subjects.Where(s => s.Semester == semester);
        }

        public Subject Update(Subject updated)
        {
            try
            {
                dbContext.Subjects.Update(updated);
                dbContext.SaveChanges();
                return updated;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
