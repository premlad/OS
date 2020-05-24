using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace OS
{
	public partial class CREATELOGIN : MASTERFORM
	{
		private AUDITLOG lg = new AUDITLOG();
		private string val = "";
		public CREATELOGIN()
		{
			InitializeComponent();
		}

		private void CREATELOGIN_Load(object sender, EventArgs e)
		{
			Login l = new Login();
			try
			{
				if (SESSIONKEYS.UserID.ToString() == "")
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
			FIllData();
			txtUPSIDateofsharing.CustomFormat = " ";
			txtUPSIEffctiveUpto.CustomFormat = " ";
		}

		public void FIllData()
		{
			try
			{
				DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_LOGIN WHERE ACTIVE = 'Y' AND ADMIN = 'N'");
				if (ds.Tables[0].Rows.Count > 0)
				{
					txtFullname.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["FULLNAME"].ToString());
					//txtPassword.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["MOBILENO"].ToString());
					txtUsername.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString().Trim();
					txtMobileNo.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["MOBILENO"].ToString());
					btnaddINSCON.Text = "UPDATE";
					txtFullname.Enabled = false;
					//txtPassword.Enabled = false;
					txtUsername.Enabled = false;
					txtMobileNo.Enabled = false;
					val = "UPDATE";
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			HOMEPAGE h = new HOMEPAGE();
			h.Show();
			Close();
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

		private void btnaddINSCON_Click(object sender, EventArgs e)
		{
			try
			{
				SetLoading(true);

				//Thread.Sleep(2000);
				Invoke((MethodInvoker)delegate
				{
					//if (new MasterClass().GETLOCKDB() == "Y")
					//{
					//	DialogResult dialog = MessageBox.Show("Database is Locked.", "Locked Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
					//}
					//else
					int value = DateTime.Compare(txtUPSIDateofsharing.Value, txtUPSIEffctiveUpto.Value);

					if (MasterClass.GETISTI() == "TEMP")
					{
						DialogResult dialog = MessageBox.Show("Date & Time is Tempered.\nPlease Check your Date & Time Settings.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
						Login l = new Login();
						lg.CURRVALUE = "LOG OUT";
						lg.DESCRIPTION = "FORCE LOGOUT DUE TO DATE MISMATCH";
						lg.TYPE = "SELECTED";
						lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
						lg.ID = SESSIONKEYS.UserID.ToString();
						string json = new MasterClass().SAVE_LOG(lg);
						SESSIONKEYS.UserID = "";
						SESSIONKEYS.Role = "";
						SESSIONKEYS.FullName = "";
						l.Show();
						Close();
					}
					//else if (txtFullname.Text == "")
					//{
					//	DialogResult dialog = MessageBox.Show("Provide Values.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
					//}
					//else if (txtMobileNo.Text == "")
					//{
					//	DialogResult dialog = MessageBox.Show("Provide Values.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
					//}
					else if (txtUsername.Text == "")
					{
						DialogResult dialog = MessageBox.Show("Provide Values.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (txtPassword.Text == "")
					{
						DialogResult dialog = MessageBox.Show("Provide Values.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (txtUPSIDateofsharing.Text == "")
					{
						DialogResult dialog = MessageBox.Show("Provide Values.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (txtUPSIEffctiveUpto.Text == "")
					{
						DialogResult dialog = MessageBox.Show("Provide Values.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (value > 0)
					{
						DialogResult dialog = MessageBox.Show("To Date cant be less than From Date.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						string ds;
						if (val == "UPDATE")
						{
							ds = new MasterClass().executeQueryForDB("UPDATE T_LOGIN SET DATEFROM = '" + txtUPSIDateofsharing.Value.ToString("yyyy-MM-dd 00:00:00") + "',DATETO = '" + txtUPSIEffctiveUpto.Value.ToString("yyyy-MM-dd 00:00:00") + "', PASSWORD = '" + CryptographyHelper.Encrypt(txtPassword.Text) + "' WHERE ADMIN = 'N'").ToString();

							lg.CURRVALUE = "LOGIN CREATION";
							lg.TYPE = "UPDATED";
							lg.ID = ds;
							lg.DESCRIPTION = "LOGIN CREATION";
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							lg.ID = SESSIONKEYS.UserID.ToString();
							new MasterClass().SAVE_LOG(lg);

							if (Convert.ToInt32(ds) > 0)
							{
								DialogResult dialog = MessageBox.Show("Updated Data Successfully.", "Login Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							else
							{
								DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Login Creation", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
						else
						{
							ds = new MasterClass().executeQuery("INSERT INTO T_LOGIN (FULLNAME,MOBILENO,EMAIL,PASSWORD,ENTEREDBY,ENTEREDON,DATEFROM,DATETO,ADMIN ,ACTIVE ,LOCK) VALUES('" + CryptographyHelper.Encrypt(txtFullname.Text) + "','" + CryptographyHelper.Encrypt(txtMobileNo.Text) + "','" + txtUsername.Text + "','" + CryptographyHelper.Encrypt(txtPassword.Text) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + txtUPSIDateofsharing.Value.ToString("yyyy-MM-dd 00:00:00") + "','" + txtUPSIEffctiveUpto.Value.ToString("yyyy-MM-dd 00:00:00") + "','N','Y','N');").ToString();

							lg.CURRVALUE = "LOGIN CREATION";
							lg.TYPE = "INSERTED";
							lg.ID = ds;
							lg.DESCRIPTION = "LOGIN CREATION";
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							lg.ID = SESSIONKEYS.UserID.ToString();
							new MasterClass().SAVE_LOG(lg);

							if (Convert.ToInt32(ds) > 0)
							{
								DialogResult dialog = MessageBox.Show("Save Data Successfully.", "Login Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							else
							{
								DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Login Creation", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
						txtPassword.Text = "";
						FIllData();
					}
				});

				SetLoading(false);
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				SetLoading(false);
				DialogResult dialog = MessageBox.Show("Data Not Saved. Please Check Your Internet Connection.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtUPSIDateofsharing_ValueChanged(object sender, EventArgs e)
		{
			txtUPSIDateofsharing.CustomFormat = "dd-MM-yyyy";
		}

		private void txtUPSIEffctiveUpto_ValueChanged(object sender, EventArgs e)
		{
			txtUPSIEffctiveUpto.CustomFormat = "dd-MM-yyyy";
		}
	}
}
