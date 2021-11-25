﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.Domains
{
    public class ExamRegistration
    {
        public long Id { get; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public Professor Professor { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? ExamDate { get; set; }
        public int? Grade { get; set; }
        public bool IsLocked { get; set; }

        public ExamRegistration()
        {
        }

    }
}
