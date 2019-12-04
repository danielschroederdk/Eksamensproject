using FluentValidation;

namespace EksamensProject.Core.Entity
{
    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(review => review.RequestHeader).NotEmpty().WithMessage("Header must not be empty");
            RuleFor(review => review.RequestBody).NotEmpty().WithMessage("Body must not be empty");

        }
    }
}