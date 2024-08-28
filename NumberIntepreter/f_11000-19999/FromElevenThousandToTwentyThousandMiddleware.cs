
namespace NumberIntepreter
{
    public class FromElevenThousandToTwentyThousandMiddleware //  11'000 ... 19'999
    { 
        private readonly RequestDelegate _next;
         
        public FromElevenThousandToTwentyThousandMiddleware(RequestDelegate next)
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

                if (number > 100000)
                {
                    await context.Response.WriteAsync("Number greater than One Hundred thousand");
                }
                else if (number < 11000 || 19999 < number) 
                {
                    await _next.Invoke(context); //Контекст запроса передаем следующему компоненту
                }
                else
                {
                    string[] Numbers = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

                    if (number % 1000 == 0)
                    {
                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Numbers[number / 1000 - 11] + " Thousand ");
                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту

                        string? result = context.Session.GetString("number"); // получим число от компонента 

                        if (19999 < number)
                        {
                            while (19999 < number) { number %= 10000; }

                            // Записываем в сессионную переменную number результат для компонента 
                            context.Session.SetString("number", Numbers[number / 1000 - 11] + " Thousand " + result);
                        }
                        else
                            // Выдаем окончательный ответ клиенту
                            await context.Response.WriteAsync("\nYour number is " + Numbers[number / 1000 - 11] + " Thousand " + result);
                    }

                }
            }
            catch (Exception)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Incorrect parameter on 11'000 ... 19'999");
            }
        }
    }
}