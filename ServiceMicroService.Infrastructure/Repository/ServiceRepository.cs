using Microsoft.EntityFrameworkCore;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure.Repository.Abstractions;

namespace ServiceMicroService.Infrastructure.Repository;

public class ServiceRepository : IServiceRepository
{
    private readonly ApplicationDbContext _db;

    public ServiceRepository(ApplicationDbContext dB)
    {
        _db = dB;
    }

    public async Task<IEnumerable<Service>> GetAllActiveOrNotAsync(bool isActive)
    {
        return await _db.Services.Where(r => r.IsActive == isActive)
            .Include(e=>e.Category)
            .Include(e=>e.Specialization)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Dictionary<string, List<Service>>> GetGroupedByCategoryAsync()
    {
        return await _db.Services.GroupBy(r => r.Category.Name)
            .Select(r => new { r.Key, Services = r.ToList() })
            .ToDictionaryAsync(r => r.Key, t => t.Services);
    }

    public async Task<Service> GetByIdAsync(Guid id)
    {
        return await _db.Services.FindAsync(id);
    }

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