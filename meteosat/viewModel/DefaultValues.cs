using System;
using meteosat.viewModel.modelObjects;

namespace meteosat.viewModel
{
    public class DefaultValues
    {
        private const string UnableToParseDefaultValueForKeyTo = "Unable to parse default value {0} for key {1} to {2}";
        private const string DefaultValueForNotFound = "Default value for {0} not found";

        public static string GetString(string key)
        {
            var value = Properties.Resources.ResourceManager.GetString(key);
            if (value != null)
            {
                return value;
            }
            else
            {
                throw new Exception(string.Format(DefaultValueForNotFound,  key));
            }
        }

        public static int GetInteger(string key)
        {
            int parsedValue;
            var value = GetString(key);
            if (int.TryParse(value, out parsedValue))
            {
                return parsedValue;
            }
            else
            {
                throw new Exception(string.Format(UnableToParseDefaultValueForKeyTo, value, key, parsedValue.GetType().ToString()));
            }
        }

        public static bool GetBoolean(string key)
        {
            bool parsedValue;
            var value = GetString(key);
            if (bool.TryParse(value, out parsedValue))
            {
                return parsedValue;
            }
            else
            {
                throw new Exception(string.Format(UnableToParseDefaultValueForKeyTo, value, key, parsedValue.GetType().ToString()));
            }
        }

    public static StyleViewModel GetStyleViewModel(string key)
        {
            return StyleConverter.ViewModelFromString(GetString(key));
        }
    }
}
