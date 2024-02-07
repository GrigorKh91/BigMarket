using BigMarket.Services.RewardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BigMarket.Services.RewardAPI.Data
{
    public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Rewards> Rewards { get; set; }
    }
}
