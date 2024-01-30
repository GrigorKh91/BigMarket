using BigMarket.Web.Models;
using BigMarket.Web.Models.ShoppingCartAPI;
using BigMarket.Web.Service.IService;
using BigMarket.Web.Utility;

namespace BigMarket.Web.Service
{
    public sealed class CartService(IBaseService baseService) : ICartService
    {
        private readonly IBaseService _baseService = baseService;

        public async Task<ResponseDto> ApplayCoupontAsync(CartDto cartDto)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/ApplayCoupon"
            };
            return await _baseService.SendAsync(request);
        }
        public async Task<ResponseDto> GetcartByUserIdAsync(string userId)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + userId
            };
            return await _baseService.SendAsync(request);
        }
        public async Task<ResponseDto> RemoveFromCartAsync(int cartDetalisId)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDetalisId,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart"
            };
            return await _baseService.SendAsync(request);
        }
        public async Task<ResponseDto> UpsertCartasync(CartDto cartDto)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/CartUpsent"
            };
            return await _baseService.SendAsync(request);
        }
    }
}
