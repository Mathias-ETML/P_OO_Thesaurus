/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Controller gérant le lancement de l'application et la vue de lancement.
 */

using P_Thesaurus.AppBusiness.EnumsAndStructs;
using P_Thesaurus.Views;
using System;

namespace P_Thesaurus.Controllers
{
    /// <summary>
    /// Controller that controls the app launching and the first view
    /// </summary>
    public class LaunchController : BaseController, IController
    {
        #region Variables
        private bool disposedValue;

        private LaunchingView _view;

        public override BaseView View { get => _view; set => _view = value as LaunchingView; }

        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public LaunchController()
        {
            this._view = new LaunchingView();
        }

        #endregion

        #region Dispose Model


        protected new virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _view.Dispose();
                }

                base.Dispose();
                disposedValue = true;
            }
        }

        public new void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
