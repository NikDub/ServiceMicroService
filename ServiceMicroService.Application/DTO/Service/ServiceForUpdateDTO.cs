namespace ServiceMicroService.Application.Dto.Service;

public class ServiceForUpdateDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }

    public string CategoryName { get; set; }
    public string SpecializationName { get; set; }
}