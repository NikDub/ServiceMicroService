using ServiceMicroService.Application.DTO.Service;

namespace ServiceMicroService.Application.Services.Abstractions;

public interface IServiceService
{
    Task<List<ServiceDto>> GetAsync(bool onlyActive = false);
    Task<List<ServiceDto>> GetByCategoryAsync(string categoryId, bool isActive = false);
    Task<ServiceDto> GetByIdAsync(string id);
    Task<ServiceDto> CreateAsync(ServiceForCreatedDto model);
    Task<ServiceDto> ChangeStatusAsync(string id, bool status);
    Task<ServiceDto> UpdateAsync(string id, ServiceForUpdateDto model);
}