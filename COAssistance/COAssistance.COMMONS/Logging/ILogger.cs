using System;

namespace COAssistance.COMMONS.Logging
{
    public interface ILogger
    {
        #region Methods

        void LogToFile(Exception exception);

        #endregion Methods
    }
}