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

            button2.Click += new EventHandler(Button2Click);
        }

        public void Init()
        {

        }
        #endregion

        #region Private Methods
        private void Button2Click(object sender, EventArgs e)
        {
            Controller.TestUrl(textBox1.Text);
        }

        #endregion

    }
}
