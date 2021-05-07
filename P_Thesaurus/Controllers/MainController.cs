/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Controller principal gérant le lançement des autres.
 */

using System;
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
        /// Disposed value
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// View field
        /// 
        /// Main controller doesn't have base view
        /// </summary>
        public BaseView View { get => null; set { return; } }

        /// <summary>
        /// Mother Controller field
        /// 
        /// Main controller doesn't have mother controller
        /// </summary>
        public IController MotherController { get => null; set { return; } }
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainController()
        {
            this._factory = new ControllerFactory();

            this._childController = new LaunchController()
            {
                MotherController = this
            };
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
            // get the new controller
            IController buffer = _factory.GetController(controllerType);

            // setup monther controller
            buffer.MotherController = this._childController;

            // launch controller
            buffer.Launch();
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
