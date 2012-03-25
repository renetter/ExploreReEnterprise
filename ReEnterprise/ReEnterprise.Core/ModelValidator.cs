using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using ReEnterprise.Core.Generic;

namespace ReEnterprise.Core
{
    /// <summary>
    /// Generic validator for model that has implemented data annotation validation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelValidator<T> : IRuleValidator<T> where T : class
    {
        private readonly IValidatorFactory _validatorFactory;
        private T _target;

        public ModelValidator(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        #region IRuleValidator<T> Members

        /// <summary>
        /// Sets the validation target.
        /// </summary>
        /// <param name="target">The target.</param>
        public void SetValidationTarget(T target)
        {
            _target = target;
        }

        /// <summary>
        /// Validates the model by using the data annotation validation.
        /// </summary>
        /// <returns>Validation error messages.</returns>
        public IEnumerable<ValidationMessage> Validate()
        {
            if (_target == null)
            {
                throw new InvalidOperationException("Target must be set before it can be validated.");
            }

            IValidator modelValidator = _validatorFactory.GetValidator<T>();

            ValidationResult validationResults = modelValidator.Validate(_target);

            // map the fluent validation message to validation message
            return validationResults.Errors.Select(validationResult => new ValidationMessage
                                                                           {
                                                                               Field = validationResult.PropertyName, MessageType = ValidationMessageType.Error, MessageValue = validationResult.ErrorMessage
                                                                           }).ToList();
        }

        #endregion
    }
}