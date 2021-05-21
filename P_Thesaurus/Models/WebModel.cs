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
        private History<HistoryEntry> _history;
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public WebModel()
        {

        }

        /// <summary>
        /// Get all web elements in an url. Make sure to check the url before using this method, else it will crash
        /// </summary>
        /// <param name="url">the url to get elements from</param>
        /// <returns>all the links and images in the page</returns>
        public List<WebElement> GetWebElements(string url)
        {
            string code = GetSourceCode(url);
            List<WebElement> toReturn = new List<WebElement>();

            string[] hrefSplitted = code.Split(new string[] { "href=\"" }, StringSplitOptions.RemoveEmptyEntries);

            for(int i = 0; i < hrefSplitted.Length; i++)
            {
                hrefSplitted[i] = hrefSplitted[i].Split('"')[0];

                toReturn.Add(new WebElement() { link = hrefSplitted[i], type = WebElementType.Link});
            }

            string[] srcSplitted = code.Split(new string[] { "src=\"" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < srcSplitted.Length; i++)
            {
                srcSplitted[i] = srcSplitted[i].Split('"')[0];

                //Check if file is an image
                if(srcSplitted[i].EndsWith(".png") ||
                   srcSplitted[i].EndsWith(".jpg") ||
                   srcSplitted[i].EndsWith(".gif") )
                {
                    toReturn.Add(new WebElement() { link = srcSplitted[i], type = WebElementType.Image });
                }

            }

            return toReturn;

        }

        /// <summary>
        /// Test an url to know if it exists. Call this method before reading the link
        /// </summary>
        /// <param name="url">the URL to test</param>
        /// <returns>true if the url is valid, else false</returns>
        public bool TestURL(string url)
        {
            try
            {
                new WebClient().OpenRead(url);
            }
            catch(Exception e)
            {
                Log.GetInstance().AddLog(LogsLevels.INFO, $"The test of the url \"{url}\" failed. Exception : {e.Message}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get the source code from a page.
        /// Helped by Santiago Sugrañes
        /// </summary>
        /// <param name="url">the web page Url</param>
        /// <returns></returns>
        private string GetSourceCode(string url)
        {
            //Deal with the "https://" prefix
            if( ! ( url.StartsWith("https://") || url.StartsWith("http://") ) )
            {
                url = url.Insert(0, "https://");
            }

            WebClient webClient = new WebClient();

            StreamReader sr;

            try
            {
                sr = new StreamReader( webClient.OpenRead(url) );
            }
            catch (Exception e)
            {
                Log.GetInstance().AddLog(LogsLevels.ERROR, "Error trying to get response from the web : " + e.Message);
                return null;
            }

            string result = sr.ReadToEnd();
            sr.Close();
            webClient.Dispose();

            return result;

        }


        #endregion
    }
}
