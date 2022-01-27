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
    public class LectureRepository : ILectureRepository
    {
        private readonly FacultyContext dbContext;

        public LectureRepository(FacultyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Lecture Add(Lecture lecture)
        {
            try
            {
                dbContext.Lectures.Add(lecture);
                dbContext.SaveChanges();
                return lecture;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Lecture Delete(long id)
        {
            try
            {
                Lecture lecture = dbContext.Lectures.SingleOrDefault(x => x.Id == id);
                dbContext.Lectures.Remove(lecture);
                dbContext.SaveChanges();
                return lecture;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(Lecture lecture)
        {
            return dbContext.Lectures.FirstOrDefault(l => l.Subject.Id == lecture.Subject.Id && l.Professor.Id == lecture.Professor.Id) != null;
        }

        public Lecture GetById(long id)
        {
            try
            {
                Lecture lecture = dbContext.Lectures
                                  .Include(x => x.Professor)
                                  .Include(x => x.Subject)
                                  .SingleOrDefault(x => x.Id == id);
                return lecture;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Lecture> GetLecturesByProfessorName(string professorName)
        {
            try
            {
                if (string.IsNullOrEmpty(professorName))
                {
                    var all = dbContext.Lectures
                        .Include(x => x.Professor)
                        .Include(x => x.Subject).ToList();
                    return all;

                }
                var lectures = dbContext.Lectures
                        .Include(x => x.Professor)
                        .Include(x => x.Subject)
                        .Where(x => x.Professor.FirstName.StartsWith(professorName)).ToList();
                return lectures;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Subject> GetSubjectsForProfessor(Professor professor)
        {
            if (professor == null)
            {
                return null;
            }

            var subjects = dbContext.Lectures
                           .Include(x => x.Professor)
                           .Include(x => x.Subject)
                           .Where(x => x.Professor.Id == professor.Id)
                           .Select(x => x.Subject).ToList();
            return subjects;

        }

        public IEnumerable<Lecture> GetLecturesBySubjectName(string subjectName)
        {
            try
            {
                if (string.IsNullOrEmpty(subjectName))
                {
                    var all = dbContext.Lectures
                        .Include(x => x.Professor)
                        .Include(x => x.Subject).ToList();
                    return all;

                }
                var lectures = dbContext.Lectures
                        .Include(x => x.Professor)
                        .Include(x => x.Subject)
                        .Where(x => x.Subject.Name.StartsWith(subjectName)).ToList();
                return lectures;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Professor> GetProfessorsForSubject(long subjectId)
        {

            var profs = dbContext.Lectures
                           .Include(x => x.Professor)
                           .Include(x => x.Subject)
                           .Where(x => x.Subject.Id == subjectId)
                           .Select(x => x.Professor).ToList();
            return profs;

        }

        public PagedList<Lecture> GetLecturesBySubjectName(LectureParameters param, string index)
        {
            try
            {
                var query = dbContext.Lectures
                        .Include(x => x.Professor)
                        .Include(x => x.Subject);

                if (string.IsNullOrEmpty(index))
                {
                    return PagedList<Lecture>.ToPagedList(query,
        param.PageNumber,
        param.PageSize);

                }
                return PagedList<Lecture>.ToPagedList(query.Where(l => l.Subject.Name.StartsWith(index)),
        param.PageNumber,
        param.PageSize);



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int GetTotalLecturesNumber(string index)
        {
            try
            {
                if (string.IsNullOrEmpty(index))
                    return dbContext.Lectures.Count();


                return dbContext.Lectures.Where(x => x.Subject.Name.StartsWith(index)).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
