namespace NumberIntepritator
{
    public static class FromElevenToNineteenExtensions
    {
        public static IApplicationBuilder UseTeens(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromElevenToNineteenMiddleware>();
        }
    }
}
