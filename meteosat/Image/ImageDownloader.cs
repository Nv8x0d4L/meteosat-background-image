using System;
using System.Net;
using log4net;

namespace meteosat.Image
{
    public class ImageDownloader
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ImageDownloader));

        private const string RetryingText = "Retrying...";
        private const string QuittingText = "Quitting...";
        private const string SuccessFormat = "---\nURL: {0}\nStatus: success";
        private const string ErrorFormat = "---\nURL: {0}\nStatus: {1}\nResponse: {2}\nMessage: {3}";
        private const string UrlZeroFormat = "{0:0}";
        private const string UrlNoFillFormat = "{0:0}00";

        private CredentialCache GetCredential(string url, string username, string password)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            var credentialCache = new CredentialCache();
            credentialCache.Add(new System.Uri(url), "Basic", new NetworkCredential(username, password));
            return credentialCache;
        }

        public void SaveToFile(string username, string password, string imagePath, bool isGridEnabled, int maximumRetries, int hoursToSubstract)
        {
            using (var webClient = new WebClient())
            {
                var hasDownloadedFile = false;
                var numberOfRetries = 0;
                while (!hasDownloadedFile && numberOfRetries <= maximumRetries)
                {
                    var url = GetUrl(isGridEnabled, numberOfRetries, hoursToSubstract);
                    webClient.Credentials = GetCredential(url, username, password);
                    try
                    {
                        webClient.DownloadFile(url, imagePath);
                        hasDownloadedFile = true;
                        Logger.Info(String.Format(SuccessFormat, url));
                    }
                    catch (WebException webException)
                    {
                        Logger.Warn(String.Format(ErrorFormat, url, webException.Status, webException.Response, webException.Message));
                        ++numberOfRetries;
                        Logger.Info(numberOfRetries <= maximumRetries ? RetryingText : QuittingText);
                    }

                }
            }
        }

        private string GetUrl(bool isGridEnabled, int numberOfRetries, int hoursToSubstract)
        {
            var currentDatetime = DateTime.UtcNow;
            var usedDateTime = currentDatetime.Subtract(TimeSpan.FromHours(hoursToSubstract));
            usedDateTime = usedDateTime.Subtract(TimeSpan.FromHours(numberOfRetries * 3));
            var fixedHour = ((int) (usedDateTime.Hour/3))*3;
            var lastImageHourAvailable = String.Format(fixedHour == 0 ? UrlZeroFormat : UrlNoFillFormat, fixedHour);
            var gridTextForUrl = isGridEnabled ? "_grid" : "";
            return String.Format(
                    "http://www.sat.dundee.ac.uk/xrit/000.0E/MSG/{0}/{1}/{2}/{3}/{0}_{1}_{2}_{3}_MSG3_16_S2{4}.jpeg",
                    currentDatetime.Year, currentDatetime.Month, currentDatetime.Day, lastImageHourAvailable,
                    gridTextForUrl);
        }
    }
}
