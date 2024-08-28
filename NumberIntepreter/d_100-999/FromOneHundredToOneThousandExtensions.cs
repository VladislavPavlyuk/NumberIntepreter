namespace NumberIntepreter
{
public static class FromOneHundredToOneThousandExtensions
{ 
    public static IApplicationBuilder UseHundred(this IApplicationBuilder builder)
    { 
        return builder.UseMiddleware<FromOneHundredToOneThousandMiddleware>();
    }
}}