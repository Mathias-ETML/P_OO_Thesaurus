/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Controller gérant les données provenant d'un dossier
 */

using P_Thesaurus.Models;
using P_Thesaurus.Views;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using P_Thesaurus.AppBusiness.HistoryReader;
using P_Thesaurus.AppBusiness.WIN32;
using P_Thesaurus.Models.WIN32;

namespace P_Thesaurus.Controllers
{
    /// <summary>
    /// Controller that controls the folder reading and navigation optionsx
    /// </summary>
    public class FolderController : FolderNavigationController
    {
        #region Variables
        /// <summary>
        /// disposed value attribute
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// model attribute
        /// </summary>
        private FolderModel _model;

        /// <summary>
        /// folder history view
        /// </summary>
        private FolderHistoryView _historyView;

        /// <summary>
        /// folder navigation view
        /// </summary>
        private FolderNavigationView _folderNavigationView;

        /// <summary>
        /// view field
        /// </summary>
        public override BaseView View { get => _historyView; set => _historyView = (FolderHistoryView)value; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FolderController()
        {
            this._model = new FolderModel();

            this._historyView = new FolderHistoryView()
            {
                Controller = this
            };

            _historyView.Init();
        }

        /// <summary>
        /// GetAllDrives function
        /// </summary>
        /// <returns>array of drives</returns>
        public List<DriveInfo> GetAllDrives()
        {
            return _model.GetAllDrives();
        }

        /// <summary>
        /// LaunchFolderNavigationView function
        /// </summary>
        /// <param name="path">path to start with</param>
        public void LaunchFolderNavigationView(string path)
        {
            this._historyView.Hide();

            _folderNavigationView = new FolderNavigationView(path)
            {
                Controller = this
            };

            _folderNavigationView.FormClosed += OnFolderNavigationViewClosing;

            // call all the function from this controller
            _folderNavigationView.Init();

            _folderNavigationView.Show(_historyView);
        }

        /// <summary>
        /// Get folder function
        /// </summary>
        /// <param name="path">pat</param>
        /// <returns>Folder</returns>
        public Folder GetFolder(string path)
        {
            return _model.GetFolder(path);
        }

        /// <summary>
        /// Start scan function
        /// </summary>
        /// <param name="folder">folder</param>
        /// <param name="node">node</param>
        public void StartScan(ref Folder folder, FolderScan.OnFolderScanEnd onScanEnded = null)
        {
            _model.StartScan(ref folder, onScanEnded);
        }

        /// <summary>
        /// Scan folder recursivly to get his root folder
        /// </summary>
        /// <param name="folder">folder</param>
        public Folder GetRootFolderRecursivly(Folder folder)
        {
            return _model.GetRootFolderRecursivly(folder);
        }

        /// <summary>
        /// Occure when the second view is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnFolderNavigationViewClosing(object sender, EventArgs e)
        {
            _historyView.Init();

            _folderNavigationView.Dispose();
        }

        /// <summary>
        /// Get you the object that match the name recursivly
        /// </summary>
        /// <param name="start">the current folder where you want to start</param>
        /// <param name="name">the object name (case ignored)</param>
        /// <returns>the object or null if not found</returns>
        public List<FolderObject> GetObjectRecursivly(Folder start, string name, bool forceRescan = false)
        {
            return _model.GetObjectRecursivly(start, name, forceRescan);
        }
        
        #endregion

        #region Dispose Model
        /// <summary>
        /// Dispose function
        /// </summary>
        /// <param name="disposing"></param>
        protected new virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                _disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~ControllerFactory()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public new void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
