using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using log4net;
using log4net.Config;
using meteosat.Background;
using meteosat.Image;
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

        public MainWindow()
        {
            XmlConfigurator.Configure();
            InitializeComponent();
            AppOptions = new Options();
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
    }
}
