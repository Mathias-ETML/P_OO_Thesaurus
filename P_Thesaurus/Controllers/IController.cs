/*
* ETML
* Clément Sartoni
* 22.03.2021
* Projet P_OO-Smart-Thésaurus
* Interface représentant tous les controllers, contenant les méthodes qu'ils sont censés implémenter
*/

using P_Thesaurus.AppBusiness.EnumsAndStructs;
using P_Thesaurus.Views;
using System;

namespace P_Thesaurus.Controllers
{
    /// <summary>
    /// Interface that represents all the controllers
    /// </summary>
    public interface IController : IDisposable
    {
        /// <summary>
        /// View Field
        /// </summary>
        BaseView View { get; set; }

        /// <summary>
        /// Mother controller field
        /// </summary>
        IController MotherController { get; set; }

        /// <summary>
        /// Launch function
        /// </summary>
        void Launch();

        /// <summary>
        /// OnCloseNotifying function
        /// </summary>
        /// <param name="controllerType">controller type</param>
        void OnCloseNotifying(ControllerType controllerType);
    }
}
