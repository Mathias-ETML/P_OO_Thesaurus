/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Controller principal gérant le lançement des autres.
 */

namespace P_Thesaurus.Controllers
{
    /// <summary>
    /// Main controller that control all controllers
    /// </summary>
    public class MainController
    {
        #region Variables
        /// <summary>
        /// The current used Controller
        /// </summary>
        private IBaseController _childController;

        /// <summary>
        /// The controller's factory
        /// </summary>
        private ControllerFactory _factory;

        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainController()
        {
            _factory = new ControllerFactory();
        }
        #endregion

        #region Dispose Model
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~MainController()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
        #endregion
    }
}
