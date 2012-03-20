using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Core.Interface;

namespace ReEnterprise.Core.Generic
{
    /// <summary>
    /// Provide the interface for business rule validation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRuleValidator<T> : IRuleValidator
    {
        /// <summary>
        /// Sets the validation target.
        /// </summary>
        /// <param name="target">The target.</param>
        void SetValidationTarget(T target);
    }
}
