//using FacultyAppWeb.RepositoryServices.InMemory;
using FacultyAppWeb.RepositoryServices.Interfaces;
using FacultyAppWeb.RepositoryServices.MySQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IStudentRepository, MySqlStudentRepository>();
builder.Services.AddSingleton<IProfessorRepository, MySqlProfessorRepository>();
builder.Services.AddSingleton<ISubjectRepository, MySqlSubjectRepository>();
builder.Services.AddSingleton<ILectureRepository, MySqlLectureRepository>();
builder.Services.AddSingleton<DbBroker, DbBroker>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
