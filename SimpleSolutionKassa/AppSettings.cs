using System;
using System.Configuration;

namespace SimpleSolutionKassa
{
    public static class AppSettings
    {

        public static string GetStringValue(string keyName)
        {
            string value = ConfigurationManager.AppSettings[keyName];

            if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                return value;

            throw new ArgumentNullException(keyName, "Указанный конфигурационный параметр отсутствует в разделе appSettings приложения!");
        }

        public static T GetValue<T>(string keyName)where T:struct
        {
            string value = ConfigurationManager.AppSettings[keyName];

            if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                return Converter.To<T>(value);

            throw new ArgumentNullException(keyName, "Указанный конфигурационный параметр отсутствует в разделе appSettings приложения!");
        }

        public static void SetStringValue(string keyName, string value)
        {
            ConfigurationManager.AppSettings.Set(keyName, value);
        }
    }
}
