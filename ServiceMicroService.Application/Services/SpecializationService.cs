using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceMicroService.Application.DTO.Specialization;
using ServiceMicroService.Application.Services.Abstractions;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure;

namespace ServiceMicroService.Application.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public SpecializationService(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<SpecializationDTO>> GetAsync()
        {
            var specializations = await _db.Specializations.ToListAsync();
            return _mapper.Map<List<SpecializationDTO>>(specializations);
        }

        public async Task<SpecializationDTO> GetByIdAsync(string id)
        {
            var specialization = await _db.Specializations.FirstOrDefaultAsync(r => r.Id == id);
            return _mapper.Map<SpecializationDTO>(specialization);
        }

        public async Task<SpecializationWithServiceDTO> GetByIdWithServicesAsync(string id)
        {
            var specialization = await _db.Specializations.FirstOrDefaultAsync(r => r.Id == id);
            return _mapper.Map<SpecializationWithServiceDTO>(specialization);
        }

        public async Task<SpecializationDTO> CreateAsync(SpecializationForCreatedDTO model)
        {
            if (model == null)
                return null;

            var specialization = _mapper.Map<Specialization>(model);

            _db.Specializations.Add(specialization);
            await _db.SaveChangesAsync();
            return _mapper.Map<SpecializationDTO>(specialization);
        }

        public async Task<SpecializationDTO> ChangeStatusAsync(string id, bool status)
        {
            var specialization = await _db.Specializations.FirstOrDefaultAsync(r => r.Id == id);
            if (specialization == null)
                return null;

            specialization.IsActive = status;
            await _db.SaveChangesAsync();
            return _mapper.Map<SpecializationDTO>(specialization);
        }

        public async Task<SpecializationDTO> UpdateAsync(string id, SpecializationForUpdateDTO model)
        {
            var specialization = await _db.Specializations.FirstOrDefaultAsync(r => r.Id == id);
            if (specialization == null)
                return null;

            _mapper.Map(model, specialization);
            await _db.SaveChangesAsync();
            return _mapper.Map<SpecializationDTO>(specialization);
        }
    }
}
