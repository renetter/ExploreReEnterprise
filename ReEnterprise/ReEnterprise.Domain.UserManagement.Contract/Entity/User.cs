using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ReEnterprise.Domain.UserManagement.Contract.Validator;
using FluentValidation.Attributes;

namespace ReEnterprise.Domain.UserManagement.Contract.Entity
{
    /// <summary>
    /// User entity.
    /// </summary>
    [DataContract]
    [Validator(typeof(UserValidator))]
    public class User
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        [DataMember]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [DataMember]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the security question.
        /// </summary>
        /// <value>
        /// The security question.
        /// </value>
        [DataMember]
        public string SecurityQuestion { get; set; }

        /// <summary>
        /// Gets or sets the security question answer.
        /// </summary>
        /// <value>
        /// The security question answer.
        /// </value>
        [DataMember]
        public string SecurityQuestionAnswer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user must change password on next logon.
        /// </summary>
        /// <value>
        ///   <c>true</c> if user forced to change password; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool ForceChangePassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether password policy applied for this user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [apply password policy]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool ApplyPasswordPolicy { get; set; }
    }
}
