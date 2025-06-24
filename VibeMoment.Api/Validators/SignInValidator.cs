using FluentValidation;
using VibeMoment.Api.Models.Requests.Auth;

namespace VibeMoment.Api.Validators;

public class SignInValidator : AbstractValidator<SignInRequest>
{
    public SignInValidator()
    {
        RuleFor(x => x.UsernameOrEmail).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().Length(6, 10);
    }
}