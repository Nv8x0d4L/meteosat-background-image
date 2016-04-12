using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options)) return;

            string fullPath;
            if (!GetFullPath(options.ImagePath, options.FileName, out fullPath)) return;

            var imageDownloader = new ImageDownloader();
            imageDownloader.SaveToFile(options.Username, options.Password, fullPath, options.IsGridEnabled, options.MaximumRetries);

            var setter = new Setter();
            setter.SetWallpaper(fullPath, (Style)options.DesktopStyle);
        }

        private static bool GetFullPath(string imagePath, string fileName, out string fullPath)
        {
            try
            {
                Directory.CreateDirectory(imagePath);
                fullPath = Path.Combine(imagePath, fileName);
                return true;
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception);
                fullPath = "";
                return false;
            }
        }
    }
}
