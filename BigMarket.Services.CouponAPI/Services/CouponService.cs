using BigMarket.Services.CouponAPI.Services.IServices;
using AutoMapper;
using BigMarket.Services.CouponAPI.Data;
using BigMarket.Services.CouponAPI.Models;
using BigMarket.Services.CouponAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using BigMarket.Services.CouponAPI.CustomExceptions;

namespace BigMarket.Services.CouponAPI.Services
{
    public class CouponService(AppDbContext db,
                                                   IMapper mapper,
                                                   ILogger<CouponService> logger) : ICouponService
    {
        private readonly AppDbContext _db = db;
        private readonly ResponseDto _response = new();
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<CouponService> _logger = logger;

        public async Task<ResponseDto> GetAsync()
        {
            IEnumerable<Coupon> couponList = await _db.Coupons.ToListAsync();
            _response.Result = _mapper.Map<IEnumerable<CouponDto>>(couponList);
            return _response;
        }

        public async Task<ResponseDto> GetAsync(int id)
        {
            // _logger.LogInformation("Get action of CouponService**********************");
            // _logger.LogInformation($"id :{id} *******************");
            var coupon = await _db.Coupons.FirstAsync(c => c.CouponId == id);
            _response.Result = _mapper.Map<CouponDto>(coupon);
            return _response;
        }

        public async Task<ResponseDto> GetByCodeAsync(string code)
        {
            var coupon = await _db.Coupons.FirstAsync(c => c.CouponCode == code);
            _response.Result = _mapper.Map<CouponDto>(coupon);
            return _response;
        }

        public async Task<ResponseDto> CreateAsync(CouponDto couponDto)
        {

            // using (Operation.Time("Time for create coupon to database"))
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                await _db.Coupons.AddAsync(coupon);
                await _db.SaveChangesAsync();

                var options = new Stripe.CouponCreateOptions
                {
                    AmountOff = (long)(couponDto.DiscountAmount * 100),
                    Name = couponDto.CouponCode,
                    Currency = "usd",
                    Id = couponDto.CouponCode
                };
                var service = new Stripe.CouponService();
                service.Create(options);
            }
            _response.Result = couponDto;
            return _response;
        }

        public ResponseDto Update(CouponDto couponDto)
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Update(coupon);  // TODO check need or not async
            _db.SaveChanges();
            _response.Result = couponDto;
            return _response;
        }

        public ResponseDto Delete(int id)
        {

            Coupon coupon = _db.Coupons.FirstOrDefault(c => c.CouponId == id);
            if (coupon is null) // TODO: for custom exception test
            {
                throw new InvalidCouponIDException("Given coupon id doesn't exist");
            }
            _db.Coupons.Remove(coupon); // TODO check need or not async
            _db.SaveChanges();

            var service = new Stripe.CouponService();
            service.Delete(coupon.CouponCode);
            return _response;
        }
    }
}
