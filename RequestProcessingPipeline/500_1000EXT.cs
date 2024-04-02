namespace RequestProcessingPipeline
{
    public static class From500_1000EXT
    {
        public static IApplicationBuilder UseFrom500_1000(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From500_1000Middle>();
        }
    }
}
