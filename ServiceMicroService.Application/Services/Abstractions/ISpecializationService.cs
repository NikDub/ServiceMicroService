using ServiceMicroService.Application.Dto.Specialization;

namespace ServiceMicroService.Application.Services.Abstractions;

public interface ISpecializationService
{
    Task<List<SpecializationDto>> GetAsync();
    Task<SpecializationDto> GetByIdAsync(Guid id);
    Task<SpecializationWithServiceDto> GetByIdWithServicesAsync(Guid id);
    Task<SpecializationDto> CreateAsync(SpecializationForCreatedDto model);
    Task<SpecializationDto> ChangeStatusAsync(Guid id, bool status);
    Task<SpecializationDto> UpdateAsync(Guid id, SpecializationForUpdateDto model);
}