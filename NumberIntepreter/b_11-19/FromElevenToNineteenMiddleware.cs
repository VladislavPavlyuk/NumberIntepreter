namespace NumberIntepreter
{
    public class FromElevenToNineteenMiddleware: StringNumbers
    {
        private readonly RequestDelegate _next;

        public FromElevenToNineteenMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);

                //string[] Teens = { "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

                if (10 < number && number < 20) // если number в пределах 10 ... 20
                {
                    // Выдаем окончательный ответ клиенту
                    await context.Response.WriteAsync("Your number is " + Teens[number - 11]);
                } 
                else                            // если number за пределами 10 ... 20 но менее 100
                {
                    if ( number < 11 || (20 < number && number < 100))
                    {
                        await _next.Invoke(context);  //Контекст запроса передаем следующему компоненту
                    }
                    else                 
                    {
                        if (number > 100) while (number > 100) { number %= 100; }

                        if (10 < number && number < 20) // если number в пределах 11 ... 19

                            context.Session.SetString("number", Teens[number % 10 - 1]);
                        else
                            await _next.Invoke(context);  //Контекст запроса передаем следующему компоненту
                    }
                }                                 
            }
            catch (Exception)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("\nIncorrect parameter on 11 ... 19");
            }
        }
    }
}
