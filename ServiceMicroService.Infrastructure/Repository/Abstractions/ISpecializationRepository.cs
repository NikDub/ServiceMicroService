using ServiceMicroService.Domain.Entities.Models;

namespace ServiceMicroService.Infrastructure.Repository.Abstractions;

public interface ISpecializationRepository
{
    Task<IEnumerable<Specialization>> GetAllAsync();
    Task<Specialization> GetByIdAsync(Guid id);
    Task<Specialization> GetByNameAsync(string name);
    Task InsertAsync(Specialization patient);
    Task UpdateAsync(Specialization patient);
}