/*
 * ETML
 * Clément Sartoni
 * 23.04.2021
 * Projet P_OO-Smart-Thésaurus
 * Form permettant d'afficher la navigation dans les fichiers FTP ou windows
 */


using P_Thesaurus.AppBusiness.WIN32;
using P_Thesaurus.Controllers;
using P_Thesaurus.Models.WIN32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace P_Thesaurus.Views
{
    /// <summary>
    /// View that shows navigation in FTP or Windows Files
    /// </summary>
    public partial class FolderNavigationView : NavigationView
    {
        #region Variables
        private const int BITS_IN_MEGA_BYTE = 1024;
        private string _path;
        private Folder _currentFolder;

        /// <summary>
        /// the view's controller
        /// </summary>
        public FolderNavigationController Controller { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Controller
        /// </summary>
        public FolderNavigationView(string path)
        {
            this.FormClosing += OnClosing;

            this._path = path;

            InitializeComponent();
        }

        /// <summary>
        /// Init function
        /// </summary>
        public void Init()
        {
            // get current folder at the end of the path
            _currentFolder = Controller.GetFolder(_path);

            // getting root folder of the athx
            Folder root = Controller.GetRootFolderRecursivly(_currentFolder);

            // adding root folder at the tree view
            folderTreeView.Nodes.Add(root);

            // getting current tree node cause pointers
            TreeNode currentNode = _currentFolder;

            // function pointer
            FolderScan.OnFolderScanEnd onScanEnd = new Models.WIN32.FolderScan.OnFolderScanEnd(ScanEnd);

            // scannig current folder
            ScanFolder(_currentFolder);

            // showing user location
            folderTreeView.SelectedNode = currentNode;
            _currentFolder.Expand();
        }

        /// <summary>
        /// Scan root folder recursivly and add each children to tree view
        /// </summary>
        /// <param name="root">folder</param>
        private void ScanRootFolderRecursivly(Folder folder)
        {
            // we can't add a node to himself
            if (folder.IsRootFolder)
            {
                return;
            }

            // check if we are not in the folder that contain this folder
            if (!folder.Folders.Contains(_currentFolder))
            {
                // check if we have 1 folder, else there is a litle problem
                if (folder.Folders.Count == 1)
                {
                    // adding the node of the future folder to the current scanned view
                    folder.Nodes.Add(folder.Folders[0]);

                    ScanRootFolderRecursivly(folder.Folders[0]);
                }
                else
                {
                    throw new NotSupportedException("There is only can be 1 folder in the path scanned recursivly", 
                        new Exception("Please check GetRootFolderRecursivly function in FolderNavigationController class"));
                }
            }
            else
            {
                folder.Nodes.Add(_currentFolder);
            }
        }

        /// <summary>
        /// Occure when the scan ended
        /// </summary>
        private void ScanEnd()
        {
            ResetListView();

            // add the objects in the list view
            foreach (Folder item in _currentFolder.Folders)
            {
                ListViewItem items = new ListViewItem(new string[] {
                                                    item.Name,
                                                    "Dossier",
                                                    item.ObjectData.ModifyTime.ToLongDateString() });

                // what do we do here is that we assign the typeof the list view item via the name of the list view
                items.Name = nameof(Folder);

                currentFolderListView.Items.Add(items);
            }

            foreach (File file in _currentFolder.Files)
            {
                ListViewItem items = new ListViewItem(new string[] {
                                                        file.Name,
                                                        "Fichier " + file.FileType,
                                                        file.ObjectData.ModifyTime.ToLongDateString() });

                // what do we do here is that we assign the typeof the list view item via the name of the list view
                items.Name = nameof(File);

                currentFolderListView.Items.Add(items);
            }
        }

        /// <summary>
        /// Reset list view function
        /// </summary>
        private void ResetListView()
        {
            // reset the list view
            currentFolderListView.Clear();
            currentFolderListView.Columns.Add("Name", 130, HorizontalAlignment.Left);
            currentFolderListView.Columns.Add("Type", 100, HorizontalAlignment.Left);
            currentFolderListView.Columns.Add("Modification", 100, HorizontalAlignment.Left);

            currentFolderListView.AllowColumnReorder = true;
            currentFolderListView.FullRowSelect = true;
            currentFolderListView.MultiSelect = false;
            currentFolderListView.View = View.Details;
        }

        /// <summary>
        /// OnFolderChange function
        /// 
        /// occure when the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTreeViewFolderChange(object sender, EventArgs e)
        {
            // casting
            TreeView obj = (TreeView)sender;
            Folder folder = obj.SelectedNode as Folder;

            // check if user clicked on a folder, else it's a file
            if (folder != null)
            {
                ScanFolder(folder);
            }
        }

        /// <summary>
        /// Scan the folder
        /// </summary>
        /// <param name="folder">folder</param>
        private void ScanFolder(Folder folder)
        {
            // fold the current node so the user see the tree view of the selected folder
            if (_currentFolder != null)
            {
                _currentFolder.Collapse();
            }

            _currentFolder = folder;

            // function pointer
            FolderScan.OnFolderScanEnd onScanEnd = new Models.WIN32.FolderScan.OnFolderScanEnd(ScanEnd);

            // scannig current folder
            Controller.StartScan(ref folder, onScanEnd);

            // showing user location
            folderTreeView.SelectedNode = _currentFolder;
            _currentFolder.Expand();

            // show current folder in the top panel
            BuildPathListView();

            // show information about current folder
            ShowInformationSelectedListViewFolder(_currentFolder);
        }

        /// <summary>
        /// Build a list view with the full path and creat a column for each 
        /// </summary>
        private void BuildPathListView()
        {
            // clear items from panel
            if (panHistoryPath.Controls.Count > 0)
            {
                foreach (Control item in panInformation.Controls)
                {
                    item.Dispose();
                }
            }

            // create list view
            ListView lsv = new ListView();

            lsv.ColumnWidthChanging += CancelListViewResize;

            lsv.Width = panHistoryPath.Width;
            lsv.Height = panHistoryPath.Height;

            lsv.AllowColumnReorder = false;
            lsv.FullRowSelect = true;
            lsv.MultiSelect = false;
            lsv.View = View.Details;

            // this is stupid, we scan recursivly to then reverse
            List<Folder> ls = new List<Folder>();

            // scan
            BuildPathRecursivly(_currentFolder);

            // reverse
            ls.Reverse();

            // build columns
            BuildColumns(ls);

            /// <summary>
            /// Build the list view recursivly
            /// 
            /// </summary>
            /// <param name="parent">parent folder</param>
            void BuildPathRecursivly(Folder parent)
            {
                ls.Add(parent);

                if (parent.FolderType != Folder.Type.Root)
                {
                    BuildPathRecursivly(parent.ParentFolder);
                }
            }

            /// <summary>
            /// Build the columns
            /// 
            /// </summary>
            /// <param name="lsf">list of folder</param>
            void BuildColumns(List<Folder> lsf)
            {
                foreach (Folder item in lsf)
                {
                    lsv.Columns.Add(item.Name);
                }
            }

            // add to view
            panHistoryPath.Controls.Add(lsv);
        }

        /// <summary>
        /// On closing function
        /// 
        /// Occure when the form is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnClosing(object sender, EventArgs e)
        {
            Controller.WriteInHistory(_currentFolder.ObjectPath);
        }

        /// <summary>
        /// occure when the user click on an folder in the tree view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTreeViewObjectSelected(object sender, EventArgs e)
        {
            // casting
            TreeView obj = (TreeView)sender;
            Folder folder = obj.SelectedNode as Folder;

            ShowInformationSelectedListViewFolder(folder);
        }

        /// <summary>
        /// on list view object selected
        /// 
        /// occure when the user select a object in the list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnListViewObjectSelected(object sender, EventArgs e)
        {
            ListView obj = (ListView)sender;

            ListViewItem current = obj.SelectedItems[0];

            // check if user selected a folder or a file
            if (current.Name == nameof(Folder))
            {
                // find the folder with the same name as the one that is selected
                Folder selectedFolder = (Folder)FindObjectFolderViaNameFromListView(current);

                ShowInformationSelectedListViewFolder(selectedFolder);
            }
            else
            {
                // find the file with the same name as the one that is selected
                File selectedFile = (File)FindObjectFolderViaNameFromListView(current);

                ShowInformationSelectedListViewFile(selectedFile);
            }
        }

        /// <summary>
        /// find folder object that match the name of 
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="current">current list view item</param>
        /// <returns>FolderObject</returns>
        private FolderObject FindObjectFolderViaNameFromListView(ListViewItem current)
        {
            return _currentFolder.Files.Find(item => item.Name == current.SubItems[0].Text);
        }

        /// <summary>
        /// show information about the selected list view object
        /// </summary>
        /// <param name="obj">folder object</param>
        private void ShowInformationSelectedListViewFolder(Folder obj)
        {
            CreateListViewToShowInformation(new string[] {"Nom", "Fichiers", "Dossiers" }, 
                new string[] {obj.Name, obj.Files.Count.ToString(), obj.Folders.Count.ToString() });
        }

        /// <summary>
        /// show information about the selected list view object
        /// </summary>
        /// <param name="obj">folder object</param>
        private void ShowInformationSelectedListViewFile(File obj)
        {
            CreateListViewToShowInformation(new string[] { "Nom", "Type", "Taille en MB" }, 
                new string[] { obj.Name, obj.FileType.ToString(), (obj.ObjectData.Size / BITS_IN_MEGA_BYTE).ToString() });
        }

        /// <summary>
        /// Create a list view to display informations
        /// </summary>
        /// <param name="titles">title of the list view</param>
        /// <param name="infos">infos that goes in the columns of the list view</param>
        private void CreateListViewToShowInformation(string[] titles, string[] infos)
        {
            if (titles.Length != infos.Length)
            {
                throw new ArgumentException("You need the same amount of infos as titles");
            }

            // create list view
            ListView lsv = new ListView();

            lsv.Width = panInformation.Width;
            lsv.Height = panInformation.Height;

            lsv.AllowColumnReorder = true;
            lsv.FullRowSelect = true;
            lsv.MultiSelect = false;
            lsv.View = View.Details;

            // clear items from panel
            if (panInformation.Controls.Count > 0)
            {
                foreach (Control item in panInformation.Controls)
                {
                    item.Dispose();
                }
            }

            // add columns title
            foreach (string title in titles)
            {
                lsv.Columns.Add(title, -2);
            }

            // add infos
            ListViewItem lsvInfos = new ListViewItem(infos);
            lsv.Items.Add(lsvInfos);

            // add new list view
            panInformation.Controls.Add(lsv);
        }

        /// <summary>
        /// on list view ovjectr double click
        /// 
        /// occure when the user double click on a object in the list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnListViewObjectDoubleClick(object sender, EventArgs e)
        {
            ListView obj = (ListView)sender;

            ListViewItem current = obj.SelectedItems[0];

            // check if user selected a folder or a file
            if (current.Name == nameof(Folder))
            {
                // find the folder with the same name as the one that is selected
                Folder selectedFolder = _currentFolder.Folders.Find(item => item.Name == current.SubItems[0].Text);

                if (selectedFolder != null)
                {
                    ScanFolder(selectedFolder);
                }
                else
                {
                    throw new NotSupportedException($"There is no folder with the name {current.SubItems[0].Text} on the current branche of the tree view");
                }
            }
            else
            {
                // find the file with the same name as the one that is selected
                File selectedFile = _currentFolder.Files.Find(item => item.Name == current.SubItems[0].Text);
            }
        }

        /// <summary>
        /// Occure when the listener of a list view trigger the resize of a column
        /// 
        /// </summary>
        /// <param name="sender">list view</param>
        /// <param name="e"></param>
        private void CancelListViewResize(object sender, ColumnWidthChangingEventArgs e)
        {
            ListView ls = (ListView)sender;

            e.Cancel = true;
            e.NewWidth = ls.Columns[e.ColumnIndex].Width;
        }
        #endregion
    }
}
