using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace OS
{
	public partial class MASTER_FOR_RECORDING_INSIDER_PROFILE : MASTERFORM
	{
		private AUDITLOG lg = new AUDITLOG();
		private string PANO = "";
		public MASTER_FOR_RECORDING_INSIDER_PROFILE()
		{
			InitializeComponent();
		}

		private void MASTER_FOR_RECORDING_INSIDER_PROFILE_Load(object sender, EventArgs e)
		{
			Login l = new Login();
			//TopMost = true;
			WindowState = FormWindowState.Maximized;
			try
			{
				if (SESSIONKEYS.UserID.ToString() == "")
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
			FillConnectPersonID();
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

		#region MASTER FOR RECORDING INSIDER PROFILE

		public void FillConnectPersonID()
		{
			try
			{


				cmdINSCONSAVEID.Items.Clear();
				DataSet ds = new MasterClass().getDataSet("SELECT ID,RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'");
				AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
				if (ds.Tables[0].Rows.Count > 0)
				{
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						ComboboxItem item = new ComboboxItem
						{
							NAME = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString()),
							ID = ds.Tables[0].Rows[i]["ID"].ToString()
						};
						cmdINSCONSAVEID.Items.Add(item);
						cmdINSCONSAVEID.DisplayMember = "NAME";
						cmdINSCONSAVEID.ValueMember = "ID";
						MyCollection.Add(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString()));
					}

				}
				txtINSPROreceipeitnID.AutoCompleteCustomSource = MyCollection;
			}
			catch (Exception)
			{

			}
		}

		private void cmbINSPROcategoryofreceipt_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar < 32 || e.KeyChar > 126)
			{
				return;
			}
			string t = cmbINSPROcategoryofreceipt.Text;
			string typedT = t.Substring(0, cmbINSPROcategoryofreceipt.SelectionStart);
			string newT = typedT + e.KeyChar;

			int i = cmbINSPROcategoryofreceipt.FindString(newT);
			if (i == -1)
			{
				e.Handled = true;
			}
		}

		private void cmbINSPROcategoryofreceipt_Leave(object sender, EventArgs e)
		{
			string t = cmbINSPROcategoryofreceipt.Text;

			if (cmbINSPROcategoryofreceipt.SelectedItem == null)
			{
				cmbINSPROcategoryofreceipt.Text = "";
			}
		}

		public class ComboboxItem
		{
			public string NAME { get; set; }
			public object ID { get; set; }

			public override string ToString()
			{
				return NAME;
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
					Thread.Sleep(4000);
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

		private void btnaddINSCON_Click(object sender, EventArgs e)
		{
			try
			{
				SetLoading(true);

				Thread.Sleep(2000);
				Invoke((MethodInvoker)delegate
				{
					if (new MasterClass().GETLOCKDB() == "Y")
					{
						DialogResult dialog = MessageBox.Show("Database is Locked.", "Locked Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					if (MasterClass.GETISTI() == "TEMP")
					{
						DialogResult dialog = MessageBox.Show("Date & Time is Tempered.\nPlease Check your Date & Time Settings.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
						Login l = new Login();
						lg.CURRVALUE = "LOG OUT";
						lg.DESCRIPTION = "LOG OUT SUCCESSFULLY WITH DATA TAMPERING";
						lg.TYPE = "SELECTED";
						lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
						lg.ID = SESSIONKEYS.UserID.ToString();
						string json = new MasterClass().SAVE_LOG(lg);
						SESSIONKEYS.UserID = "";
						SESSIONKEYS.Role = "";
						SESSIONKEYS.FullName = "";
						l.Show();
						Hide();
					}
					//else if (txtINSPROreceipeitnID.Text == "")
					//{
					//	DialogResult dialog = MessageBox.Show("Enter Recepient ID.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
					//}
					else if (txtINSPROnameofinsider.Text == "")
					{
						DialogResult dialog = MessageBox.Show("Enter Name of Insider.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (txtINSPROpannomaster.Text == "")
					{
						DialogResult dialog = MessageBox.Show("Enter Pan No.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (txtMobileINSPRONumber.Text == "")
					{
						DialogResult dialog = MessageBox.Show("Enter Mobile No.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (!new MasterClass().IsValidPanno(txtINSPROpannomaster.Text))
					{
						DialogResult dialog = MessageBox.Show("Please Enter PAN No in Proper Format.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (!new MasterClass().IsValidEmail(txtEmailINSPRONumber.Text))
					{
						DialogResult dialog = MessageBox.Show("Please Enter Email in Proper Format.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						DataSet ds1 = new MasterClass().getDataSet("select PANNO from T_INS_PER WHERE ACTIVE = 'Y'");
						DataSet ds2 = new MasterClass().getDataSet("select PANNO from T_INS_PRO WHERE ACTIVE = 'Y'");
						List<string> termsList = new List<string>();
						string[] b = { };
						if (ds1.Tables[0].Rows.Count > 0)
						{
							for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
							{
								string a = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["PANNO"].ToString());
								termsList.Add(a);
							}
							b = termsList.ToArray();
						}
						if (ds2.Tables[0].Rows.Count > 0)
						{
							for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
							{
								string a = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["PANNO"].ToString());
								termsList.Add(a);
							}
							b = termsList.ToArray();
						}
						if (b.Contains(txtINSPROpannomaster.Text))
						{
							DialogResult dialog = MessageBox.Show("Pan No Already Exists in our Database.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							T_INS_PRO PRO = new T_INS_PRO
							{
								NAME_OF_INSIDER = txtINSPROnameofinsider.Text,
								RECEPIENT_ID = txtINSPROreceipeitnID.Text,
								ADDRESS = txtINSPROaddressmaster.Text,
								PAN_NO = txtINSPROpannomaster.Text,
								AADHAR_NO = txtINSPROaadhar.Text,
								PAN_NO_OF_AFFILAIATES = txtPANNOINSPRONumber.Text,
								MOBILE_NO = txtMobileINSPRONumber.Text,
								LANDLINE_NO = txtlandlineINSPRONumber.Text,
								EMAIL_ID = txtEmailINSPRONumber.Text
							};
							if (cmbINSPROcategoryofreceipt.Text == "OTHERS")
							{
								PRO.CATEGORY_OF_RECEIPT = "OTHERS|" + cmdINSPROcategoryothers.Text;
							}
							else
							{
								PRO.CATEGORY_OF_RECEIPT = cmbINSPROcategoryofreceipt.Text;
							}
							PRO.ID = "";
							PRO.ENTEREDBY = SESSIONKEYS.UserID.ToString();

							string CPID = "IP" + new MasterClass().GETIPID();
							string ds = new MasterClass().executeQuery("INSERT INTO T_INS_PRO (RECEPIENTID,NAMEINSIDER,CATEGORYRECEIPT,ADDRESS,PANNO,AADHARNO,MOBILENO,LANDLINENO,EMAILID,PANNOAFFILIATES,ENTEREDBY,ENTEREDON,ACTIVE,LOCK) VALUES ('" + CryptographyHelper.Encrypt(CPID) + "','" + CryptographyHelper.Encrypt(PRO.NAME_OF_INSIDER) + "','" + CryptographyHelper.Encrypt(PRO.CATEGORY_OF_RECEIPT) + "','" + CryptographyHelper.Encrypt(PRO.ADDRESS) + "','" + CryptographyHelper.Encrypt(PRO.PAN_NO) + "','" + CryptographyHelper.Encrypt(PRO.AADHAR_NO) + "','" + CryptographyHelper.Encrypt(PRO.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(PRO.LANDLINE_NO) + "','" + CryptographyHelper.Encrypt(PRO.EMAIL_ID) + "','" + CryptographyHelper.Encrypt(PRO.PAN_NO_OF_AFFILAIATES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','Y','N') ;").ToString();
							string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PRO_LOG(TID,RECEPIENTID,NAMEINSIDER,CATEGORYRECEIPT,ADDRESS,PANNO,AADHARNO,MOBILENO,LANDLINENO,EMAILID,PANNOAFFILIATES,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ds + "','" + CryptographyHelper.Encrypt(CPID) + "','" + CryptographyHelper.Encrypt(PRO.NAME_OF_INSIDER) + "','" + CryptographyHelper.Encrypt(PRO.CATEGORY_OF_RECEIPT) + "','" + CryptographyHelper.Encrypt(PRO.ADDRESS) + "','" + CryptographyHelper.Encrypt(PRO.PAN_NO) + "','" + CryptographyHelper.Encrypt(PRO.AADHAR_NO) + "','" + CryptographyHelper.Encrypt(PRO.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(PRO.LANDLINE_NO) + "','" + CryptographyHelper.Encrypt(PRO.EMAIL_ID) + "','" + CryptographyHelper.Encrypt(PRO.PAN_NO_OF_AFFILAIATES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N') ;").ToString();

							lg.CURRVALUE = "INSIDER PROFILE TAB";
							lg.TYPE = "INSERTED";
							lg.ID = perlogid;
							lg.DESCRIPTION = "INSERTED VALUE :- " + CPID;
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							lg.ID = SESSIONKEYS.UserID.ToString();
							new MasterClass().SAVE_LOG(lg);
							if (Convert.ToInt32(ds) > 0)
							{
								DialogResult dialog = MessageBox.Show("Data Saved Successfully.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							else
							{
								DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							Clear();
							FillConnectPersonID();
						}
					}

				});

				SetLoading(false);
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Data Not Saved. Please Check Your Internet Connection.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtMobileINSPRONumber_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
		(e.KeyChar != '.'))
			{
				e.Handled = true;
			}
		}

		private void cmbINSPROcategoryofreceipt_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbINSPROcategoryofreceipt.Text == "OTHERS")
			{
				label12.Visible = true;
				cmdINSPROcategoryothers.Visible = true;
			}
			else
			{
				label12.Visible = false;
				cmdINSPROcategoryothers.Visible = false;
			}
		}

		private void txtINSPROreceipeitnID_Leave(object sender, EventArgs e)
		{
			try
			{
				cmbINSPROcategoryofreceipt.Text = "";
				if (txtINSPROreceipeitnID.Text == "")
				{
					Clear();
					txtINSPROreceipeitnID.Enabled = true;
					btnupdateINSCON.Visible = false;
					btnaddINSCONdeelete.Visible = false;
					btncacncelINSCON.Visible = false;
					btnaddINSCON.Visible = true;
				}
				else
				{
					for (int i = 0; i < cmdINSCONSAVEID.Items.Count; i++)
					{
						if (txtINSPROreceipeitnID.Text == cmdINSCONSAVEID.GetItemText(cmdINSCONSAVEID.Items[i]))
						{
							cmdINSCONSAVEID.SelectedIndex = i;

							DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID + "'");
							if (ds.Tables[0].Rows.Count > 0)
							{
								txtINSPROnameofinsider.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["NAMEINSIDER"].ToString());
								txtINSPROreceipeitnID.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RECEPIENTID"].ToString());
								txtINSPROaddressmaster.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["ADDRESS"].ToString());
								txtINSPROpannomaster.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PANNO"].ToString());
								PANO = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PANNO"].ToString());
								txtINSPROaadhar.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["AADHARNO"].ToString());
								txtPANNOINSPRONumber.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PANNOAFFILIATES"].ToString());
								txtMobileINSPRONumber.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["MOBILENO"].ToString());
								txtlandlineINSPRONumber.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["LANDLINENO"].ToString());
								txtEmailINSPRONumber.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["EMAILID"].ToString());
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString()).Contains("OTHERS"))
								{
									string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString()).Split('|');
									cmbINSPROcategoryofreceipt.SelectedText = abc[0];
									cmdINSPROcategoryothers.Text = abc[1];
									label12.Visible = true;
									cmdINSPROcategoryothers.Visible = true;
								}
								else
								{
									string a = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString());
									cmbINSPROcategoryofreceipt.SelectedText = a;
								}
								//txtINSPROreceipeitnID.ReadOnly = true;
								btnupdateINSCON.Visible = true;
								btnaddINSCONdeelete.Visible = true;
								btncacncelINSCON.Visible = true;
								btnaddINSCON.Visible = false;
							}
							else
							{
								Clear();
								txtINSPROreceipeitnID.Enabled = true;
								btnupdateINSCON.Visible = false;
								btnaddINSCONdeelete.Visible = false;
								btncacncelINSCON.Visible = false;
								btnaddINSCON.Visible = true;
							}
						}
					}
				}


			}
			catch (Exception)
			{
				throw;
			}
		}

		public void Clear()
		{
			txtINSPROnameofinsider.Text = "";
			txtINSPROreceipeitnID.Text = "";
			txtINSPROaddressmaster.Text = "";
			txtINSPROpannomaster.Text = "";
			txtINSPROaadhar.Text = "";
			txtPANNOINSPRONumber.Text = "";
			txtMobileINSPRONumber.Text = "";
			txtlandlineINSPRONumber.Text = "";
			txtEmailINSPRONumber.Text = "";
			cmbINSPROcategoryofreceipt.Text = "";
		}

		private void btncacncelINSCON_Click(object sender, EventArgs e)
		{
			Clear();
			FillConnectPersonID();
			txtINSPROreceipeitnID.Enabled = true;
			btnupdateINSCON.Visible = false;
			btnaddINSCONdeelete.Visible = false;
			btncacncelINSCON.Visible = false;
			btnaddINSCON.Visible = true;
		}

		private void btnupdateINSCON_Click(object sender, EventArgs e)
		{
			try
			{
				SetLoading(true);

				Thread.Sleep(2000);
				Invoke((MethodInvoker)delegate
				{
					if (new MasterClass().GETLOCKDB() == "Y")
					{
						DialogResult dialog = MessageBox.Show("Database is Locked.", "Locked Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					if (MasterClass.GETISTI() == "TEMP")
					{
						DialogResult dialog = MessageBox.Show("Date & Time is Tempered.\nPlease Check your Date & Time Settings.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
						Login l = new Login();
						lg.CURRVALUE = "LOG OUT";
						lg.DESCRIPTION = "LOG OUT SUCCESSFULLY WITH DATA TAMPERING";
						lg.TYPE = "SELECTED";
						lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
						lg.ID = SESSIONKEYS.UserID.ToString();
						string json = new MasterClass().SAVE_LOG(lg);
						SESSIONKEYS.UserID = "";
						SESSIONKEYS.Role = "";
						SESSIONKEYS.FullName = "";
						l.Show();
						Hide();
					}
					else if (txtINSPROreceipeitnID.Text == "")
					{
						DialogResult dialog = MessageBox.Show("Enter Recepient ID.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (txtINSPROnameofinsider.Text == "")
					{
						DialogResult dialog = MessageBox.Show("Enter Name of Insider.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (txtINSPROpannomaster.Text == "")
					{
						DialogResult dialog = MessageBox.Show("Enter Pan No.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (txtMobileINSPRONumber.Text == "")
					{
						DialogResult dialog = MessageBox.Show("Enter Mobile No.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (!new MasterClass().IsValidPanno(txtINSPROpannomaster.Text))
					{
						DialogResult dialog = MessageBox.Show("Please Enter PAN No in Proper Format.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (!new MasterClass().IsValidEmail(txtEmailINSPRONumber.Text))
					{
						DialogResult dialog = MessageBox.Show("Please Enter Email in Proper Format.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						DataSet ds1 = new MasterClass().getDataSet("select PANNO from T_INS_PER WHERE ACTIVE = 'Y'");
						DataSet ds2 = new MasterClass().getDataSet("select PANNO from T_INS_PRO WHERE ACTIVE = 'Y'");
						List<string> termsList = new List<string>();
						string[] b = { };
						if (PANO != txtINSPROpannomaster.Text)
						{
							if (ds1.Tables[0].Rows.Count > 0)
							{
								for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
								{
									string a = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["PANNO"].ToString());
									termsList.Add(a);
								}
								b = termsList.ToArray();
							}
							if (ds2.Tables[0].Rows.Count > 0)
							{
								for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
								{
									string a = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["PANNO"].ToString());
									termsList.Add(a);
								}
								b = termsList.ToArray();
							}
						}

						if (b.Contains(txtINSPROpannomaster.Text))
						{
							DialogResult dialog = MessageBox.Show("Pan No Already Exists in our Database.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							T_INS_PRO PRO = new T_INS_PRO
							{
								ID = ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString(),
								NAME_OF_INSIDER = txtINSPROnameofinsider.Text,
								RECEPIENT_ID = txtINSPROreceipeitnID.Text,
								ADDRESS = txtINSPROaddressmaster.Text,
								PAN_NO = txtINSPROpannomaster.Text,
								AADHAR_NO = txtINSPROaadhar.Text,
								PAN_NO_OF_AFFILAIATES = txtPANNOINSPRONumber.Text,
								MOBILE_NO = txtMobileINSPRONumber.Text,
								LANDLINE_NO = txtlandlineINSPRONumber.Text,
								EMAIL_ID = txtEmailINSPRONumber.Text
							};
							if (cmbINSPROcategoryofreceipt.Text == "OTHERS")
							{
								PRO.CATEGORY_OF_RECEIPT = "OTHERS|" + cmdINSPROcategoryothers.Text;
							}
							else
							{
								PRO.CATEGORY_OF_RECEIPT = cmbINSPROcategoryofreceipt.Text;
							}
							PRO.ID = "";
							PRO.ENTEREDBY = SESSIONKEYS.UserID.ToString();

							string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_PRO SET RECEPIENTID = '" + CryptographyHelper.Encrypt(PRO.RECEPIENT_ID) + "',NAMEINSIDER = '" + CryptographyHelper.Encrypt(PRO.NAME_OF_INSIDER) + "',CATEGORYRECEIPT = '" + CryptographyHelper.Encrypt(PRO.CATEGORY_OF_RECEIPT) + "',ADDRESS = '" + CryptographyHelper.Encrypt(PRO.ADDRESS) + "',PANNO = '" + CryptographyHelper.Encrypt(PRO.PAN_NO) + "',AADHARNO = '" + CryptographyHelper.Encrypt(PRO.AADHAR_NO) + "',MOBILENO = '" + CryptographyHelper.Encrypt(PRO.MOBILE_NO) + "',LANDLINENO = '" + CryptographyHelper.Encrypt(PRO.LANDLINE_NO) + "',EMAILID = '" + CryptographyHelper.Encrypt(PRO.EMAIL_ID) + "',PANNOAFFILIATES = '" + CryptographyHelper.Encrypt(PRO.PAN_NO_OF_AFFILAIATES) + "',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON ='" + MasterClass.GETIST() + "' WHERE ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "' ; ").ToString();

							string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PRO_LOG(TID,RECEPIENTID,NAMEINSIDER,CATEGORYRECEIPT,ADDRESS,PANNO,AADHARNO,MOBILENO,LANDLINENO,EMAILID,PANNOAFFILIATES,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + CryptographyHelper.Encrypt(PRO.RECEPIENT_ID) + "','" + CryptographyHelper.Encrypt(PRO.NAME_OF_INSIDER) + "','" + CryptographyHelper.Encrypt(PRO.CATEGORY_OF_RECEIPT) + "','" + CryptographyHelper.Encrypt(PRO.ADDRESS) + "','" + CryptographyHelper.Encrypt(PRO.PAN_NO) + "','" + CryptographyHelper.Encrypt(PRO.AADHAR_NO) + "','" + CryptographyHelper.Encrypt(PRO.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(PRO.LANDLINE_NO) + "','" + CryptographyHelper.Encrypt(PRO.EMAIL_ID) + "','" + CryptographyHelper.Encrypt(PRO.PAN_NO_OF_AFFILAIATES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N') ;").ToString();

							lg.CURRVALUE = "INSIDER PROFILE TAB";
							lg.TYPE = "UPDATED";
							lg.ID = perlogid;
							lg.DESCRIPTION = "UPDATED VALUE :- " + PRO.RECEPIENT_ID;
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							lg.ID = SESSIONKEYS.UserID.ToString();
							new MasterClass().SAVE_LOG(lg);

							if (Convert.ToInt32(ds) > 0)
							{
								DialogResult dialog = MessageBox.Show("Updated Successfully.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
								Clear();
								FillConnectPersonID();
								txtINSPROreceipeitnID.ReadOnly = false;
								btnupdateINSCON.Visible = false;
								btnaddINSCONdeelete.Visible = false;
								btncacncelINSCON.Visible = false;
								btnaddINSCON.Visible = true;
								txtINSPROreceipeitnID.Enabled = true;
							}
							else
							{
								DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
						}
					}

				});

				SetLoading(false);
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Data Not Updated. Please Check Your Internet Connection.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnaddINSCONdeelete_Click(object sender, EventArgs e)
		{
			try
			{
				SetLoading(true);

				Thread.Sleep(2000);
				Invoke((MethodInvoker)delegate
				{
					if (new MasterClass().GETLOCKDB() == "Y")
					{
						DialogResult dialog = MessageBox.Show("Database is Locked.", "Locked Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					if (MasterClass.GETISTI() == "TEMP")
					{
						DialogResult dialog = MessageBox.Show("Date & Time is Tempered.\nPlease Check your Date & Time Settings.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
						Login l = new Login();
						lg.CURRVALUE = "LOG OUT";
						lg.DESCRIPTION = "LOG OUT SUCCESSFULLY WITH DATA TAMPERING";
						lg.TYPE = "SELECTED";
						lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
						lg.ID = SESSIONKEYS.UserID.ToString();
						string json = new MasterClass().SAVE_LOG(lg);
						SESSIONKEYS.UserID = "";
						SESSIONKEYS.Role = "";
						SESSIONKEYS.FullName = "";
						l.Show();
						Hide();
					}
					else
					{
						T_INS_PRO PRO = new T_INS_PRO
						{
							ID = ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString(),
							NAME_OF_INSIDER = txtINSPROnameofinsider.Text,
							RECEPIENT_ID = txtINSPROreceipeitnID.Text,
							ADDRESS = txtINSPROaddressmaster.Text,
							PAN_NO = txtINSPROpannomaster.Text,
							AADHAR_NO = txtINSPROaadhar.Text,
							PAN_NO_OF_AFFILAIATES = txtPANNOINSPRONumber.Text,
							MOBILE_NO = txtMobileINSPRONumber.Text,
							LANDLINE_NO = txtlandlineINSPRONumber.Text,
							EMAIL_ID = txtEmailINSPRONumber.Text
						};
						if (cmbINSPROcategoryofreceipt.Text == "OTHERS")
						{
							PRO.CATEGORY_OF_RECEIPT = "OTHERS|" + cmdINSPROcategoryothers.Text;
						}
						else
						{
							PRO.CATEGORY_OF_RECEIPT = cmbINSPROcategoryofreceipt.Text;
						}
						PRO.ID = "";
						PRO.ENTEREDBY = SESSIONKEYS.UserID.ToString();

						DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Insider Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (dialogResult == DialogResult.Yes)
						{

							string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_PRO  SET ACTIVE = 'N',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + MasterClass.GETIST() + "' WHERE ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "' ; ").ToString();

							string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PRO_LOG(TID,RECEPIENTID,NAMEINSIDER,CATEGORYRECEIPT,ADDRESS,PANNO,AADHARNO,MOBILENO,LANDLINENO,EMAILID,PANNOAFFILIATES,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + CryptographyHelper.Encrypt(PRO.RECEPIENT_ID) + "','" + CryptographyHelper.Encrypt(PRO.NAME_OF_INSIDER) + "','" + CryptographyHelper.Encrypt(PRO.CATEGORY_OF_RECEIPT) + "','" + CryptographyHelper.Encrypt(PRO.ADDRESS) + "','" + CryptographyHelper.Encrypt(PRO.PAN_NO) + "','" + CryptographyHelper.Encrypt(PRO.AADHAR_NO) + "','" + CryptographyHelper.Encrypt(PRO.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(PRO.LANDLINE_NO) + "','" + CryptographyHelper.Encrypt(PRO.EMAIL_ID) + "','" + CryptographyHelper.Encrypt(PRO.PAN_NO_OF_AFFILAIATES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("DELETED") + "','Y','N') ;").ToString();

							lg.CURRVALUE = "INSIDER PROFILE TAB";
							lg.TYPE = "DELETED";
							lg.ID = perlogid;
							lg.DESCRIPTION = "DELETED VALUE :- " + PRO.RECEPIENT_ID;
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							lg.ID = SESSIONKEYS.UserID.ToString();
							new MasterClass().SAVE_LOG(lg);
							if (Convert.ToInt32(ds) > 0)
							{
								DialogResult dialog = MessageBox.Show("Deleted Successfully.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
								Clear();
								FillConnectPersonID();
								txtINSPROreceipeitnID.Enabled = true;
								btnupdateINSCON.Visible = false;
								btnaddINSCONdeelete.Visible = false;
								btncacncelINSCON.Visible = false;
								btnaddINSCON.Visible = true;
							}
							else
							{
								DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
						}
					}
				});

				SetLoading(false);
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Data Not Deleted. Please Check Your Internet Connection.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{
			HOMEPAGE h = new HOMEPAGE();
			h.Show();
			Hide();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Clear();
			FillConnectPersonID();
			txtINSPROreceipeitnID.Enabled = true;
			btnupdateINSCON.Visible = true;
			btnaddINSCONdeelete.Visible = true;
			btncacncelINSCON.Visible = true;
			btnaddINSCON.Visible = false;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Clear();
			FillConnectPersonID();
			txtINSPROreceipeitnID.Enabled = false;
			btnupdateINSCON.Visible = false;
			btnaddINSCONdeelete.Visible = false;
			btncacncelINSCON.Visible = false;
			btnaddINSCON.Visible = true;
		}

		#endregion

	}
}
