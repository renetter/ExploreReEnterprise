using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ReEnterprise.Core
{
    /// <summary>
    /// Provide base class for all entity.
    /// </summary>
    [DataContract]
    public class ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseBase"/> class.
        /// </summary>
        public ResponseBase()
        {
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        [DataMember]
        public IList<ValidationMessage> ValidationMessages { get; private set; }
    }
}