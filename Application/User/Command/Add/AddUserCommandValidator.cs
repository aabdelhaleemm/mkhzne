using FluentValidation;

namespace Application.User.Command.Add
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotNull()
                .WithMessage("Please enter valid Email!");
            RuleFor(x => x.Password)
                .NotNull();
            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty();
        }
    }
}