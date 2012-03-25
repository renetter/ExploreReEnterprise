namespace ReEnterprise.Core.Interface
{
    /// <summary>
    /// Provides interface for saving application log.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteLog(string message);

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type of this log.</param>
        void WriteLog(string message, ValidationMessageType type);

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type of this log.</param>
        /// <param name="severity">The severity.</param>
        void WriteLog(string message, ValidationMessageType type, ErrorSeverity severity);
    }
}