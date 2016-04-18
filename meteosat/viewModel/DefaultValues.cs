using System;
using meteosat.viewModel.modelObjects;

namespace meteosat.viewModel
{
    public class DefaultValues
    {
        public static string GetString(string key)
        {
            var value = Properties.Resources.ResourceManager.GetString(key);
            if (value != null)
            {
                return value;
            }
            else
            {
                throw new Exception($"Default value for {key} not found");
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
                throw new Exception($"Unable to parse default value {value} for key {key} to int");
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
                throw new Exception($"Unable to parse default value {value} for key {key} to bool");
            }
        }

    public static StyleViewModel GetStyleViewModel(string key)
        {
            return StyleConverter.ViewModelFromString(GetString(key));
        }
    }
}
