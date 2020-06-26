using Domain;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System.Reflection;



namespace Data {
    public class PanelContext : IdentityDbContext {
        public PanelContext(DbContextOptions<PanelContext> options) : base(options) {
            Options = options;
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogNew> BlogNews { get; set; }
        public DbSet<PageComment> PageComments { get; set; }
        public DbContextOptions<PanelContext> Options { get; }
        public DbSet<Setting> SiteSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //Seed Data
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2", Name = "Admin" });
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "1", Name = "Owner" });
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "3", Name = "User" });
            //modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser {Id = "1",Email = "iqprogrammer@outlook.com", EmailConfirmed = true,PasswordHash = "(BLOOD)_(0)-(0)_", UserName = "Owner"});
            //modelBuilder.Entity<IdentityUserRole>().HasData(new IdentityUserRole { UserId = "1" ,RoleId = "1"});
            //modelBuilder.Entity<IdentityUserRole>().HasData(new IdentityUserRole { UserId = "1" ,RoleId = "2"});
            //modelBuilder.Entity<IdentityUserClaim>().HasData(new IdentityUserClaim { UserId = "1", ClaimType = "ClaimsAddPolicy" });
            //modelBuilder.Entity<IdentityUserClaim>().HasData(new IdentityUserClaim { UserId = "1", ClaimType = "ClaimsDeletePolicy" });

            //modelBuilder.Entity<IdentityUserClaim>().HasData(new IdentityUserClaim { UserId = "1", ClaimType = "RoleListPolicy" });
            //modelBuilder.Entity<IdentityUserClaim>().HasData(new IdentityUserClaim { UserId = "1", ClaimType = "RoleAddPolicy" });
            //modelBuilder.Entity<IdentityUserClaim>().HasData(new IdentityUserClaim { UserId = "1", ClaimType = "RoleEditPolicy" });
            //modelBuilder.Entity<IdentityUserClaim>().HasData(new IdentityUserClaim { UserId = "1", ClaimType = "RoleDeletePolicy" });

            //modelBuilder.Entity<IdentityUserClaim>().HasData(new IdentityUserClaim { UserId = "1", ClaimType = "UserListPolicy" });
            //modelBuilder.Entity<IdentityUserClaim>().HasData(new IdentityUserClaim { UserId = "1", ClaimType = "UserAddPolicy" });
            //modelBuilder.Entity<IdentityUserClaim>().HasData(new IdentityUserClaim { UserId = "1", ClaimType = "UserEditPolicy" });
            //modelBuilder.Entity<IdentityUserClaim>().HasData(new IdentityUserClaim { UserId = "1", ClaimType = "UserDeletePolicy" });
        }
    }
}
