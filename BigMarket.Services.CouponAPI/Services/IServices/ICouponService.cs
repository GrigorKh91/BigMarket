using BigMarket.Services.CouponAPI.Models.Dto;

namespace BigMarket.Services.CouponAPI.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDto> GetAsync();
        Task<ResponseDto> GetAsync(int id);
        Task<ResponseDto> GetByCodeAsync(string code);
        Task<ResponseDto> CreateAsync(CouponDto couponDto);
        ResponseDto Update(CouponDto couponDto);
        ResponseDto Delete(int id);
    }
}
