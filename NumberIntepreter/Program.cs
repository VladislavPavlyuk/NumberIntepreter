using NumberIntepreter;

var builder = WebApplication.CreateBuilder(args);

// Все сессии работают поверх объекта IDistributedCache, и 
// ASP.NET Core предоставляет встроенную реализацию IDistributedCache
builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
builder.Services.AddSession();  // Добавляем сервисы сессии
var app = builder.Build();

app.UseSession();   // Добавляем middleware-компонент для работы с сессиями

// Добавляем middleware-компоненты в конвейер обработки запроса.

//app.UseThousand();    // 1001 - 10'000
app.UseHundred();   // 101 - 1000

app.UseTens();  // 20 - 100
app.UseTeens(); // 11 - 19
app.UseOnes(); // 1 - 10

app.Run();
