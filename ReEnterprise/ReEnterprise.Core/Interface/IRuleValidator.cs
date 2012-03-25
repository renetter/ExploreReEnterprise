using System.Collections.Generic;

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