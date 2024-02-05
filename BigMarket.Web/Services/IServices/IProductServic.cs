using BigMarket.Web.Models;
using BigMarket.Web.Models.ProductAPI;

namespace BigMarket.Web.Services.IServices
{
    public interface IProductService
    {
        Task<ResponseDto> GetAllProductsAsync();
        Task<ResponseDto> GetProductByIdAsync(int id);
        Task<ResponseDto> CreateProductAsync(ProductDto ProductDto);
        Task<ResponseDto> UpdateProductAsync(ProductDto ProductDto);
        Task<ResponseDto> DeleteProductAsync(int id);
    }
}
