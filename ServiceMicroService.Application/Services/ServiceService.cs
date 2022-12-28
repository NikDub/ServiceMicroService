using AutoMapper;
using ServiceMicroService.Application.DTO.Service;
using ServiceMicroService.Application.Services.Abstractions;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure.Repository.Abstractions;

namespace ServiceMicroService.Application.Services;

public class ServiceService : IServiceService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IServiceRepository _serviceRepository;
    private readonly ISpecializationRepository _specializationRepository;

    public ServiceService(IServiceRepository serviceRepository, ISpecializationRepository specializationRepository,
        ICategoryRepository categoryRepository, IMapper mapper)
    {
        _serviceRepository = serviceRepository;
        _specializationRepository = specializationRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<ServiceDto>> GetAsync(bool isActive = false)
    {
        IEnumerable<Service> services;
        if (isActive)
            services = await _serviceRepository.GetAllActiveOrNotAsync(isActive);
        else
            services = await _serviceRepository.GetAllAsync();

        return _mapper.Map<List<ServiceDto>>(services);
    }

    public async Task<List<ServiceDto>> GetByCategoryAsync(string categoryId, bool isActive = false)
    {
        var services = await _serviceRepository.GetByCategoryAndIsActiveAsync(isActive, categoryId);
        return _mapper.Map<List<ServiceDto>>(services);
    }

    public async Task<ServiceDto> GetByIdAsync(string Id)
    {
        var service = await _serviceRepository.GetByIdAsync(Id);
        return _mapper.Map<ServiceDto>(service);
    }

    public async Task<ServiceDto> CreateAsync(ServiceForCreatedDto model)
    {
        if (model == null)
            return null;

        var category = await _categoryRepository.GetByNameAsync(model.CategoryName);
        var specialization = await _specializationRepository.GetByNameAsync(model.SpecializationName);
        if (category == null || specialization == null)
            return null;

        var service = _mapper.Map<Service>(model);
        service.CategoryId = category.Id;
        service.SpecializationId = specialization.Id;

        await _serviceRepository.InsertAsync(service);
        return _mapper.Map<ServiceDto>(service);
    }

    public async Task<ServiceDto> ChangeStatusAsync(string id, bool status)
    {
        var service = await _serviceRepository.GetByIdAsync(id);
        if (service == null)
            return null;

        service.IsActive = status;
        await _serviceRepository.UpdateAsync(service);
        return _mapper.Map<ServiceDto>(service);
    }

    public async Task<ServiceDto> UpdateAsync(string id, ServiceForUpdateDto model)
    {
        var service = await _serviceRepository.GetByIdAsync(id);
        if (service == null)
            return null;

        var category = await _categoryRepository.GetByNameAsync(model.CategoryName);
        var specialization = await _specializationRepository.GetByNameAsync(model.SpecializationName);
        if (category == null || specialization == null)
            return null;

        _mapper.Map(model, service);
        service.CategoryId = category.Id;
        service.SpecializationId = specialization.Id;
        await _serviceRepository.UpdateAsync(service);
        return _mapper.Map<ServiceDto>(service);
    }

    public async Task<List<ServiceDto>> GetAsync()
    {
        var services = await _serviceRepository.GetAllAsync();
        return _mapper.Map<List<ServiceDto>>(services);
    }
}