/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Model utilisé pour récupérer les données depuis une source FTP.
 */

using P_Thesaurus.AppBusiness.HistoryReader;

namespace P_Thesaurus.Models
{
    /// <summary>
    /// Model used to connect to and get datas from FTP
    /// </summary>
    public class FtpModel
    {
        #region Variables
        private History<FtpHistoryEntry> _history;
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FtpModel()
        {

        }
        #endregion
    }
}
