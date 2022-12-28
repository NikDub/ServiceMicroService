using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceMicroService.Domain.Entities.Models;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string CategoryName { get; set; }
    public int TimeSlotSize { get; set; }

    public virtual ICollection<Service> Services { get; set; }
}