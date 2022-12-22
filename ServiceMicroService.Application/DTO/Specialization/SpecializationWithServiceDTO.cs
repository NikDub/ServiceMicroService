using ServiceMicroService.Application.DTO.Service;

namespace ServiceMicroService.Application.DTO.Specialization
{
    public class SpecializationWithServiceDTO
    {
        public string Id { get; set; }
        public string SpecializationName { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ServiceDTO> Services { get; set; }
    }
}
