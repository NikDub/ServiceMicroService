using ServiceMicroService.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMicroService.Infrastructure.Repository.Abstractions
{
    public interface ISpecializationRepository
    {
        Task<IEnumerable<Specialization>> GetAllAsync();
        Task<Specialization> GetByIdAsync(string id);
        Task<Specialization> GetByNameAsync(string name);
        Task InsertAsync(Specialization patient);
        Task UpdateAsync(Specialization patient);
        Task DeleteAsync(string id);
    }
}
