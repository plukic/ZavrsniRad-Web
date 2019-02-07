namespace COAssistance.WEB.Constants
{
    public static class AssistanceConstants
    {
        #region Constants

        public const string ApplicationName = "Assistance";

        public const string ApplicationNameShort = "Ass";

        public const string ApplicationVersion = "1.0";

        public const string Developer = "Petar Lukić";

        public const string DeveloperSite = "https://www.google.ba";

        public const string DefaultDateTimeFormat = "dd-MM-yyyy / HH:mm";

        public const string DefaultDateFormat = "dd-MM-yyyy";

        public const string DefaultTimeFormat = "HH:mm";

        public const int PageSize = 15;

        public const string ImagesLocation = "~/Content/template/img/";

        public const string LogsLocation = "~/App_Data/Logs/";

        public const string LogsFileNameFormat = "dd-MM-yyyy";

        #endregion Constants

        #region Path constants

        public static class Paths
        {
            public const string ImagesLocation = "~/Content/template/img/mapicons/";

            public const string LogsLocation = "~/App_Data/Logs/";
        }

        #endregion Path constants

        #region Cookie constants

        public static class Cookies
        {
            public const string AuthenticationTokenKey = "Assistance.user";
        }

        #endregion Cookie constants
    }
}