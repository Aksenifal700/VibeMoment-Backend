using FluentValidation;
using VibeMoment.Api.Models.Requests.Photo;

namespace VibeMoment.Api.Validators;

public class UploadPhotoValidator : AbstractValidator<UploadPhotoRequest>
{
    public UploadPhotoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().Length(1, 256);
        RuleFor(x => x.Photo).NotNull();
    }
}