namespace OS
{
	partial class LIST_OF_INSIDERS
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LIST_OF_INSIDERS));
			this.label15 = new System.Windows.Forms.Label();
			this.txtFromDate = new System.Windows.Forms.DateTimePicker();
			this.txtToDate = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtInsiderID = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnSearch = new OS.ButtonLastest();
			this.label6 = new System.Windows.Forms.Label();
			this.btnDownloadexcel = new OS.ButtonLastest();
			this.btnDownloadPDF = new OS.ButtonLastest();
			this.btnDownloadPrinter = new OS.ButtonLastest();
			this.panel1 = new System.Windows.Forms.Panel();
			this.dataGridViewTable = new System.Windows.Forms.DataGridView();
			this.InsiderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NameoftheInsider = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CategoryofReceipt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.anyotheidentifierno = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.AadharNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PANNoofAffiliates = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.MobileNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LandlineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EmailId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Dateofentryusername = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.button1 = new OS.ButtonLastest();
			this.button2 = new OS.ButtonLastest();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
			this.SuspendLayout();
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.BackColor = System.Drawing.Color.Transparent;
			this.label15.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.Location = new System.Drawing.Point(20, 40);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(75, 19);
			this.label15.TabIndex = 71;
			this.label15.Text = "From Date";
			this.label15.Visible = false;
			// 
			// txtFromDate
			// 
			this.txtFromDate.Checked = false;
			this.txtFromDate.Font = new System.Drawing.Font("Times New Roman", 12F);
			this.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.txtFromDate.Location = new System.Drawing.Point(24, 62);
			this.txtFromDate.Name = "txtFromDate";
			this.txtFromDate.Size = new System.Drawing.Size(218, 26);
			this.txtFromDate.TabIndex = 73;
			this.txtFromDate.Visible = false;
			// 
			// txtToDate
			// 
			this.txtToDate.Checked = false;
			this.txtToDate.Font = new System.Drawing.Font("Times New Roman", 12F);
			this.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.txtToDate.Location = new System.Drawing.Point(278, 62);
			this.txtToDate.Name = "txtToDate";
			this.txtToDate.Size = new System.Drawing.Size(218, 26);
			this.txtToDate.TabIndex = 76;
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
			this.label2.TabIndex = 74;
			this.label2.Text = "To Date";
			this.label2.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(250, 58);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(23, 31);
			this.label1.TabIndex = 77;
			this.label1.Text = "-";
			this.label1.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(513, 58);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 31);
			this.label3.TabIndex = 78;
			this.label3.Text = "OR";
			this.label3.Visible = false;
			// 
			// txtInsiderID
			// 
			this.txtInsiderID.BackColor = System.Drawing.Color.MintCream;
			this.txtInsiderID.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtInsiderID.Location = new System.Drawing.Point(69, 62);
			this.txtInsiderID.MaxLength = 20;
			this.txtInsiderID.Name = "txtInsiderID";
			this.txtInsiderID.Size = new System.Drawing.Size(217, 27);
			this.txtInsiderID.TabIndex = 79;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(65, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 19);
			this.label4.TabIndex = 80;
			this.label4.Text = "Insider ID";
			// 
			// btnSearch
			// 
			this.btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
			this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSearch.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSearch.Location = new System.Drawing.Point(308, 54);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(112, 40);
			this.btnSearch.TabIndex = 81;
			this.btnSearch.Text = "SEARCH";
			this.btnSearch.UseVisualStyleBackColor = false;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.White;
			this.label6.Location = new System.Drawing.Point(913, 64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(79, 19);
			this.label6.TabIndex = 83;
			this.label6.Text = "Download :";
			// 
			// btnDownloadexcel
			// 
			this.btnDownloadexcel.BackColor = System.Drawing.Color.DodgerBlue;
			this.btnDownloadexcel.BackgroundImage = global::OS.Properties.Resources.icons8_microsoft_excel;
			this.btnDownloadexcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnDownloadexcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDownloadexcel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDownloadexcel.Location = new System.Drawing.Point(1018, 53);
			this.btnDownloadexcel.Name = "btnDownloadexcel";
			this.btnDownloadexcel.Size = new System.Drawing.Size(112, 40);
			this.btnDownloadexcel.TabIndex = 84;
			this.btnDownloadexcel.UseVisualStyleBackColor = false;
			this.btnDownloadexcel.Click += new System.EventHandler(this.btnDownloadexcel_Click);
			// 
			// btnDownloadPDF
			// 
			this.btnDownloadPDF.BackColor = System.Drawing.Color.DodgerBlue;
			this.btnDownloadPDF.BackgroundImage = global::OS.Properties.Resources.icons8_pdf;
			this.btnDownloadPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnDownloadPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDownloadPDF.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDownloadPDF.Location = new System.Drawing.Point(1152, 53);
			this.btnDownloadPDF.Name = "btnDownloadPDF";
			this.btnDownloadPDF.Size = new System.Drawing.Size(112, 40);
			this.btnDownloadPDF.TabIndex = 85;
			this.btnDownloadPDF.UseVisualStyleBackColor = false;
			this.btnDownloadPDF.Click += new System.EventHandler(this.btnDownloadPDF_Click);
			// 
			// btnDownloadPrinter
			// 
			this.btnDownloadPrinter.BackColor = System.Drawing.Color.DodgerBlue;
			this.btnDownloadPrinter.BackgroundImage = global::OS.Properties.Resources.icons8_print_filled;
			this.btnDownloadPrinter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnDownloadPrinter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDownloadPrinter.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDownloadPrinter.Location = new System.Drawing.Point(1270, 54);
			this.btnDownloadPrinter.Name = "btnDownloadPrinter";
			this.btnDownloadPrinter.Size = new System.Drawing.Size(46, 40);
			this.btnDownloadPrinter.TabIndex = 86;
			this.btnDownloadPrinter.UseVisualStyleBackColor = false;
			this.btnDownloadPrinter.Visible = false;
			this.btnDownloadPrinter.Click += new System.EventHandler(this.btnDownloadPrinter_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.dataGridViewTable);
			this.panel1.Location = new System.Drawing.Point(13, 108);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1345, 582);
			this.panel1.TabIndex = 87;
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
			dataGridViewCellStyle2.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InsiderId,
            this.NameoftheInsider,
            this.CategoryofReceipt,
            this.Address,
            this.PAN,
            this.anyotheidentifierno,
            this.AadharNo,
            this.PANNoofAffiliates,
            this.MobileNo,
            this.LandlineNo,
            this.EmailId,
            this.Dateofentryusername});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
			// InsiderId
			// 
			this.InsiderId.HeaderText = "Insider Id";
			this.InsiderId.Name = "InsiderId";
			this.InsiderId.ReadOnly = true;
			this.InsiderId.ToolTipText = "Insider Id";
			this.InsiderId.Width = 87;
			// 
			// NameoftheInsider
			// 
			this.NameoftheInsider.HeaderText = "Name of the Insider";
			this.NameoftheInsider.Name = "NameoftheInsider";
			this.NameoftheInsider.ReadOnly = true;
			this.NameoftheInsider.ToolTipText = "Name of the Insider";
			this.NameoftheInsider.Width = 108;
			// 
			// CategoryofReceipt
			// 
			this.CategoryofReceipt.HeaderText = "Category of Receipt";
			this.CategoryofReceipt.Name = "CategoryofReceipt";
			this.CategoryofReceipt.ReadOnly = true;
			this.CategoryofReceipt.ToolTipText = "Category of Receipt";
			this.CategoryofReceipt.Width = 105;
			// 
			// Address
			// 
			this.Address.HeaderText = "Address";
			this.Address.Name = "Address";
			this.Address.ReadOnly = true;
			this.Address.ToolTipText = "Address";
			this.Address.Width = 89;
			// 
			// PAN
			// 
			this.PAN.HeaderText = "PAN No";
			this.PAN.Name = "PAN";
			this.PAN.ReadOnly = true;
			this.PAN.ToolTipText = "PAN No";
			this.PAN.Width = 78;
			// 
			// anyotheidentifierno
			// 
			this.anyotheidentifierno.HeaderText = "Any Other Identifier No.";
			this.anyotheidentifierno.Name = "anyotheidentifierno";
			this.anyotheidentifierno.ReadOnly = true;
			this.anyotheidentifierno.Width = 151;
			// 
			// AadharNo
			// 
			this.AadharNo.HeaderText = "Aadhar No";
			this.AadharNo.Name = "AadharNo";
			this.AadharNo.ReadOnly = true;
			this.AadharNo.ToolTipText = "Aadhar No";
			this.AadharNo.Width = 96;
			// 
			// PANNoofAffiliates
			// 
			this.PANNoofAffiliates.HeaderText = "PAN No. of Affiliates, in case the recipient is an entity";
			this.PANNoofAffiliates.Name = "PANNoofAffiliates";
			this.PANNoofAffiliates.ReadOnly = true;
			this.PANNoofAffiliates.ToolTipText = "PAN No. of Affiliates, in case the recipient is an entity";
			this.PANNoofAffiliates.Width = 221;
			// 
			// MobileNo
			// 
			this.MobileNo.HeaderText = "Mobile No";
			this.MobileNo.Name = "MobileNo";
			this.MobileNo.ReadOnly = true;
			this.MobileNo.ToolTipText = "Mobile No";
			this.MobileNo.Width = 92;
			// 
			// LandlineNo
			// 
			this.LandlineNo.HeaderText = "Landline No";
			this.LandlineNo.Name = "LandlineNo";
			this.LandlineNo.ReadOnly = true;
			this.LandlineNo.ToolTipText = "Landline No";
			this.LandlineNo.Width = 102;
			// 
			// EmailId
			// 
			this.EmailId.HeaderText = "Email Id";
			this.EmailId.Name = "EmailId";
			this.EmailId.ReadOnly = true;
			this.EmailId.ToolTipText = "Email Id";
			this.EmailId.Width = 79;
			// 
			// Dateofentryusername
			// 
			this.Dateofentryusername.HeaderText = "Date of Entry - Username";
			this.Dateofentryusername.Name = "Dateofentryusername";
			this.Dateofentryusername.ReadOnly = true;
			this.Dateofentryusername.ToolTipText = "Date of Entry - Username";
			this.Dateofentryusername.Width = 122;
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
			this.button1.BackColor = System.Drawing.Color.DodgerBlue;
			this.button1.BackgroundImage = global::OS.Properties.Resources.icons8_refresh;
			this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(471, 53);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(112, 40);
			this.button1.TabIndex = 88;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.DodgerBlue;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.Location = new System.Drawing.Point(640, 53);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(112, 40);
			this.button2.TabIndex = 105;
			this.button2.Text = "CLOSE";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// LIST_OF_INSIDERS
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
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.txtInsiderID);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtToDate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtFromDate);
			this.Controls.Add(this.label15);
			this.Name = "LIST_OF_INSIDERS";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "LIST OF INSIDERS";
			this.Load += new System.EventHandler(this.LIST_OF_INSIDERS_Load);
			this.Controls.SetChildIndex(this.label15, 0);
			this.Controls.SetChildIndex(this.txtFromDate, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.txtToDate, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.txtInsiderID, 0);
			this.Controls.SetChildIndex(this.btnSearch, 0);
			this.Controls.SetChildIndex(this.label6, 0);
			this.Controls.SetChildIndex(this.btnDownloadexcel, 0);
			this.Controls.SetChildIndex(this.btnDownloadPDF, 0);
			this.Controls.SetChildIndex(this.btnDownloadPrinter, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.button2, 0);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.DateTimePicker txtFromDate;
		private System.Windows.Forms.DateTimePicker txtToDate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox txtInsiderID;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGridView dataGridViewTable;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.DataGridViewTextBoxColumn InsiderId;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameoftheInsider;
		private System.Windows.Forms.DataGridViewTextBoxColumn CategoryofReceipt;
		private System.Windows.Forms.DataGridViewTextBoxColumn Address;
		private System.Windows.Forms.DataGridViewTextBoxColumn PAN;
		private System.Windows.Forms.DataGridViewTextBoxColumn anyotheidentifierno;
		private System.Windows.Forms.DataGridViewTextBoxColumn AadharNo;
		private System.Windows.Forms.DataGridViewTextBoxColumn PANNoofAffiliates;
		private System.Windows.Forms.DataGridViewTextBoxColumn MobileNo;
		private System.Windows.Forms.DataGridViewTextBoxColumn LandlineNo;
		private System.Windows.Forms.DataGridViewTextBoxColumn EmailId;
		private System.Windows.Forms.DataGridViewTextBoxColumn Dateofentryusername;
		private ButtonLastest btnSearch;
		private ButtonLastest btnDownloadexcel;
		private ButtonLastest btnDownloadPDF;
		private ButtonLastest btnDownloadPrinter;
		private ButtonLastest button1;
		private ButtonLastest button2;
	}
}