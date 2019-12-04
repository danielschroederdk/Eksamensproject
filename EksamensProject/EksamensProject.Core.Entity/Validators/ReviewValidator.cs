using FluentValidation;

namespace EksamensProject.Core.Entity
{
    public class ReviewValidator : AbstractValidator<Review>
    {
        public ReviewValidator()
        {
            RuleFor(review => review.ReviewHeader).NotEmpty().WithMessage("Header must not be empty");
            RuleFor(review => review.ReviewBody).NotEmpty().WithMessage("Body must not be empty");

        }
    }
}