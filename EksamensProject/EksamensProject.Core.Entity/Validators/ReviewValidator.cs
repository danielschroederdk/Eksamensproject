using FluentValidation;

namespace EksamensProject.Core.Entity
{
    public class ReviewValidator : AbstractValidator<Testimonial>
    {
        public ReviewValidator()
        {
            RuleFor(review => review.User).NotEmpty().WithMessage("User cannot be empty");
            RuleFor(review => review.TestimonialHeader).NotEmpty().WithMessage("Header must not be empty");
            RuleFor(review => review.TestimonialBody).NotEmpty().WithMessage("Body must not be empty");

        }
    }
}