namespace EksamensProject.Core.Entity
{
    using FluentValidation;
    
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).NotEmpty()
                .Matches("^[a-zA-Z -]*$");
            RuleFor(user => user.Email).NotEmpty().NotNull();
        }
        
        
    }
}
