using System;
using System.Runtime.Serialization;

namespace COAssistance.WEB.Infrastructure
{
    [Serializable]
    public class COUnauthorizedException : Exception
    {
        #region Constructors

        public COUnauthorizedException()
        {
        }

        public COUnauthorizedException(string message) : base(message)
        {
        }

        public COUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected COUnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Constructors
    }
}