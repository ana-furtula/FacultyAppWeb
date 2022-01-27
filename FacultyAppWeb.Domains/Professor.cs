using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.Domains
{
    [Index(nameof(Email), IsUnique = true)]
    public class Professor : Person
    {

        [Required]
        public long Id { get; set; }

        public Professor()
        {
        }
        public Professor(string firstName, string lastName, string jmbg, string email) : base(firstName, lastName, jmbg, email)
        {
        }

        public Professor(long id, string firstName, string lastName, string jmbg, string email) : base(firstName, lastName, jmbg, email)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"JMBG: {JMBG}, First name: {FirstName}, Last name: {LastName}";
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
                return false;
            if (obj is Professor prof)
            {
                return FirstName.Equals(prof.FirstName) && LastName.Equals(prof.LastName) && Id == prof.Id;
            }
            return false;
        }

    }
}
