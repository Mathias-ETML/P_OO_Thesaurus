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
            List<DriveInfo> drives = Controller.GetAllDrives();

            foreach (DriveInfo item in drives)
            {
                if (item.IsReady && item.DriveType != DriveType.Network)
                {
                    TreeNode node = new TreeNode("Nom : " + item.Name + "  Espace : " + item.AvailableFreeSpace / 1000000000);

                    node.Name = item.Name.Substring(0, 2);

                    driveTreeView.Nodes.Add(node);
                }
            }

            List<HistoryEntry> history = Controller.GetHistory();

            if (history.Count == 0)
            {
                TreeNode node = new TreeNode("Aucun dossier disponible");
                node.Name = null;

                historyTreeView.Nodes.Add(node);
            }
            else
            {
                foreach (HistoryEntry item in history)
                {
                    TreeNode node = new TreeNode("Dossier : " + item.Content + "  Date : " + item.DateTime);
                    node.Name = item.Content;

                    historyTreeView.Nodes.Add(node);
                }
            }

            driveTreeView.NodeMouseDoubleClick += OnDriveSelection;
            historyTreeView.NodeMouseDoubleClick += OnDriveSelection;
        }

        /// <summary>
        /// OnDriveSelection function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDriveSelection(object sender, EventArgs e)
        {
            TreeView obj = (TreeView)sender;

            TreeNode selected = obj.SelectedNode;

            if (selected.Name != null)
            {
                // we are passing the path trough the node name, wich is a simple way if giving wich drive or folder the user wants
                Controller.LaunchFolderNavigationView(selected.Name);
            }
            
        }
        #endregion
    }
}
