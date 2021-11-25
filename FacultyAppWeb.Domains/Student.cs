﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.Domains
{
    public class Student : Person
    {
        [Required]
        [StringLength(9, ErrorMessage = "Index length can't be more than 9.")]
        public string Index { get; set; }
        [Required]
        public long Id { get; set; }

        public Student()
        {
        }

        public Student(string firstName, string lastName, string indeks, string jmbg) : base(firstName, lastName, jmbg)
        {
            Index = indeks;
        }

        public Student(long id, string firstName, string lastName, string indeks, string jmbg) : base(firstName, lastName, jmbg)
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
