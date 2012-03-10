using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReEnterprise.Core.Interface
{
    /// <summary>
    /// Provide the interface for business rule validation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Sets the validation target.
        /// </summary>
        /// <param name="target">The target.</param>
        void SetValidationTarget(T target);

        /// <summary>
        /// Check whether the target validation satisfy the business rule validation.
        /// </summary>
        /// <returns>Business rule validation result.</returns>
        IList<ValidationMessage> Validate();
    }
}
