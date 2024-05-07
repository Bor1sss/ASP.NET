using Microsoft.EntityFrameworkCore;
using MusicPortal_WebApi.IRepository.GenreF;
using MusicPortal_WebApi.IRepository.Music;
using MusicPortal_WebApi.IRepository.User;
using MusicPortal_WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IMusicRep, MusicRepository>();
builder.Services.AddScoped<IRepositoryUser, UserRepository>();
builder.Services.AddScoped<IGenreRep, GenreRepository>();
// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<MusicContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
