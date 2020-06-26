using Data;

using AllInterfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AllServises;
using AllInterfaces.Interface;
using AllServises.Services;
using Microsoft.AspNetCore.Identity;
using System;
using PersianTranslation.Identity;

namespace WebRepository.Utility {
    public static class EFExtensions {
        public static IServiceCollection AddEntityFeamework(this IServiceCollection services, string connectionString) {
            services.AddDbContext<PanelContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly(nameof(WebRepository))));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMessageSender, MessageSender>();
            services.AddIdentity<IdentityUser, IdentityRole>(p => {
                p.User.RequireUniqueEmail = true;
                p.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";
                p.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
                p.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<PanelContext>().AddDefaultTokenProviders().AddErrorDescriber<PersianIdentityErrorDescriber>();
            return services;
        }
        
    }
}
