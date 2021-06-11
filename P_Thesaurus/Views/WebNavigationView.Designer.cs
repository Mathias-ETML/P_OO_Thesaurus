
namespace P_Thesaurus.Views
{
    partial class WebNavigationView
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
            this.components = new System.ComponentModel.Container();
            this._listView = new System.Windows.Forms.ListView();
            this.URL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblCurrentUrl = new System.Windows.Forms.Label();
            this.btnFiltre = new System.Windows.Forms.Button();
            this.filterChckdLstBox = new System.Windows.Forms.CheckedListBox();
            this.txbSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txbUrl = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _listView
            // 
            this._listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.URL,
            this.Type});
            this._listView.GridLines = true;
            this._listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._listView.HideSelection = false;
            this._listView.Location = new System.Drawing.Point(159, 113);
            this._listView.Margin = new System.Windows.Forms.Padding(4);
            this._listView.Name = "_listView";
            this._listView.Size = new System.Drawing.Size(751, 425);
            this._listView.TabIndex = 2;
            this._listView.UseCompatibleStateImageBehavior = false;
            this._listView.VirtualListSize = 4;
            this._listView.SelectedIndexChanged += new System.EventHandler(this._listView_SelectedIndexChanged);
            // 
            // URL
            // 
            this.URL.Text = "URL";
            this.URL.Width = 470;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            // 
            // lblCurrentUrl
            // 
            this.lblCurrentUrl.AutoSize = true;
            this.lblCurrentUrl.Location = new System.Drawing.Point(156, 88);
            this.lblCurrentUrl.Name = "lblCurrentUrl";
            this.lblCurrentUrl.Size = new System.Drawing.Size(44, 17);
            this.lblCurrentUrl.TabIndex = 7;
            this.lblCurrentUrl.Text = "URL :";
            // 
            // btnFiltre
            // 
            this.btnFiltre.Location = new System.Drawing.Point(790, 42);
            this.btnFiltre.Margin = new System.Windows.Forms.Padding(4);
            this.btnFiltre.Name = "btnFiltre";
            this.btnFiltre.Size = new System.Drawing.Size(120, 27);
            this.btnFiltre.TabIndex = 10;
            this.btnFiltre.Text = "Fitrer";
            this.btnFiltre.UseVisualStyleBackColor = true;
            this.btnFiltre.Click += new System.EventHandler(this.BtnFiltreClick);
            // 
            // filterChckdLstBox
            // 
            this.filterChckdLstBox.CheckOnClick = true;
            this.filterChckdLstBox.FormattingEnabled = true;
            this.filterChckdLstBox.Location = new System.Drawing.Point(791, 67);
            this.filterChckdLstBox.Margin = new System.Windows.Forms.Padding(4);
            this.filterChckdLstBox.Name = "filterChckdLstBox";
            this.filterChckdLstBox.Size = new System.Drawing.Size(119, 55);
            this.filterChckdLstBox.TabIndex = 9;
            this.filterChckdLstBox.Visible = false;
            this.filterChckdLstBox.SelectedIndexChanged += new System.EventHandler(this.FilterChckdLstBoxItemCheck);
            // 
            // txbSearch
            // 
            this.txbSearch.Location = new System.Drawing.Point(159, 44);
            this.txbSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Size = new System.Drawing.Size(623, 22);
            this.txbSearch.TabIndex = 8;
            this.txbSearch.TextChanged += new System.EventHandler(this.TxbSearchTextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Recherche";
            // 
            // txbUrl
            // 
            this.txbUrl.Location = new System.Drawing.Point(207, 85);
            this.txbUrl.Margin = new System.Windows.Forms.Padding(4);
            this.txbUrl.Name = "txbUrl";
            this.txbUrl.Size = new System.Drawing.Size(576, 22);
            this.txbUrl.TabIndex = 12;
            this.txbUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxbUrlKeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copierToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 28);
            // 
            // copierToolStripMenuItem
            // 
            this.copierToolStripMenuItem.Name = "copierToolStripMenuItem";
            this.copierToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.copierToolStripMenuItem.Text = "Copier";
            // 
            // WebNavigationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.txbUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFiltre);
            this.Controls.Add(this.filterChckdLstBox);
            this.Controls.Add(this.txbSearch);
            this.Controls.Add(this.lblCurrentUrl);
            this.Controls.Add(this._listView);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "WebNavigationView";
            this.Text = "-";
            this.Controls.SetChildIndex(this._listView, 0);
            this.Controls.SetChildIndex(this.lblCurrentUrl, 0);
            this.Controls.SetChildIndex(this.txbSearch, 0);
            this.Controls.SetChildIndex(this.filterChckdLstBox, 0);
            this.Controls.SetChildIndex(this.btnFiltre, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txbUrl, 0);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView _listView;
        private System.Windows.Forms.ColumnHeader URL;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.Label lblCurrentUrl;
        private System.Windows.Forms.Button btnFiltre;
        private System.Windows.Forms.CheckedListBox filterChckdLstBox;
        private System.Windows.Forms.TextBox txbSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbUrl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copierToolStripMenuItem;
    }
}