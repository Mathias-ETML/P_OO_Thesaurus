/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Base des controllers gérant les opérations communes à tous les controlleurs
 */

using P_Thesaurus.AppBusiness.EnumsAndStructs;
using System.Windows.Forms;
using P_Thesaurus.Views;
using System;

namespace P_Thesaurus.Controllers
{
    /// <summary>
    /// Base controller for all of them
    /// </summary>
    public abstract class BaseController : IController
    {
        #region Variables
        /// <summary>
        /// _disposedValue attribut
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// mother controller attribut
        /// </summary>
        private IController _motherController;

        /// <summary>
        /// View property
        /// </summary>
        public abstract BaseView View { get; set; }

        /// <summary>
        /// MotherController property
        /// </summary>
        public IController MotherController { get => _motherController; set => _motherController = value; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseController()
        {

        }

        /// <summary>
        /// Launch the view
        /// </summary>
        public void Launch()
        {
            Application.Run(View);
        }

        /// <summary>
        /// Occure when the controller need to be disposed
        /// Call his mother controller to swap to the required controller
        /// And dispose this controller
        /// </summary>
        /// <param name="controllerType">controllerType</param>
        public void OnCloseNotifying(ControllerType controllerType)
        {
            MotherController.OnCloseNotifying(controllerType);
        }
        #endregion

        #region Dispose Model

        protected virtual void Dispose(bool disposing)
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

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }



        #endregion
    }
}
