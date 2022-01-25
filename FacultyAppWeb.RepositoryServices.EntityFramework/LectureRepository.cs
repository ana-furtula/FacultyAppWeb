﻿using FacultyAppWeb.Domains;
using FacultyAppWeb.RepositoryServices.Interfaces;
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
                Lecture lecture = dbContext.Lectures.SingleOrDefault(x => x.Id == id);
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
                    return dbContext.Lectures;
                }
                return dbContext.Lectures.Where(x => x.Professor.FirstName.StartsWith(professorName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Lecture> GetLecturesForProfessor(Professor professor)
        {
            try
            {
                if (professor==null)
                {
                    return null;
                }
                return dbContext.Lectures.Where(x => x.Professor.Id == professor.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Lecture> GetLecturesBySubjectName(string subjectName)
        {
            try
            {
                var query = from lecture in dbContext.Lectures
                            join professor in dbContext.Professors
                                on lecture.Professor.Id equals professor.Id
                            join subject in dbContext.Subjects
                                on lecture.Subject.Id equals subject.Id
                            select new Lecture() { Professor = professor, Subject = subject, Id = lecture.Id };
                
                if (string.IsNullOrEmpty(subjectName))
                {
                    return query;
                }
                return query.Where(l=>l.Subject.Name.StartsWith(subjectName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Professor> GetProfessorsForSubject(long subjectId)
        {
            var query = from lecture in dbContext.Lectures
                        join professor in dbContext.Professors
                            on lecture.Professor.Id equals professor.Id
                        join subject in dbContext.Subjects
                            on lecture.Subject.Id equals subject.Id
                        where subject.Id == subjectId
                        select professor;

            return query;

        }
    }
}
