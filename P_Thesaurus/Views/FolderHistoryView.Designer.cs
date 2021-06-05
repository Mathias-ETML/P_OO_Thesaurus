
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
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // historyListView
            // 
            this.historyListView.DoubleClick += new System.EventHandler(this.OnDriveSelectionHistory);
            // 
            // driveTreeView
            // 
            this.driveTreeView.Location = new System.Drawing.Point(68, 139);
            this.driveTreeView.Name = "driveTreeView";
            this.driveTreeView.Size = new System.Drawing.Size(153, 154);
            this.driveTreeView.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(65, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Disques";
            // 
            // FolderHistoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 484);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.driveTreeView);
            this.MaximumSize = new System.Drawing.Size(490, 376);
            this.MinimumSize = new System.Drawing.Size(490, 376);
            this.Name = "FolderHistoryView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FolderHistoryView";
            this.Controls.SetChildIndex(this.historyListView, 0);
            this.Controls.SetChildIndex(this.driveTreeView, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView driveTreeView;
        private System.Windows.Forms.Label label2;
    }
}