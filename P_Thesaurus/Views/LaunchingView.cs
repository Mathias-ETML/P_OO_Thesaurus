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
        #region Variables
        /// <summary>
        /// View's controller
        /// </summary>
        public LaunchController Controller { get; set;}
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public LaunchingView()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Button "Folder" Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFolderClick(object sender, System.EventArgs e)
        {
            Controller.OnCloseNotifying(AppBusiness.EnumsAndStructs.ControllerType.Folder);
        }

        /// <summary>
        /// Button "Web" Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWebClick(object sender, System.EventArgs e)
        {
            Controller.OnCloseNotifying(AppBusiness.EnumsAndStructs.ControllerType.Web);
        }


        #endregion
        /// <summary>
        /// Button "Ftp" click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFtpClick(object sender, System.EventArgs e)
        {
            ShowMessageBox("Fonction non implémenté");
        }
    }
}
