using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using log4net;
using log4net.Config;
using meteosat.Background;
using meteosat.Image;
using Newtonsoft.Json;
using Application = System.Windows.Application;
using Path = System.IO.Path;
using Setter = meteosat.Background.Setter;
using Style = meteosat.Background.Style;

namespace meteosat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));

        public Options AppOptions { get; set; }
        public EncryptionHandler Encryption { get; set; }
        public NotifyIcon NotifyIcon { get; set; }
        private ConfigHandler Config { get; set; }

        public MainWindow()
        {
            XmlConfigurator.Configure();
            InitializeComponent();
            AppOptions = new Options(AppDomain.CurrentDomain.BaseDirectory);
            InitializeNotifyIcon();
            Application.Current.Exit += Current_Exit;

            Encryption = new EncryptionHandler();
            string configPath = Path.Combine(AppOptions.InputDirectory, "meteosat.config");
            Config = new ConfigHandler(configPath);
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                if (NotifyIcon == null) return;
                NotifyIcon.Visible = false;
                NotifyIcon.Icon = null;
                NotifyIcon.Dispose();
                NotifyIcon = null;
            }
            catch (Exception)
            {
                // handle the error
            }
        }

        private void InitializeNotifyIcon()
        {

            NotifyIcon = new NotifyIcon();
            NotifyIcon.Icon = (Icon)Properties.Resources.ResourceManager.GetObject("TrayIcon");
            NotifyIcon.Visible = true;
            NotifyIcon.DoubleClick +=
                (sender, args) =>
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
            {
                this.Hide();
            }

            base.OnStateChanged(e);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            var temporaryFileHandler = new TemporaryFileHandler();
            if (!temporaryFileHandler.CreateFullPath(AppOptions.InputDirectory, "meteosat.jpg")) return;

            var imageDownloader = new ImageDownloader();
            imageDownloader.SaveToFile(AppOptions.Username, InputPassword.Password, temporaryFileHandler.FullPath,
                AppOptions.IsGridEnabled, AppOptions.MaximumRetries, AppOptions.HoursToSubstract);

            var setter = new Setter();
            setter.SetWallpaper(temporaryFileHandler.FullPath, AppOptions.DesktopStyle);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
                AppOptions.Password = Encryption.Encrypt(InputPassword.Password);
                var json = JsonConvert.SerializeObject(AppOptions);
                this.Config.Write(json);
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var json = this.Config.Read();
            var tmpOptions = JsonConvert.DeserializeObject<Options>(json);
            AppOptions.Username = tmpOptions.Username;
            AppOptions.InputDirectory = tmpOptions.InputDirectory;
            AppOptions.IsGridEnabled = tmpOptions.IsGridEnabled;
            AppOptions.MaximumRetries = tmpOptions.MaximumRetries;
            AppOptions.DesktopStyle = tmpOptions.DesktopStyle;
            AppOptions.HoursToSubstract = tmpOptions.HoursToSubstract;
            AppOptions.Password = "";
            InputPassword.Password = Encryption.Decrypt(tmpOptions.Password);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }
    }

    
}
