﻿using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using FluentValidation;

public sealed class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Araç adı boş olamaz!")
            .MinimumLength(3).WithMessage("Araç adı en az 3 karakter olmalıdır.");

        RuleFor(p => p.Model)
            .NotEmpty().WithMessage("Araç modeli boş olamaz!")
            .MinimumLength(3).WithMessage("Araç modeli en az 3 karakter olmalıdır.");

        RuleFor(p => p.EnginePower)
            .NotEmpty().WithMessage("Araç motor gücü boş olamaz.")
            .GreaterThan(0).WithMessage("Araç motor gücü 0'dan büyük olmalıdır.");
    }
}
