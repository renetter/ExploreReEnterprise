using System.Runtime.Serialization;
using FluentValidation.Attributes;
using ReEnterprise.Domain.UserManagement.Contract.Validator;

namespace ReEnterprise.Domain.UserManagement.Contract.Entity
{
    /// <summary>
    /// Password Policy
    /// </summary>
    [DataContract]
    [Validator(typeof (PasswordPolicyValidator))]
    public class PasswordPolicy
    {
        /// <summary>
        /// Gets or sets the minimum length.
        /// </summary>
        /// <value>
        /// The minimum length.
        /// </value>
        [DataMember]
        public virtual int MinimumLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mixed character.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is mixed character; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public virtual bool StrongPassword { get; set; }
    }
}