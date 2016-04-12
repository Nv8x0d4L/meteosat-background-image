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
            const string imagePath = "C:\\Temp\\1.jpg";
            var setter = new Setter();
            setter.SetWallpaper(imagePath, Style.Fit);
        }
    }
}
