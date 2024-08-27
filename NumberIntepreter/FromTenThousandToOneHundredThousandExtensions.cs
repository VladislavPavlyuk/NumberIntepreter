namespace NumberIntepreter
{

    public static class FromTenThousandToOneHundredThousandExtensions
    { 
        public static IApplicationBuilder UseTenThousand(this IApplicationBuilder builder)
        { 
            return builder.UseMiddleware<FromTenThousandToOneHundredThousandMiddleware>();
        }
    }
}