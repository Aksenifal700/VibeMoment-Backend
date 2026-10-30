using FluentValidation;
using VibeMoment.BusinessLogic.DTOs.Auth;

namespace VibeMoment.Api.Validators;

public class UpdateUserProfileDtoValidator : AbstractValidator<UpdateUserProfileDto>
{
    public UpdateUserProfileDtoValidator()
    {
        RuleFor(x => x.FirstName).MaximumLength(50);
        RuleFor(x => x.LastName).MaximumLength(50);
        RuleFor(x => x.Bio).MaximumLength(400);
        RuleFor(x => x.Country).MaximumLength(25);
        RuleFor(x => x.City).MaximumLength(40);
        RuleFor(x => x.Gender).IsInEnum();
    }
}