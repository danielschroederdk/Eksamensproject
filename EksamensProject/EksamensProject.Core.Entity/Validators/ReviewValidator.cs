using FluentValidation;

namespace EksamensProject.Core.Entity
{
    public class ReviewValidator : AbstractValidator<Testimonial>
    {
        public ReviewValidator()
        {
            RuleFor(review => review.User).NotNull().WithMessage("User cannot be null").NotEmpty().WithMessage("User cannot be empty");
            RuleFor(review => review.TestimonialHeader).NotEmpty().WithMessage("Header must not be empty");
            RuleFor(review => review.TestimonialBody).NotEmpty().WithMessage("Body must not be empty");

        }
    }
}