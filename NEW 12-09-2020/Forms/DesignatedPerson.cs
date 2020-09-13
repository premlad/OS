using RSACryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using The_PIT_Archive.Data_Access_Layer;
using The_PIT_Archive.Data_Entity;

namespace The_PIT_Archive.Forms
{
    public partial class DesignatedPerson : Form
    {
        AUDITLOG lg = new AUDITLOG();
        private string PANO = "";
        private string[] relative;
        private string[] financial;
        private MasterClass mc = new MasterClass();
        public DesignatedPerson()
        {
            InitializeComponent();
        }

        private void DesignatedPerson_Load(object sender, EventArgs e)
        {
            Login l = new Login();
            //TopMost = true;
            //WindowState = FormWindowState.Maximized;
            try
            {
                if (SESSIONKEYS.UserID.ToString() == "")
                {
                    Hide();
                    l.Show();
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                Hide();
                l.Show();
            }
            FillConnectPersonID();
        }

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
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtMobileINSCONNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnINSCONaddnumber_Click(object sender, EventArgs e)
        {
            if (txtMobileINSCONNumber.Text == "")
            {
                DialogResult dialog = MessageBox.Show("Enter Mobile/Phone No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DialogResult dialog = MessageBox.Show("Enter Demat A.c No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //else if (!new MasterClass().IsValidDematAcno(txtINSCONdemataccountno.Text))
            //{
            //    DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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
                DialogResult dialog = MessageBox.Show("Enter Graduation Institution", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DialogResult dialog = MessageBox.Show("Enter Past Employee.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Designated Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    dataGridViewPhonemobile.Rows.RemoveAt(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewDematAcoount_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Designated Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    dataGridViewDematAcoount.Rows.RemoveAt(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewGraduationInstitution_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Designated Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    dataGridViewGraduationInstitution.Rows.RemoveAt(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewPastEmployee_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Designated Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    dataGridViewPastEmployee.Rows.RemoveAt(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnddrelative_Click(object sender, EventArgs e)
        {
            int error = 0;
            string a = CMBTYPE.Text;
            if (txtINSCONrelativefullnam.Text == "" && txtINSCONrelativemobileno.Text == "" && txtINSCONrelativepanno.Text == "" && txtINSCONrelativedematacno.Text == "" && txtINSCONrelativerelationship.Text == "" && txtINSCONrelativeaddress.Text == "" && txtINSCONrelativeEmailID.Text == "")
            {
                error++;
                DialogResult dialog = MessageBox.Show("Enter Values.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtINSCONrelativeEmailID.Text != "")
            {
                if (!new MasterClass().IsValidEmail(txtINSCONrelativeEmailID.Text))
                {
                    error++;
                    DialogResult dialog = MessageBox.Show("Enter Proper Email ID.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (a == "" || a == null)
            {
                error++;
                DialogResult dialog = MessageBox.Show("Enter Values.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //else if (txtINSCONrelativedematacno.Text != "")
            //{
            //    if (!new MasterClass().IsValidDematAcno(txtINSCONrelativedematacno.Text))
            //    {
            //        error++;
            //        DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            if (txtINSCONrelativepanno.Text != "")
            {
                if (!new MasterClass().IsValidPanno(txtINSCONrelativepanno.Text))
                {
                    error++;
                    DialogResult dialog = MessageBox.Show("Enter Proper PAN No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            string type = "";

            if (CMBTYPE.SelectedText.ToString() == "My Immediate Relative's")
            {
                type = "My Immediate Relative's";
            }
            else if (CMBTYPE.SelectedText.ToString() == "The Person With Whom, I share Material Financial Relationship")
            {
                type = "The Person With Whom, I share Material Financial Relationship";
            }

            if (error == 0)
            {
                string[] row = { type, txtINSCONrelativefullnam.Text, txtINSCONrelativemobileno.Text, txtINSCONrelativepanno.Text, txtINSCONrelativedematacno.Text, txtINSCONrelativerelationship.Text, txtINSCONrelativeEmailID.Text, txtINSCONrelativeaddress.Text, "" };
                dataGridViewISNCONrelativetable.Rows.Add(row);
                txtINSCONrelativefullnam.Text = "";
                txtINSCONrelativemobileno.Text = "";
                txtINSCONrelativepanno.Text = "";
                txtINSCONrelativedematacno.Text = "";
                txtINSCONrelativerelationship.Text = "";
                txtINSCONrelativeaddress.Text = "";
                txtINSCONrelativeEmailID.Text = "";
                CMBTYPE.Text = "";
            }
        }

        private void dataGridViewISNCONrelativetable_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Designated Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    dataGridViewISNCONrelativetable.Rows.RemoveAt(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnaddINSCON_Click(object sender, EventArgs e)
        {
            try
            {
                int error = 0;
                //SetLoading(true);

                //Thread.Sleep(2000);
                //Invoke((MethodInvoker)delegate
                //{
                //if (txtINSCONconnectperson.Text == "")
                //{
                //	DialogResult dialog = MessageBox.Show("Enter Connect Person ID.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Hide();
                }
                else if (txtINSCONnameofemployee.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Enter Name of Employee.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtINSCONaddressmaster.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Enter Permanent Address.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (permanentaddress.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Enter Correspondence Address.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!new MasterClass().IsValidEmail(txtEmailemp.Text))
                {
                    DialogResult dialog = MessageBox.Show("Please Enter Email in Proper Format.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dataGridViewPhonemobile.Rows.Count <= 0)
                {
                    DialogResult dialog = MessageBox.Show("Enter Landline/Mobile No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (txtINSCONpannomaster.Text == "")
                    {
                        if (txtotheridentifier.Text == "")
                        {
                            DialogResult dialog = MessageBox.Show("Enter Other Identifier No or Pan No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            error++;
                        }
                    }
                    else
                    {
                        if (!new MasterClass().IsValidPanno(txtINSCONpannomaster.Text))
                        {
                            DialogResult dialog = MessageBox.Show("Enter Proper PAN No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        DialogResult dialog = MessageBox.Show("Pan No Already Exists in our Database.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            //if (!new MasterClass().IsValidDematAcno(row.Cells["datagridviewdematacno"].Value.ToString()))
                            //{
                            //    DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    error++;
                            //}
                            //else
                            //{
                            if (row.Index == 0)
                            {
                                DematAcNo += row.Cells["datagridviewdematacno"].Value.ToString();
                            }
                            else
                            {
                                DematAcNo += "|" + row.Cells["datagridviewdematacno"].Value.ToString();
                            }
                            //}
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
                            string DPID = "DP" + new MasterClass().GETCPID();
                            string ds = new MasterClass().executeQuery("INSERT INTO T_INS_PER (CONNECTPERSONID, EMPNAME, CURRDESIGNATION, ADDRESS, RESIADDRESS, PANNO, OTHERIDENTIFIER, DEMATACNO, MOBILENO, EMAILID, GRADUATIONINSTI, PASTEMP, ENTEREDBY, ENTEREDON, ACTIVE, LOCK) VALUES ('" + CryptographyHelper.Encrypt(DPID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.EMAILID) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N')").ToString();
                            string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,RESIADDRESS,PANNO,OTHERIDENTIFIER,DEMATACNO,MOBILENO,EMAILID,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ds + "','" + CryptographyHelper.Encrypt(DPID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.EMAILID) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N')").ToString();
                            lg.CURRVALUE = "DESIGNATED PERSON TAB";
                            lg.TYPE = "INSERTED";
                            lg.ID = perlogid;
                            lg.DESCRIPTION = "INSERTED VALUE :- " + DPID;
                            lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                            //lg.ID = SESSIONKEYS.UserID.ToString();
                            new MasterClass().SAVE_LOG(lg);
                            if (Convert.ToInt32(ds) > 0)
                            {
                                lblidforcp.Text = Convert.ToString(ds);
                                DialogResult dialogResult = MessageBox.Show("Designated Person Data Saved Successfully. Do You want to Add Relative & Financial Data?", "Designated Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    panel1.Enabled = false;
                                    panel2.Enabled = true;
                                    btnaddrelafinaci.Enabled = true;
                                    btnaddINSCON.Enabled = false;
                                    txtINSCONrelativeEmailID.Enabled = true;
                                    CMBTYPE.Enabled = true;
                                }
                                else
                                {
                                    Clear();
                                    btnupdateINSCON.Visible = false;
                                    btnaddINSCONdeelete.Visible = false;
                                    btncacncelINSCON.Visible = false;
                                    btnaddINSCON.Visible = true;
                                    txtINSCONrelativeEmailID.Enabled = false;
                                    CMBTYPE.Enabled = false;
                                    FillConnectPersonID();
                                    button2click();
                                }
                            }
                            else
                            {
                                DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                //});
                //SetLoading(false);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                //SetLoading(false);
                DialogResult dialog = MessageBox.Show("Data Not Saved. Please Check Your Internet Connection.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                List<string> financialList = new List<string>();
                                List<string> relativeList = new List<string>();
                                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                                {
                                    string a = CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["TYPE"].ToString());
                                    if (CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["TYPE"].ToString()) == "FINANCIAL")
                                    {
                                        string[] row = { "The Person With Whom, I share Material Financial Relationship", CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["DEMATACNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["RELATIONSHIP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["ADDRESS"].ToString()), ds.Tables[1].Rows[i]["ID"].ToString() };
                                        dataGridViewISNCONrelativetable.Rows.Add(row);
                                        financialList.Add(ds.Tables[1].Rows[i]["ID"].ToString());
                                    }
                                    else
                                    {
                                        string[] row = { "My Immediate Relative's", CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["DEMATACNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["RELATIONSHIP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[1].Rows[i]["ADDRESS"].ToString()), ds.Tables[1].Rows[i]["ID"].ToString() };
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
                                txtINSCONrelativeEmailID.Text = "";
                                txtINSCONconnectperson.Enabled = true;
                                btnupdateINSCON.Visible = true;
                                btnaddINSCONdeelete.Visible = true;
                                btncacncelINSCON.Visible = true;
                                btnaddINSCON.Visible = false;

                                btnaddrelafinaci.Visible = false;
                                btnupdatefinanrel.Visible = true;
                                btnupdatefinanrel.Enabled = true;
                                panel1.Enabled = true;
                                panel2.Enabled = true;
                                txtINSCONrelativeEmailID.Enabled = true;
                                CMBTYPE.Enabled = true;
                                if (ds.Tables[0].Rows[0]["ACTIVE"].ToString().Trim() == "N")
                                {
                                    btnupdateINSCON.Visible = false;
                                    btnaddINSCONdeelete.Text = "RETREIVE DP";
                                    btnupdatefinanrel.Visible = false;
                                    panel2.Enabled = false;
                                    panel1.Enabled = false;
                                }
                                else
                                {
                                    btnupdateINSCON.Enabled = true;
                                    btnaddINSCONdeelete.Text = "NOMORE DP";
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
                    button2click();
                }

            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtINSCONrelativefullnam.Text = "";
            txtINSCONrelativemobileno.Text = "";
            txtINSCONrelativepanno.Text = "";
            txtINSCONrelativedematacno.Text = "";
            txtINSCONrelativerelationship.Text = "";
            txtINSCONrelativeaddress.Text = "";
            permanentaddress.Text = "";
            txtotheridentifier.Text = "";
            txtEmailemp.Text = "";
            lblidforcp.Text = "";
            txtINSCONrelativeEmailID.Text = "";
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
            button2click();
        }

        private void btnupdateINSCON_Click(object sender, EventArgs e)
        {
            try
            {
                int error = 0;
                //SetLoading(true);

                //Thread.Sleep(2000);
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
                    Hide();
                }
                else if (txtINSCONconnectperson.Text == "")
                {
                    error++;
                    DialogResult dialog = MessageBox.Show("Enter Designated Person ID.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtINSCONnameofemployee.Text == "")
                {
                    error++;
                    DialogResult dialog = MessageBox.Show("Enter Name of Employee.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtINSCONaddressmaster.Text == "")
                {
                    error++;
                    DialogResult dialog = MessageBox.Show("Enter Permanent Address.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (permanentaddress.Text == "")
                {
                    error++;
                    DialogResult dialog = MessageBox.Show("Enter Correspondence Address.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!new MasterClass().IsValidEmail(txtEmailemp.Text))
                {
                    error++;
                    DialogResult dialog = MessageBox.Show("Please Enter Email in Proper Format.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dataGridViewPhonemobile.Rows.Count <= 0)
                {
                    error++;
                    DialogResult dialog = MessageBox.Show("Enter Phone No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (txtINSCONpannomaster.Text == "")
                    {
                        if (txtotheridentifier.Text == "")
                        {
                            DialogResult dialog = MessageBox.Show("Enter Other Identifier No or Pan No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            error++;
                        }
                    }
                    else
                    {
                        if (!new MasterClass().IsValidPanno(txtINSCONpannomaster.Text))
                        {
                            DialogResult dialog = MessageBox.Show("Enter Proper PAN No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        DialogResult dialog = MessageBox.Show("Pan No Already Exists in our Database.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            //if (!new MasterClass().IsValidDematAcno(row.Cells["datagridviewdematacno"].Value.ToString()))
                            //{
                            //    DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    error++;
                            //}
                            //else
                            //{
                            if (row.Index == 0)
                            {
                                DematAcNo += row.Cells["datagridviewdematacno"].Value.ToString();
                            }
                            else
                            {
                                DematAcNo += "|" + row.Cells["datagridviewdematacno"].Value.ToString();
                            }
                            //}
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
                            string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_PER SET EMPNAME = '" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "',CURRDESIGNATION = '" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "',ADDRESS = '" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "',RESIADDRESS = '" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "',PANNO = '" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "',OTHERIDENTIFIER = '" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "',DEMATACNO = '" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "',MOBILENO = '" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "',EMAILID = '" + CryptographyHelper.Encrypt(CNS.EMAILID) + "',GRADUATIONINSTI = '" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "',PASTEMP = '" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE ID = '" + lblidforcp.Text + "' ; ").ToString();
                            //new MasterClass().executeQuery("UPDATE T_INS_PER_DT SET ACTIVE = 'N' WHERE PERID = '" + lblidforcp.Text + "' AND ACTIVE = 'Y'");
                            //new MasterClass().executeQuery("UPDATE T_INS_PER_DT_LOG SET ACTIVE = 'N' WHERE PERID = '" + lblidforcp.Text + "' AND ACTIVE = 'Y'");
                            string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,RESIADDRESS,PANNO,OTHERIDENTIFIER,DEMATACNO,MOBILENO,EMAILID,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(CNS.CONNECT_PERSON_ID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.EMAILID) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();

                            lg.CURRVALUE = "DESIGNATED PERSON TAB";
                            lg.TYPE = "UPDATED";
                            lg.ID = perlogid + "|" + getval.Tables[0].Rows[0]["ID"].ToString();
                            lg.DESCRIPTION = "UPDATED VALUE :- " + CNS.CONNECT_PERSON_ID;
                            lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                            //lg.ID = SESSIONKEYS.UserID.ToString();
                            new MasterClass().SAVE_LOG(lg);
                            //Thread.Sleep(1000);
                            if (Convert.ToInt32(ds) > 0)
                            {
                                DialogResult dialogResult = MessageBox.Show("Designated Person Updated Data Successfully. Do You want to Update Relative & Financial Data?", "Designated Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                                    txtINSCONrelativeEmailID.Enabled = false;
                                    CMBTYPE.Enabled = false;
                                    FillConnectPersonID();
                                    button2click();
                                }

                            }
                            else
                            {
                                DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                //});

                //SetLoading(false);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                //SetLoading(false);
                DialogResult dialog = MessageBox.Show("Data Not Updated. Please Check Your Internet Connection.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnaddINSCONdeelete_Click(object sender, EventArgs e)
        {
            try
            {
                //SetLoading(true);

                //Thread.Sleep(2000);
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
                    Hide();
                }
                else if (txtINSCONconnectperson.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Enter Designated Person ID.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        //if (!new MasterClass().IsValidDematAcno(row.Cells["datagridviewdematacno"].Value.ToString()))
                        //{
                        //    DialogResult dialog = MessageBox.Show("Enter Proper Demat A.c No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
                        //else
                        //{
                        if (row.Index == 0)
                        {
                            DematAcNo += row.Cells["datagridviewdematacno"].Value.ToString();
                        }
                        else
                        {
                            DematAcNo += "|" + row.Cells["datagridviewdematacno"].Value.ToString();
                        }
                        //}
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
                        if (row.Cells["Type"].Value.ToString() == "My Immediate Relative's")
                        {
                            T_INS_PER_IMMEDAITE_RELATIVES RELATIVEARRAY = new T_INS_PER_IMMEDAITE_RELATIVES
                            {
                                NAME = row.Cells["FullName"].Value.ToString(),
                                MOBILE_NO = row.Cells["MobileNo"].Value.ToString(),
                                PAN_NO = row.Cells["PANNo"].Value.ToString(),
                                DEMAT_AC_NO = row.Cells["DematAcNo"].Value.ToString(),
                                RELATIONSHIP = row.Cells["Relationship"].Value.ToString(),
                                EMAILID = row.Cells["relativeEmailID"].Value.ToString(),
                                ADDRESS = row.Cells["Address"].Value.ToString()
                            };
                            T_INS_PER_IMMEDAITE_RELATIVES.Add(RELATIVEARRAY);
                        }
                    }
                    CNS.RELATIVEARRAY = T_INS_PER_IMMEDAITE_RELATIVES.ToArray();

                    List<T_INS_PER_MATERIAL_FINANCIAL> T_INS_PER_MATERIAL_FINANCIAL = new List<T_INS_PER_MATERIAL_FINANCIAL>();

                    foreach (DataGridViewRow row in dataGridViewISNCONrelativetable.Rows)
                    {
                        if (row.Cells["Type"].Value.ToString() != "My Immediate Relative's")
                        {
                            T_INS_PER_MATERIAL_FINANCIAL FINANCIALARRAY = new T_INS_PER_MATERIAL_FINANCIAL
                            {
                                NAME = row.Cells["FullName"].Value.ToString(),
                                MOBILE_NO = row.Cells["MobileNo"].Value.ToString(),
                                PAN_NO = row.Cells["PANNo"].Value.ToString(),
                                DEMAT_AC_NO = row.Cells["DematAcNo"].Value.ToString(),
                                RELATIONSHIP = row.Cells["Relationship"].Value.ToString(),
                                EMAILID = row.Cells["relativeEmailID"].Value.ToString(),
                                ADDRESS = row.Cells["Address"].Value.ToString()
                            };
                            T_INS_PER_MATERIAL_FINANCIAL.Add(FINANCIALARRAY);
                        }
                    }
                    CNS.FINANCIALARRAY = T_INS_PER_MATERIAL_FINANCIAL.ToArray();
                    CNS.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                    DialogResult dialogResult;
                    if (btnaddINSCONdeelete.Text == "RETREIVE DP")
                    {
                        dialogResult = MessageBox.Show("Are You Sure You Want to Retreive As DP?", "Designated Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_PER SET ACTIVE = 'Y',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE ID = '" + lblidforcp.Text + "'").ToString();
                            //string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,RESIADDRESS,PANNO,OTHERIDENTIFIER,DEMATACNO,MOBILENO,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(CNS.CONNECT_PERSON_ID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("RETREIVE DP") + "','Y','N')").ToString();
                            string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,RESIADDRESS,PANNO,OTHERIDENTIFIER,DEMATACNO,MOBILENO,EMAILID,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(CNS.CONNECT_PERSON_ID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.EMAILID) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("RETREIVE DP") + "','Y','N')").ToString();
                            lg.CURRVALUE = "DESIGNATED PERSON TAB";
                            lg.TYPE = "RETREIVE DP";
                            lg.ID = perlogid;
                            lg.DESCRIPTION = "RETREIVE DP :- " + CNS.CONNECT_PERSON_ID;
                            lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                            //lg.ID = SESSIONKEYS.UserID.ToString();
                            new MasterClass().SAVE_LOG(lg);


                            string dsS = new MasterClass().executeQueryForDB("UPDATE T_INS_PER_DT SET ACTIVE = 'Y',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE PERID = '" + lblidforcp.Text + "'").ToString();
                            foreach (T_INS_PER_MATERIAL_FINANCIAL m_x in CNS.FINANCIALARRAY)
                            {
                                string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + lblidforcp.Text + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("RETREIVE DP") + "','Y','N')").ToString();
                                lg.CURRVALUE = "DESIGNATED PERSON TAB FINANCIAL RELATIONSHIP";
                                lg.TYPE = "RETREIVE DP";
                                lg.ID = perlogisdt;
                                lg.DESCRIPTION = "RETREIVE DP :- " + CNS.CONNECT_PERSON_ID;
                                lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                                //lg.ID = SESSIONKEYS.UserID.ToString();
                                new MasterClass().SAVE_LOG(lg);
                            }

                            foreach (T_INS_PER_IMMEDAITE_RELATIVES m_x in CNS.RELATIVEARRAY)
                            {
                                string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dsS + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("RETREIVE DP") + "','Y','N')").ToString();
                                lg.CURRVALUE = "DESIGNATED PERSON TAB RELATIVE RELATIONSHIP";
                                lg.TYPE = "RETREIVE DP";
                                lg.ID = perlogisdt;
                                lg.DESCRIPTION = "RETREIVE DP :- " + CNS.CONNECT_PERSON_ID;
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
                                DialogResult dialog = MessageBox.Show("Updated Successfully.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();
                                txtINSCONconnectperson.Text = "";
                                btnupdateINSCON.Visible = false;
                                btnaddINSCONdeelete.Visible = false;
                                btncacncelINSCON.Visible = false;
                                btnaddINSCON.Visible = true;
                                FillConnectPersonID();
                                button2click();
                            }
                            else
                            {
                                DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        dialogResult = MessageBox.Show("Are You Sure You Want to Tag As Nomore DP?", "Designated Person", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialogResult == DialogResult.Yes)
                        {
                            string ds = new MasterClass().executeQueryForDB("UPDATE T_INS_PER SET ACTIVE = 'N',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE ID = '" + lblidforcp.Text + "'").ToString();
                            string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_PER_LOG(TID,CONNECTPERSONID,EMPNAME,CURRDESIGNATION,ADDRESS,RESIADDRESS,PANNO,OTHERIDENTIFIER,DEMATACNO,MOBILENO,EMAILID,GRADUATIONINSTI,PASTEMP,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(CNS.CONNECT_PERSON_ID) + "','" + CryptographyHelper.Encrypt(CNS.NAME_OF_EMP) + "','" + CryptographyHelper.Encrypt(CNS.CURRENT_DESIGNATION) + "','" + CryptographyHelper.Encrypt(CNS.ADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.RESIADDRESS) + "','" + CryptographyHelper.Encrypt(CNS.PAN_NO) + "','" + CryptographyHelper.Encrypt(CNS.OTHERIDENTIFIER) + "','" + CryptographyHelper.Encrypt(CNS.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(CNS.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(CNS.EMAILID) + "','" + CryptographyHelper.Encrypt(CNS.GRADUATION_INSTITUTIONS) + "','" + CryptographyHelper.Encrypt(CNS.PAST_EMPLOYEES) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("NOMORE DP") + "','Y','N')").ToString();
                            lg.CURRVALUE = "DESIGNATED PERSON TAB";
                            lg.TYPE = "NOMORE DP";
                            lg.ID = perlogid;
                            lg.DESCRIPTION = "NOMORE DP :- " + CNS.CONNECT_PERSON_ID;
                            lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                            //lg.ID = SESSIONKEYS.UserID.ToString();
                            new MasterClass().SAVE_LOG(lg);


                            string dsS = new MasterClass().executeQueryForDB("UPDATE T_INS_PER_DT SET ACTIVE = 'N',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE PERID = '" + lblidforcp.Text + "'").ToString();
                            foreach (T_INS_PER_MATERIAL_FINANCIAL m_x in CNS.FINANCIALARRAY)
                            {
                                string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + lblidforcp.Text + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("NOMORE DP") + "','Y','N')").ToString();
                                lg.CURRVALUE = "DESIGNATED PERSON TAB FINANCIAL RELATIONSHIP";
                                lg.TYPE = "NOMORE DP";
                                lg.ID = perlogisdt;
                                lg.DESCRIPTION = "NOMORE DP :- " + CNS.CONNECT_PERSON_ID;
                                lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                                //lg.ID = SESSIONKEYS.UserID.ToString();
                                new MasterClass().SAVE_LOG(lg);
                            }

                            foreach (T_INS_PER_IMMEDAITE_RELATIVES m_x in CNS.RELATIVEARRAY)
                            {
                                string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dsS + "','" + ds + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("NOMORE DP") + "','Y','N')").ToString();
                                lg.CURRVALUE = "DESIGNATED PERSON TAB RELATIVE RELATIONSHIP";
                                lg.TYPE = "NOMORE DP";
                                lg.ID = perlogisdt;
                                lg.DESCRIPTION = "NOMORE DP :- " + CNS.CONNECT_PERSON_ID;
                                lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                                //lg.ID = SESSIONKEYS.UserID.ToString();
                                new MasterClass().SAVE_LOG(lg);
                            }
                            if (Convert.ToInt32(ds) > 0)
                            {
                                DialogResult dialog = MessageBox.Show("Updated Successfully.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();
                                txtINSCONconnectperson.Text = "";
                                btnupdateINSCON.Visible = false;
                                btnaddINSCONdeelete.Visible = false;
                                btncacncelINSCON.Visible = false;
                                btnaddINSCON.Visible = true;
                                FillConnectPersonID();
                                button2click();
                            }
                            else
                            {
                                DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    //btnaddINSCONdeelete.Text = "RETREIVE DP";

                }

                //});

                //SetLoading(false);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                //SetLoading(false);
                DialogResult dialog = MessageBox.Show("Data Not Saved. Please Check Your Internet Connection.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Clear();
            FillConnectPersonID();
            string DPID = "DP" + new MasterClass().GETCPID();
            txtINSCONconnectperson.Text = DPID;
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
            txtEmailemp.Enabled = true;
            btnINSCONGraduationInstitution.Enabled = true;
            panel2.Enabled = false;
            panel1.Enabled = true;
        }

        void button2click()
        {
            Clear();
            FillConnectPersonID();
            string DPID = "DP" + new MasterClass().GETCPID();
            txtINSCONconnectperson.Text = DPID;
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
            txtEmailemp.Enabled = true;
            btnINSCONGraduationInstitution.Enabled = true;
            panel2.Enabled = false;
            panel1.Enabled = true;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
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
            txtEmailemp.Enabled = true;
            btnINSCONGraduationInstitution.Enabled = true;
            panel1.Enabled = true;
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
                    Hide();
                }
                else
                {
                    int error = 0;
                    T_INS_PER CNS = new T_INS_PER();
                    List<T_INS_PER_IMMEDAITE_RELATIVES> T_INS_PER_IMMEDAITE_RELATIVES = new List<T_INS_PER_IMMEDAITE_RELATIVES>();
                    foreach (DataGridViewRow row in dataGridViewISNCONrelativetable.Rows)
                    {
                        if (row.Cells["Type"].Value.ToString() == "My Immediate Relative's")
                        {
                            if (row.Cells["PANNo"].Value.ToString() != "")
                            {
                                if (!new MasterClass().IsValidPanno(row.Cells["PANNo"].Value.ToString()))
                                {
                                    DialogResult dialoga = MessageBox.Show("Enter Proper PAN No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    error++;
                                }
                            }

                            //if (row.Cells["DematAcNo"].Value.ToString() != "")
                            //{
                            //    if (!new MasterClass().IsValidDematAcno(row.Cells["DematAcNo"].Value.ToString()))
                            //    {
                            //        DialogResult dialoga = MessageBox.Show("Enter Proper Demat A.c No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //        error++;
                            //    }
                            //}

                            if (row.Cells["relativeEmailID"].Value.ToString() != "")
                            {
                                if (!new MasterClass().IsValidEmail(row.Cells["relativeEmailID"].Value.ToString()))
                                {
                                    DialogResult dialoga = MessageBox.Show("Enter Proper Email ID.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    error++;
                                }
                            }

                            if (error == 0)
                            {
                                T_INS_PER_IMMEDAITE_RELATIVES RELATIVEARRAY = new T_INS_PER_IMMEDAITE_RELATIVES
                                {
                                    NAME = row.Cells["FullName"].Value.ToString(),
                                    MOBILE_NO = row.Cells["MobileNo"].Value.ToString(),
                                    PAN_NO = row.Cells["PANNo"].Value.ToString(),
                                    DEMAT_AC_NO = row.Cells["DematAcNo"].Value.ToString(),
                                    RELATIONSHIP = row.Cells["Relationship"].Value.ToString(),
                                    EMAILID = row.Cells["relativeEmailID"].Value.ToString(),
                                    ADDRESS = row.Cells["Address"].Value.ToString()
                                };

                                T_INS_PER_IMMEDAITE_RELATIVES.Add(RELATIVEARRAY);
                            }
                        }
                    }
                    CNS.RELATIVEARRAY = T_INS_PER_IMMEDAITE_RELATIVES.ToArray();

                    List<T_INS_PER_MATERIAL_FINANCIAL> T_INS_PER_MATERIAL_FINANCIAL = new List<T_INS_PER_MATERIAL_FINANCIAL>();

                    foreach (DataGridViewRow row in dataGridViewISNCONrelativetable.Rows)
                    {
                        if (row.Cells["Type"].Value.ToString() != "My Immediate Relative's")
                        {
                            if (row.Cells["PANNo"].Value.ToString() != "")
                            {
                                if (!new MasterClass().IsValidPanno(row.Cells["PANNo"].Value.ToString()))
                                {
                                    DialogResult dialoga = MessageBox.Show("Enter Proper PAN No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    error++;
                                }
                            }

                            //if (row.Cells["DematAcNo"].Value.ToString() != "")
                            //{
                            //    if (!new MasterClass().IsValidDematAcno(row.Cells["DematAcNo"].Value.ToString()))
                            //    {
                            //        DialogResult dialoga = MessageBox.Show("Enter Proper Demat A.c No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //        error++;
                            //    }
                            //}

                            if (row.Cells["relativeEmailID"].Value.ToString() != "")
                            {
                                if (!new MasterClass().IsValidEmail(row.Cells["relativeEmailID"].Value.ToString()))
                                {
                                    DialogResult dialoga = MessageBox.Show("Enter Proper Email ID.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    error++;
                                }
                            }

                            if (error == 0)
                            {
                                T_INS_PER_MATERIAL_FINANCIAL FINANCIALARRAY = new T_INS_PER_MATERIAL_FINANCIAL
                                {
                                    NAME = row.Cells["FullName"].Value.ToString(),
                                    MOBILE_NO = row.Cells["MobileNo"].Value.ToString(),
                                    PAN_NO = row.Cells["PANNo"].Value.ToString(),
                                    DEMAT_AC_NO = row.Cells["DematAcNo"].Value.ToString(),
                                    RELATIONSHIP = row.Cells["Relationship"].Value.ToString(),
                                    EMAILID = row.Cells["relativeEmailID"].Value.ToString(),
                                    ADDRESS = row.Cells["Address"].Value.ToString()
                                };
                                T_INS_PER_MATERIAL_FINANCIAL.Add(FINANCIALARRAY);
                            }
                        }
                    }
                    CNS.FINANCIALARRAY = T_INS_PER_MATERIAL_FINANCIAL.ToArray();

                    if (error == 0)
                    {
                        foreach (T_INS_PER_MATERIAL_FINANCIAL m_x in CNS.FINANCIALARRAY)
                        {
                            string dss = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT(PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK)  VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N')").ToString();
                            string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dss + "','" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N')").ToString();
                            lg.CURRVALUE = "DESIGNATED PERSON TAB FINANCIAL RELATIONSHIP";
                            lg.TYPE = "INSERTED";
                            lg.ID = perlogisdt;
                            lg.DESCRIPTION = "INSERTED VALUE :- " + txtINSCONconnectperson.Text;
                            lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                            //&& = SESSIONKEYS.UserID.ToString();
                            new MasterClass().SAVE_LOG(lg);
                        }

                        foreach (T_INS_PER_IMMEDAITE_RELATIVES m_x in CNS.RELATIVEARRAY)
                        {
                            string dss = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT(PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK)  VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N')").ToString();
                            string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dss + "','" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N')").ToString();
                            lg.CURRVALUE = "DESIGNATED PERSON TAB RELATIVE RELATIONSHIP";
                            lg.TYPE = "INSERTED";
                            lg.ID = perlogisdt;
                            lg.DESCRIPTION = "INSERTED VALUE :- " + txtINSCONconnectperson.Text;
                            lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                            //&& = SESSIONKEYS.UserID.ToString();
                            new MasterClass().SAVE_LOG(lg);
                        }
                        DialogResult dialog = MessageBox.Show("Inserted Designated Person Relative & Financial Relationship.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                        btnupdateINSCON.Visible = false;
                        btnaddINSCONdeelete.Visible = false;
                        btncacncelINSCON.Visible = false;
                        btnaddINSCON.Visible = true;
                        FillConnectPersonID();
                        button2click();
                    }
                }
            }
            catch (Exception)
            {
                DialogResult dialog = MessageBox.Show("Data Not Updated. Please Check Your Internet Connection.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Hide();
                }
                else
                {
                    int error = 0;
                    T_INS_PER CNS = new T_INS_PER();
                    List<T_INS_PER_IMMEDAITE_RELATIVES> T_INS_PER_IMMEDAITE_RELATIVES = new List<T_INS_PER_IMMEDAITE_RELATIVES>();
                    foreach (DataGridViewRow row in dataGridViewISNCONrelativetable.Rows)
                    {
                        row.Cells["Type"].Value = row.Cells["Type"].Value == null ? "" : row.Cells["Type"].Value.ToString();
                        row.Cells["RelativeID"].Value = row.Cells["RelativeID"].Value == null ? "" : row.Cells["RelativeID"].Value.ToString();
                        row.Cells["FullName"].Value = row.Cells["FullName"].Value == null ? "" : row.Cells["FullName"].Value.ToString();
                        row.Cells["MobileNo"].Value = row.Cells["MobileNo"].Value == null ? "" : row.Cells["MobileNo"].Value.ToString();
                        row.Cells["PANNo"].Value = row.Cells["PANNo"].Value == null ? "" : row.Cells["PANNo"].Value.ToString();
                        row.Cells["DematAcNo"].Value = row.Cells["DematAcNo"].Value == null ? "" : row.Cells["DematAcNo"].Value.ToString();
                        row.Cells["Relationship"].Value = row.Cells["Relationship"].Value == null ? "" : row.Cells["Relationship"].Value.ToString();
                        row.Cells["Address"].Value = row.Cells["Address"].Value == null ? "" : row.Cells["Address"].Value.ToString();
                        row.Cells["relativeEmailID"].Value = row.Cells["relativeEmailID"].Value == null ? "" : row.Cells["relativeEmailID"].Value.ToString();
                        if (row.Cells["Type"].Value.ToString() == "My Immediate Relative's")
                        {
                            if (row.Cells["PANNo"].Value.ToString() != "")
                            {
                                if (!new MasterClass().IsValidPanno(row.Cells["PANNo"].Value.ToString()))
                                {
                                    DialogResult dialoga = MessageBox.Show("Enter Proper PAN No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    error++;
                                }
                            }

                            if (row.Cells["relativeEmailID"].Value.ToString() != "")
                            {
                                if (!new MasterClass().IsValidEmail(row.Cells["relativeEmailID"].Value.ToString()))
                                {
                                    DialogResult dialoga = MessageBox.Show("Enter Proper Email ID.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                    ADDRESS = row.Cells["Address"].Value == null ? "" : row.Cells["Address"].Value.ToString(),
                                    EMAILID = row.Cells["relativeEmailID"].Value == null ? "" : row.Cells["relativeEmailID"].Value.ToString()
                                };
                                T_INS_PER_IMMEDAITE_RELATIVES.Add(RELATIVEARRAY);
                            }
                        }
                    }
                    CNS.RELATIVEARRAY = T_INS_PER_IMMEDAITE_RELATIVES.ToArray();

                    List<T_INS_PER_MATERIAL_FINANCIAL> T_INS_PER_MATERIAL_FINANCIAL = new List<T_INS_PER_MATERIAL_FINANCIAL>();

                    foreach (DataGridViewRow row in dataGridViewISNCONrelativetable.Rows)
                    {
                        row.Cells["Type"].Value = row.Cells["Type"].Value == null ? "" : row.Cells["Type"].Value.ToString();
                        row.Cells["RelativeID"].Value = row.Cells["RelativeID"].Value == null ? "" : row.Cells["RelativeID"].Value.ToString();
                        row.Cells["FullName"].Value = row.Cells["FullName"].Value == null ? "" : row.Cells["FullName"].Value.ToString();
                        row.Cells["MobileNo"].Value = row.Cells["MobileNo"].Value == null ? "" : row.Cells["MobileNo"].Value.ToString();
                        row.Cells["PANNo"].Value = row.Cells["PANNo"].Value == null ? "" : row.Cells["PANNo"].Value.ToString();
                        row.Cells["DematAcNo"].Value = row.Cells["DematAcNo"].Value == null ? "" : row.Cells["DematAcNo"].Value.ToString();
                        row.Cells["Relationship"].Value = row.Cells["Relationship"].Value == null ? "" : row.Cells["Relationship"].Value.ToString();
                        row.Cells["Address"].Value = row.Cells["Address"].Value == null ? "" : row.Cells["Address"].Value.ToString();
                        row.Cells["relativeEmailID"].Value = row.Cells["relativeEmailID"].Value == null ? "" : row.Cells["relativeEmailID"].Value.ToString();
                        if (row.Cells["Type"].Value.ToString() != "My Immediate Relative's")
                        {
                            if (row.Cells["PANNo"].Value.ToString() != "")
                            {
                                if (!new MasterClass().IsValidPanno(row.Cells["PANNo"].Value.ToString()))
                                {
                                    DialogResult dialoga = MessageBox.Show("Enter Proper PAN No.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    error++;
                                }
                            }

                            if (row.Cells["relativeEmailID"].Value.ToString() != "")
                            {
                                if (!new MasterClass().IsValidEmail(row.Cells["relativeEmailID"].Value.ToString()))
                                {
                                    DialogResult dialoga = MessageBox.Show("Enter Proper Email ID.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    error++;
                                }
                            }

                            if (error == 0)
                            {
                                T_INS_PER_MATERIAL_FINANCIAL FINANCIALARRAY = new T_INS_PER_MATERIAL_FINANCIAL
                                {
                                    ID = row.Cells["RelativeID"].Value == null ? "" : row.Cells["RelativeID"].Value.ToString(),
                                    NAME = row.Cells["FullName"].Value == null ? "" : row.Cells["FullName"].Value.ToString(),
                                    MOBILE_NO = row.Cells["MobileNo"].Value == null ? "" : row.Cells["MobileNo"].Value.ToString(),
                                    PAN_NO = row.Cells["PANNo"].Value == null ? "" : row.Cells["PANNo"].Value.ToString(),
                                    DEMAT_AC_NO = row.Cells["DematAcNo"].Value == null ? "" : row.Cells["DematAcNo"].Value.ToString(),
                                    RELATIONSHIP = row.Cells["Relationship"].Value == null ? "" : row.Cells["Relationship"].Value.ToString(),
                                    ADDRESS = row.Cells["Address"].Value == null ? "" : row.Cells["Address"].Value.ToString(),
                                    EMAILID = row.Cells["relativeEmailID"].Value == null ? "" : row.Cells["relativeEmailID"].Value.ToString()
                                };
                                T_INS_PER_MATERIAL_FINANCIAL.Add(FINANCIALARRAY);
                            }
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
                                dss = new MasterClass().executeQuery("UPDATE T_INS_PER_DT SET PERID =  '" + lblidforcp.Text + "',NAME='" + CryptographyHelper.Encrypt(m_x.NAME) + "',ADDRESS ='" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "',RELATIONSHIP = '" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "',MOBILENO = '" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "',PANNO = '" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "',DEMATACNO = '" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "',EMAILID = '" + CryptographyHelper.Encrypt(m_x.EMAILID) + "',ACTIVE = 'Y' WHERE ID = '" + m_x.ID + "' ;").ToString();

                                string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + m_x.ID + "','" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();
                                lg.CURRVALUE = "DESIGNATED PERSON TAB FINANCIAL RELATIONSHIP";
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
                                dss = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT(PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK)  VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N')").ToString();

                                string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dss + "','" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("FINANCIAL") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();
                                lg.CURRVALUE = "DESIGNATED PERSON TAB FINANCIAL RELATIONSHIP";
                                lg.TYPE = "UPDATED";
                                lg.ID = perlogisdt;
                                lg.DESCRIPTION = "UPDATED VALUE :- " + CNS.CONNECT_PERSON_ID;
                                lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                                //lg.ID = SESSIONKEYS.UserID.ToString();
                                new MasterClass().SAVE_LOG(lg);
                            }



                        }
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
                                dss = new MasterClass().executeQuery("UPDATE T_INS_PER_DT SET PERID =  '" + lblidforcp.Text + "',NAME='" + CryptographyHelper.Encrypt(m_x.NAME) + "',ADDRESS ='" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "',RELATIONSHIP = '" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "',MOBILENO = '" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "',PANNO = '" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "',DEMATACNO = '" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "',EMAILID = '" + CryptographyHelper.Encrypt(m_x.EMAILID) + "',ACTIVE = 'Y' WHERE ID = '" + m_x.ID + "' ;").ToString();

                                string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + m_x.ID + "','" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();
                                lg.CURRVALUE = "DESIGNATED PERSON TAB RELATIVE RELATIONSHIP";
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
                                dss = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT(PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK)  VALUES ('" + lblidforcp.Text + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N')").ToString();

                                string perlogisdt = new MasterClass().executeQuery("INSERT INTO T_INS_PER_DT_LOG(TID,PERID,NAME,ADDRESS,RELATIONSHIP,MOBILENO,PANNO,DEMATACNO,EMAILID,TYPE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + dss + "','" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + CryptographyHelper.Encrypt(m_x.NAME) + "','" + CryptographyHelper.Encrypt(m_x.ADDRESS) + "','" + CryptographyHelper.Encrypt(m_x.RELATIONSHIP) + "','" + CryptographyHelper.Encrypt(m_x.MOBILE_NO) + "','" + CryptographyHelper.Encrypt(m_x.PAN_NO) + "','" + CryptographyHelper.Encrypt(m_x.DEMAT_AC_NO) + "','" + CryptographyHelper.Encrypt(m_x.EMAILID) + "','" + CryptographyHelper.Encrypt("RELATIVE") + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N')").ToString();
                                lg.CURRVALUE = "DESIGNATED PERSON TAB RELATIVE RELATIONSHIP";
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
                                lg.CURRVALUE = "DESIGNATED PERSON TAB RELATIVE RELATIONSHIP";
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
                                lg.CURRVALUE = "DESIGNATED PERSON TAB FINANCIAL RELATIONSHIP";
                                lg.TYPE = "DELETED";
                                lg.ID = financial[i];
                                lg.DESCRIPTION = "DELETED VALUE :- " + CNS.CONNECT_PERSON_ID;
                                lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                                //lg.ID = SESSIONKEYS.UserID.ToString();
                                new MasterClass().SAVE_LOG(lg);
                                financial = financial.Where(val => val != financial[i]).ToArray();
                            }
                        }

                        DialogResult dialog = MessageBox.Show("Updated Designated Person Relative & Financial Relationship.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                        btnupdateINSCON.Visible = false;
                        btnaddINSCONdeelete.Visible = false;
                        btncacncelINSCON.Visible = false;
                        btnaddINSCON.Visible = true;
                        FillConnectPersonID();
                        button2click();
                    }
                }
            }
            catch (Exception)
            {
                DialogResult dialog = MessageBox.Show("Data Not Updated. Please Check Your Internet Connection.", "Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuFlatButton1_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, bunifuFlatButton1.Width, bunifuFlatButton1.Height, 15, 15);
            bunifuFlatButton1.Region = Region.FromHrgn(ptr);
        }

        private void bunifuFlatButton2_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, bunifuFlatButton2.Width, bunifuFlatButton2.Height, 15, 15);
            bunifuFlatButton2.Region = Region.FromHrgn(ptr);
        }

        private void btnaddINSCON_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, btnaddINSCON.Width, btnaddINSCON.Height, 15, 15);
            btnaddINSCON.Region = Region.FromHrgn(ptr);
        }

        private void btnupdateINSCON_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, btnupdateINSCON.Width, btnupdateINSCON.Height, 15, 15);
            btnupdateINSCON.Region = Region.FromHrgn(ptr);
        }

        private void btnaddINSCONdeelete_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, btnaddINSCONdeelete.Width, btnaddINSCONdeelete.Height, 15, 15);
            btnaddINSCONdeelete.Region = Region.FromHrgn(ptr);
        }

        private void btncacncelINSCON_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, btncacncelINSCON.Width, btncacncelINSCON.Height, 15, 15);
            btncacncelINSCON.Region = Region.FromHrgn(ptr);
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // width of ellipse
           int nHeightEllipse // height of ellipse
       );

        private void btnaddrelafinaci_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, btnaddrelafinaci.Width, btnaddrelafinaci.Height, 15, 15);
            btnaddrelafinaci.Region = Region.FromHrgn(ptr);
        }

        private void btnupdatefinanrel_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, btnupdatefinanrel.Width, btnupdatefinanrel.Height, 15, 15);
            btnupdatefinanrel.Region = Region.FromHrgn(ptr);
        }

        private void CMBTYPE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < 32 || e.KeyChar > 126)
            {
                return;
            }
            string t = CMBTYPE.Text;
            string typedT = t.Substring(0, CMBTYPE.SelectionStart);
            string newT = typedT + e.KeyChar;

            int i = CMBTYPE.FindString(newT);
            if (i == -1)
            {
                e.Handled = true;
            }
        }

        private void CMBTYPE_Leave(object sender, EventArgs e)
        {
            string t = CMBTYPE.Text;

            if (CMBTYPE.SelectedItem == null)
            {
                CMBTYPE.Text = "";
            }
        }
    }
}
