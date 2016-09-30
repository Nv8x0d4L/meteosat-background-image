using System.IO;
using Newtonsoft.Json;

namespace meteosat.model.Config
{
    public class ConfigHandler
    {
        private const string MeteosatConfig = "meteosat.config";
        private ConfigFileHandler Config { get; set; }
        private EncryptionHandler Crypt { get; set; }

        public ConfigHandler(string directory)
        {
            string configPath = Path.Combine(directory, MeteosatConfig);
            Config = new ConfigFileHandler(configPath);
            Crypt = new EncryptionHandler();
        }

        public void WriteConfig(Options options)
        {
            options.Password = Crypt.Encrypt(options.Password);
            var json = JsonConvert.SerializeObject(options);
            this.Config.Write(json);
        }

        public Options ReadConfig()
        {
            var json = this.Config.Read();
            var options = JsonConvert.DeserializeObject<Options>(json);
            options.Password = Crypt.Decrypt(options.Password);
            return options;
        }
    }
}
