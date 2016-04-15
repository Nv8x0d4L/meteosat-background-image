using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using meteosat.Annotations;
using meteosat.Background;

namespace meteosat
{
    public class Options : INotifyPropertyChanged
    {
        private string _username;
        public string Username
        {
            get { return this._username; }
            set
            {
                if (value != this._username)
                {
                    this._username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private string _inputDirectory;
        public string InputDirectory
        {
            get { return _inputDirectory; }
            set
            {
                if (value != _inputDirectory)
                {
                    _inputDirectory = value;
                    OnPropertyChanged(nameof(InputDirectory));
                }
            }
        }


        private bool _isGridEnabled;
        public bool IsGridEnabled
        {
            get { return _isGridEnabled; }
            set
            {
                if (value != _isGridEnabled)
                {
                    _isGridEnabled = value;
                    OnPropertyChanged(nameof(IsGridEnabled));
                }
            }
        }



        private int _maximumRetries;
        public int MaximumRetries
        {
            get { return _maximumRetries; }
            set
            {
                if (value != _maximumRetries)
                {
                    _maximumRetries = value;
                    OnPropertyChanged(nameof(MaximumRetries));
                }
            }
        }

        private Style _desktopStyle;
        public Style DesktopStyle
        {
            get { return _desktopStyle; }
            set
            {
                if (value != _desktopStyle)
                {
                    _desktopStyle = value;
                    OnPropertyChanged(nameof(DesktopStyle));
                }
            }
        }


        private int _hoursToSubstract;
        public int HoursToSubstract
        {
            get { return _hoursToSubstract; }
            set
            {
                if (value != _hoursToSubstract)
                {
                    _hoursToSubstract = value;
                    OnPropertyChanged(nameof(HoursToSubstract));
                }
            }
        }

        public Options(string baseDirectory)
        {
            this.Username = "qwerty";
            InputDirectory = baseDirectory;
            IsGridEnabled = false;
            MaximumRetries = 5;
            DesktopStyle = Style.Fit;
            HoursToSubstract = 0;

            Password = "asdf";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
