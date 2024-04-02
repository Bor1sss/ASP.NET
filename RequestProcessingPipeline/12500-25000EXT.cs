namespace RequestProcessingPipeline
{
    public static class From12500_25000EXT
    {
        public static IApplicationBuilder UseFrom12500_25000(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From12500_25000Middle>();
        }
    }
}
