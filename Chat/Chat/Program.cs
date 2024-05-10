using Chat.Controllers;
using Chat.Models;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ChatContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();

app.MapHub<ChatHub>("/chat");   

app.Run();
