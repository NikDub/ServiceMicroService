using ServiceMicroService.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMicroService.Infrastructure.Repository.Abstractions
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(string id);
        Task<Category> GetByNameAsync(string name);
        Task InsertAsync(Category patient);
        Task UpdateAsync(Category patient);
        Task DeleteAsync(string id);
    }
}
