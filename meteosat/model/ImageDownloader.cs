using System;
using System.Net;
using log4net;
using meteosat.util;

namespace meteosat.model
{
    public class ImageDownloader
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ImageDownloader));
        
        private static readonly string RetryingText = DefaultValues.GetString("RetryingText");
        private static readonly string QuittingText = DefaultValues.GetString("QuittingText");
        private static readonly string SuccessFormat = DefaultValues.GetString("SuccessFormat");
        private static readonly string ErrorFormat = DefaultValues.GetString("ErrorFormat");
        private static readonly string UrlZeroFormat = DefaultValues.GetString("UrlZeroFormat");
        private static readonly string UrlNoFillFormat = DefaultValues.GetString("UrlNoFillFormat");
        private static readonly string GridTextForUrl = DefaultValues.GetString("GridTextForUrl");
        private static readonly string AuthType = DefaultValues.GetString("AuthType");
        private static readonly string MeteosatURL = DefaultValues.GetString("MeteosatURL");

        private CredentialCache GetCredential(string url, string username, string password)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            var credentialCache = new CredentialCache();
            credentialCache.Add(new System.Uri(url), AuthType, new NetworkCredential(username, password));
            return credentialCache;
        }

        public void SaveToFile(string username, string password, string imagePath, bool isGridEnabled,
            int maximumRetries, int hoursToSubtract)
        {
            using (var webClient = new WebClient())
            {
                var hasDownloadedFile = false;
                var numberOfRetries = 0;
                while (!hasDownloadedFile && numberOfRetries <= maximumRetries)
                {
                    var url = GetUrl(isGridEnabled, numberOfRetries, hoursToSubtract);
                    webClient.Credentials = GetCredential(url, username, password);
                    try
                    {
                        webClient.DownloadFile(url, imagePath);
                        hasDownloadedFile = true;
                        Logger.Info(string.Format(SuccessFormat, url));
                    }
                    catch (WebException webException)
                    {
                        Logger.Warn(string.Format(ErrorFormat, url, webException.Status, webException.Response,
                            webException.Message));
                        ++numberOfRetries;
                        Logger.Info(numberOfRetries <= maximumRetries ? RetryingText : QuittingText);
                    }

                }
            }
        }

        private string GetUrl(bool isGridEnabled, int numberOfRetries, int hoursToSubtract)
        {
            var usedDateTime = DateTime.UtcNow.Subtract(TimeSpan.FromHours(hoursToSubtract));
            usedDateTime = usedDateTime.Subtract(TimeSpan.FromHours(numberOfRetries*3));
            var fixedHour = ((int) (usedDateTime.Hour/3))*3;
            var lastImageHourAvailable = string.Format(fixedHour == 0 ? UrlZeroFormat : UrlNoFillFormat, fixedHour);
            var gridTextForUrl = isGridEnabled ? GridTextForUrl : string.Empty;
            return string.Format(MeteosatURL, usedDateTime.Year, usedDateTime.Month, usedDateTime.Day,
                lastImageHourAvailable, gridTextForUrl);
        }
    }
}