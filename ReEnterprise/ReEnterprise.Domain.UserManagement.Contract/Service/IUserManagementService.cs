using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Core;

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
        SavePasswordPolicyResponse SavePasswordPolicy(PasswordPolicy passwordPolicy);

        /// <summary>
        /// Retrieves the password policy.
        /// </summary>
        /// <returns>Password policy entity.</returns>
        RetrievePasswordPolicyResponse RetrievePasswordPolicy();

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <returns>User entity, Contain validation result message if any.</returns>
        CreateUserResponse CreateUser(User newUser);
    }

    public class SavePasswordPolicyResponse : ResponseBase
    {
        public PasswordPolicy PasswordPolicy { get; set; }
    }

    public class RetrievePasswordPolicyResponse : ResponseBase
    {
        public PasswordPolicy PasswordPolicy { get; set; }
    }

    public class CreateUserResponse : ResponseBase
    {
        public User User { get; set; }
    }

}
