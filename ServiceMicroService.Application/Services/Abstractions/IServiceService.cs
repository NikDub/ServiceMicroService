using ServiceMicroService.Application.DTO.Service;

namespace ServiceMicroService.Application.Services.Abstractions
{
    public interface IServiceService
    {
        Task<List<ServiceDTO>> GetAsync(bool OnlyActive = false);
        Task<List<ServiceDTO>> GetByCategoryAsync(string CategoryId, bool isActive = false);
        Task<ServiceDTO> GetByIDAsync(string Id);
        Task<ServiceDTO> CreateAsync(ServiceForCreatedDTO model);
        Task<ServiceDTO> ChangeStatusAsync(string id, bool status);
        Task<ServiceDTO> UpdateAsync(string id, ServiceForUpdateDTO model);
    }
}
