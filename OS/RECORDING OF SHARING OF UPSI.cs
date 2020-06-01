using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace OS
{
	public partial class RECORDING_OF_SHARING_OF_UPSI : MASTERFORM
	{
		private AUDITLOG lg = new AUDITLOG();
		public RECORDING_OF_SHARING_OF_UPSI()
		{
			InitializeComponent();
		}

		private void RECORDING_OF_SHARING_OF_UPSI_Load(object sender, EventArgs e)
		{

			Login l = new Login();
			//TopMost = true;
			//WindowState = FormWindowState.Maximized;
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

		#region RECORDING OF SHARING OF UPSI

		public void FillConnectPersonID()
		{
			try
			{
				//txtUPSIDateofsharing.CustomFormat = "dd-MM-yyyy";
				//txtUPSIEffctiveUpto.CustomFormat = "dd-MM-yyyy";
				//txtUPSIUPSIavaailabe.CustomFormat = "dd-MM-yyyy";
				txtUPSIDateofsharing.CustomFormat = " ";
				txtUPSIEffctiveUpto.CustomFormat = " ";
				txtUPSIUPSIavaailabe.CustomFormat = " ";
				DataSet ds = new MasterClass().getDataSet("SELECT ID,UPSIID FROM T_INS_UPSI WHERE ACTIVE = 'Y' AND LOCK = 'N'");
				AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
				if (ds.Tables[0].Rows.Count > 0)
				{
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						ComboboxItem item = new ComboboxItem
						{
							NAME = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIID"].ToString()),
							ID = ds.Tables[0].Rows[i]["ID"].ToString()
						};
						cmdINSCONSAVEID.Items.Add(item);
						cmdINSCONSAVEID.DisplayMember = "NAME";
						cmdINSCONSAVEID.ValueMember = "ID";
						MyCollection.Add(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIID"].ToString()));
					}
				}
				txtUPSIID.AutoCompleteCustomSource = MyCollection;
				txtUPSINAME.Items.Clear();
				DataSet ds1 = new MasterClass().getDataSet("SELECT ID,NAMEINSIDER,RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'");
				DataSet ds2 = new MasterClass().getDataSet("SELECT ID,EMPNAME,CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'");

				for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
				{
					ComboboxItem1 item = new ComboboxItem1
					{
						NAMEINSIDER = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["NAMEINSIDER"].ToString()) + " - " + CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["RECEPIENTID"].ToString()),
						UPSIID = ds1.Tables[0].Rows[i]["ID"].ToString()
					};
					txtUPSINAME.Items.Add(item);
					txtUPSINAME.DisplayMember = "NAMEINSIDER";
					txtUPSINAME.ValueMember = "UPSIID";
				}

				for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
				{
					ComboboxItem1 item = new ComboboxItem1
					{
						NAMEINSIDER = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["EMPNAME"].ToString()) + " - " + CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["CONNECTPERSONID"].ToString()),
						UPSIID = ds2.Tables[0].Rows[i]["ID"].ToString()
					};
					txtUPSINAME.Items.Add(item);
					txtUPSINAME.DisplayMember = "NAMEINSIDER";
					txtUPSINAME.ValueMember = "UPSIID";
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public class ComboboxItem1
		{
			public string NAMEINSIDER { get; set; }
			public object UPSIID { get; set; }

			public override string ToString()
			{
				return NAMEINSIDER;
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
			try
			{
				if (displayLoader)
				{
					if (Cursors.WaitCursor != Cursor)
					{
						//Invoke((MethodInvoker)delegate
						//{
						//picLoader.Visible = true;
						Cursor = Cursors.WaitCursor;
						//Thread.Sleep(4000);
						//});
					}
				}
				else
				{
					if (Cursors.Default != Cursor)
					{
						//Invoke((MethodInvoker)delegate
						//{
						//picLoader.Visible = false;
						Cursor = Cursors.Default;
						//});
					}

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

				Thread.Sleep(2000);
				//Invoke((MethodInvoker)delegate
				//{
				int val = 0;
				int val2 = 0;
				if (txtUPSIEffctiveUpto.Text.ToString().Trim() != "")
				{
					val = DateTime.Compare(txtUPSIDateofsharing.Value, txtUPSIEffctiveUpto.Value);
				}
				if (txtUPSIUPSIavaailabe.Text.ToString().Trim() != "")
				{
					val2 = DateTime.Compare(txtUPSIDateofsharing.Value, txtUPSIUPSIavaailabe.Value);
				}
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
					lg.DESCRIPTION = "FORCE LOGOUT DUE TO DATE MISMATCH";
					lg.TYPE = "SELECTED";
					lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
					//lg.ID = SESSIONKEYS.UserID.ToString();
					string json = new MasterClass().SAVE_LOG(lg);
					SESSIONKEYS.UserID = "";
					SESSIONKEYS.Role = "";
					SESSIONKEYS.FullName = "";
					l.Show();
					Close();
				}
				//if (txtUPSIID.Text == "")
				//{
				//	DialogResult dialog = MessageBox.Show("Enter UPSI ID.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//}
				else if (txtUPSINAME.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Recepeint Name.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (txtUPSIAdress.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Address.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (txtUPSIcategory.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Category.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (txtUPSIPanno.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter PAN No. or any other Identifier ID.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (txtUPSINatureUPSI.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Nature of UPSI.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (txtUPSIpurposesharing.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Purpose of Sharing.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (txtUPSIDateofsharing.Text.Trim() == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Date of Sharing.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (radioButtonNDAYES.Checked == false && radioButtonNDANo.Checked == false)
				{
					DialogResult dialog = MessageBox.Show("Enter NDA has been signed or Notice of confidentiality has been given.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (val > 0)
				{
					DialogResult dialog = MessageBox.Show("Date of Sharing can't be more than Effective Date.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (val2 > 0)
				{
					DialogResult dialog = MessageBox.Show("Date of Sharing can't be more than UPSI available Date.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					T_INS_UPSI PRO = new T_INS_UPSI
					{
						UPSIID = txtUPSIID.Text,
						RECIPIENTNAME = txtUPSINAME.Text,
						PANNO = txtUPSIPanno.Text,
						ADDRESS = txtUPSIAdress.Text,
						UPSINATURE = txtUPSINatureUPSI.Text,
						SHARINGPURPOSE = txtUPSIpurposesharing.Text,
						SHARINGDATE = txtUPSIDateofsharing.Text,
						EFFECTIVEUPTO = txtUPSIEffctiveUpto.Text,
						REMARKS = txtUPSIremarks.Text,
						UPSIAVAILABLE = txtUPSIUPSIavaailabe.Text,
					};
					if (radioButtonNDAYES.Checked == true)
					{
						PRO.NDASIGNED = "YES|" + lblnNDS.Text;
					}
					else if (radioButtonNDANo.Checked == true)
					{
						PRO.NDASIGNED = "NO|" + lblnNDS.Text;
					}
					else
					{
						PRO.NDASIGNED = "|" + lblnNDS.Text;
					}

					if (txtUPSIcategory.Text == "OTHERS")
					{
						PRO.RECIPIENTCAT = "OTHERS|" + txtUPSIOthercategory.Text;
					}
					else
					{
						PRO.RECIPIENTCAT = txtUPSIcategory.Text;
					}
					PRO.ID = "";
					PRO.ENTEREDBY = SESSIONKEYS.UserID.ToString();

					string CPID = "UPSI" + new MasterClass().GETUPSIID();
					string ds = new MasterClass().executeQuery("INSERT INTO T_INS_UPSI(UPSIID,RECIPIENTNAME,RECIPIENTCAT,PANNO,ADDRESS,UPSINATURE,SHARINGPURPOSE,SHARINGDATE,EFFECTIVEUPTO,REMARKS,NDASIGNED,UPSIAVAILABLE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK) VALUES ('" + CryptographyHelper.Encrypt(CPID) + "','" + CryptographyHelper.Encrypt(PRO.RECIPIENTNAME) + "','" + CryptographyHelper.Encrypt(PRO.RECIPIENTCAT) + "','" + CryptographyHelper.Encrypt(PRO.PANNO) + "','" + CryptographyHelper.Encrypt(PRO.ADDRESS) + "','" + CryptographyHelper.Encrypt(PRO.UPSINATURE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGDATE) + "','" + CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) + "','" + CryptographyHelper.Encrypt(PRO.REMARKS) + "','" + CryptographyHelper.Encrypt(PRO.NDASIGNED) + "','" + CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N') ;").ToString();
					string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_UPSI_LOG (TID,UPSIID,RECIPIENTNAME,RECIPIENTCAT,PANNO,ADDRESS,UPSINATURE,SHARINGPURPOSE,SHARINGDATE,EFFECTIVEUPTO,REMARKS,NDASIGNED,UPSIAVAILABLE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ds + "','" + CryptographyHelper.Encrypt(CPID) + "','" + CryptographyHelper.Encrypt(PRO.RECIPIENTNAME) + "','" + CryptographyHelper.Encrypt(PRO.RECIPIENTCAT) + "','" + CryptographyHelper.Encrypt(PRO.PANNO) + "','" + CryptographyHelper.Encrypt(PRO.ADDRESS) + "','" + CryptographyHelper.Encrypt(PRO.UPSINATURE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGDATE) + "','" + CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) + "','" + CryptographyHelper.Encrypt(PRO.REMARKS) + "','" + CryptographyHelper.Encrypt(PRO.NDASIGNED) + "','" + CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N') ;").ToString();

					lg.CURRVALUE = "SHARING OF UPSI PROFILE TAB";
					lg.TYPE = "INSERTED";
					lg.ID = perlogid;
					lg.DESCRIPTION = "INSERTED VALUE :- " + CPID;
					lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
					//lg.ID = SESSIONKEYS.UserID.ToString();
					new MasterClass().SAVE_LOG(lg);

					if (Convert.ToInt32(ds) > 0)
					{
						DialogResult dialog = MessageBox.Show("Data Saved Successfully.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					Clear();
					FillConnectPersonID();
					button2.PerformClick();
				}
				//});

				SetLoading(false);
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				SetLoading(false);
				DialogResult dialog = MessageBox.Show("Data Not Saved. Please Check Your Internet Connection.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtUPSINAME_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string[] a = txtUPSINAME.SelectedItem.ToString().Split('-');

				DataSet ds1 = new MasterClass().getDataSet("SELECT * FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N' AND ID = '" + ((ComboboxItem1)txtUPSINAME.SelectedItem).UPSIID + "'");
				DataSet ds2 = new MasterClass().getDataSet("SELECT * FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N' AND ID = '" + ((ComboboxItem1)txtUPSINAME.SelectedItem).UPSIID + "'");
				txtUPSIOthercategory.Text = "";
				if (ds1.Tables[0].Rows.Count > 0)
				{
					if (CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["RECEPIENTID"].ToString()) == a[1].Trim())
					{
						txtUPSIAdress.Text = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["ADDRESS"].ToString());
						if (CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["PANNO"].ToString()).Trim() == "")
						{
							txtUPSIPanno.Text = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["OTHERIDENTIFIER"].ToString());
						}
						else
						{
							txtUPSIPanno.Text = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["PANNO"].ToString());
						}

						lblnNDS.Text = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["PANNOAFFILIATES"].ToString());
						txtUPSIcategory.Text = "";
						if (CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString()).Contains("OTHERS"))
						{
							string[] abc = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString()).Split('|');
							txtUPSIcategory.SelectedText = abc[0];
							txtUPSIOthercategory.Text = abc[1];
							label12.Visible = true;
							txtUPSIOthercategory.Visible = true;
						}
						else
						{
							txtUPSIOthercategory.Text = "";
							txtUPSIcategory.SelectedText = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString());
							label12.Visible = false;
							txtUPSIOthercategory.Visible = false;
						}
					}
				}

				if (ds2.Tables[0].Rows.Count > 0)
				{
					if (CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["CONNECTPERSONID"].ToString()) == a[1].Trim())
					{
						txtUPSIAdress.Text = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["ADDRESS"].ToString());
						if (CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["PANNO"].ToString()).Trim() == "")
						{
							txtUPSIPanno.Text = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["OTHERIDENTIFIER"].ToString());
						}
						else
						{
							txtUPSIPanno.Text = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["PANNO"].ToString());
						}
						txtUPSIcategory.Text = "Connected Person";
						lblnNDS.Text = "";
						//if (CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString()).Contains("OTHERS"))
						//{
						//	string[] abc = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString()).Split('|');
						//	txtUPSIcategory.SelectedText = abc[0];
						//	txtUPSIOthercategory.Text = abc[1];
						//	label12.Visible = true;
						//	txtUPSIOthercategory.Visible = true;
						//}
						//else
						//{
						//	txtUPSIOthercategory.Text = "";
						//	txtUPSIcategory.SelectedText = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString());
						//	label12.Visible = false;
						//	txtUPSIOthercategory.Visible = false;
						//}
					}
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}


		}

		private void txtUPSIID_Leave(object sender, EventArgs e)
		{
			try
			{
				int val = 0;
				if (txtUPSIID.Text == "")
				{
					//Clear();
					//btnupdateINSCON.Visible = false;
					//btnaddINSCONdeelete.Visible = false;
					//btncacncelINSCON.Visible = false;
					//btnaddINSCON.Visible = true;
				}
				else
				{
					for (int i = 0; i < cmdINSCONSAVEID.Items.Count; i++)
					{
						if (txtUPSIID.Text == cmdINSCONSAVEID.GetItemText(cmdINSCONSAVEID.Items[i]))
						{
							cmdINSCONSAVEID.SelectedIndex = i;
							DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_UPSI WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID + "'");
							if (ds.Tables[0].Rows.Count > 0)
							{
								txtUPSINAME.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RECIPIENTNAME"].ToString());
								txtUPSIPanno.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PANNO"].ToString());
								txtUPSIAdress.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["ADDRESS"].ToString());
								txtUPSINatureUPSI.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["UPSINATURE"].ToString());
								txtUPSIpurposesharing.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["SHARINGPURPOSE"].ToString());
								if (CryptographyHelper.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["SHARINGDATE"].ToString())).Trim() == "")
								{
									txtUPSIDateofsharing.CustomFormat = " ";
								}
								else
								{
									txtUPSIDateofsharing.Value = DateTime.ParseExact(CryptographyHelper.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["SHARINGDATE"].ToString())), "dd-MM-yyyy", CultureInfo.InvariantCulture);
								}

								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["EFFECTIVEUPTO"].ToString()).Trim() == "")
								{
									txtUPSIEffctiveUpto.CustomFormat = " ";
								}
								else
								{
									txtUPSIEffctiveUpto.Value = DateTime.ParseExact(CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["EFFECTIVEUPTO"].ToString()), "dd-MM-yyyy", CultureInfo.InvariantCulture);
								}

								txtUPSIremarks.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["REMARKS"].ToString());

								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["UPSIAVAILABLE"].ToString()).Trim() == "")
								{
									txtUPSIUPSIavaailabe.CustomFormat = " ";
								}
								else
								{
									txtUPSIUPSIavaailabe.Value = DateTime.ParseExact(CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["UPSIAVAILABLE"].ToString()), "dd-MM-yyyy", CultureInfo.InvariantCulture);

								}


								txtUPSIcategory.Text = "";
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RECIPIENTCAT"].ToString()).Contains("OTHERS"))
								{
									string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RECIPIENTCAT"].ToString()).Split('|');
									txtUPSIcategory.SelectedText = abc[0];
									txtUPSIOthercategory.Text = abc[1];
									label12.Visible = true;
									txtUPSIOthercategory.Visible = true;
								}
								else
								{
									txtUPSIcategory.SelectedText = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RECIPIENTCAT"].ToString());
								}
								string[] yes = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["NDASIGNED"].ToString()).Split('|');
								if (yes.Length > 1)
								{
									if (yes[0] == "YES")
									{
										radioButtonNDAYES.Checked = true;
									}
									else if (yes[0] == "NO")
									{
										radioButtonNDANo.Checked = true;
									}
									lblnNDS.Text = yes[1];
								}

								//txtUPSIID.ReadOnly = true;
								btnupdateINSCON.Visible = true;
								btnaddINSCONdeelete.Visible = true;
								btncacncelINSCON.Visible = true;
								btnaddINSCON.Visible = false;
								txtUPSINAME.Enabled = false;
								val++;
								//if (ds.Tables[0].Rows[0]["ACTIVE"].ToString().Trim() == "N")
								//{
								//	btnupdateINSCON.Enabled = false;
								//	btnaddINSCONdeelete.Text = "RETREIVE";
								//}
								//else
								//{
								//	btnupdateINSCON.Enabled = true;
								//	btnaddINSCONdeelete.Text = "DELETE";
								//}
							}
							else
							{
								//Clear();
								//btnupdateINSCON.Visible = false;
								//btnaddINSCONdeelete.Visible = false;
								//btncacncelINSCON.Visible = false;
								//btnaddINSCON.Visible = true;
							}
						}
					}
				}

				if (val == 0)
				{
					button2.PerformClick();
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void Clear()
		{
			lblnNDS.Text = "";
			txtUPSIID.Text = "";
			txtUPSINAME.Text = "";
			txtUPSIPanno.Text = "";
			txtUPSIAdress.Text = "";
			txtUPSINatureUPSI.Text = "";
			txtUPSIpurposesharing.Text = "";
			txtUPSIDateofsharing.Text = "";
			txtUPSIEffctiveUpto.Text = "";
			txtUPSIremarks.Text = "";
			txtUPSIUPSIavaailabe.Text = "";
			txtUPSIcategory.Text = "";
			txtUPSIOthercategory.Text = "";
			radioButtonNDAYES.Checked = false;
			radioButtonNDANo.Checked = false;
			txtUPSIOthercategory.Visible = false;
			label12.Visible = false;
		}

		private void btncacncelINSCON_Click(object sender, EventArgs e)
		{
			Clear();
			txtUPSIID.ReadOnly = false;
			btnupdateINSCON.Visible = false;
			btnaddINSCONdeelete.Visible = false;
			btncacncelINSCON.Visible = false;
			btnaddINSCON.Visible = true;
			btnaddINSCON.Enabled = false;
			FillConnectPersonID();
			button2.PerformClick();
		}

		private void btnupdateINSCON_Click(object sender, EventArgs e)
		{
			try
			{
				SetLoading(true);

				Thread.Sleep(2000);
				//Invoke((MethodInvoker)delegate
				//{
				int val = 0;
				int val2 = 0;
				if (txtUPSIEffctiveUpto.Text.ToString().Trim() != "")
				{
					val = DateTime.Compare(txtUPSIDateofsharing.Value, txtUPSIEffctiveUpto.Value);
				}
				if (txtUPSIUPSIavaailabe.Text.ToString().Trim() != "")
				{
					val2 = DateTime.Compare(txtUPSIDateofsharing.Value, txtUPSIUPSIavaailabe.Value);
				}
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
					lg.DESCRIPTION = "FORCE LOGOUT DUE TO DATE MISMATCH";
					lg.TYPE = "SELECTED";
					lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
					//lg.ID = SESSIONKEYS.UserID.ToString();
					string json = new MasterClass().SAVE_LOG(lg);
					SESSIONKEYS.UserID = "";
					SESSIONKEYS.Role = "";
					SESSIONKEYS.FullName = "";
					l.Show();
					Close();
				}
				else
				if (txtUPSIID.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter UPSI ID.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (txtUPSINAME.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Recepeint Name.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (val > 0)
				{
					DialogResult dialog = MessageBox.Show("Date of Sharing can't be more than Effective Date.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (val2 > 0)
				{
					DialogResult dialog = MessageBox.Show("Date of Sharing can't be more than UPSI available Date.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					T_INS_UPSI PRO = new T_INS_UPSI
					{
						UPSIID = txtUPSIID.Text,
						RECIPIENTNAME = txtUPSINAME.Text,
						PANNO = txtUPSIPanno.Text,
						ADDRESS = txtUPSIAdress.Text,
						UPSINATURE = txtUPSINatureUPSI.Text,
						SHARINGPURPOSE = txtUPSIpurposesharing.Text,
						SHARINGDATE = txtUPSIDateofsharing.Text,
						EFFECTIVEUPTO = txtUPSIEffctiveUpto.Text,
						REMARKS = txtUPSIremarks.Text,
						UPSIAVAILABLE = txtUPSIUPSIavaailabe.Text,
					};
					if (radioButtonNDAYES.Checked == true)
					{
						PRO.NDASIGNED = "YES|" + lblnNDS.Text;
					}
					else if (radioButtonNDANo.Checked == true)
					{
						PRO.NDASIGNED = "NO|" + lblnNDS.Text;
					}
					else
					{
						PRO.NDASIGNED = "|" + lblnNDS.Text;
					}

					if (txtUPSIcategory.Text == "OTHERS")
					{
						PRO.RECIPIENTCAT = "OTHERS|" + txtUPSIOthercategory.Text;
					}
					else
					{
						PRO.RECIPIENTCAT = txtUPSIcategory.Text;
					}
					PRO.ID = "";
					PRO.ENTEREDBY = SESSIONKEYS.UserID.ToString();

					DataSet getval = new MasterClass().getDataSet("SELECT ID FROM T_INS_UPSI_LOG WHERE ACTIVE = 'Y' ORDER BY ENTEREDON DESC");

					string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_UPSI SET UPSIID = '" + CryptographyHelper.Encrypt(PRO.UPSIID) + "',RECIPIENTNAME = '" + CryptographyHelper.Encrypt(PRO.RECIPIENTNAME) + "',RECIPIENTCAT = '" + CryptographyHelper.Encrypt(PRO.RECIPIENTCAT) + "',PANNO = '" + CryptographyHelper.Encrypt(PRO.PANNO) + "',ADDRESS = '" + CryptographyHelper.Encrypt(PRO.ADDRESS) + "',UPSINATURE = '" + CryptographyHelper.Encrypt(PRO.UPSINATURE) + "',SHARINGPURPOSE = '" + CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) + "',SHARINGDATE = '" + CryptographyHelper.Encrypt(PRO.SHARINGDATE) + "',EFFECTIVEUPTO = '" + CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) + "',REMARKS = '" + CryptographyHelper.Encrypt(PRO.REMARKS) + "',NDASIGNED = '" + CryptographyHelper.Encrypt(PRO.NDASIGNED) + "',UPSIAVAILABLE = '" + CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) + "',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "' ; ").ToString();

					string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_UPSI_LOG (TID,UPSIID,RECIPIENTNAME,RECIPIENTCAT,PANNO,ADDRESS,UPSINATURE,SHARINGPURPOSE,SHARINGDATE,EFFECTIVEUPTO,REMARKS,NDASIGNED,UPSIAVAILABLE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + CryptographyHelper.Encrypt(PRO.UPSIID) + "','" + CryptographyHelper.Encrypt(PRO.RECIPIENTNAME) + "','" + CryptographyHelper.Encrypt(PRO.RECIPIENTCAT) + "','" + CryptographyHelper.Encrypt(PRO.PANNO) + "','" + CryptographyHelper.Encrypt(PRO.ADDRESS) + "','" + CryptographyHelper.Encrypt(PRO.UPSINATURE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGDATE) + "','" + CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) + "','" + CryptographyHelper.Encrypt(PRO.REMARKS) + "','" + CryptographyHelper.Encrypt(PRO.NDASIGNED) + "','" + CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N') ;").ToString();

					lg.CURRVALUE = "SHARING OF UPSI PROFILE TAB";
					lg.TYPE = "UPDATED";
					lg.ID = perlogid + "|" + getval.Tables[0].Rows[0]["ID"].ToString();
					lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
					//lg.ID = SESSIONKEYS.UserID.ToString();
					lg.DESCRIPTION = "UPDATED VALUE :- " + PRO.UPSIID;
					new MasterClass().SAVE_LOG(lg);

					if (Convert.ToInt32(ds) > 0)
					{
						DialogResult dialog = MessageBox.Show("Updated Data Successfully.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					Clear();
					txtUPSIID.ReadOnly = false;
					btnupdateINSCON.Visible = false;
					btnaddINSCONdeelete.Visible = false;
					btncacncelINSCON.Visible = false;
					btnaddINSCON.Visible = true;
					FillConnectPersonID();
					button2.PerformClick();
				}
				//});

				SetLoading(false);
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				SetLoading(false);
				DialogResult dialog = MessageBox.Show("Data Not Updated. Please Check Your Internet Connection.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnaddINSCONdeelete_Click(object sender, EventArgs e)
		{
			try
			{
				SetLoading(true);

				Thread.Sleep(2000);
				//Invoke((MethodInvoker)delegate
				//{
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
					lg.DESCRIPTION = "FORCE LOGOUT DUE TO DATE MISMATCH";
					lg.TYPE = "SELECTED";
					lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
					//lg.ID = SESSIONKEYS.UserID.ToString();
					string json = new MasterClass().SAVE_LOG(lg);
					SESSIONKEYS.UserID = "";
					SESSIONKEYS.Role = "";
					SESSIONKEYS.FullName = "";
					l.Show();
					Close();
				}
				else
				{
					T_INS_UPSI PRO = new T_INS_UPSI
					{
						UPSIID = txtUPSIID.Text,
						RECIPIENTNAME = txtUPSINAME.Text,
						PANNO = txtUPSIPanno.Text,
						ADDRESS = txtUPSIAdress.Text,
						UPSINATURE = txtUPSINatureUPSI.Text,
						SHARINGPURPOSE = txtUPSIpurposesharing.Text,
						SHARINGDATE = txtUPSIDateofsharing.Text,
						EFFECTIVEUPTO = txtUPSIEffctiveUpto.Text,
						REMARKS = txtUPSIremarks.Text,
						UPSIAVAILABLE = txtUPSIUPSIavaailabe.Text,
					};
					if (radioButtonNDAYES.Checked == true)
					{
						PRO.NDASIGNED = "YES|" + lblnNDS.Text;
					}
					else if (radioButtonNDANo.Checked == true)
					{
						PRO.NDASIGNED = "NO|" + lblnNDS.Text;
					}
					else
					{
						PRO.NDASIGNED = "|" + lblnNDS.Text;
					}

					if (txtUPSIcategory.Text == "OTHERS")
					{
						PRO.RECIPIENTCAT = "OTHERS|" + txtUPSIOthercategory.Text;
					}
					else
					{
						PRO.RECIPIENTCAT = txtUPSIcategory.Text;
					}
					PRO.ID = "";
					PRO.ENTEREDBY = SESSIONKEYS.UserID.ToString();
					DialogResult dialogResult;
					if (btnaddINSCONdeelete.Text == "RETREIVE")
					{
						dialogResult = MessageBox.Show("Are You Sure You Want to Retreive?", "Sharing of UPSI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (dialogResult == DialogResult.Yes)
						{
							string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_UPSI  SET ACTIVE = 'Y',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + MasterClass.GETIST() + "' WHERE ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "' ; ").ToString();

							string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_UPSI_LOG (TID,UPSIID,RECIPIENTNAME,RECIPIENTCAT,PANNO,ADDRESS,UPSINATURE,SHARINGPURPOSE,SHARINGDATE,EFFECTIVEUPTO,REMARKS,NDASIGNED,UPSIAVAILABLE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + CryptographyHelper.Encrypt(PRO.UPSIID) + "','" + CryptographyHelper.Encrypt(PRO.RECIPIENTNAME) + "','" + CryptographyHelper.Encrypt(PRO.RECIPIENTCAT) + "','" + CryptographyHelper.Encrypt(PRO.PANNO) + "','" + CryptographyHelper.Encrypt(PRO.ADDRESS) + "','" + CryptographyHelper.Encrypt(PRO.UPSINATURE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGDATE) + "','" + CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) + "','" + CryptographyHelper.Encrypt(PRO.REMARKS) + "','" + CryptographyHelper.Encrypt(PRO.NDASIGNED) + "','" + CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("RETREIVE") + "','Y','N') ;").ToString();

							lg.CURRVALUE = "SHARING OF UPSI PROFILE TAB";
							lg.TYPE = "RETREIVE";
							lg.ID = perlogid;
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							//lg.ID = SESSIONKEYS.UserID.ToString();
							lg.DESCRIPTION = "DELETED VALUE :- " + PRO.UPSIID;
							new MasterClass().SAVE_LOG(lg);

							if (Convert.ToInt32(ds) > 0)
							{
								DialogResult dialog = MessageBox.Show("Updated Successfully.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
								Clear();
								txtUPSIID.ReadOnly = false;
								btnupdateINSCON.Visible = false;
								btnaddINSCONdeelete.Visible = false;
								btncacncelINSCON.Visible = false;
								btnaddINSCON.Visible = true;
								FillConnectPersonID();
								button2.PerformClick();
							}
							else
							{
								DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
						}
					}
					else
					{
						dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Sharing of UPSI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (dialogResult == DialogResult.Yes)
						{
							string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_UPSI  SET ACTIVE = 'N',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + MasterClass.GETIST() + "' WHERE ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "' ; ").ToString();

							string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_UPSI_LOG (TID,UPSIID,RECIPIENTNAME,RECIPIENTCAT,PANNO,ADDRESS,UPSINATURE,SHARINGPURPOSE,SHARINGDATE,EFFECTIVEUPTO,REMARKS,NDASIGNED,UPSIAVAILABLE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + CryptographyHelper.Encrypt(PRO.UPSIID) + "','" + CryptographyHelper.Encrypt(PRO.RECIPIENTNAME) + "','" + CryptographyHelper.Encrypt(PRO.RECIPIENTCAT) + "','" + CryptographyHelper.Encrypt(PRO.PANNO) + "','" + CryptographyHelper.Encrypt(PRO.ADDRESS) + "','" + CryptographyHelper.Encrypt(PRO.UPSINATURE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGDATE) + "','" + CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) + "','" + CryptographyHelper.Encrypt(PRO.REMARKS) + "','" + CryptographyHelper.Encrypt(PRO.NDASIGNED) + "','" + CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("DELETED") + "','Y','N') ;").ToString();

							lg.CURRVALUE = "SHARING OF UPSI PROFILE TAB";
							lg.TYPE = "DELETED";
							lg.ID = perlogid;
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							//lg.ID = SESSIONKEYS.UserID.ToString();
							lg.DESCRIPTION = "DELETED VALUE :- " + PRO.UPSIID;
							new MasterClass().SAVE_LOG(lg);

							if (Convert.ToInt32(ds) > 0)
							{
								DialogResult dialog = MessageBox.Show("Updated Successfully.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
								Clear();
								txtUPSIID.ReadOnly = false;
								btnupdateINSCON.Visible = false;
								btnaddINSCONdeelete.Visible = false;
								btncacncelINSCON.Visible = false;
								btnaddINSCON.Visible = true;
								FillConnectPersonID();
								button2.PerformClick();
							}
							else
							{
								DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
						}

					}

				}
				//});

				SetLoading(false);
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				SetLoading(false);
				DialogResult dialog = MessageBox.Show("Data Not Deleted. Please Check Your Internet Connection.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtUPSINAME_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar < 32 || e.KeyChar > 126)
			{
				return;
			}
			string t = txtUPSINAME.Text;
			string typedT = t.Substring(0, txtUPSINAME.SelectionStart);
			string newT = typedT + e.KeyChar;

			int i = txtUPSINAME.FindString(newT);
			if (i == -1)
			{
				e.Handled = true;
			}
		}

		private void txtUPSINAME_Leave(object sender, EventArgs e)
		{
			string t = txtUPSINAME.Text;

			if (txtUPSINAME.SelectedItem == null)
			{
				txtUPSINAME.Text = "";
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			HOMEPAGE h = new HOMEPAGE();
			h.Show();
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Clear();
			FillConnectPersonID();
			string CPID = "UPSI" + new MasterClass().GETUPSIID();
			txtUPSIID.Text = CPID;
			txtUPSIID.Enabled = false;
			btnupdateINSCON.Visible = false;
			btnaddINSCONdeelete.Visible = false;
			btncacncelINSCON.Visible = false;
			btnaddINSCON.Visible = true;

			btnaddINSCON.Enabled = true;
			txtUPSINAME.Enabled = true;
			txtUPSINatureUPSI.Enabled = true;
			txtUPSIpurposesharing.Enabled = true;
			txtUPSIDateofsharing.Enabled = true;
			txtUPSIEffctiveUpto.Enabled = true;
			txtUPSIremarks.Enabled = true;
			radioButtonNDAYES.Enabled = true;
			radioButtonNDANo.Enabled = true;
			txtUPSIUPSIavaailabe.Enabled = true;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Clear();
			FillConnectPersonID();
			txtUPSIID.Text = "";
			txtUPSIID.Enabled = true;
			btnupdateINSCON.Visible = true;
			btnaddINSCONdeelete.Visible = true;
			btncacncelINSCON.Visible = true;
			btnaddINSCON.Visible = false;

			btnupdateINSCON.Enabled = true;
			btnaddINSCONdeelete.Enabled = true;
			btncacncelINSCON.Enabled = true;

			txtUPSINAME.Enabled = true;
			txtUPSINatureUPSI.Enabled = true;
			txtUPSIpurposesharing.Enabled = true;
			txtUPSIDateofsharing.Enabled = true;
			txtUPSIEffctiveUpto.Enabled = true;
			txtUPSIremarks.Enabled = true;
			radioButtonNDAYES.Enabled = true;
			radioButtonNDANo.Enabled = true;
			txtUPSIUPSIavaailabe.Enabled = true;
		}

		private void txtUPSIDateofsharing_ValueChanged(object sender, EventArgs e)
		{
			txtUPSIDateofsharing.CustomFormat = "dd-MM-yyyy";
		}

		private void txtUPSIEffctiveUpto_ValueChanged(object sender, EventArgs e)
		{
			txtUPSIEffctiveUpto.CustomFormat = "dd-MM-yyyy";
		}

		private void txtUPSIUPSIavaailabe_ValueChanged(object sender, EventArgs e)
		{
			txtUPSIUPSIavaailabe.CustomFormat = "dd-MM-yyyy";
		}

		#endregion
	}
}
