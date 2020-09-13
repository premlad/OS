namespace The_PIT_Archive.Forms
{
    partial class CreateLogin
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtToDate = new System.Windows.Forms.DateTimePicker();
            this.txtFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnaddINSCON = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.bunifuFlatButton1 = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SF Pro Display", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label5.Location = new System.Drawing.Point(20, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 24);
            this.label5.TabIndex = 10;
            this.label5.Text = "CREATE | RESET LOGIN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label2.Location = new System.Drawing.Point(184, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 19);
            this.label2.TabIndex = 199;
            this.label2.Text = "CREATE | SET LOGIN DATA";
            // 
            // txtToDate
            // 
            this.txtToDate.BackColor = System.Drawing.Color.SeaGreen;
            this.txtToDate.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToDate.ForeColor = System.Drawing.Color.White;
            this.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtToDate.Location = new System.Drawing.Point(266, 386);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(168, 27);
            this.txtToDate.TabIndex = 3;
            this.txtToDate.Value = new System.DateTime(2020, 8, 16, 13, 47, 46, 874);
            this.txtToDate.ValueChanged += new System.EventHandler(this.txtToDate_ValueChanged);
            // 
            // txtFromDate
            // 
            this.txtFromDate.BackColor = System.Drawing.Color.SeaGreen;
            this.txtFromDate.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromDate.ForeColor = System.Drawing.Color.White;
            this.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFromDate.Location = new System.Drawing.Point(266, 318);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(168, 27);
            this.txtFromDate.TabIndex = 2;
            this.txtFromDate.Value = new System.DateTime(2020, 8, 16, 13, 47, 46, 874);
            this.txtFromDate.ValueChanged += new System.EventHandler(this.txtFromDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label1.Location = new System.Drawing.Point(184, 391);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 19);
            this.label1.TabIndex = 203;
            this.label1.Text = "TO DATE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label3.Location = new System.Drawing.Point(161, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 19);
            this.label3.TabIndex = 201;
            this.label3.Text = "FROM DATE";
            // 
            // btnaddINSCON
            // 
            this.btnaddINSCON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnaddINSCON.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnaddINSCON.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnaddINSCON.FlatAppearance.BorderSize = 0;
            this.btnaddINSCON.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SeaGreen;
            this.btnaddINSCON.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnaddINSCON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddINSCON.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddINSCON.Location = new System.Drawing.Point(135, 458);
            this.btnaddINSCON.Margin = new System.Windows.Forms.Padding(6);
            this.btnaddINSCON.Name = "btnaddINSCON";
            this.btnaddINSCON.Size = new System.Drawing.Size(363, 52);
            this.btnaddINSCON.TabIndex = 4;
            this.btnaddINSCON.Text = "SAVE";
            this.btnaddINSCON.UseVisualStyleBackColor = false;
            this.btnaddINSCON.Click += new System.EventHandler(this.btnaddINSCON_Click);
            this.btnaddINSCON.Paint += new System.Windows.Forms.PaintEventHandler(this.bunifuFlatButton2_Paint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label4.Location = new System.Drawing.Point(733, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 19);
            this.label4.TabIndex = 232;
            this.label4.Text = "RESET LOGIN DATA";
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
            this.bunifuFlatButton1.Location = new System.Drawing.Point(638, 268);
            this.bunifuFlatButton1.Margin = new System.Windows.Forms.Padding(6);
            this.bunifuFlatButton1.Name = "bunifuFlatButton1";
            this.bunifuFlatButton1.Size = new System.Drawing.Size(363, 52);
            this.bunifuFlatButton1.TabIndex = 5;
            this.bunifuFlatButton1.Text = "RESET LOGIN FOR OFFICER";
            this.bunifuFlatButton1.UseVisualStyleBackColor = false;
            this.bunifuFlatButton1.Click += new System.EventHandler(this.bunifuFlatButton1_Click);
            this.bunifuFlatButton1.Paint += new System.Windows.Forms.PaintEventHandler(this.bunifuFlatButton1_Paint);
            // 
            // txtUsername
            // 
            this.txtUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUsername.Enabled = false;
            this.txtUsername.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtUsername.Location = new System.Drawing.Point(253, 176);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 27);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.Text = "COMPLIANCE_OFFICER";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label7.Location = new System.Drawing.Point(145, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 19);
            this.label7.TabIndex = 236;
            this.label7.Text = "USERNAME";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label6.Location = new System.Drawing.Point(145, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 19);
            this.label6.TabIndex = 238;
            this.label6.Text = "PASSWORD";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(254, 244);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(199, 27);
            this.txtPassword.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(24, 48);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(542, 10);
            this.progressBar1.TabIndex = 239;
            this.progressBar1.Value = 100;
            // 
            // CreateLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 582);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.bunifuFlatButton1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnaddINSCON);
            this.Controls.Add(this.txtToDate);
            this.Controls.Add(this.txtFromDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreateLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreateLogin";
            this.Load += new System.EventHandler(this.CreateLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtToDate;
        private System.Windows.Forms.DateTimePicker txtFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnaddINSCON;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bunifuFlatButton1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}