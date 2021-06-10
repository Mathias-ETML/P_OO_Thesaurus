/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Model utilisé pour récupérer les données depuis un site Web.
 */

using P_Thesaurus.AppBusiness.EnumsAndStructs;
using P_Thesaurus.AppBusiness.HistoryReader;
using P_Thesaurus.AppBusiness.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace P_Thesaurus.Models
{
    /// <summary>
    /// Model used to get datas from a website
    /// </summary>
    public class WebModel
    {
        #region Variables
        public const string DEFAULT_WEB_HISTORY_PATH = ".\\web_history.txt";
        private History<HistoryEntry> _history;
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public WebModel()
        {
            this._history = new History<HistoryEntry>(DEFAULT_WEB_HISTORY_PATH, true);
        }

        /// <summary>
        /// Get all web elements in an url. Make sure to check the url before using this method, else it will crash
        /// </summary>
        /// <param name="url">the url to get elements from</param>
        /// <returns> all the links and images in the page</returns>
        public List<WebElement> GetWebElements(string url)
        {
            url = CheckUrlStart(url);

            string code = GetSourceCode(url);
            List<WebElement> toReturn = new List<WebElement>();
            string sourceUrl;

            //contains temp variables for calculations
            {
                string[] urlSplitted = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                sourceUrl = urlSplitted[0] + "//" + urlSplitted[1];
            }

            //get all hrefs in the page
            string[] hrefSplitted = code.Split(new string[] { "href=\"" }, StringSplitOptions.RemoveEmptyEntries);

            //filters the results of the href split
            for (int i = 1; i < hrefSplitted.Length; i++)
            {
                hrefSplitted[i] = hrefSplitted[i].Split('"')[0];

                //check if the file is a relative or a normal path
                if (hrefSplitted[i].StartsWith("/"))
                {
                    hrefSplitted[i] = sourceUrl + hrefSplitted[i];
                }

                if ((hrefSplitted[i].StartsWith("https://") || hrefSplitted[i].StartsWith("http://")) && !hrefSplitted[i].Contains(".css"))
                {
                    toReturn.Add(new WebElement() { Link = hrefSplitted[i], Type = WebElementType.Link });

                    Debug.WriteLine(hrefSplitted[i]);
                }
            }

            //get all srcs in the page
            string[] srcSplitted = code.Split(new string[] { "src=\"" }, StringSplitOptions.RemoveEmptyEntries);

            //filter the src's split
            for (int i = 0; i < srcSplitted.Length; i++)
            {
                srcSplitted[i] = srcSplitted[i].Split('"')[0];

                //Check if file is an image
                if (srcSplitted[i].EndsWith(".png") ||
                   srcSplitted[i].EndsWith(".jpg") ||
                   srcSplitted[i].EndsWith(".gif"))
                {
                    //check if the file is a relative or a normal path
                    if (srcSplitted[i].StartsWith("/"))
                    {
                        srcSplitted[i] = sourceUrl + srcSplitted[i];
                    }

                    toReturn.Add(new WebElement() { Link = srcSplitted[i], Type = WebElementType.Image });

                    Debug.WriteLine(srcSplitted[i]);
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Put https and www before the start of the url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string CheckUrlStart(string url)
        {
            //Deal with the "https://" prefix
            if (!(url.StartsWith("https://") || url.StartsWith("http://")))
            {
                if (!url.StartsWith("www."))
                {
                    url = url.Insert(0, "www.");
                }

                url = url.Insert(0, "https://");
            }

            return url;
        }

        /// <summary>
        /// Test an url to know if it exists. Call this method before reading the link
        /// </summary>
        /// <param name="url">the URL to test</param>
        /// <returns>true if the url is valid, else false</returns>
        public bool TestURL(string url)
        {
            url = CheckUrlStart(url);
            StreamReader sr;

            try
            {
                sr = new StreamReader(new WebClient().OpenRead(url));
            }
            catch(Exception e)
            {
                Log.GetInstance().AddLog(LogsLevels.INFO, $"The test of the url \"{url}\" failed. Exception : {e.Message}");
                return false;
            }

            sr.Close();

            return true;
        }

        /// <summary>
        /// Get the source code from a page.
        /// Helped by Santiago Sugrañes
        /// </summary>
        /// <param name="url">the web page Url</param>
        /// <returns>the web page in string</returns>
        private string GetSourceCode(string url)
        {
            WebClient webClient = new WebClient();

            StreamReader sr;

            // try to get the page
            try
            {
                Stream webStream = webClient.OpenRead(url);
                sr = new StreamReader( webStream );
            }
            catch (Exception e)
            {
                Log.GetInstance().AddLog(LogsLevels.ERROR, "Error trying to get response from the web : " + e.Message);
                return null;
            }

            // get the page
            string result = sr.ReadToEnd();
            sr.Close();
            webClient.Dispose();

            return result;
        }

        /// <summary>
        /// GetHistory function
        /// </summary>
        /// <returns>list on histories entries</returns>
        public List<HistoryEntry> GetHistory()
        {
            return _history.Read();
        }

        /// <summary>
        /// Write in history function
        /// </summary>
        /// <param name="url">full path</param>
        public void WriteInHistory(string url)
        {
            HistoryEntry entry = new HistoryEntry()
            {
                Content = url,
                DateTime = DateTime.Now
            };

            _history.AddEntry(entry);
        }

        #endregion
    }
}
