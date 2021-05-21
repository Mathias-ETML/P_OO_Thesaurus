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
        /// Start scan function
        /// </summary>
        /// <param name="folder">folder</param>
        /// <param name="node">node</param>
        public void StartScan(ref Folder folder, ref TreeNode node, Delegate onScanEnded = null)
        {
            _folderScan = new FolderScan(ref folder, ref node);

            if (onScanEnded != null)
            {
                _folderScan.FolderScanEnd += (FolderScan.OnFolderScanEnd)onScanEnded;
            }

            _folderScan.Start();
        }
        #endregion
    }
}
