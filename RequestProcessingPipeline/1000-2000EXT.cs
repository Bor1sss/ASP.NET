namespace RequestProcessingPipeline
{
    public static class From1000_2000EXT
    {
        public static IApplicationBuilder UseFrom1000_2000(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From1000_2000Middle>();
        }
    }
}
