namespace The_PIT_Archive.Forms
{
    partial class AuditTrail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtToDate = new System.Windows.Forms.DateTimePicker();
            this.bunifuImageButton4 = new System.Windows.Forms.PictureBox();
            this.bunifuImageButton2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bunifuImageButton3 = new System.Windows.Forms.PictureBox();
            this.bunifuImageButton1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewTable = new System.Windows.Forms.DataGridView();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PageTab = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActivityLog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActivityTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdatedColumns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailLog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SF Pro Display", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label5.Location = new System.Drawing.Point(30, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 24);
            this.label5.TabIndex = 10;
            this.label5.Text = "AUDIT TRAIL";
            // 
            // txtFromDate
            // 
            this.txtFromDate.BackColor = System.Drawing.Color.SeaGreen;
            this.txtFromDate.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromDate.ForeColor = System.Drawing.Color.White;
            this.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFromDate.Location = new System.Drawing.Point(122, 64);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(168, 27);
            this.txtFromDate.TabIndex = 0;
            this.txtFromDate.Value = new System.DateTime(2020, 8, 22, 0, 0, 0, 0);
            this.txtFromDate.ValueChanged += new System.EventHandler(this.txtFromDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label1.Location = new System.Drawing.Point(17, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "FROM DATE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label2.Location = new System.Drawing.Point(294, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 19);
            this.label2.TabIndex = 15;
            this.label2.Text = "TO DATE";
            // 
            // txtToDate
            // 
            this.txtToDate.BackColor = System.Drawing.Color.SeaGreen;
            this.txtToDate.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToDate.ForeColor = System.Drawing.Color.White;
            this.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtToDate.Location = new System.Drawing.Point(376, 64);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(168, 27);
            this.txtToDate.TabIndex = 1;
            this.txtToDate.Value = new System.DateTime(2020, 8, 22, 0, 0, 0, 0);
            this.txtToDate.ValueChanged += new System.EventHandler(this.txtToDate_ValueChanged);
            // 
            // bunifuImageButton4
            // 
            this.bunifuImageButton4.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton4.Image = global::The_PIT_Archive.Properties.Resources.icons8_google_web_search_96;
            this.bunifuImageButton4.Location = new System.Drawing.Point(559, 61);
            this.bunifuImageButton4.Name = "bunifuImageButton4";
            this.bunifuImageButton4.Size = new System.Drawing.Size(35, 35);
            this.bunifuImageButton4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton4.TabIndex = 199;
            this.bunifuImageButton4.TabStop = false;
            this.bunifuImageButton4.Click += new System.EventHandler(this.bunifuImageButton4_Click);
            // 
            // bunifuImageButton2
            // 
            this.bunifuImageButton2.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton2.Image = global::The_PIT_Archive.Properties.Resources.icons8_refresh_96;
            this.bunifuImageButton2.Location = new System.Drawing.Point(745, 59);
            this.bunifuImageButton2.Name = "bunifuImageButton2";
            this.bunifuImageButton2.Size = new System.Drawing.Size(35, 35);
            this.bunifuImageButton2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton2.TabIndex = 201;
            this.bunifuImageButton2.TabStop = false;
            this.bunifuImageButton2.Click += new System.EventHandler(this.bunifuImageButton2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label3.Location = new System.Drawing.Point(642, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 19);
            this.label3.TabIndex = 202;
            this.label3.Text = "REFRESH";
            // 
            // bunifuImageButton3
            // 
            this.bunifuImageButton3.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton3.Image = global::The_PIT_Archive.Properties.Resources.icons8_pdf_96;
            this.bunifuImageButton3.Location = new System.Drawing.Point(1020, 59);
            this.bunifuImageButton3.Name = "bunifuImageButton3";
            this.bunifuImageButton3.Size = new System.Drawing.Size(35, 35);
            this.bunifuImageButton3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton3.TabIndex = 207;
            this.bunifuImageButton3.TabStop = false;
            this.bunifuImageButton3.Click += new System.EventHandler(this.bunifuImageButton3_Click);
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton1.Image = global::The_PIT_Archive.Properties.Resources.icons8_microsoft_excel_96;
            this.bunifuImageButton1.Location = new System.Drawing.Point(967, 59);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(35, 35);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 204;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.label4.Location = new System.Drawing.Point(848, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 19);
            this.label4.TabIndex = 205;
            this.label4.Text = "DOWNLOAD";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridViewTable);
            this.panel1.Location = new System.Drawing.Point(12, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1062, 487);
            this.panel1.TabIndex = 208;
            // 
            // dataGridViewTable
            // 
            this.dataGridViewTable.AllowUserToAddRows = false;
            this.dataGridViewTable.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTable.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridViewTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SF Pro Display", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Username,
            this.PageTab,
            this.Operation,
            this.ActivityLog,
            this.ActivityTime,
            this.UpdatedColumns,
            this.DetailLog});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTable.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTable.EnableHeadersVisualStyles = false;
            this.dataGridViewTable.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewTable.Name = "dataGridViewTable";
            this.dataGridViewTable.ReadOnly = true;
            this.dataGridViewTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTable.Size = new System.Drawing.Size(1062, 487);
            this.dataGridViewTable.TabIndex = 0;
            // 
            // Username
            // 
            this.Username.HeaderText = "Username";
            this.Username.Name = "Username";
            this.Username.ReadOnly = true;
            // 
            // PageTab
            // 
            this.PageTab.HeaderText = "Page/Tab";
            this.PageTab.Name = "PageTab";
            this.PageTab.ReadOnly = true;
            // 
            // Operation
            // 
            this.Operation.HeaderText = "Operation";
            this.Operation.Name = "Operation";
            this.Operation.ReadOnly = true;
            // 
            // ActivityLog
            // 
            this.ActivityLog.HeaderText = "Activity Log";
            this.ActivityLog.Name = "ActivityLog";
            this.ActivityLog.ReadOnly = true;
            // 
            // ActivityTime
            // 
            this.ActivityTime.HeaderText = "Activity Time";
            this.ActivityTime.Name = "ActivityTime";
            this.ActivityTime.ReadOnly = true;
            // 
            // UpdatedColumns
            // 
            this.UpdatedColumns.HeaderText = "Updated Columns";
            this.UpdatedColumns.Name = "UpdatedColumns";
            this.UpdatedColumns.ReadOnly = true;
            // 
            // DetailLog
            // 
            this.DetailLog.HeaderText = "Detail Log";
            this.DetailLog.Name = "DetailLog";
            this.DetailLog.ReadOnly = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(34, 39);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(542, 10);
            this.progressBar1.TabIndex = 218;
            this.progressBar1.Value = 100;
            // 
            // AuditTrail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 621);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bunifuImageButton3);
            this.Controls.Add(this.bunifuImageButton1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bunifuImageButton2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bunifuImageButton4);
            this.Controls.Add(this.txtToDate);
            this.Controls.Add(this.txtFromDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AuditTrail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AuditTrail";
            this.Load += new System.EventHandler(this.AuditTrail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker txtFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtToDate;
        private System.Windows.Forms.PictureBox bunifuImageButton4;
        private System.Windows.Forms.PictureBox bunifuImageButton2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox bunifuImageButton3;
        private System.Windows.Forms.PictureBox bunifuImageButton1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridViewTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Username;
        private System.Windows.Forms.DataGridViewTextBoxColumn PageTab;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActivityLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActivityTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdatedColumns;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailLog;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}