using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using ServiceMicroService.Application.Dto.Specialization;
using ServiceMicroService.Application.Services.Abstractions;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure.Repository.Abstractions;
using SharedModel;

namespace ServiceMicroService.Application.Services;

public class SpecializationService : ISpecializationService
{
    private readonly IMapper _mapper;
    private readonly ISendEndpoint _endPoint;
    private readonly ISpecializationRepository _specializationRepository;

    public SpecializationService(ISpecializationRepository specializationRepository, IMapper mapper, IBus bus, IConfiguration configuration)
    {
        _specializationRepository = specializationRepository;
        _mapper = mapper;
        _endPoint = bus.GetSendEndpoint(new Uri(configuration.GetValue<string>("RabbitMQ:Uri") + configuration.GetValue<string>("RabbitMQ:QueueName:Producer:Specialization"))).GetAwaiter().GetResult();
    }

    public async Task<List<SpecializationDto>> GetAsync()
    {
        var specializations = await _specializationRepository.GetAllAsync();
        return _mapper.Map<List<SpecializationDto>>(specializations);
    }

    public async Task<SpecializationDto> GetByIdAsync(Guid id)
    {
        var specialization = await _specializationRepository.GetByIdAsync(id);
        return _mapper.Map<SpecializationDto>(specialization);
    }

    public async Task<SpecializationWithServiceDto> GetByIdWithServicesAsync(Guid id)
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

    public async Task<SpecializationDto> ChangeStatusAsync(Guid id, bool status)
    {
        var specialization = await _specializationRepository.GetByIdAsync(id);
        if (specialization == null)
            return null;

        specialization.IsActive = status;
        await _specializationRepository.UpdateAsync(specialization);
        return _mapper.Map<SpecializationDto>(specialization);
    }

    public async Task<SpecializationDto> UpdateAsync(Guid id, SpecializationForUpdateDto model)
    {
        var specialization = await _specializationRepository.GetByIdAsync(id);
        if (specialization == null)
            return null;

        _mapper.Map(model, specialization);
        await _specializationRepository.UpdateAsync(specialization);
        var message = new SpecializationMessage
        {
            Id = id,
            SpecializationName = model.Name
        };
        await _endPoint.Send(message);
        return _mapper.Map<SpecializationDto>(specialization);
    }
}