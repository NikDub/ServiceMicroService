using ServiceMicroService.Application.DTO.Service;

namespace ServiceMicroService.Application.DTO.Specialization;

public class SpecializationWithServiceDto
{
    public string Id { get; set; }
    public string SpecializationName { get; set; }
    public bool IsActive { get; set; }

    public ICollection<ServiceDto> Services { get; set; }
}