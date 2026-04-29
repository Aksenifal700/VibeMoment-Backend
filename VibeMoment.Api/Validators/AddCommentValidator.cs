using FluentValidation;
using VibeMoment.Api.Models.Requests.Photo;

namespace VibeMoment.Api.Validators;

public class AddCommentValidator : AbstractValidator<CommentRequest>
{
    public AddCommentValidator()
    {
        RuleFor(x => x.Content).NotEmpty().Length(1, 1488);
    }
}