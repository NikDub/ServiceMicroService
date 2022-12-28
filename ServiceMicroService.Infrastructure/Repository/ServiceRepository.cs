using Microsoft.EntityFrameworkCore;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure.Repository.Abstractions;

namespace ServiceMicroService.Infrastructure.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDBContext _db;
        public ServiceRepository(ApplicationDBContext dB)
        {
            _db = dB;
        }

        public async Task DeleteAsync(string id)
        {
            var service = await _db.Services.FindAsync(id);
            _db.Services.Remove(service);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> GetAllActiveOrNotAsync(bool isActive) =>
            await _db.Services.Where(r => r.IsActive == isActive).ToListAsync();

        public async Task<IEnumerable<Service>> GetAllAsync() =>
            await _db.Services.ToListAsync();

        public async Task<IEnumerable<Service>> GetByCategoryAndIsActiveAsync(bool isActive, string CategoryId) =>
             await _db.Services.Where(r => r.IsActive == isActive && r.CategoryId == CategoryId).ToListAsync();

        public async Task<Service> GetByIdAsync(string id) =>
            await _db.Services.FindAsync(id);

        public async Task InsertAsync(Service service)
        {
            await _db.Services.AddAsync(service);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Service service)
        {
            _db.Entry(service).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
