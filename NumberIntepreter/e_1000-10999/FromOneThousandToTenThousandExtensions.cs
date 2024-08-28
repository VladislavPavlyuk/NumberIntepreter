namespace NumberIntepreter
{

    public static class FromOneThousandToTenThousandExtensions
    {
        public static IApplicationBuilder UseOnesThousand(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromOneThousandToTenThousandMiddleware>();
        }
    }
}