using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace OS
{
	public partial class RECORDING_OF_SHARING_OF_UPSI : MASTERFORM
	{
		public RECORDING_OF_SHARING_OF_UPSI()
		{
			InitializeComponent();
		}

		private void RECORDING_OF_SHARING_OF_UPSI_Load(object sender, EventArgs e)
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

		#region RECORDING OF SHARING OF UPSI

		public void FillConnectPersonID()
		{
			txtUPSIDateofsharing.CustomFormat = "dd-MM-yyyy";
			txtUPSIEffctiveUpto.CustomFormat = "dd-MM-yyyy";
			txtUPSIUPSIavaailabe.CustomFormat = "dd-MM-yyyy";
			Hashtable hstmst = new Hashtable
				{
					{ "@ACTION", "2" }
				};
			DataSet ds = new MasterClass().executeDatable_SP("STP_INS_UPSI", hstmst);
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

			for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
			{
				ComboboxItem1 item = new ComboboxItem1
				{
					NAMEINSIDER = CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["NAMEINSIDER"].ToString()),
					UPSIID = ds.Tables[1].Rows[i]["ID"].ToString()
				};
				txtUPSINAME.Items.Add(item);
				txtUPSINAME.DisplayMember = "NAMEINSIDER";
				txtUPSINAME.ValueMember = "UPSIID";
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

		private void btnaddINSCON_Click(object sender, EventArgs e)
		{
			if (txtUPSIID.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter UPSI ID.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (txtUPSINAME.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Recepeint Name.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			//else if (Convert.ToDateTime(txtUPSIDateofsharing.Text) < Convert.ToDateTime(txtUPSIEffctiveUpto.Text))
			//{
			//	DialogResult dialog = MessageBox.Show("Date of Sharing can't be less than Effective Date.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//}
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
					PRO.NDASIGNED = "YES";
				}
				else if (radioButtonNDANo.Checked == true)
				{
					PRO.NDASIGNED = "NO";
				}
				else
				{
					PRO.NDASIGNED = "";
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
				Hashtable hstmst = new Hashtable
				{
					{ "@UPSIID", CryptographyHelper.Encrypt(PRO.UPSIID) },
					{ "@RECIPIENTNAME", CryptographyHelper.Encrypt(PRO.RECIPIENTNAME) },
					{ "@RECIPIENTCAT", CryptographyHelper.Encrypt(PRO.RECIPIENTCAT) },
					{ "@PANNO", CryptographyHelper.Encrypt(PRO.PANNO) },
					{ "@ADDRESS", CryptographyHelper.Encrypt(PRO.ADDRESS) },
					{ "@UPSINATURE", CryptographyHelper.Encrypt(PRO.UPSINATURE) },
					{ "@SHARINGPURPOSE", CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) },
					{ "@SHARINGDATE", CryptographyHelper.Encrypt(PRO.SHARINGDATE) },
					{ "@EFFECTIVEUPTO", CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) },
					{ "@REMARKS", CryptographyHelper.Encrypt(PRO.REMARKS) },
					{ "@NDASIGNED", CryptographyHelper.Encrypt(PRO.NDASIGNED) },
					{ "@UPSIAVAILABLE", CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) },
					{ "@ENTEREDON", MasterClass.GETIST() },
					{ "@ENTEREDBY", SESSIONKEYS.UserID.ToString() },//SESSIONKEYS.UserID.ToString()
					{ "@ACTION", "1" }
				};
				string ds = new MasterClass().executeScalar_SP("STP_INS_UPSI", hstmst);
				if (Convert.ToInt32(ds) > 0)
				{
					DialogResult dialog = MessageBox.Show("Data Saved Successfully.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				InitializeComponent();
				FillConnectPersonID();
			}
		}

		private void txtUPSINAME_SelectedIndexChanged(object sender, EventArgs e)
		{
			Hashtable hstmst = new Hashtable
			{
				{ "@ACTION", "6" },
				{ "@ID", ((ComboboxItem1)txtUPSINAME.SelectedItem).UPSIID }
			};
			DataSet ds = new MasterClass().executeDatable_SP("STP_INS_UPSI", hstmst);
			if (ds.Tables[0].Rows.Count > 0)
			{
				txtUPSIAdress.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["ADDRESS"].ToString());
				txtUPSIPanno.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PANNO"].ToString());
				txtUPSIcategory.Text = "";
				if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString()).Contains("OTHERS"))
				{
					string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString()).Split('|');
					txtUPSIcategory.SelectedText = abc[0];
					txtUPSIOthercategory.Text = abc[1];
					label12.Visible = true;
					txtUPSIOthercategory.Visible = true;
				}
				else
				{
					txtUPSIOthercategory.Text = "";
					txtUPSIcategory.SelectedText = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString());
					label12.Visible = false;
					txtUPSIOthercategory.Visible = false;
				}
			}
		}

		private void txtUPSIID_Leave(object sender, EventArgs e)
		{
			try
			{
				if (txtUPSIID.Text == "")
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
						if (txtUPSIID.Text == cmdINSCONSAVEID.GetItemText(cmdINSCONSAVEID.Items[i]))
						{
							cmdINSCONSAVEID.SelectedIndex = i;
							Hashtable hstmst = new Hashtable
							{
								{ "@ACTION", "3" },
								{ "@ID", ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID }
							};
							DataSet ds = new MasterClass().executeDatable_SP("STP_INS_UPSI", hstmst);
							if (ds.Tables[0].Rows.Count > 0)
							{
								txtUPSINAME.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RECIPIENTNAME"].ToString());
								txtUPSIPanno.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PANNO"].ToString());
								txtUPSIAdress.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["ADDRESS"].ToString());
								txtUPSINatureUPSI.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["UPSINATURE"].ToString());
								txtUPSIpurposesharing.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["SHARINGPURPOSE"].ToString());
								txtUPSIDateofsharing.Value = DateTime.ParseExact(CryptographyHelper.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["SHARINGDATE"].ToString())), "dd-MM-yyyy", CultureInfo.InvariantCulture);

								txtUPSIEffctiveUpto.Value = DateTime.ParseExact(CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["EFFECTIVEUPTO"].ToString()), "dd-MM-yyyy", CultureInfo.InvariantCulture);

								txtUPSIremarks.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["REMARKS"].ToString());

								txtUPSIUPSIavaailabe.Value = DateTime.ParseExact(CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["UPSIAVAILABLE"].ToString()), "dd-MM-yyyy", CultureInfo.InvariantCulture);

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
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["REMARKS"].ToString()) == "YES")
								{
									radioButtonNDAYES.Checked = true;
								}
								else
								{
									radioButtonNDANo.Checked = true;
								}
								txtUPSIID.ReadOnly = true;
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
		}

		private void btncacncelINSCON_Click(object sender, EventArgs e)
		{
			InitializeComponent();
			FillConnectPersonID();
		}

		private void btnupdateINSCON_Click(object sender, EventArgs e)
		{
			if (txtUPSIID.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter UPSI ID.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (txtUPSINAME.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Recepeint Name.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
					PRO.NDASIGNED = "YES";
				}
				else if (radioButtonNDANo.Checked == true)
				{
					PRO.NDASIGNED = "NO";
				}
				else
				{
					PRO.NDASIGNED = "";
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
				Hashtable hstmst = new Hashtable
				{
					{ "@ID", ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() },
					{ "@UPSIID", CryptographyHelper.Encrypt(PRO.UPSIID) },
					{ "@RECIPIENTNAME", CryptographyHelper.Encrypt(PRO.RECIPIENTNAME) },
					{ "@RECIPIENTCAT", CryptographyHelper.Encrypt(PRO.RECIPIENTCAT) },
					{ "@PANNO", CryptographyHelper.Encrypt(PRO.PANNO) },
					{ "@ADDRESS", CryptographyHelper.Encrypt(PRO.ADDRESS) },
					{ "@UPSINATURE", CryptographyHelper.Encrypt(PRO.UPSINATURE) },
					{ "@SHARINGPURPOSE", CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) },
					{ "@SHARINGDATE", CryptographyHelper.Encrypt(PRO.SHARINGDATE) },
					{ "@EFFECTIVEUPTO", CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) },
					{ "@REMARKS", CryptographyHelper.Encrypt(PRO.REMARKS) },
					{ "@NDASIGNED", CryptographyHelper.Encrypt(PRO.NDASIGNED) },
					{ "@UPSIAVAILABLE", CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) },
					{ "@ENTEREDON", MasterClass.GETIST() },
					{ "@ENTEREDBY", SESSIONKEYS.UserID.ToString() },//SESSIONKEYS.UserID.ToString()
					{ "@ACTION", "4" }
				};
				string ds = new MasterClass().executeScalar_SP("STP_INS_UPSI", hstmst);
				if (Convert.ToInt32(ds) > 0)
				{
					DialogResult dialog = MessageBox.Show("Updated Data Successfully.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				InitializeComponent();
				FillConnectPersonID();
			}
		}

		private void btnaddINSCONdeelete_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Sharing of UPSI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				Hashtable hstmst = new Hashtable
				{
					{ "@ID", ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() },
					{ "@ENTEREDON", MasterClass.GETIST() },
					{ "@ENTEREDBY", SESSIONKEYS.UserID.ToString() },//SESSIONKEYS.UserID.ToString()
					{ "@ACTION", "5" }
				};
				string ds = new MasterClass().executeScalar_SP("STP_INS_UPSI", hstmst);
				if (Convert.ToInt32(ds) > 0)
				{
					DialogResult dialog = MessageBox.Show("Deleted Successfully.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
					InitializeComponent();
					FillConnectPersonID();
				}
				else
				{
					DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
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
			Hide();
		}

		#endregion

	}
}
