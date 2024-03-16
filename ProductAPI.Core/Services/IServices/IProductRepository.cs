using BigMarket.Services.ProductAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Core.Services.IServices
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<string>> GetAllProductsNameAsync(); // TODO : for api versioning test
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Product UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
