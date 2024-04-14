
using Microsoft.EntityFrameworkCore;
using Guest.Models;
using Guest.Repository;



var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.Name = "Session"; 

});
// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));

// ��������� ������� MVC
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository, MessageRepository>();

var app = builder.Build();
app.UseStaticFiles(); // ������������ ������� � ������ � ����� wwwroot

app.UseSession();   // ��������� middleware-��������� ��� ������ � ��������


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Register}/{action=Index}/{id?}");

app.Run();
