using P_Thesaurus.Controllers;

/*
 * ETML
 * Clément Sartoni
 * 23.04.2021
 * Projet P_OO-Smart-Thésaurus
 * Form permettant d'afficher l'accueil de l'application
 */

namespace P_Thesaurus.Views
{
    /// <summary>
    /// Form permettant d'afficher l'accueil de l'application
    /// </summary>
    public partial class LaunchingView : BaseView
    {
        public LaunchController Controller { get; set;}

        public LaunchingView()
        {
            InitializeComponent();
        }
    }
}
