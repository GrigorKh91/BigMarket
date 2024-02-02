using BigMarket.Services.ShoppingCartAPI.Models.Dto;

namespace BigMarket.Services.ShoppingCartAPI.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
