using AutoMapper;
using BigMarket.Services.OrderAPI.Models;
using BigMarket.Services.OrderAPI.Models.Dto;
using Microsoft.EntityFrameworkCore.Storage;



namespace BigMarket.Services.ShoppingCartAPI
{
    public sealed class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderHeaderDto, CartHeaderDto>()
                .ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal)).ReverseMap();

                config.CreateMap<CartDetalisDto, OrderDetalisDto>()
              .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Name))
              .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));

                config.CreateMap<OrderDetalisDto, CartDetalisDto>();

                config.CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
                config.CreateMap<OrderDetalisDto, OrderDetalis>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
