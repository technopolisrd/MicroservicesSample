using AutoMapper;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.DTO;

namespace Mango.Services.ShoppingCartAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDTO, Product>().ReverseMap();
            config.CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();
            config.CreateMap<CartDetailDTO, CartDetail>().ReverseMap();
            config.CreateMap<CartDTO, Cart>().ReverseMap();
        });
        return mappingConfig;
    }
}
