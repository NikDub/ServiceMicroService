﻿using FluentValidation;
using ServiceMicroService.Application.Dto.Service;

namespace ServiceMicroService.Application.Validators.Service;

public class ServiceForUpdateValidator : AbstractValidator<ServiceForUpdateDto>
{
    public ServiceForUpdateValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.CategoryName).NotEmpty();
        RuleFor(x => x.SpecializationName).NotEmpty();
    }
}