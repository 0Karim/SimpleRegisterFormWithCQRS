using CleanArch.Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IDbContext _context;
        private readonly IDateTime _dateTime;

        public CreateUserCommandValidator(IDbContext context , IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First Name Name is required.")
                .MaximumLength(20).WithMessage("First Name Length Not Exceed 20")
                .MustAsync(BeValidString).WithMessage("First Name is not valid");

            RuleFor(c => c.MiddleName)
                .MaximumLength(40).WithMessage("Middle Name Length Not Exceed 40")
                .MustAsync(BeValidString).WithMessage("Middle Name is not valid");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last Name required")
                .MaximumLength(20).WithMessage("Last Name Length Not Exceed 40")
                .MustAsync(BeValidString).WithMessage("Last Name is not valid");

            RuleFor(c => c.BirthDate)
                .NotEmpty().WithMessage("Birthdate required")
                .MustAsync(BeValidAge).WithMessage("Minimum Age is 20");

            RuleFor(c => c.MobileNumber)
                .NotEmpty().WithMessage("Birthdate required")
                .MustAsync(BeValidMobile).WithMessage("Mobile Number is not valid");

            RuleFor(c => c.Email).NotEmpty().WithMessage("Email required")
                .EmailAddress().WithMessage("A valid email is required");

            RuleForEach(c => c.AddressInfos).SetValidator(new AddressInfoValidator());
        }

        public async Task<bool> BeValidString(string value, CancellationToken cancellationToken)
        {
            var regex = new Regex("^[a-zA-Z\u0621-\u064A ]+$");
            return regex.IsMatch(value);
        }

        public async Task<bool> BeValidMobile(string value, CancellationToken cancellationToken)
        {
            var regex = new Regex("\\^(\\+02)[0-9]{10}$");
            return regex.IsMatch(value);
        }

        public async Task<bool> BeValidAge(DateTime? value, CancellationToken cancellationToken)
        {
            DateTime today = _dateTime.Now;

            if (!value.HasValue)
                return false;

            int age = today.Year - value.Value.Year;

            return age >= 20 ? true : false;
        }


    }
}
