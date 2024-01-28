using BigMarket.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BigMarket.Services.ShoppingCartAPI.Data
{
    public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetalis> CartDetalis { get; set; }
    }
}
