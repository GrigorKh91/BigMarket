using BigMarket.Services.ProductAPI.Models.Dto;

namespace BigMarket.Services.ProductAPI.Services.IServices
{
    public interface IProductService
    {
        Task<ResponseDto> GetAllProductsAsync();
        Task<ResponseDto> GetProductByIdAsync(int id);
        Task<ResponseDto> CreateProductAsync(ProductDto ProductDto, string baseUrl);
        ResponseDto UpdateProductAsync(ProductDto ProductDto, string baseUrl);
        Task<ResponseDto> DeleteProductAsync(int id);
    }
}
