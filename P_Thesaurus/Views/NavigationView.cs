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
    public partial class NavigationView : Form
    {
        public NavigationView()
        {
            InitializeComponent();
        }


        protected void TestHeritance()
        {
            MessageBox.Show("blbl!");
        }
    }
}
