namespace RequestProcessingPipeline
{
    public static class From10k_10kkEXT
    {
        public static IApplicationBuilder UseFrom10k_10kk(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From10k_10kkMiddle>();
        }
    }
}
