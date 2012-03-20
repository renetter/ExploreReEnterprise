using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// Creates the validator and add the validator with the specified targetModel.
        /// </summary>
        /// <typeparam name="TValidator">The type of the validator.</typeparam>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="targetModel">The target model.</param>
        /// <returns>The validator instance</returns>
        TValidator CreateValidator<TValidator, TModel>(TModel targetModel) where TValidator : IRuleValidator<TModel>;

        /// <summary>
        /// Validates the business rule.
        /// </summary>
        /// <returns>List of validation messages.</returns>
        IEnumerable<ValidationMessage> Validate();
    }
}
