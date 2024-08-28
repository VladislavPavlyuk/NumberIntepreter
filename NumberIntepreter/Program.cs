using NumberIntepreter;

var builder = WebApplication.CreateBuilder(args);

// Все сессии работают поверх объекта IDistributedCache, и 
// ASP.NET Core предоставляет встроенную реализацию IDistributedCache
builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
builder.Services.AddSession();  // Добавляем сервисы сессии
var app = builder.Build();

app.UseSession();   // Добавляем middleware-компонент для работы с сессиями

// Добавляем middleware-компоненты в конвейер обработки запроса.

app.UseTensThousand();  // 20'000 - 100'000
app.UseTeensThousand(); // 11'000 - 19'999
app.UseOnesThousand();  // 1000 - 10'999
app.UseHundred();       // 100 - 999
app.UseTens();          // 20 - 99
app.UseTeens();         // 11 - 19
app.UseOnes();          // 1 - 10

app.Run();
