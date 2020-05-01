using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OS
{
	public partial class AUDIT_TRAIL : MASTERFORM
	{
		private Bitmap bitmap;
		public AUDIT_TRAIL()
		{
			InitializeComponent();
		}

		private void AUDIT_TRAIL_Load(object sender, EventArgs e)
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

		private void FillDataGrid()
		{
			SetLoading(true);

			dataGridViewTable.Rows.Clear();
			dataGridViewTable.Refresh();

			DataSet ds = new MasterClass().getDataSet("SELECT * FROM M_LOG_AUDIT ORDER BY ENTEREDON DESC");
			if (ds.Tables[0].Rows.Count > 0)
			{
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					DataSet sgrow = new DataSet();
					string output = "";
					if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "CONNECTED PERSON TAB")
					{
						sgrow = new MasterClass().getDataSet("SELECT CONNECTPERSONID AS [Connect Person Id],EMPNAME AS [Name of the Employee],CURRDESIGNATION AS [Current Designation],ADDRESS AS [Address], PANNO AS[PAN], DEMATACNO AS [Demat A / c No], MOBILENO AS [Mobile No],GRADUATIONINSTI AS [Graduation Institution], PASTEMP AS [Past Employee] FROM T_INS_PER_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
						string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

						for (int j = 0; j < columnNames.Length; j++)
						{
							if (j == 0)
							{
								output += j + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
							}
							else
							{
								output += " | " + j + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
							}
						}

					}

					if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "INSIDER PROFILE TAB")
					{
						sgrow = new MasterClass().getDataSet("SELECT RECEPIENTID AS [Recipient Id],NAMEINSIDER AS [Name of the Insider],CATEGORYRECEIPT AS [Category of Receipt], ADDRESS AS [Address], PANNO AS [PAN], AADHARNO AS [Aadhar No],MOBILENO AS [Mobile No],LANDLINENO AS[Landline No], EMAILID AS [Email Id], PANNOAFFILIATES AS [PAN No.of Affiliates] FROM T_INS_PRO_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
						string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

						for (int j = 0; j < columnNames.Length; j++)
						{
							if (j == 0)
							{
								output += CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
							}
							else
							{
								output += "|" + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
							}
						}

					}

					if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "SHARING OF UPSI PROFILE TAB")
					{
						sgrow = new MasterClass().getDataSet("SELECT UPSIID AS [UPSI ID],RECIPIENTNAME AS [Recipient Name],RECIPIENTCAT AS [Category of Recipient],PANNO AS [PAN], ADDRESS AS [Address],UPSINATURE AS [Nature of UPSI], SHARINGPURPOSE AS [Purpose of Sharing], SHARINGDATE AS [Date of Sharing],EFFECTIVEUPTO AS [Effective Upto],REMARKS AS [Remarks], NDASIGNED AS [Whether NDA has been signed and Notice of confidentiality has been given ?],UPSIAVAILABLE AS [Date when UPSI became publicly available] FROM T_INS_UPSI_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
						string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

						for (int j = 0; j < columnNames.Length; j++)
						{
							if (j == 0)
							{
								output += CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
							}
							else
							{
								output += "|" + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
							}
						}

					}

					DataSet dsLogin = new MasterClass().getDataSet("SELECT EMAIL FROM T_LOGIN WHERE ID = '" + ds.Tables[0].Rows[i]["ENTEREDBY"].ToString() + "'");
					string a = "";
					if (dsLogin.Tables[0].Rows.Count > 0)
					{
						a = dsLogin.Tables[0].Rows[0]["EMAIL"].ToString();
					}


					string[] row = { a.Trim(), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DESCRIPTION"].ToString()), ds.Tables[0].Rows[i]["ENTEREDON"].ToString(), output };
					dataGridViewTable.Rows.Add(row);
				}
			}

			SetLoading(false);
		}

		private void SetLoading(bool displayLoader)
		{
			if (displayLoader)
			{
				//Invoke((MethodInvoker)delegate
				//{
				//	picLoader.Visible = true;
				//	Cursor = Cursors.WaitCursor;
				//});
			}
			else
			{
				//Invoke((MethodInvoker)delegate
				//{
				//	picLoader.Visible = false;
				//	Cursor = Cursors.Default;
				//});
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			HOMEPAGE h = new HOMEPAGE();
			h.Show();
			Hide();
		}
	}
}
