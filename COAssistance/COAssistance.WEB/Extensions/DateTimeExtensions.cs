using COAssistance.WEB.Constants;
using System;

namespace COAssistance.WEB.Extensions
{
    /// <summary>
    /// Extends existing DateTime methods
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Methods

        public static string ToDefaultFormat(this DateTime date)
        {
            return date.ToString(AssistanceConstants.DefaultDateTimeFormat);
        }

        public static string ToRangeFormat(this DateTime? dateFrom, DateTime? dateTo, DateTimeFormat format = DateTimeFormat.DateTime)
        {
            if (dateFrom != null && dateTo != null)
            {
                return $"{((DateTime)dateFrom).ToLocalTimeWithFormat(format)} / {((DateTime)dateTo).ToLocalTimeWithFormat(format)}";
            }
            else if (dateFrom != null)
            {
                return $"{((DateTime)dateFrom).ToLocalTimeWithFormat(format)} - /";
            }
            else if (dateTo != null)
            {
                return $"/ - {((DateTime)dateTo).ToLocalTimeWithFormat(format)}";
            }

            return "/";
        }

        public static string ToDefaultFormat(this DateTime? date, DateTimeFormat format = DateTimeFormat.DateTime)
        {
            if (date == null)
            {
                return "/";
            }

            return ((DateTime)date).ToString(GetFormatTemplate(format));
        }

        public static string ToLocalTimeWithFormat(this DateTime date, DateTimeFormat format = DateTimeFormat.DateTime)
        {
            return date.ToLocalTime().ToString(GetFormatTemplate(format));
        }

        #endregion Methods

        #region Utils

        private static string GetFormatTemplate(DateTimeFormat format)
        {
            switch (format)
            {
                case DateTimeFormat.DateTime:
                    return AssistanceConstants.DefaultDateTimeFormat;

                case DateTimeFormat.Date:
                    return AssistanceConstants.DefaultDateFormat;

                case DateTimeFormat.Time:
                    return AssistanceConstants.DefaultTimeFormat;

                default:
                    return AssistanceConstants.DefaultDateTimeFormat;
            }
        }

        #endregion Utils
    }

    public enum DateTimeFormat
    {
        DateTime = 1,
        Date = 2,
        Time = 3
    }
}