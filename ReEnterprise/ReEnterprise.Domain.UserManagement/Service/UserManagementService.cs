using System.Collections.Generic;
using System.Linq;
using ReEnterprise.Core;
using ReEnterprise.Core.Generic;
using ReEnterprise.Core.Interface;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Domain.UserManagement.Contract.Repository;
using ReEnterprise.Domain.UserManagement.Contract.Service;
using ReEnterprise.Domain.UserManagement.Contract.Validator;

namespace ReEnterprise.Domain.UserManagement.Service
{
    /// <summary>
    /// User management service.
    /// </summary>
    public class UserManagementService : IUserManagementService
    {
        private readonly IPasswordPolicyRepository _passwordPolicyRepository;
        private readonly IUserRepository _userRepository;

        public UserManagementService(IPasswordPolicyRepository passwordPolicyRepository, IUserRepository userRepository)
        {
            _passwordPolicyRepository = passwordPolicyRepository;
            _userRepository = userRepository;
        }

        public IRuleValidator<User> UserModelValidator { get; set; }

        public IRuleValidator<PasswordPolicy> PasswordPolicyValidator { get; set; }

        public IPasswordPolicyRuleValidator PasswordPolicyRuleValidator { get; set; }

        #region IUserManagementService Members

        /// <summary>
        /// Saves the password policy.
        /// </summary>
        /// <param name="passwordPolicy">The password policy.</param>
        /// <returns>
        /// Password policy entity, containing validation messages.
        /// </returns>
        public SavePasswordPolicyResponse SavePasswordPolicy(PasswordPolicy passwordPolicy)
        {
            IEnumerable<ValidationMessage> validationResult = ValidatePasswordPolicy(passwordPolicy).ToList();

            if (!validationResult.HasError())
            {
                _passwordPolicyRepository.SavePasswordPolicy(passwordPolicy);
            }

            var response = new SavePasswordPolicyResponse {PasswordPolicy = passwordPolicy};

            response.ValidationMessages.AddValidationMessages(validationResult);

            return response;
        }

        /// <summary>
        /// Retrieves the password policy.
        /// </summary>
        /// <returns>
        /// Password policy entity.
        /// </returns>
        public RetrievePasswordPolicyResponse RetrievePasswordPolicy()
        {
            var response = new RetrievePasswordPolicyResponse
                               {PasswordPolicy = _passwordPolicyRepository.GetPasswordPolicy()};


            return response;
        }


        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <returns>
        /// User entity, Contain validation result message if any.
        /// </returns>
        public CreateUserResponse CreateUser(User newUser)
        {
            IEnumerable<ValidationMessage> validationResult = ValidateUser(newUser).ToList();

            if (!validationResult.HasError())
            {
                _userRepository.Create(newUser);
            }

            var response = new CreateUserResponse {User = newUser};

            response.ValidationMessages.AddValidationMessages(validationResult);

            return response;
        }

        #endregion

        private IEnumerable<ValidationMessage> ValidateUser(User newUser)
        {
            IBusinessRulesValidator businessRuleValidator = new BusinessRulesValidator();
            businessRuleValidator.Add(UserModelValidator, newUser);
            businessRuleValidator.Add(PasswordPolicyRuleValidator, newUser);

            return businessRuleValidator.Validate();
        }

        /// <summary>
        /// Validates the specified password policy entity.
        /// </summary>
        /// <param name="passwordPolicy">The password policy.</param>
        private IEnumerable<ValidationMessage> ValidatePasswordPolicy(PasswordPolicy passwordPolicy)
        {
            IBusinessRulesValidator validator = new BusinessRulesValidator();
            validator.Add(PasswordPolicyValidator, passwordPolicy);

            return validator.Validate();
        }
    }
}