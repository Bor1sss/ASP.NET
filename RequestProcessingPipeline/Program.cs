using RequestProcessingPipeline;

var builder = WebApplication.CreateBuilder(args);

// Все сессии работают поверх объекта IDistributedCache, и 
// ASP.NET Core предоставляет встроенную реализацию IDistributedCache
builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
builder.Services.AddSession();  // Добавляем сервисы сессии
var app = builder.Build();

app.UseSession();   // Добавляем middleware-компонент для работы с сессиями

app.UseFrom10kk_100k(); 
app.UseFrom10k_10kk(); //10k-10kk
app.UseFrom1000_10k(); //1000-10000
app.UseFrom100_1000(); //100-1000
app.UseFromTwentyToHundred(); // 20-100
app.UseFromElevenToNineteen(); //11-19
app.UseFromOneToTen(); //1-9

app.Run();
