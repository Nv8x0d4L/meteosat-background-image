using log4net.Config;
using meteosat.Background;
using meteosat.Image;

namespace meteosat
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options)) return;

            var temporaryFileHandler = new TemporaryFileHandler();
            if (!temporaryFileHandler.CreateFullPath(options.ImagePath, options.FileName)) return;

            var imageDownloader = new ImageDownloader();
            imageDownloader.SaveToFile(options.Username, options.Password, temporaryFileHandler.FullPath, 
                options.IsGridEnabled, options.MaximumRetries, options.HoursToSubstract);

            var setter = new Setter();
            setter.SetWallpaper(temporaryFileHandler.FullPath, (Style)options.DesktopStyle);

            temporaryFileHandler.DeleteImage();
        }
    }
}
