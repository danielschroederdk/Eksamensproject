namespace EksamensProject.Core.Entity
{
    using FluentValidation;
    
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(User => User.Name).NotEmpty()
                .Matches("^[a-zA-Z -]*$");
            RuleFor(User => User.Email).NotEmpty().NotNull();
        }
        
    }
}