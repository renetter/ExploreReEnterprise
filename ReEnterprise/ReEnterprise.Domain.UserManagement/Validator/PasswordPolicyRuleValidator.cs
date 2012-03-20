using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Domain.UserManagement.Contract.Validator;
using ReEnterprise.Domain.UserManagement.Contract.Repository;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Core;
using ReEnterprise.Domain.UserManagement.Resources;
using ReEnterprise.Domain.UserManagement.Contract.Resources;

namespace ReEnterprise.Domain.UserManagement.Validator
{
    public class PasswordPolicyRuleValidator : IPasswordPolicyRuleValidator
    {
        private IPasswordPolicyRepository _passwordPolicyRepository;
        private User _target;

        private const string PasswordField = "Password";
        private const string SpecialCharacter = "~`!@#$%^&*()_+-={[}]:;\"\'<,>.?/|\\";

        public PasswordPolicyRuleValidator(IPasswordPolicyRepository passwordPolicyRepository)
        {
            _passwordPolicyRepository = passwordPolicyRepository;
        }

        public void SetValidationTarget(Contract.Entity.User target)
        {
            _target = target;
        }

        public IEnumerable<Core.ValidationMessage> Validate()
        {
            IList<ValidationMessage> result = new List<ValidationMessage>();
            
            if (_target.ApplyPasswordPolicy)
            {
                PasswordPolicy passwordPolicy = _passwordPolicyRepository.GetPasswordPolicy();

                if (_target.Password.Length < passwordPolicy.MinimumLength)
                {
                    result.Add(new ValidationMessage
                    {
                        Field = PasswordField,
                        MessageType = ValidationMessageType.Error,
                        MessageValue = string.Format(PasswordPolicyRuleValidatorResources.PasswordMinimumLengthMessage, UserResources.Password, passwordPolicy.MinimumLength)
                    });
                }

                if (passwordPolicy.StrongPassword)
                {
                    ValidateStrongPassword(_target.Password, result);
                }
            }

            return result;
        }

        private void ValidateStrongPassword(string password, IList<ValidationMessage> result)
        {
            bool hasLetter = false;
            bool hasNumeric = false;
            bool hasSymbol = false;

            foreach (var character in password)
            {
                if (!hasLetter && char.IsLetter(character))
                {
                    hasLetter = true;
                    continue;
                }

                if (!hasNumeric && char.IsNumber(character))
                {
                    hasNumeric = true;
                    continue;
                }

                if (!hasSymbol && IsSpecialCharacter(character))
                {
                    hasSymbol = true;
                    continue;
                }

                if (hasLetter && hasNumeric && hasSymbol)
                {
                    break;
                }
            }

            if (!hasLetter || !hasNumeric || !hasSymbol)
            {
                result.Add(new ValidationMessage
                {
                    Field = PasswordField,
                    MessageType = ValidationMessageType.Error,
                    MessageValue = string.Format(PasswordPolicyRuleValidatorResources.PasswordMustContainLetterNumericSymbol, UserResources.Password)
                });
            }
        }

        private bool IsSpecialCharacter(char character)
        {
            return SpecialCharacter.Contains(character);
        }
    }
}
