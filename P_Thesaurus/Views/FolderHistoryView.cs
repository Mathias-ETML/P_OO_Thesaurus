using P_Thesaurus.Controllers;

/*
 * ETML
 * Clément Sartoni
 * 23.04.2021
 * Projet P_OO-Smart-Thésaurus
 * Form permettant d'afficher l'historique des connexions FTP
 */


namespace P_Thesaurus.Views
{
    /// <summary>
    /// Form permettant d'afficher l'historique des connexions FTP
    /// </summary>
    public partial class FolderHistoryView : HistoryView
    {
        public FolderController Controller { get; protected set; }

        public FolderHistoryView()
        {
            InitializeComponent();
        }
    }
}
