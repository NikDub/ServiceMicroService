using AutoMapper;
using ServiceMicroService.Application.Dto.Category;
using ServiceMicroService.Application.Dto.Service;
using ServiceMicroService.Application.Dto.Specialization;
using ServiceMicroService.Domain.Entities.Models;

namespace ServiceMicroService.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Service, ServiceDto>();
        CreateMap<ServiceForCreatedDto, Service>().ReverseMap();
        CreateMap<ServiceForUpdateDto, Service>().ReverseMap();

        CreateMap<Specialization, SpecializationDto>().ReverseMap();
        CreateMap<Specialization, SpecializationForCreatedDto>().ReverseMap();
        CreateMap<Specialization, SpecializationForUpdateDto>().ReverseMap();
        CreateMap<Specialization, SpecializationWithServiceDto>().ReverseMap();

        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}