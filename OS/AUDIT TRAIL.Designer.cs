namespace OS
{
	partial class AUDIT_TRAIL
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AUDIT_TRAIL));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.button2 = new System.Windows.Forms.Button();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
			this.button1 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.dataGridViewTable = new System.Windows.Forms.DataGridView();
			this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PageTab = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.opeartion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ActivityLog = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ActivityTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.detailog = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnDownloadPrinter = new System.Windows.Forms.Button();
			this.btnDownloadPDF = new System.Windows.Forms.Button();
			this.btnDownloadexcel = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtToDate = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.txtFromDate = new System.Windows.Forms.DateTimePicker();
			this.label15 = new System.Windows.Forms.Label();
			this.btnSearch = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
			this.SuspendLayout();
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.Transparent;
			this.button2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.Location = new System.Drawing.Point(893, 49);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(94, 40);
			this.button2.TabIndex = 122;
			this.button2.Text = "CLOSE";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// printPreviewDialog1
			// 
			this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
			this.printPreviewDialog1.Enabled = true;
			this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
			this.printPreviewDialog1.Name = "printPreviewDialog1";
			this.printPreviewDialog1.Visible = false;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Transparent;
			this.button1.BackgroundImage = global::OS.Properties.Resources.icons8_refresh;
			this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(704, 50);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(46, 40);
			this.button1.TabIndex = 121;
			this.button1.UseVisualStyleBackColor = false;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.dataGridViewTable);
			this.panel1.Location = new System.Drawing.Point(13, 108);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1345, 582);
			this.panel1.TabIndex = 120;
			// 
			// dataGridViewTable
			// 
			this.dataGridViewTable.AllowUserToAddRows = false;
			this.dataGridViewTable.AllowUserToDeleteRows = false;
			this.dataGridViewTable.AllowUserToOrderColumns = true;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridViewTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Username,
            this.PageTab,
            this.opeartion,
            this.ActivityLog,
            this.ActivityTime,
            this.detailog});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTable.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewTable.GridColor = System.Drawing.SystemColors.Control;
			this.dataGridViewTable.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewTable.Name = "dataGridViewTable";
			this.dataGridViewTable.ReadOnly = true;
			this.dataGridViewTable.Size = new System.Drawing.Size(1345, 582);
			this.dataGridViewTable.TabIndex = 6;
			// 
			// Username
			// 
			this.Username.HeaderText = "Username";
			this.Username.Name = "Username";
			this.Username.ReadOnly = true;
			this.Username.Width = 95;
			// 
			// PageTab
			// 
			this.PageTab.HeaderText = "Page/Tab";
			this.PageTab.Name = "PageTab";
			this.PageTab.ReadOnly = true;
			this.PageTab.Width = 91;
			// 
			// opeartion
			// 
			this.opeartion.HeaderText = "Operation";
			this.opeartion.Name = "opeartion";
			this.opeartion.ReadOnly = true;
			this.opeartion.Width = 95;
			// 
			// ActivityLog
			// 
			this.ActivityLog.HeaderText = "Activity Log";
			this.ActivityLog.Name = "ActivityLog";
			this.ActivityLog.ReadOnly = true;
			this.ActivityLog.Width = 108;
			// 
			// ActivityTime
			// 
			this.ActivityTime.HeaderText = "Activity Time";
			this.ActivityTime.Name = "ActivityTime";
			this.ActivityTime.ReadOnly = true;
			this.ActivityTime.Width = 113;
			// 
			// detailog
			// 
			this.detailog.HeaderText = "Detail Log";
			this.detailog.Name = "detailog";
			this.detailog.ReadOnly = true;
			this.detailog.Width = 97;
			// 
			// btnDownloadPrinter
			// 
			this.btnDownloadPrinter.BackColor = System.Drawing.Color.Transparent;
			this.btnDownloadPrinter.BackgroundImage = global::OS.Properties.Resources.icons8_print_filled;
			this.btnDownloadPrinter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnDownloadPrinter.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDownloadPrinter.Location = new System.Drawing.Point(1270, 54);
			this.btnDownloadPrinter.Name = "btnDownloadPrinter";
			this.btnDownloadPrinter.Size = new System.Drawing.Size(46, 40);
			this.btnDownloadPrinter.TabIndex = 119;
			this.btnDownloadPrinter.UseVisualStyleBackColor = false;
			// 
			// btnDownloadPDF
			// 
			this.btnDownloadPDF.BackColor = System.Drawing.Color.Transparent;
			this.btnDownloadPDF.BackgroundImage = global::OS.Properties.Resources.icons8_pdf;
			this.btnDownloadPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnDownloadPDF.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDownloadPDF.Location = new System.Drawing.Point(1197, 54);
			this.btnDownloadPDF.Name = "btnDownloadPDF";
			this.btnDownloadPDF.Size = new System.Drawing.Size(46, 40);
			this.btnDownloadPDF.TabIndex = 118;
			this.btnDownloadPDF.UseVisualStyleBackColor = false;
			// 
			// btnDownloadexcel
			// 
			this.btnDownloadexcel.BackColor = System.Drawing.Color.Transparent;
			this.btnDownloadexcel.BackgroundImage = global::OS.Properties.Resources.icons8_microsoft_excel;
			this.btnDownloadexcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnDownloadexcel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDownloadexcel.Location = new System.Drawing.Point(1125, 53);
			this.btnDownloadexcel.Name = "btnDownloadexcel";
			this.btnDownloadexcel.Size = new System.Drawing.Size(46, 40);
			this.btnDownloadexcel.TabIndex = 117;
			this.btnDownloadexcel.UseVisualStyleBackColor = false;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(1033, 64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(79, 19);
			this.label6.TabIndex = 116;
			this.label6.Text = "Download :";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(771, 54);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(20, 31);
			this.label5.TabIndex = 115;
			this.label5.Text = "|";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(250, 58);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(23, 31);
			this.label1.TabIndex = 110;
			this.label1.Text = "-";
			this.label1.Visible = false;
			// 
			// txtToDate
			// 
			this.txtToDate.Checked = false;
			this.txtToDate.Font = new System.Drawing.Font("Times New Roman", 12F);
			this.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.txtToDate.Location = new System.Drawing.Point(278, 62);
			this.txtToDate.Name = "txtToDate";
			this.txtToDate.Size = new System.Drawing.Size(218, 26);
			this.txtToDate.TabIndex = 109;
			this.txtToDate.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(274, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 19);
			this.label2.TabIndex = 108;
			this.label2.Text = "To Date";
			this.label2.Visible = false;
			// 
			// txtFromDate
			// 
			this.txtFromDate.Checked = false;
			this.txtFromDate.Font = new System.Drawing.Font("Times New Roman", 12F);
			this.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.txtFromDate.Location = new System.Drawing.Point(24, 62);
			this.txtFromDate.Name = "txtFromDate";
			this.txtFromDate.Size = new System.Drawing.Size(218, 26);
			this.txtFromDate.TabIndex = 107;
			this.txtFromDate.Visible = false;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.BackColor = System.Drawing.Color.Transparent;
			this.label15.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.Location = new System.Drawing.Point(20, 40);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(75, 19);
			this.label15.TabIndex = 106;
			this.label15.Text = "From Date";
			this.label15.Visible = false;
			// 
			// btnSearch
			// 
			this.btnSearch.BackColor = System.Drawing.Color.Transparent;
			this.btnSearch.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSearch.Location = new System.Drawing.Point(523, 53);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(112, 40);
			this.btnSearch.TabIndex = 114;
			this.btnSearch.Text = "SEARCH";
			this.btnSearch.UseVisualStyleBackColor = false;
			// 
			// AUDIT_TRAIL
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1370, 730);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnDownloadPrinter);
			this.Controls.Add(this.btnDownloadPDF);
			this.Controls.Add(this.btnDownloadexcel);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtToDate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtFromDate);
			this.Controls.Add(this.label15);
			this.Name = "AUDIT_TRAIL";
			this.Text = "AUDIT TRAIL";
			this.Load += new System.EventHandler(this.AUDIT_TRAIL_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button2;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGridView dataGridViewTable;
		private System.Windows.Forms.Button btnDownloadPrinter;
		private System.Windows.Forms.Button btnDownloadPDF;
		private System.Windows.Forms.Button btnDownloadexcel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker txtToDate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker txtFromDate;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.DataGridViewTextBoxColumn Username;
		private System.Windows.Forms.DataGridViewTextBoxColumn PageTab;
		private System.Windows.Forms.DataGridViewTextBoxColumn opeartion;
		private System.Windows.Forms.DataGridViewTextBoxColumn ActivityLog;
		private System.Windows.Forms.DataGridViewTextBoxColumn ActivityTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn detailog;
	}
}