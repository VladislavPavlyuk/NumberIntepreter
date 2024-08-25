namespace NumberIntepritator
{
    public static class FromTwentyToHundredExtensions
    {
        public static IApplicationBuilder UseTens(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromTwentyToHundredMiddleware>();
        }
    }
}
