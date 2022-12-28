using ServiceMicroService.Domain.Entities.Models;

namespace ServiceMicroService.Infrastructure.Repository.Abstractions;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAllAsync();
    Task<IEnumerable<Service>> GetAllActiveOrNotAsync(bool isActive);
    Task<IEnumerable<Service>> GetByCategoryAndIsActiveAsync(bool isActive, string categoryId);
    Task<Service> GetByIdAsync(string id);
    Task InsertAsync(Service patient);
    Task UpdateAsync(Service patient);
    Task DeleteAsync(string id);
}