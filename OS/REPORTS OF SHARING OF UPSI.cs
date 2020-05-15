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
	public partial class REPORTS_OF_SHARING_OF_UPSI : MASTERFORM
	{
		private AUDITLOG lg = new AUDITLOG();
		private Bitmap bitmap;
		public REPORTS_OF_SHARING_OF_UPSI()
		{
			InitializeComponent();
		}

		private void REPORTS_OF_SHARING_OF_UPSI_Load(object sender, EventArgs e)
		{
			Login l = new Login();
			//TopMost = true;
			//WindowState = FormWindowState.Maximized;
			try
			{
				if (SESSIONKEYS.UserID.ToString() == "" || SESSIONKEYS.UserID.ToString() == null)
				{
					Close();
					l.Show();
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				Close();
				l.Show();
			}
			//txtFromDate.CustomFormat = "dd-MM-yyyy";
			//txtToDate.CustomFormat = "dd-MM-yyyy";
			FillDataGrid();
		}

		private void SetLoading(bool displayLoader)
		{
			try
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
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				//Login l = new Login();
				//l.Show();
				//Close();
			}
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
				});

				SetLoading(false);
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				SetLoading(false);
				DialogResult dialog = MessageBox.Show("Something Went Wrong.", "LIST OF SHARING OF UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtInsiderID.Text != "")
				{
					FillDataGrid(txtInsiderID.Text, "", "");
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("Following Value doesnt Match Any Records.", "LIST OF SHARING OF UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

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
					DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_UPSI P INNER JOIN T_LOGIN L ON L.ID = P.ENTEREDBY WHERE P.ACTIVE = 'Y' AND L.ACTIVE = 'Y'");
					if (ds.Tables[0].Rows.Count > 0)
					{
						for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
						{
							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIID"].ToString()) == text)
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
				});

				SetLoading(false);
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				SetLoading(false);
				DialogResult dialog = MessageBox.Show("Something Went Wrong.", "LIST OF SHARING OF UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
					else
					{
						MessageBox.Show("No Record To Export !!!", "Info");
					}
				});

				SetLoading(false);
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				SetLoading(false);
				DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "LIST OF SHARING OF UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				});

				SetLoading(false);
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				SetLoading(false);
				DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "LIST OF SHARING OF UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			txtInsiderID.Text = "";
			FillDataGrid();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			HOMEPAGE h = new HOMEPAGE();
			h.Show();
			Close();
		}
	}
}
