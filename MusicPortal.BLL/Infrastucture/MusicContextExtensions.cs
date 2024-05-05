using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.EF;


namespace MusicPortal.BLL.Infrastucture
{
    public static class MusicContextExtensions
    {
        public static void AddMusicContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MusicContext>(options => options.UseSqlServer(connection));
        }
    }
}
