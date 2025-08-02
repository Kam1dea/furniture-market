using AutoMapper;
using Furniture.Application.Dtos.Review;
using Furniture.Domain.Entities;

namespace Furniture.Application.Mapping;

public class ReviewProfile: Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>().ReverseMap();

        CreateMap<CreateReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<UpdateReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}