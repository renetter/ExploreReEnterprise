using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace ReEnterprise.Core
{
    /// <summary>
    /// Helper methods for determining validation error type.
    /// </summary>
    public static class ValidatorExtension
    {
        /// <summary>
        /// Creates the validation message.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="targetModel">The target model.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="messages">The messages.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <returns>Validation Message.</returns>
        public static ValidationMessage CreateValidationMessage<TModel, TValue>(this TModel targetModel, Expression<Func<TModel, TValue>> expression, string messages, ValidationMessageType messageType) 
        {
            string fieldName = FindMemberName(expression);

            return new ValidationMessage { Field = fieldName, MessageType = messageType, MessageValue = messages };
        }

        /// <summary>
        /// Adds the validation messages.
        /// </summary>
        /// <param name="validationMessages">The validation messages.</param>
        /// <param name="addedValidationMessages">The added validation messages.</param>
        public static void AddValidationMessages(this ICollection<ValidationMessage> validationMessages, IEnumerable<ValidationMessage> addedValidationMessages) 
        {
            if (addedValidationMessages == null)
            {
                return;
            }

            foreach (var validationMessage in addedValidationMessages)
            {
                validationMessages.Add(validationMessage);
            }
        }

        /// <summary>
        /// Determines whether the specified messages are contain errors.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns>
        ///   <c>true</c> if the specified messages are contain errors; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasError(this IEnumerable<ValidationMessage> messages)
        {
            return messages.Where(c => c.MessageType == ValidationMessageType.Error).Any();
        }

        /// <summary>
        /// Determines whether the specified model is contain errors.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>True if the specified model has error.</returns>
        public static bool EntityHasError<TModel>(this TModel model) where TModel : EntityBase
        {
            return model.ValidationMessages.HasError();
        }

        /// <summary>
        /// Determines whether the specified messages are contain warning.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns>
        ///   <c>true</c> if the specified messages are contain warning; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasWarning(this IEnumerable<ValidationMessage> messages)
        {
            return messages.Where(c => c.MessageType == ValidationMessageType.Warning).Any();
        }

        /// <summary>
        /// Determines whether the specified entity is contain warning.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>True if the specified model contain warning.</returns>
        public static bool EntityHasWarning<TModel>(this TModel model) where TModel : EntityBase
        {
            return model.ValidationMessages.HasWarning();
        }

        /// <summary>
        /// Determines whether the specified messages are contain information.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns>
        ///   <c>true</c> if the specified messages are contain information; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasInformation(this IEnumerable<ValidationMessage> messages)
        {
            return messages.Where(c => c.MessageType == ValidationMessageType.Information).Any();
        }

        /// <summary>
        /// Determines whether the specified entity is contain information message.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>True if the specified model contain information message.</returns>
        public static bool EntityHasInformation<TModel>(this TModel model) where TModel : EntityBase
        {
            return model.ValidationMessages.HasInformation();
        }

        /// <summary>
        /// Finds the name of the member.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        private static string FindMemberName<TModel, TValue>(Expression<Func<TModel, TValue>> expression)
        {
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                return (expression.Body as MemberExpression).Member.Name;
            }

            return string.Empty;
        }
    }

}
