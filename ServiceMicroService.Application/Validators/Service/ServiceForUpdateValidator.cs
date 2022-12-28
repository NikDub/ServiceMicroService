using FluentValidation;
using ServiceMicroService.Application.DTO.Service;

namespace ServiceMicroService.Application.Validators.Service;

public class ServiceForUpdateValidator : AbstractValidator<ServiceForUpdateDto>
{
    public ServiceForUpdateValidator()
    {
        RuleFor(x => x.ServiceName).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.IsActive).NotEmpty();
        RuleFor(x => x.CategoryName).NotEmpty();
        RuleFor(x => x.SpecializationName).NotEmpty();
    }
}