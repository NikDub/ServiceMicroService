using ServiceMicroService.Application.Dto.Service;

namespace ServiceMicroService.Application.Services.Abstractions;

public interface IServiceService
{
    Task<List<ServiceDto>> GetActiveAsync();
    Task<ServicesListsDto> GetAllDividedByCategoryAsync();
    Task<ServiceDto> GetByIdAsync(string id);
    Task<ServiceDto> CreateAsync(ServiceForCreatedDto model);
    Task<ServiceDto> ChangeStatusAsync(string id, bool status);
    Task<ServiceDto> UpdateAsync(string id, ServiceForUpdateDto model);
}