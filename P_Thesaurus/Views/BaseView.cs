/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Form d'héritage représentant toutes les vues.
 */
using System.Windows.Forms;
using P_Thesaurus.Controllers;

namespace P_Thesaurus.Views
{
    /// <summary>
    /// View that reproups all the common fields and methods of the views
    /// </summary>
    public partial class BaseView : Form
    {
        public BaseView()
        {
            InitializeComponent();
        }
    }
}
