using FluentValidation;
using ServiceMicroService.Application.DTO.Specialization;

namespace ServiceMicroService.Application.Validators.Specialization;

public class SpecializationForCreatedValidator : AbstractValidator<SpecializationForCreatedDto>
{
    public SpecializationForCreatedValidator()
    {
        RuleFor(r => r.SpecializationName).NotEmpty();
        RuleFor(r => r.IsActive).NotEmpty();
    }
}