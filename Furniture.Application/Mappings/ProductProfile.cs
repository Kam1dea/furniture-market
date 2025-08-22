using AutoMapper;
using Furniture.Application.Dtos.Product;
using Furniture.Domain.Entities;

namespace Furniture.Application.Mappings;

public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();

        CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<UpdateProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}