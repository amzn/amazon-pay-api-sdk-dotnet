using System;

namespace Amazon.Pay.API.Exceptions
{
    public class ApiConfigurationException : Exception
    {
        /// <summary>
        /// Constructs ApiConfigurationException with given message
        /// </summary>
        /// <param name="message"></param>
        public ApiConfigurationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructs ApiConfigurationException with given message and underlying exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ApiConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}