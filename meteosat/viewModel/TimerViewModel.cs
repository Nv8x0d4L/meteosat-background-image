namespace meteosat.viewModel
{
    public class TimerViewModel
    {
        private const string ConfigurationTextInterval = "DefaultTimerInterval";
        public int Interval { get; set; }

        public TimerViewModel()
        {
            Interval = DefaultValues.GetInteger(ConfigurationTextInterval);
        }
    }
}
