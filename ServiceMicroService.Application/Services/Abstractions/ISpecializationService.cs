using ServiceMicroService.Application.DTO.Specialization;

namespace ServiceMicroService.Application.Services.Abstractions
{
    public interface ISpecializationService
    {
        Task<List<SpecializationDTO>> GetAsync();
        Task<SpecializationDTO> GetByIdAsync(string id);
        Task<SpecializationWithServiceDTO> GetByIdWithServicesAsync(string id);
        Task<SpecializationDTO> CreateAsync(SpecializationForCreatedDTO model);
        Task<SpecializationDTO> ChangeStatusAsync(string id, bool status);
        Task<SpecializationDTO> UpdateAsync(string id, SpecializationForUpdateDTO model);
    }
}
