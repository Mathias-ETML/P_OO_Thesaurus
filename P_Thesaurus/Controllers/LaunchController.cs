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
            this._view = new LaunchingView()
            {
                Controller = this
            };
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
        /// <summary>
        /// Dispose function
        /// </summary>
        /// <param name="disposing">disposing</param>
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
