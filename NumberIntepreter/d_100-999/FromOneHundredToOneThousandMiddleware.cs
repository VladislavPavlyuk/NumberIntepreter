using Microsoft.AspNetCore.Http;

namespace NumberIntepreter
{ 
    public class FromOneHundredToOneThousandMiddleware  // 100 ... 999
    {
        private readonly RequestDelegate _next;

        public FromOneHundredToOneThousandMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"]; // Получим число из контекста запроса
            try
            {
                int number = Convert.ToInt32(token);

                number = Math.Abs(number);

                if (number == 100)
                {
                    // Выдаем окончательный ответ клиенту
                    await context.Response.WriteAsync("Your number is One Hundred");
                }
                else if (number < 100)
                {
                    await _next.Invoke(context); //Контекст запроса передаем следующему компоненту
                }
                else
                {
                    string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                    if (number % 100 == 0)
                    {
                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Numbers[number / 100 - 1] + " Hundred ");
                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту

                        string? result = context.Session.GetString("number"); // получим число от компонента 

                        if (999 < number)
                        {
                            while (999 < number) { number %= 1000; }
                            if (99 < number)
                                // Записываем в сессионную переменную number результат для компонента 
                                context.Session.SetString("number", Numbers[number / 100 - 1] + " Hundred " + result);
                            else
                                await _next.Invoke(context);  //Контекст запроса передаем следующему компоненту
                        }
                        else
                            // Выдаем окончательный ответ клиенту
                            await context.Response.WriteAsync("\nYour number is " + Numbers[number / 100 - 1] + " Hundred " + result);
                    }
                }
            }
            catch (Exception)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Incorrect parameter on 100 ... 999");
            }
        }
    }
}