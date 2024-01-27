using AutoMapper;
using BigMarket.Services.ProductAPI.Models;
using BigMarket.Services.ProductAPI.Models.Dto;


namespace BigMarket.Services.ProductAPI
{
    public sealed class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
