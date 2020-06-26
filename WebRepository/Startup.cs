
using AllServises;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebRepository {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            var connectionString = Configuration.GetConnectionString("Default");
            services.AddEntityFeamework(connectionString);
            services.AddAuthentication().AddGoogle(p => { p.ClientId = "287011403834-oi2lf5nogfdg27eqo7080bhqkebraefu.apps.googleusercontent.com"; p.ClientSecret = "DD7mPac1BKBCB-gusSS9tT_h"; });
            services.GetAddClaims();
            services.AddControllersWithViews();
            services.AddMvc(x => x.EnableEndpointRouting = false);
            services.AddMemoryCache();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); } else { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //Areas
            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name: "CategoryArea", pattern: "{area:exists}/{controller=Categories}/{action=Index}/{id?}"); });
            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name: "ClaimsArea", pattern: "{area:exists}/{controller=Claims}/{action=AddUserToClaim}/{id?}"); });
            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name: "IdentityArea", pattern: "{area:exists}/{controller=ManageRole}/{action=Index}/{id?}"); });
            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name: "IdentityArea", pattern: "{area:exists}/{controller=ManageUser}/{action=Index}/{id?}"); });
            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name: "SiteSettingArea", pattern: "{area:exists}/{controller=SiteSetting}/{action=Index}/{id?}"); });
            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name: "SiteSettingArea", pattern: "{area:exists}/{controller=Role}/{action=Index}/{id?}"); });
            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"); });
        }
    }
}
