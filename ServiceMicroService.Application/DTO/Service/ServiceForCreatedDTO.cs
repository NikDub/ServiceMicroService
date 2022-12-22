using System.ComponentModel.DataAnnotations;

namespace ServiceMicroService.Application.DTO.Service
{
    public class ServiceForCreatedDTO
    {
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public string CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string SpecializationId { get; set; }
        [Required]
        public string SpecializationName { get; set; }
    }
}
