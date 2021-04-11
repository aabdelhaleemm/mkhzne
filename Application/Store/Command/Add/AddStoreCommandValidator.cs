using FluentValidation;

namespace Application.Store.Command.Add
{
    public class AddStoreCommandValidator : AbstractValidator<AddStoreCommand>
    {
        public AddStoreCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please provide store name");
        }
    }
}