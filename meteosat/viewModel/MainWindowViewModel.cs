using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using meteosat.model;
using meteosat.model.Config;
using meteosat.viewModel.CommandHandler;
using meteosat.viewModel.modelObjects;

namespace meteosat.viewModel
{
    public class MainWindowViewModel
    {
        public OptionsViewModel Options { get; set; }
        public ParameterizedCommandHandler<PasswordBox> SendCommand { get; set; }
        public EmptyCommandHandler ExitCommand { get; set; }
        public ParameterizedCommandHandler<PasswordBox> SaveCommand { get; set; }
        public ParameterizedCommandHandler<PasswordBox> LoadCommand { get; set; }

        private ConfigHandler _configHandler { get; set; }

        public MainWindowViewModel()
        {
            Options = new OptionsViewModel(AppDomain.CurrentDomain.BaseDirectory);
            _configHandler = new ConfigHandler(Options.InputDirectory);

            SendCommand = new ParameterizedCommandHandler<PasswordBox>(SendAction);
            ExitCommand = new EmptyCommandHandler(ExitAction);
            SaveCommand = new ParameterizedCommandHandler<PasswordBox>(SaveAction);
            LoadCommand = new ParameterizedCommandHandler<PasswordBox>(LoadAction);
        }

        private void SaveAction(PasswordBox parameter)
        {
            Options.Password = parameter.Password;
            _configHandler.WriteConfig(Options.OptionsModel);
        }

        private void LoadAction(PasswordBox parameter)
        {
            var tempOptionsModel = _configHandler.ReadConfig();
            Options.Username = tempOptionsModel.Username;
            Options.InputDirectory = tempOptionsModel.InputDirectory;
            Options.IsGridEnabled = tempOptionsModel.IsGridEnabled;
            Options.MaximumRetries = tempOptionsModel.MaximumRetries;
            Options.HoursToSubstract = tempOptionsModel.HoursToSubstract;
            Options.SetDesktopStyleWithModel(tempOptionsModel.DesktopStyle);
            Options.Password = "";
            parameter.Password = tempOptionsModel.Password;
        }

        private void ExitAction()
        {
            Application.Current.Shutdown(0);
        }

        public void SendAction(PasswordBox parameter)
        {
            Options.Password = parameter.Password;
            var worker = new Worker(this.Options.OptionsModel);
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync(worker);
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (Worker) e.Argument;
            worker.DoWork();
        }
    }
}
