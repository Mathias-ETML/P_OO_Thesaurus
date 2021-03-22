using System;
using Microsoft.Win32.SafeHandles;
using System.Windows.Forms;
using System.ComponentModel;
using P_Thesaurus.AppBusiness.WIN32;
using static P_Thesaurus.Models.WIN32.FileAPI;

namespace P_Thesaurus.Models.WIN32
{
    /// <summary>
    /// AsyncFolderScanning class
    /// 
    /// This was made by Mathias R
    /// 
    /// Translating c++ into c#.
    /// 
    /// </summary>
    public class AsyncFolderScanning : IDisposable
    {
        // https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.backgroundworker?view=net-5.0



        private BackgroundWorker _worker;
        private DoWorkEventArgs _events;

        private bool disposedValue = false; // Pour détecter les appels redondants


        /// <summary>
        /// path field
        /// </summary>
        protected string _path;

        /// <summary>
        /// parentFolder field
        /// </summary>
        protected Folder _parentFolder;

        /// <summary>
        /// node field
        /// </summary>
        protected TreeNode _node;

        /// <summary>
        /// custom constructor
        /// </summary>
        /// <param name="parentFolder">parent folder</param>
        public AsyncFolderScanning(ref Folder parentFolder)
        {
            this._path = parentFolder.Path + "\\*";
            this._parentFolder = parentFolder;
        }

        /// <summary>
        /// custom constructor
        /// </summary>
        /// <param name="parentFolder">parent folder</param>
        public AsyncFolderScanning(ref Folder parentFolder, ref TreeNode node)
        {
            this._path = parentFolder.Path + "\\*";
            this._parentFolder = parentFolder;
            this._node = node;
        }

        /// <summary>
        /// StartAsync function
        /// 
        /// background worker implementaiton
        /// </summary>
        /// <param name="sender">background worker</param>
        /// <param name="e">DoWorkEventArgs</param>
        public void StartAsync(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            this._worker = worker;
            this._events = e;

            Scan();
        }

        /// <summary>
        /// Scan function
        /// </summary>
        protected void Scan()
        {
            // hold the file data
            WIN32_FIND_DATA data = new WIN32_FIND_DATA();

            // kernel call to get first file or folder from the path
            SafeFileHandle firstFile = FindFirstFile(_path, ref data);

            // handle to first file
            IntPtr handle = firstFile.DangerousGetHandle();

            // moving through the folder with the first file as ref
            while (FindNextFile(firstFile, ref data) && !_worker.CancellationPending)
            {
                // check if we are getting "." and ".." folders
                if (data.IsRelativeDirectory)
                {
                    continue;
                }

                // check wich type of file we have
                if (data.IsFile)
                {
                    _parentFolder.Files.Add(new File(_parentFolder, data));


                    _node.Nodes.Add(data.cFileName);

                }
                else
                {
                    _parentFolder.Folders.Add(new Folder(_parentFolder, data));

                    if (_node != null)
                    {
                        _node.Nodes.Add(data.cFileName);
                    }
                }
            }

            // closing handle
            if (FindClose(handle))
            {
                // memory managment
                firstFile.SetHandleAsInvalid();
                firstFile.Dispose();
            }

            ScanFinished();
        }

        /// <summary>
        /// ScanEnd function
        /// </summary>
        private void ScanFinished()
        {
            if (!_worker.CancellationPending)
            {
                this._events.Cancel = true;
            }
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~AsyncFolderScanning() {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
