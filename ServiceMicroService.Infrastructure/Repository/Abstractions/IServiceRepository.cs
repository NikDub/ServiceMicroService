using ServiceMicroService.Domain.Entities.Models;

namespace ServiceMicroService.Infrastructure.Repository.Abstractions;

public interface IServiceRepository
{
    Task<Dictionary<string, List<Service>>> GetGroupedByCategoryAsync();
    Task<IEnumerable<Service>> GetAllActiveOrNotAsync(bool isActive);
    Task<Service> GetByIdAsync(Guid id);
    Task InsertAsync(Service patient);
    Task UpdateAsync(Service patient);
}