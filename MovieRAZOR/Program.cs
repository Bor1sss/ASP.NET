using Microsoft.EntityFrameworkCore;
using MovieRAZOR.IRepository;
using MovieRAZOR.Repository;
using MVC_first;

var builder = WebApplication.CreateBuilder(args);
// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IRepository<Movie>, MovieRepository>();
// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();
