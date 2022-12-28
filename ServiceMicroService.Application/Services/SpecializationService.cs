using AutoMapper;
using ServiceMicroService.Application.DTO.Specialization;
using ServiceMicroService.Application.Services.Abstractions;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure.Repository.Abstractions;

namespace ServiceMicroService.Application.Services;

public class SpecializationService : ISpecializationService
{
    private readonly IMapper _mapper;
    private readonly ISpecializationRepository _specializationRepository;

    public SpecializationService(ISpecializationRepository specializationRepository, IMapper mapper)
    {
        _specializationRepository = specializationRepository;
        _mapper = mapper;
    }

    public async Task<List<SpecializationDto>> GetAsync()
    {
        var specializations = await _specializationRepository.GetAllAsync();
        return _mapper.Map<List<SpecializationDto>>(specializations);
    }

    public async Task<SpecializationDto> GetByIdAsync(string id)
    {
        var specialization = await _specializationRepository.GetByIdAsync(id);
        return _mapper.Map<SpecializationDto>(specialization);
    }

    public async Task<SpecializationWithServiceDto> GetByIdWithServicesAsync(string id)
    {
        var specialization = await _specializationRepository.GetByIdAsync(id);
        return _mapper.Map<SpecializationWithServiceDto>(specialization);
    }

    public async Task<SpecializationDto> CreateAsync(SpecializationForCreatedDto model)
    {
        if (model == null)
            return null;

        var specialization = _mapper.Map<Specialization>(model);

        await _specializationRepository.InsertAsync(specialization);
        return _mapper.Map<SpecializationDto>(specialization);
    }

    public async Task<SpecializationDto> ChangeStatusAsync(string id, bool status)
    {
        var specialization = await _specializationRepository.GetByIdAsync(id);
        if (specialization == null)
            return null;

        specialization.IsActive = status;
        await _specializationRepository.UpdateAsync(specialization);
        return _mapper.Map<SpecializationDto>(specialization);
    }

    public async Task<SpecializationDto> UpdateAsync(string id, SpecializationForUpdateDto model)
    {
        var specialization = await _specializationRepository.GetByIdAsync(id);
        if (specialization == null)
            return null;

        _mapper.Map(model, specialization);
        await _specializationRepository.UpdateAsync(specialization);
        return _mapper.Map<SpecializationDto>(specialization);
    }
}