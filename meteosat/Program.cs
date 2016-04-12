using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using meteosat.Background;

namespace meteosat
{
    class Program
    {
        static void Main(string[] args)
        {
            var argumentParser = new ArgumentParser();
            if (args.Length == 0)
            {
                argumentParser.PrintHelp();
                return;
            }

            string username, password;
            bool isGridEnabled;
            argumentParser.ParseArgs(args, out username, out password, out isGridEnabled);
            if (username == "" || password == "")
            {
                Console.Out.WriteLine("Error: You must specify both username and password.");
                argumentParser.PrintHelp();
                return;
            }

            const string imagePath = "C:\\Temp\\1.jpg";
            const int maxRetries = 5;

            var imageDownloader = new ImageDownloader();
            imageDownloader.SaveToFile(username, password, imagePath, isGridEnabled, maxRetries);
            var setter = new Setter();
            setter.SetWallpaper(imagePath, Style.Fit);
        }
    }
}
