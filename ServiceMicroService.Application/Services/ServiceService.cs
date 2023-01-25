using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using ServiceMicroService.Application.Dto.Service;
using ServiceMicroService.Application.Services.Abstractions;
using ServiceMicroService.Domain.Entities.Enums;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure.Repository.Abstractions;
using SharedModel;

namespace ServiceMicroService.Application.Services;

public class ServiceService : IServiceService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ISendEndpoint _endPoint;
    private readonly IServiceRepository _serviceRepository;
    private readonly ISpecializationRepository _specializationRepository;

    public ServiceService(IServiceRepository serviceRepository, ISpecializationRepository specializationRepository,
        ICategoryRepository categoryRepository, IMapper mapper, IBus bus, IConfiguration configuration)
    {
        _serviceRepository = serviceRepository;
        _specializationRepository = specializationRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _endPoint = bus.GetSendEndpoint(new Uri(configuration.GetValue<string>("RabbitMQ:Uri") + configuration.GetValue<string>("RabbitMQ:QueueName:Producer:Service"))).GetAwaiter().GetResult();
    }

    public async Task<List<ServiceDto>> GetActiveAsync()
    {
        var services = await _serviceRepository.GetAllActiveOrNotAsync(true);
        return _mapper.Map<List<ServiceDto>>(services);
    }

    public async Task<ServicesListsDto> GetAllDividedByCategoryAsync()
    {
        var services = await _serviceRepository.GetGroupedByCategoryAsync();
        var serviceList = new ServicesListsDto();

        if (services.ContainsKey(nameof(CategoryEnum.Analyzes)))
            serviceList.Analyzes = _mapper.Map<List<ServiceDto>>(services[nameof(CategoryEnum.Analyzes)].ToList());

        if (services.ContainsKey(nameof(CategoryEnum.Diagnostics)))
            serviceList.Analyzes = _mapper.Map<List<ServiceDto>>(services[nameof(CategoryEnum.Diagnostics)].ToList());

        if (services.ContainsKey(nameof(CategoryEnum.Consultations)))
            serviceList.Analyzes = _mapper.Map<List<ServiceDto>>(services[nameof(CategoryEnum.Consultations)].ToList());

        return serviceList;
    }

    public async Task<ServiceDto> GetByIdAsync(Guid id)
    {
        var service = await _serviceRepository.GetByIdAsync(id);
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

    public async Task<ServiceDto> ChangeStatusAsync(Guid id, bool status)
    {
        var service = await _serviceRepository.GetByIdAsync(id);
        if (service == null)
            return null;

        service.IsActive = status;
        await _serviceRepository.UpdateAsync(service);
        return _mapper.Map<ServiceDto>(service);
    }

    public async Task<ServiceDto> UpdateAsync(Guid id, ServiceForUpdateDto model)
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
        var message = new ServiceMessage
        {
            Id = id,
            ServiceName = model.Name
        };
        await _endPoint.Send(message);
        return _mapper.Map<ServiceDto>(service);
    }
}