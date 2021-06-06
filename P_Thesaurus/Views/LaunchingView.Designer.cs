
namespace P_Thesaurus.Views
{
    partial class LaunchingView
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
            this.btnFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnWeb = new System.Windows.Forms.Button();
            this.btnFtp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFolder
            // 
            this.btnFolder.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFolder.Location = new System.Drawing.Point(140, 286);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(134, 53);
            this.btnFolder.TabIndex = 0;
            this.btnFolder.Text = "Dossier";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.BtnFolderClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(275, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Smart Thésaurus";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(334, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Où chercher?";
            // 
            // btnWeb
            // 
            this.btnWeb.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeb.Location = new System.Drawing.Point(366, 286);
            this.btnWeb.Name = "btnWeb";
            this.btnWeb.Size = new System.Drawing.Size(134, 53);
            this.btnWeb.TabIndex = 3;
            this.btnWeb.Text = "Web";
            this.btnWeb.UseVisualStyleBackColor = true;
            // 
            // btnFtp
            // 
            this.btnFtp.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFtp.Location = new System.Drawing.Point(591, 286);
            this.btnFtp.Name = "btnFtp";
            this.btnFtp.Size = new System.Drawing.Size(134, 53);
            this.btnFtp.TabIndex = 4;
            this.btnFtp.Text = "FTP";
            this.btnFtp.UseVisualStyleBackColor = true;
            this.btnFtp.Click += new System.EventHandler(this.BtnFtpClick);
            // 
            // LaunchingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnFtp);
            this.Controls.Add(this.btnWeb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFolder);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "LaunchingView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LaunchingView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnWeb;
        private System.Windows.Forms.Button btnFtp;
    }
}