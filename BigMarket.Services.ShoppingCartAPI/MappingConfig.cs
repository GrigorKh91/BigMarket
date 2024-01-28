using AutoMapper;
using BigMarket.Services.ShoppingCartAPI.Models;
using BigMarket.Services.ShoppingCartAPI.Models.Dto;


namespace BigMarket.Services.ShoppingCartAPI
{
    public sealed class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetalis, CartDetalisDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
