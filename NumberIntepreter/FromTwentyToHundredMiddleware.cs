namespace NumberIntepreter
{
    public class FromTwentyToHundredMiddleware
    {
        private readonly RequestDelegate _next;

        public FromTwentyToHundredMiddleware(RequestDelegate next)
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

                if ((number < 20) || (number > 100))
                {
                    await _next.Invoke(context); //Контекст запроса передаем следующему компоненту
                }

                else if (number == 100)
                {
                    // Выдаем окончательный ответ клиенту
                    await context.Response.WriteAsync("Your number is one hundred");
                }
                else
                {
                    string[] Tens = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                    if (number % 10 == 0)
                    {
                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Tens[number / 10 - 2]);
                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту

                        string? result = context.Session.GetString("number"); // получим число от компонента FromOneToTenMiddleware
                        
                        // Записываем в сессионную переменную number результат для компонента 
                        context.Session.SetString("number", Tens[number / 10 - 2] + " " + result);

                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Tens[number / 10 - 2] + " " + result);
                    }
                }
            }
            catch (Exception)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
