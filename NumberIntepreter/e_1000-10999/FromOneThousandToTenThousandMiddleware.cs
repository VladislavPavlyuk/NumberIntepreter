﻿namespace NumberIntepreter
{
    public class FromOneThousandToTenThousandMiddleware //  1000 ... 10'999
    {
        private readonly RequestDelegate _next;

        public FromOneThousandToTenThousandMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Session.Clear();

            string? token = context.Request.Query["number"]; // Получим число из контекста запроса
            try
            {
                int number = Convert.ToInt32(token);

                number = Math.Abs(number);

                if (number < 1001 || number > 10999)
                {
                    await _next.Invoke(context); //Контекст запроса передаем следующему компоненту
                }
                else if (number == 1000)
                {
                    // Выдаем окончательный ответ клиенту
                    await context.Response.WriteAsync("Your number is one Thousand");
                }
                else
                {
                    string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };

                    if (number % 1000 == 0)
                    {
                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Numbers[number / 1000 - 1] + " Thousand ");
                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту

                        string? result = context.Session.GetString("number"); // получим число от компонента 

                        if (10999 < number)
                        {
                            while (10999 < number) { number %= 10000; }

                            // Записываем в сессионную переменную number результат для компонента 
                            context.Session.SetString("number", Numbers[number / 1000 - 1] + " Thousand " + result);
                        }
                        else
                            // Выдаем окончательный ответ клиенту
                            await context.Response.WriteAsync("\nYour number is " + Numbers[number / 1000 - 1] + " Thousand " + result);
                    }
                }
            }
            catch (Exception)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("\nIncorrect parameter on 1000 ... 9'999");
            }
        }
    }
}