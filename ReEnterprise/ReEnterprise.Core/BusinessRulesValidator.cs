using System.Collections.Generic;
using ReEnterprise.Core.Generic;
using ReEnterprise.Core.Interface;

namespace ReEnterprise.Core
{
    /// <summary>
    /// Business rule validator class.
    /// </summary>
    public class BusinessRulesValidator : IBusinessRulesValidator
    {
        private readonly IList<IRuleValidator> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRulesValidator"/> class.
        /// </summary>
        public BusinessRulesValidator()
        {
            _validators = new List<IRuleValidator>();
        }

        #region IBusinessRulesValidator Members

        /// <summary>
        /// Adds the specified validator.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="target">The target.</param>
        public void Add<TModel>(IRuleValidator<TModel> validator, TModel target)
        {
            validator.SetValidationTarget(target);
            _validators.Add(validator);
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

            foreach (IRuleValidator validator in _validators)
            {
                result.AddValidationMessages(validator.Validate());
            }

            return result;
        }

        #endregion
    }
}