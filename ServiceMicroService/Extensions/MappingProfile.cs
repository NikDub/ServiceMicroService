using AutoMapper;
using ServiceMicroService.Application.DTO.Service;
using ServiceMicroService.Application.DTO.Specialization;
using ServiceMicroService.Domain.Entities.Models;

namespace ProfilesMicroService.Api.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Service, ServiceDTO>()
                .ForMember("CategoryName", r => r.MapFrom(c => c.Category.CategoryName))
                .ForMember("SpecializationName", r => r.MapFrom(c => c.Specialization.SpecializationName));

            CreateMap<ServiceForCreatedDTO, Service>().ReverseMap();
            CreateMap<ServiceForUpdateDTO, Service>().ReverseMap();

            CreateMap<Specialization, SpecializationDTO>().ReverseMap();
            CreateMap<Specialization, SpecializationForCreatedDTO>().ReverseMap();
            CreateMap<Specialization, SpecializationForUpdateDTO>().ReverseMap();
            CreateMap<Specialization, SpecializationWithServiceDTO>().ReverseMap();
        }
    }
}
