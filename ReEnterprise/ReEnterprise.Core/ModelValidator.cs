using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ReEnterprise.Core.Interface;

namespace ReEnterprise.Core
{
    /// <summary>
    /// Generic validator for model that has implemented data annotation validation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelValidator<T> : IValidator<T>
    {
        private T _target;

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

            IList<ValidationResult> validationResults = new List<ValidationResult>();
            IList<ValidationMessage> modelValidationResults = new List<ValidationMessage>();

            // invoke the data annotation validator
            Validator.TryValidateObject(_target, new ValidationContext(_target, null, null), validationResults, true);

            // map the data annotation validation message to validation message
            foreach (var validationResult in validationResults)
            {
                modelValidationResults.Add(new ValidationMessage
                {
                    Code = CoreConstants.ValidationErrorCodes.DataAnnotation,
                    Field = validationResult.MemberNames.FirstOrDefault(),
                    MessageType = ValidationMessageType.Error,
                    MessageValue = validationResult.ErrorMessage                    
                });
            }

            return modelValidationResults;
        }
    }
}
