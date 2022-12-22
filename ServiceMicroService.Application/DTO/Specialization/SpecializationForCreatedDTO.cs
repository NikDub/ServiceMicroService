using System.ComponentModel.DataAnnotations;

namespace ServiceMicroService.Application.DTO.Specialization
{
    public class SpecializationForCreatedDTO
    {
        [Required]
        public string SpecializationName { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
