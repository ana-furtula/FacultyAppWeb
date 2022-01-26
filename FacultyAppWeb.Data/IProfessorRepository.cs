﻿using FacultyAppWeb.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.RepositoryServices.Interfaces
{
    public interface IProfessorRepository
    {
        Professor GetById(long id);
        Professor GetByJMBG(string JMBG);
        IEnumerable<Professor> GetProfessorsByName(string name);
        PagedList<Professor> GetProfessorsByName(ProfessorParameters param, string index);
        Professor Update(Professor updated);
        Professor Add(Professor professor);
        Professor Delete(long id);
        int GetProfessorsNumber(string index);
    }
}
