using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace OS
{
	public partial class List_of_PAN_s_Registered_under_Database : MASTERFORM
	{
		private AUDITLOG lg = new AUDITLOG();
		public List_of_PAN_s_Registered_under_Database()
		{
			InitializeComponent();
		}

		private void List_of_PAN_s_Registered_under_Database_Load(object sender, EventArgs e)
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
			FillDataGrid();
		}

		#region LIST OF PAN'S REGISTERED IN DB

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
					DataSet ds = new MasterClass().getDataSet("SELECT NAMEINSIDER AS [NAME],PANNO,'' AS [DEMATAC],OTHERIDENTIFIER,PANNOAFFILIATES FROM T_INS_PRO");
					DataSet ds1 = new MasterClass().getDataSet("SELECT EMPNAME AS [NAME],PANNO,DEMATACNO AS [DEMATAC],OTHERIDENTIFIER FROM T_INS_PER");
					DataSet ds2 = new MasterClass().getDataSet("SELECT NAME AS [NAME],PANNO,DEMATACNO AS [DEMATAC] FROM T_INS_PER_DT");
					if (ds.Tables[0].Rows.Count > 0)
					{
						for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
						{
							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNOAFFILIATES"].ToString()) == "")
							{
								string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATAC"].ToString()) };
								dataGridViewTable.Rows.Add(row);
							}
							else
							{
								string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()) + " | PAN No. of Affiliates : " + CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNOAFFILIATES"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATAC"].ToString()) };
								dataGridViewTable.Rows.Add(row);
							}

						}
					}

					if (ds1.Tables[0].Rows.Count > 0)
					{
						for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
						{
							string[] row = { CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString()), CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["DEMATAC"].ToString()) };
							dataGridViewTable.Rows.Add(row);
						}
					}

					if (ds2.Tables[0].Rows.Count > 0)
					{
						for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
						{
							if (CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["PANNO"].ToString()).Trim() != "")
							{
								string[] row = { CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["PANNO"].ToString()), "", CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["DEMATAC"].ToString()) };
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
				DialogResult dialog = MessageBox.Show("Something Went Wrong.", "List of PAN’s Registered under Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			FillDataGrid();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			HOMEPAGE h = new HOMEPAGE();
			h.Show();
			Close();
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
						lg.CURRVALUE = "LIST OF PAN'S REGISTERED IN DB TAB";
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
							MessageBox.Show("Exported Data Successfully in PDF Sheet.", "List of PAN’s Registered under Database", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
				DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
						lg.CURRVALUE = "LIST OF PAN'S REGISTERED IN DB TAB";
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
							MessageBox.Show("Exported Data Successfully in Excel Sheet.", "List of PAN’s Registered under Database", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
				});

				SetLoading(false);
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				SetLoading(false);
				DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "List of PAN’s Registered under Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
	}
}
