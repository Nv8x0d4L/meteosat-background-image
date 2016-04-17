namespace meteosat.model
{
    public class Options
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string InputDirectory { get; set; }
        public bool IsGridEnabled { get; set; }
        public int MaximumRetries { get; set; }
        public Style DesktopStyle { get; set; }
        public int HoursToSubstract { get; set; }
    }
}
