
namespace P_Thesaurus.Views
{
    partial class FolderNavigationView
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderTreeView = new System.Windows.Forms.TreeView();
            this.currentFolderListView = new System.Windows.Forms.ListView();
            this.panInformation = new System.Windows.Forms.Panel();
            this.panHistoryPath = new System.Windows.Forms.Panel();
            this.txtBoxObjectName = new System.Windows.Forms.TextBox();
            this.lblRecherche = new System.Windows.Forms.Label();
            this.filterChckdLstBox = new System.Windows.Forms.CheckedListBox();
            this.btnFiltre = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // folderTreeView
            // 
            this.folderTreeView.Location = new System.Drawing.Point(68, 164);
            this.folderTreeView.Name = "folderTreeView";
            this.folderTreeView.Size = new System.Drawing.Size(217, 353);
            this.folderTreeView.TabIndex = 0;
            this.folderTreeView.Click += new System.EventHandler(this.OnTreeViewObjectSelected);
            this.folderTreeView.DoubleClick += new System.EventHandler(this.OnTreeViewFolderChange);
            // 
            // currentFolderListView
            // 
            this.currentFolderListView.GridLines = true;
            this.currentFolderListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.currentFolderListView.HideSelection = false;
            this.currentFolderListView.Location = new System.Drawing.Point(335, 267);
            this.currentFolderListView.Name = "currentFolderListView";
            this.currentFolderListView.Size = new System.Drawing.Size(400, 250);
            this.currentFolderListView.TabIndex = 1;
            this.currentFolderListView.UseCompatibleStateImageBehavior = false;
            this.currentFolderListView.VirtualListSize = 4;
            this.currentFolderListView.Click += new System.EventHandler(this.OnListViewObjectSelected);
            this.currentFolderListView.DoubleClick += new System.EventHandler(this.OnListViewObjectDoubleClick);
            // 
            // panInformation
            // 
            this.panInformation.BackColor = System.Drawing.Color.White;
            this.panInformation.Location = new System.Drawing.Point(335, 164);
            this.panInformation.Name = "panInformation";
            this.panInformation.Size = new System.Drawing.Size(400, 84);
            this.panInformation.TabIndex = 2;
            // 
            // panHistoryPath
            // 
            this.panHistoryPath.BackColor = System.Drawing.Color.White;
            this.panHistoryPath.Location = new System.Drawing.Point(68, 99);
            this.panHistoryPath.Name = "panHistoryPath";
            this.panHistoryPath.Size = new System.Drawing.Size(667, 49);
            this.panHistoryPath.TabIndex = 3;
            // 
            // txtBoxObjectName
            // 
            this.txtBoxObjectName.Location = new System.Drawing.Point(68, 64);
            this.txtBoxObjectName.Name = "txtBoxObjectName";
            this.txtBoxObjectName.Size = new System.Drawing.Size(545, 20);
            this.txtBoxObjectName.TabIndex = 4;
            this.txtBoxObjectName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxResearchKeyPress);
            // 
            // lblRecherche
            // 
            this.lblRecherche.AutoSize = true;
            this.lblRecherche.Location = new System.Drawing.Point(65, 38);
            this.lblRecherche.Name = "lblRecherche";
            this.lblRecherche.Size = new System.Drawing.Size(60, 13);
            this.lblRecherche.TabIndex = 5;
            this.lblRecherche.Text = "Recherche";
            // 
            // filterChckdLstBox
            // 
            this.filterChckdLstBox.CheckOnClick = true;
            this.filterChckdLstBox.FormattingEnabled = true;
            this.filterChckdLstBox.Items.AddRange(new object[] {
            "None",
            "Text",
            "Office",
            "Image",
            "Music",
            "Video",
            "Compressed",
            "Executable",
            "Code",
            "Shortcut",
            "System",
            "Other"});
            this.filterChckdLstBox.Location = new System.Drawing.Point(621, 84);
            this.filterChckdLstBox.Name = "filterChckdLstBox";
            this.filterChckdLstBox.Size = new System.Drawing.Size(114, 184);
            this.filterChckdLstBox.TabIndex = 6;
            this.filterChckdLstBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.FilterChckdLstBox_ItemCheck);
            // 
            // btnFiltre
            // 
            this.btnFiltre.Location = new System.Drawing.Point(620, 63);
            this.btnFiltre.Name = "btnFiltre";
            this.btnFiltre.Size = new System.Drawing.Size(116, 22);
            this.btnFiltre.TabIndex = 7;
            this.btnFiltre.Text = "Fitrer";
            this.btnFiltre.UseVisualStyleBackColor = true;
            this.btnFiltre.Click += new System.EventHandler(this.BtnFiltre_Click);
            // 
            // FolderNavigationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(800, 529);
            this.Controls.Add(this.btnFiltre);
            this.Controls.Add(this.filterChckdLstBox);
            this.Controls.Add(this.lblRecherche);
            this.Controls.Add(this.txtBoxObjectName);
            this.Controls.Add(this.panHistoryPath);
            this.Controls.Add(this.panInformation);
            this.Controls.Add(this.currentFolderListView);
            this.Controls.Add(this.folderTreeView);
            this.MaximumSize = new System.Drawing.Size(816, 568);
            this.MinimumSize = new System.Drawing.Size(816, 568);
            this.Name = "FolderNavigationView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recherche Dossier";
            this.Controls.SetChildIndex(this.folderTreeView, 0);
            this.Controls.SetChildIndex(this.currentFolderListView, 0);
            this.Controls.SetChildIndex(this.panInformation, 0);
            this.Controls.SetChildIndex(this.panHistoryPath, 0);
            this.Controls.SetChildIndex(this.txtBoxObjectName, 0);
            this.Controls.SetChildIndex(this.lblRecherche, 0);
            this.Controls.SetChildIndex(this.filterChckdLstBox, 0);
            this.Controls.SetChildIndex(this.btnFiltre, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView folderTreeView;
        private System.Windows.Forms.ListView currentFolderListView;
        private System.Windows.Forms.Panel panInformation;
        private System.Windows.Forms.Panel panHistoryPath;
        private System.Windows.Forms.TextBox txtBoxObjectName;
        private System.Windows.Forms.Label lblRecherche;
        private System.Windows.Forms.CheckedListBox filterChckdLstBox;
        private System.Windows.Forms.Button btnFiltre;
    }
}
