using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using AspCoreGroupe12025.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AspCoreGroupe12025.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            options.UseInMemoryDatabase("TestDb");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Flotte> Flottes { get; set; }

    }
}
