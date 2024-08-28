
namespace NumberIntepreter
{
    public class FromTwentyThousandToHundredThousandMiddleware //  20'000 ... 100'000
    { 
        private readonly RequestDelegate _next;
         
        public FromTwentyThousandToHundredThousandMiddleware(RequestDelegate next)
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

                if (number == 100000)
                {
                    await context.Response.WriteAsync("Your number is One Hundred thousand");
                }
                else if (number > 100000)
                {
                    await context.Response.WriteAsync("Number greater than One Hundred thousand");
                }
                else if (number < 20000) 
                {
                    await _next.Invoke(context); //Контекст запроса передаем следующему компоненту
                }
                else
                {
                    string[] Tens = { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                    if (number % 10000 == 0)
                    {
                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Tens[number / 10000 - 2] + " Thousand ");
                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту

                        string? result = context.Session.GetString("number"); // получим число от компонента 

                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("\nYour number is " + Tens[number / 10000 - 2] + " " + result);
                    }
                }
            }
            catch (Exception)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("\nIncorrect parameter on 20'000 ... 100'000");
            }
        }
    }
}