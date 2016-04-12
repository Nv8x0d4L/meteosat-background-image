using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace meteosat
{
    internal class Options
    {
        [Option('u', "username", Required = true,
            HelpText = "Specifies the username.")]
        public string Username { get; set; }

        [Option('p', "password", Required = true,
            HelpText = "Specifies the password.")]
        public string Password { get; set; }

        [Option('t', "temp-dir", Required = false,
            DefaultValue = "C:\\Temp",
            HelpText = "The directory to temporarily store the image.")]
        public string ImagePath { get; set; }

        [Option('f', "file-name", Required = false,
            DefaultValue = "meteosat.jpg",
            HelpText = "The name for the temporary file.")]
        public string FileName { get; set; }

        [Option('g', "enable-grid", Required = false,
            DefaultValue = false,
            HelpText = "If this option is present, then an image with grid will be used.")]
        public bool IsGridEnabled { get; set; }

        [Option('r', "maximum-retries", Required = false,
            DefaultValue = 5,
            HelpText = "Number of Attempts to download images, including earlier hours.")]
        public int MaximumRetries { get; set; }
        
        [Option('s', "desktop-style", Required = false,
            DefaultValue = 3,
            HelpText = "Desktop style for the wallpaper to use.")]
        public int DesktopStyle { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
