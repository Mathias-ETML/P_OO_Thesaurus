﻿/*
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
using System.Threading;

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
            // check if we need to do a linked list of view
            if (MotherController.View == null)
            {
                Application.Run(View);
            }
            else
            {
                MotherController.View.AddOwnedForm(this.View);
                MotherController.View.Hide();
                this.View.Show();
            }
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
        /// <summary>
        /// Dispose function
        /// </summary>
        /// <param name="disposing">dispose</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (View != null)
                    {
                        View.Dispose();
                    }
                }

                _disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose function
        /// </summary>
        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }



        #endregion
    }
}
