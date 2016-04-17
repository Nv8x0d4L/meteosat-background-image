using System.ComponentModel;
using System.Runtime.CompilerServices;
using meteosat.Annotations;
using meteosat.model;

namespace meteosat.viewModel.modelObjects
{
    public class OptionsViewModel : INotifyPropertyChanged
    {
        public Options OptionsModel { get; private set; }

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

        public int HoursToSubstract
        {
            get { return OptionsModel.HoursToSubstract; }
            set
            {
                if (value == OptionsModel.HoursToSubstract) return;
                OptionsModel.HoursToSubstract = value;
                OnPropertyChanged(nameof(HoursToSubstract));
            }
        }

        public OptionsViewModel(string baseDirectory)
        {
            OptionsModel = new Options();
            Username = "qwerty";
            Password = "asdf";
            InputDirectory = baseDirectory;
            IsGridEnabled = false;
            MaximumRetries = 5;
            DesktopStyle = StyleViewModel.Fit;
            HoursToSubstract = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
