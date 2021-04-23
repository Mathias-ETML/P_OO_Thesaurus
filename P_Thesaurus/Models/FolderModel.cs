/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Model utilisé pour récupérer les données depuis des fichiers.
 */

using P_Thesaurus.AppBusiness.HistoryReader;

namespace P_Thesaurus.Models
{
    /// <summary>
    /// Model used to get datas from the windows folders
    /// </summary>
    public class FolderModel
    {
        #region Variables
        private History<HistoryEntry> _history;
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FolderModel()
        {

        }
        #endregion
    }
}
