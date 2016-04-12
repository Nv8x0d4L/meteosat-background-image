using System;
using System.IO;
using log4net;

namespace meteosat.Image
{
    public class TemporaryFileHandler
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TemporaryFileHandler));

        public string FullPath { get; set; }

        public bool CreateFullPath(string imagePath, string fileName)
        {
            try
            {
                Directory.CreateDirectory(imagePath);
                FullPath = Path.Combine(imagePath, fileName);
                return true;
            }
            catch (Exception exception)
            {
                Logger.Error(exception.ToString());
                return false;
            }
        }

        public bool DeleteImage()
        {
            try
            {
                File.Delete(FullPath);
                return true;
            }
            catch (Exception exception)
            {
                Logger.Error(exception.ToString());
                return false;
            }
        }
    }
}
