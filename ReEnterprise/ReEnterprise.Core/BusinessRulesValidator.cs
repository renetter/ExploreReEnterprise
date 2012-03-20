using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Core.Interface;
using ReEnterprise.Core.Generic;
using Microsoft.Practices.ServiceLocation;

namespace ReEnterprise.Core
{
    /// <summary>
    /// Business rule validator class.
    /// </summary>
    public class BusinessRulesValidator : IBusinessRulesValidator
    {
        private IList<IRuleValidator> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRulesValidator"/> class.
        /// </summary>
        public BusinessRulesValidator()
        {
            _validators = new List<IRuleValidator>();
        }

        /// <summary>
        /// Adds the specified validator.
        /// </summary>
        /// <param name="validator">The validator.</param>
        public void Add(IRuleValidator validator)
        {
            _validators.Add(validator);
        }

        /// <summary>
        /// Validates the business rule using the all the validator that already been added.
        /// </summary>
        /// <returns>
        /// List of validation messages.
        /// </returns>
        public IEnumerable<ValidationMessage> Validate()
        {
            IList<ValidationMessage> result = new List<ValidationMessage>();

            foreach (var validator in _validators)
            {
                result.AddValidationMessages(validator.Validate());
            }

            return result;
        }

        /// <summary>
        /// Creates the validator and add the validator with the specified targetModel.
        /// </summary>
        /// <typeparam name="TValidator">The type of the validator.</typeparam>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="targetModel">The target model.</param>
        /// <returns>
        /// The validator instance
        /// </returns>
        public TValidator CreateValidator<TValidator, TModel>(TModel targetModel) where TValidator : IRuleValidator<TModel>
        {
            // Get validator instance
            TValidator validator = ServiceLocator.Current.GetInstance<TValidator>();
            
            // Set validator target
            validator.SetValidationTarget(targetModel);

            // Add the validator to the validators
            _validators.Add(validator);

            return validator;
        }
    }
}
