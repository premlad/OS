using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace OS
{
	public partial class AUDIT_TRAIL : MASTERFORM
	{
		private AUDITLOG lg = new AUDITLOG();
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
			try
			{
				SetLoading(true);

				Thread.Sleep(2000);
				Invoke((MethodInvoker)delegate
				{

					dataGridViewTable.Rows.Clear();
					dataGridViewTable.Refresh();

					DataSet ds = new MasterClass().getDataSet("SELECT * FROM M_LOG_AUDIT ORDER BY ENTEREDON DESC");
					if (ds.Tables[0].Rows.Count > 0)
					{
						for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
						{
							DataSet sgrow = new DataSet();
							DataSet updtrow = new DataSet();
							string output = "";
							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "CONNECTED PERSON TAB")
							{
								sgrow = new MasterClass().getDataSet("SELECT TID,CONNECTPERSONID AS [Connect Person Id],EMPNAME AS [Name of the Employee],CURRDESIGNATION AS [Current Designation],ADDRESS AS [Address], PANNO AS[PAN], DEMATACNO AS [Demat A / c No], MOBILENO AS [Mobile No],GRADUATIONINSTI AS [Graduation Institution], PASTEMP AS [Past Employee] FROM T_INS_PER_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");

								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
								{
									updtrow = new MasterClass().getDataSet("SELECT CONNECTPERSONID AS [Connect Person Id],EMPNAME AS [Name of the Employee],CURRDESIGNATION AS [Current Designation],ADDRESS AS [Address], PANNO AS[PAN], DEMATACNO AS [Demat A / c No], MOBILENO AS [Mobile No],GRADUATIONINSTI AS [Graduation Institution], PASTEMP AS [Past Employee] FROM T_INS_PER WHERE ID = '" + sgrow.Tables[0].Rows[0]["TID"].ToString() + "'");

									string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
									output += "Updated Value :- ";
									for (int j = 0; j < columnNames.Length; j++)
									{
										if (j == 0)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
									}
									output += Environment.NewLine + "\nPrevious Value :- ";

									string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames1.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}
								else
								{
									string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}

							}

							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "CONNECTED PERSON TAB RELATIVE RELATIONSHIP")
							{
								sgrow = new MasterClass().getDataSet("SELECT TID,NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat Ac No],TYPE as [TYPE] FROM T_INS_PER_DT_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
								{
									updtrow = new MasterClass().getDataSet("SELECT NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat Ac No],TYPE as [TYPE] FROM T_INS_PER_DT WHERE ID = '" + sgrow.Tables[0].Rows[0]["TID"].ToString() + "'");

									string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
									output += "Updated Value :- ";
									for (int j = 0; j < columnNames.Length; j++)
									{
										if (j == 0)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
									}
									output += " Previous Value :- ";

									string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames1.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}
								else
								{
									string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}

							}

							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "CONNECTED PERSON TAB FINANCIAL RELATIONSHIP")
							{
								sgrow = new MasterClass().getDataSet("SELECT TID,NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat Ac No],TYPE as [TYPE] FROM T_INS_PER_DT_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
								{
									updtrow = new MasterClass().getDataSet("SELECT NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat Ac No],TYPE as [TYPE] FROM T_INS_PER_DT WHERE ID = '" + sgrow.Tables[0].Rows[0]["TID"].ToString() + "'");

									string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
									output += "Updated Value :- ";
									for (int j = 0; j < columnNames.Length; j++)
									{
										if (j == 0)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
									}
									output += " Previous Value :- ";

									string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames1.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}
								else
								{
									string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}

							}

							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "INSIDER PROFILE TAB")
							{
								sgrow = new MasterClass().getDataSet("SELECT TID,RECEPIENTID AS [Recipient Id],NAMEINSIDER AS [Name of the Insider],CATEGORYRECEIPT AS [Category of Receipt], ADDRESS AS [Address], PANNO AS [PAN], AADHARNO AS [Aadhar No],MOBILENO AS [Mobile No],LANDLINENO AS[Landline No], EMAILID AS [Email Id], PANNOAFFILIATES AS [PAN No.of Affiliates] FROM T_INS_PRO_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
								{
									updtrow = new MasterClass().getDataSet("SELECT RECEPIENTID AS [Recipient Id],NAMEINSIDER AS [Name of the Insider],CATEGORYRECEIPT AS [Category of Receipt], ADDRESS AS [Address], PANNO AS [PAN], AADHARNO AS [Aadhar No],MOBILENO AS [Mobile No],LANDLINENO AS[Landline No], EMAILID AS [Email Id], PANNOAFFILIATES AS [PAN No.of Affiliates] FROM T_INS_PRO WHERE ID = '" + sgrow.Tables[0].Rows[0]["TID"].ToString() + "'");

									string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
									output += "Updated Value :- ";
									for (int j = 0; j < columnNames.Length; j++)
									{
										if (j == 0)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
									}
									output += Environment.NewLine + "\nPrevious Value :- ";

									string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames1.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}
								else
								{
									string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}


							}

							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "SHARING OF UPSI PROFILE TAB")
							{
								sgrow = new MasterClass().getDataSet("SELECT TID,UPSIID AS [UPSI ID],RECIPIENTNAME AS [Recipient Name],RECIPIENTCAT AS [Category of Recipient],PANNO AS [PAN], ADDRESS AS [Address],UPSINATURE AS [Nature of UPSI], SHARINGPURPOSE AS [Purpose of Sharing], SHARINGDATE AS [Date of Sharing],EFFECTIVEUPTO AS [Effective Upto],REMARKS AS [Remarks], NDASIGNED AS [Whether NDA has been signed and Notice of confidentiality has been given ?],UPSIAVAILABLE AS [Date when UPSI became publicly available] FROM T_INS_UPSI_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
								{
									updtrow = new MasterClass().getDataSet("SELECT UPSIID AS [UPSI ID],RECIPIENTNAME AS [Recipient Name],RECIPIENTCAT AS [Category of Recipient],PANNO AS [PAN], ADDRESS AS [Address],UPSINATURE AS [Nature of UPSI], SHARINGPURPOSE AS [Purpose of Sharing], SHARINGDATE AS [Date of Sharing],EFFECTIVEUPTO AS [Effective Upto],REMARKS AS [Remarks], NDASIGNED AS [Whether NDA has been signed and Notice of confidentiality has been given ?],UPSIAVAILABLE AS [Date when UPSI became publicly available] FROM T_INS_UPSI WHERE ID = '" + sgrow.Tables[0].Rows[0]["TID"].ToString() + "'");

									string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
									output += "Updated Value :- ";
									for (int j = 0; j < columnNames.Length; j++)
									{

										//for (int j = 0; j < columnNames.Length; j++)
										//{
										//	string[] columnNames12 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

										//	for (int k = 1; k <= columnNames12.Length - 1; k++)
										//	{
										//		if (columnNames[j] == columnNames12[k])
										//		{
										//			if (CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()) == CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][k].ToString()))
										//			{
										//				if (j == 0)
										//				{
										//					output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										//				}
										//				else
										//				{
										//					output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										//				}
										//			}
										//			else
										//			{
										//				if (j == 0)
										//				{
										//					output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										//				}
										//				else
										//				{
										//					output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										//				}
										//			}
										//		}
										//	}


										//}



										if (j == 0)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
									}
									output += Environment.NewLine + "\nPrevious Value :- ";

									string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames1.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}
								else
								{
									string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}

							}

							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "MASTER DATA OF COMPANY PROFILE TAB")
							{
								sgrow = new MasterClass().getDataSet("SELECT TID,COMPANYNAME AS [Company Name],REGOFFICE AS [Registered Office],CORPORATEOFFICE AS [Corporate Office],MOBILENO AS [Mobile No],LANDLINENO AS [Landline No],EMAILID AS [Email Id],CIN AS [CIN],BSECODE AS [BSE SCRIP CODE],NSECODE AS [NSE SCIRP CODE],ISIN AS [ISIN],OFFICERNAME AS [Compliance Officer Name],DESIGNATION AS [Desgination] FROM T_INS_COMPANY_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
								{
									updtrow = new MasterClass().getDataSet("SELECT COMPANYNAME AS [Company Name],REGOFFICE AS [Registered Office],CORPORATEOFFICE AS [Corporate Office],MOBILENO AS [Mobile No],LANDLINENO AS [Landline No],EMAILID AS [Email Id],CIN AS [CIN],BSECODE AS [BSE SCRIP CODE],NSECODE AS [NSE SCIRP CODE],ISIN AS [ISIN],OFFICERNAME AS [Compliance Officer Name],DESIGNATION AS [Desgination] FROM T_INS_COMPANY WHERE ID = '" + sgrow.Tables[0].Rows[0]["TID"].ToString() + "'");

									string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
									output += "Updated Value :- ";
									for (int j = 0; j < columnNames.Length; j++)
									{
										if (j == 0)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
									}
									output += Environment.NewLine + "\nPrevious Value :- ";

									string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames1.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}
								else
								{
									string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
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

					dataGridViewTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

				});

				SetLoading(false);
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Please Check Your Internet Connection.", "Audit Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
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

		private void button2_Click(object sender, EventArgs e)
		{
			HOMEPAGE h = new HOMEPAGE();
			h.Show();
			Hide();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				FillDataGrid(txtFromDate.Value, txtToDate.Value);
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Following Value doesnt Match Any Records.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			FillDataGrid();
		}

		public void FillDataGrid(DateTime From, DateTime To)
		{

			try
			{
				SetLoading(true);

				Thread.Sleep(2000);
				Invoke((MethodInvoker)delegate
				{


					dataGridViewTable.Rows.Clear();
					dataGridViewTable.Refresh();

					string qry = "SELECT * FROM M_LOG_AUDIT WHERE 1 = 1 ";
					if (From.ToString() != "" || From.ToString() != null)
					{
						qry += " AND CONVERT(DATETIME,ENTEREDON) >= CONVERT(DATETIME,'" + From.ToString("yyyy-MM-dd 00:00:00") + "')";
					}

					if (To.ToString() != "" || To.ToString() != null)
					{
						qry += " AND CONVERT(DATETIME,ENTEREDON) <= CONVERT(DATETIME,'" + To.ToString("yyyy-MM-dd 23:59:59") + "')";
					}

					qry += "ORDER BY ENTEREDON DESC";

					DataSet ds = new MasterClass().getDataSet(qry);
					if (ds.Tables[0].Rows.Count > 0)
					{
						for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
						{
							DataSet sgrow = new DataSet();
							DataSet updtrow = new DataSet();
							string output = "";
							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "CONNECTED PERSON TAB")
							{
								sgrow = new MasterClass().getDataSet("SELECT TID,CONNECTPERSONID AS [Connect Person Id],EMPNAME AS [Name of the Employee],CURRDESIGNATION AS [Current Designation],ADDRESS AS [Address], PANNO AS[PAN], DEMATACNO AS [Demat A / c No], MOBILENO AS [Mobile No],GRADUATIONINSTI AS [Graduation Institution], PASTEMP AS [Past Employee] FROM T_INS_PER_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");

								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
								{
									updtrow = new MasterClass().getDataSet("SELECT CONNECTPERSONID AS [Connect Person Id],EMPNAME AS [Name of the Employee],CURRDESIGNATION AS [Current Designation],ADDRESS AS [Address], PANNO AS[PAN], DEMATACNO AS [Demat A / c No], MOBILENO AS [Mobile No],GRADUATIONINSTI AS [Graduation Institution], PASTEMP AS [Past Employee] FROM T_INS_PER WHERE ID = '" + sgrow.Tables[0].Rows[0]["TID"].ToString() + "'");

									string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
									output += "Updated Value :- ";
									for (int j = 0; j < columnNames.Length; j++)
									{
										if (j == 0)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
									}
									output += " Previous Value :- ";

									string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames1.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}
								else
								{
									string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}

							}

							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "CONNECTED PERSON TAB RELATIVE RELATIONSHIP")
							{
								sgrow = new MasterClass().getDataSet("SELECT TID,SELECT PERID,NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat Ac No],TYPE as [TYPE] FROM T_INS_PER_DT_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
								{
									updtrow = new MasterClass().getDataSet("SELECT PERID,NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat Ac No],TYPE as [TYPE] FROM T_INS_PER_DT WHERE ID = '" + sgrow.Tables[0].Rows[0]["TID"].ToString() + "'");

									string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
									output += "Updated Value :- ";
									for (int j = 0; j < columnNames.Length; j++)
									{
										if (j == 0)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
									}
									output += " Previous Value :- ";

									string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames1.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}
								else
								{
									string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}

							}

							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "CONNECTED PERSON TAB FINANCIAL RELATIONSHIP")
							{
								sgrow = new MasterClass().getDataSet("SELECT TID,SELECT PERID,NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat Ac No],TYPE as [TYPE] FROM T_INS_PER_DT_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
								{
									updtrow = new MasterClass().getDataSet("SELECT PERID,NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat Ac No],TYPE as [TYPE] FROM T_INS_PER_DT WHERE ID = '" + sgrow.Tables[0].Rows[0]["TID"].ToString() + "'");

									string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
									output += "Updated Value :- ";
									for (int j = 0; j < columnNames.Length; j++)
									{
										if (j == 0)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
									}
									output += " Previous Value :- ";

									string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames1.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}
								else
								{
									string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}

							}

							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "INSIDER PROFILE TAB")
							{
								sgrow = new MasterClass().getDataSet("SELECT TID,RECEPIENTID AS [Recipient Id],NAMEINSIDER AS [Name of the Insider],CATEGORYRECEIPT AS [Category of Receipt], ADDRESS AS [Address], PANNO AS [PAN], AADHARNO AS [Aadhar No],MOBILENO AS [Mobile No],LANDLINENO AS[Landline No], EMAILID AS [Email Id], PANNOAFFILIATES AS [PAN No.of Affiliates] FROM T_INS_PRO_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
								{
									updtrow = new MasterClass().getDataSet("SELECT RECEPIENTID AS [Recipient Id],NAMEINSIDER AS [Name of the Insider],CATEGORYRECEIPT AS [Category of Receipt], ADDRESS AS [Address], PANNO AS [PAN], AADHARNO AS [Aadhar No],MOBILENO AS [Mobile No],LANDLINENO AS[Landline No], EMAILID AS [Email Id], PANNOAFFILIATES AS [PAN No.of Affiliates] FROM T_INS_PRO WHERE ID = '" + sgrow.Tables[0].Rows[0]["TID"].ToString() + "'");

									string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
									output += "Updated Value :- ";
									for (int j = 0; j < columnNames.Length; j++)
									{
										if (j == 0)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
									}
									output += " Previous Value :- ";

									string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames1.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}
								else
								{
									string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}


							}

							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "SHARING OF UPSI PROFILE TAB")
							{
								sgrow = new MasterClass().getDataSet("SELECT TID,UPSIID AS [UPSI ID],RECIPIENTNAME AS [Recipient Name],RECIPIENTCAT AS [Category of Recipient],PANNO AS [PAN], ADDRESS AS [Address],UPSINATURE AS [Nature of UPSI], SHARINGPURPOSE AS [Purpose of Sharing], SHARINGDATE AS [Date of Sharing],EFFECTIVEUPTO AS [Effective Upto],REMARKS AS [Remarks], NDASIGNED AS [Whether NDA has been signed and Notice of confidentiality has been given ?],UPSIAVAILABLE AS [Date when UPSI became publicly available] FROM T_INS_UPSI_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
								{
									updtrow = new MasterClass().getDataSet("SELECT UPSIID AS [UPSI ID],RECIPIENTNAME AS [Recipient Name],RECIPIENTCAT AS [Category of Recipient],PANNO AS [PAN], ADDRESS AS [Address],UPSINATURE AS [Nature of UPSI], SHARINGPURPOSE AS [Purpose of Sharing], SHARINGDATE AS [Date of Sharing],EFFECTIVEUPTO AS [Effective Upto],REMARKS AS [Remarks], NDASIGNED AS [Whether NDA has been signed and Notice of confidentiality has been given ?],UPSIAVAILABLE AS [Date when UPSI became publicly available] FROM T_INS_UPSI WHERE ID = '" + sgrow.Tables[0].Rows[0]["TID"].ToString() + "'");

									string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
									output += "Updated Value :- ";
									for (int j = 0; j < columnNames.Length; j++)
									{
										if (j == 0)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
									}
									output += " Previous Value :- ";

									string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames1.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}
								else
								{
									string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}

							}

							if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()) == "MASTER DATA OF COMPANY PROFILE TAB")
							{
								sgrow = new MasterClass().getDataSet("SELECT TID,COMPANYNAME AS [Company Name],REGOFFICE AS [Registered Office],CORPORATEOFFICE AS [Corporate Office],MOBILENO AS [Mobile No],LANDLINENO AS [Landline No],EMAILID AS [Email Id],CIN AS [CIN],BSECODE AS [BSE SCRIP CODE],NSECODE AS [NSE SCIRP CODE],ISIN AS [ISIN],OFFICERNAME AS [Compliance Officer Name],DESIGNATION AS [Desgination] FROM T_INS_COMPANY_LOG WHERE ID = '" + ds.Tables[0].Rows[i]["TID"].ToString() + "'");
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
								{
									updtrow = new MasterClass().getDataSet("SELECT COMPANYNAME AS [Company Name],REGOFFICE AS [Registered Office],CORPORATEOFFICE AS [Corporate Office],MOBILENO AS [Mobile No],LANDLINENO AS [Landline No],EMAILID AS [Email Id],CIN AS [CIN],BSECODE AS [BSE SCRIP CODE],NSECODE AS [NSE SCIRP CODE],ISIN AS [ISIN],OFFICERNAME AS [Compliance Officer Name],DESIGNATION AS [Desgination] FROM T_INS_COMPANY WHERE ID = '" + sgrow.Tables[0].Rows[0]["TID"].ToString() + "'");

									string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
									output += "Updated Value :- ";
									for (int j = 0; j < columnNames.Length; j++)
									{
										if (j == 0)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
										}
									}
									output += " Previous Value :- ";

									string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames1.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
									}
								}
								else
								{
									string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

									for (int j = 1; j <= columnNames.Length - 1; j++)
									{
										if (j == 1)
										{
											output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
										else
										{
											output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
										}
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

				});

				SetLoading(false);
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Please Check Your Internet Connection.", "Audit Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
						lg.CURRVALUE = "AUDIT LOG TAB";
						lg.DESCRIPTION = "DOWNLOADED EXCEL FILE";
						lg.TYPE = "SELECTED";
						lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
						lg.ID = SESSIONKEYS.UserID.ToString();
						string json = new MasterClass().SAVE_LOG(lg);

						SaveFileDialog sfd = new SaveFileDialog
						{
							Filter = "Excel Documents (*.xls)|*.xls",
							FileName = "AUDIT LOG.xls"
						};

						if (sfd.ShowDialog() == DialogResult.OK)
						{
							new MasterClass().ToCsV(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
							MessageBox.Show("Exported Data Successfully in Excel Sheet.", "AUDIT LOG", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
				});

				SetLoading(false);
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "Audit Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
						lg.CURRVALUE = "AUDIT LOG TAB";
						lg.DESCRIPTION = "DOWNLOADED PDF FILE";
						lg.TYPE = "SELECTED";
						lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
						lg.ID = SESSIONKEYS.UserID.ToString();
						string json = new MasterClass().SAVE_LOG(lg);


						SaveFileDialog sfd = new SaveFileDialog
						{
							Filter = "PDF (*.pdf)|*.pdf",
							FileName = "AUDIT LOG.pdf"
						};
						if (sfd.ShowDialog() == DialogResult.OK)
						{
							new MasterClass().ToPDF(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
							MessageBox.Show("Exported Data Successfully in PDF Sheet.", "AUDIT LOG", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
				DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "Audit Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
