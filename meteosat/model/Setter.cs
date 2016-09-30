using System.Runtime.InteropServices;
using meteosat.util;
using Microsoft.Win32;

namespace meteosat.model
{
    public class Setter
    {
        private static readonly int SetDesktopWallpaper = DefaultValues.GetInteger("SetDesktopWallpaper");
        private static readonly int UpdateIniFile = DefaultValues.GetInteger("UpdateIniFile");
        private static readonly int SendWinIniChange = DefaultValues.GetInteger("SendWinIniChange");
        private static readonly int UParam = DefaultValues.GetInteger("UParam");

        private static readonly string Wallpaperstyle = DefaultValues.GetString("Wallpaperstyle");
        private static readonly string Tilewallpaper = DefaultValues.GetString("Tilewallpaper");
        private static readonly string WallpaperstyleStrechValue = DefaultValues.GetString("WallpaperstyleStrechValue");
        private static readonly string WallpaperstyleValueCenter = DefaultValues.GetString("WallpaperstyleValueCenter");
        private static readonly string WallpaperstyleValueTile = DefaultValues.GetString("WallpaperstyleValueTile");
        private static readonly string WallpaperstyleValueFit = DefaultValues.GetString("WallpaperstyleValueFit");
        private static readonly string TilewallpaperValueFalse = DefaultValues.GetString("TilewallpaperValueFalse");
        private static readonly string TilewallpaperValueTrue = DefaultValues.GetString("TilewallpaperValueTrue");
        private static readonly string WallpaperRegistryKey = DefaultValues.GetString("WallpaperRegistryKey");
        private const string User32Dll = "user32.dll";

        [DllImport(User32Dll, SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public void SetWallpaper(string path, Style style)
        {
            SystemParametersInfo(SetDesktopWallpaper, UParam, path, UpdateIniFile | SendWinIniChange);
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
