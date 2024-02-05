using BigMarket.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BigMarket.Services.OrderAPI.Data
{
    public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetalis> OredrDetalis { get; set; }
    }
}
