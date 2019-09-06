using System;

namespace AmazonPayV2.Exceptions
{
    public class AmazonPayClientException : Exception
    {
        /// <summary>
        /// Constructs AmazonPayClientException with given message
        /// </summary>
        /// <param name="message"></param>
        public AmazonPayClientException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructs AmazonPayClientException with given message and underlying exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AmazonPayClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}