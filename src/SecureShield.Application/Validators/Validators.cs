using FluentValidation;

namespace SecureShield.Application.Validators;

public class CareerApplicationValidator : AbstractValidator<DTOs.CareerApplicationDto>
{
    public CareerApplicationValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).WithMessage("Name must be at least 2 characters");
        RuleFor(x => x.Phone).NotEmpty().Matches(@"^(\+?880|0)?1[3-9]\d{8}$").WithMessage("Enter a valid Bangladeshi phone number");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid email address");
        RuleFor(x => x.Address).NotEmpty().MinimumLength(5).WithMessage("Address is required");
        RuleFor(x => x.Education).NotEmpty().WithMessage("Education is required");
        RuleFor(x => x.Experience).NotEmpty().WithMessage("Experience is required");
        RuleFor(x => x.Position).NotEmpty().WithMessage("Position is required");
    }
}

public class ContactMessageValidator : AbstractValidator<DTOs.ContactMessageDto>
{
    public ContactMessageValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).WithMessage("Name is required");
        RuleFor(x => x.Phone).NotEmpty().Matches(@"^(\+?880|0)?1[3-9]\d{8}$").WithMessage("Enter a valid phone number");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid email");
        RuleFor(x => x.Subject).NotEmpty().MinimumLength(2).WithMessage("Subject is required");
        RuleFor(x => x.Message).NotEmpty().MinimumLength(10).WithMessage("Message must be at least 10 characters");
    }
}

public class LoginValidator : AbstractValidator<DTOs.LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid email");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
}
