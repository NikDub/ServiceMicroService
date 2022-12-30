using ServiceMicroService.Application.Dto.Specialization;

namespace ServiceMicroService.Application.Services.Abstractions;

public interface ISpecializationService
{
    Task<List<SpecializationDto>> GetAsync();
    Task<SpecializationDto> GetByIdAsync(string id);
    Task<SpecializationWithServiceDto> GetByIdWithServicesAsync(string id);
    Task<SpecializationDto> CreateAsync(SpecializationForCreatedDto model);
    Task<SpecializationDto> ChangeStatusAsync(string id, bool status);
    Task<SpecializationDto> UpdateAsync(string id, SpecializationForUpdateDto model);
}