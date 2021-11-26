using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.Domains
{
    public class ExamRegistration
    {
        public long Id { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public Professor Professor { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? ExamDate { get; set; }
        [Required]
        [Range(5,10)]
        public int? Grade { get; set; }
        public bool IsLocked { get; set; }

        public ExamRegistration()
        {
        }

    }
}
