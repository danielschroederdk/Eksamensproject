using FluentValidation;

namespace EksamensProject.Core.Entity
{
    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(request => request.User).NotNull().WithMessage("User cannot be null").NotEmpty().WithMessage("User cannot be empty");
            RuleFor(request => request.RequestHeader).NotEmpty().WithMessage("Header must not be empty");
            RuleFor(request => request.RequestBody).NotEmpty().WithMessage("Body must not be empty");

        }
    }
}