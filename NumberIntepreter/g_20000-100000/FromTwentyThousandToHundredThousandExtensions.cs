namespace NumberIntepreter
{

    public static class FromTwentyThousandToHundredThousandExtensions
    { 
        public static IApplicationBuilder UseTensThousand(this IApplicationBuilder builder)
        { 
            return builder.UseMiddleware<FromTwentyThousandToHundredThousandMiddleware>();
        }
    }
}