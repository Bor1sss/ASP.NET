namespace RequestProcessingPipeline
{
    public static class From100_500EXT
    {
        public static IApplicationBuilder UseFrom100_500(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From100_500Middle>();
        }
    }
}
