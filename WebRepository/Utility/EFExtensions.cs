using Data;

using IGenericRepository;
using IGenericRepository.Interface;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebRepository.Utility {
    public static class EFExtensions {
        public static IServiceCollection AddEntityFeamework(this IServiceCollection services,string connectionString) {
            services.AddDbContext<PanelContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly(nameof(WebRepository))));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
