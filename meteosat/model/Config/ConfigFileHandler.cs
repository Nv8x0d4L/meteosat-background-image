using System.IO;
using log4net;

namespace meteosat.model.Config
{
    public class ConfigFileHandler
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ConfigFileHandler));

        private string FullPath { get; set; }

        public ConfigFileHandler(string fullPath)
        {
            FullPath = fullPath;
        }

        public void Write(string content)
        {
            using (var writer = new StreamWriter(FullPath))
            {
                writer.WriteLine(content);
            }
        }

        public string Read()
        {
            using (var reader = new StreamReader(FullPath))
            {
                return reader.ReadLine();
            }
        }

        public bool Exists()
        {
            return File.Exists(FullPath);
        }
    }
}
