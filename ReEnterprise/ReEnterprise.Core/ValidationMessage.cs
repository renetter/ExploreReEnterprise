using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ReEnterprise.Core
{
    /// <summary>
    /// Contain information, warning or error message.
    /// </summary>
    [DataContract]
    public class ValidationMessage
    {
        /// <summary>
        /// Gets or sets the field that invoked the validation message. May be empty if there are no field that invokes the validation message.
        /// </summary>
        /// <value>
        /// The field that invokes the validation message.
        /// </value>
        [DataMember]
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>
        /// The type of the message.
        /// </value>
        [DataMember]
        public ValidationMessageType MessageType { get; set; }

        /// <summary>
        /// Gets or sets the message value.
        /// </summary>
        /// <value>
        /// The message value.
        /// </value>
        [DataMember]
        public string MessageValue { get; set; }
    }
}
