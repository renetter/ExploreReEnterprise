using System;
using System.Runtime.Serialization;
using FluentValidation.Attributes;
using ReEnterprise.Domain.UserManagement.Contract.Validator;

namespace ReEnterprise.Domain.UserManagement.Contract.Entity
{
    /// <summary>
    /// User entity.
    /// </summary>
    [DataContract]
    [Validator(typeof (UserValidator))]
    public class User
    {
        private string _userId;

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            _userId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        [DataMember]
        public virtual string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [DataMember]
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [DataMember]
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [DataMember]
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [DataMember]
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [DataMember]
        public virtual string Password { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [DataMember]
        public virtual string Address { get; set; }

        /// <summary>
        /// Gets or sets the security question.
        /// </summary>
        /// <value>
        /// The security question.
        /// </value>
        [DataMember]
        public virtual string SecurityQuestion { get; set; }

        /// <summary>
        /// Gets or sets the security question answer.
        /// </summary>
        /// <value>
        /// The security question answer.
        /// </value>
        [DataMember]
        public virtual string SecurityQuestionAnswer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user must change password on next logon.
        /// </summary>
        /// <value>
        ///   <c>true</c> if user forced to change password; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public virtual bool ForceChangePassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether password policy applied for this user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [apply password policy]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public virtual bool ApplyPasswordPolicy { get; set; }
    }
}