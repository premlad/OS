using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace OS
{
	public partial class LIST_OF_INSIDERS : MASTERFORM
	{
		private AUDITLOG lg = new AUDITLOG();
		private Bitmap bitmap;
		public LIST_OF_INSIDERS()
		{
			InitializeComponent();
		}

		private void LIST_OF_INSIDERS_Load(object sender, EventArgs e)
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
			txtFromDate.CustomFormat = "dd-MM-yyyy";
			txtToDate.CustomFormat = "dd-MM-yyyy";
			FillDataGrid();
		}

		private const int CP_NOCLOSE_BUTTON = 0x200;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams myCp = base.CreateParams;
				myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
				return myCp;
			}
		}

		#region LIST OF INSIDERS

		public void FillDataGrid(string text, string From, string To)
		{

			try
			{
				SetLoading(true);

				Thread.Sleep(2000);
				Invoke((MethodInvoker)delegate
				{
					dataGridViewTable.Rows.Clear();
					dataGridViewTable.Refresh();
					DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_PRO P INNER JOIN T_LOGIN L ON L.ID = P.ENTEREDBY WHERE P.ACTIVE = 'Y' AND L.ACTIVE = 'Y'");
					if (ds.Tables[0].Rows.Count > 0)
					{
						for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
						{
							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString()) == text)
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

								string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAMEINSIDER"].ToString()), cat, CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["AADHARNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNOAFFILIATES"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["LANDLINENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), (ds.Tables[0].Rows[i]["ENTEREDON"].ToString() + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim() };
								dataGridViewTable.Rows.Add(row);
							}

						}
					}
				});

				SetLoading(false);
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Please Check Your Internet Connection.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnDownloadexcel_Click(object sender, EventArgs e)
		{
			try
			{
				SetLoading(true);

				//Thread.Sleep(2000);
				Invoke((MethodInvoker)delegate
				{
					if (dataGridViewTable.Rows.Count > 0)
					{
						lg.CURRVALUE = "INSIDER PROFILE TAB";
						lg.DESCRIPTION = "DOWNLOADED EXCEL FILE";
						lg.TYPE = "SELECTED";
						lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
						lg.ID = SESSIONKEYS.UserID.ToString();
						string json = new MasterClass().SAVE_LOG(lg);

						SaveFileDialog sfd = new SaveFileDialog
						{
							Filter = "Excel Documents (*.xls)|*.xls",
							FileName = "LIST OF INSIDERS.xls"
						};

						if (sfd.ShowDialog() == DialogResult.OK)
						{
							new MasterClass().ToCsV(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
							MessageBox.Show("Exported Data Successfully in Excel Sheet.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
				});

				SetLoading(false);
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnDownloadPDF_Click(object sender, EventArgs e)
		{
			try
			{
				SetLoading(true);

				//Thread.Sleep(2000);
				Invoke((MethodInvoker)delegate
				{
					if (dataGridViewTable.Rows.Count > 0)
					{
						lg.CURRVALUE = "INSIDER PROFILE TAB";
						lg.DESCRIPTION = "DOWNLOADED PDF FILE";
						lg.TYPE = "SELECTED";
						lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
						lg.ID = SESSIONKEYS.UserID.ToString();
						string json = new MasterClass().SAVE_LOG(lg);


						SaveFileDialog sfd = new SaveFileDialog
						{
							Filter = "PDF (*.pdf)|*.pdf",
							FileName = "LIST OF INSIDERS.pdf"
						};
						if (sfd.ShowDialog() == DialogResult.OK)
						{
							new MasterClass().ToPDF(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
							MessageBox.Show("Exported Data Successfully in PDF Sheet.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
					else
					{
						MessageBox.Show("No Record To Export !!!", "Info");
					}
				});

				SetLoading(false);
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnDownloadPrinter_Click(object sender, EventArgs e)
		{
			lg.CURRVALUE = "INSIDER PROFILE TAB";
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

		private void SetLoading(bool displayLoader)
		{
			if (displayLoader)
			{
				Invoke((MethodInvoker)delegate
				{
					//picLoader.Visible = true;
					Cursor = Cursors.WaitCursor;
					//Thread.Sleep(4000);
				});
			}
			else
			{
				Invoke((MethodInvoker)delegate
				{
					//picLoader.Visible = false;
					Cursor = Cursors.Default;
				});
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{

			try
			{
				if (txtInsiderID.Text != "")
				{
					FillDataGrid(txtInsiderID.Text, txtFromDate.Value.ToString(), txtToDate.Value.ToString());
					//int rowIndex = -1;
					//foreach (DataGridViewRow row in dataGridViewTable.Rows)
					//{
					//	if (row.Cells[0].Value.ToString().Equals(txtInsiderID.Text))
					//	{
					//		rowIndex = row.Index;
					//		//dataGridViewTable.Rows[rowIndex].Selected = true;
					//	}
					//	else
					//	{
					//		dataGridViewTable.Rows.RemoveAt(row.Index);
					//	}
					//}
				}
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Following Value doesnt Match Any Records.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			txtInsiderID.Text = "";
			FillDataGrid();
		}

		private void FillDataGrid()
		{
			try
			{
				SetLoading(true);

				Thread.Sleep(2000);
				Invoke((MethodInvoker)delegate
				{
					dataGridViewTable.Rows.Clear();
					dataGridViewTable.Refresh();
					DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_PRO P INNER JOIN T_LOGIN L ON L.ID = P.ENTEREDBY WHERE P.ACTIVE = 'Y' AND L.ACTIVE = 'Y'");
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

							string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAMEINSIDER"].ToString()), cat, CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["AADHARNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNOAFFILIATES"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["LANDLINENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), (ds.Tables[0].Rows[i]["ENTEREDON"].ToString() + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim() };
							dataGridViewTable.Rows.Add(row);
						}
					}
				});

				SetLoading(false);
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Please Check Your Internet Connection.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

		private void button2_Click(object sender, EventArgs e)
		{
			HOMEPAGE h = new HOMEPAGE();
			h.Show();
			Hide();
		}
	}
}
