using FacultyAppWeb.Domains;
using FacultyAppWeb.RepositoryServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.RepositoryServices.EntityFramework
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly FacultyContext dbContext;

        public ProfessorRepository(FacultyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Professor Add(Professor professor)
        {
            try
            {
                dbContext.Add(professor);
                dbContext.SaveChanges();
                return professor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Professor Delete(long id)
        {
            try
            {
                Professor professor = dbContext.Professors.SingleOrDefault(x => x.Id == id);
                dbContext.Professors.Remove(professor);
                dbContext.SaveChanges();
                return professor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PagedList<Professor> GetProfessorsByName(ProfessorParameters professorParameters, string index)
        {
            try
            {

                if (string.IsNullOrEmpty(index))
                    return PagedList<Professor>.ToPagedList(dbContext.Professors,
        professorParameters.PageNumber,
        professorParameters.PageSize);


                return PagedList<Professor>.ToPagedList(dbContext.Professors.Where(x => x.FirstName.StartsWith(index)),
        professorParameters.PageNumber,
        professorParameters.PageSize);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Professor GetById(long id)
        {
            try
            {
                Professor professor = dbContext.Professors.SingleOrDefault(x => x.Id == id);
                return professor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Professor GetByJMBG(string JMBG)
        {
            try
            {
                Professor professor = dbContext.Professors.SingleOrDefault(x => x.JMBG == JMBG);
                return professor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Professor> GetProfessorsByName(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return dbContext.Professors;
                }
                return dbContext.Professors.Where(p => p.FirstName.StartsWith(name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Professor Update(Professor updated)
        {
            try
            {
                dbContext.Professors.Update(updated);
                dbContext.SaveChanges();
                return updated;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetProfessorsNumber(string index)
        {
            try
            {
                if (string.IsNullOrEmpty(index))
                    return dbContext.Professors.Count();


                return dbContext.Professors.Where(x => x.FirstName.StartsWith(index)).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
    }
}
