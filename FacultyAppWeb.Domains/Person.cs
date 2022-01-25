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
    public class Person
    {
        [Required]
        [StringLength(20, ErrorMessage = "First name length can't be more than 20.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Last name length can't be more than 20.")]
        public string LastName { get; set; }
        [Required]
        [StringLength(13, ErrorMessage = "JMBG length can't be more than 13.")]
        public string JMBG { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Person()
        {
        }

        public Person(string firstName, string lastName, string jmbg, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            JMBG = jmbg;
            Email = email;
        }
    }
}
