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

namespace P_Thesaurus.Models
{
    /// <summary>
    /// Model used to get datas from the windows folders
    /// </summary>
    public class FolderModel
    {
        #region Variables
        public const string DEFAULT_FOLDER_HISTORY_PATH = ".\\folder_history.txt";
        private History<HistoryEntry> _history;
        private FolderScan _folderScan;
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
                Folder buffer = Folder.GetFolder(String.Join("\\", folders, 0, i + 1));

                buffer.ParentFolder = last;

                last.Folders.Add(buffer);

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
        public List<ResearchElement> GetObjectRecursivly(Folder start, List<string> names, bool forceRescan)
        {
            List<ResearchElement> items = new List<ResearchElement>();

            for (int i = 0; i < names.Count; i++)
            {
                names[i] = names[i].ToLowerInvariant();
            }

            return GetObjectRecursivly(start, names, forceRescan, ref items);
        }

        /// <summary>
        /// Scan the folder recursivly for all the items with the entered name
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="name"></param>
        /// <param name="forceRescan"></param>
        /// <param name="objects"></param>
        private List<ResearchElement> GetObjectRecursivly(Folder start, List<string> names, bool forceRescan, ref List<ResearchElement> objects)
        {
            // check if we need to scan the folder
            if (start.Folders.Count == 0 || !start.Scanned || forceRescan)
            {
                StartScan(ref start);
            }

            // search item
            foreach (FolderObject item in start.FolderObjects)
            {
                foreach (string term in names)
                {
                    if (item.ObjectData.FileName.Equals(term, StringComparison.InvariantCultureIgnoreCase) ||
                    item.ObjectData.FileName.ToLowerInvariant().Contains(term))
                    {
                        objects.Add(new ResearchElement {
                            Object = item,
                            Ration = term.Length / item.ObjectData.FileName.Length
                        });
                    }
                }
            }

            // check if we are at the top of the folder system
            if (start.Folders.Count == 0)
            {
                return objects;
            }
            else
            {
                // scan recursivly
                foreach (Folder item in start.Folders)
                {
                    return GetObjectRecursivly(item, names, forceRescan, ref objects);
                }
            }

            // this sould never happen, because of the if statment below
            return objects;
        }

        /// <summary>
        /// Start scan function
        /// </summary>
        /// <param name="folder">folder</param>
        /// <param name="node">node</param>
        public void StartScan(ref Folder folder, FolderScan.OnFolderScanEnd onScanEnded = null)
        {
            _folderScan = new FolderScan(ref folder);

            if (onScanEnded != null)
            {
                _folderScan.FolderScanEnd += onScanEnded;
            }

            _folderScan.Start();
        }
        #endregion
    }
}
