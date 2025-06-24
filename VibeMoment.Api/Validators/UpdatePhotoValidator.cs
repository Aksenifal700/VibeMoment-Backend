using FluentValidation;
using VibeMoment.Api.Models.Requests.Photo;

namespace VibeMoment.Api.Validators;

public class UpdatePhotoValidator : AbstractValidator<UpdatePhotoRequest>
{
    public UpdatePhotoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().Length(1, 256);
    }
}