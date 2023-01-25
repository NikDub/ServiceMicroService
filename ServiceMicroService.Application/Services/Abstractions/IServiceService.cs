using ServiceMicroService.Application.Dto.Service;

namespace ServiceMicroService.Application.Services.Abstractions;

public interface IServiceService
{
    Task<List<ServiceDto>> GetActiveAsync();
    Task<ServicesListsDto> GetAllDividedByCategoryAsync();
    Task<ServiceDto> GetByIdAsync(Guid id);
    Task<ServiceDto> CreateAsync(ServiceForCreatedDto model);
    Task<ServiceDto> ChangeStatusAsync(Guid id, bool status);
    Task<ServiceDto> UpdateAsync(Guid id, ServiceForUpdateDto model);
}