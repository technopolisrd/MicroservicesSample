using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTO;

namespace Mango.Services.CouponAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CouponDTO, Coupon>().ReverseMap();
            //config.CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();
            //config.CreateMap<CartDetailDTO, CartDetail>().ReverseMap();
            //config.CreateMap<CartDTO, Cart>().ReverseMap();
        });
        return mappingConfig;
    }
}
