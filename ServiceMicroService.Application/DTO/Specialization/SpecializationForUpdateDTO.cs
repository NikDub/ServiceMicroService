using System.ComponentModel.DataAnnotations;

namespace ServiceMicroService.Application.Dto.Specialization;

public class SpecializationForUpdateDto
{
    [Required] public string Name { get; set; }

    [Required] public bool IsActive { get; set; }
}