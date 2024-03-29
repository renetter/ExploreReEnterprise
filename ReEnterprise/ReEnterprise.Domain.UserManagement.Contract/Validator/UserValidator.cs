﻿using FluentValidation;
using ReEnterprise.Domain.UserManagement.Contract.Entity;

namespace ReEnterprise.Domain.UserManagement.Contract.Validator
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.Email).EmailAddress().NotEmpty();
            RuleFor(c => c.PhoneNumber).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }
}
