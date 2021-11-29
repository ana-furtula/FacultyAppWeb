using FacultyAppWeb.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.RepositoryServices.EntityFramework
{
    public class FacultyContext : DbContext
    {
        private readonly IConfiguration config;

        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<ExamRegistration> ExamRegistrations { get; set; }

       /* public FacultyContext(IConfiguration config)
        {
            this.config = config;
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog= Faculty_App_DB;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True;");
        }
    }
}
