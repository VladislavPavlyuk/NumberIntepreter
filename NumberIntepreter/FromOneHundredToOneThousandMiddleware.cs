namespace NumberIntepreter
{ 
    public class FromOneHundredToOneThousandMiddleware  // 101 ... 1000
    { 
        private readonly RequestDelegate _next; 
        public FromOneHundredToOneThousandMiddleware(RequestDelegate next)
        {
            _next = next;
        }
         
        public async Task Invoke(HttpContext context)
        { 
            string? token = context.Request.Query["number"]; // Получим число из контекста запроса
            try
            { 
                //var s = context.Session.GetString("number"); 

                var number = Convert.ToInt32(token);

                number = Math.Abs(number); 

                var hundreds = number % 1000 / 100; 

                if (number < 100)
                {
                    await _next.Invoke(context);
                } 
                else
                { 
                    if (hundreds > 0)
                    {
                        string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                        var result = context.Session.GetString("number") + " " + Numbers[hundreds - 1] + " " + "hundred"; 

                        context.Session.SetString("number", result); 

                        await _next.Invoke(context);
                    }
                    else
                    { 
                        await _next.Invoke(context);
                    }
                }
            } 
            catch (Exception)
            {
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}