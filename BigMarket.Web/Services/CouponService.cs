using BigMarket.Web.Models;
using BigMarket.Web.Models.CouponApi;
using BigMarket.Web.Services.IServices;
using BigMarket.Web.Utility;

namespace BigMarket.Web.Services
{
    public sealed class CouponService(IBaseService baseService) : ICouponService
    {
        private readonly IBaseService _baseService = baseService;

        public async Task<ResponseDto> CreateCouponAsync(CouponDto couponDto)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon/"
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> DeleteCouponAsync(int id)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/coupon/" + id
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> GetAllCouponsAsync()
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon"
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> GetCouponAsync(string couponCode)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> GetCouponByIdAsync(int id)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + id
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/coupon/"
            };
            return await _baseService.SendAsync(request);
        }
    }
}
