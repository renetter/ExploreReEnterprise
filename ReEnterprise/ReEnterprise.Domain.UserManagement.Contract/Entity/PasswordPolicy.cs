using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Core;
using System.Runtime.Serialization;
using ReEnterprise.Domain.UserManagement.Contract.Validator;
using FluentValidation.Attributes;

namespace ReEnterprise.Domain.UserManagement.Contract.Entity
{
    /// <summary>
    /// Password Policy
    /// </summary>
    [DataContract]
    [Validator(typeof(PasswordPolicyValidator))]
    public class PasswordPolicy : EntityBase
    {
        /// <summary>
        /// Gets or sets the minimum length.
        /// </summary>
        /// <value>
        /// The minimum length.
        /// </value>
        [DataMember]
        public int MinimumLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mixed character.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is mixed character; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsMixedCharacter { get; set; }
    }
}
