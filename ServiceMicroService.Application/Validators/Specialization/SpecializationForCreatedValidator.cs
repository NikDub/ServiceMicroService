using FluentValidation;
using ServiceMicroService.Application.DTO.Specialization;

namespace ServiceMicroService.Application.Validators.Specialization
{
    public class SpecializationForCreatedValidator : AbstractValidator<SpecializationForCreatedDTO>
    {
        public SpecializationForCreatedValidator()
        {
            RuleFor(r => r.SpecializationName).NotEmpty();
            RuleFor(r => r.IsActive).NotEmpty();
        }
    }
}
