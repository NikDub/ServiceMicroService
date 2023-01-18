using ServiceMicroService.Domain.Entities.Models;

namespace ServiceMicroService.Infrastructure.Repository.Abstractions;

public interface ICategoryRepository
{
    Task<Category> GetByNameAsync(string name);
}