/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Controller gérant les opérations de la connexion et du transfert des données d'un site Web.
 */
using P_Thesaurus.AppBusiness.EnumsAndStructs;
using P_Thesaurus.Models;
using P_Thesaurus.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace P_Thesaurus.Controllers
{
    /// <summary>
    /// Controller that controls the web opening, reading and navigation actions
    /// </summary>
    public class WebController : BaseController
    {
        #region Variables
        /// <summary>
        /// disposed value attribut
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// model attribut
        /// </summary>
        private WebModel _model;

        /// <summary>
        /// view attribute
        /// </summary>
        private BaseView _view;

        /// <summary>
        /// folder history view
        /// </summary>
        private WebHistoryView _historyView;

        /// <summary>
        /// folder navigation view
        /// </summary>
        private WebNavigationView _webNavigationView;

        /// <summary>
        /// View field
        /// </summary>
        public override BaseView View { get => _view; set => _view = value; }

        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public WebController()
        {
            _model = new  WebModel();

            this._historyView = new WebHistoryView()
            {
                Controller = this
            };

            _view = _historyView;

            this._historyView.Init();
        }

        public void SetDatas(string url)
        {
            if(_model.TestURL(url))
            {
                List<WebElement> datas = _model.GetWebElements(url);

                _webNavigationView.InitializeElements(datas);

                _model.WriteInHistory(url);
            }
            else
            {
                _view.ShowMessageBox("L'url sélectionnée n'est pas accessible. Beaucoup d'erreurs externes peuvent en être la cause (sites fermés, erreurs de serveur ou votre connexion internet)");
            }
        }

        public bool TestUrl(string url)
        {
            if(_model.TestURL(url))
            {
                this._historyView.Hide();

                _webNavigationView = new WebNavigationView()
                {
                    Controller = this
                };

                _view = _webNavigationView;

                //TODO : the onclosing event was here once 

                // call all the function from this controller
                SetDatas(url);

                _webNavigationView.Show(_historyView);

                return true;
            }
            else
            {
                _historyView.ShowMessageBox("L'url sélectionnée n'est pas accessible");
                return false;
            }
        }

        public void TestUrl(WebElement link)
        {
            if(link.type == WebElementType.Link)
            {
                SetDatas(link.link);
            }
            else
            {
                Process.Start(link.link);
            }
        }
        #endregion

        #region Dispose Model


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
