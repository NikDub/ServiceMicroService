using FluentValidation;
using ServiceMicroService.Application.Dto.Specialization;

namespace ServiceMicroService.Application.Validators.Specialization;

public class SpecializationForCreatedValidator : AbstractValidator<SpecializationForCreatedDto>
{
    public SpecializationForCreatedValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
    }
}