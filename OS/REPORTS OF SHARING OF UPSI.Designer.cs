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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(REPORTS_OF_SHARING_OF_UPSI));
			this.panel1 = new System.Windows.Forms.Panel();
			this.dataGridViewTable = new System.Windows.Forms.DataGridView();
			this.UPSID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.InsiderIDCOnnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Pan = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Pannoofaffl = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.detailsofUPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Datteime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NDSsigned = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DateofEntry = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Dateofsecofnetry = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.datehwnupsi = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.button1 = new System.Windows.Forms.Button();
			this.btnDownloadPrinter = new System.Windows.Forms.Button();
			this.btnDownloadPDF = new System.Windows.Forms.Button();
			this.btnDownloadexcel = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.btnSearch = new System.Windows.Forms.Button();
			this.txtInsiderID = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
			this.button2 = new System.Windows.Forms.Button();
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
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridViewTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UPSID,
            this.InsiderIDCOnnID,
            this.Name,
            this.Category,
            this.Pan,
            this.Pannoofaffl,
            this.detailsofUPID,
            this.Datteime,
            this.NDSsigned,
            this.DateofEntry,
            this.Dateofsecofnetry,
            this.datehwnupsi});
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTable.DefaultCellStyle = dataGridViewCellStyle6;
			this.dataGridViewTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewTable.GridColor = System.Drawing.SystemColors.Control;
			this.dataGridViewTable.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewTable.Name = "dataGridViewTable";
			this.dataGridViewTable.ReadOnly = true;
			this.dataGridViewTable.Size = new System.Drawing.Size(1345, 582);
			this.dataGridViewTable.TabIndex = 6;
			// 
			// UPSID
			// 
			this.UPSID.HeaderText = "UPSI ID";
			this.UPSID.Name = "UPSID";
			this.UPSID.ReadOnly = true;
			this.UPSID.Width = 81;
			// 
			// InsiderIDCOnnID
			// 
			this.InsiderIDCOnnID.HeaderText = "Insider ID | Connected ID";
			this.InsiderIDCOnnID.Name = "InsiderIDCOnnID";
			this.InsiderIDCOnnID.ReadOnly = true;
			this.InsiderIDCOnnID.Width = 110;
			// 
			// Name
			// 
			this.Name.HeaderText = "Name";
			this.Name.Name = "Name";
			this.Name.ReadOnly = true;
			this.Name.Width = 71;
			// 
			// Category
			// 
			this.Category.HeaderText = "Category of Recipient";
			this.Category.Name = "Category";
			this.Category.ReadOnly = true;
			this.Category.Width = 101;
			// 
			// Pan
			// 
			this.Pan.HeaderText = "Pan No";
			this.Pan.Name = "Pan";
			this.Pan.ReadOnly = true;
			this.Pan.Width = 57;
			// 
			// Pannoofaffl
			// 
			this.Pannoofaffl.HeaderText = "PAN No. of Affiliates, in case the recipient is an entity";
			this.Pannoofaffl.Name = "Pannoofaffl";
			this.Pannoofaffl.ReadOnly = true;
			this.Pannoofaffl.Width = 169;
			// 
			// detailsofUPID
			// 
			this.detailsofUPID.HeaderText = "Details of UPSI along with reason of sharing";
			this.detailsofUPID.Name = "detailsofUPID";
			this.detailsofUPID.ReadOnly = true;
			this.detailsofUPID.Width = 154;
			// 
			// Datteime
			// 
			this.Datteime.HeaderText = "Date and Time of Sharing";
			this.Datteime.Name = "Datteime";
			this.Datteime.ReadOnly = true;
			this.Datteime.Width = 115;
			// 
			// NDSsigned
			// 
			this.NDSsigned.HeaderText = "Whether NDA has been signed and Notice of confidentiality has been given?";
			this.NDSsigned.Name = "NDSsigned";
			this.NDSsigned.ReadOnly = true;
			this.NDSsigned.Width = 225;
			// 
			// DateofEntry
			// 
			this.DateofEntry.HeaderText = "Date of first entry with User Name";
			this.DateofEntry.Name = "DateofEntry";
			this.DateofEntry.ReadOnly = true;
			this.DateofEntry.Width = 119;
			// 
			// Dateofsecofnetry
			// 
			this.Dateofsecofnetry.HeaderText = "Date of second and all entry with User Name";
			this.Dateofsecofnetry.Name = "Dateofsecofnetry";
			this.Dateofsecofnetry.ReadOnly = true;
			this.Dateofsecofnetry.Width = 142;
			// 
			// datehwnupsi
			// 
			this.datehwnupsi.HeaderText = "Date when UPSI became publicly available";
			this.datehwnupsi.Name = "datehwnupsi";
			this.datehwnupsi.ReadOnly = true;
			this.datehwnupsi.Width = 175;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Transparent;
			this.button1.BackgroundImage = global::OS.Properties.Resources.icons8_refresh;
			this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(475, 47);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(46, 40);
			this.button1.TabIndex = 103;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnDownloadPrinter
			// 
			this.btnDownloadPrinter.BackColor = System.Drawing.Color.Transparent;
			this.btnDownloadPrinter.BackgroundImage = global::OS.Properties.Resources.icons8_print_filled;
			this.btnDownloadPrinter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnDownloadPrinter.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
			this.btnDownloadPDF.BackColor = System.Drawing.Color.Transparent;
			this.btnDownloadPDF.BackgroundImage = global::OS.Properties.Resources.icons8_pdf;
			this.btnDownloadPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnDownloadPDF.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDownloadPDF.Location = new System.Drawing.Point(1210, 48);
			this.btnDownloadPDF.Name = "btnDownloadPDF";
			this.btnDownloadPDF.Size = new System.Drawing.Size(46, 40);
			this.btnDownloadPDF.TabIndex = 101;
			this.btnDownloadPDF.UseVisualStyleBackColor = false;
			this.btnDownloadPDF.Click += new System.EventHandler(this.btnDownloadPDF_Click);
			// 
			// btnDownloadexcel
			// 
			this.btnDownloadexcel.BackColor = System.Drawing.Color.Transparent;
			this.btnDownloadexcel.BackgroundImage = global::OS.Properties.Resources.icons8_microsoft_excel;
			this.btnDownloadexcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnDownloadexcel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDownloadexcel.Location = new System.Drawing.Point(1138, 47);
			this.btnDownloadexcel.Name = "btnDownloadexcel";
			this.btnDownloadexcel.Size = new System.Drawing.Size(46, 40);
			this.btnDownloadexcel.TabIndex = 100;
			this.btnDownloadexcel.UseVisualStyleBackColor = false;
			this.btnDownloadexcel.Click += new System.EventHandler(this.btnDownloadexcel_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(1046, 58);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(79, 19);
			this.label6.TabIndex = 99;
			this.label6.Text = "Download :";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(1007, 49);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(20, 31);
			this.label5.TabIndex = 98;
			this.label5.Text = "|";
			// 
			// btnSearch
			// 
			this.btnSearch.BackColor = System.Drawing.Color.Transparent;
			this.btnSearch.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
			this.txtInsiderID.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtInsiderID.Location = new System.Drawing.Point(111, 55);
			this.txtInsiderID.MaxLength = 20;
			this.txtInsiderID.Name = "txtInsiderID";
			this.txtInsiderID.Size = new System.Drawing.Size(217, 26);
			this.txtInsiderID.TabIndex = 95;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(107, 33);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 19);
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
			this.button2.BackColor = System.Drawing.Color.Transparent;
			this.button2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.Location = new System.Drawing.Point(722, 41);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(94, 40);
			this.button2.TabIndex = 106;
			this.button2.Text = "CLOSE";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
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
			this.Controls.Add(this.label5);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.txtInsiderID);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.panel1);
			this.Text = "REPORTS_OF_SHARING_OF_UPSI";
			this.Load += new System.EventHandler(this.REPORTS_OF_SHARING_OF_UPSI_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGridView dataGridViewTable;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnDownloadPrinter;
		private System.Windows.Forms.Button btnDownloadPDF;
		private System.Windows.Forms.Button btnDownloadexcel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnSearch;
		public System.Windows.Forms.TextBox txtInsiderID;
		private System.Windows.Forms.Label label4;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private System.Windows.Forms.DataGridViewTextBoxColumn UPSID;
		private System.Windows.Forms.DataGridViewTextBoxColumn InsiderIDCOnnID;
		private System.Windows.Forms.DataGridViewTextBoxColumn Name;
		private System.Windows.Forms.DataGridViewTextBoxColumn Category;
		private System.Windows.Forms.DataGridViewTextBoxColumn Pan;
		private System.Windows.Forms.DataGridViewTextBoxColumn Pannoofaffl;
		private System.Windows.Forms.DataGridViewTextBoxColumn detailsofUPID;
		private System.Windows.Forms.DataGridViewTextBoxColumn Datteime;
		private System.Windows.Forms.DataGridViewTextBoxColumn NDSsigned;
		private System.Windows.Forms.DataGridViewTextBoxColumn DateofEntry;
		private System.Windows.Forms.DataGridViewTextBoxColumn Dateofsecofnetry;
		private System.Windows.Forms.DataGridViewTextBoxColumn datehwnupsi;
		private System.Windows.Forms.Button button2;
	}
}