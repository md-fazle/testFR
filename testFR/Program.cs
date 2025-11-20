using Microsoft.EntityFrameworkCore;
using testFR.Data;
using testFR.DAL;
using testFR.Interfaces;
using testFR.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register your custom DbConnection class
builder.Services.AddScoped<testFR.Data.DbConnection.DbConnection>();

// DAL
builder.Services.AddScoped<StudentsDataAccessLayer>();

// Services
builder.Services.AddScoped<ISubjectServices, SubjectServices>();
builder.Services.AddScoped<IStudentServices, StudentServices>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
