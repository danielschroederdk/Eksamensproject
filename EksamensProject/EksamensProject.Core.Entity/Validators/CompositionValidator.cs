namespace EksamensProject.Core.Entity
{
    
    using FluentValidation;
    
    public class CompositionValidator : AbstractValidator<Composition>
    {
        public CompositionValidator()
        {
            RuleFor(composition => composition.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(composition => composition.Duration).NotEmpty().WithMessage("Duration is required");
            RuleFor(composition => composition.Tempo).NotEmpty().WithMessage("Tempo is required");
            RuleFor(composition => composition.Year).NotEmpty().WithMessage("Date is required");
            RuleFor(composition => composition.Style).NotEmpty().WithMessage("Style is required");
        }
        
    }
}