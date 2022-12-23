using FluentValidation;
using ServiceMicroService.Application.DTO.Service;

namespace ServiceMicroService.Application.Validators.Service
{
    public class ServiceForCreatedValidator : AbstractValidator<ServiceForCreatedDTO>
    {
        public ServiceForCreatedValidator()
        {
            RuleFor(x => x.ServiceName).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.IsActive).NotEmpty();
            RuleFor(x => x.CategoryName).NotEmpty();
            RuleFor(x => x.SpecializationName).NotEmpty();
        }
    }
}
