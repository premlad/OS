using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace OS
{
	public partial class MASTER_FOR_RECORDING_INSIDER_CONNECTED_PERSON : MASTERFORM
	{
		private AUDITLOG lg = new AUDITLOG();
		private string PANO = "";
		private string[] relative;
		private string[] financial;
		private MasterClass mc = new MasterClass();

		public MASTER_FOR_RECORDING_INSIDER_CONNECTED_PERSON()
		{
			InitializeComponent();
		}

		private void MASTER_FOR_RECORDING_INSIDER_CONNECTED_PERSON_Load(object sender, EventArgs e)
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
			dataGridViewPhonemobile.DefaultCellStyle.ForeColor = Color.Black;
			dataGridViewDematAcoount.DefaultCellStyle.ForeColor = Color.Black;
			dataGridViewGraduationInstitution.DefaultCellStyle.ForeColor = Color.Black;
			dataGridViewPastEmployee.DefaultCellStyle.ForeColor = Color.Black;
			dataGridViewmaterialfinancial.DefaultCellStyle.ForeColor = Color.Black;
			dataGridViewISNCONrelativetable.DefaultCellStyle.ForeColor = Color.Black;
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
			try
			{
				cmdINSCONSAVEID.Items.Clear();
				DataSet ds = new MasterClass().getDataSet("SELECT ID,CONNECTPERSONID FROM T_INS_PER");
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
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			else if (!new MasterClass().IsValidDematAcno(txtINSCONdemataccountno.Text))
			{
				DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			try
			{
				DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					dataGridViewPhonemobile.Rows.RemoveAt(e.RowIndex);
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void dataGridViewDematAcoount_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					dataGridViewDematAcoount.Rows.RemoveAt(e.RowIndex);
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void dataGridViewGraduationInstitution_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					dataGridViewGraduationInstitution.Rows.RemoveAt(e.RowIndex);
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void dataGridViewPastEmployee_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					dataGridViewPastEmployee.Rows.RemoveAt(e.RowIndex);
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			if (txtINSCONrelativefullnam.Text == "" && txtINSCONrelativemobileno.Text == "" && txtINSCONrelativepanno.Text == "" && txtINSCONrelativedematacno.Text == "" && txtINSCONrelativerelationship.Text == "" && txtINSCONrelativeaddress.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Values.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			//else if (!new MasterClass().IsValidDematAcno(txtINSCONdemataccountno.Text))
			//{
			//	DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//}
			//else if (!new MasterClass().IsValidPanno(txtINSCONrelativepanno.Text))
			//{
			//	DialogResult dialog = MessageBox.Show("Enter Proper PAN No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//}
			else
			{
				if (txtINSCONrelativepanno.Text != "")
				{
					if (!new MasterClass().IsValidPanno(txtINSCONrelativepanno.Text))
					{
						DialogResult dialog = MessageBox.Show("Enter Proper PAN No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (txtINSCONrelativedematacno.Text != "")
					{
						if (!new MasterClass().IsValidDematAcno(txtINSCONrelativedematacno.Text))
						{
							DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							string[] row = { txtINSCONrelativefullnam.Text, txtINSCONrelativemobileno.Text, txtINSCONrelativepanno.Text, txtINSCONrelativedematacno.Text, txtINSCONrelativerelationship.Text, txtINSCONrelativeaddress.Text, "" };
							dataGridViewISNCONrelativetable.Rows.Add(row);
							txtINSCONrelativefullnam.Text = "";
							txtINSCONrelativemobileno.Text = "";
							txtINSCONrelativepanno.Text = "";
							txtINSCONrelativedematacno.Text = "";
							txtINSCONrelativerelationship.Text = "";
							txtINSCONrelativeaddress.Text = "";
						}
					}
					else
					{
						string[] row = { txtINSCONrelativefullnam.Text, txtINSCONrelativemobileno.Text, txtINSCONrelativepanno.Text, txtINSCONrelativedematacno.Text, txtINSCONrelativerelationship.Text, txtINSCONrelativeaddress.Text, "" };
						dataGridViewISNCONrelativetable.Rows.Add(row);
						txtINSCONrelativefullnam.Text = "";
						txtINSCONrelativemobileno.Text = "";
						txtINSCONrelativepanno.Text = "";
						txtINSCONrelativedematacno.Text = "";
						txtINSCONrelativerelationship.Text = "";
						txtINSCONrelativeaddress.Text = "";
					}
				}
				else if (txtINSCONrelativedematacno.Text != "")
				{
					if (!new MasterClass().IsValidDematAcno(txtINSCONrelativedematacno.Text))
					{
						DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						string[] row = { txtINSCONrelativefullnam.Text, txtINSCONrelativemobileno.Text, txtINSCONrelativepanno.Text, txtINSCONrelativedematacno.Text, txtINSCONrelativerelationship.Text, txtINSCONrelativeaddress.Text, "" };
						dataGridViewISNCONrelativetable.Rows.Add(row);
						txtINSCONrelativefullnam.Text = "";
						txtINSCONrelativemobileno.Text = "";
						txtINSCONrelativepanno.Text = "";
						txtINSCONrelativedematacno.Text = "";
						txtINSCONrelativerelationship.Text = "";
						txtINSCONrelativeaddress.Text = "";
					}
				}
				else
				{
					string[] row = { txtINSCONrelativefullnam.Text, txtINSCONrelativemobileno.Text, txtINSCONrelativepanno.Text, txtINSCONrelativedematacno.Text, txtINSCONrelativerelationship.Text, txtINSCONrelativeaddress.Text, "" };
					dataGridViewISNCONrelativetable.Rows.Add(row);
					txtINSCONrelativefullnam.Text = "";
					txtINSCONrelativemobileno.Text = "";
					txtINSCONrelativepanno.Text = "";
					txtINSCONrelativedematacno.Text = "";
					txtINSCONrelativerelationship.Text = "";
					txtINSCONrelativeaddress.Text = "";
				}
			}
		}

		private void btnmaterial_Click(object sender, EventArgs e)
		{
			if (txtINSCONfinancialfullname.Text == "" && txtINSCONfinancialmobileno.Text == "" && txtINSCONfinancialpanno.Text == "" && txtINSCONfinancialdematacno.Text == "" && txtINSCONfinancialrelationship.Text == "" && txtINSCONfinancialaddress.Text == "")
			{
				DialogResult dialog = MessageBox.Show("Enter Values.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			//else if (!new MasterClass().IsValidDematAcno(txtINSCONdemataccountno.Text))
			//{
			//	DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//}
			//else if (!new MasterClass().IsValidPanno(txtINSCONfinancialpanno.Text))
			//{
			//	DialogResult dialog = MessageBox.Show("Enter Proper PAN No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//}
			else
			{
				if (txtINSCONfinancialpanno.Text != "")
				{
					if (!new MasterClass().IsValidPanno(txtINSCONfinancialpanno.Text))
					{
						DialogResult dialog = MessageBox.Show("Enter Proper PAN No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (txtINSCONfinancialdematacno.Text != "")
					{
						if (!new MasterClass().IsValidDematAcno(txtINSCONfinancialdematacno.Text))
						{
							DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							string[] row = { txtINSCONfinancialfullname.Text, txtINSCONfinancialmobileno.Text, txtINSCONfinancialpanno.Text, txtINSCONfinancialdematacno.Text, txtINSCONfinancialrelationship.Text, txtINSCONfinancialaddress.Text, "" };
							dataGridViewmaterialfinancial.Rows.Add(row);
							txtINSCONfinancialfullname.Text = "";
							txtINSCONfinancialmobileno.Text = "";
							txtINSCONfinancialpanno.Text = "";
							txtINSCONfinancialdematacno.Text = "";
							txtINSCONfinancialrelationship.Text = "";
							txtINSCONfinancialaddress.Text = "";
						}
					}
					else
					{
						string[] row = { txtINSCONfinancialfullname.Text, txtINSCONfinancialmobileno.Text, txtINSCONfinancialpanno.Text, txtINSCONfinancialdematacno.Text, txtINSCONfinancialrelationship.Text, txtINSCONfinancialaddress.Text, "" };
						dataGridViewmaterialfinancial.Rows.Add(row);
						txtINSCONfinancialfullname.Text = "";
						txtINSCONfinancialmobileno.Text = "";
						txtINSCONfinancialpanno.Text = "";
						txtINSCONfinancialdematacno.Text = "";
						txtINSCONfinancialrelationship.Text = "";
						txtINSCONfinancialaddress.Text = "";
					}
				}
				else if (txtINSCONfinancialdematacno.Text != "")
				{
					if (!new MasterClass().IsValidDematAcno(txtINSCONfinancialdematacno.Text))
					{
						DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						string[] row = { txtINSCONfinancialfullname.Text, txtINSCONfinancialmobileno.Text, txtINSCONfinancialpanno.Text, txtINSCONfinancialdematacno.Text, txtINSCONfinancialrelationship.Text, txtINSCONfinancialaddress.Text, "" };
						dataGridViewmaterialfinancial.Rows.Add(row);
						txtINSCONfinancialfullname.Text = "";
						txtINSCONfinancialmobileno.Text = "";
						txtINSCONfinancialpanno.Text = "";
						txtINSCONfinancialdematacno.Text = "";
						txtINSCONfinancialrelationship.Text = "";
						txtINSCONfinancialaddress.Text = "";
					}
				}
				else
				{
					string[] row = { txtINSCONfinancialfullname.Text, txtINSCONfinancialmobileno.Text, txtINSCONfinancialpanno.Text, txtINSCONfinancialdematacno.Text, txtINSCONfinancialrelationship.Text, txtINSCONfinancialaddress.Text, "" };
					dataGridViewmaterialfinancial.Rows.Add(row);
					txtINSCONfinancialfullname.Text = "";
					txtINSCONfinancialmobileno.Text = "";
					txtINSCONfinancialpanno.Text = "";
					txtINSCONfinancialdematacno.Text = "";
					txtINSCONfinancialrelationship.Text = "";
					txtINSCONfinancialaddress.Text = "";
				}
			}
		}

		private void dataGridViewmaterialfinancial_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					dataGridViewmaterialfinancial.Rows.RemoveAt(e.RowIndex);
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void dataGridViewISNCONrelativetable_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					dataGridViewISNCONrelativetable.Rows.RemoveAt(e.RowIndex);
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				int error = 0;
				SetLoading(true);

				Thread.Sleep(2000);
				//Invoke((MethodInvoker)delegate
				//{
				//if (txtINSCONconnectperson.Text == "")
				//{
				//	DialogResult dialog = MessageBox.Show("Enter Connect Person ID.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//}
				//else 
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
				else if (txtINSCONnameofemployee.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Name of Employee.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (txtINSCONaddressmaster.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Permanent Address.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (permanentaddress.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Correspondence Address.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (!new MasterClass().IsValidEmail(txtEmailemp.Text))
				{
					DialogResult dialog = MessageBox.Show("Please Enter Email in Proper Format.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (dataGridViewPhonemobile.Rows.Count <= 0)
				{
					DialogResult dialog = MessageBox.Show("Enter Landline/Mobile No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					if (txtINSCONpannomaster.Text == "")
					{
						if (txtotheridentifier.Text == "")
						{
							DialogResult dialog = MessageBox.Show("Enter Other Identifier No or Pan No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
							error++;
						}
					}
					else
					{
						if (!new MasterClass().IsValidPanno(txtINSCONpannomaster.Text))
						{
							DialogResult dialog = MessageBox.Show("Enter Proper PAN No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
							error++;
						}
					}

					DataSet ds1 = new MasterClass().getDataSet("select PANNO from T_INS_PER WHERE ACTIVE = 'Y'");
					DataSet ds2 = new MasterClass().getDataSet("select PANNO from T_INS_PRO WHERE ACTIVE = 'Y'");
					List<string> termsList = new List<string>();
					string[] b = { };
					if (ds1.Tables[0].Rows.Count > 0)
					{
						for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
						{
							string a = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["PANNO"].ToString());
							if (a.Trim() != "")
							{
								termsList.Add(a);
							}
						}
						b = termsList.ToArray();
					}
					if (ds2.Tables[0].Rows.Count > 0)
					{
						for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
						{
							string a = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["PANNO"].ToString());
							if (a.Trim() != "")
							{
								termsList.Add(a);
							}
						}
						b = termsList.ToArray();
					}
					if (b.Contains(txtINSCONpannomaster.Text))
					{
						DialogResult dialog = MessageBox.Show("Pan No Already Exists in our Database.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
						error++;
					}
					else
					{

						T_INS_PER CNS = new T_INS_PER
						{
							CONNECT_PERSON_ID = txtINSCONconnectperson.Text,
							NAME_OF_EMP = txtINSCONnameofemployee.Text,
							ADDRESS = txtINSCONaddressmaster.Text,
							PAN_NO = txtINSCONpannomaster.Text,
							CURRENT_DESIGNATION = txtINSCONcurrentdesigantion.Text,
							OTHERIDENTIFIER = txtotheridentifier.Text,
							RESIADDRESS = permanentaddress.Text,
							EMAILID = txtEmailemp.Text
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
							if (!new MasterClass().IsValidDematAcno(row.Cells["datagridviewdematacno"].Value.ToString()))
							{
								DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error++;
							}
							else
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


						CNS.ENTEREDBY = SESSIONKEYS.UserID.ToString();

						if (error == 0)
						{
							string CPID = "CP" + new MasterClass().GETCPID();
							string ds = new MasterClass().executeQuery("INSERT INTO T_INS_PER (CONNECTPERSONID, EMPNAME, CURRDESIGNATION, ADDRESS, RESIADDRESS, PANNO, OTHERIDENTIFIER, DEMATACNO, MOBILENO, EMAILID, GRADUATIONINSTI, PASTEMP, ENTEREDBY, ENTEREDON, ACTIVE, LOCK) VALUES ('" + CryptographyHelper.Encrypt(CPID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.EMAILID) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N')").ToString();
							string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,RESIADDRESS,PANNO,OTHERIDENTIFIER,DEMATACNO,MOBILENO,EMAILID,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ds + "','" + CryptographyHelper.Encrypt(CPID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.EMAILID) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N')").ToString();
							lg.CURRVALUE = "CONNECTED PERSON TAB";
							lg.TYPE = "INSERTED";
							lg.ID = perlogid;
							lg.DESCRIPTION = "INSERTED VALUE :- " + CPID;
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							//lg.ID = SESSIONKEYS.UserID.ToString();
							new MasterClass().SAVE_LOG(lg);
							if (Convert.ToInt32(ds) > 0)
							{
								lblidforcp.Text = Convert.ToString(ds);
								DialogResult dialogResult = MessageBox.Show("Connected Person Data Saved Successfully. Do You want to Add Relative & Financial Data?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
								if (dialogResult == DialogResult.Yes)
								{
									groupBox2.Enabled = true;
									groupBox3.Enabled = true;
									groupBox3.Enabled = true;
									groupBox3.Enabled = true;
									btnaddrelafinaci.Enabled = true;
									btnaddINSCON.Enabled = false;
								}
								else
								{
									Clear();
									btnupdateINSCON.Visible = false;
									btnaddINSCONdeelete.Visible = false;
									btncacncelINSCON.Visible = false;
									btnaddINSCON.Visible = true;
									FillConnectPersonID();
									button2.PerformClick();
								}
							}
							else
							{
								DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
				DialogResult dialog = MessageBox.Show("Data Not Saved. Please Check Your Internet Connection.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtINSCONconnectperson_Leave(object sender, EventArgs e)
		{
			try
			{
				int val = 0;
				if (txtINSCONconnectperson.Text == "")
				{

				}
				else
				{
					for (int i = 0; i < cmdINSCONSAVEID.Items.Count; i++)
					{
						if (txtINSCONconnectperson.Text == cmdINSCONSAVEID.GetItemText(cmdINSCONSAVEID.Items[i]))
						{
							cmdINSCONSAVEID.SelectedIndex = i;
							DataTable a1 = new MasterClass().getDataTable("SELECT * FROM T_INS_PER WHERE ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID + "';");
							DataTable b1 = new MasterClass().getDataTable("SELECT * FROM T_INS_PER_DT WHERE PERID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID + "';");
							DataSet ds = new DataSet();
							ds.Tables.Add(a1);
							ds.Tables.Add(b1);


							if (ds.Tables[0].Rows.Count > 0)
							{
								lblidforcp.Text = ds.Tables[0].Rows[0]["ID"].ToString();
								txtINSCONnameofemployee.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["EMPNAME"].ToString());
								txtINSCONpannomaster.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PANNO"].ToString());
								PANO = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PANNO"].ToString());
								txtINSCONcurrentdesigantion.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["CURRDESIGNATION"].ToString());
								txtINSCONaddressmaster.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["ADDRESS"].ToString());
								permanentaddress.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RESIADDRESS"].ToString());
								txtotheridentifier.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["OTHERIDENTIFIER"].ToString());
								txtEmailemp.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["EMAILID"].ToString());

								if (permanentaddress.Text == txtINSCONaddressmaster.Text)
								{
									checkBox1.Checked = true;
									permanentaddress.Enabled = false;
								}
								else
								{
									checkBox1.Checked = false;
									permanentaddress.Enabled = true;
								}
								permanentaddress.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RESIADDRESS"].ToString());
								dataGridViewPhonemobile.Rows.Clear();
								dataGridViewPhonemobile.Refresh();

								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["MOBILENO"].ToString()).Contains("|"))
								{
									string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["MOBILENO"].ToString()).Split('|');
									for (int j = 0; j < abc.Length; j++)
									{
										if (abc[j] != "" && abc[j] != null)
										{
											dataGridViewPhonemobile.Rows.Add(abc[j]);
										}
									}
								}
								else
								{
									string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["MOBILENO"].ToString()) };
									if (row[0] != "" && row[0] != null)
									{
										dataGridViewPhonemobile.Rows.Add(row);
									}
								}
								txtMobileINSCONNumber.Text = "";

								dataGridViewDematAcoount.Rows.Clear();
								dataGridViewDematAcoount.Refresh();
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["DEMATACNO"].ToString()).Contains("|"))
								{
									string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["DEMATACNO"].ToString()).Split('|');
									for (int j = 0; j < abc.Length; j++)
									{
										if (abc[j] != "" && abc[j] != null)
										{
											dataGridViewDematAcoount.Rows.Add(abc[j]);
										}
									}
								}
								else
								{
									string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["DEMATACNO"].ToString()) };
									if (row[0] != "" && row[0] != null)
									{
										dataGridViewDematAcoount.Rows.Add(row);
									}
								}
								txtINSCONdemataccountno.Text = "";

								dataGridViewGraduationInstitution.Rows.Clear();
								dataGridViewGraduationInstitution.Refresh();
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["GRADUATIONINSTI"].ToString()).Contains("|"))
								{
									string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["GRADUATIONINSTI"].ToString()).Split('|');
									for (int j = 0; j < abc.Length; j++)
									{
										if (abc[j] != "" && abc[j] != null)
										{
											dataGridViewGraduationInstitution.Rows.Add(abc[j]);
										}
									}
								}
								else
								{
									string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["GRADUATIONINSTI"].ToString()) };
									if (row[0] != "" && row[0] != null)
									{
										dataGridViewGraduationInstitution.Rows.Add(row);
									}
								}
								txtINSCONgraduationinstitution.Text = "";

								dataGridViewPastEmployee.Rows.Clear();
								dataGridViewPastEmployee.Refresh();
								if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PASTEMP"].ToString()).Contains("|"))
								{
									string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PASTEMP"].ToString()).Split('|');
									for (int j = 0; j < abc.Length; j++)
									{
										if (abc[j] != "" && abc[j] != null)
										{
											dataGridViewPastEmployee.Rows.Add(abc[j]);
										}
									}
								}
								else
								{
									string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PASTEMP"].ToString()) };
									if (row[0] != "" && row[0] != null)
									{
										dataGridViewPastEmployee.Rows.Add(row);
									}
								}
								txtINSCONpastemployee.Text = "";

								dataGridViewISNCONrelativetable.Rows.Clear();
								dataGridViewISNCONrelativetable.Refresh();
								dataGridViewmaterialfinancial.Rows.Clear();
								dataGridViewmaterialfinancial.Refresh();
								List<string> financialList = new List<string>();
								List<string> relativeList = new List<string>();
								for (i = 0; i < ds.Tables[1].Rows.Count; i++)
								{
									string a = CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["TYPE"].ToString());
									if (CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["TYPE"].ToString()) == "FINANCIAL")
									{
										string[] row = { CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["DEMATACNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["RELATIONSHIP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["ADDRESS"].ToString()), ds.Tables[1].Rows[i]["ID"].ToString() };
										dataGridViewmaterialfinancial.Rows.Add(row);
										financialList.Add(ds.Tables[1].Rows[i]["ID"].ToString());
									}
									else
									{
										string[] row = { CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["DEMATACNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["RELATIONSHIP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["ADDRESS"].ToString()), ds.Tables[1].Rows[i]["ID"].ToString() };
										relativeList.Add(ds.Tables[1].Rows[i]["ID"].ToString());
										dataGridViewISNCONrelativetable.Rows.Add(row);
									}
								}
								financial = financialList.ToArray();
								relative = relativeList.ToArray();

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

								btnaddrelafinaci.Visible = false;
								btnupdatefinanrel.Visible = true;
								btnupdatefinanrel.Enabled = true;
								groupBox2.Enabled = true;
								groupBox3.Enabled = true;

								if (ds.Tables[0].Rows[0]["ACTIVE"].ToString().Trim() == "N")
								{
									btnupdateINSCON.Visible = false;
									btnaddINSCONdeelete.Text = "RETREIVE CP";
									btnupdatefinanrel.Visible = false;
									groupBox2.Enabled = false;
									groupBox3.Enabled = false;
									groupBox1.Enabled = false;
								}
								else
								{
									btnupdateINSCON.Enabled = true;
									btnaddINSCONdeelete.Text = "NOMORE CP";
								}
								val++;
								break;
							}
							else
							{
								//Clear();
								//txtINSCONconnectperson.Enabled = true;
								//btnupdateINSCON.Visible = false;
								//btnaddINSCONdeelete.Visible = false;
								//btncacncelINSCON.Visible = false;
								//btnupdatefinanrel.Visible = false;
								//btnaddINSCON.Visible = true;
								//btnaddrelafinaci.Visible = true;
							}
						}
						else
						{
							//Clear();
							//txtINSCONconnectperson.Enabled = true;
							//btnupdateINSCON.Visible = false;
							//btnaddINSCONdeelete.Visible = false;
							//btncacncelINSCON.Visible = false;
							//btnupdatefinanrel.Visible = false;
							//btnaddINSCON.Visible = true;
							//btnaddrelafinaci.Visible = true;
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
			permanentaddress.Text = "";
			txtotheridentifier.Text = "";
			txtEmailemp.Text = "";
			lblidforcp.Text = "";
		}

		private void btncacncelINSCON_Click(object sender, EventArgs e)
		{
			Clear();
			txtINSCONconnectperson.Text = "";
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
				int error = 0;
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
				else if (txtINSCONconnectperson.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Connected Person ID.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (txtINSCONnameofemployee.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Name of Employee.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (txtINSCONaddressmaster.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Permanent Address.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (permanentaddress.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Correspondence Address.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (!new MasterClass().IsValidEmail(txtEmailemp.Text))
				{
					DialogResult dialog = MessageBox.Show("Please Enter Email in Proper Format.", "Insider Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (dataGridViewPhonemobile.Rows.Count <= 0)
				{
					DialogResult dialog = MessageBox.Show("Enter Phone No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					if (txtINSCONpannomaster.Text == "")
					{
						if (txtotheridentifier.Text == "")
						{
							DialogResult dialog = MessageBox.Show("Enter Other Identifier No or Pan No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
							error++;
						}
					}
					else
					{
						if (!new MasterClass().IsValidPanno(txtINSCONpannomaster.Text))
						{
							DialogResult dialog = MessageBox.Show("Enter Proper PAN No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
							error++;
						}
					}

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
								if (a.Trim() != "")
								{
									termsList.Add(a);
								}
							}
							b = termsList.ToArray();
						}
						if (ds2.Tables[0].Rows.Count > 0)
						{
							for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
							{
								string a = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["PANNO"].ToString());
								if (a.Trim() != "")
								{
									termsList.Add(a);
								}
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
							CURRENT_DESIGNATION = txtINSCONcurrentdesigantion.Text,
							OTHERIDENTIFIER = txtotheridentifier.Text,
							RESIADDRESS = permanentaddress.Text,
							EMAILID = txtEmailemp.Text
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
							if (!new MasterClass().IsValidDematAcno(row.Cells["datagridviewdematacno"].Value.ToString()))
							{
								DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error++;
							}
							else
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

						CNS.ENTEREDBY = SESSIONKEYS.UserID.ToString();

						if (error == 0)
						{
							DataSet getval = new MasterClass().getDataSet("SELECT ID FROM T_INS_PER_LOG WHERE ACTIVE = 'Y' AND TID = '" + lblidforcp.Text + "' ORDER BY ENTEREDON DESC");
							DataSet updateUPSI = new MasterClass().getDataSet("SELECT ID,RECIPIENTNAME FROM T_INS_UPSI");

							string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_PER SET EMPNAME = '" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "',CURRDESIGNATION = '" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "',ADDRESS = '" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "',RESIADDRESS = '" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "',PANNO = '" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "',OTHERIDENTIFIER = '" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "',DEMATACNO = '" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "',MOBILENO = '" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "',EMAILID = '" + CryptographyHelper.Encrypt(CNS.EMAILID) + "',GRADUATIONINSTI = '" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "',PASTEMP = '" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE ID = '" + lblidforcp.Text + "' ; ").ToString();
							//new MasterClass().executeQuery("UPDATE T_INS_PER_DT SET ACTIVE = 'N' WHERE PERID = '" + lblidforcp.Text + "' AND ACTIVE = 'Y'");
							//new MasterClass().executeQuery("UPDATE T_INS_PER_DT_LOG SET ACTIVE = 'N' WHERE PERID = '" + lblidforcp.Text + "' AND ACTIVE = 'Y'");
							string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,RESIADDRESS,PANNO,OTHERIDENTIFIER,DEMATACNO,MOBILENO,EMAILID,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(CNS.CONNECT_PERSON_ID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.EMAILID) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();
							if (updateUPSI.Tables[0].Rows.Count > 0)
							{
								for (int i = 0; i < updateUPSI.Tables[0].Rows.Count; i++)
								{
									string id = updateUPSI.Tables[0].Rows[i]["ID"].ToString();
									string[] name = CryptographyHelper.Decrypt(updateUPSI.Tables[0].Rows[i]["RECIPIENTNAME"].ToString().Trim()).Split('-');
									if (CNS.CONNECT_PERSON_ID == name[1].Trim())
									{
										if (CNS.PAN_NO != "")
										{
											string a = CNS.NAME_OF_EMP + " - " + CNS.CONNECT_PERSON_ID;
											new MasterClass().executeQuery("UPDATE T_INS_UPSI SET RECIPIENTNAME = '" + CryptographyHelper.Encrypt(a) + "',ADDRESS='" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "',PANNO = '" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "' WHERE ID = '" + id + "'");
										}
										else
										{
											string a = CNS.NAME_OF_EMP + " - " + CNS.CONNECT_PERSON_ID;
											new MasterClass().executeQuery("UPDATE T_INS_UPSI SET RECIPIENTNAME = '" + CryptographyHelper.Encrypt(a) + "',ADDRESS='" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "',PANNO = '" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "' WHERE ID = '" + id + "'");
										}

									}
								}
							}


							lg.CURRVALUE = "CONNECTED PERSON TAB";
							lg.TYPE = "UPDATED";
							lg.ID = perlogid + "|" + getval.Tables[0].Rows[0]["ID"].ToString();
							lg.DESCRIPTION = "UPDATED VALUE :- " + CNS.CONNECT_PERSON_ID;
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							//lg.ID = SESSIONKEYS.UserID.ToString();
							new MasterClass().SAVE_LOG(lg);
							//Thread.Sleep(1000);
							if (Convert.ToInt32(ds) > 0)
							{
								DialogResult dialogResult = MessageBox.Show("Connected Person Updated Data Successfully. Do You want to Update Relative & Financial Data?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
								if (dialogResult == DialogResult.Yes)
								{
									//groupBox2.Enabled = true;
									//groupBox3.Enabled = true;
								}
								else
								{
									Clear();
									btnupdateINSCON.Visible = false;
									btnaddINSCONdeelete.Visible = false;
									btncacncelINSCON.Visible = false;
									btnaddINSCON.Visible = true;
									FillConnectPersonID();
									button2.PerformClick();
								}

							}
							else
							{
								DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
				DialogResult dialog = MessageBox.Show("Data Not Updated. Please Check Your Internet Connection.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				else if (txtINSCONconnectperson.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Enter Connected Person ID.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					T_INS_PER CNS = new T_INS_PER
					{
						CONNECT_PERSON_ID = txtINSCONconnectperson.Text,
						NAME_OF_EMP = txtINSCONnameofemployee.Text,
						ADDRESS = txtINSCONaddressmaster.Text,
						PAN_NO = txtINSCONpannomaster.Text,
						CURRENT_DESIGNATION = txtINSCONcurrentdesigantion.Text,
						OTHERIDENTIFIER = txtotheridentifier.Text,
						RESIADDRESS = permanentaddress.Text,
						EMAILID = txtEmailemp.Text
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
						if (!new MasterClass().IsValidDematAcno(row.Cells["datagridviewdematacno"].Value.ToString()))
						{
							DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
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
					DialogResult dialogResult;
					if (btnaddINSCONdeelete.Text == "RETREIVE CP")
					{
						dialogResult = MessageBox.Show("Are You Sure You Want to Retreive As CP?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (dialogResult == DialogResult.Yes)
						{
							string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_PER SET ACTIVE = 'Y',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE ID = '" + lblidforcp.Text + "'").ToString();
							//string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,RESIADDRESS,PANNO,OTHERIDENTIFIER,DEMATACNO,MOBILENO,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(CNS.CONNECT_PERSON_ID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("RETREIVE CP") + "','Y','N')").ToString();
							string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,RESIADDRESS,PANNO,OTHERIDENTIFIER,DEMATACNO,MOBILENO,EMAILID,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(CNS.CONNECT_PERSON_ID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.EMAILID) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("RETREIVE CP") + "','Y','N')").ToString();
							lg.CURRVALUE = "CONNECTED PERSON TAB";
							lg.TYPE = "RETREIVE CP";
							lg.ID = perlogid;
							lg.DESCRIPTION = "RETREIVE CP :- " + CNS.CONNECT_PERSON_ID;
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							//lg.ID = SESSIONKEYS.UserID.ToString();
							new MasterClass().SAVE_LOG(lg);


							string dsS = new MasterClass().executeQueryForDB("UPDATE T_INS_PER_DT SET ACTIVE = 'Y',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE PERID = '" + lblidforcp.Text + "'").ToString();
							foreach (T_INS_PER_MATERIAL_FINANCIAL m_x in CNS.FINANCIALARRAY)
							{
								string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + lblidforcp.Text + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("RETREIVE CP") + "','Y','N')").ToString();
								lg.CURRVALUE = "CONNECTED PERSON TAB FINANCIAL RELATIONSHIP";
								lg.TYPE = "RETREIVE CP";
								lg.ID = perlogisdt;
								lg.DESCRIPTION = "RETREIVE CP :- " + CNS.CONNECT_PERSON_ID;
								lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
								//lg.ID = SESSIONKEYS.UserID.ToString();
								new MasterClass().SAVE_LOG(lg);
							}

							foreach (T_INS_PER_IMMEDAITE_RELATIVES m_x in CNS.RELATIVEARRAY)
							{
								string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dsS + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("RETREIVE CP") + "','Y','N')").ToString();
								lg.CURRVALUE = "CONNECTED PERSON TAB RELATIVE RELATIONSHIP";
								lg.TYPE = "RETREIVE CP";
								lg.ID = perlogisdt;
								lg.DESCRIPTION = "RETREIVE CP :- " + CNS.CONNECT_PERSON_ID;
								lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
								//lg.ID = SESSIONKEYS.UserID.ToString();
								new MasterClass().SAVE_LOG(lg);
							}
							if (relative.Length > 0)
							{
								for (int i = 0; i < relative.Length; i++)
								{
									relative = relative.Where(val => val != relative[i]).ToArray();
								}
							}
							if (financial.Length > 0)
							{
								for (int i = 0; i < financial.Length; i++)
								{
									financial = financial.Where(val => val != financial[i]).ToArray();
								}
							}

							if (Convert.ToInt32(ds) > 0)
							{
								DialogResult dialog = MessageBox.Show("Updated Successfully.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
								Clear();
								txtINSCONconnectperson.Text = "";
								btnupdateINSCON.Visible = false;
								btnaddINSCONdeelete.Visible = false;
								btncacncelINSCON.Visible = false;
								btnaddINSCON.Visible = true;
								FillConnectPersonID();
								button2.PerformClick();
							}
							else
							{
								DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
						}
					}
					else
					{
						dialogResult = MessageBox.Show("Are You Sure You Want to Tag As Nomore CP?", "Connected Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

						if (dialogResult == DialogResult.Yes)
						{
							string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_PER SET ACTIVE = 'N',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE ID = '" + lblidforcp.Text + "'").ToString();
							string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,RESIADDRESS,PANNO,OTHERIDENTIFIER,DEMATACNO,MOBILENO,EMAILID,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(CNS.CONNECT_PERSON_ID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.EMAILID) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("NOMORE CP") + "','Y','N')").ToString();
							lg.CURRVALUE = "CONNECTED PERSON TAB";
							lg.TYPE = "NOMORE CP";
							lg.ID = perlogid;
							lg.DESCRIPTION = "NOMORE CP :- " + CNS.CONNECT_PERSON_ID;
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							//lg.ID = SESSIONKEYS.UserID.ToString();
							new MasterClass().SAVE_LOG(lg);


							string dsS = new MasterClass().executeQueryForDB("UPDATE T_INS_PER_DT SET ACTIVE = 'N',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE PERID = '" + lblidforcp.Text + "'").ToString();
							foreach (T_INS_PER_MATERIAL_FINANCIAL m_x in CNS.FINANCIALARRAY)
							{
								string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + lblidforcp.Text + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("NOMORE CP") + "','Y','N')").ToString();
								lg.CURRVALUE = "CONNECTED PERSON TAB FINANCIAL RELATIONSHIP";
								lg.TYPE = "NOMORE CP";
								lg.ID = perlogisdt;
								lg.DESCRIPTION = "NOMORE CP :- " + CNS.CONNECT_PERSON_ID;
								lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
								//lg.ID = SESSIONKEYS.UserID.ToString();
								new MasterClass().SAVE_LOG(lg);
							}

							foreach (T_INS_PER_IMMEDAITE_RELATIVES m_x in CNS.RELATIVEARRAY)
							{
								string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dsS + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("NOMORE CP") + "','Y','N')").ToString();
								lg.CURRVALUE = "CONNECTED PERSON TAB RELATIVE RELATIONSHIP";
								lg.TYPE = "NOMORE CP";
								lg.ID = perlogisdt;
								lg.DESCRIPTION = "NOMORE CP :- " + CNS.CONNECT_PERSON_ID;
								lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
								//lg.ID = SESSIONKEYS.UserID.ToString();
								new MasterClass().SAVE_LOG(lg);
							}
							if (Convert.ToInt32(ds) > 0)
							{
								DialogResult dialog = MessageBox.Show("Updated Successfully.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
								Clear();
								txtINSCONconnectperson.Text = "";
								btnupdateINSCON.Visible = false;
								btnaddINSCONdeelete.Visible = false;
								btncacncelINSCON.Visible = false;
								btnaddINSCON.Visible = true;
								FillConnectPersonID();
								button2.PerformClick();
							}
							else
							{
								DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
						}
					}
					//btnaddINSCONdeelete.Text = "RETREIVE CP";

				}

				//});

				SetLoading(false);
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				SetLoading(false);
				DialogResult dialog = MessageBox.Show("Data Not Saved. Please Check Your Internet Connection.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void button1_Click_2(object sender, EventArgs e)
		{
			HOMEPAGE H = new HOMEPAGE();
			H.Show();
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Clear();
			FillConnectPersonID();
			string CPID = "CP" + new MasterClass().GETCPID();
			txtINSCONconnectperson.Text = CPID;
			txtINSCONconnectperson.Enabled = false;
			btnupdateINSCON.Visible = false;
			btnaddINSCONdeelete.Visible = false;
			btncacncelINSCON.Visible = false;
			btnaddINSCON.Visible = true;
			btnaddINSCON.Enabled = true;
			btnupdatefinanrel.Visible = false;
			btnaddrelafinaci.Visible = true;

			txtINSCONnameofemployee.Enabled = true;
			txtINSCONpannomaster.Enabled = true;
			txtINSCONcurrentdesigantion.Enabled = true;
			txtINSCONaddressmaster.Enabled = true;
			txtINSCONrelativefullnam.Enabled = true;
			txtINSCONrelativemobileno.Enabled = true;
			txtINSCONrelativepanno.Enabled = true;
			txtINSCONrelativedematacno.Enabled = true;
			txtINSCONrelativerelationship.Enabled = true;
			txtINSCONrelativeaddress.Enabled = true;
			txtINSCONfinancialfullname.Enabled = true;
			txtINSCONfinancialmobileno.Enabled = true;
			txtINSCONfinancialpanno.Enabled = true;
			txtINSCONfinancialdematacno.Enabled = true;
			txtINSCONfinancialrelationship.Enabled = true;
			txtINSCONfinancialaddress.Enabled = true;
			permanentaddress.Enabled = true;
			txtotheridentifier.Enabled = true;
			txtMobileINSCONNumber.Enabled = true;
			btnINSCONaddnumber.Enabled = true;
			txtINSCONdemataccountno.Enabled = true;
			btnINSCONadddematacno.Enabled = true;
			txtINSCONgraduationinstitution.Enabled = true;
			txtINSCONpastemployee.Enabled = true;
			btnINSCONpastemployees.Enabled = true;
			btnddrelative.Enabled = true;
			btnmaterial.Enabled = true;
			txtEmailemp.Enabled = true;
			btnINSCONGraduationInstitution.Enabled = true;
			groupBox2.Enabled = false;
			groupBox3.Enabled = false;
			groupBox1.Enabled = true;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Clear();
			FillConnectPersonID();
			txtINSCONconnectperson.Text = "";
			txtINSCONconnectperson.Enabled = true;
			btnupdateINSCON.Visible = true;
			btnaddINSCONdeelete.Visible = true;
			btncacncelINSCON.Visible = true;
			btnaddINSCON.Visible = false;
			btnupdateINSCON.Enabled = false;
			btnaddINSCONdeelete.Enabled = true;
			btncacncelINSCON.Enabled = true;
			btnupdatefinanrel.Visible = true;
			btnaddrelafinaci.Visible = false;

			txtINSCONnameofemployee.Enabled = true;
			txtINSCONpannomaster.Enabled = true;
			txtINSCONcurrentdesigantion.Enabled = true;
			txtINSCONaddressmaster.Enabled = true;
			txtINSCONrelativefullnam.Enabled = true;
			txtINSCONrelativemobileno.Enabled = true;
			txtINSCONrelativepanno.Enabled = true;
			txtINSCONrelativedematacno.Enabled = true;
			txtINSCONrelativerelationship.Enabled = true;
			txtINSCONrelativeaddress.Enabled = true;
			txtINSCONfinancialfullname.Enabled = true;
			txtINSCONfinancialmobileno.Enabled = true;
			txtINSCONfinancialpanno.Enabled = true;
			txtINSCONfinancialdematacno.Enabled = true;
			txtINSCONfinancialrelationship.Enabled = true;
			txtINSCONfinancialaddress.Enabled = true;
			permanentaddress.Enabled = true;
			txtotheridentifier.Enabled = true;
			txtMobileINSCONNumber.Enabled = true;
			btnINSCONaddnumber.Enabled = true;
			txtINSCONdemataccountno.Enabled = true;
			btnINSCONadddematacno.Enabled = true;
			txtINSCONgraduationinstitution.Enabled = true;
			txtINSCONpastemployee.Enabled = true;
			btnINSCONpastemployees.Enabled = true;
			btnddrelative.Enabled = true;
			btnmaterial.Enabled = true;
			txtEmailemp.Enabled = true;
			btnINSCONGraduationInstitution.Enabled = true;
			groupBox1.Enabled = true;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked == true)
			{
				permanentaddress.Text = txtINSCONaddressmaster.Text;
				permanentaddress.Enabled = false;
			}
			else
			{
				permanentaddress.Text = "";
				permanentaddress.Enabled = true;
			}
		}

		#endregion

		private void btnaddrelafinaci_Click(object sender, EventArgs e)
		{
			try
			{
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
					int error = 0;
					T_INS_PER CNS = new T_INS_PER();
					List<T_INS_PER_IMMEDAITE_RELATIVES> T_INS_PER_IMMEDAITE_RELATIVES = new List<T_INS_PER_IMMEDAITE_RELATIVES>();
					foreach (DataGridViewRow row in dataGridViewISNCONrelativetable.Rows)
					{
						if (row.Cells["PANNo"].Value.ToString() != "")
						{
							if (!new MasterClass().IsValidPanno(row.Cells["PANNo"].Value.ToString()))
							{
								DialogResult dialoga = MessageBox.Show("Enter Proper PAN No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error++;
							}
							else if (row.Cells["DematAcNo"].Value.ToString() != "")
							{
								if (!new MasterClass().IsValidDematAcno(row.Cells["DematAcNo"].Value.ToString()))
								{
									DialogResult dialoga = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
									error++;
								}
								else
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
							}
							else
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
						}
						else if (row.Cells["DematAcNo"].Value.ToString() != "")
						{
							if (!new MasterClass().IsValidDematAcno(row.Cells["DematAcNo"].Value.ToString()))
							{
								DialogResult dialoga = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error++;
							}
							else
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
						}
						else
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
					}
					CNS.RELATIVEARRAY = T_INS_PER_IMMEDAITE_RELATIVES.ToArray();

					List<T_INS_PER_MATERIAL_FINANCIAL> T_INS_PER_MATERIAL_FINANCIAL = new List<T_INS_PER_MATERIAL_FINANCIAL>();

					foreach (DataGridViewRow row in dataGridViewmaterialfinancial.Rows)
					{
						if (row.Cells["FinancialPanno"].Value.ToString() != "")
						{
							if (!new MasterClass().IsValidPanno(row.Cells["FinancialPanno"].Value.ToString()))
							{
								DialogResult dialoga = MessageBox.Show("Enter Proper PAN No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error++;
							}
							else if (row.Cells["FinancialDemaAcno"].Value.ToString() != "")
							{
								if (!new MasterClass().IsValidDematAcno(row.Cells["FinancialDemaAcno"].Value.ToString()))
								{
									DialogResult dialoga = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
									error++;
								}
								else
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
							}
							else
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
						}
						else if (row.Cells["FinancialDemaAcno"].Value.ToString() != "")
						{
							if (!new MasterClass().IsValidDematAcno(row.Cells["FinancialDemaAcno"].Value.ToString()))
							{
								DialogResult dialoga = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error++;
							}
							else
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
						}
						else
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
					}
					CNS.FINANCIALARRAY = T_INS_PER_MATERIAL_FINANCIAL.ToArray();

					if (error == 0)
					{
						foreach (T_INS_PER_MATERIAL_FINANCIAL m_x in CNS.FINANCIALARRAY)
						{
							string dss = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT(PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK)  VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N')").ToString();
							string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dss + "','" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N')").ToString();
							lg.CURRVALUE = "CONNECTED PERSON TAB FINANCIAL RELATIONSHIP";
							lg.TYPE = "INSERTED";
							lg.ID = perlogisdt;
							lg.DESCRIPTION = "INSERTED VALUE :- " + txtINSCONconnectperson.Text;
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							//&& = SESSIONKEYS.UserID.ToString();
							new MasterClass().SAVE_LOG(lg);
						}

						foreach (T_INS_PER_IMMEDAITE_RELATIVES m_x in CNS.RELATIVEARRAY)
						{
							string dss = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT(PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK)  VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N')").ToString();
							string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dss + "','" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N')").ToString();
							lg.CURRVALUE = "CONNECTED PERSON TAB RELATIVE RELATIONSHIP";
							lg.TYPE = "INSERTED";
							lg.ID = perlogisdt;
							lg.DESCRIPTION = "INSERTED VALUE :- " + txtINSCONconnectperson.Text;
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							//&& = SESSIONKEYS.UserID.ToString();
							new MasterClass().SAVE_LOG(lg);
						}
						DialogResult dialog = MessageBox.Show("Inserted Connected Person Relative & Financial Relationship.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
						Clear();
						btnupdateINSCON.Visible = false;
						btnaddINSCONdeelete.Visible = false;
						btncacncelINSCON.Visible = false;
						btnaddINSCON.Visible = true;
						FillConnectPersonID();
						button2.PerformClick();
					}
				}
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Data Not Updated. Please Check Your Internet Connection.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnupdatefinanrel_Click(object sender, EventArgs e)
		{
			try
			{
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
					int error = 0;
					T_INS_PER CNS = new T_INS_PER();
					List<T_INS_PER_IMMEDAITE_RELATIVES> T_INS_PER_IMMEDAITE_RELATIVES = new List<T_INS_PER_IMMEDAITE_RELATIVES>();
					foreach (DataGridViewRow row in dataGridViewISNCONrelativetable.Rows)
					{
						row.Cells["RelativeID"].Value = row.Cells["RelativeID"].Value == null ? "" : row.Cells["RelativeID"].Value.ToString();
						row.Cells["FullName"].Value = row.Cells["FullName"].Value == null ? "" : row.Cells["FullName"].Value.ToString();
						row.Cells["MobileNo"].Value = row.Cells["MobileNo"].Value == null ? "" : row.Cells["MobileNo"].Value.ToString();
						row.Cells["PANNo"].Value = row.Cells["PANNo"].Value == null ? "" : row.Cells["PANNo"].Value.ToString();
						row.Cells["DematAcNo"].Value = row.Cells["DematAcNo"].Value == null ? "" : row.Cells["DematAcNo"].Value.ToString();
						row.Cells["Relationship"].Value = row.Cells["Relationship"].Value == null ? "" : row.Cells["Relationship"].Value.ToString();
						row.Cells["Address"].Value = row.Cells["Address"].Value == null ? "" : row.Cells["Address"].Value.ToString();

						if (row.Cells["PANNo"].Value.ToString() != "")
						{
							if (!new MasterClass().IsValidPanno(row.Cells["PANNo"].Value.ToString()))
							{
								DialogResult dialoga = MessageBox.Show("Enter Proper PAN No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error++;
							}
							else if (row.Cells["DematAcNo"].Value.ToString() != "")
							{
								if (!new MasterClass().IsValidDematAcno(row.Cells["DematAcNo"].Value.ToString()))
								{
									DialogResult dialoga = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
									error++;
								}
							}
						}
						else if (row.Cells["DematAcNo"].Value.ToString() != "")
						{
							if (!new MasterClass().IsValidDematAcno(row.Cells["DematAcNo"].Value.ToString()))
							{
								DialogResult dialoga = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error++;
							}
						}
						if (error == 0)
						{
							T_INS_PER_IMMEDAITE_RELATIVES RELATIVEARRAY = new T_INS_PER_IMMEDAITE_RELATIVES
							{
								ID = row.Cells["RelativeID"].Value == null ? "" : row.Cells["RelativeID"].Value.ToString(),
								NAME = row.Cells["FullName"].Value == null ? "" : row.Cells["FullName"].Value.ToString(),
								MOBILE_NO = row.Cells["MobileNo"].Value == null ? "" : row.Cells["MobileNo"].Value.ToString(),
								PAN_NO = row.Cells["PANNo"].Value == null ? "" : row.Cells["PANNo"].Value.ToString(),
								DEMAT_AC_NO = row.Cells["DematAcNo"].Value == null ? "" : row.Cells["DematAcNo"].Value.ToString(),
								RELATIONSHIP = row.Cells["Relationship"].Value == null ? "" : row.Cells["Relationship"].Value.ToString(),
								ADDRESS = row.Cells["Address"].Value == null ? "" : row.Cells["Address"].Value.ToString()
							};

							T_INS_PER_IMMEDAITE_RELATIVES.Add(RELATIVEARRAY);
						}

					}
					CNS.RELATIVEARRAY = T_INS_PER_IMMEDAITE_RELATIVES.ToArray();

					List<T_INS_PER_MATERIAL_FINANCIAL> T_INS_PER_MATERIAL_FINANCIAL = new List<T_INS_PER_MATERIAL_FINANCIAL>();

					foreach (DataGridViewRow row in dataGridViewmaterialfinancial.Rows)
					{
						row.Cells["FinancialID"].Value = row.Cells["FinancialID"].Value == null ? "" : row.Cells["FinancialID"].Value.ToString();
						row.Cells["financilaFullName"].Value = row.Cells["financilaFullName"].Value == null ? "" : row.Cells["financilaFullName"].Value.ToString();
						row.Cells["financilaMobileNo"].Value = row.Cells["financilaMobileNo"].Value == null ? "" : row.Cells["financilaMobileNo"].Value.ToString();
						row.Cells["FinancialPanno"].Value = row.Cells["FinancialPanno"].Value == null ? "" : row.Cells["FinancialPanno"].Value.ToString();
						row.Cells["FinancialDemaAcno"].Value = row.Cells["FinancialDemaAcno"].Value == null ? "" : row.Cells["FinancialDemaAcno"].Value.ToString();
						row.Cells["FinancialRelationship"].Value = row.Cells["FinancialRelationship"].Value == null ? "" : row.Cells["FinancialRelationship"].Value.ToString();
						row.Cells["Financialaddress"].Value = row.Cells["Financialaddress"].Value == null ? "" : row.Cells["Financialaddress"].Value.ToString();

						if (row.Cells["FinancialPanno"].Value.ToString() != "")
						{
							if (!new MasterClass().IsValidPanno(row.Cells["FinancialPanno"].Value.ToString()))
							{
								DialogResult dialoga = MessageBox.Show("Enter Proper PAN No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error++;
							}
							else if (row.Cells["FinancialDemaAcno"].Value.ToString() != "")
							{
								if (!new MasterClass().IsValidDematAcno(row.Cells["FinancialDemaAcno"].Value.ToString()))
								{
									DialogResult dialoga = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
									error++;
								}
							}
						}
						else if (row.Cells["FinancialDemaAcno"].Value.ToString() != "")
						{
							if (!new MasterClass().IsValidDematAcno(row.Cells["FinancialDemaAcno"].Value.ToString()))
							{
								DialogResult dialoga = MessageBox.Show("Enter Proper Demat A.c No.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
								error++;
							}
						}

						if (error == 0)
						{
							T_INS_PER_MATERIAL_FINANCIAL FINANCIALARRAY = new T_INS_PER_MATERIAL_FINANCIAL
							{
								ID = row.Cells["FinancialID"].Value == null ? "" : row.Cells["FinancialID"].Value.ToString(),
								NAME = row.Cells["financilaFullName"].Value == null ? "" : row.Cells["financilaFullName"].Value.ToString(),
								MOBILE_NO = row.Cells["financilaMobileNo"].Value == null ? "" : row.Cells["financilaMobileNo"].Value.ToString(),
								PAN_NO = row.Cells["FinancialPanno"].Value == null ? "" : row.Cells["FinancialPanno"].Value.ToString(),
								DEMAT_AC_NO = row.Cells["FinancialDemaAcno"].Value == null ? "" : row.Cells["FinancialDemaAcno"].Value.ToString(),
								RELATIONSHIP = row.Cells["FinancialRelationship"].Value == null ? "" : row.Cells["FinancialRelationship"].Value.ToString(),
								ADDRESS = row.Cells["Financialaddress"].Value == null ? "" : row.Cells["Financialaddress"].Value.ToString()
							};
							T_INS_PER_MATERIAL_FINANCIAL.Add(FINANCIALARRAY);
						}
					}
					CNS.FINANCIALARRAY = T_INS_PER_MATERIAL_FINANCIAL.ToArray();
					CNS.CONNECT_PERSON_ID = txtINSCONconnectperson.Text;

					if (error == 0)
					{
						new MasterClass().executeQueryForDB("UPDATE T_INS_PER SET MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE ID = '" + lblidforcp.Text + "'").ToString();

						foreach (T_INS_PER_MATERIAL_FINANCIAL m_x in CNS.FINANCIALARRAY)
						{
							string dss = "";
							if (m_x.ID != "")
							{
								DataSet getvals = new MasterClass().getDataSet("SELECT ID,TYPE FROM T_INS_PER_DT_LOG WHERE ACTIVE = 'Y' AND TID = '" + m_x.ID + "' ORDER BY ENTEREDON DESC");
								financial = financial.Where(val => val != m_x.ID).ToArray();
								//for (int i = 0; i < getvals.Tables[0].Rows.Count; i++)
								//{
								//	if (m_x.ID == getvals.Tables[0].Rows[i]["ID"].ToString())
								//	{
								dss = new MasterClass().executeQuery("UPDATE T_INS_PER_DT SET PERID =  '" + lblidforcp.Text + "',NAME='" + CryptographyHelper.Encrypt(m_x.NAME) + "',ADDRESS ='" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "',RELATIONSHIP = '" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "',MOBILENO = '" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "',PANNO = '" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "',DEMATACNO = '" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "',ACTIVE = 'Y' WHERE ID = '" + m_x.ID + "' ;").ToString();

								string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + m_x.ID + "','" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();
								lg.CURRVALUE = "CONNECTED PERSON TAB FINANCIAL RELATIONSHIP";
								lg.TYPE = "UPDATED";
								lg.ID = perlogisdt + "|" + getvals.Tables[0].Rows[0]["ID"].ToString();
								lg.DESCRIPTION = "UPDATED VALUE :- " + CNS.CONNECT_PERSON_ID;
								lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
								//lg.ID = SESSIONKEYS.UserID.ToString();
								new MasterClass().SAVE_LOG(lg);
								//	}
								//}
							}
							else
							{
								dss = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT(PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK)  VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N')").ToString();

								string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dss + "','" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();
								lg.CURRVALUE = "CONNECTED PERSON TAB FINANCIAL RELATIONSHIP";
								lg.TYPE = "UPDATED";
								lg.ID = perlogisdt;
								lg.DESCRIPTION = "UPDATED VALUE :- " + CNS.CONNECT_PERSON_ID;
								lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
								//lg.ID = SESSIONKEYS.UserID.ToString();
								new MasterClass().SAVE_LOG(lg);
							}



						}
						Thread.Sleep(1000);
						foreach (T_INS_PER_IMMEDAITE_RELATIVES m_x in CNS.RELATIVEARRAY)
						{
							string dss = "";
							if (m_x.ID != "")
							{
								DataSet getvals = new MasterClass().getDataSet("SELECT ID,TYPE FROM T_INS_PER_DT_LOG WHERE ACTIVE = 'Y' AND TID = '" + m_x.ID + "' ORDER BY ENTEREDON DESC");
								relative = relative.Where(val => val != m_x.ID).ToArray();
								//for (int i = 0; i < getvals.Tables[0].Rows.Count; i++)
								//{
								//	if (m_x.ID == getvals.Tables[0].Rows[i]["ID"].ToString().Trim())
								//	{
								dss = new MasterClass().executeQuery("UPDATE T_INS_PER_DT SET PERID =  '" + lblidforcp.Text + "',NAME='" + CryptographyHelper.Encrypt(m_x.NAME) + "',ADDRESS ='" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "',RELATIONSHIP = '" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "',MOBILENO = '" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "',PANNO = '" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "',DEMATACNO = '" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "',ACTIVE = 'Y' WHERE ID = '" + m_x.ID + "' ;").ToString();

								string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + m_x.ID + "','" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();
								lg.CURRVALUE = "CONNECTED PERSON TAB RELATIVE RELATIONSHIP";
								lg.TYPE = "UPDATED";
								lg.ID = perlogisdt + "|" + getvals.Tables[0].Rows[0]["ID"].ToString();
								lg.DESCRIPTION = "UPDATED VALUE :- " + CNS.CONNECT_PERSON_ID;
								lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
								//lg.ID = SESSIONKEYS.UserID.ToString();
								new MasterClass().SAVE_LOG(lg);
								//	}
								//}
							}
							else
							{
								dss = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT(PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK)  VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N')").ToString();

								string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dss + "','" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();
								lg.CURRVALUE = "CONNECTED PERSON TAB RELATIVE RELATIONSHIP";
								lg.TYPE = "UPDATED";
								lg.ID = perlogisdt;
								lg.DESCRIPTION = "UPDATED VALUE :- " + CNS.CONNECT_PERSON_ID;
								lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
								//lg.ID = SESSIONKEYS.UserID.ToString();
								new MasterClass().SAVE_LOG(lg);
							}


						}
						if (relative.Length > 0)
						{
							for (int i = 0; i < relative.Length; i++)
							{
								new MasterClass().executeQuery("DELETE FROM T_INS_PER_DT WHERE ID = '" + relative[i] + "'");
								lg.CURRVALUE = "CONNECTED PERSON TAB RELATIVE RELATIONSHIP";
								lg.TYPE = "DELETED";
								lg.ID = relative[i];
								lg.DESCRIPTION = "DELETED VALUE :- " + CNS.CONNECT_PERSON_ID;
								lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
								//lg.ID = SESSIONKEYS.UserID.ToString();
								new MasterClass().SAVE_LOG(lg);
								relative = relative.Where(val => val != relative[i]).ToArray();
							}
						}
						if (financial.Length > 0)
						{
							for (int i = 0; i < financial.Length; i++)
							{
								new MasterClass().executeQuery("DELETE FROM T_INS_PER_DT WHERE ID = '" + financial[i] + "'");
								lg.CURRVALUE = "CONNECTED PERSON TAB FINANCIAL RELATIONSHIP";
								lg.TYPE = "DELETED";
								lg.ID = financial[i];
								lg.DESCRIPTION = "DELETED VALUE :- " + CNS.CONNECT_PERSON_ID;
								lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
								//lg.ID = SESSIONKEYS.UserID.ToString();
								new MasterClass().SAVE_LOG(lg);
								financial = financial.Where(val => val != financial[i]).ToArray();
							}
						}

						DialogResult dialog = MessageBox.Show("Updated Connected Person Relative & Financial Relationship.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
						Clear();
						btnupdateINSCON.Visible = false;
						btnaddINSCONdeelete.Visible = false;
						btncacncelINSCON.Visible = false;
						btnaddINSCON.Visible = true;
						FillConnectPersonID();
						button2.PerformClick();
					}
				}
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Data Not Updated. Please Check Your Internet Connection.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
