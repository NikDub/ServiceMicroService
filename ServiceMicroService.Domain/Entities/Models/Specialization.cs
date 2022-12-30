using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceMicroService.Domain.Entities.Models;

public class Specialization
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string Name { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Service> Services { get; set; }
}