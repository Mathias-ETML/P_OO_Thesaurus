
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
            this.SuspendLayout();
            // 
            // folderTreeView
            // 
            this.folderTreeView.Location = new System.Drawing.Point(71, 109);
            this.folderTreeView.Name = "folderTreeView";
            this.folderTreeView.Size = new System.Drawing.Size(217, 329);
            this.folderTreeView.TabIndex = 0;
            this.folderTreeView.DoubleClick += new System.EventHandler(this.OnFolderChange);
            // 
            // currentFolderListView
            // 
            this.currentFolderListView.GridLines = true;
            this.currentFolderListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.currentFolderListView.HideSelection = false;
            this.currentFolderListView.Location = new System.Drawing.Point(338, 188);
            this.currentFolderListView.Name = "currentFolderListView";
            this.currentFolderListView.Size = new System.Drawing.Size(400, 250);
            this.currentFolderListView.TabIndex = 1;
            this.currentFolderListView.UseCompatibleStateImageBehavior = false;
            this.currentFolderListView.VirtualListSize = 4;
            this.currentFolderListView.DoubleClick += new System.EventHandler(this.OnListViewObjectSelected);
            // 
            // FolderNavigationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.currentFolderListView);
            this.Controls.Add(this.folderTreeView);
            this.Name = "FolderNavigationView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView folderTreeView;
        private System.Windows.Forms.ListView currentFolderListView;
    }
}
