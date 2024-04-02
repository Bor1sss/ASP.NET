namespace RequestProcessingPipeline
{
    public static class From25000_50000EXT
    {
        public static IApplicationBuilder UseFrom25000_50000(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From25000_50000Middle>();
        }
    }
}
