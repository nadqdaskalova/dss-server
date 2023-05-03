using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DSS_Backend.Models;

namespace DSS_Backend.Helpers
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;

        public DataContext(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(_config.GetConnectionString("dss_backend"));
    }
}
