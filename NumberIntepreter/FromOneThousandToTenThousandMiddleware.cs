
namespace NumberIntepreter
{
    public class FromOneThousandToTenThousandMiddleware //  1000 ... 10'000
    {
        private readonly RequestDelegate _next;

        public FromOneThousandToTenThousandMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Session.Clear();

            string? token = context.Request.Query["number"]; // Получим число из контекста запроса
            try
            {
                int number = Convert.ToInt32(token);

                number = Math.Abs(number);

                if ((number < 1001) || (number > 10000))
                {
                    await _next.Invoke(context); //Контекст запроса передаем следующему компоненту
                }
                else if (number == 10000)
                {
                    // Выдаем окончательный ответ клиенту
                    await context.Response.WriteAsync("Your number is ten Thousand");
                }
                else
                {
                    string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                    if (number % 1000 == 0)
                    {
                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Numbers[number / 1000 - 1] + " Thousand ");
                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту

                        string? result = context.Session.GetString("number"); // получим число от компонента 

                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("\nYour number is " + Numbers[number / 1000 - 1] + " Thousand " + result);
                    }
                }
            }
            catch (Exception)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Incorrect parameter on 1000 ... 10'000");
            }
        }
    }
}