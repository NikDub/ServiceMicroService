using ServiceMicroService.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMicroService.Infrastructure.Repository.Abstractions
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<IEnumerable<Service>> GetAllActiveOrNotAsync(bool isActive);
        Task<IEnumerable<Service>> GetByCategoryAndIsActiveAsync(bool isActive, string CategoryId);
        Task<Service> GetByIdAsync(string id);
        Task InsertAsync(Service patient);
        Task UpdateAsync(Service patient);
        Task DeleteAsync(string id);
    }
}
