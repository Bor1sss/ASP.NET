namespace RequestProcessingPipeline
{
    public static class From2000_3125EXT
    {
        public static IApplicationBuilder UseFrom2000_3125(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From2000_3125Middle>();
        }
    }
}
