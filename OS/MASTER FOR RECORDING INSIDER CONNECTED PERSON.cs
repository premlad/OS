using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace OS
{
	public partial class MASTER_FOR_RECORDING_INSIDER_CONNECTED_PERSON : MASTERFORM
	{
		private AUDITLOG lg = new AUDITLOG();
		private readonly UnicodeEncoding ByteConverter = new UnicodeEncoding();
		private readonly RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
		private readonly byte[] plaintext;
		private readonly byte[] encryptedtext;
		private string PANO = "";
		private MasterClass mc = new MasterClass();

		public MASTER_FOR_RECORDING_INSIDER_CONNECTED_PERSON()
		{
			InitializeComponent();
		}

		private void MASTER_FOR_RECORDING_INSIDER_CONNECTED_PERSON_Load(object sender, EventArgs e)
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

		#region MASTER FOR RECORDING INSIDER CONNECTED PERSON

		public void FillConnectPersonID()
		{
			DataSet ds = new MasterClass().getDataSet("SELECT ID,CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'");
			AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
			if (ds.Tables[0].Rows.Count > 0)
			{
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					ComboboxItem item = new ComboboxItem
					{
						NAME = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CONNECTPERSONID"].ToString()),
						ID = ds.Tables[0].Rows[i]["ID"].ToString()
					};
					cmdINSCONSAVEID.Items.Add(item);
					cmdINSCONSAVEID.DisplayMember = "NAME";
					cmdINSCONSAVEID.ValueMember = "ID";
					MyCollection.Add(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CONNECTPERSONID"].ToString()));
				}

			}
			txtINSCONconnectperson.AutoCompleteCustomSource = MyCollection;
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

		private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
		(e.KeyChar != '.'))
			{
				e.Handled = true;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (txtMobileINSCONNumber.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Mobile/Phone No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				string fisrt = txtMobileINSCONNumber.Text;
				string[] row = { fisrt };
				dataGridViewPhonemobile.Rows.Add(row);
				txtMobileINSCONNumber.Text = "";
			}
		}

		private void btnINSCONadddematacno_Click(object sender, EventArgs e)
		{
			if (txtINSCONdemataccountno.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				string fisrt = txtINSCONdemataccountno.Text;
				string[] row = { fisrt };
				dataGridViewDematAcoount.Rows.Add(row);
				txtINSCONdemataccountno.Text = "";
			}
		}

		private void btnINSCONGraduationInstitution_Click(object sender, EventArgs e)
		{
			if (txtINSCONgraduationinstitution.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Graduation Institution", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				string fisrt = txtINSCONgraduationinstitution.Text;
				string[] row = { fisrt };
				dataGridViewGraduationInstitution.Rows.Add(row);
				txtINSCONgraduationinstitution.Text = "";
			}
		}

		private void btnINSCONpastemployees_Click(object sender, EventArgs e)
		{
			if (txtINSCONpastemployee.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Past Employee.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				string fisrt = txtINSCONpastemployee.Text;
				string[] row = { fisrt };
				dataGridViewPastEmployee.Rows.Add(row);
				txtINSCONpastemployee.Text = "";
			}
		}

		private void dataGridViewPhonemobile_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				dataGridViewPhonemobile.Rows.RemoveAt(e.RowIndex);
			}
		}

		private void dataGridViewDematAcoount_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				dataGridViewDematAcoount.Rows.RemoveAt(e.RowIndex);
			}
		}

		private void dataGridViewGraduationInstitution_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				dataGridViewGraduationInstitution.Rows.RemoveAt(e.RowIndex);
			}
		}

		private void dataGridViewPastEmployee_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				dataGridViewPastEmployee.Rows.RemoveAt(e.RowIndex);
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			if (txtINSCONrelativefullnam.Text == "" && txtINSCONrelativemobileno.Text == "" && txtINSCONrelativepanno.Text == "" && txtINSCONrelativedematacno.Text == "" && txtINSCONrelativerelationship.Text == "" && txtINSCONrelativeaddress.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Values.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				string[] row = { txtINSCONrelativefullnam.Text, txtINSCONrelativemobileno.Text, txtINSCONrelativepanno.Text, txtINSCONrelativedematacno.Text, txtINSCONrelativerelationship.Text, txtINSCONrelativeaddress.Text };
				dataGridViewISNCONrelativetable.Rows.Add(row);
				txtINSCONrelativefullnam.Text = "";
				txtINSCONrelativemobileno.Text = "";
				txtINSCONrelativepanno.Text = "";
				txtINSCONrelativedematacno.Text = "";
				txtINSCONrelativerelationship.Text = "";
				txtINSCONrelativeaddress.Text = "";
			}
		}

		private void btnmaterial_Click(object sender, EventArgs e)
		{
			if (txtINSCONfinancialfullname.Text == "" && txtINSCONfinancialmobileno.Text == "" && txtINSCONfinancialpanno.Text == "" && txtINSCONfinancialdematacno.Text == "" && txtINSCONfinancialrelationship.Text == "" && txtINSCONfinancialaddress.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Values.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				string[] row = { txtINSCONfinancialfullname.Text, txtINSCONfinancialmobileno.Text, txtINSCONfinancialpanno.Text, txtINSCONfinancialdematacno.Text, txtINSCONfinancialrelationship.Text, txtINSCONfinancialaddress.Text };
				dataGridViewmaterialfinancial.Rows.Add(row);
				txtINSCONfinancialfullname.Text = "";
				txtINSCONfinancialmobileno.Text = "";
				txtINSCONfinancialpanno.Text = "";
				txtINSCONfinancialdematacno.Text = "";
				txtINSCONfinancialrelationship.Text = "";
				txtINSCONfinancialaddress.Text = "";
			}
		}

		private void dataGridViewmaterialfinancial_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				dataGridViewmaterialfinancial.Rows.RemoveAt(e.RowIndex);
			}
		}

		private void dataGridViewISNCONrelativetable_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				dataGridViewISNCONrelativetable.Rows.RemoveAt(e.RowIndex);
			}
		}

		private void btnaddINSCON_Click(object sender, EventArgs e)
		{
			//if (txtINSCONconnectperson.Text == "")
			//{
			//	DialogResult dialog = MessageBox.Show("Enter Connect Person ID.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//}
			//else 
			if (MasterClass.GETISTI() == "TEMP")
			{
				DialogResult dialog = MessageBox.Show("Date & Time is Tempered.\nPlease Check your Date & Time Settings.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Login l = new Login();
				SESSIONKEYS.UserID = "";
				SESSIONKEYS.Role = "";
				SESSIONKEYS.FullName = "";
				lg.CURRVALUE = "LOG OUT";
				lg.DESCRIPTION = "LOG OUT SUCCESSFULLY";
				lg.TYPE = "SELECTED";
				lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
				lg.ID = SESSIONKEYS.UserID.ToString();
				string json = new MasterClass().SAVE_LOG(lg);
				l.Show();
				Hide();
			}
			else if (txtINSCONnameofemployee.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Name of Employee.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (txtINSCONpannomaster.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Pan No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (dataGridViewPhonemobile.Rows.Count <= 0)
			{
				DialogResult dialog = MessageBox.Show("Enter Phone No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				if (b.Contains(txtINSCONpannomaster.Text))
				{
					DialogResult dialog = MessageBox.Show("Pan No Already Exists in our Database.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{

					T_INS_PER CNS = new T_INS_PER
					{
						CONNECT_PERSON_ID = txtINSCONconnectperson.Text,
						NAME_OF_EMP = txtINSCONnameofemployee.Text,
						ADDRESS = txtINSCONaddressmaster.Text,
						PAN_NO = txtINSCONpannomaster.Text,
						CURRENT_DESIGNATION = txtINSCONcurrentdesigantion.Text
					};
					string PhoneNo = "";
					foreach (DataGridViewRow row in dataGridViewPhonemobile.Rows)
					{
						if (row.Index == 0)
						{
							PhoneNo += row.Cells["PhoneNo"].Value.ToString();
						}
						else
						{
							PhoneNo += "|" + row.Cells["PhoneNo"].Value.ToString();
						}

					}
					CNS.MOBILE_NO = PhoneNo;

					string DematAcNo = "";
					foreach (DataGridViewRow row in dataGridViewDematAcoount.Rows)
					{
						if (row.Index == 0)
						{
							DematAcNo += row.Cells["datagridviewdematacno"].Value.ToString();
						}
						else
						{
							DematAcNo += "|" + row.Cells["datagridviewdematacno"].Value.ToString();
						}
					}
					CNS.DEMAT_AC_NO = DematAcNo;

					string Graduation = "";
					foreach (DataGridViewRow row in dataGridViewGraduationInstitution.Rows)
					{
						if (row.Index == 0)
						{
							Graduation += row.Cells["graduationinstiytuiondatagridview"].Value.ToString();
						}
						else
						{
							Graduation += "|" + row.Cells["graduationinstiytuiondatagridview"].Value.ToString();
						}
					}
					CNS.GRADUATION_INSTITUTIONS = Graduation;

					string PastEmployee = "";
					foreach (DataGridViewRow row in dataGridViewPastEmployee.Rows)
					{
						if (row.Index == 0)
						{
							PastEmployee += row.Cells["pastemployeegridview"].Value.ToString();
						}
						else
						{
							PastEmployee += "|" + row.Cells["pastemployeegridview"].Value.ToString();
						}
					}
					CNS.PAST_EMPLOYEES = PastEmployee;

					List<T_INS_PER_IMMEDAITE_RELATIVES> T_INS_PER_IMMEDAITE_RELATIVES = new List<T_INS_PER_IMMEDAITE_RELATIVES>();
					foreach (DataGridViewRow row in dataGridViewISNCONrelativetable.Rows)
					{
						T_INS_PER_IMMEDAITE_RELATIVES RELATIVEARRAY = new T_INS_PER_IMMEDAITE_RELATIVES
						{
							NAME = row.Cells["FullName"].Value.ToString(),
							MOBILE_NO = row.Cells["MobileNo"].Value.ToString(),
							PAN_NO = row.Cells["PANNo"].Value.ToString(),
							DEMAT_AC_NO = row.Cells["DematAcNo"].Value.ToString(),
							RELATIONSHIP = row.Cells["Relationship"].Value.ToString(),
							ADDRESS = row.Cells["Address"].Value.ToString()
						};

						T_INS_PER_IMMEDAITE_RELATIVES.Add(RELATIVEARRAY);
					}
					CNS.RELATIVEARRAY = T_INS_PER_IMMEDAITE_RELATIVES.ToArray();

					List<T_INS_PER_MATERIAL_FINANCIAL> T_INS_PER_MATERIAL_FINANCIAL = new List<T_INS_PER_MATERIAL_FINANCIAL>();

					foreach (DataGridViewRow row in dataGridViewmaterialfinancial.Rows)
					{
						T_INS_PER_MATERIAL_FINANCIAL FINANCIALARRAY = new T_INS_PER_MATERIAL_FINANCIAL
						{
							NAME = row.Cells["financilaFullName"].Value.ToString(),
							MOBILE_NO = row.Cells["financilaMobileNo"].Value.ToString(),
							PAN_NO = row.Cells["FinancialPanno"].Value.ToString(),
							DEMAT_AC_NO = row.Cells["FinancialDemaAcno"].Value.ToString(),
							RELATIONSHIP = row.Cells["FinancialRelationship"].Value.ToString(),
							ADDRESS = row.Cells["Financialaddress"].Value.ToString()
						};
						T_INS_PER_MATERIAL_FINANCIAL.Add(FINANCIALARRAY);
					}
					CNS.FINANCIALARRAY = T_INS_PER_MATERIAL_FINANCIAL.ToArray();
					CNS.ENTEREDBY = SESSIONKEYS.UserID.ToString();
					string CPID = "CP" + new MasterClass().GETCPID();
					string ds = new MasterClass().executeQuery("INSERT INTO T_INS_PER (CONNECTPERSONID, EMPNAME, CURRDESIGNATION, ADDRESS, PANNO, DEMATACNO, MOBILENO, GRADUATIONINSTI, PASTEMP, ENTEREDBY, ENTEREDON, ACTIVE, LOCK) VALUES ('" + CryptographyHelper.Encrypt(CPID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','Y','N')").ToString();
					string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,PANNO,DEMATACNO,MOBILENO,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ds + "','" + CryptographyHelper.Encrypt(CPID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N')").ToString();
					lg.CURRVALUE = "CONNECTED PERSON TAB";
					lg.TYPE = "INSERTED";
					lg.ID = perlogid;
					lg.DESCRIPTION = "INSERTED VALUE :- " + CPID;
					new MasterClass().SAVE_LOG(lg);
					if (Convert.ToInt32(ds) > 0)
					{
						foreach (T_INS_PER_MATERIAL_FINANCIAL m_x in CNS.FINANCIALARRAY)
						{
							string dss = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT(PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK)  VALUES ('" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','Y','N')").ToString();
							string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dss + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N')").ToString();
							lg.CURRVALUE = "CONNECTED PERSON TAB FINANCIAL RELATIONSHIP";
							lg.TYPE = "INSERTED";
							lg.ID = perlogisdt;
							lg.DESCRIPTION = "INSERTED VALUE :- " + CPID;
							new MasterClass().SAVE_LOG(lg);
						}

						foreach (T_INS_PER_IMMEDAITE_RELATIVES m_x in CNS.RELATIVEARRAY)
						{
							string dss = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT(PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK)  VALUES ('" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','Y','N')").ToString();
							string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dss + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N')").ToString();
							lg.CURRVALUE = "CONNECTED PERSON TAB RELATIVE RELATIONSHIP";
							lg.TYPE = "INSERTED";
							lg.ID = perlogisdt;
							lg.DESCRIPTION = "INSERTED VALUE :- " + CPID;
							new MasterClass().SAVE_LOG(lg);
						}
						DialogResult dialog = MessageBox.Show("Data Saved Successfully.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					Clear();
					btnupdateINSCON.Visible = false;
					btnaddINSCONdeelete.Visible = false;
					btncacncelINSCON.Visible = false;
					btnaddINSCON.Visible = true;
					FillConnectPersonID();
				}
			}
		}

		private void txtINSCONconnectperson_Leave(object sender, EventArgs e)
		{
			try
			{

				if (txtINSCONconnectperson.Text == "")
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
						if (txtINSCONconnectperson.Text == cmdINSCONSAVEID.GetItemText(cmdINSCONSAVEID.Items[i]))
						{
							cmdINSCONSAVEID.SelectedIndex = i;
							DataTable a1 = new MasterClass().getDataTable("SELECT * FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N' AND ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID + "';");
							DataTable b1 = new MasterClass().getDataTable("SELECT * FROM T_INS_PER_DT  WHERE ACTIVE = 'Y' AND LOCK = 'N' AND PERID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID + "';");
							DataSet ds = new DataSet();
							ds.Tables.Add(a1);
							ds.Tables.Add(b1);
							if (ds.Tables[0].Rows.Count > 0)
							{
								txtINSCONnameofemployee.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["EMPNAME"].ToString());
								txtINSCONpannomaster.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PANNO"].ToString());
								PANO = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PANNO"].ToString());
								txtINSCONcurrentdesigantion.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["CURRDESIGNATION"].ToString());
								txtINSCONaddressmaster.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["ADDRESS"].ToString());

								dataGridViewPhonemobile.Rows.Clear();
								dataGridViewPhonemobile.Refresh();

								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["MOBILENO"].ToString()).Contains("|"))
								{
									string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["MOBILENO"].ToString()).Split('|');
									for (int j = 0; j < abc.Length; j++)
									{
										dataGridViewPhonemobile.Rows.Add(abc[j]);
									}
								}
								else
								{
									string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["MOBILENO"].ToString()) };
									dataGridViewPhonemobile.Rows.Add(row);
								}
								txtMobileINSCONNumber.Text = "";

								dataGridViewDematAcoount.Rows.Clear();
								dataGridViewDematAcoount.Refresh();
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["DEMATACNO"].ToString()).Contains("|"))
								{
									string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["DEMATACNO"].ToString()).Split('|');
									for (int j = 0; j < abc.Length; j++)
									{
										dataGridViewDematAcoount.Rows.Add(abc[j]);
									}
								}
								else
								{
									string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["DEMATACNO"].ToString()) };
									dataGridViewDematAcoount.Rows.Add(row);
								}
								txtINSCONdemataccountno.Text = "";

								dataGridViewGraduationInstitution.Rows.Clear();
								dataGridViewGraduationInstitution.Refresh();
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["GRADUATIONINSTI"].ToString()).Contains("|"))
								{
									string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["GRADUATIONINSTI"].ToString()).Split('|');
									for (int j = 0; j < abc.Length; j++)
									{
										dataGridViewGraduationInstitution.Rows.Add(abc[j]);
									}
								}
								else
								{
									string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["GRADUATIONINSTI"].ToString()) };
									dataGridViewGraduationInstitution.Rows.Add(row);
								}
								txtINSCONgraduationinstitution.Text = "";

								dataGridViewPastEmployee.Rows.Clear();
								dataGridViewPastEmployee.Refresh();
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["GRADUATIONINSTI"].ToString()).Contains("|"))
								{
									string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PASTEMP"].ToString()).Split('|');
									for (int j = 0; j < abc.Length; j++)
									{
										dataGridViewPastEmployee.Rows.Add(abc[j]);
									}
								}
								else
								{
									string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PASTEMP"].ToString()) };
									dataGridViewPastEmployee.Rows.Add(row);
								}
								txtINSCONpastemployee.Text = "";

								dataGridViewISNCONrelativetable.Rows.Clear();
								dataGridViewISNCONrelativetable.Refresh();
								dataGridViewmaterialfinancial.Rows.Clear();
								dataGridViewmaterialfinancial.Refresh();
								for (i = 0; i < ds.Tables[1].Rows.Count; i++)
								{
									string a = CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["TYPE"].ToString());
									if (CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["TYPE"].ToString()) == "FINANCIAL")
									{
										string[] row = { CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["DEMATACNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["RELATIONSHIP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["ADDRESS"].ToString()) };
										dataGridViewmaterialfinancial.Rows.Add(row);
									}
									else
									{
										string[] row = { CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["DEMATACNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["RELATIONSHIP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["ADDRESS"].ToString()) };
										dataGridViewISNCONrelativetable.Rows.Add(row);
									}
								}
								txtINSCONrelativefullnam.Text = "";
								txtINSCONrelativemobileno.Text = "";
								txtINSCONrelativepanno.Text = "";
								txtINSCONrelativedematacno.Text = "";
								txtINSCONrelativerelationship.Text = "";
								txtINSCONrelativeaddress.Text = "";
								txtINSCONfinancialfullname.Text = "";
								txtINSCONfinancialmobileno.Text = "";
								txtINSCONfinancialpanno.Text = "";
								txtINSCONfinancialdematacno.Text = "";
								txtINSCONfinancialrelationship.Text = "";
								txtINSCONfinancialaddress.Text = "";
								txtINSCONconnectperson.Enabled = true;
								btnupdateINSCON.Visible = true;
								btnaddINSCONdeelete.Visible = true;
								btncacncelINSCON.Visible = true;
								btnaddINSCON.Visible = false;
								break;
							}
							else
							{
								Clear();
								txtINSCONconnectperson.Enabled = false;
								btnupdateINSCON.Visible = false;
								btnaddINSCONdeelete.Visible = false;
								btncacncelINSCON.Visible = false;
								btnaddINSCON.Visible = true;
							}
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
			catch (Exception)
			{
				throw;
			}
		}

		public void Clear()
		{
			txtINSCONnameofemployee.Text = "";
			txtINSCONpannomaster.Text = "";
			txtINSCONcurrentdesigantion.Text = "";
			txtINSCONaddressmaster.Text = "";
			dataGridViewPhonemobile.Rows.Clear();
			dataGridViewPhonemobile.Refresh();
			dataGridViewDematAcoount.Rows.Clear();
			dataGridViewDematAcoount.Refresh();
			dataGridViewGraduationInstitution.Rows.Clear();
			dataGridViewGraduationInstitution.Refresh();
			dataGridViewPastEmployee.Rows.Clear();
			dataGridViewPastEmployee.Refresh();
			dataGridViewISNCONrelativetable.Rows.Clear();
			dataGridViewISNCONrelativetable.Refresh();
			dataGridViewmaterialfinancial.Rows.Clear();
			dataGridViewmaterialfinancial.Refresh();
			txtINSCONrelativefullnam.Text = "";
			txtINSCONrelativemobileno.Text = "";
			txtINSCONrelativepanno.Text = "";
			txtINSCONrelativedematacno.Text = "";
			txtINSCONrelativerelationship.Text = "";
			txtINSCONrelativeaddress.Text = "";
			txtINSCONfinancialfullname.Text = "";
			txtINSCONfinancialmobileno.Text = "";
			txtINSCONfinancialpanno.Text = "";
			txtINSCONfinancialdematacno.Text = "";
			txtINSCONfinancialrelationship.Text = "";
			txtINSCONfinancialaddress.Text = "";
		}

		private void btncacncelINSCON_Click(object sender, EventArgs e)
		{
			Clear();
			txtINSCONconnectperson.Text = "";
			btnupdateINSCON.Visible = false;
			btnaddINSCONdeelete.Visible = false;
			btncacncelINSCON.Visible = false;
			btnaddINSCON.Visible = true;
			FillConnectPersonID();
		}

		private void btnupdateINSCON_Click(object sender, EventArgs e)
		{
			if (MasterClass.GETISTI() == "TEMP")
			{
				DialogResult dialog = MessageBox.Show("Date & Time is Tempered.\nPlease Check your Date & Time Settings.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Login l = new Login();
				SESSIONKEYS.UserID = "";
				SESSIONKEYS.Role = "";
				SESSIONKEYS.FullName = "";
				lg.CURRVALUE = "LOG OUT";
				lg.DESCRIPTION = "LOG OUT SUCCESSFULLY";
				lg.TYPE = "SELECTED";
				lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
				lg.ID = SESSIONKEYS.UserID.ToString();
				string json = new MasterClass().SAVE_LOG(lg);
				l.Show();
				Hide();
			}
			else if (txtINSCONconnectperson.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Connect Person ID.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (txtINSCONnameofemployee.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Name of Employee.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (txtINSCONpannomaster.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Pan No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (dataGridViewPhonemobile.Rows.Count <= 0)
			{
				DialogResult dialog = MessageBox.Show("Enter Phone No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				DataSet ds1 = new MasterClass().getDataSet("select PANNO from T_INS_PER WHERE ACTIVE = 'Y'");
				DataSet ds2 = new MasterClass().getDataSet("select PANNO from T_INS_PRO WHERE ACTIVE = 'Y'");
				List<string> termsList = new List<string>();
				string[] b = { };
				if (PANO != txtINSCONpannomaster.Text)
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
				if (b.Contains(txtINSCONpannomaster.Text))
				{
					DialogResult dialog = MessageBox.Show("Pan No Already Exists in our Database.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				else
				{
					T_INS_PER CNS = new T_INS_PER
					{
						CONNECT_PERSON_ID = txtINSCONconnectperson.Text,
						NAME_OF_EMP = txtINSCONnameofemployee.Text,
						ADDRESS = txtINSCONaddressmaster.Text,
						PAN_NO = txtINSCONpannomaster.Text,
						CURRENT_DESIGNATION = txtINSCONcurrentdesigantion.Text
					};
					string PhoneNo = "";
					foreach (DataGridViewRow row in dataGridViewPhonemobile.Rows)
					{
						if (row.Index == 0)
						{
							PhoneNo += row.Cells["PhoneNo"].Value.ToString();
						}
						else
						{
							PhoneNo += "|" + row.Cells["PhoneNo"].Value.ToString();
						}

					}
					CNS.MOBILE_NO = PhoneNo;

					string DematAcNo = "";
					foreach (DataGridViewRow row in dataGridViewDematAcoount.Rows)
					{
						if (row.Index == 0)
						{
							DematAcNo += row.Cells["datagridviewdematacno"].Value.ToString();
						}
						else
						{
							DematAcNo += "|" + row.Cells["datagridviewdematacno"].Value.ToString();
						}
					}
					CNS.DEMAT_AC_NO = DematAcNo;

					string Graduation = "";
					foreach (DataGridViewRow row in dataGridViewGraduationInstitution.Rows)
					{
						if (row.Index == 0)
						{
							Graduation += row.Cells["graduationinstiytuiondatagridview"].Value.ToString();
						}
						else
						{
							Graduation += "|" + row.Cells["graduationinstiytuiondatagridview"].Value.ToString();
						}
					}
					CNS.GRADUATION_INSTITUTIONS = Graduation;

					string PastEmployee = "";
					foreach (DataGridViewRow row in dataGridViewPastEmployee.Rows)
					{
						if (row.Index == 0)
						{
							PastEmployee += row.Cells["pastemployeegridview"].Value.ToString();
						}
						else
						{
							PastEmployee += "|" + row.Cells["pastemployeegridview"].Value.ToString();
						}
					}
					CNS.PAST_EMPLOYEES = PastEmployee;

					List<T_INS_PER_IMMEDAITE_RELATIVES> T_INS_PER_IMMEDAITE_RELATIVES = new List<T_INS_PER_IMMEDAITE_RELATIVES>();
					foreach (DataGridViewRow row in dataGridViewISNCONrelativetable.Rows)
					{
						T_INS_PER_IMMEDAITE_RELATIVES RELATIVEARRAY = new T_INS_PER_IMMEDAITE_RELATIVES
						{
							NAME = row.Cells["FullName"].Value.ToString(),
							MOBILE_NO = row.Cells["MobileNo"].Value.ToString(),
							PAN_NO = row.Cells["PANNo"].Value.ToString(),
							DEMAT_AC_NO = row.Cells["DematAcNo"].Value.ToString(),
							RELATIONSHIP = row.Cells["Relationship"].Value.ToString(),
							ADDRESS = row.Cells["Address"].Value.ToString()
						};

						T_INS_PER_IMMEDAITE_RELATIVES.Add(RELATIVEARRAY);
					}
					CNS.RELATIVEARRAY = T_INS_PER_IMMEDAITE_RELATIVES.ToArray();

					List<T_INS_PER_MATERIAL_FINANCIAL> T_INS_PER_MATERIAL_FINANCIAL = new List<T_INS_PER_MATERIAL_FINANCIAL>();

					foreach (DataGridViewRow row in dataGridViewmaterialfinancial.Rows)
					{
						T_INS_PER_MATERIAL_FINANCIAL FINANCIALARRAY = new T_INS_PER_MATERIAL_FINANCIAL
						{
							NAME = row.Cells["financilaFullName"].Value.ToString(),
							MOBILE_NO = row.Cells["financilaMobileNo"].Value.ToString(),
							PAN_NO = row.Cells["FinancialPanno"].Value.ToString(),
							DEMAT_AC_NO = row.Cells["FinancialDemaAcno"].Value.ToString(),
							RELATIONSHIP = row.Cells["FinancialRelationship"].Value.ToString(),
							ADDRESS = row.Cells["Financialaddress"].Value.ToString()
						};
						T_INS_PER_MATERIAL_FINANCIAL.Add(FINANCIALARRAY);
					}
					CNS.FINANCIALARRAY = T_INS_PER_MATERIAL_FINANCIAL.ToArray();
					CNS.ENTEREDBY = SESSIONKEYS.UserID.ToString();

					string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_PER SET EMPNAME = '" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "',CURRDESIGNATION = '" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "',ADDRESS = '" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "',PANNO = '" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "',DEMATACNO = '" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "',MOBILENO = '" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "',GRADUATIONINSTI = '" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "',PASTEMP = '" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + MasterClass.GETIST() + "' WHERE ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "' ; ").ToString();
					new MasterClass().executeQuery("DELETE T_INS_PER_DT	WHERE PERID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "' AND ACTIVE = 'Y'");
					string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,PANNO,DEMATACNO,MOBILENO,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + CryptographyHelper.Encrypt(CNS.CONNECT_PERSON_ID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();
					lg.CURRVALUE = "CONNECTED PERSON TAB";
					lg.TYPE = "UPDATED";
					lg.ID = perlogid;
					lg.DESCRIPTION = "UPDATED VALUE :- " + CNS.CONNECT_PERSON_ID;
					new MasterClass().SAVE_LOG(lg);
					if (Convert.ToInt32(ds) > 0)
					{
						foreach (T_INS_PER_MATERIAL_FINANCIAL m_x in CNS.FINANCIALARRAY)
						{
							string dss = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT(PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK)  VALUES ('" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','Y','N')").ToString();
							string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dss + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();
							lg.CURRVALUE = "CONNECTED PERSON TAB FINANCIAL RELATIONSHIP";
							lg.TYPE = "UPDATED";
							lg.ID = perlogisdt;
							lg.DESCRIPTION = "UPDATED VALUE :- " + CNS.CONNECT_PERSON_ID;
							new MasterClass().SAVE_LOG(lg);
						}

						foreach (T_INS_PER_IMMEDAITE_RELATIVES m_x in CNS.RELATIVEARRAY)
						{
							string dss = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT(PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK)  VALUES ('" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','Y','N')").ToString();
							string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dss + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();
							lg.CURRVALUE = "CONNECTED PERSON TAB RELATIVE RELATIONSHIP";
							lg.TYPE = "UPDATED";
							lg.ID = perlogisdt;
							lg.DESCRIPTION = "UPDATED VALUE :- " + CNS.CONNECT_PERSON_ID;
							new MasterClass().SAVE_LOG(lg);
						}
						DialogResult dialog = MessageBox.Show("Updated Data Successfully.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					Clear();
					txtINSCONconnectperson.Text = "";
					btnupdateINSCON.Visible = false;
					btnaddINSCONdeelete.Visible = false;
					btncacncelINSCON.Visible = false;
					btnaddINSCON.Visible = true;
					FillConnectPersonID();
				}
			}
		}

		private void btnaddINSCONdeelete_Click(object sender, EventArgs e)
		{
			if (MasterClass.GETISTI() == "TEMP")
			{
				DialogResult dialog = MessageBox.Show("Date & Time is Tempered.\nPlease Check your Date & Time Settings.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Login l = new Login();
				SESSIONKEYS.UserID = "";
				SESSIONKEYS.Role = "";
				SESSIONKEYS.FullName = "";
				lg.CURRVALUE = "LOG OUT";
				lg.DESCRIPTION = "LOG OUT SUCCESSFULLY";
				lg.TYPE = "SELECTED";
				lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
				lg.ID = SESSIONKEYS.UserID.ToString();
				string json = new MasterClass().SAVE_LOG(lg);
				l.Show();
				Hide();
			}
			else
			{
				T_INS_PER CNS = new T_INS_PER
				{
					CONNECT_PERSON_ID = txtINSCONconnectperson.Text,
					NAME_OF_EMP = txtINSCONnameofemployee.Text,
					ADDRESS = txtINSCONaddressmaster.Text,
					PAN_NO = txtINSCONpannomaster.Text,
					CURRENT_DESIGNATION = txtINSCONcurrentdesigantion.Text
				};
				string PhoneNo = "";
				foreach (DataGridViewRow row in dataGridViewPhonemobile.Rows)
				{
					if (row.Index == 0)
					{
						PhoneNo += row.Cells["PhoneNo"].Value.ToString();
					}
					else
					{
						PhoneNo += "|" + row.Cells["PhoneNo"].Value.ToString();
					}

				}
				CNS.MOBILE_NO = PhoneNo;

				string DematAcNo = "";
				foreach (DataGridViewRow row in dataGridViewDematAcoount.Rows)
				{
					if (row.Index == 0)
					{
						DematAcNo += row.Cells["datagridviewdematacno"].Value.ToString();
					}
					else
					{
						DematAcNo += "|" + row.Cells["datagridviewdematacno"].Value.ToString();
					}
				}
				CNS.DEMAT_AC_NO = DematAcNo;

				string Graduation = "";
				foreach (DataGridViewRow row in dataGridViewGraduationInstitution.Rows)
				{
					if (row.Index == 0)
					{
						Graduation += row.Cells["graduationinstiytuiondatagridview"].Value.ToString();
					}
					else
					{
						Graduation += "|" + row.Cells["graduationinstiytuiondatagridview"].Value.ToString();
					}
				}
				CNS.GRADUATION_INSTITUTIONS = Graduation;

				string PastEmployee = "";
				foreach (DataGridViewRow row in dataGridViewPastEmployee.Rows)
				{
					if (row.Index == 0)
					{
						PastEmployee += row.Cells["pastemployeegridview"].Value.ToString();
					}
					else
					{
						PastEmployee += "|" + row.Cells["pastemployeegridview"].Value.ToString();
					}
				}
				CNS.PAST_EMPLOYEES = PastEmployee;

				List<T_INS_PER_IMMEDAITE_RELATIVES> T_INS_PER_IMMEDAITE_RELATIVES = new List<T_INS_PER_IMMEDAITE_RELATIVES>();
				foreach (DataGridViewRow row in dataGridViewISNCONrelativetable.Rows)
				{
					T_INS_PER_IMMEDAITE_RELATIVES RELATIVEARRAY = new T_INS_PER_IMMEDAITE_RELATIVES
					{
						NAME = row.Cells["FullName"].Value.ToString(),
						MOBILE_NO = row.Cells["MobileNo"].Value.ToString(),
						PAN_NO = row.Cells["PANNo"].Value.ToString(),
						DEMAT_AC_NO = row.Cells["DematAcNo"].Value.ToString(),
						RELATIONSHIP = row.Cells["Relationship"].Value.ToString(),
						ADDRESS = row.Cells["Address"].Value.ToString()
					};

					T_INS_PER_IMMEDAITE_RELATIVES.Add(RELATIVEARRAY);
				}
				CNS.RELATIVEARRAY = T_INS_PER_IMMEDAITE_RELATIVES.ToArray();

				List<T_INS_PER_MATERIAL_FINANCIAL> T_INS_PER_MATERIAL_FINANCIAL = new List<T_INS_PER_MATERIAL_FINANCIAL>();

				foreach (DataGridViewRow row in dataGridViewmaterialfinancial.Rows)
				{
					T_INS_PER_MATERIAL_FINANCIAL FINANCIALARRAY = new T_INS_PER_MATERIAL_FINANCIAL
					{
						NAME = row.Cells["financilaFullName"].Value.ToString(),
						MOBILE_NO = row.Cells["financilaMobileNo"].Value.ToString(),
						PAN_NO = row.Cells["FinancialPanno"].Value.ToString(),
						DEMAT_AC_NO = row.Cells["FinancialDemaAcno"].Value.ToString(),
						RELATIONSHIP = row.Cells["FinancialRelationship"].Value.ToString(),
						ADDRESS = row.Cells["Financialaddress"].Value.ToString()
					};
					T_INS_PER_MATERIAL_FINANCIAL.Add(FINANCIALARRAY);
				}
				CNS.FINANCIALARRAY = T_INS_PER_MATERIAL_FINANCIAL.ToArray();
				CNS.ENTEREDBY = SESSIONKEYS.UserID.ToString();

				DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_PER SET ACTIVE = 'N',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + MasterClass.GETIST() + "' WHERE ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "'").ToString();
					string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,PANNO,DEMATACNO,MOBILENO,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + CryptographyHelper.Encrypt(CNS.CONNECT_PERSON_ID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("DELETED") + "','Y','N')").ToString();
					lg.CURRVALUE = "CONNECTED PERSON TAB";
					lg.TYPE = "DELETED";
					lg.ID = perlogid;
					lg.DESCRIPTION = "DELETED VALUE :- " + CNS.CONNECT_PERSON_ID;
					new MasterClass().SAVE_LOG(lg);


					string dsS = new MasterClass().executeQueryForDB("UPDATE T_INS_PER_DT SET ACTIVE = 'N',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + MasterClass.GETIST() + "' WHERE PERID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "'").ToString();
					foreach (T_INS_PER_MATERIAL_FINANCIAL m_x in CNS.FINANCIALARRAY)
					{
						string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("DELETED") + "','Y','N')").ToString();
						lg.CURRVALUE = "CONNECTED PERSON TAB FINANCIAL RELATIONSHIP";
						lg.TYPE = "DELETED";
						lg.ID = perlogisdt;
						lg.DESCRIPTION = "DELETED VALUE :- " + CNS.CONNECT_PERSON_ID;
						new MasterClass().SAVE_LOG(lg);
					}

					foreach (T_INS_PER_IMMEDAITE_RELATIVES m_x in CNS.RELATIVEARRAY)
					{
						string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dsS + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("DELETED") + "','Y','N')").ToString();
						lg.CURRVALUE = "CONNECTED PERSON TAB RELATIVE RELATIONSHIP";
						lg.TYPE = "DELETED";
						lg.ID = perlogisdt;
						lg.DESCRIPTION = "DELETED VALUE :- " + CNS.CONNECT_PERSON_ID;
						new MasterClass().SAVE_LOG(lg);
					}
					if (Convert.ToInt32(ds) > 0)
					{
						DialogResult dialog = MessageBox.Show("Deleted Successfully.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
						Clear();
						txtINSCONconnectperson.Text = "";
						btnupdateINSCON.Visible = false;
						btnaddINSCONdeelete.Visible = false;
						btncacncelINSCON.Visible = false;
						btnaddINSCON.Visible = true;
						FillConnectPersonID();
					}
					else
					{
						DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
		}

		private void button1_Click_2(object sender, EventArgs e)
		{
			HOMEPAGE H = new HOMEPAGE();
			H.Show();
			Hide();
		}

		#endregion


	}
}
