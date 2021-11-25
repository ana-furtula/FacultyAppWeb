using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.Domains
{
    public class Lecture
    {
        public Professor Professor { get; set; }
        public Subject Subject { get; set; }
    }
}
