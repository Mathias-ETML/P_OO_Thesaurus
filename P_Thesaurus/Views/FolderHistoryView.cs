﻿
/*
 * ETML
 * Clément Sartoni
 * 23.04.2021
 * Projet P_OO-Smart-Thésaurus
 * 
 */

using P_Thesaurus.AppBusiness.HistoryReader;
using P_Thesaurus.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace P_Thesaurus.Views
{

    /// <summary>
    /// Form permettant d'afficher l'historique des connexions FTP
    /// </summary>
    public partial class FolderHistoryView : HistoryView
    {
        #region Variables
        /// <summary>
        /// This view's controller
        /// </summary>
        public FolderController Controller { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FolderHistoryView()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Init function
        /// </summary>
        public void Init()
        {
            //InitializeComponent();

            historyListView.Items.Clear();

            List<DriveInfo> drives = Controller.GetAllDrives();

            // loop trough each drives
            foreach (DriveInfo item in drives)
            {
                // check if the drive is like a local drive
                if (item.IsReady && item.DriveType != DriveType.Network)
                {
                    //Convert bytes into Gigabytes when displaying disks
                    TreeNode node = new TreeNode("Nom : " + item.Name + "  Espace : " + item.AvailableFreeSpace / 1000000000);
                    node.Name = item.Name.Substring(0, 2);

                    // this is not the best way but we only do this once in the view, so it's pretty fine
                    foreach (TreeNode drive in driveTreeView.Nodes)
                    {
                        // check if item exist
                        if (drive.Name == node.Name)
                        {
                            node = null;
                            break;
                        }
                    }

                    if (node != null)
                    {
                        driveTreeView.Nodes.Add(node);
                    }
                }
            }

            SetHistory(Controller.GetHistory());

            driveTreeView.NodeMouseDoubleClick += OnDriveSelectionRoots;
        }

        

        /// <summary>
        /// OnDriveSelection function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDriveSelectionRoots(object sender, EventArgs e)
        {
            TreeView obj = (TreeView)sender;

            TreeNode selected = obj.SelectedNode;

            if (selected.Name != null)
            {
                // we are passing the path trough the node name, wich is a simple way if giving wich drive or folder the user wants
                Controller.LaunchFolderNavigationView(selected.Name);

                // stupid bug where the event is doubled
                driveTreeView.NodeMouseDoubleClick -= OnDriveSelectionRoots;
            }
        }

        /// <summary>
        /// Occure when the user click on a object of the history
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDriveSelectionHistory(object sender, EventArgs e)
        {
            string selected = historyListView.SelectedItems[0].Text;

            if (selected != null)
            {
                // we are passing the path trough the node name, wich is a simple way if giving wich drive or folder the user wants
                Controller.LaunchFolderNavigationView(selected);

                // stupid bug where the event is doubled
                driveTreeView.NodeMouseDoubleClick -= OnDriveSelectionRoots;
            }
        }

        /// <summary>
        /// Occure when the user click on the button to launch the folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLaunchFolder(object sender, EventArgs e)
        {
            if (Directory.Exists(txtBoxPath.Text))
            {
                Controller.LaunchFolderNavigationView(txtBoxPath.Text);

                // stupid bug where the event is doubled
                driveTreeView.NodeMouseDoubleClick -= OnDriveSelectionRoots;
            }
        }
        #endregion
    }
}
