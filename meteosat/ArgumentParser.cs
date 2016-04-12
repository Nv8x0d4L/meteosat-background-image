using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meteosat
{
    class ArgumentParser
    {

        public void PrintHelp()
        {
            Console.Out.Write("Usage:\n\tmeteosat.exe [options]\n\nwhere\n\toptions:\n");
            var optionFormat = "\t\t{0}: {1}\n";
            Console.Out.Write(optionFormat, "--username [username]", "specifies the username");
            Console.Out.Write(optionFormat, "-u [username]", "alias for --username");
            Console.Out.Write(optionFormat, "--password [password]", "specifies the password");
            Console.Out.Write(optionFormat, "-p [password]", "alias for --password");
            Console.Out.Write(optionFormat, "--enable-grid", "if this option is present, then an image with grid will be used.");
        }

        public void ParseArgs(string[] args, out string username, out string password, out bool isGridEnabled)
        {
            var arguments = args.ToList();
            username = GetStringValue(arguments, "--username", "-u");
            password = GetStringValue(arguments, "--password", "-p");
            isGridEnabled = GetBooleanFlag(arguments, false, "--enable-grid");
        }

        private bool GetBooleanFlag(List<string> arguments, bool defaultValue, string keyword)
        {
            return ExtractBooleanFlagFromList(arguments, keyword) ? !defaultValue : defaultValue;
        }

        private bool ExtractBooleanFlagFromList(List<string> arguments, string option)
        {
            return arguments.Contains(option);
        }

        private string GetStringValue(List<string> arguments, params string[] keywords)
        {
            string option = "";
            foreach (var k in keywords)
            {
                option = (option == "") ? ExtractStringValueFromList(arguments, k) : option;
            }
            return option;
        }

        private string ExtractStringValueFromList(List<string> arguments, string option)
        {
            if (arguments.Contains(option))
            {
                var targetIndex = arguments.IndexOf(option) + 1;
                if (arguments.Count > targetIndex)
                {
                    return arguments[targetIndex];
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}
