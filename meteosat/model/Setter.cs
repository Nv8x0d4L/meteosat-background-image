using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace meteosat.model
{
    public class Setter
    {
        public const int SetDesktopWallpaper = 20;
        public const int UpdateIniFile = 0x01;
        public const int SendWinIniChange = 0x02;
        private const string Wallpaperstyle = @"WallpaperStyle";
        private const string Tilewallpaper = @"TileWallpaper";
        private const string WallpaperstyleStrechValue = "2";
        private const string WallpaperstyleValueCenter = "1";
        private const string WallpaperstyleValueTile = "1";
        private const string WallpaperstyleValueFit = "6";
        private const string TilewallpaperValueFalse = "0";
        private const string TilewallpaperValueTrue = "1";
        private const string WallpaperRegistryKey = @"Control Panel\Desktop";
        private const string User32Dll = "user32.dll";

        [DllImport(User32Dll, SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public void SetWallpaper(string path, Style style)
        {
            SystemParametersInfo(SetDesktopWallpaper, 0, path, UpdateIniFile | SendWinIniChange);
            RegistryKey key = Registry.CurrentUser.OpenSubKey(WallpaperRegistryKey, true);
            if (key != null)
            {
                switch (style)
                {
                    case Style.Stretch:
                        key.SetValue(Wallpaperstyle, WallpaperstyleStrechValue);
                        key.SetValue(Tilewallpaper, TilewallpaperValueFalse);
                        break;
                    case Style.Center:
                        key.SetValue(Wallpaperstyle, WallpaperstyleValueCenter);
                        key.SetValue(Tilewallpaper, TilewallpaperValueFalse);
                        break;
                    case Style.Tile:
                        key.SetValue(Wallpaperstyle, WallpaperstyleValueTile);
                        key.SetValue(Tilewallpaper, TilewallpaperValueTrue);
                        break;
                    case Style.Fit:
                        key.SetValue(Wallpaperstyle, WallpaperstyleValueFit);
                        key.SetValue(Tilewallpaper, TilewallpaperValueFalse);
                        break;
                    case Style.NoChange:
                        break;
                }
                key.Close();
            }
        }
    }
}
