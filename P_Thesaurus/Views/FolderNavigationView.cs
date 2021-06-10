/*
 * ETML
 * Clément Sartoni
 * 23.04.2021
 * Projet P_OO-Smart-Thésaurus
 * Form permettant d'afficher la navigation dans les fichiers FTP ou windows
 */


using P_Thesaurus.AppBusiness.EnumsAndStructs;
using P_Thesaurus.AppBusiness.WIN32;
using P_Thesaurus.Controllers;
using P_Thesaurus.Models.WIN32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace P_Thesaurus.Views
{
    /// <summary>
    /// View that shows navigation in Windows Files
    /// </summary>
    public partial class FolderNavigationView : NavigationView
    {
        #region Variables
        private const int BITS_IN_MEGA_BYTE = 1024;
        private string _path;
        private Folder _currentFolder;
        private Folder _root;
        private List<File.Type> _filters;
        private bool _researchMode = false;
        private List<ResearchElement> _foundItems;

        /// <summary>
        /// the view's controller
        /// </summary>
        public FolderController Controller {  get; set; }
        #endregion

        #region delegates
        /// <summary>
        /// AddNodeToNodeViaInvokeDelegate field
        /// </summary>
        public delegate void AddNodeToNodeViaInvokeDelegate(TreeNode parent, TreeNode child);

        /// <summary>
        /// ScanEnded field
        /// </summary>
        public delegate void ResearchEndedDelegate(List<ResearchElement> elements);
        #endregion delegates

        #region Public Methods
        /// <summary>
        /// Default Controller
        /// </summary>
        public FolderNavigationView(string path)
        {
            this.FormClosing += OnClosing;

            this._path = path;

            this._foundItems = new List<ResearchElement>();

            InitializeComponent();

            this.filterChckdLstBox.Visible = false;

            this._filters = new List<File.Type>();
        }

        /// <summary>
        /// Init function
        /// </summary>
        public void Init()
        {
            // get current folder at the end of the path
            _currentFolder = Controller.GetFolder(_path);

            if (!_currentFolder.IsRootFolder)
            {
                // getting root folder of the athx
                _root = Controller.GetRootFolderRecursivly(_currentFolder);

                // adding root folder at the tree view
                folderTreeView.Nodes.Add(_root);
            }
            else
            {
                _root = _currentFolder;

                // adding root folder at the tree view
                folderTreeView.Nodes.Add(_currentFolder);
            }

            // scannig current folder
            ScanFolder(_currentFolder);
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
            // if this is true, it mean that we are at the bottom of the scanned system
            if (!folder.Folders.Contains(_currentFolder))
            {
                // check if we have 1 folder, else there is a litle problem
                if (folder.Folders.Count == 1)
                {
                    // we get the child folder and we redo this again
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

            AddItemsCurrentListView();
        }

        /// <summary>
        /// add item in the current list view
        /// </summary>
        private void AddItemsCurrentListView()
        {
            // check if the user has selected filters
            // if yes, we don't show the folders
            // caus they are in the tree view
            if (_filters.Count == 0)
            {
                // add the objects in the list view
                foreach (Folder item in _currentFolder.Folders)
                {
                    ListViewItem items = GetFormatedFolderItem(item);

                    // what do we do here is that we assign the typeof the list view item via the name of the list view
                    items.Name = nameof(Folder);

                    currentFolderListView.Items.Add(items);
                }
            }

            foreach (File file in _currentFolder.Files)
            {
                // check if we can add the item to the current list view
                if (_filters.Contains(file.FileType) || _filters.Count == 0)
                {
                    ListViewItem items = GetFormatedFileItem(file);

                    // what do we do here is that we assign the typeof the list view item via the name of the list view
                    items.Name = nameof(File);

                    currentFolderListView.Items.Add(items);
                }
            }

            _currentFolder.Expand();
        }

        /// <summary>
        /// Get you a formated list view item
        /// </summary>
        /// <param name="folder">folder</param>
        /// <returns>ListViewItem</returns>
        private ListViewItem GetFormatedFolderItem(Folder folder)
        {
            ListViewItem buffer = new ListViewItem(new string[] {
                                    folder.Name,
                                    "Dossier",
                                    folder.ObjectData.ModifyTime.ToLongDateString() });

            buffer.Name = nameof(Folder);

            return buffer;
        }

        /// <summary>
        /// Get formated list view item
        /// </summary>
        /// <param name="file">file</param>
        /// <returns>ListViewItem</returns>
        private ListViewItem GetFormatedFileItem(File file)
        {
            return new ListViewItem(new string[] {
                                    file.Name,
                                    "Fichier " + file.FileType,
                                    file.ObjectData.ModifyTime.ToLongDateString() });
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
        /// occure when the user click on another folder
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
            _root.Collapse();

            _currentFolder = folder;

            // function pointer
            FolderScan.OnFolderScanEnd onScanEnd = new FolderScan.OnFolderScanEnd(ScanEnd);

            // function pointer
            AddNodeToNodeViaInvokeDelegate invoke = new AddNodeToNodeViaInvokeDelegate(AddNodeToNodeViaInvoke);

            // scannig current folder
            Controller.StartScan(ref folder, onScanEnd, invoke);

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
            panHistoryPath.Controls.Clear();

            // create list view
            ListView lsv = new ListView();

            lsv.ColumnWidthChanging += CancelListViewResize;
            lsv.ColumnClick += OnListViewColumnClicked;

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

            // add folder to name to list
            foreach (Folder item in ls)
            {
                lsv.Columns.Add(item.Name);
            }

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
                Folder selectedFolder;

                // in research mode, the current folder doesn't always contain the wanted file, so we need to check in the list of founded file
                if (_researchMode)
                {
                    // check if the object is a folder and the name match
                    selectedFolder = (Folder)_foundItems.Find(item => item.Object.Name == current.SubItems[0].Text && item.Type == typeof(Folder)).Object;
                }
                else
                {
                    // find the folder with the same name as the one that is selected
                    selectedFolder = (Folder)FindObjectFolderViaNameFromListView(current);
                }

                ShowInformationSelectedListViewFolder(selectedFolder);

            }
            else
            {
                File selectedFile;

                // in research mode, the current folder doesn't always contain the wanted file, so we need to check in the list of founded file
                if (_researchMode)
                {
                    // check if the object is a file and the name match
                    selectedFile = (File)_foundItems.Find(item => item.Object.Name == current.SubItems[0].Text && item.Type == typeof(File)).Object;
                }
                else
                {
                    // find the file with the same name as the one that is selected
                    selectedFile = (File)FindObjectFolderViaNameFromListView(current);
                }

                ShowInformationSelectedListViewFile(selectedFile);
            }
        }

        /// <summary>
        /// find folder object that match the name of list view item
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="current">current list view item</param>
        /// <returns>FolderObject or null if not found</returns>
        private FolderObject FindObjectFolderViaNameFromListView(ListViewItem current)
        {
            // simple way to make a foreach lopp in 1 line
            return _currentFolder.FolderObjectsList.Find(item => item.Name == current.SubItems[0].Text);
        }

        /// <summary>
        /// find folder that match name of input
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Folder or null if not found</returns>
        private Folder FindFolderFromListViewViaName(string name)
        {
            if (_currentFolder.IsRootFolder)
            {
                return _currentFolder;
            }

            return FindFolder(_currentFolder.ParentFolder);

            /// <summary>
            /// find folder that match name of input
            /// </summary>
            /// <param folder="parent">name</param>
            /// <returns>Folder or null if not found</returns>
            Folder FindFolder(Folder parent)
            {
                if (!parent.IsRootFolder && parent.Name != name)
                {
                    return FindFolder(parent.ParentFolder);
                }

                return parent;
            }
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
                if (_researchMode)
                {
                    _researchMode = false;

                    currentFolderListView.Items.Clear();

                    // find the selected item with the matching name of all of them
                    FolderObject flo = _foundItems.Find(item => item.Object.Name == current.SubItems[0].Text).Object;

                    // check if user click on a file or folder
                    if ((flo as Folder) != null)
                    {
                        // reset list
                        _foundItems = null;

                        // user want to open the folder
                        Folder item = (Folder)flo;

                        ScanFolder(item);
                    }
                    else
                    {
                        // else the user want to open the file
                        File item = (File)flo;

                        Process.Start(item.ObjectPath);
                    }
                }
                else
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
            }
            else
            {
                // find the file with the same name as the one that is selected
                File selectedFile = _currentFolder.Files.Find(item => item.Name == current.SubItems[0].Text);

                Process.Start(selectedFile.ObjectPath);
            }
        }

        /// <summary>
        /// Occure when the a list view trigger the resize of a column
        /// Need to add as listener to work
        /// </summary>
        /// <param name="sender">list view</param>
        /// <param name="e"></param>
        private void CancelListViewResize(object sender, ColumnWidthChangingEventArgs e)
        {
            ListView ls = (ListView)sender;

            e.Cancel = true;
            e.NewWidth = ls.Columns[e.ColumnIndex].Width;
        }

        /// <summary>
        /// Occure when the a list view trigger the click on a list view column title
        /// Need to add as listener to work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnListViewColumnClicked(object sender, ColumnClickEventArgs e)
        {
            ListView ls = (ListView)sender;

            Folder fl = FindFolderFromListViewViaName(ls.Columns[e.Column].Text);

            // if null, then user clicked on the column of the actual folder
            if (fl != null)
            {
                ScanFolder(fl);
            }
        }

        /// <summary>
        /// Occure when the user click on the filter button
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFiltre_Click(object sender, EventArgs e)
        {
            // check if the user entered a word to be research, so we don't show the filters
            if (txtBoxObjectName.Text.Length > 0)
            {
                ResearchObjectRecursivly();

                txtBoxObjectName.Text = "";

                return;
            }

            if (filterChckdLstBox.Visible)
            {
                filterChckdLstBox.Visible = false;
            }
            else
            {
                filterChckdLstBox.Visible = true;
            }
        }

        /// <summary>
        /// Occure when the user click on a item from the list of item to check
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterChckdLstBox_ItemCheck(object sender, EventArgs e)
        {
            // yes, this is stupid
            // but we need to compare to file type
            File.Type name = (File.Type)Enum.Parse(typeof(File.Type), filterChckdLstBox.SelectedItem.ToString(), true);

            if (_filters.Contains(name))
            {
                _filters.Remove(name);
            }
            else
            {
                _filters.Add(name);
            }

            ResetListView();

            AddItemsCurrentListView();
        }

        /// <summary>
        /// research an object from the current folder recursivly
        /// </summary>
        private void ResearchObjectRecursivly()
        {
            _researchMode = true;

            currentFolderListView.Items.Clear();
            currentFolderListView.Items.Add(new ListViewItem("Recherche en cours"));

            // split for the + and trim the spaces, caus doesn't work
            List<string> terms = new List<string>(txtBoxObjectName.Text.Split(new string[1] { "+" }, StringSplitOptions.RemoveEmptyEntries));

            for (int i = 0; i < terms.Count; i++)
            {
                terms[i] = terms[i].Trim();
            }

            AddNodeToNodeViaInvokeDelegate invoke = new AddNodeToNodeViaInvokeDelegate(AddNodeToNodeViaInvoke);
            ResearchEndedDelegate end = new ResearchEndedDelegate(ResearchEnded);

            // get all the found items and sort them via the ratio
            Controller.GetObjectRecursivly(_currentFolder, terms, AddNodeToNodeViaInvoke, ResearchEnded, true);
        }

        /// <summary>
        /// Occure when the user press a key in the text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtBoxResearchKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ResearchObjectRecursivly();
            }
        }

        /// <summary>
        /// FolderScanEnd field
        /// </summary>
        private void AddNodeToNodeViaInvoke(TreeNode parent, TreeNode child)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => parent.Nodes.Add(child)));
            }
            else
            {
                parent.Nodes.Add(child);
            }
        }

        /// <summary>
        /// ResearchEnded function
        /// 
        /// call when all the folders have been scanned
        /// </summary>
        private void ResearchEnded(List<ResearchElement> elements)
        {
            if (_researchMode)
            {
                // we don't want to clear and sort and do all sort
                // and do all sort of useless stuff
                if (_foundItems.Count > elements.Count)
                {
                    return;
                }

                _foundItems = elements;
                _foundItems.Sort(CompareObject);

                /// <summary>
                /// compare function
                /// </summary>
                int CompareObject(ResearchElement first, ResearchElement other)
                {
                    if (first.Ratio == other.Ratio)
                    {
                        return 0;
                    }

                    if (first.Ratio < other.Ratio)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }

                // check if we have found something
                if (_foundItems.Count > 0)
                {
                    Invoke(new Action(() => currentFolderListView.Items.Clear()));

                    for (int i = 0; i < _foundItems.Count; i++)
                    {
                        ResearchElement item = _foundItems[i];

                        // check if we got a folder or file
                        if ((item.Object as Folder) != null)
                        {
                            Folder obj = (Folder)item.Object;

                            Invoke(new Action(() => currentFolderListView.Items.Add(GetFormatedFolderItem(obj))));
                        }
                        else
                        {
                            File obj = (File)item.Object;

                            Invoke(new Action(() => currentFolderListView.Items.Add(GetFormatedFileItem(obj))));
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Aucun résultat trouvé");

                    Invoke(new Action(() => currentFolderListView.Items.Clear()));
                }
            }
        }
        #endregion
    }
}
