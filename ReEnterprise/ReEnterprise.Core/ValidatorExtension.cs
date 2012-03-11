using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReEnterprise.Core
{
    /// <summary>
    /// Helper methods for determining validation error type.
    /// </summary>
    public static class ValidatorExtension
    {
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
    }

}
