using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceMicroService.Application.DTO.Service;
using ServiceMicroService.Application.Services.Abstractions;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure;

namespace ServiceMicroService.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public ServiceService(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<ServiceDTO>> GetAsync()
        {
            var services = await _db.Services.ToListAsync();
            return _mapper.Map<List<ServiceDTO>>(services);
        }

        public async Task<List<ServiceDTO>> GetAsync(bool OnlyActive = false)
        {
            List<Service> services;
            if (OnlyActive)
                services = await _db.Services.Where(r => r.IsActive == OnlyActive).ToListAsync();
            else
                services = await _db.Services.ToListAsync();

            return _mapper.Map<List<ServiceDTO>>(services);
        }

        public async Task<List<ServiceDTO>> GetByCategoryAsync(string CategoryId, bool isActive = false)
        {
            var services = await _db.Services.Where(r => r.IsActive == isActive && r.CategoryId == CategoryId).ToListAsync();
            return _mapper.Map<List<ServiceDTO>>(services);
        }

        public async Task<ServiceDTO> GetByIDAsync(string Id)
        {
            var service = await _db.Services.FirstOrDefaultAsync(r => r.Id == Id);
            return _mapper.Map<ServiceDTO>(service);
        }

        public async Task<ServiceDTO> CreateAsync(ServiceForCreatedDTO model)
        {
            if (model == null)
                return null;

            var category = await _db.Categories.FirstOrDefaultAsync(r => r.CategoryName == model.CategoryName);
            var specialization = await _db.Specializations.FirstOrDefaultAsync(r => r.SpecializationName == model.SpecializationName);
            if (category == null || specialization == null)
                return null;

            var service = _mapper.Map<Service>(model);
            service.CategoryId = category.Id;
            service.SpecializationId = specialization.Id;

            _db.Services.Add(service);
            await _db.SaveChangesAsync();
            return _mapper.Map<ServiceDTO>(service);
        }

        public async Task<ServiceDTO> ChangeStatusAsync(string id, bool status)
        {
            var service = await _db.Services.FirstOrDefaultAsync(r => r.Id == id);
            if (service == null)
                return null;

            service.IsActive = status;
            await _db.SaveChangesAsync();
            return _mapper.Map<ServiceDTO>(service);
        }

        public async Task<ServiceDTO> UpdateAsync(string id, ServiceForUpdateDTO model)
        {
            var service = await _db.Services.FirstOrDefaultAsync(r => r.Id == id);
            if (service == null)
                return null;

            var category = await _db.Categories.FirstOrDefaultAsync(r => r.CategoryName == model.CategoryName);
            var specialization = await _db.Specializations.FirstOrDefaultAsync(r => r.SpecializationName == model.SpecializationName);
            if (category == null || specialization == null)
                return null;

            _mapper.Map(model, service);
            service.CategoryId = category.Id;
            service.SpecializationId = specialization.Id;
            await _db.SaveChangesAsync();
            return _mapper.Map<ServiceDTO>(service);
        }
    }
}
