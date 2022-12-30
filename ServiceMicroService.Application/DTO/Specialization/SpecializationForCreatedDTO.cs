using System.ComponentModel.DataAnnotations;

namespace ServiceMicroService.Application.Dto.Specialization;

public class SpecializationForCreatedDto
{
    [Required] public string Name { get; set; }

    [Required] public bool IsActive { get; set; }
}