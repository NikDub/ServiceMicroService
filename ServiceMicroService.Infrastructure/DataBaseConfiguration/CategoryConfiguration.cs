using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceMicroService.Domain.Entities.Enums;
using ServiceMicroService.Domain.Entities.Models;

namespace ServiceMicroService.Infrastructure.DataBaseConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        AddInitialData(builder);
    }

    private void AddInitialData(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category
            {
                Id = Guid.Parse("4050f457-6642-4bd6-b113-d2a9ad92af56"),
                TimeSlotSize = 10,
                Name = nameof(CategoryEnum.Consultations)
            },
            new Category
            {
                Id = Guid.Parse("c47124fa-96d5-4534-bc1e-2dc81bdfdc6b"),
                TimeSlotSize = 10,
                Name = nameof(CategoryEnum.Diagnostics)
            },
            new Category
            {
                Id = Guid.Parse("8a8abd81-222a-4c27-a414-43dff95e6549"),
                TimeSlotSize = 10,
                Name = nameof(CategoryEnum.Analyzes)
            });
    }
}