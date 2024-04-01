var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles(); 
app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("wwwroot/1.html");
});


app.Run();
