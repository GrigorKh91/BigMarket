using BigMarket.Services.OrderAPI.Models.Dto;

namespace BigMarket.Services.OrderAPI.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
