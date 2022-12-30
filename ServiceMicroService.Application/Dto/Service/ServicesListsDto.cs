namespace ServiceMicroService.Application.Dto.Service;

public class ServicesListsDto
{
    public IEnumerable<ServiceDto> Consultations { get; set; }
    public IEnumerable<ServiceDto> Diagnostics { get; set; }
    public IEnumerable<ServiceDto> Analyzes { get; set; }
}