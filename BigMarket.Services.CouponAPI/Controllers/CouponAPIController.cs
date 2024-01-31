using AutoMapper;
using BigMarket.Services.CouponAPI.Data;
using BigMarket.Services.CouponAPI.Models;
using BigMarket.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigMarket.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController(AppDbContext db, IMapper mapper) : ControllerBase
    {
        private readonly AppDbContext _db = db;
        private readonly ResponseDto _response = new();
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Coupon> couponList = await _db.Coupons.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(couponList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("{id:int}")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                var coupon = await _db.Coupons.FirstAsync(c => c.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetByCode/{code}")]
        public async Task<ResponseDto> GetByCode(string code)
        {
            try
            {
                var coupon = await _db.Coupons.FirstAsync(c => c.CouponCode == code);
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]  // TODO change from hatd code
        public async Task<ResponseDto> Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                await _db.Coupons.AddAsync(coupon);
                await _db.SaveChangesAsync();
                _response.Result = couponDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]  // TODO change from hatd code
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(coupon);  // TODO check need or not async
                _db.SaveChanges();
                _response.Result = couponDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]  // TODO change from hatd code
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(c => c.CouponId == id);
                _db.Coupons.Remove(coupon); // TODO check need or not async
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
