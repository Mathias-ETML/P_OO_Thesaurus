
namespace P_Thesaurus.Views
{
    partial class FolderHistoryView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.driveTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // historyListView
            // 
            this.historyListView.DoubleClick += new System.EventHandler(this.OnDriveSelectionHistory);
            // 
            // driveTreeView
            // 
            this.driveTreeView.Location = new System.Drawing.Point(39, 207);
            this.driveTreeView.Name = "driveTreeView";
            this.driveTreeView.Size = new System.Drawing.Size(153, 154);
            this.driveTreeView.TabIndex = 1;
            // 
            // FolderHistoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 484);
            this.Controls.Add(this.driveTreeView);
            this.Name = "FolderHistoryView";
            this.Text = "FolderHistoryView";
            this.Controls.SetChildIndex(this.historyListView, 0);
            this.Controls.SetChildIndex(this.driveTreeView, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView driveTreeView;
    }
}