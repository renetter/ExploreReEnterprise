using System.Collections.Generic;
using ReEnterprise.Core.Generic;

namespace ReEnterprise.Core.Interface
{
    public interface IBusinessRulesValidator
    {
        /// <summary>
        /// Manually adds the specified validator.
        /// </summary>
        /// <param name="validator">The validator.</param>
        void Add(IRuleValidator validator);

        /// <summary>
        /// Adds the specified validator.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="target">The target.</param>
        void Add<TModel>(IRuleValidator<TModel> validator, TModel target);

        /// <summary>
        /// Validates the business rule.
        /// </summary>
        /// <returns>List of validation messages.</returns>
        IEnumerable<ValidationMessage> Validate();
    }
}