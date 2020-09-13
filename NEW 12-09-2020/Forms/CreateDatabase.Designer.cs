namespace The_PIT_Archive.Forms
{
    partial class CreateDatabase
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
            this.label5 = new System.Windows.Forms.Label();
            this.bunifuFlatButton2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtdbpath = new System.Windows.Forms.TextBox();
            this.bunifuFlatButton1 = new System.Windows.Forms.Button();
            this.btnLOGIN = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SF Pro Display", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label5.Location = new System.Drawing.Point(29, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(300, 24);
            this.label5.TabIndex = 8;
            this.label5.Text = "CHOOSE | CREATE DATABASE";
            // 
            // bunifuFlatButton2
            // 
            this.bunifuFlatButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuFlatButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuFlatButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuFlatButton2.Enabled = false;
            this.bunifuFlatButton2.FlatAppearance.BorderSize = 0;
            this.bunifuFlatButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SeaGreen;
            this.bunifuFlatButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.bunifuFlatButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bunifuFlatButton2.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButton2.Location = new System.Drawing.Point(393, 320);
            this.bunifuFlatButton2.Margin = new System.Windows.Forms.Padding(6);
            this.bunifuFlatButton2.Name = "bunifuFlatButton2";
            this.bunifuFlatButton2.Size = new System.Drawing.Size(363, 52);
            this.bunifuFlatButton2.TabIndex = 3;
            this.bunifuFlatButton2.Text = "SAVE PATH";
            this.bunifuFlatButton2.UseVisualStyleBackColor = false;
            this.bunifuFlatButton2.Click += new System.EventHandler(this.bunifuFlatButton2_Click);
            this.bunifuFlatButton2.Paint += new System.Windows.Forms.PaintEventHandler(this.bunifuFlatButton2_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("SF Pro Display", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(268, 403);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(620, 69);
            this.label3.TabIndex = 4;
            this.label3.Text = "Note :-\r\nThe Following Selected Database should not be Removed/Deleted/Rename.\r\nS" +
    "elected Database is the Primary source.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("SF Pro Display", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(304, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 23);
            this.label2.TabIndex = 227;
            this.label2.Text = "Selected Database Path";
            // 
            // txtdbpath
            // 
            this.txtdbpath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtdbpath.Enabled = false;
            this.txtdbpath.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdbpath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtdbpath.Location = new System.Drawing.Point(307, 257);
            this.txtdbpath.Margin = new System.Windows.Forms.Padding(4);
            this.txtdbpath.Name = "txtdbpath";
            this.txtdbpath.Size = new System.Drawing.Size(514, 27);
            this.txtdbpath.TabIndex = 2;
            // 
            // bunifuFlatButton1
            // 
            this.bunifuFlatButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuFlatButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuFlatButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuFlatButton1.FlatAppearance.BorderSize = 0;
            this.bunifuFlatButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SeaGreen;
            this.bunifuFlatButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.bunifuFlatButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bunifuFlatButton1.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButton1.Location = new System.Drawing.Point(570, 137);
            this.bunifuFlatButton1.Margin = new System.Windows.Forms.Padding(6);
            this.bunifuFlatButton1.Name = "bunifuFlatButton1";
            this.bunifuFlatButton1.Size = new System.Drawing.Size(251, 52);
            this.bunifuFlatButton1.TabIndex = 1;
            this.bunifuFlatButton1.Text = "CREATE DATABASE";
            this.bunifuFlatButton1.UseVisualStyleBackColor = false;
            this.bunifuFlatButton1.Click += new System.EventHandler(this.bunifuFlatButton1_Click);
            this.bunifuFlatButton1.Paint += new System.Windows.Forms.PaintEventHandler(this.bunifuFlatButton1_Paint);
            // 
            // btnLOGIN
            // 
            this.btnLOGIN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnLOGIN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLOGIN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLOGIN.FlatAppearance.BorderSize = 0;
            this.btnLOGIN.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SeaGreen;
            this.btnLOGIN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnLOGIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLOGIN.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLOGIN.Location = new System.Drawing.Point(307, 137);
            this.btnLOGIN.Margin = new System.Windows.Forms.Padding(6);
            this.btnLOGIN.Name = "btnLOGIN";
            this.btnLOGIN.Size = new System.Drawing.Size(251, 52);
            this.btnLOGIN.TabIndex = 0;
            this.btnLOGIN.Text = "CHOOSE DATABASE";
            this.btnLOGIN.UseVisualStyleBackColor = false;
            this.btnLOGIN.Click += new System.EventHandler(this.btnLOGIN_Click);
            this.btnLOGIN.Paint += new System.Windows.Forms.PaintEventHandler(this.btnLOGIN_Paint);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(33, 54);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(542, 10);
            this.progressBar1.TabIndex = 228;
            this.progressBar1.Value = 100;
            // 
            // CreateDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 621);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.bunifuFlatButton2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtdbpath);
            this.Controls.Add(this.bunifuFlatButton1);
            this.Controls.Add(this.btnLOGIN);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreateDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreateDatabase";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bunifuFlatButton2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtdbpath;
        private System.Windows.Forms.Button bunifuFlatButton1;
        private System.Windows.Forms.Button btnLOGIN;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}