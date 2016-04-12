using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace meteosat
{
    class ImageDownloader
    {
        private CredentialCache GetCredential(string url, string username, string password)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            var credentialCache = new CredentialCache();
            credentialCache.Add(new System.Uri(url), "Basic", new NetworkCredential(username, password));
            return credentialCache;
        }

        public void SaveToFile(string username, string password, string path, bool isGridEnabled, int maxRetries)
        {
            using (var webClient = new WebClient())
            {
                var hasDownloadedFile = false;
                var numberOfRetries = 0;
                while (!hasDownloadedFile && numberOfRetries <= maxRetries)
                {
                    var url = GetUrl(isGridEnabled, numberOfRetries);
                    webClient.Credentials = GetCredential(url, username, password);
                    try
                    {
                        webClient.DownloadFile(url, path);
                        hasDownloadedFile = true;
                        Console.Out.WriteLine("---\nURL:{0}Status: success", url);
                    }
                    catch (WebException webException)
                    {
                        Console.Out.WriteLine("---\nURL:{0}Status: {1}\nResponse: {2}\nMessage: {3}", url, 
                            webException.Status, webException.Response, webException.Message);
                        ++numberOfRetries;
                        if (numberOfRetries <= maxRetries)
                        {
                            Console.Out.WriteLine("Retrying...");
                        }
                        else
                        {
                            Console.Out.WriteLine("Quitting.");
                        }
                    }

                }
            }
        }

        private string GetUrl(bool isGridEnabled, int retries = 0)
        {
            var currentDatetime = DateTime.UtcNow;
            var usedDateTime = currentDatetime.Subtract(TimeSpan.FromHours(retries * 3));
            var lastImageHourAvailable = String.Format("{0:0}00", ((int)(usedDateTime.Hour / 3)) * 3);
            var gridTextForUrl = isGridEnabled ? "_grid" : "";
            return String.Format(
                    "http://www.sat.dundee.ac.uk/xrit/000.0E/MSG/{0}/{1}/{2}/{3}/{0}_{1}_{2}_{3}_MSG3_16_S2{4}.jpeg",
                    currentDatetime.Year, currentDatetime.Month, currentDatetime.Day, lastImageHourAvailable,
                    gridTextForUrl);
        }
    }
}
