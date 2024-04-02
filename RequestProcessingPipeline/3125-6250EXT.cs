namespace RequestProcessingPipeline
{
    public static class From3125_6250EXT
    {
        public static IApplicationBuilder UseFrom3125_6250(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From3125_6250Middle>();
        }
    }
}
