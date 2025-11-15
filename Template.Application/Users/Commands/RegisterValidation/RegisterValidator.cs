using FluentValidation;

namespace Template.Application.Users.Commands.RegisterValidation
{
    public class RegisterValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterValidator()
        {
            When(x => x.Role == "user", () =>
            {
                RuleFor(x => x.GovernmentalEntityId)
                    .Null()
                    .WithMessage(" Govermental entity must be null when role is 'user'.");
            });

            // Id MUST NOT be null when Role is NOT "user"
            When(x => x.Role != "user", () =>
            {
                RuleFor(x => x.GovernmentalEntityId)
                    .NotNull()
                    .WithMessage("Governmental entity is required when role is not 'user'.");
            });

        }
    }
}

