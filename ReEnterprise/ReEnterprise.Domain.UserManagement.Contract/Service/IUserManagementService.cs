using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Domain.UserManagement.Contract.Entity;

namespace ReEnterprise.Domain.UserManagement.Contract.Service
{
    /// <summary>
    /// User Service.
    /// </summary>
    public interface IUserManagementService
    {
        /// <summary>
        /// Saves the password policy.
        /// </summary>
        /// <param name="passwordPolicy">The password policy.</param>
        /// <returns>Password policy entity, containing validation messages.</returns>
        PasswordPolicy SavePasswordPolicy(PasswordPolicy passwordPolicy);

        /// <summary>
        /// Retrieves the password policy.
        /// </summary>
        /// <returns>Password policy entity.</returns>
        PasswordPolicy RetrievePasswordPolicy();
    }

}
