using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using log4net;
using meteosat.model;
using meteosat.model.Config;
using meteosat.viewModel.CommandHandler;
using meteosat.viewModel.modelObjects;

namespace meteosat.viewModel
{
    public class MainWindowViewModel
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindowViewModel));

        public OptionsViewModel Options { get; set; }
        public TimerViewModel Timer { get; set; }

        public ParameterizedCommandHandler<PasswordBox> SendCommand { get; set; }
        public EmptyCommandHandler ExitCommand { get; set; }
        public ParameterizedCommandHandler<PasswordBox> ConfigSaveCommand { get; set; }
        public ParameterizedCommandHandler<PasswordBox> ConfigLoadCommand { get; set; }
        public EmptyCommandHandler TimerStopCommand { get; set; }
        public ParameterizedCommandHandler<PasswordBox> TimerStartCommand { get; set; }
        public EmptyCommandHandler ShowAboutCommand { get; set; }
        
        private AboutBox About { get; set; }

        private ConfigHandler _configHandler { get; set; }

        public MainWindowViewModel()
        {
            Options = new OptionsViewModel(AppDomain.CurrentDomain.BaseDirectory);
            Timer = new TimerViewModel();
            Timer.Tick += dispatcherTimer_Tick;
            
            SendCommand = new ParameterizedCommandHandler<PasswordBox>(SendAction);
            ExitCommand = new EmptyCommandHandler(ExitAction);
            ConfigSaveCommand = new ParameterizedCommandHandler<PasswordBox>(ConfigSaveAction);
            ConfigLoadCommand = new ParameterizedCommandHandler<PasswordBox>(ConfigLoadAction);
            TimerStartCommand = new ParameterizedCommandHandler<PasswordBox>(TimerStartAction);
            TimerStopCommand = new EmptyCommandHandler(TimerStop);
            ShowAboutCommand = new EmptyCommandHandler(ShowAboutAction);

            _configHandler = new ConfigHandler(Options.InputDirectory, Options.OptionsModel);

            About = new AboutBox();
        }

        private void ShowAboutAction()
        {
            About.ShowDialog();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var worker = new Worker(Options.OptionsModel);
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync(worker);
        }

        private void TimerStop()
        {
            Timer.Stop();
        }

        private void TimerStartAction(PasswordBox parameter)
        {
            Options.Password = parameter.Password;
            if (!Timer.IsEnabled)
            {
                Timer.Start();
            }
        }

        private void ConfigSaveAction(PasswordBox parameter)
        {
            Logger.Info("Save");
            Options.Password = parameter.Password;
            _configHandler.WriteConfig(Options.OptionsModel);
        }

        private void ConfigLoadAction(PasswordBox parameter)
        {
            Logger.Info("Load");
            var tempOptionsModel = _configHandler.ReadOrCreateConfig();
            Options.Username = tempOptionsModel.Username;
            Options.InputDirectory = tempOptionsModel.InputDirectory;
            Options.IsGridEnabled = tempOptionsModel.IsGridEnabled;
            Options.MaximumRetries = tempOptionsModel.MaximumRetries;
            Options.HoursToSubtract = tempOptionsModel.HoursToSubtract;
            Options.SetDesktopStyleWithModel(tempOptionsModel.DesktopStyle);
            parameter.Password = tempOptionsModel.Password;
        }

        private void ExitAction()
        {
            Logger.Info("Exit");
            Application.Current.Shutdown(0);
        }

        public void SendAction(PasswordBox parameter)
        {
            Logger.Info("Send");
            Options.Password = parameter.Password;
            var worker = new Worker(this.Options.OptionsModel);
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync(worker);
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Timer.SetRunToNow();
            var worker = (Worker) e.Argument;
            worker.DoWork();
        }
    }
}
