using System.Collections.Generic;
using System.Linq;
using ReEnterprise.Core;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Domain.UserManagement.Contract.Repository;
using ReEnterprise.Domain.UserManagement.Contract.Validator;
using ReEnterprise.Domain.UserManagement.Service.Contract.Resources;
using ReEnterprise.Domain.UserManagement.Service.Resources;

namespace ReEnterprise.Domain.UserManagement.Validator
{
    public class PasswordPolicyRuleValidator : IPasswordPolicyRuleValidator
    {
        private const string PasswordField = "Password";
        private const string SpecialCharacter = "~`!@#$%^&*()_+-={[}]:;\"\'<,>.?/|\\";
        private readonly IPasswordPolicyRepository _passwordPolicyRepository;
        private User _target;

        public PasswordPolicyRuleValidator(IPasswordPolicyRepository passwordPolicyRepository)
        {
            _passwordPolicyRepository = passwordPolicyRepository;
        }

        #region IPasswordPolicyRuleValidator Members

        public void SetValidationTarget(User target)
        {
            _target = target;
        }

        public IEnumerable<ValidationMessage> Validate()
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
                                       MessageValue =
                                           string.Format(
                                               PasswordPolicyRuleValidatorResources.PasswordMinimumLengthMessage,
                                               UserResources.Password, passwordPolicy.MinimumLength)
                                   });
                }

                if (passwordPolicy.StrongPassword)
                {
                    ValidateStrongPassword(_target.Password, result);
                }
            }

            return result;
        }

        #endregion

        private void ValidateStrongPassword(string password, IList<ValidationMessage> result)
        {
            bool hasLetter = false;
            bool hasNumeric = false;
            bool hasSymbol = false;

            foreach (char character in password)
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
                                   MessageValue =
                                       string.Format(
                                           PasswordPolicyRuleValidatorResources.PasswordMustContainLetterNumericSymbol,
                                           UserResources.Password)
                               });
            }
        }

        private static bool IsSpecialCharacter(char character)
        {
            return SpecialCharacter.Contains(character);
        }
    }
}