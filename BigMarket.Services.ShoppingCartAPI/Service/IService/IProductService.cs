using BigMarket.Services.ShoppingCartAPI.Models.Dto;

namespace BigMarket.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
