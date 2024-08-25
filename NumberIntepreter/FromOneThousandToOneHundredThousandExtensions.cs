namespace NumberIntepreter
{

    public static class FromOneThousandToOneHundredThousandExtensions
    { 
        public static IApplicationBuilder UseThousand(this IApplicationBuilder builder)
        { 
            return builder.UseMiddleware<FromOneThousandToOneHundredThousandMiddleware>();
        }
    }
}