using System;
using System.IO;
using System.Text;
using System.Web;

namespace COAssistance.COMMONS.Logging
{
    public class Logger : ILogger
    {
        #region Fields

        private readonly string _logsFileNameFormat;
        private readonly string _logsLocation;

        #endregion Fields

        #region Constructor

        public Logger(string logsFileNameFormat, string logsLocation)
        {
            _logsFileNameFormat = logsFileNameFormat;
            _logsLocation = logsLocation;
        }

        #endregion Constructor

        #region Methods

        public void LogToFile(Exception exception)
        {
            var builder = new StringBuilder();

            builder.Append($"{Environment.NewLine}<log>{Environment.NewLine}");
            builder.Append($"\tDate: {DateTime.UtcNow.ToString()}{Environment.NewLine}");
            builder.Append($"\tMessage: {exception.Message}{Environment.NewLine}");
            builder.Append($"\tDetails: {exception.InnerException?.Message}{Environment.NewLine}");
            builder.Append($"\tUrl: {HttpContext.Current.Request.Url.PathAndQuery}{Environment.NewLine}");
            builder.Append($"\tUser: {HttpContext.Current.Request.UserHostAddress}{Environment.NewLine}");
            builder.Append($"\tBrowser: {HttpContext.Current.Request.Browser.Browser}{Environment.NewLine}");
            builder.Append($"</log>{Environment.NewLine}");

            Directory.CreateDirectory(DirectoryLocation);

            File.AppendAllText(this.FullFileName, builder.ToString());
        }

        #endregion Methods

        #region Properties

        private string DirectoryLocation => HttpContext.Current.Server.MapPath(_logsLocation);

        private string FullFileName
        {
            get
            {
                var fileName = DateTime.UtcNow.ToString(_logsFileNameFormat) + ".txt";

                return HttpContext.Current.Server.MapPath(_logsLocation + fileName);
            }
        }

        #endregion Properties
    }
}