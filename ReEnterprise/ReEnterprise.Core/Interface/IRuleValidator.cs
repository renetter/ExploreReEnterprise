using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReEnterprise.Core.Interface
{
    public interface IRuleValidator
    {
        /// <summary>
        /// Check whether the target validation satisfy the business rule validation.
        /// </summary>
        /// <returns>Business rule validation result.</returns>
        IEnumerable<ValidationMessage> Validate();
    }
}
