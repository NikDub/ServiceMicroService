using ServiceMicroService.Application.Dto.Service;

namespace ServiceMicroService.Application.Dto.Specialization;

public class SpecializationWithServiceDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public ICollection<ServiceDto> Services { get; set; }
}