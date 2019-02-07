using System;
using System.Runtime.Serialization;

namespace COAssistance.WEB.Infrastructure
{
    [Serializable]
    public class COException : Exception
    {
        #region Constructors

        public COException()
        {
        }

        public COException(string message) : base(message)
        {
        }

        public COException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected COException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Constructors
    }
}