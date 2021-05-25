﻿using System;
using Microsoft.Win32.SafeHandles;
using System.Windows.Forms;

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
    public class FolderScan
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
        protected Folder _parentFolder;

        /// <summary>
        /// stoped field
        /// </summary>
        protected bool _stoped;

        /// <summary>
        /// custom constructor
        /// </summary>
        /// <param name="parentFolder">parent folder</param>
        public FolderScan(ref Folder parentFolder)
        {
            this._path = parentFolder.ObjectPath + "\\*";
            this._parentFolder = parentFolder;
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
                    // compact a foreach loop in 1 line
                    // check if the file is allready in the node system of the folder
                    bool isDuplicate = _parentFolder.Files.Exists(item => item.Name == data.cFileName);

                    // if no, add it
                    if (!isDuplicate)
                    {
                        File file = new File(_parentFolder, data);

                        _parentFolder.Files.Add(file);
                    }
                }
                else
                {
                    // compact a foreach loop in 1 line
                    // check if the file is allready in the node system of the folder
                    bool isDuplicate = _parentFolder.Files.Exists(item => item.Name == data.cFileName);

                    // if no, add it
                    if (!isDuplicate)
                    {
                        Folder folder = new Folder(_parentFolder, data);

                        _parentFolder.Folders.Add(folder);

                        _parentFolder.Nodes.Add(folder);
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
            }
        }

        /// <summary>
        /// Stop function
        /// </summary>
        public void Stop()
        {
            this._stoped = true;
        }
    }
}
