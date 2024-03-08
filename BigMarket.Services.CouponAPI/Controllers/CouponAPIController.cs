using AutoMapper;
using BigMarket.Services.CouponAPI.Data;
using BigMarket.Services.CouponAPI.Models;
using BigMarket.Services.CouponAPI.Models.Dto;
using BigMarket.Services.CouponAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BigMarket.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    //[Authorize]
    public class CouponAPIController(ICouponService couponService) : ControllerBase
    {
        private readonly ICouponService _couponService = couponService;

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            ResponseDto _response = await _couponService.GetAsync();
            return _response;
        }

        [HttpGet("{id:int}")]
        public async Task<ResponseDto> Get(int id)
        {
            ResponseDto _response = await _couponService.GetAsync(id);
            return _response;
        }

        [HttpGet("GetByCode/{code}")]
        public async Task<ResponseDto> GetByCode(string code)
        {
            ResponseDto _response = await _couponService.GetByCodeAsync(code);
            return _response;
        }

        [HttpPost]
       // [Authorize(Roles = "ADMIN")]  // TODO change from hatd code
        public async Task<ResponseDto> Post([FromBody] CouponDto couponDto)
        {
            ResponseDto _response = await _couponService.CreateAsync(couponDto);
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]  // TODO change from hatd code
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            ResponseDto _response = _couponService.Update(couponDto);
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]  // TODO change from hatd code
        public ResponseDto Delete(int id)
        {
            ResponseDto _response = _couponService.Delete(id);
            return _response;
        }
    }
}
