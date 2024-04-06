﻿using FluentValidation;
using MusicShop.Presentation.Common.DTOs.Authentication;

namespace MusicShop.Application.Common.Behavior
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
       public RegisterRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .Length(1,20);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(1, 40);
        }
    }
}
