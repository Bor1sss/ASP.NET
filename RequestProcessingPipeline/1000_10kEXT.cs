namespace RequestProcessingPipeline
{
    public static class From1000_10kEXT
    {
        public static IApplicationBuilder UseFrom1000_10k(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From1000_10kMiddle>();
        }
    }
}
