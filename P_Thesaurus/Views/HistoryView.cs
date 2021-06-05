﻿/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Form d'héritage représentant toutes les vues gérant l'affichage de l'historique et la séléction d'un endroit à indexer
 */
using P_Thesaurus.Controllers;
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

        /// <summary>
        /// BtnBack click that close the current form to go back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBackClick(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}
