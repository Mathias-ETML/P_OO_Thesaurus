/*
 * ETML
 * Clément Sartoni
 * 23.04.2021
 * Projet P_OO-Smart-Thésaurus
 * Form permettant d'afficher la navigation dans les fichiers FTP ou windows
 */


using P_Thesaurus.AppBusiness.WIN32;
using P_Thesaurus.Controllers;
using System;
using System.Windows.Forms;

namespace P_Thesaurus.Views
{
    /// <summary>
    /// View that shows navigation in FTP or Windows Files
    /// </summary>
    public partial class FolderNavigationView : NavigationView
    {
        #region Variables
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

            this.currentFolderListView.AllowColumnReorder = true;
            this.currentFolderListView.FullRowSelect = true;
            this.currentFolderListView.MultiSelect = false;
            this.currentFolderListView.View = View.Details;
        }

        /// <summary>
        /// Init function
        /// </summary>
        public void Init()
        {
            // get current folder at the end of the path
            _currentFolder = Controller.GetFolder(_path);

            // getting root folder of the ath
            Folder root = Controller.GetRootFolderRecursivly(_currentFolder);

            // adding root folder at the tree view
            folderTreeView.Nodes.Add(root);

            // scanning the root from top to bottom
            ScanRootFolderRecursivly(root);

            // getting current tree node cause pointers
            TreeNode currentNode = _currentFolder;

            // function pointer
            Delegate onScanEnd = new Models.WIN32.FolderScan.OnFolderScanEnd(ScanEnd);

            // scannig current folder
            Controller.StartScan(ref _currentFolder, ref currentNode, onScanEnd);
        }

        /// <summary>
        /// Scan root folder recursivly and add each children to tree view
        /// </summary>
        /// <param name="root">folder</param>
        private void ScanRootFolderRecursivly(Folder folder)
        {
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
            // reset the list view
            currentFolderListView.Clear();
            currentFolderListView.Columns.Add("Name", 150, HorizontalAlignment.Left);
            currentFolderListView.Columns.Add("Type", 100, HorizontalAlignment.Left);
            currentFolderListView.Columns.Add("Modification", 100, HorizontalAlignment.Left);
            currentFolderListView.Columns.Add("Taille", 100, HorizontalAlignment.Left);

            // add the objects in the list view
            foreach (Folder item in _currentFolder.Folders)
            {
                ListViewItem items = new ListViewItem(new string[] {
                                                    item.Name,
                                                    "Dossier",
                                                    item.FolderData.ModifyTime.ToLongDateString(),
                                                    item.FolderData.Size.ToString() });

                currentFolderListView.Items.Add(items);
            }

            foreach (File file in _currentFolder.Files)
            {
                ListViewItem items = new ListViewItem(new string[] {
                                                        file.Name,
                                                        "Fichier " + file.FileType,
                                                        file.FolderData.ModifyTime.ToLongDateString(),
                                                        file.FolderData.Size.ToString() });

                currentFolderListView.Items.Add(items);
            }
        }

        /// <summary>
        /// OnFolderChange function
        /// 
        /// occure when the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFolderChange(object sender, EventArgs e)
        {
            // casting
            TreeView obj = (TreeView)sender;
            Folder folder = obj.SelectedNode as Folder;

            // check if user clicked on a folder, else it's a file
            if (folder != null)
            {
                TreeNode node = folder; // this is stupid, but i am not a compilator so yea

                _currentFolder = folder;

                // function pointer
                Delegate onScanEnd = new Models.WIN32.FolderScan.OnFolderScanEnd(ScanEnd);

                // scannig current folder
                Controller.StartScan(ref folder, ref node, onScanEnd);

                node.Expand();
            }
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
            Controller.WriteInHistory(_currentFolder.Path);
        }
        #endregion
    }
}
