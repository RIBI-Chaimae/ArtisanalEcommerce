using Artisanal.Service.ProductAPI.Models;
using Artisanal.Service.ProductAPI.Models.Dto;
using AutoMapper;

namespace Artisanal.Service.ProductAPI
{
    public class MappingConfig
    {public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(
                config =>
                {
                    config.CreateMap<ProductDto, Product>();
                    config.CreateMap<Product, ProductDto>();
                }
                
                
                
                
                );
            return mappingConfig;
        }
    }
}
