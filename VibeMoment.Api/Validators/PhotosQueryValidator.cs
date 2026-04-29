using FluentValidation;
using VibeMoment.Api.Models.Requests.Photo;

namespace VibeMoment.Api.Validators;

public class PhotosQueryValidator : AbstractValidator<PhotosQueryRequest>
{
    public PhotosQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 50);
    }
}