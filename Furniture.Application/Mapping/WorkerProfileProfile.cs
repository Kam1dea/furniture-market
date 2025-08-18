using AutoMapper;
using Furniture.Application.Dtos.WorkerProfile;
using Furniture.Domain.Entities;

namespace Furniture.Application.Mapping;

public class WorkerProfileProfile: Profile
{
    public WorkerProfileProfile()
    {
        CreateMap<WorkerProfileDto, WorkerProfile>().ReverseMap();
        
        CreateMap<UpdateWorkerProfileDto, WorkerProfile>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateWorkerProfileDto, WorkerProfile>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());;
    } 
}