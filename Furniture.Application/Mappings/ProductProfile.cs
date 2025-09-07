using AutoMapper;
using Furniture.Application.Dtos.Product;
using Furniture.Domain.Entities;

namespace Furniture.Application.Mappings;

public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();

        CreateMap<CreateProductWithImageDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.WorkerProfileId, opt => opt.Ignore());
        
        CreateMap<UpdateProductWithImageDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}