namespace NumberIntepritator
{
    public static class FromOneToTenExtensions
    {
        public static IApplicationBuilder UseOnes(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromOneToTenMiddleware>();
        }
    }
}
