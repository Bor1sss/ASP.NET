namespace RequestProcessingPipeline
{
    public static class From100_500EXT
    {
        public static IApplicationBuilder UseFrom100_1000(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From100_1000Middle>();
        }
    }
}
