namespace EksamensProject.Core.Entity
{
    using FluentValidation;
    
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(User => User.Name).NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
        
    }
}