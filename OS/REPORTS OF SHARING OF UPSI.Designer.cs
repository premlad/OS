namespace OS
{
	partial class REPORTS_OF_SHARING_OF_UPSI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(REPORTS_OF_SHARING_OF_UPSI));
			this.panel1 = new System.Windows.Forms.Panel();
			this.dataGridViewTable = new System.Windows.Forms.DataGridView();
			this.button1 = new OS.ButtonLastest();
			this.btnDownloadPrinter = new OS.ButtonLastest();
			this.btnDownloadPDF = new OS.ButtonLastest();
			this.btnDownloadexcel = new OS.ButtonLastest();
			this.label6 = new System.Windows.Forms.Label();
			this.btnSearch = new OS.ButtonLastest();
			this.txtInsiderID = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
			this.button2 = new OS.ButtonLastest();
			this.UPSID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.InsiderIDCOnnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NameUPSI = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Pan = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Pannoofaffl = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.detailsofUPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Datteime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.effectiveupto = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NDSsigned = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DateofEntry = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Dateofsecofnetry = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.datehwnupsi = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.dataGridViewTable);
			this.panel1.Location = new System.Drawing.Point(12, 103);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1345, 582);
			this.panel1.TabIndex = 88;
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
            this.UPSID,
            this.InsiderIDCOnnID,
            this.NameUPSI,
            this.Category,
            this.Pan,
            this.address,
            this.Pannoofaffl,
            this.detailsofUPID,
            this.Datteime,
            this.effectiveupto,
            this.remarks,
            this.NDSsigned,
            this.DateofEntry,
            this.Dateofsecofnetry,
            this.datehwnupsi});
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
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.DodgerBlue;
			this.button1.BackgroundImage = global::OS.Properties.Resources.icons8_refresh;
			this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(498, 47);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(112, 40);
			this.button1.TabIndex = 103;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnDownloadPrinter
			// 
			this.btnDownloadPrinter.BackColor = System.Drawing.Color.DodgerBlue;
			this.btnDownloadPrinter.BackgroundImage = global::OS.Properties.Resources.icons8_print_filled;
			this.btnDownloadPrinter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnDownloadPrinter.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDownloadPrinter.Location = new System.Drawing.Point(1283, 48);
			this.btnDownloadPrinter.Name = "btnDownloadPrinter";
			this.btnDownloadPrinter.Size = new System.Drawing.Size(46, 40);
			this.btnDownloadPrinter.TabIndex = 102;
			this.btnDownloadPrinter.UseVisualStyleBackColor = false;
			this.btnDownloadPrinter.Visible = false;
			this.btnDownloadPrinter.Click += new System.EventHandler(this.btnDownloadPrinter_Click);
			// 
			// btnDownloadPDF
			// 
			this.btnDownloadPDF.BackColor = System.Drawing.Color.DodgerBlue;
			this.btnDownloadPDF.BackgroundImage = global::OS.Properties.Resources.icons8_pdf;
			this.btnDownloadPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnDownloadPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDownloadPDF.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDownloadPDF.Location = new System.Drawing.Point(1136, 47);
			this.btnDownloadPDF.Name = "btnDownloadPDF";
			this.btnDownloadPDF.Size = new System.Drawing.Size(112, 40);
			this.btnDownloadPDF.TabIndex = 101;
			this.btnDownloadPDF.UseVisualStyleBackColor = false;
			this.btnDownloadPDF.Click += new System.EventHandler(this.btnDownloadPDF_Click);
			// 
			// btnDownloadexcel
			// 
			this.btnDownloadexcel.BackColor = System.Drawing.Color.DodgerBlue;
			this.btnDownloadexcel.BackgroundImage = global::OS.Properties.Resources.icons8_microsoft_excel;
			this.btnDownloadexcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnDownloadexcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDownloadexcel.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDownloadexcel.Location = new System.Drawing.Point(999, 47);
			this.btnDownloadexcel.Name = "btnDownloadexcel";
			this.btnDownloadexcel.Size = new System.Drawing.Size(112, 40);
			this.btnDownloadexcel.TabIndex = 100;
			this.btnDownloadexcel.UseVisualStyleBackColor = false;
			this.btnDownloadexcel.Click += new System.EventHandler(this.btnDownloadexcel_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.White;
			this.label6.Location = new System.Drawing.Point(889, 59);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(82, 19);
			this.label6.TabIndex = 99;
			this.label6.Text = "Download :";
			// 
			// btnSearch
			// 
			this.btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
			this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSearch.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSearch.Location = new System.Drawing.Point(350, 47);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(112, 40);
			this.btnSearch.TabIndex = 97;
			this.btnSearch.Text = "SEARCH";
			this.btnSearch.UseVisualStyleBackColor = false;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// txtInsiderID
			// 
			this.txtInsiderID.BackColor = System.Drawing.Color.MintCream;
			this.txtInsiderID.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtInsiderID.Location = new System.Drawing.Point(111, 55);
			this.txtInsiderID.MaxLength = 20;
			this.txtInsiderID.Name = "txtInsiderID";
			this.txtInsiderID.Size = new System.Drawing.Size(217, 27);
			this.txtInsiderID.TabIndex = 95;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(107, 33);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(61, 19);
			this.label4.TabIndex = 96;
			this.label4.Text = "UPSI ID";
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
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.DodgerBlue;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Font = new System.Drawing.Font("SF Pro Display", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.Location = new System.Drawing.Point(653, 47);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(112, 40);
			this.button2.TabIndex = 106;
			this.button2.Text = "CLOSE";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// UPSID
			// 
			this.UPSID.HeaderText = "UPSI ID";
			this.UPSID.Name = "UPSID";
			this.UPSID.ReadOnly = true;
			this.UPSID.Width = 86;
			// 
			// InsiderIDCOnnID
			// 
			this.InsiderIDCOnnID.HeaderText = "Insider ID | Connected ID";
			this.InsiderIDCOnnID.Name = "InsiderIDCOnnID";
			this.InsiderIDCOnnID.ReadOnly = true;
			this.InsiderIDCOnnID.Width = 164;
			// 
			// NameUPSI
			// 
			this.NameUPSI.HeaderText = "Name";
			this.NameUPSI.Name = "NameUPSI";
			this.NameUPSI.ReadOnly = true;
			this.NameUPSI.Width = 74;
			// 
			// Category
			// 
			this.Category.HeaderText = "Category of Recipient";
			this.Category.Name = "Category";
			this.Category.ReadOnly = true;
			this.Category.Width = 161;
			// 
			// Pan
			// 
			this.Pan.HeaderText = "PAN No. or any other Identifier ID ";
			this.Pan.Name = "Pan";
			this.Pan.ReadOnly = true;
			this.Pan.Width = 157;
			// 
			// address
			// 
			this.address.HeaderText = "Address";
			this.address.Name = "address";
			this.address.ReadOnly = true;
			this.address.Width = 89;
			// 
			// Pannoofaffl
			// 
			this.Pannoofaffl.HeaderText = "PAN No. of Affiliates, in case the recipient is an entity";
			this.Pannoofaffl.Name = "Pannoofaffl";
			this.Pannoofaffl.ReadOnly = true;
			this.Pannoofaffl.Width = 221;
			// 
			// detailsofUPID
			// 
			this.detailsofUPID.HeaderText = "Details of UPSI along with reason of sharing";
			this.detailsofUPID.Name = "detailsofUPID";
			this.detailsofUPID.ReadOnly = true;
			this.detailsofUPID.Width = 186;
			// 
			// Datteime
			// 
			this.Datteime.HeaderText = "Date and Time of Sharing";
			this.Datteime.Name = "Datteime";
			this.Datteime.ReadOnly = true;
			this.Datteime.Width = 136;
			// 
			// effectiveupto
			// 
			this.effectiveupto.HeaderText = "Effective Upto";
			this.effectiveupto.Name = "effectiveupto";
			this.effectiveupto.ReadOnly = true;
			this.effectiveupto.Width = 118;
			// 
			// remarks
			// 
			this.remarks.HeaderText = "Remarks";
			this.remarks.Name = "remarks";
			this.remarks.ReadOnly = true;
			this.remarks.Width = 93;
			// 
			// NDSsigned
			// 
			this.NDSsigned.HeaderText = "Whether NDA has been signed and Notice of confidentiality has been given?";
			this.NDSsigned.Name = "NDSsigned";
			this.NDSsigned.ReadOnly = true;
			this.NDSsigned.Width = 244;
			// 
			// DateofEntry
			// 
			this.DateofEntry.HeaderText = "Date of first entry with User Name";
			this.DateofEntry.Name = "DateofEntry";
			this.DateofEntry.ReadOnly = true;
			this.DateofEntry.Width = 124;
			// 
			// Dateofsecofnetry
			// 
			this.Dateofsecofnetry.HeaderText = "Date of second and all entry with User Name";
			this.Dateofsecofnetry.Name = "Dateofsecofnetry";
			this.Dateofsecofnetry.ReadOnly = true;
			this.Dateofsecofnetry.Width = 150;
			// 
			// datehwnupsi
			// 
			this.datehwnupsi.HeaderText = "Date when UPSI became publicly available";
			this.datehwnupsi.Name = "datehwnupsi";
			this.datehwnupsi.ReadOnly = true;
			this.datehwnupsi.Width = 184;
			// 
			// REPORTS_OF_SHARING_OF_UPSI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1370, 730);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnDownloadPrinter);
			this.Controls.Add(this.btnDownloadPDF);
			this.Controls.Add(this.btnDownloadexcel);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.txtInsiderID);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.panel1);
			this.Name = "REPORTS_OF_SHARING_OF_UPSI";
			this.Text = "REPORTS OF SHARING OF UPSI";
			this.Load += new System.EventHandler(this.REPORTS_OF_SHARING_OF_UPSI_Load);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.txtInsiderID, 0);
			this.Controls.SetChildIndex(this.btnSearch, 0);
			this.Controls.SetChildIndex(this.label6, 0);
			this.Controls.SetChildIndex(this.btnDownloadexcel, 0);
			this.Controls.SetChildIndex(this.btnDownloadPDF, 0);
			this.Controls.SetChildIndex(this.btnDownloadPrinter, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.button2, 0);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGridView dataGridViewTable;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.TextBox txtInsiderID;
		private System.Windows.Forms.Label label4;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private ButtonLastest button1;
		private ButtonLastest btnDownloadPrinter;
		private ButtonLastest btnDownloadPDF;
		private ButtonLastest btnDownloadexcel;
		private ButtonLastest btnSearch;
		private ButtonLastest button2;
		private System.Windows.Forms.DataGridViewTextBoxColumn UPSID;
		private System.Windows.Forms.DataGridViewTextBoxColumn InsiderIDCOnnID;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameUPSI;
		private System.Windows.Forms.DataGridViewTextBoxColumn Category;
		private System.Windows.Forms.DataGridViewTextBoxColumn Pan;
		private System.Windows.Forms.DataGridViewTextBoxColumn address;
		private System.Windows.Forms.DataGridViewTextBoxColumn Pannoofaffl;
		private System.Windows.Forms.DataGridViewTextBoxColumn detailsofUPID;
		private System.Windows.Forms.DataGridViewTextBoxColumn Datteime;
		private System.Windows.Forms.DataGridViewTextBoxColumn effectiveupto;
		private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
		private System.Windows.Forms.DataGridViewTextBoxColumn NDSsigned;
		private System.Windows.Forms.DataGridViewTextBoxColumn DateofEntry;
		private System.Windows.Forms.DataGridViewTextBoxColumn Dateofsecofnetry;
		private System.Windows.Forms.DataGridViewTextBoxColumn datehwnupsi;
	}
}