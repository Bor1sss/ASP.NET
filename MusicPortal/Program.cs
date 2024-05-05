using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using MusicPortal.BLL.Infrastucture;
using UserPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.BLL.Interfaces;
using GenrePortal.BLL.Interfaces;
using GenrePortal.BLL.Services;
var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из файла конфигурации
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




// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles(); // обрабатывает запросы к файлам в папке wwwroot
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Register}/{action=Index}/{id?}");

app.Run();
