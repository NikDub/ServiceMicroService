using FluentValidation;
using ServiceMicroService.Application.Dto.Specialization;

namespace ServiceMicroService.Application.Validators.Specialization;

public class SpecializationForUpdateValidator : AbstractValidator<SpecializationForUpdateDto>
{
    public SpecializationForUpdateValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
    }
}