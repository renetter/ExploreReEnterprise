using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Domain.UserManagement.Contract;
using ReEnterprise.Domain.UserManagement.Contract.Service;
using ReEnterprise.Domain.UserManagement.Contract.Validator;
using ReEnterprise.Core;
using ReEnterprise.Domain.UserManagement.Contract.Repository;
using ReEnterprise.Core.Interface;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Core.Generic;

namespace ReEnterprise.Domain.UserManagement
{
    /// <summary>
    /// User management service.
    /// </summary>
    public class UserManagementService : IUserManagementService
    {
        private IPasswordPolicyRepository _passwordPolicyRepository;
        private IUserRepository _userRepository;

        public IRuleValidator<User> UserModelValidator { get; set; }

        public IRuleValidator<PasswordPolicy> PasswordPolicyValidator { get; set; }

        public IPasswordPolicyRuleValidator PasswordPolicyRuleValidator { get; set; }
        
        
        public UserManagementService(IPasswordPolicyRepository passwordPolicyRepository, IUserRepository userRepository)
        {
            _passwordPolicyRepository = passwordPolicyRepository;
            _userRepository = userRepository;
        }
        
        
        /// <summary>
        /// Saves the password policy.
        /// </summary>
        /// <param name="passwordPolicy">The password policy.</param>
        /// <returns>
        /// Password policy entity, containing validation messages.
        /// </returns>
        public SavePasswordPolicyResponse SavePasswordPolicy(Contract.Entity.PasswordPolicy passwordPolicy)
        {
            var validationResult = ValidatePasswordPolicy(passwordPolicy);

            if (!validationResult.HasError())
            {
                _passwordPolicyRepository.SavePasswordPolicy(passwordPolicy);
            }

            SavePasswordPolicyResponse response = new SavePasswordPolicyResponse();

            response.PasswordPolicy = passwordPolicy;
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
            RetrievePasswordPolicyResponse response = new RetrievePasswordPolicyResponse();

            response.PasswordPolicy = _passwordPolicyRepository.GetPasswordPolicy();
            
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
            var validationResult = ValidateUser(newUser);

            if (!validationResult.HasError())
            {
                _userRepository.Create(newUser);
            }

            CreateUserResponse response = new CreateUserResponse();

            response.User = newUser;
            response.ValidationMessages.AddValidationMessages(validationResult);

            return response;
        }

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
