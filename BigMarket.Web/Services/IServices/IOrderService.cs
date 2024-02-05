using BigMarket.Web.Models;
using BigMarket.Web.Models.ShoppingCartAPI;

namespace BigMarket.Web.Services.IServices
{
    public interface IOrderService
    {
        Task<ResponseDto> CreateOrder(CartDto cartDto);
    }
}
