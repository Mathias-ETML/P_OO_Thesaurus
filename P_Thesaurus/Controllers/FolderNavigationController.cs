/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Controller contenant les méthodes communes aux vues de navigation dans des fichiers
 */

using System;
using P_Thesaurus.Views;

namespace P_Thesaurus.Controllers
{
    /// <summary>
    /// Controller used to put the common code to all the files-based controllers
    /// </summary>
    public abstract class FolderNavigationController : BaseController
    {
        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FolderNavigationController()
        {

        }
        #endregion

        #region Dispose Model
        private bool disposedValue;

        protected new virtual void Dispose(bool disposing)
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
