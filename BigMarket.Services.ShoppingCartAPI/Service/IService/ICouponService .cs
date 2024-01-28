using BigMarket.Services.ShoppingCartAPI.Models.Dto;

namespace BigMarket.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon( string couponCode);
    }
}
