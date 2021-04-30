/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Controller principal gérant le lançement des autres.
 */

using System;
using System.Windows.Forms;
using P_Thesaurus.Views;
using P_Thesaurus.AppBusiness.EnumsAndStructs;

namespace P_Thesaurus.Controllers
{
    /// <summary>
    /// Main controller that control all controllers
    /// </summary>
    public class MainController : IController
    {
        #region Variables
        /// <summary>
        /// The current used Controller
        /// </summary>
        private IController _childController;

        /// <summary>
        /// The controller's factory
        /// </summary>
        private ControllerFactory _factory;

        /// <summary>
        /// BaseView property, always null
        /// </summary>
        private BaseView _baseView = null;

        /// <summary>
        /// Disposed value
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// View field
        /// </summary>
        public BaseView View { get => _baseView; set => _baseView = value; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainController()
        {
            this._factory = new ControllerFactory();
            this._childController = new LaunchController();
        }

        /// <summary>
        /// Launch function
        /// </summary>
        public void Launch()
        {
            this._childController.Launch();
        }

        /// <summary>
        /// OnCloseNotifying function
        /// </summary>
        /// <param name="controllerType">type</param>
        public void OnCloseNotifying(ControllerType controllerType)
        {
            this._childController = _factory.GetController(controllerType);
        }
        #endregion

        #region Dispose Model
        /// <summary>
        /// Dispose funciton
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _childController.Dispose();
                    _factory.Dispose();
                }

                _disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose function
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
