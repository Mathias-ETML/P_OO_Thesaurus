/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Classe Singleton permettant de log des message dans un fichier txt.
 */

using P_Thesaurus.AppBusiness.EnumsAndStructs;
using System;
using System.Diagnostics;
using System.IO;

namespace P_Thesaurus.AppBusiness
{
    /// <summary>
    /// Singleton Logger. Default, log in a "Logs.txt" file next to the .exe file.
    /// </summary>
    public class Log
    {
        #region Variables

        /// <summary>
        /// Singleton instance
        /// </summary>
        private static Log _instance;

        /// <summary>
        /// The log's file path. Default : ".\Logs.txt"
        /// </summary>
        public string Path { get; set; }

        #endregion
        #region Methods
        /// <summary>
        /// default private constructor
        /// </summary>
        private Log()
        {
            Path = ".\\Logs.txt";
        }

        /// <summary>
        /// Singleton method to get the logger's instance
        /// </summary>
        /// <returns>The current instance of the Log class</returns>
        public static Log GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Log();
            }
            return _instance;
        }

        /// <summary>
        /// Format and add a log in the log file.
        /// </summary>
        /// <param name="level">the log level of the log/param>
        /// <param name="message">the message to log</param>
        /// <returns>true if the adding of succeed , else false</returns>
        public bool AddLog(LogsLevels level, string message)
        {
            try
            {
                using(StreamWriter writer = File.AppendText(Path))
                {
                    writer.WriteLine($"[{DateTime.Now.ToString("G")}][{level}][{message}]");
                }
            }
            catch(IOException e)
            {
                Debug.WriteLine("There was an error with the logs : " + e.ToString());
                return false;
            }
            return true;
        }

        #endregion
    }
}
