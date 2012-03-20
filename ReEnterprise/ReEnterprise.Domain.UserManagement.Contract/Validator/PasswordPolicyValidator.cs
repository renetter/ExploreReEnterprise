using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using FluentValidation;
using ReEnterprise.Domain.UserManagement.Contract.Resources;

namespace ReEnterprise.Domain.UserManagement.Contract.Validator
{
    public class PasswordPolicyValidator : AbstractValidator<PasswordPolicy>
    {
        public PasswordPolicyValidator()
        {
            RuleFor(c => c.MinimumLength).GreaterThan(0).WithName(PasswordPolicyResources.MinimumLength);
            // strong password can only be set if the minimun length greather than 3
            RuleFor(c => c.StrongPassword).Must((model, propertyValue) => model.MinimumLength >= 3 || !propertyValue)
                .WithMessage(string.Format(PasswordPolicyResources.MixedCharacterValidationError, 
                                           PasswordPolicyResources.StrongPassword, 
                                           PasswordPolicyResources.MinimumLength, 3));
        }
    }
}
