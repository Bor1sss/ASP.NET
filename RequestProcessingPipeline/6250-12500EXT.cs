namespace RequestProcessingPipeline
{
    public static class From6250_12500EXT
    {
        public static IApplicationBuilder UseFrom6250_12500(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From6250_12500Middle>();
        }
    }
}
