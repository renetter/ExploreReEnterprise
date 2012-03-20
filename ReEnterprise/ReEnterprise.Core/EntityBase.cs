using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReEnterprise.Core
{
    /// <summary>
    /// Provide base class for all entity.
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBase"/> class.
        /// </summary>
        public EntityBase()
        {
            ValidationMessages = new List<ValidationMessage>();
        }

        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        public IList<ValidationMessage> ValidationMessages { get; private set; }
    }
}
