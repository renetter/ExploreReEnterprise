using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Domain.UserManagement.Contract;
using ReEnterprise.Domain.UserManagement.Contract.Service;
using ReEnterprise.Domain.UserManagement.Contract.Validator;
using Microsoft.Practices.ServiceLocation;
using ReEnterprise.Core;
using ReEnterprise.Domain.UserManagement.Contract.Repository;
using ReEnterprise.Core.Interface;
using ReEnterprise.Domain.UserManagement.Contract.Entity;

namespace ReEnterprise.Domain.UserManagement
{
    /// <summary>
    /// User management service.
    /// </summary>
    public class UserManagementService : IUserManagementService
    {
        private IPasswordPolicyRepository _passwordPolicyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManagementService"/> class.
        /// </summary>
        /// <param name="passwordPolicyRepository">The password policy repository.</param>
        public UserManagementService(IPasswordPolicyRepository passwordPolicyRepository)
        {
            _passwordPolicyRepository = passwordPolicyRepository;
        }

        /// <summary>
        /// Saves the password policy.
        /// </summary>
        /// <param name="passwordPolicy">The password policy.</param>
        /// <returns>
        /// Password policy entity, containing validation messages.
        /// </returns>
        public Contract.Entity.PasswordPolicy SavePasswordPolicy(Contract.Entity.PasswordPolicy passwordPolicy)
        {
            Validate(passwordPolicy);

            if (!passwordPolicy.EntityHasError())
            {
                _passwordPolicyRepository.SavePasswordPolicy(passwordPolicy);
            }

            return passwordPolicy;
        }

        /// <summary>
        /// Validates the specified password policy entity.
        /// </summary>
        /// <param name="passwordPolicy">The password policy.</param>
        private static void Validate(Contract.Entity.PasswordPolicy passwordPolicy)
        {
            IBusinessRulesValidator validator = ServiceLocator.Current.GetInstance<IBusinessRulesValidator>();
            validator.CreateValidator<ModelValidator<PasswordPolicy>, PasswordPolicy>(passwordPolicy);

            passwordPolicy.ValidationMessages.AddValidationMessages(validator.Validate());
        }

        /// <summary>
        /// Retrieves the password policy.
        /// </summary>
        /// <returns>
        /// Password policy entity.
        /// </returns>
        public Contract.Entity.PasswordPolicy RetrievePasswordPolicy()
        {
            return _passwordPolicyRepository.GetPasswordPolicy();
        }
    }
}
