using FluentValidation;
using MusicShop.Presentation.Common.DTOs.Authentication;

namespace MusicShop.Application.Common.Behavior
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
       public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(1,20);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(1, 20);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(1, 40);
        }
    }
}
