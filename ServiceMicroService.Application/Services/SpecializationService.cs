using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceMicroService.Application.DTO.Specialization;
using ServiceMicroService.Application.Services.Abstractions;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure.Repository.Abstractions;

namespace ServiceMicroService.Application.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IMapper _mapper;

        public SpecializationService(ISpecializationRepository specializationRepository, IMapper mapper)
        {
            this._specializationRepository = specializationRepository;
            _mapper = mapper;
        }

        public async Task<List<SpecializationDTO>> GetAsync()
        {
            var specializations = await _specializationRepository.GetAllAsync();
            return _mapper.Map<List<SpecializationDTO>>(specializations);
        }

        public async Task<SpecializationDTO> GetByIdAsync(string id)
        {
            var specialization = await _specializationRepository.GetByIdAsync(id);
            return _mapper.Map<SpecializationDTO>(specialization);
        }

        public async Task<SpecializationWithServiceDTO> GetByIdWithServicesAsync(string id)
        {
            var specialization = await _specializationRepository.GetByIdAsync(id);
            return _mapper.Map<SpecializationWithServiceDTO>(specialization);
        }

        public async Task<SpecializationDTO> CreateAsync(SpecializationForCreatedDTO model)
        {
            if (model == null)
                return null;

            var specialization = _mapper.Map<Specialization>(model);

            await _specializationRepository.InsertAsync(specialization);
            return _mapper.Map<SpecializationDTO>(specialization);
        }

        public async Task<SpecializationDTO> ChangeStatusAsync(string id, bool status)
        {
            var specialization = await _specializationRepository.GetByIdAsync(id);
            if (specialization == null)
                return null;

            specialization.IsActive = status;
            await _specializationRepository.UpdateAsync(specialization);
            return _mapper.Map<SpecializationDTO>(specialization);
        }

        public async Task<SpecializationDTO> UpdateAsync(string id, SpecializationForUpdateDTO model)
        {
            var specialization = await _specializationRepository.GetByIdAsync(id);
            if (specialization == null)
                return null;

            _mapper.Map(model, specialization);
            await _specializationRepository.UpdateAsync(specialization);
            return _mapper.Map<SpecializationDTO>(specialization);
        }
    }
}
