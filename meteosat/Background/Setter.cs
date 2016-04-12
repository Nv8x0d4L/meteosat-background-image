using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace meteosat.Background
{
    public class Setter
    {
        public const int SetDesktopWallpaper = 20;
        public const int UpdateIniFile = 0x01;
        public const int SendWinIniChange = 0x02;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public void SetWallpaper(string path, Style style)
        {
            SystemParametersInfo(SetDesktopWallpaper, 0, path, UpdateIniFile | SendWinIniChange);
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
            if (key != null)
            {
                switch (style)
                {
                    case Style.Stretch:
                        key.SetValue(@"WallpaperStyle", "2");
                        key.SetValue(@"TileWallpaper", "0");
                        break;
                    case Style.Center:
                        key.SetValue(@"WallpaperStyle", "1");
                        key.SetValue(@"TileWallpaper", "0");
                        break;
                    case Style.Tile:
                        key.SetValue(@"WallpaperStyle", "1");
                        key.SetValue(@"TileWallpaper", "1");
                        break;
                    case Style.Fit:
                        key.SetValue(@"WallpaperStyle", "6");
                        key.SetValue(@"TileWallpaper", "0");
                        break;
                    case Style.NoChange:
                        break;
                }
                key.Close();
            }
        }
    }
}
