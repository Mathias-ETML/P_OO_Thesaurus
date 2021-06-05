/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Controller contenant les méthodes communes aux vues de navigation dans des fichiers
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using P_Thesaurus.AppBusiness.HistoryReader;
using P_Thesaurus.AppBusiness.WIN32;
using P_Thesaurus.Models;
using P_Thesaurus.Models.WIN32;
using P_Thesaurus.Views;

namespace P_Thesaurus.Controllers
{
    /// <summary>
    /// Controller used to put the common code to all the files-based controllers
    /// </summary>
    public abstract class FolderNavigationController : BaseController
    {
        /// <summary>
        /// model attribute
        /// </summary>
        private FolderModel _model;

        private bool _disposedValue;


        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FolderNavigationController()
        {
            this._model = new FolderModel();
        }

        /// <summary>
        /// GetHistory function
        /// </summary>
        /// <returns></returns>
        public List<HistoryEntry> GetHistory()
        {
            return _model.GetHistory();
        }

        /// <summary>
        /// Write in history function
        /// </summary>
        /// <param name="path">full path</param>
        public void WriteInHistory(string path)
        {
            _model.WriteInHistory(path);
        }
        #endregion

        #region Dispose Model
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
