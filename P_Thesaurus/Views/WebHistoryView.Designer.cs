
namespace P_Thesaurus.Views
{
    partial class WebHistoryView
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
            this.SuspendLayout();
            // 
            // historyListView
            // 
            this.historyListView.Location = new System.Drawing.Point(68, 139);
            this.historyListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.historyListView.Size = new System.Drawing.Size(458, 313);
            this.historyListView.SelectedIndexChanged += new System.EventHandler(this.OnDriveSelectionHistory);
            // 
            // txtBoxPath
            // 
            this.txtBoxPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // columnHeaderPath
            // 
            this.columnHeaderPath.Width = 340;
            // 
            // columnHeaderDate
            // 
            this.columnHeaderDate.Width = 240;
            // 
            // WebHistoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 498);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "WebHistoryView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historique Web";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}