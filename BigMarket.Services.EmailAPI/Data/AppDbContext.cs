using BigMarket.Services.EmailAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BigMarket.Services.EmailAPI.Data
{
    public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<EmailLogger> EmailLoggers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
