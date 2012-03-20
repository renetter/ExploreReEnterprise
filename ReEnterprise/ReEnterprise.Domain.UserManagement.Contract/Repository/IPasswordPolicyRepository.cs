using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Domain.UserManagement.Contract.Entity;

namespace ReEnterprise.Domain.UserManagement.Contract.Repository
{
    /// <summary>
    /// Password policy repository.
    /// </summary>
    public interface IPasswordPolicyRepository
    {
        /// <summary>
        /// Saves the password policy.
        /// </summary>
        /// <param name="passwordPolicy">The password policy.</param>
        void SavePasswordPolicy(PasswordPolicy passwordPolicy);

        /// <summary>
        /// Gets the password policy.
        /// </summary>
        /// <returns>Password policy.</returns>
        PasswordPolicy GetPasswordPolicy();
    }
}
