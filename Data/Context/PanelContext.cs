using Domain;

using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace Data {
    public class PanelContext :DbContext {
        public PanelContext(DbContextOptions<PanelContext> options) : base(options) { }
        public DbSet<Person> People { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogNew> BlogNews { get; set; }
        public DbSet<PageComment> PageComments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //Seed Data
            modelBuilder.Entity<Person>().HasData(new Person { Name = "Masoud", Family = "Kargar" });
        }
    }
}
