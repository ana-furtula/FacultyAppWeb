using FacultyAppWeb.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FacultyAppWeb.RepositoryServices.EntityFramework
{
    public class FacultyContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private readonly IConfiguration config;
        
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<ExamRegistration> ExamRegistrations { get; set; }

        public FacultyContext(DbContextOptions<FacultyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Professor>()
                .HasIndex(p => p.JMBG)
                .IsUnique();

            builder.Entity<Professor>()
                .HasData(
                    new Professor() { Id = 2, FirstName = "Marko", LastName = "Markovic", JMBG = "1234561234561", Email = "marko@gmail.com" },
                    new Professor() { Id = 3, FirstName = "Lazar", LastName = "Lazarevic", JMBG = "1234567123456", Email = "lazar@gmail.com" },
                    new Professor() { Id = 4, FirstName = "Ana", LastName = "Anic", JMBG = "1234567812345", Email = "ana1@gmail.com" },
                    new Professor() { Id = 5, FirstName = "Lena", LastName = "Lenic", JMBG = "1234567891234", Email = "lena@gmail.com" },
                    new Professor() { Id = 6, FirstName = "Mika", LastName = "Mikic", JMBG = "1234567823456", Email = "mika@gmail.com" },
                    new Professor() { Id = 7, FirstName = "Dragana", LastName = "Stefanovic", JMBG = "2345678912345", Email = "gaga@gmail.com" },
                    new Professor() { Id = 8, FirstName = "Nikola", LastName = "Nikolic", JMBG = "0123456789012", Email = "nikola@gmail.com" }
                );

            builder.Entity<Student>()
                .HasData(
                new Student() { Id = 2, Index = "2014/0155", FirstName = "Filip", LastName = "Filipovic", JMBG = "2512995280025", Email = "filip1@gmail.com" },
                new Student() { Id = 3, Index = "2018/0175", FirstName = "Aleksandra", LastName = "Furtula", JMBG = "0207999285019", Email = "ana1@gmail.com" },
                new Student() { Id = 10002, Index = "2012/0001", FirstName = "Matija", LastName = "Matijevic", JMBG = "1111111111111", Email = "matija@gmail.com" },
                new Student() { Id = 10003, Index = "2000/2000", FirstName = "Milos", LastName = "Milosevic", JMBG = "2222222222222", Email = "milos@gmail.com" },
                new Student() { Id = 20002, Index = "1234/2000", FirstName = "Vojin", LastName = "Vojislavljevic", JMBG = "1234567891234", Email = "vojin@gmail.com" }
                );

            builder.Entity<Subject>()
                .HasData(
                    new Subject() { Id = 10002, Name = "EPOS", ESPB = 4, Semester = 4 },
                    new Subject() { Id = 10004, Name = "Projektovanje softvera", ESPB = 5, Semester = 5 },
                    new Subject() { Id = 10005, Name = "Softverski paterni", ESPB = 6, Semester = 6 },
                    new Subject() { Id = 20002, Name = "Programski jezici", ESPB = 6, Semester = 6 },
                    new Subject() { Id = 20003, Name = "ITEH", ESPB = 3, Semester = 3 }
                );

            builder.Entity<IdentityUser>()
                .HasData(
                    new IdentityUser() { Id="1", Email = "admin@gmail.com", EmailConfirmed = true, NormalizedEmail = "ADMIN@GMAIL.COM", NormalizedUserName= "ADMIN@GMAIL.COM", UserName= "admin@gmail.com", PasswordHash= "AQAAAAEAACcQAAAAEFOaCu5yyRPbxeuirMzP1WEc14y8ciFN3v0Xcv39tHDIwT94nZ5hmBKuSJoHsue4Xw==" }
                );

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole() { Id = "1", Name="Admin", NormalizedName ="ADMIN"},
                    new IdentityRole() { Id = "2", Name = "Professor", NormalizedName = "PROFESSOR" },
                    new IdentityRole() { Id = "3", Name = "Student", NormalizedName = "STUDENT" }
                );

            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = "1",
                        UserId = "1"
                    }
                );

            base.OnModelCreating(builder);
        }

        /* public FacultyContext(IConfiguration config)
         {
             this.config = config;
         }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog= Faculty_App_DB_TEST;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True;");
        }
    }
}
