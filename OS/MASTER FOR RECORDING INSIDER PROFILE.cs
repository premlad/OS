using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OS
{
	public partial class MASTER_FOR_RECORDING_INSIDER_PROFILE : MASTERFORM
	{
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
			Hashtable hstmst = new Hashtable
				{
					{ "@ACTION", "2" }
				};
			DataSet ds = new MasterClass().executeDatable_SP("STP_INS_PRO", hstmst);
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

		public class ComboboxItem
		{
			public string NAME { get; set; }
			public object ID { get; set; }

			public override string ToString()
			{
				return NAME;
			}
		}

		private void btnaddINSCON_Click(object sender, EventArgs e)
		{
			if (txtINSPROreceipeitnID.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Recepient ID.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (txtINSPROnameofinsider.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Name of Insider.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (txtINSPROpannomaster.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Pan No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (txtMobileINSPRONumber.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Mobile No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				Hashtable hstmst = new Hashtable
				{
					{ "@RECEPIENTID", CryptographyHelper.Encrypt(PRO.RECEPIENT_ID) },
					{ "@NAMEINSIDER", CryptographyHelper.Encrypt(PRO.NAME_OF_INSIDER) },
					{ "@CATEGORYRECEIPT", CryptographyHelper.Encrypt(PRO.CATEGORY_OF_RECEIPT) },
					{ "@ADDRESS", CryptographyHelper.Encrypt(PRO.ADDRESS) },
					{ "@PANNO", CryptographyHelper.Encrypt(PRO.PAN_NO) },
					{ "@AADHARNO", CryptographyHelper.Encrypt(PRO.AADHAR_NO) },
					{ "@MOBILENO", CryptographyHelper.Encrypt(PRO.MOBILE_NO) },
					{ "@LANDLINENO", CryptographyHelper.Encrypt(PRO.LANDLINE_NO) },
					{ "@EMAILID", CryptographyHelper.Encrypt(PRO.EMAIL_ID) },
					{ "@PANNOAFFILIATES", CryptographyHelper.Encrypt(PRO.PAN_NO_OF_AFFILAIATES) },
					{ "@ENTEREDON", MasterClass.GETIST() },
					{ "@ENTEREDBY", SESSIONKEYS.UserID.ToString() },//SESSIONKEYS.UserID.ToString()
					{ "@ACTION", "1" }
				};
				string ds = new MasterClass().executeScalar_SP("STP_INS_PRO", hstmst);
				if (Convert.ToInt32(ds) > 0)
				{
					DialogResult dialog = MessageBox.Show("Data Saved Successfully.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				InitializeComponent();
				FillConnectPersonID();
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
				if (txtINSPROreceipeitnID.Text == "")
				{
					Clear();
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
							Hashtable hstmst = new Hashtable
							{
								{ "@ACTION", "3" },
								{ "@ID", ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID }
							};
							DataSet ds = new MasterClass().executeDatable_SP("STP_INS_PRO", hstmst);
							if (ds.Tables[0].Rows.Count > 0)
							{
								txtINSPROnameofinsider.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["NAMEINSIDER"].ToString());
								txtINSPROreceipeitnID.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RECEPIENTID"].ToString());
								txtINSPROaddressmaster.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["ADDRESS"].ToString());
								txtINSPROpannomaster.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PANNO"].ToString());
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
									cmdINSPROcategoryothers.SelectedText = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString());
								}
								txtINSPROreceipeitnID.ReadOnly = true;
								btnupdateINSCON.Visible = true;
								btnaddINSCONdeelete.Visible = true;
								btncacncelINSCON.Visible = true;
								btnaddINSCON.Visible = false;
							}
							else
							{
								Clear();
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
		}

		private void btncacncelINSCON_Click(object sender, EventArgs e)
		{
			InitializeComponent();
			FillConnectPersonID();

		}

		private void btnupdateINSCON_Click(object sender, EventArgs e)
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
			Hashtable hstmst = new Hashtable
				{
					{ "@ID", ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() },
					{ "@RECEPIENTID", CryptographyHelper.Encrypt(PRO.RECEPIENT_ID) },
					{ "@NAMEINSIDER", CryptographyHelper.Encrypt(PRO.NAME_OF_INSIDER) },
					{ "@CATEGORYRECEIPT", CryptographyHelper.Encrypt(PRO.CATEGORY_OF_RECEIPT) },
					{ "@ADDRESS", CryptographyHelper.Encrypt(PRO.ADDRESS) },
					{ "@PANNO", CryptographyHelper.Encrypt(PRO.PAN_NO) },
					{ "@AADHARNO", CryptographyHelper.Encrypt(PRO.AADHAR_NO) },
					{ "@MOBILENO", CryptographyHelper.Encrypt(PRO.MOBILE_NO) },
					{ "@LANDLINENO", CryptographyHelper.Encrypt(PRO.LANDLINE_NO) },
					{ "@EMAILID", CryptographyHelper.Encrypt(PRO.EMAIL_ID) },
					{ "@PANNOAFFILIATES", CryptographyHelper.Encrypt(PRO.PAN_NO_OF_AFFILAIATES) },
					{ "@ENTEREDON", MasterClass.GETIST() },
					{ "@ENTEREDBY", SESSIONKEYS.UserID.ToString() },//SESSIONKEYS.UserID.ToString()
					{ "@ACTION", "4" }
				};
			string ds = new MasterClass().executeScalar_SP("STP_INS_PRO", hstmst);
			if (Convert.ToInt32(ds) > 0)
			{
				DialogResult dialog = MessageBox.Show("Updated Successfully.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
				InitializeComponent();
				FillConnectPersonID();
			}
			else
			{
				DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void btnaddINSCONdeelete_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Insider Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				Hashtable hstmst = new Hashtable
				{
					{ "@ID", ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() },
					{ "@ENTEREDON", MasterClass.GETIST() },
					{ "@ENTEREDBY", SESSIONKEYS.UserID.ToString() },//SESSIONKEYS.UserID.ToString()
					{ "@ACTION", "5" }
				};
				string ds = new MasterClass().executeScalar_SP("STP_INS_PRO", hstmst);
				if (Convert.ToInt32(ds) > 0)
				{
					DialogResult dialog = MessageBox.Show("Deleted Successfully.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
					InitializeComponent();
					FillConnectPersonID();
				}
				else
				{
					DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		#endregion
	}
}
