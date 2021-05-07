﻿/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Controller gérant les données provenant d'un dossier
 */

using P_Thesaurus.Models;
using P_Thesaurus.Views;
using System;

namespace P_Thesaurus.Controllers
{
    /// <summary>
    /// Controller that controls the folder reading and navigation options
    /// </summary>
    public class FolderController : FolderNavigationController
    {
        #region Variables
        /// <summary>
        /// disposed value attribute
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// model attribute
        /// </summary>
        private FolderModel _model;

        /// <summary>
        /// view attribute
        /// </summary>
        private BaseView _view;

        /// <summary>
        /// view field
        /// </summary>
        public override BaseView View { get => _view; set => _view = value; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FolderController()
        {

        }
        #endregion

        #region Dispose Model
        /// <summary>
        /// Dispose function
        /// </summary>
        /// <param name="disposing"></param>
        protected new virtual void Dispose(bool disposing)
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

        public new void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
