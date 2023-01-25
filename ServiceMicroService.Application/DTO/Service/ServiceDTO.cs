using ServiceMicroService.Application.Dto.Category;
using ServiceMicroService.Application.Dto.Specialization;

namespace ServiceMicroService.Application.Dto.Service;

public class ServiceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }

    public CategoryDto Category { get; set; }
    public SpecializationDto Specialization { get; set; }
}