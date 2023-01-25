using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ServiceMicroService.Domain.Entities.Models;

public class Service
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }

    [Precision(18, 2)] public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public Guid SpecializationId { get; set; }
    public virtual Specialization Specialization { get; set; }
}