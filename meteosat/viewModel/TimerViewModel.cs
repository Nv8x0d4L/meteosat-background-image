using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using meteosat.Annotations;
using meteosat.util;

namespace meteosat.viewModel
{
    public class TimerViewModel : INotifyPropertyChanged
    {
        private const string ConfigurationTextInterval = "DefaultTimerInterval";
        private int _interval;
        public int Interval
        {
            get { return _interval; }
            set
            {

                if (value == _interval) return;
                _interval = value;
                RefreshDispatcherTimerIntervall();
                OnPropertyChanged(nameof(Interval));
            }
        }

        private void RefreshDispatcherTimerIntervall()
        {
            switch (IntervalIn)
            {
                case IntervallMeasurement.Hours:
                    dispatcherTimer.Interval = new TimeSpan(Interval, 0, 0);
                    break;
                case IntervallMeasurement.Minutes:
                    dispatcherTimer.Interval = new TimeSpan(0, Interval, 0);
                    break;
                case IntervallMeasurement.Seconds:
                    dispatcherTimer.Interval = new TimeSpan(0, 0, Interval);
                    break;
                default:
                    dispatcherTimer.Interval = new TimeSpan(0, Interval, 0);
                    break;
            }
        }

        private IntervallMeasurement _intervallMeasurement;
        public IntervallMeasurement IntervalIn {
            get { return _intervallMeasurement; }
            set
            {
                if (value == _intervallMeasurement) return;
                _intervallMeasurement = value;
                RefreshDispatcherTimerIntervall();
                OnPropertyChanged(nameof(IntervalIn));
            }
        }

        private const string ConfigurationTextDefaultTextNoDate = "DefaultTimerDefaultTextNoDate";
        private static readonly string NextRunFormat = DefaultValues.GetString("NextRunFormat");
        private string _defaultTextNoDate;
        public string DefaultTextNoDate
        {
            get { return _defaultTextNoDate; }
            set
            {
                if (value == _defaultTextNoDate) return;
                _defaultTextNoDate = value;
                OnPropertyChanged(nameof(DefaultTextNoDate));
            }
        }


        private DispatcherTimer dispatcherTimer { get; set; }

        private DateTime? _lastRun;
        public DateTime? LastRun
        {
            get {
                return _lastRun;
            }
            set
            {
                if (value == _lastRun) return;
                _lastRun = value;
                OnPropertyChanged(nameof(LastRun));
                OnPropertyChanged(nameof(LastRunString));
            }
        }

        public string LastRunString => LastRun?.ToString() ?? DefaultTextNoDate;


        private DateTime? _nextRun;
        public DateTime? NextRun
        {
            get
            {
                return _nextRun;
            }
            set
            {
                if (value == NextRun) return;
                _nextRun = value;
                OnPropertyChanged(nameof(NextRun));
                OnPropertyChanged(nameof(NextRunString));
                OnPropertyChanged(nameof(NextRunInString));
            }
        }

        public string NextRunString => NextRun?.ToString() ?? DefaultTextNoDate;

        private string _nextRunInString;
        public string NextRunInString
        {
            get { return _nextRunInString; }
            set
            {
                if (value == _nextRunInString) return;
                _nextRunInString = value;
                OnPropertyChanged(nameof(NextRunInString));
            }
        }

        public TimerViewModel()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            IntervalIn = IntervallMeasurement.Minutes;
            Interval = DefaultValues.GetInteger(ConfigurationTextInterval);
            LastRun = null;
            NextRun = null;
            DefaultTextNoDate = DefaultValues.GetString(ConfigurationTextDefaultTextNoDate);

            var nextRunInTimer = new DispatcherTimer();
            nextRunInTimer.Interval = new TimeSpan(0, 0, 1);
            nextRunInTimer.Tick += (s, e) => RefreshNextRunIn();
            nextRunInTimer.Start();
        }

        private void RefreshNextRunIn()
        {
            if (NextRun.HasValue)
            {
                var nextRunIn = NextRun.Value - DateTime.Now;
                NextRunInString = string.Format(NextRunFormat, nextRunIn.Hours, nextRunIn.Minutes,
                    nextRunIn.Seconds);
            }
            else
            {
                NextRunInString = DefaultTextNoDate;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetRunToNow()
        {
            LastRun = DateTime.Now;
        }

        public void Stop()
        {
            dispatcherTimer.Stop();
            NextRun = null;
            RefreshNextRunIn();
        }

        public void Start()
        {
            dispatcherTimer.Start();
            RefreshNextTexts();
        }

        private void RefreshNextTexts()
        {
            RefreshNextRun();
            RefreshNextRunIn();
        }

        private void RefreshNextRun()
        {
            NextRun = DateTime.Now.Add(dispatcherTimer.Interval);
        }

        public bool IsEnabled => dispatcherTimer.IsEnabled;
        public event EventHandler Tick;

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            RefreshNextTexts();
            Tick?.Invoke(sender, e);
        }
    }
}
