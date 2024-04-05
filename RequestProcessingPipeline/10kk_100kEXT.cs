namespace RequestProcessingPipeline
{
    public static class From10kk_100kEXT
    {
        public static IApplicationBuilder UseFrom10kk_100k(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From10kk_100kMiddle>();
        }
    }
}
