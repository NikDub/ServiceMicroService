using Microsoft.EntityFrameworkCore;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure.DataBaseConfiguration;

namespace ServiceMicroService.Infrastructure
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureTables(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }
        private void ConfigureTables(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
