﻿
namespace P_Thesaurus.Views
{
    partial class HistoryView
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
            this.historyListView = new System.Windows.Forms.ListView();
            this.columnHeaderPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PathLabel = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtBoxPath = new System.Windows.Forms.TextBox();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // historyListView
            // 
            this.historyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPath,
            this.columnHeaderDate});
            this.historyListView.GridLines = true;
            this.historyListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.historyListView.HideSelection = false;
            this.historyListView.Location = new System.Drawing.Point(247, 139);
            this.historyListView.Name = "historyListView";
            this.historyListView.Size = new System.Drawing.Size(279, 313);
            this.historyListView.TabIndex = 2;
            this.historyListView.UseCompatibleStateImageBehavior = false;
            this.historyListView.VirtualListSize = 4;
            // 
            // columnHeaderPath
            // 
            this.columnHeaderPath.Text = "Chemin";
            this.columnHeaderPath.Width = 200;
            // 
            // columnHeaderDate
            // 
            this.columnHeaderDate.Text = "Date";
            // 
            // PathLabel
            // 
            this.PathLabel.AutoSize = true;
            this.PathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PathLabel.Location = new System.Drawing.Point(65, 53);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(163, 18);
            this.PathLabel.TabIndex = 3;
            this.PathLabel.Text = "url / chemin (à replacer)";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(13, 13);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(41, 29);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "<----";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.BtnBackClick);
            // 
            // txtBoxPath
            // 
            this.txtBoxPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxPath.Location = new System.Drawing.Point(68, 74);
            this.txtBoxPath.Name = "txtBoxPath";
            this.txtBoxPath.Size = new System.Drawing.Size(411, 21);
            this.txtBoxPath.TabIndex = 5;
            this.txtBoxPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxPathKeyDown);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(485, 74);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(41, 21);
            this.btnOpenFolder.TabIndex = 6;
            this.btnOpenFolder.Text = "GO";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            // 
            // lblHistoryTitle
            // 
            this.lblHistoryTitle.AutoSize = true;
            this.lblHistoryTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistoryTitle.Location = new System.Drawing.Point(244, 118);
            this.lblHistoryTitle.Name = "lblHistoryTitle";
            this.lblHistoryTitle.Size = new System.Drawing.Size(75, 18);
            this.lblHistoryTitle.TabIndex = 7;
            this.lblHistoryTitle.Text = "Historique";
            // 
            // HistoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 484);
            this.Controls.Add(this.lblHistoryTitle);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.txtBoxPath);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.PathLabel);
            this.Controls.Add(this.historyListView);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "HistoryView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HistoryView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBack;
        protected System.Windows.Forms.ListView historyListView;
        private System.Windows.Forms.Label lblHistoryTitle;
        public System.Windows.Forms.Label PathLabel;
        protected System.Windows.Forms.Button btnOpenFolder;
        protected System.Windows.Forms.TextBox txtBoxPath;
        protected System.Windows.Forms.ColumnHeader columnHeaderPath;
        protected System.Windows.Forms.ColumnHeader columnHeaderDate;
    }
}