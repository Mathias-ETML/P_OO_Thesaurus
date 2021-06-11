/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Model utilisé pour récupérer les données depuis des fichiers.
 */

using P_Thesaurus.AppBusiness.HistoryReader;
using System;
using System.IO;
using System.Collections.Generic;
using P_Thesaurus.Models.WIN32;
using P_Thesaurus.AppBusiness.WIN32;
using System.Windows.Forms;
using P_Thesaurus.AppBusiness.EnumsAndStructs;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using static P_Thesaurus.Views.FolderNavigationView;

namespace P_Thesaurus.Models
{
    /// <summary>
    /// Model used to get datas from the windows folders
    /// </summary>
    public class FolderModel : IDisposable
    {
        #region Variables
        public static readonly string DEFAULT_FOLDER_HISTORY_PATH = AppDomain.CurrentDomain.BaseDirectory + "\\folder_history.txt";
        private History<HistoryEntry> _history;
        private FolderScan _folderScan;
        private bool disposedValue = false; // Pour détecter les appels redondants
        private List<ResearchElement> _foundItems;
        private int _createdThreads;
        private AddNodeToNodeViaInvokeDelegate _invokeNode;
        private ResearchEndedDelegate _endResearch;
        private bool _stopScan = false;

        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FolderModel()
        {
            this._history = new History<HistoryEntry>(DEFAULT_FOLDER_HISTORY_PATH, true);
        }

        /// <summary>
        /// GetAllDrives function
        /// </summary>
        /// <returns>array of drives</returns>
        public List<DriveInfo> GetAllDrives()
        {
            string[] drives = Environment.GetLogicalDrives();

            List<DriveInfo> buffer = new List<DriveInfo>(drives.Length);

            foreach (string item in drives)
            {
                buffer.Add(new DriveInfo(item));
            }

            return buffer;
        }

        /// <summary>
        /// GetHistory function
        /// </summary>
        /// <returns>list on histories entries</returns>
        public List<HistoryEntry> GetHistory()
        {
            return _history.Read();
        }

        /// <summary>
        /// Write in history function
        /// </summary>
        /// <param name="path">full path</param>
        public void WriteInHistory(string path)
        {
            // we make sure we get all the last paths
            _history.Read();

            HistoryEntry entry = new HistoryEntry()
            {
                Content = path,
                DateTime = DateTime.Now
            };

            _history.AddEntry(entry);
        }

        /// <summary>
        /// Scan folder recursivly to get hsi root folder
        /// </summary>
        /// <param name="folder">folder</param>
        public Folder GetRootFolderRecursivly(Folder folder)
        {
            if (!folder.IsRootFolder)
            {
                return GetRootFolderRecursivly(folder.ParentFolder);
            }

            return folder;
        }

        /// <summary>
        /// Get folder function
        /// </summary>
        /// <param name="path">pat</param>
        /// <returns>Folder</returns>
        public Folder GetFolder(string path)
        {
            if (path == null || path.Length <= 0)
            {
                throw new ArgumentNullException("path");
            }

            string[] folders = path.Split(Path.DirectorySeparatorChar);

            // check if we have a folder like C:\
            if (folders.Length == 1)
            {
                return Folder.GetRootFolder(folders[0][0]);
            }

            // getting root folder
            Folder root = Folder.GetRootFolder(folders[0][0]);
            Folder last = root;

            // getting all the sub folders of the path
            for (int i = 1; i < folders.Length; i++)
            {
                // getting the sub folder, creating it and chaining it to his parent
                // and doing it again
                Folder buffer = Folder.GetFolder(String.Join("\\", folders, 0, i + 1));

                buffer.ParentFolder = last;

                last.Folders.Add(buffer);
                last.Nodes.Add(buffer);

                last = buffer;
            }

            return last;
        }

        /// <summary>
        /// Get you the object that match the name recursivly
        /// </summary>
        /// <param name="start">the current folder where you want to start</param>
        /// <param name="name">the object name (case ignored)</param>
        /// <returns>the object or null if not found</returns>
        public void GetObjectRecursivly(Folder start, List<string> names, AddNodeToNodeViaInvokeDelegate invoke, ResearchEndedDelegate end, bool forceRescan)
        {
            _stopScan = false;
            _foundItems = new List<ResearchElement>();
            _invokeNode = invoke;
            _endResearch = end;

            // put all names to lower
            for (int i = 0; i < names.Count; i++)
            {
                names[i] = names[i].ToLowerInvariant();
            }

            GetObjectRecursivlyAsync(start, names, forceRescan);
        }

        /// <summary>
        /// Scan the folder recursivly for all the items with the entered name
        /// 
        /// </summary>
        /// <param name="start">start folder</param>
        /// <param name="name">name</param>
        /// <param name="forceRescan">force scan</param>
        private async void GetObjectRecursivlyAsync(Folder start, List<string> names, bool forceRescan)
        {
            if (_stopScan)
            {
                return;
            }

            _createdThreads++;

            // check if we need to scan the folder
            if (start.Folders.Count == 0 || !start.Scanned || forceRescan)
            {
                StartScan(ref start);
            }

            // search item
            foreach (FolderObject item in start.FolderObjects)
            {
                if (item != null)
                {
                    // check for each term
                    foreach (string term in names)
                    {
                        // check if item contains each search term
                        if (item.ObjectData.FileName.Equals(term, StringComparison.InvariantCultureIgnoreCase) ||
                        item.ObjectData.FileName.ToLowerInvariant().Contains(term))
                        {
                            ResearchElement element = new ResearchElement()
                            {
                                Object = item,
                                Ratio = term.Length / item.ObjectData.FileName.Length
                            };

                            if ((item as Folder) != null)
                            {
                                element.Type = typeof(Folder);
                            }
                            else
                            {
                                element.Type = typeof(P_Thesaurus.AppBusiness.WIN32.File);
                            }

                            _foundItems.Add(element);
                        }
                    }
                }
            }

            // scan each folders async
            foreach (Folder item in start.Folders)
            {
                await Task.Run(() =>
                {
                    GetObjectRecursivlyAsync(item, names, forceRescan);
                });
            }

            CheckScanEnded();
        }

        /// <summary>
        /// Stop scan function
        /// </summary>
        public void StopScan()
        {
            _stopScan = true;
            _folderScan.Stop();
        }

        /// <summary>
        /// Folder scan ender
        /// 
        /// Call this when program has finished scanning a folder
        /// </summary>
        private void CheckScanEnded()
        {
            _createdThreads--;

            if (_createdThreads <= 0)
            {
                _endResearch(_foundItems);
            }
        }

        /// <summary>
        /// Start scan function
        /// </summary>
        /// <param name="folder">folder</param>
        /// <param name="node">node</param>
        public void StartScan(ref Folder folder, FolderScan.OnFolderScanEnd onScanEnded = null)
        {
            _folderScan = new FolderScan(ref folder, _invokeNode);

            if (onScanEnded != null)
            {
                _folderScan.FolderScanEnd += onScanEnded;
            }

            _folderScan.Start();
        }
        #endregion

        #region IDisposable Support
        /// <summary>
        /// Dispose function
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _history.Dispose();
                    _folderScan.Dispose();
                }

                disposedValue = true;
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
