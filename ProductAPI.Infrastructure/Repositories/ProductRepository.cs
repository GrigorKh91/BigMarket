using BigMarket.Services.ProductAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Core.Services.IServices;

namespace ProductAPI.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext db) : IProductRepository
    {
        private readonly AppDbContext _db = db;

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            Product product = await _db.Products.FirstAsync(c => c.ProductId == id);
            _db.Products.Remove(product);
            return _db.SaveChanges() > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            IEnumerable<Product> productList = await _db.Products.ToListAsync();
            return productList;
        }

        public async Task<IEnumerable<string>> GetAllProductsNameAsync()
        {
            IEnumerable<string> productList = await _db.Products.Select(p => p.Name).ToListAsync();
            return productList;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _db.Products.FirstAsync(c => c.ProductId == id);
            return product;
        }

        public Product UpdateProductAsync(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return product;
        }
    }
}
