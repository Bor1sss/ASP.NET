namespace RequestProcessingPipeline
{
    public static class From50000_100000EXT
    {
        public static IApplicationBuilder UseFrom50000_100000(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From50000_100000Middle>();
        }
    }
}
