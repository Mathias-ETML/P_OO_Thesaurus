/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Factory utilisée par le MainController pour instancier les controllers dont il a besoin
 */
using System;
using P_Thesaurus.Controllers;
using P_Thesaurus.AppBusiness.EnumsAndStructs;

namespace P_Thesaurus.Controllers
{
    /// <summary>
    /// Factory that creates controllers
    /// </summary>
    public class ControllerFactory : IDisposable
    {
        #region Variables
        private bool _disposedValue;

        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ControllerFactory()
        {

        }

        public IController GetController(ControllerType type)
        {
            return null;
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
