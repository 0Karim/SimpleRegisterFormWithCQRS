using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Application.Users.Commands.CreateUser
{
    public class AddressInfoValidator : AbstractValidator<AddressInfoCommand>
    {
        public AddressInfoValidator()
        {
            RuleFor(c => c.GovId)
                .NotEmpty().WithMessage("Governrate is required.");

            RuleFor(c => c.CityId)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("Street required")
                .MaximumLength(200).WithMessage("Street Length Not Exceed 200");

            RuleFor(c => c.BuildingNumber)
                .NotEmpty().WithMessage("Build Number required")
                .MaximumLength(200).WithMessage("Street Length Not Exceed 200");

            RuleFor(c => c.FlatNumber)
                .NotEmpty().WithMessage("Flat Number required");
        }
    }
}
