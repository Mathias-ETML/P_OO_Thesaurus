/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Factory utilisée par le MainController pour instancier les controllers dont il a besoin
 */
using System;
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

        /// <summary>
        /// GetController function
        /// </summary>
        /// <param name="type">type of controller</param>
        /// <returns>the controller that fit the type</returns>
        public IController GetController(ControllerType type)
        {
            switch (type)
            {
                case ControllerType.Launching:
                    return new LaunchController();

                case ControllerType.Ftp:
                    return new FtpController();

                case ControllerType.Folder:
                    return new FolderController();

                case ControllerType.Web:
                    return new WebController();

                default:
                    throw new ArgumentException("Please include a valid type of controller");
            }
        }
        #endregion

        /// <summary>
        /// Dispose function
        /// </summary>
        /// <param name="disposing">disposing</param>
        #region Dispose Model
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {

                }

                _disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose function
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
