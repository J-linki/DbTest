using DbTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DbTest.Data
{
    public class TestContext : DbContext
    {
        //public TestContext(DbContextOptions<TestContext> options): base(options) 
        //{ }
        public DbSet<UserModel>? Users { get; set; }

        public DbSet<StocksModel> Stocks {  get; set; }

        public DbSet<CompanyModel> Companys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DatabaseTest"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
