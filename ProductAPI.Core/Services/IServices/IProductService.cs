using BigMarket.Services.ProductAPI.Core.Models.Dto;
namespace BigMarket.Services.ProductAPI.Services.IServices
{
    public interface IProductService
    {
        Task<ResponseDto> GetAllProductsAsync();
        Task<ResponseDto> GetAllProductsNameAsync(); // TODO : for api versioning test
        Task<ResponseDto> GetProductByIdAsync(int id);
        Task<ResponseDto> CreateProductAsync(ProductDto ProductDto, string baseUrl);
        ResponseDto UpdateProductAsync(ProductDto ProductDto, string baseUrl);
        Task<ResponseDto> DeleteProductAsync(int id);
    }
}
