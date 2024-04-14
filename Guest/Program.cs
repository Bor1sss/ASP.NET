
using Microsoft.EntityFrameworkCore;
using Guest.Models;
using Guest.Repository;



var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.Name = "Session"; 

});
// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));

// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository, MessageRepository>();

var app = builder.Build();
app.UseStaticFiles(); // обрабатывает запросы к файлам в папке wwwroot

app.UseSession();   // Добавляем middleware-компонент для работы с сессиями


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Register}/{action=Index}/{id?}");

app.Run();
