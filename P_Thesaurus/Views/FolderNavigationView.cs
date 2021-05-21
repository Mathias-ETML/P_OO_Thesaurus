/*
 * ETML
 * Clément Sartoni
 * 23.04.2021
 * Projet P_OO-Smart-Thésaurus
 * Form permettant d'afficher la navigation dans les fichiers FTP ou windows
 */


using P_Thesaurus.AppBusiness.WIN32;
using P_Thesaurus.Controllers;

namespace P_Thesaurus.Views
{
    /// <summary>
    /// View that shows navigation in FTP or Windows Files
    /// </summary>
    public partial class FolderNavigationView : NavigationView
    {
        #region Variables
        private string _path;
        private Folder _currentFolder;

        /// <summary>
        /// the view's controller
        /// </summary>
        public FolderNavigationController Controller { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Controller
        /// </summary>
        public FolderNavigationView(string path)
        {
            this._path = path;

            InitializeComponent();
        }

        /// <summary>
        /// Init function
        /// </summary>
        public void Init()
        {
            _currentFolder = Controller.GetFolder(_path);
        }
        #endregion
    }
}
