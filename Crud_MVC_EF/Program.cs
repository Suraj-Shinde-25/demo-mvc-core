using Crud_MVC_EF.Data;
using Crud_MVC_EF.Repository;
using Crud_MVC_EF.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//------- Logger
var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
if (Directory.Exists(logDirectory))
{
    Directory.CreateDirectory(logDirectory);
}

//Logger 
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
    .WriteTo.File("Log/log.txt",
    rollingInterval: RollingInterval.Day,
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
});
builder.Services.AddSession(op =>
{
    op.IdleTimeout = TimeSpan.FromMinutes(10);
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IStudentRepo, StudentRepoImpl>();
builder.Services.AddScoped<IUserRepo, UserRepoImpl>();

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
