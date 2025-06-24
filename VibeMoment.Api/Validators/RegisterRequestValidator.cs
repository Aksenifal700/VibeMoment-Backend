using FluentValidation;
using VibeMoment.Api.Models.Requests.Auth;

namespace VibeMoment.Api.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().Length(6, 10);
        RuleFor(x => x.Username).NotEmpty().Length(6, 10).Must(username => char.IsUpper(username[0]));
    }
} 