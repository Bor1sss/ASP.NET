using Microsoft.Extensions.DependencyInjection;
using MusicPortal.DAL.IRepository;
using MusicPortal.DAL.Repositories;

namespace MusicPortal.BLL.Infrastucture
{
    public static class UnitOfWorkServiceExtensions
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, EFUnitOfWork>();
        }
    }
}
