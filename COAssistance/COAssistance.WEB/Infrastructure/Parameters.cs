using System;
using System.Collections.Generic;

namespace COAssistance.WEB.Infrastructure
{
    public static class Parameters
    {
        #region Methods

        //public static KeyValuePair<string, T> Pair<T>(string key, T value)
        //    where T : IConvertible
        //{
        //    return new KeyValuePair<string, T>(key, value);
        //}

        public static KeyValuePair<string, object> Pair(string key, object value)
        {
            return new KeyValuePair<string, object>(key, value);
        }

        #endregion Methods
    }
}