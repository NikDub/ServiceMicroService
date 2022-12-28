using AutoMapper;
using ServiceMicroService.Application.DTO.Service;
using ServiceMicroService.Application.DTO.Specialization;
using ServiceMicroService.Domain.Entities.Models;

namespace ServiceMicroService.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Service, ServiceDto>()
            .ForMember("CategoryName", r => r.MapFrom(c => c.Category.CategoryName))
            .ForMember("SpecializationName", r => r.MapFrom(c => c.Specialization.SpecializationName));

        CreateMap<ServiceForCreatedDto, Service>().ReverseMap();
        CreateMap<ServiceForUpdateDto, Service>().ReverseMap();

        CreateMap<Specialization, SpecializationDto>().ReverseMap();
        CreateMap<Specialization, SpecializationForCreatedDto>().ReverseMap();
        CreateMap<Specialization, SpecializationForUpdateDto>().ReverseMap();
        CreateMap<Specialization, SpecializationWithServiceDto>().ReverseMap();
    }
}