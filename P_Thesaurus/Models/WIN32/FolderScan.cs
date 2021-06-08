using System;
using Microsoft.Win32.SafeHandles;
using System.Windows.Forms;

using P_Thesaurus.AppBusiness.WIN32;
using static P_Thesaurus.Models.WIN32.FileAPI;
using static P_Thesaurus.Views.FolderNavigationView;

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
    public class FolderScan : IDisposable
    {
        // https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.backgroundworker?view=net-5.0

        #region event
        /// <summary>
        /// OnFolderScanEnd field
        /// </summary>
        public delegate void OnFolderScanEnd();

        /// <summary>
        /// FolderScanEnd field
        /// </summary>
        public OnFolderScanEnd FolderScanEnd;

        /// <summary>
        /// OnFolderScanEndRaiseEvent function
        /// </summary>
        protected virtual void OnFolderScanEndRaiseEvent()
        {
            FolderScanEnd?.Invoke();
        }

        /// <summary>
        /// OnFolderScanStoped field
        /// </summary>
        public delegate void OnFolderScanStoped();

        /// <summary>
        /// OnFolderScanStoped field
        /// </summary>
        public OnFolderScanStoped FolderScanStoped;

        /// <summary>
        /// OnFolderScanStopedRaiseEvent function
        /// </summary>
        protected virtual void OnFolderScanStopedRaiseEvent()
        {
            FolderScanStoped?.Invoke();
        }

        #endregion event

        /// <summary>
        /// path field
        /// </summary>
        protected string _path;

        /// <summary>
        /// parentFolder field
        /// </summary>
        protected Folder _folder;

        /// <summary>
        /// stoped field
        /// </summary>
        protected bool _stoped;

        /// <summary>
        /// disposed value
        /// </summary>
        private bool _disposedValue = false; // Pour détecter les appels redondants

        /// <summary>
        /// node invoke field
        /// </summary>
        private AddNodeToNodeViaInvokeDelegate _nodeInvoke;

        /// <summary>
        /// custom constructor
        /// </summary>
        /// <param name="parentFolder">parent folder</param>
        public FolderScan(ref Folder parentFolder, AddNodeToNodeViaInvokeDelegate invoke = null)
        {
            if (invoke != null)
            {
                _nodeInvoke = invoke;
            }

            this._path = parentFolder.ObjectPath + "\\*";
            this._folder = parentFolder;
        }

        /// <summary>
        /// Start scan 
        /// </summary>
        public void Start()
        {
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
            while (FindNextFile(firstFile, ref data) && !_stoped)
            {
                // check if we are getting "." and ".." folders
                if (data.IsRelativeDirectory)
                {
                    continue;
                }

                // check wich type of file we have
                if (data.IsFile)
                {
                    // check if the file is allready in the node system of the folder
                    bool isDuplicate = false;

                    for (int i = 0; i < _folder.Files.Count; i++)
                    {
                        File item = _folder.Files[i];

                        if (item != null)
                        {
                            if (item.ObjectData.FileName == data.cFileName)
                            {
                                isDuplicate = true;

                                break;
                            }
                        }
                    }

                    // if no, add it
                    if (!isDuplicate)
                    {
                        File file = new File(_folder, data);

                        _folder.Files.Add(file);
                    }
                }
                else
                {
                    // compact a foreach loop in 1 line
                    // check if the file is allready in the node system of the folder
                    bool isDuplicate = false;

                    for (int i = 0; i < _folder.Folders.Count; i++)
                    {
                        Folder item = _folder.Folders[i];

                        if (item != null)
                        {
                            if (item.ObjectData.FileName == data.cFileName)
                            {
                                isDuplicate = true;

                                break;
                            }
                        }
                    }

                    // if no, add it
                    if (!isDuplicate)
                    {
                        Folder folder = new Folder(_folder, data);

                        _folder.Folders.Add(folder);

                        if (_nodeInvoke != null)
                        {
                            _nodeInvoke(_folder, folder);
                        }
                        else
                        {
                            _folder.Nodes.Add(folder);
                        }
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
            if (_stoped)
            {
                this.OnFolderScanStopedRaiseEvent();
            }
            else
            {
                this.OnFolderScanEndRaiseEvent();

                _folder.Scanned = true;
            }
        }

        /// <summary>
        /// Stop function
        /// </summary>
        public void Stop()
        {
            this._stoped = true;
        }

        #region IDisposable Support
        /// <summary>
        /// Dispose function
        /// </summary>
        /// <param name="disposing">disposing</param>
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
