//using FacultyAppWeb.RepositoryServices.InMemory;
using FacultyAppWeb.RepositoryServices.EntityFramework;
using FacultyAppWeb.RepositoryServices.Interfaces;
using FacultyAppWeb.RepositoryServices.MySQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Web.UI;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<IProfessorRepository, ProfessorRepository>();
builder.Services.AddTransient<ISubjectRepository, SubjectRepository>();
builder.Services.AddTransient<ILectureRepository, LectureRepository>();
builder.Services.AddTransient<IExamRegistrationRepository, ExamRegistrationRepository>();
builder.Services.AddTransient<DbBroker>();
builder.Services.AddDbContext<FacultyContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("FacultyContextDB")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FacultyContext>();builder.Services.AddDbContext<FacultyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FacultyContextDB")));

builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
builder.Services.AddMvc().AddXmlSerializerFormatters();
builder.Services.AddMvc().AddXmlDataContractSerializerFormatters();

/*
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
        options => {
            options.SignIn.RequireConfirmedAccount = false;

            //Other options go here
        }
        )
    .AddEntityFrameworkStores<FacultyContext>();*/

builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();