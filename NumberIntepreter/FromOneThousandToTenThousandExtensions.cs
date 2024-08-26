namespace NumberIntepreter
{

    public static class FromOneThousandToTenThousandExtensions
    { 
        public static IApplicationBuilder UseThousand(this IApplicationBuilder builder)
        { 
            return builder.UseMiddleware<FromOneThousandToTenThousandMiddleware>();
        }
    }
}