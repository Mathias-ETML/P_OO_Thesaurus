/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Form d'héritage représentant toutes les vues gérant la navigation dans les éléments indexés
 */
using System.Windows.Forms;

namespace P_Thesaurus.Views
{
    public partial class NavigationView : BaseView
    {
        public NavigationView()
        {
            InitializeComponent();
        }

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
