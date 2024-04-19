using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models.IRepository.Genre;
using MusicPortal.Models.IRepository.Music;
using MusicPortal.Models.MusicModel;
using Repository;




var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.Name = "Session";

});
builder.Services.AddDbContext<MusicContext>(options => options.UseSqlServer(connection));

// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMusicRep, MusicRepository>();
builder.Services.AddScoped<IRepositoryUser, UserRepository>();
builder.Services.AddScoped<IGenreRep, GenreRepository>();

var app = builder.Build();
app.UseStaticFiles(); 

app.UseSession();  


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
