using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OS
{
	public partial class REPORTS_OF_SHARING_OF_UPSI : MASTERFORM
	{
		AUDITLOG lg = new AUDITLOG();
		private Bitmap bitmap;
		public REPORTS_OF_SHARING_OF_UPSI()
		{
			InitializeComponent();
		}

		private void REPORTS_OF_SHARING_OF_UPSI_Load(object sender, EventArgs e)
		{
			Login l = new Login();
			//TopMost = true;
			WindowState = FormWindowState.Maximized;
			try
			{
				if (SESSIONKEYS.UserID.ToString() == "" || SESSIONKEYS.UserID.ToString() == null)
				{
					Hide();
					l.Show();
				}
			}
			catch (Exception)
			{
				Hide();
				l.Show();
			}
			//txtFromDate.CustomFormat = "dd-MM-yyyy";
			//txtToDate.CustomFormat = "dd-MM-yyyy";
			FillDataGrid();
		}

		private void FillDataGrid()
		{
			dataGridViewTable.Rows.Clear();
			dataGridViewTable.Refresh();
			DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_UPSI P INNER JOIN T_LOGIN L ON L.ID = P.ENTEREDBY WHERE P.ACTIVE = 'Y' AND L.ACTIVE = 'Y'");
			if (ds.Tables[0].Rows.Count > 0)
			{
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					string cat = "";
					if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECIPIENTCAT"].ToString()).Contains("|"))
					{
						string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECIPIENTCAT"].ToString()).Split('|');
						cat = abc[0] + " - " + abc[1];
					}
					else
					{
						cat = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECIPIENTCAT"].ToString());
					}
					string[] vabc = { };
					if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECIPIENTNAME"].ToString()).Contains("-"))
					{
						vabc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECIPIENTNAME"].ToString()).Split('-');
					}

					string[] nda = { };
					if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NDASIGNED"].ToString()).Contains("|"))
					{
						nda = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NDASIGNED"].ToString()).Split('|');
					}

					string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIID"].ToString()), vabc[1], vabc[0], cat, CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), nda[1], CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["SHARINGPURPOSE"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["SHARINGDATE"].ToString()), nda[0], (ds.Tables[0].Rows[i]["ENTEREDON"].ToString() + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), "", CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIAVAILABLE"].ToString()) };
					dataGridViewTable.Rows.Add(row);
				}
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtInsiderID.Text != "")
				{
					int rowIndex = -1;
					foreach (DataGridViewRow row in dataGridViewTable.Rows)
					{
						if (row.Cells[0].Value.ToString().Equals(txtInsiderID.Text))
						{
							rowIndex = row.Index;
							//dataGridViewTable.Rows[rowIndex].Selected = true;
						}
						else
						{
							dataGridViewTable.Rows.RemoveAt(row.Index);
						}
					}
				}
				else
				{
					//FillDataGrid(txtInsiderID.Text, txtFromDate.Value.ToString(), txtToDate.Value.ToString());
				}
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Following Value doesnt Match Any Records.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void FillDataGrid(string text, string From, string To)
		{

			dataGridViewTable.Rows.Clear();
			dataGridViewTable.Refresh();
			Hashtable hstmst = new Hashtable
				{
					{ "@FROM", From},
					{ "@TO", To },
					{ "@ACTION", "6" }
				};
			DataSet ds = new MasterClass().executeDatable_SP("STP_INS_PRO", hstmst);
			if (ds.Tables[0].Rows.Count > 0)
			{
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					string cat = "";

					if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CATEGORYRECEIPT"].ToString()).Contains("|"))
					{

						string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CATEGORYRECEIPT"].ToString()).Split('|');
						cat = abc[0] + " - " + abc[1];
					}
					else
					{
						cat = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CATEGORYRECEIPT"].ToString());
					}

					string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAMEINSIDER"].ToString()), cat, CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["AADHARNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNOAFFILIATES"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["LANDLINENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString()) };
					dataGridViewTable.Rows.Add(row);
				}
			}
		}

		private void btnDownloadexcel_Click(object sender, EventArgs e)
		{
			if (dataGridViewTable.Rows.Count > 0)
			{
				lg.CURRVALUE = "SHARING OF UPSI PROFILE TAB";
				lg.DESCRIPTION = "DOWNLOADED EXCEL FILE";
				lg.TYPE = "SELECTED";
				lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
				lg.ID = SESSIONKEYS.UserID.ToString();
				string json = new MasterClass().SAVE_LOG(lg);

				SaveFileDialog sfd = new SaveFileDialog
				{
					Filter = "Excel Documents (*.xls)|*.xls",
					FileName = "LIST OF SHARING OF UPSI.xls"
				};

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					new MasterClass().ToCsV(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
					MessageBox.Show("Exported Data Successfully in Excel Sheet.", "LIST OF SHARING OF UPSI", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}

		private void btnDownloadPDF_Click(object sender, EventArgs e)
		{
			if (dataGridViewTable.Rows.Count > 0)
			{
				lg.CURRVALUE = "SHARING OF UPSI PROFILE TAB";
				lg.DESCRIPTION = "DOWNLOADED PDF FILE";
				lg.TYPE = "SELECTED";
				lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
				lg.ID = SESSIONKEYS.UserID.ToString();
				string json = new MasterClass().SAVE_LOG(lg);

				SaveFileDialog sfd = new SaveFileDialog
				{
					Filter = "PDF (*.pdf)|*.pdf",
					FileName = "LIST OF SHARING OF UPSI.pdf"
				};
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					new MasterClass().ToPDF(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
					MessageBox.Show("Exported Data Successfully in PDF Sheet.", "LIST OF SHARING OF UPSI", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else
			{
				MessageBox.Show("No Record To Export !!!", "Info");
			}
		}

		private void btnDownloadPrinter_Click(object sender, EventArgs e)
		{
			lg.CURRVALUE = "SHARING OF UPSI PROFILE TAB";
			lg.DESCRIPTION = "PRINT FILE";
			lg.TYPE = "SELECTED";
			lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
			lg.ID = SESSIONKEYS.UserID.ToString();
			string json = new MasterClass().SAVE_LOG(lg);

			//Resize DataGridView to full height.
			int height = dataGridViewTable.Height;
			dataGridViewTable.Height = dataGridViewTable.RowCount * dataGridViewTable.RowTemplate.Height;

			//Create a Bitmap and draw the DataGridView on it.
			bitmap = new Bitmap(dataGridViewTable.Width, dataGridViewTable.Height);
			dataGridViewTable.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridViewTable.Width, dataGridViewTable.Height));

			//Resize DataGridView back to original height.
			dataGridViewTable.Height = height;

			//Show the Print Preview Dialog.
			printPreviewDialog1.Document = printDocument1;
			printPreviewDialog1.PrintPreviewControl.Zoom = 1;
			printPreviewDialog1.ShowDialog();
		}

		private void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			//Print the contents.
			e.Graphics.DrawImage(bitmap, 0, 0);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			FillDataGrid();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			HOMEPAGE h = new HOMEPAGE();
			h.Show();
			Hide();
		}
	}
}
