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
using Microsoft.AspNetCore.Authorization;
using AllServises.Claims;

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
        public static IServiceCollection GetAddClaims(this IServiceCollection services) {
            services.AddAuthorization(p => {
                //Role
                p.AddPolicy("RoleListPolicy", policy => policy.RequireClaim(ClaimTypeStpre.RoleList, true.ToString()));
                p.AddPolicy("RoleAddPolicy", policy => policy.RequireClaim(ClaimTypeStpre.RoleAdd, true.ToString()));
                p.AddPolicy("RoleEditPolicy", policy => policy.RequireClaim(ClaimTypeStpre.RoleEdit, true.ToString()));
                p.AddPolicy("RoleDeletePolicy", policy => policy.RequireClaim(ClaimTypeStpre.RoleDelete, true.ToString()));
                p.AddPolicy("RoleDetailsPolicy", policy => policy.RequireClaim(ClaimTypeStpre.RoleDetails, true.ToString()));


                //User
                p.AddPolicy("UserListPolicy", policy => policy.RequireClaim(ClaimTypeStpre.UserList, true.ToString()));
                p.AddPolicy("UserAddPolicy", policy => policy.RequireClaim(ClaimTypeStpre.UserAdd, true.ToString()));
                p.AddPolicy("UserEditPolicy", policy => policy.RequireClaim(ClaimTypeStpre.UserEdit, true.ToString()));
                p.AddPolicy("UserDeletePolicy", policy => policy.RequireClaim(ClaimTypeStpre.UserDelete, true.ToString()));
                p.AddPolicy("UserDetailsPolicy", policy => policy.RequireClaim(ClaimTypeStpre.UserDetails, true.ToString()));

                //Category
                p.AddPolicy("CategoryListPolicy", policy => policy.RequireClaim(ClaimTypeStpre.CategoryList, true.ToString()));
                p.AddPolicy("CategoryAddPolicy", policy => policy.RequireClaim(ClaimTypeStpre.CategoryAdd, true.ToString()));
                p.AddPolicy("CategoryEditPolicy", policy => policy.RequireClaim(ClaimTypeStpre.CategoryEdit, true.ToString()));
                p.AddPolicy("CategoryDeletePolicy", policy => policy.RequireClaim(ClaimTypeStpre.CategoryDelete, true.ToString()));
                p.AddPolicy("CategoryDetailsPolicy", policy => policy.RequireClaim(ClaimTypeStpre.CategoryDetails, true.ToString()));

                //Sub Category
                p.AddPolicy("SubCategoryListPolicy", policy => policy.RequireClaim(ClaimTypeStpre.SubCategoryList, true.ToString()));
                p.AddPolicy("SubCategoryAddPolicy", policy => policy.RequireClaim(ClaimTypeStpre.SubCategoryAdd, true.ToString()));
                p.AddPolicy("SubCategoryEditPolicy", policy => policy.RequireClaim(ClaimTypeStpre.SubCategoryEdit, true.ToString()));
                p.AddPolicy("SubCategoryDeletePolicy", policy => policy.RequireClaim(ClaimTypeStpre.SubCategoryDelete, true.ToString()));
                p.AddPolicy("SubCategoryDetailsPolicy", policy => policy.RequireClaim(ClaimTypeStpre.SubCategoryDetails, true.ToString()));

                //Claims
                p.AddPolicy("ClaimsAddPolicy", policy => policy.RequireClaim(ClaimTypeStpre.ClaimsAdd, true.ToString()));
                p.AddPolicy("ClaimsDeletePolicy", policy => policy.RequireClaim(ClaimTypeStpre.ClaimsRemove, true.ToString()));

                // Claim Mix
                p.AddPolicy("ClaimsAdmin", policy => policy.RequireAssertion(ClaimsOrRole));

            });
            return services;
        }
        private static bool ClaimsOrRole(AuthorizationHandlerContext context) => context.User.HasClaim(ClaimTypeStpre.ClaimsAdd, true.ToString()) && context.User.IsInRole("Owner");
    }

}
