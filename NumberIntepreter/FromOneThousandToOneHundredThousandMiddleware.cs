
namespace NumberIntepreter
{
    public class FromOneThousandToOneHundredThousandMiddleware //  1000 ... 10000
    { 
        private readonly RequestDelegate _next;
         
        public FromOneThousandToOneHundredThousandMiddleware(RequestDelegate next)
        { 
            _next = next;
        }
         
        public async Task Invoke(HttpContext context)
        { 
            context.Session.Clear(); 

            string? token = context.Request.Query["number"];    // Получим число из контекста запроса

            try
            { 
                var number = Convert.ToInt32(token); 

                number = Math.Abs(number); 

                if (number < 1000)  {

                    await _next.Invoke(context);
                } 
                else if (number > 100000) {

                    await context.Response.WriteAsync("Number greater than one hundred thousand");
                }
                else  { 

                    var countNumbersInDigit = 0;
                    var temp = number;
                    while (temp > 0)
                    {
                        temp /= 10;
                        countNumbersInDigit++;
                    }
                     
                    var thousand = 0;
                    if (countNumbersInDigit > 3)
                    {
                        thousand = number / 1000;
                        number %= 1000;
                        countNumbersInDigit -= 3;
                    }
                     
                    if (thousand > 0)
                    {
                        var result = string.Empty;
                        if (countNumbersInDigit == 1) {

                            string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                            result = Numbers[thousand - 1] + " " + "thousand";

                            context.Session.SetString("number", result);

                            await _next.Invoke(context);
                        }
                        else if (countNumbersInDigit == 2) {

                            var num1 = thousand / 10;

                            if (num1 == 1) {

                                string[] Numbers =  {

                                    "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen",
                                    "seventeen", "eighteen", "nineteen"
                                };
                                result = Numbers[thousand % 10];
                            }
                            else  {

                                var num2 = thousand % 10;

                                string[] Numbers = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                                result = Numbers[num1 - 2];

                                if (num2 > 0) {

                                    string[] Numbers2 = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                                    result += " " + Numbers2[num2 - 1];
                                }
                            }

                            result += " " + "thousand";

                            context.Session.SetString("number", result);

                            await _next.Invoke(context);

                            // Remove double spaces in the string
                            context.Session.SetString("number", context.Session.GetString("number").Replace("  ", " "));
                        }
                        else  {

                            await context.Response.WriteAsync("Your number is one hundred thousand");
                        }
                    }
                }
            } 
            catch (Exception) {

                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}