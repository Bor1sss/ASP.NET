using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using MusicPortal.BLL.Infrastucture;
using UserPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.BLL.Interfaces;
using GenrePortal.BLL.Interfaces;
using GenrePortal.BLL.Services;
var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.Name = "Session";

});

builder.Services.AddMusicContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IMusicService, MusicService>();
builder.Services.AddTransient<IGenreService, GenreService>();




// ��������� ������� MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles(); // ������������ ������� � ������ � ����� wwwroot
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Register}/{action=Index}/{id?}");

app.Run();
