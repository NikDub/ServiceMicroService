using FluentValidation;
using ServiceMicroService.Application.DTO.Specialization;

namespace ServiceMicroService.Application.Validators.Specialization
{
    public class SpecializationForUpdateValidator : AbstractValidator<SpecializationForUpdateDTO>
    {
        public SpecializationForUpdateValidator()
        {
            RuleFor(r => r.SpecializationName).NotEmpty();
            RuleFor(r => r.IsActive).NotEmpty();
        }
    }
}
