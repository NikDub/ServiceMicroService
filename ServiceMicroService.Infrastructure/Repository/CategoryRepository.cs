using Microsoft.EntityFrameworkCore;
using ServiceMicroService.Domain.Entities.Models;
using ServiceMicroService.Infrastructure.Repository.Abstractions;

namespace ServiceMicroService.Infrastructure.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext dB)
    {
        _db = dB;
    }

    public async Task<Category> GetByNameAsync(string name)
    {
        return await _db.Categories.AsNoTracking().FirstOrDefaultAsync(r => r.Name == name);
    }
}