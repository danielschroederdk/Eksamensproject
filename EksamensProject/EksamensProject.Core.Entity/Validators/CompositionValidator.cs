namespace EksamensProject.Core.Entity
{
    using FluentValidation;
    public class CompositionValidator : AbstractValidator<Composition>
    {
        public CompositionValidator()
        {
            RuleFor(composition => composition.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(composition => composition.Duration).NotEmpty().WithMessage("Duration is required");
            RuleFor(composition => composition.Duration).NotEqual(0).WithMessage("Duration cannot be 0");
            RuleFor(composition => composition.Duration).GreaterThan(0)
                .WithMessage("Duration must be a positive number");
            RuleFor(composition => composition.Year).NotEmpty().WithMessage("Date is required");
        }
        
    }
}