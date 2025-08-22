using AutoMapper;
using Furniture.Application.Dtos.Review;
using Furniture.Domain.Entities;

namespace Furniture.Application.Mappings;

public class ReviewProfile: Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>()
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName));

        CreateMap<CreateReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<UpdateReviewDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}