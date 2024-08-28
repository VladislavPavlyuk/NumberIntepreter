namespace NumberIntepreter
{

    public static class FromElevenThousandToTwentyThousandExtensions
    { 
        public static IApplicationBuilder UseTeensThousand(this IApplicationBuilder builder)
        { 
            return builder.UseMiddleware<FromElevenThousandToTwentyThousandMiddleware>();
        }
    }
}