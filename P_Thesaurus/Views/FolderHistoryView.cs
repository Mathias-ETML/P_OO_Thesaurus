/*
 * ETML
 * Clément Sartoni
 * 23.04.2021
 * Projet P_OO-Smart-Thésaurus
 * Form permettant d'afficher l'historique des connexions FTP
 */


using P_Thesaurus.Controllers;

namespace P_Thesaurus.Views
{
    /// <summary>
    /// Form permettant d'afficher l'historique des connexions FTP
    /// </summary>
    public partial class FolderHistoryView : HistoryView
    {
        #region Variables
        /// <summary>
        /// This view's controller
        /// </summary>
        public FolderController Controller { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FolderHistoryView()
        {
            InitializeComponent();

        }
        #endregion
    }
}
