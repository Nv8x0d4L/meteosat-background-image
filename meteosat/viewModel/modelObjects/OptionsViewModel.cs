using System.ComponentModel;
using System.Runtime.CompilerServices;
using meteosat.Annotations;
using meteosat.model;
using meteosat.util;

namespace meteosat.viewModel.modelObjects
{
    public class OptionsViewModel : INotifyPropertyChanged
    {
        public Options OptionsModel { get; private set; }

        private const string ConfigurationTextUsername = "DefaultOptionUsername";
        public string Username
        {
            get { return OptionsModel.Username; }
            set
            {
                if (value == OptionsModel.Username) return;
                OptionsModel.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private const string ConfigurationTextPassword = "DefaultOptionPassword";
        public string Password
        {
            get { return OptionsModel.Password; }
            set
            {
                if (value == OptionsModel.Password) return;
                OptionsModel.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string InputDirectory
        {
            get { return OptionsModel.InputDirectory; }
            set
            {
                if (value == OptionsModel.InputDirectory) return;
                OptionsModel.InputDirectory = value;
                OnPropertyChanged(nameof(InputDirectory));
            }
        }

        private const string ConfigurationTextIsGridEnabled = "DefaultOptionIsGridEnabled";
        public bool IsGridEnabled
        {
            get { return OptionsModel.IsGridEnabled; }
            set
            {
                if (value == OptionsModel.IsGridEnabled) return;
                OptionsModel.IsGridEnabled = value;
                OnPropertyChanged(nameof(IsGridEnabled));
            }
        }

        private const string ConfigurationTextMaximumRetries = "DefaultOptionMaximumRetries";
        public int MaximumRetries
        {
            get { return OptionsModel.MaximumRetries; }
            set
            {
                if (value == OptionsModel.MaximumRetries) return;
                OptionsModel.MaximumRetries = value;
                OnPropertyChanged(nameof(MaximumRetries));
            }
        }

        private const string ConfigurationTextDesktopStyle = "DefaultOptionDesktopStyle";
        public StyleViewModel DesktopStyle
        {
            get
            {
                return StyleConverter.ModelToViewModel(OptionsModel.DesktopStyle);
            }
            set
            {
                var newValue = StyleConverter.ViewModelToModel(value);
                if (newValue == OptionsModel.DesktopStyle) return;
                OptionsModel.DesktopStyle = newValue;
                OnPropertyChanged(nameof(DesktopStyle));
            }
        }

        public void SetDesktopStyleWithModel(Style model)
        {
            if (model == OptionsModel.DesktopStyle) return;
            OptionsModel.DesktopStyle = model;
            OnPropertyChanged(nameof(DesktopStyle));
        }

        private const string ConfigurationTextHoursToSubtract = "DefaultOptionHoursToSubtract";
        public int HoursToSubtract
        {
            get { return OptionsModel.HoursToSubtract; }
            set
            {
                if (value == OptionsModel.HoursToSubtract) return;
                OptionsModel.HoursToSubtract = value;
                OnPropertyChanged(nameof(HoursToSubtract));
            }
        }


        public OptionsViewModel(string baseDirectory)
        {
            OptionsModel = new Options();
            Username = DefaultValues.GetString(ConfigurationTextUsername);
            Password = DefaultValues.GetString(ConfigurationTextPassword);
            InputDirectory = baseDirectory;
            IsGridEnabled = bool.Parse(DefaultValues.GetString(ConfigurationTextIsGridEnabled));
            MaximumRetries = DefaultValues.GetInteger(ConfigurationTextMaximumRetries);
            DesktopStyle = DefaultValues.GetStyleViewModel(ConfigurationTextDesktopStyle);
            HoursToSubtract = DefaultValues.GetInteger(ConfigurationTextHoursToSubtract);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
