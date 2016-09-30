using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using log4net;
using Application = System.Windows.Application;
using meteosat.util;
using meteosat.viewModel;

namespace meteosat.view
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string CaptionShow = DefaultValues.GetString("CaptionShow");
        private static readonly string CaptionHide = DefaultValues.GetString("CaptionHide");
        private static readonly string CaptionExit = DefaultValues.GetString("CaptionExit");
        private static readonly string TrayIconResourceName = DefaultValues.GetString("TrayIconResourceName");
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));
        public NotifyIcon NotifyIcon { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitializeNotifyIcon();
            Application.Current.Exit += Current_Exit;
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                if (NotifyIcon == null) return;
                NotifyIcon.Visible = false;
                //NotifyIcon.Icon = null;
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
            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(CaptionShow, (s, e) => ShowWindow());
            contextMenu.MenuItems.Add(CaptionHide, (s, e) => this.Hide());
            contextMenu.MenuItems.Add(CaptionExit, (s, e) => Application.Current.Shutdown(0));

            NotifyIcon = new NotifyIcon
            {
                Icon = (Icon)Properties.Resources.ResourceManager.GetObject(TrayIconResourceName),
                Visible = true,
                ContextMenu = contextMenu
            };
            NotifyIcon.DoubleClick += (s, e) => ShowWindow();
        }

        private void ShowWindow()
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
            {
                this.Hide();
            }

            base.OnStateChanged(e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = this.MainPanel.DataContext as MainWindowViewModel;
            viewModel?.ConfigLoadCommand.Execute(InputPassword);
        }
    }
}
