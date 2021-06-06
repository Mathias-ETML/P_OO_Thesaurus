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
        /// <summary>
        /// Dispose function
        /// </summary>
        /// <param name="disposing">dispose</param>
        protected new virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _model.Dispose();
                }

                _disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose function
        /// </summary>
        public new void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
