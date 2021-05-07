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
    public class LaunchController : BaseController
    {
        #region Variables
        /// <summary>
        /// disposedValue attribute
        /// </summary>
        private bool disposedValue;

        /// <summary>
        /// view attribute
        /// </summary>
        private BaseView _view;

        /// <summary>
        /// View field
        /// </summary>
        public override BaseView View { get => _view; set => _view = value; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public LaunchController()
        {
            this._view = new LaunchingView();
        }

        /// <summary>
        /// NotifyMotherController function
        /// </summary>
        /// <param name="type">type of controller you want to launch</param>
        public void NotifyMotherController(ControllerType type)
        {
            base.OnCloseNotifying(type);
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
