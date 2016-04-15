using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace meteosat
{
    public class ConfigHandler
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (ConfigHandler));

        private string FullPath { get; set; }

        public ConfigHandler(string fullPath)
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
    }
}
