using FluentValidation;

namespace Application.User.Command.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotNull()
                .NotEmpty()
                .WithMessage("Email address is required!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .WithMessage("Password is required");
        }
    }
}