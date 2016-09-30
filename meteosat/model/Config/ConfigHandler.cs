using System.IO;
using Newtonsoft.Json;

namespace meteosat.model.Config
{
    public class ConfigHandler
    {
        private const string MeteosatConfig = "meteosat.config";
        private ConfigFileHandler Config { get; set; }
        private EncryptionHandler Crypt { get; set; }
        private Options DefaultOptions { get; set; }

        public ConfigHandler(string directory, Options options)
        {
            var configPath = Path.Combine(directory, MeteosatConfig);
            Config = new ConfigFileHandler(configPath);
            Crypt = new EncryptionHandler();
            DefaultOptions = options;
        }

        public void WriteConfig(Options options)
        {
            options.Password = Crypt.Encrypt(options.Password);
            var json = JsonConvert.SerializeObject(options);
            this.Config.Write(json);
        }

        public Options ReadOrCreateConfig()
        {
            if (this.Config.Exists())
            {
                return this.ReadConfig();
            }
            else
            {
                this.WriteConfig(DefaultOptions);
                return DefaultOptions;
            }
        }

        private Options ReadConfig()
        {
            var json = this.Config.Read();
            var options = JsonConvert.DeserializeObject<Options>(json);
            options.Password = Crypt.Decrypt(options.Password);
            return options;
        }
    }
}
