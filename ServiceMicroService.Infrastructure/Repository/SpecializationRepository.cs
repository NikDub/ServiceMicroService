using Microsoft.EntityFrameworkCore;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure.Repository.Abstractions;

namespace ServiceMicroService.Infrastructure.Repository;

public class SpecializationRepository : ISpecializationRepository
{
    private readonly ApplicationDbContext _db;

    public SpecializationRepository(ApplicationDbContext dB)
    {
        _db = dB;
    }

    public async Task DeleteAsync(string id)
    {
        var specialization = await _db.Specializations.FindAsync(id);
        _db.Specializations.Remove(specialization);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Specialization>> GetAllAsync()
    {
        return await _db.Specializations.AsNoTracking().ToListAsync();
    }

    public async Task<Specialization> GetByNameAsync(string name)
    {
        return await _db.Specializations.AsNoTracking().FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<Specialization> GetByIdAsync(string id)
    {
        return await _db.Specializations.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task InsertAsync(Specialization patient)
    {
        await _db.Specializations.AddAsync(patient);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Specialization patient)
    {
        _db.Entry(patient).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }
}