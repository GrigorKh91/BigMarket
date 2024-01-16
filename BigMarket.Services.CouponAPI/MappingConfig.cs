using AutoMapper;
using BigMarket.Services.CouponAPI.Models;
using BigMarket.Services.CouponAPI.Models.Dto;

namespace BigMarket.Services.CouponAPI
{
    public sealed class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
