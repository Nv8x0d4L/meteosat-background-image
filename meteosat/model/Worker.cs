using meteosat.util;

namespace meteosat.model
{
    public class Worker
    {
        private static readonly string MeteosatJpg = DefaultValues.GetString("MeteosatJpg");
        private Options Configuration { get; set; }
        public Worker(Options options)
        {
            Configuration = options;
        }

        public void DoWork()
        {
            var temporaryFileHandler = new TemporaryFileHandler();
            if (!temporaryFileHandler.CreateFullPath(Configuration.InputDirectory, MeteosatJpg)) return;

            var imageDownloader = new ImageDownloader();
            imageDownloader.SaveToFile(Configuration.Username, Configuration.Password, temporaryFileHandler.FullPath,
                Configuration.IsGridEnabled, Configuration.MaximumRetries, Configuration.HoursToSubtract);

            var setter = new Setter();
            setter.SetWallpaper(temporaryFileHandler.FullPath, Configuration.DesktopStyle);
        }
    }
}
