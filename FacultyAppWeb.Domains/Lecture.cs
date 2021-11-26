using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.Domains
{
    public class Lecture
    {
        public long Id { get; set; }
        public Professor Professor { get; set; }
        public Subject Subject { get; set; }
    }
}
