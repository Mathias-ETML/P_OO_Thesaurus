/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Form d'héritage représentant toutes les vues gérant l'affichage de l'historique et la séléction d'un endroit à indexer
 */
using P_Thesaurus.AppBusiness.HistoryReader;
using P_Thesaurus.Controllers;
using System.Collections.Generic;
using System.Windows.Forms;

namespace P_Thesaurus.Views
{
    public partial class HistoryView : BaseView
    {
        #region Variables
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// 
        public HistoryView()
        {
            InitializeComponent();

            historyListView.AllowColumnReorder = true;
            historyListView.FullRowSelect = true;
            historyListView.MultiSelect = false;
            historyListView.View = View.Details;


        }

        #endregion

        #region Private Form Methods
        /// <summary>
        /// BtnBack click that close the current form to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBackClick(object sender, System.EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// When the enter key is pressed when typing, send the click to the open button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtBoxPathKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnOpenFolder.PerformClick();
            }
        }

        protected void SetHistory(List<HistoryEntry> history)
        {
            history.Reverse();

            // check if user have history
            if (history.Count == 0)
            {
                ListViewItem line = new ListViewItem(new string[] { "Aucun historique disponible", "" });

                historyListView.Items.Add(line);
            }
            else
            {
                // we check foreach item in from the json reader if it has been added, wich is likly to happen
                foreach (HistoryEntry item in history)
                {
                    ListViewItem line = new ListViewItem(new string[] { item.Content, item.DateTime.ToString("g") });

                    historyListView.Items.Add(line);
                }
            }
        }
        #endregion
    }
}
