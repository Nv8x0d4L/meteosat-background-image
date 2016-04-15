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
        public string Username { get; set; }
        public SecureString Password { get; set; }
        public string InputDirectory { get; set; }
        public bool IsGridEnabled { get; set; }
        public int MaximumRetries { get; set; }
        public Style DesktopStyle { get; set; }
        public int HoursToSubstract { get; set; }

        public Options(string baseDirectory)
        {
            this.Username = "qwerty";
            InputDirectory = baseDirectory;
            IsGridEnabled = false;
            MaximumRetries = 5;
            DesktopStyle = Style.Fit;
            HoursToSubstract = 0;

            Password = new SecureString();
            Password.AppendChar('a');
            Password.AppendChar('s');
            Password.AppendChar('d');
            Password.AppendChar('f');
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
