
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
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.modification = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.taille = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.currentFolderListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.type,
            this.modification,
            this.taille});
            this.currentFolderListView.GridLines = true;
            this.currentFolderListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.currentFolderListView.HideSelection = false;
            this.currentFolderListView.Location = new System.Drawing.Point(338, 188);
            this.currentFolderListView.Name = "currentFolderListView";
            this.currentFolderListView.Size = new System.Drawing.Size(400, 250);
            this.currentFolderListView.TabIndex = 1;
            this.currentFolderListView.UseCompatibleStateImageBehavior = false;
            this.currentFolderListView.VirtualListSize = 4;
            // 
            // name
            // 
            this.name.Text = "Nom";
            this.name.Width = 150;
            // 
            // type
            // 
            this.type.Text = "Type";
            this.type.Width = 100;
            // 
            // modification
            // 
            this.modification.Text = "Modification Date";
            this.modification.Width = 100;
            // 
            // taille
            // 
            this.taille.Text = "Taille";
            this.taille.Width = 40;
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
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader modification;
        private System.Windows.Forms.ColumnHeader taille;
    }
}
