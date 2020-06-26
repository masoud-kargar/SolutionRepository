using AllInterfaces.Interface;

using AllServises.Claims;
using AllServises.Services;

using Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PersianTranslation.Identity;

using System;

using WebRepository.Utility;

namespace WebRepository {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            var connectionString = Configuration.GetConnectionString("Default");
            services.AddEntityFeamework(connectionString);
            services.AddControllersWithViews();
            services.AddMvc(x => x.EnableEndpointRouting = false);
            services.AddAuthentication().AddGoogle(p => {
                p.ClientId = "287011403834-oi2lf5nogfdg27eqo7080bhqkebraefu.apps.googleusercontent.com";
                p.ClientSecret = "DD7mPac1BKBCB-gusSS9tT_h";
            });
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
                p.AddPolicy("ClaimsAddPolicy", policy => policy.RequireClaim(ClaimTypeStpre.ClaimsAdd,true.ToString()));
                p.AddPolicy("ClaimsDeletePolicy", policy => policy.RequireClaim(ClaimTypeStpre.ClaimsRemove,true.ToString()));

            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); } else { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            //Areas
            app.UseEndpoints(

                endpoints => {
                    endpoints.MapControllerRoute(
                        name: "IdentityArea",
                        pattern: "{area:exists}/{controller=ManageRole}/{action=Index}/{id?}"
                    );
                });
            app.UseEndpoints(
                endpoints => {
                    endpoints.MapControllerRoute(
                        name: "IdentityArea",
                        pattern: "{area:exists}/{controller=ManageUser}/{action=Index}/{id?}"
                    );
                });
            app.UseEndpoints(
                endpoints => {
                    endpoints.MapControllerRoute(
                        name: "CategoryArea",
                        pattern: "{area:exists}/{controller=Categories}/{action=Index}/{id?}"
                    );
                });
            app.UseEndpoints(
                endpoints => {
                    endpoints.MapControllerRoute(
                        name: "ClaimsArea",
                        pattern: "{area:exists}/{controller=Claims}/{action=AddUserToClaim}/{id?}"
                    );
                });
            app.UseEndpoints(
                endpoints => {
                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                   );
                });

        }
    }
}
