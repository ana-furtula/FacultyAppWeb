using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FacultyAppWeb.Domains
{
    public class Student : Person
    {
        public string Index { get; set; }
        [Required]
        public long Id { get; set; }

        public Student()
        {
        }

        public Student(string firstName, string lastName, string indeks, string jmbg, string email) : base(firstName, lastName, jmbg, email)
        {
            Index = indeks;
        }

        public Student(long id, string firstName, string lastName, string indeks, string jmbg, string email) : base(firstName, lastName, jmbg, email)
        {
            Index = indeks;
            Id = id;
        }

        public override string ToString()
        {
            return $"JMBG: {JMBG}, First name: {FirstName}, Last name: {LastName}, Index: {Index}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Student s1)
            {
                return Index.Equals(s1.Index);
            }

            return false;
        }
    }
}
