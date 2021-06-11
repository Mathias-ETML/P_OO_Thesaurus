/*
 * ETML
 * Clément Sartoni
 * 23.04.2021
 * Projet P_OO-Smart-Thésaurus
 * Form permettant d'afficher l'historique des connexions FTP
 */

using P_Thesaurus.Controllers;
using System;
using System.Windows.Forms;

namespace P_Thesaurus.Views
{
    /// <summary>
    /// Form permettant d'afficher l'historique des connexions FTP
    /// </summary>
    public partial class WebHistoryView : HistoryView
    {
        #region Variables
        /// <summary>
        /// This view's controller
        /// </summary>
        public WebController Controller { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public WebHistoryView()
        {
            InitializeComponent();

            PathLabel.Text = "Url";

            btnOpenFolder.Click += new EventHandler(Button2Click);
        }

        public void Init()
        {
            SetHistory(Controller.GetHistory());
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// send the link to the controller when the "OK" button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2Click(object sender, EventArgs e)
        {
            Controller.TestUrl(txtBoxPath.Text);
        }

        /// <summary>
        /// Occure when the user click on a object of the history
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDriveSelectionHistory(object sender, EventArgs e)
        {
            if(historyListView.SelectedItems.Count > 0)
            {
                string selected = historyListView.SelectedItems[0].Text;

                if (selected != null)
                {
                    // we are passing the path trough the node name, wich is a simple way if giving wich drive or folder the user wants
                    Controller.TestUrl(selected);
                }
            }
        }

        #endregion

    }
}
