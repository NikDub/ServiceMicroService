using Microsoft.EntityFrameworkCore;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure.Repository.Abstractions;

namespace ServiceMicroService.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _db;
        public CategoryRepository(ApplicationDBContext dB)
        {
            _db = dB;
        }

        public async Task DeleteAsync(string id)
        {
            var category = await _db.Categories.FindAsync(id);
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await _db.Categories.ToListAsync();

        public async Task<Category> GetByIdAsync(string id) =>
            await _db.Categories.FindAsync(id);

        public async Task<Category> GetByNameAsync(string name)=>
             await _db.Categories.FindAsync(name);

        public async Task InsertAsync(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _db.Entry(category).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
