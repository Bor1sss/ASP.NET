using RequestProcessingPipeline;

var builder = WebApplication.CreateBuilder(args);

// Все сессии работают поверх объекта IDistributedCache, и 
// ASP.NET Core предоставляет встроенную реализацию IDistributedCache
builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
builder.Services.AddSession();  // Добавляем сервисы сессии
var app = builder.Build();

app.UseSession();   // Добавляем middleware-компонент для работы с сессиями

// Добавляем middleware-компоненты в конвейер обработки запроса.
app.UseFrom50000_100000(); //50000_100000
app.UseFrom25000_50000(); //25000_50000
app.UseFrom12500_25000(); //12500_25000
app.UseFrom6250_12500(); //6250_12500
app.UseFrom3125_6250(); //3125_6250
app.UseFrom2000_3125(); //1000-3125
app.UseFrom1000_2000(); //1000-2000
app.UseFrom500_1000(); //500-1000
app.UseFrom100_500(); //100-500
app.UseFromTwentyToHundred(); // 20-100
app.UseFromElevenToNineteen(); //11-19
app.UseFromOneToTen(); //1-9

app.Run();
