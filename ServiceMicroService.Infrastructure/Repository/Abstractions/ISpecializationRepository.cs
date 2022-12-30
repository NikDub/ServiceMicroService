using ServiceMicroService.Domain.Entities.Models;

namespace ServiceMicroService.Infrastructure.Repository.Abstractions;

public interface ISpecializationRepository
{
    Task<IEnumerable<Specialization>> GetAllAsync();
    Task<Specialization> GetByIdAsync(string id);
    Task<Specialization> GetByNameAsync(string name);
    Task InsertAsync(Specialization patient);
    Task UpdateAsync(Specialization patient);
    Task DeleteAsync(string id);
}