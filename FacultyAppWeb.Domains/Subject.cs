using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.Domains
{
    public class Subject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int ESPB { get; set; }
        [Required]
        public int Semester { get; set; }
        public long Id { get; }

        public Subject(string name, int espb, int semester)
        {
            Name = name;
            ESPB = espb;
            Semester = semester;
        }

        public Subject(long id, string name, int espb, int semester)
        {
            Id = id;
            Name = name;
            ESPB = espb;
            Semester = semester;
        }


        public override string ToString()
        {
            return $"Name: {Name}, ESPB: {ESPB}, Semester: {Semester}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Subject subject)
            {
                return Name.Equals(subject.Name);
            }
            return false;
        }
    }
}
