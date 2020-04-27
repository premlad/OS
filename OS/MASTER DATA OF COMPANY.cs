using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OS
{
	public partial class MASTER_DATA_OF_COMPANY : MASTERFORM
	{
		public MASTER_DATA_OF_COMPANY()
		{
			InitializeComponent();
		}

		private void MASTER_DATA_OF_COMPANY_Load(object sender, EventArgs e)
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
			FIllData();
		}

		#region MASTER DATA OF COMPANY

		public void FIllData()
		{
			Hashtable hstmst = new Hashtable
				{
					{ "@ID", "1" },
					{ "@ACTION", "2" }
				};
			DataSet ds = new MasterClass().executeDatable_SP("STP_INS_COMPANY", hstmst);
			if (ds.Tables[0].Rows.Count > 0)
			{
				txtCompanyName.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["COMPANYNAME"].ToString());
				txtregisteredOffice.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["REGOFFICE"].ToString());
				txtCorporateOffice.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["CORPORATEOFFICE"].ToString());
				txtMobileNo.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["MOBILENO"].ToString());
				txtLandLineNo.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["LANDLINENO"].ToString());
				txtEmailID.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["EMAILID"].ToString());
				txtCIN.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["CIN"].ToString());
				txtBSE.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["BSECODE"].ToString());
				txtNSE.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["NSECODE"].ToString());
				txtISIN.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["ISIN"].ToString());
				txtOfficerName.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["OFFICERNAME"].ToString());
				txtDesignation.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["DESIGNATION"].ToString());
			}
		}

		private void btnaddINSCON_Click(object sender, EventArgs e)
		{
			if (!new MasterClass().IsValidEmail(txtEmailID.Text))
			{
				DialogResult dialog = MessageBox.Show("Please Enter Email in Proper Format.", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				Hashtable hstmst = new Hashtable
				{
					{ "@ID", "1" },
					{ "@COMPANYNAME", CryptographyHelper.Encrypt(txtCompanyName.Text)},
					{ "@REGOFFICE", CryptographyHelper.Encrypt(txtregisteredOffice.Text)},
					{ "@CORPORATEOFFICE", CryptographyHelper.Encrypt(txtCorporateOffice.Text)},
					{ "@MOBILENO", CryptographyHelper.Encrypt(txtMobileNo.Text)},
					{ "@LANDLINENO", CryptographyHelper.Encrypt(txtLandLineNo.Text)},
					{ "@EMAILID", CryptographyHelper.Encrypt(txtEmailID.Text)},
					{ "@CIN", CryptographyHelper.Encrypt(txtCIN.Text)},
					{ "@BSECODE", CryptographyHelper.Encrypt(txtBSE.Text)},
					{ "@NSECODE", CryptographyHelper.Encrypt(txtNSE.Text)},
					{ "@ISIN", CryptographyHelper.Encrypt(txtISIN.Text)},
					{ "@OFFICERNAME", CryptographyHelper.Encrypt(txtOfficerName.Text)},
					{ "@DESIGNATION", CryptographyHelper.Encrypt(txtDesignation.Text)},
					{ "@ENTEREDON", MasterClass.GETIST() },
					{ "@ENTEREDBY", SESSIONKEYS.UserID.ToString() },//SESSIONKEYS.UserID.ToString()
					{ "@ACTION", "1" }
				};
				string ds = new MasterClass().executeScalar_SP("STP_INS_COMPANY", hstmst);
				if (Convert.ToInt32(ds) > 0)
				{
					DialogResult dialog = MessageBox.Show("Updated Data Successfully.", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				InitializeComponent();
				FIllData();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			HOMEPAGE H = new HOMEPAGE();
			H.Show();
			Hide();
		}

		#endregion

	}
}
