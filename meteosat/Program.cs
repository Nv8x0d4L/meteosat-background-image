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
            var username = args[0];
            var password = args[1];
            var isGridEnabled = args[2] == "true";
            var maxRetries = Int32.Parse(args[3]);
            const string imagePath = "C:\\Temp\\1.jpg";
            var imageDownloader = new ImageDownloader();
            imageDownloader.SaveToFile(username, password, imagePath, isGridEnabled, maxRetries);
            var setter = new Setter();
            setter.SetWallpaper(imagePath, Style.Fit);
        }
    }
}
