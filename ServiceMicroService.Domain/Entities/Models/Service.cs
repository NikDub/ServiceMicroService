using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceMicroService.Domain.Entities.Models;

public class Service
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string ServiceName { get; set; }
    public float Price { get; set; }
    public bool IsActive { get; set; }

    public string CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public string SpecializationId { get; set; }
    public virtual Specialization Specialization { get; set; }
}