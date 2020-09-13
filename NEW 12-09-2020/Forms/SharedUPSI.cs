using RSACryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using The_PIT_Archive.Data_Access_Layer;
using The_PIT_Archive.Data_Entity;

namespace The_PIT_Archive.Forms
{
    public partial class SharedUPSI : Form
    {
        AUDITLOG lg = new AUDITLOG();
        public SharedUPSI()
        {
            InitializeComponent();
        }

        private void SharedUPSI_Load(object sender, EventArgs e)
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

        private void dataGridViewPhonemobile_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Sharing of UPSI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    dataGridViewPhonemobile.Rows.RemoveAt(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Sharing of UPSI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("You cant Remove the Header Row.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnINSCONaddnumber_Click(object sender, EventArgs e)
        {
            try
            {
                int error = 0;
                if (dataGridViewPhonemobile.Rows.Count < 15 && dataGridView1.Rows.Count < 15)
                {
                    if (recpientradioButton.Checked == true || informantradioButton.Checked == true)
                    {
                        if (txtUPSINAME.SelectedItem != null)
                        {
                            if (recpientradioButton.Checked == true)
                            {
                                foreach (DataGridViewRow rows in dataGridViewPhonemobile.Rows)
                                {
                                    if (txtUPSINAME.SelectedItem.ToString() == rows.Cells["Namedg"].Value.ToString())
                                    {
                                        DialogResult dialog = MessageBox.Show("Following Record is already Added in Recepeint Tab.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        error++;
                                        break;
                                    }
                                }

                                foreach (DataGridViewRow rows in dataGridView1.Rows)
                                {
                                    if (txtUPSINAME.SelectedItem.ToString() == rows.Cells["Namdginfo"].Value.ToString())
                                    {
                                        DialogResult dialog = MessageBox.Show("Following Record is already Added in Informant Tab.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        error++;
                                        break;
                                    }
                                }
                            }
                            if (informantradioButton.Checked == true)
                            {
                                foreach (DataGridViewRow rows in dataGridViewPhonemobile.Rows)
                                {
                                    if (txtUPSINAME.SelectedItem.ToString() == rows.Cells["Namedg"].Value.ToString())
                                    {
                                        DialogResult dialog = MessageBox.Show("Following Record is already Added in Recepeint Tab.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        error++;
                                        break;
                                    }
                                }

                                foreach (DataGridViewRow rows in dataGridView1.Rows)
                                {
                                    if (txtUPSINAME.SelectedItem.ToString() == rows.Cells["Namdginfo"].Value.ToString())
                                    {
                                        DialogResult dialog = MessageBox.Show("Following Record is already Added in Informant Tab.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        error++;
                                        break;
                                    }
                                }
                            }

                            if (error == 0)
                            {
                                string[] a = txtUPSINAME.SelectedItem.ToString().Split('-');
                                DataSet ds1 = new MasterClass().getDataSet("SELECT * FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N' AND ID = '" + ((ComboboxItem1)txtUPSINAME.SelectedItem).UPSIID + "'");
                                DataSet ds2 = new MasterClass().getDataSet("SELECT * FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N' AND ID = '" + ((ComboboxItem1)txtUPSINAME.SelectedItem).UPSIID + "'");
                                //txtUPSIOthercategory.Text = "";
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    if (CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["RECEPIENTID"].ToString()) == a[1].Trim())
                                    {
                                        string first, second, third, four, fifith, sixth = "";
                                        first = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["NAMEINSIDER"].ToString()) + " - " + CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["RECEPIENTID"].ToString());
                                        four = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["ADDRESS"].ToString());
                                        if (CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["PANNO"].ToString()).Trim() == "")
                                        {
                                            second = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["OTHERIDENTIFIER"].ToString());
                                        }
                                        else
                                        {
                                            second = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["PANNO"].ToString());
                                        }

                                        //lblnNDS.Text = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["PANNOAFFILIATES"].ToString());
                                        //txtUPSIcategory.Text = "";
                                        if (CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString()).Contains("OTHERS"))
                                        {
                                            string[] abc = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString()).Split('|');
                                            third = abc[0] + "-" + abc[1];
                                        }
                                        else
                                        {
                                            third = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[0]["CATEGORYRECEIPT"].ToString());
                                        }
                                        fifith = ds1.Tables[0].Rows[0]["ID"].ToString();

                                        if (recpientradioButton.Checked == true)
                                        {
                                            sixth = "Recipient";
                                            string[] row = { first, sixth, fifith, third };
                                            dataGridViewPhonemobile.Rows.Add(row);
                                        }
                                        else if (informantradioButton.Checked == true)
                                        {
                                            sixth = "Informants";
                                            string[] row = { first, sixth, fifith, third };
                                            dataGridView1.Rows.Add(row);
                                        }
                                        txtUPSINAME.SelectedItem = null;
                                        //txtUPSINAME.Items.RemoveAt(txtUPSINAME.SelectedIndex);
                                    }
                                }

                                if (ds2.Tables[0].Rows.Count > 0)
                                {
                                    string first, second, third, four, fifith, sixth = "";



                                    if (CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["CONNECTPERSONID"].ToString()) == a[1].Trim())
                                    {
                                        first = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["EMPNAME"].ToString()) + " - " + CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["CONNECTPERSONID"].ToString());

                                        four = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["ADDRESS"].ToString());
                                        if (CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["PANNO"].ToString()).Trim() == "")
                                        {
                                            second = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["OTHERIDENTIFIER"].ToString());
                                        }
                                        else
                                        {
                                            second = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[0]["PANNO"].ToString());
                                        }
                                        third = "Designated Person";
                                        //lblnNDS.Text = "";
                                        fifith = ds2.Tables[0].Rows[0]["ID"].ToString();
                                        if (recpientradioButton.Checked == true)
                                        {
                                            sixth = "Recipient";
                                            string[] row = { first, sixth, fifith, third };
                                            dataGridViewPhonemobile.Rows.Add(row);
                                        }
                                        else if (informantradioButton.Checked == true)
                                        {
                                            sixth = "Informants";
                                            string[] row = { first, sixth, fifith, third };
                                            dataGridView1.Rows.Add(row);
                                        }

                                        txtUPSINAME.SelectedItem = null;
                                        //txtUPSINAME.Items.RemoveAt(txtUPSINAME.SelectedIndex);
                                    }
                                }
                            }
                            else
                            {
                                txtUPSINAME.SelectedItem = null;
                            }
                        }
                        else
                        {
                            DialogResult dialog = MessageBox.Show("Please Provide Values.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        DialogResult dialog = MessageBox.Show("Select Either Recipient/Informants.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("You Can Add Only 15 Recipents.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUPSINAME.Text = "";
                }

            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            btnINSCONaddnumber.Enabled = true;
        }

        void button2click()
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
            btnINSCONaddnumber.Enabled = true;
        }

        private void txtUPSINAME_Leave(object sender, EventArgs e)
        {
            string t = txtUPSINAME.Text;

            if (txtUPSINAME.SelectedItem == null)
            {
                txtUPSINAME.Text = "";
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
                else
                {
                    string ds = "";
                    T_INS_UPSI PRO = new T_INS_UPSI
                    {
                        UPSIID = txtUPSIID.Text,
                        UPSINATURE = txtUPSINatureUPSI.Text,
                        SHARINGPURPOSE = txtUPSIpurposesharing.Text,
                        SHARINGDATE = txtUPSIDateofsharing.Value.ToString(),
                        EFFECTIVEUPTO = txtUPSIEffctiveUpto.Value.ToString(),
                        REMARKS = txtUPSIremarks.Text,
                        UPSIAVAILABLE = txtUPSIUPSIavaailabe.Value.ToString(),
                    };

                    foreach (DataGridViewRow row in dataGridViewPhonemobile.Rows)
                    {
                        if (row.Cells["category"].Value.ToString() == "Designated Person")
                        {
                            if (PRO.IDOFDP == "" || PRO.IDOFDP == null)
                            {
                                PRO.IDOFDP += "Recipient:'" + row.Cells["IDrecp"].Value.ToString() + "'";
                            }
                            else
                            {
                                PRO.IDOFDP += "," + "'" + row.Cells["IDrecp"].Value.ToString() + "'";
                            }
                        }
                        else
                        {
                            if (PRO.IDOFIP == "" || PRO.IDOFIP == null)
                            {
                                PRO.IDOFIP += "Recipient:'" + row.Cells["IDrecp"].Value.ToString() + "'";
                            }
                            else
                            {
                                PRO.IDOFIP += "," + "'" + row.Cells["IDrecp"].Value.ToString() + "'";
                            }
                        }
                    }
                    PRO.IDOFIP += "|Informants:";
                    PRO.IDOFDP += "|Informants:";
                    string idofp = "";
                    string idofdp = "";
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["categoryin"].Value.ToString() == "Designated Person")
                        {
                            if (idofdp == "")
                            {
                                idofdp += "'" + row.Cells["IDinfo"].Value.ToString() + "'";
                            }
                            else
                            {
                                idofdp += "," + "'" + row.Cells["IDinfo"].Value.ToString() + "'";
                            }
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["categoryin"].Value.ToString() != "Designated Person")
                        {
                            if (idofp == "")
                            {
                                idofp += "'" + row.Cells["IDinfo"].Value.ToString() + "'";
                            }
                            else
                            {
                                idofp += "," + "'" + row.Cells["IDinfo"].Value.ToString() + "'";
                            }
                        }
                    }

                    PRO.IDOFIP += idofp;
                    PRO.IDOFDP += idofdp;


                    if (radioButtonNDAYES.Checked == true)
                    {
                        PRO.NDASIGNED = "YES";
                    }
                    else if (radioButtonNDANo.Checked == true)
                    {
                        PRO.NDASIGNED = "NO";
                    }

                    PRO.ID = "";
                    PRO.ENTEREDBY = SESSIONKEYS.UserID.ToString();

                    DialogResult dialogResult;
                    if (btnaddINSCONdeelete.Text == "RETREIVE")
                    {
                        dialogResult = MessageBox.Show("Are You Sure You Want to Retreive?", "Sharing of UPSI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            ds = new MasterClass().executeQueryForDB("UPDATE T_INS_UPSI  SET ACTIVE = 'Y',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + MasterClass.GETIST() + "' WHERE ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "' ; ").ToString();

                            string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_UPSI_LOG (TID,UPSIID,IDOFIP,IDOFDP,UPSINATURE,SHARINGPURPOSE,SHARINGDATE,EFFECTIVEUPTO,REMARKS,NDASIGNED,UPSIAVAILABLE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + CryptographyHelper.Encrypt(PRO.UPSIID) + "','" + CryptographyHelper.Encrypt(PRO.IDOFIP) + "','" + CryptographyHelper.Encrypt(PRO.IDOFDP) + "','" + CryptographyHelper.Encrypt(PRO.UPSINATURE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGDATE) + "','" + CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) + "','" + CryptographyHelper.Encrypt(PRO.REMARKS) + "','" + CryptographyHelper.Encrypt(PRO.NDASIGNED) + "','" + CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("RETREIVE") + "','Y','N') ;").ToString();

                            lg.CURRVALUE = "SHARING OF UPSI PROFILE TAB";
                            lg.TYPE = "RETREIVE";
                            lg.ID = perlogid;
                            lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                            //lg.ID = SESSIONKEYS.UserID.ToString();
                            lg.DESCRIPTION = "DELETED VALUE :- " + PRO.UPSIID;
                            new MasterClass().SAVE_LOG(lg);
                        }
                    }
                    else
                    {
                        dialogResult = MessageBox.Show("Are You Sure You Want to Delete?", "Sharing of UPSI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            ds = new MasterClass().executeQueryForDB("UPDATE T_INS_UPSI  SET ACTIVE = 'N',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + MasterClass.GETIST() + "' WHERE ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "' ; ").ToString();

                            string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_UPSI_LOG (TID,UPSIID,IDOFIP,IDOFDP,UPSINATURE,SHARINGPURPOSE,SHARINGDATE,EFFECTIVEUPTO,REMARKS,NDASIGNED,UPSIAVAILABLE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + CryptographyHelper.Encrypt(PRO.UPSIID) + "','" + CryptographyHelper.Encrypt(PRO.IDOFIP) + "','" + CryptographyHelper.Encrypt(PRO.IDOFDP) + "','" + CryptographyHelper.Encrypt(PRO.UPSINATURE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGDATE) + "','" + CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) + "','" + CryptographyHelper.Encrypt(PRO.REMARKS) + "','" + CryptographyHelper.Encrypt(PRO.NDASIGNED) + "','" + CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("DELETED") + "','Y','N') ;").ToString();

                            lg.CURRVALUE = "SHARING OF UPSI PROFILE TAB";
                            lg.TYPE = "DELETED";
                            lg.ID = perlogid;
                            lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                            //lg.ID = SESSIONKEYS.UserID.ToString();
                            lg.DESCRIPTION = "DELETED VALUE :- " + PRO.UPSIID;
                            new MasterClass().SAVE_LOG(lg);
                        }


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
                            button2click();
                        }
                        else
                        {
                            DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                DialogResult dialog = MessageBox.Show("Data Not Deleted. Please Check Your Internet Connection.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnupdateINSCON_Click(object sender, EventArgs e)
        {
            try
            {
                //SetLoading(true);

                //Thread.Sleep(2000);
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
                    Hide();
                }
                else
                if (txtUPSIID.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Enter UPSI ID.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dataGridViewPhonemobile.Rows.Count == 0)
                {
                    DialogResult dialog = MessageBox.Show("Enter Atleast 1 Recepient Name.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string ds = "";
                    T_INS_UPSI PRO = new T_INS_UPSI
                    {
                        UPSIID = txtUPSIID.Text,
                        UPSINATURE = txtUPSINatureUPSI.Text,
                        SHARINGPURPOSE = txtUPSIpurposesharing.Text,
                        SHARINGDATE = txtUPSIDateofsharing.Value.ToString(),
                        EFFECTIVEUPTO = txtUPSIEffctiveUpto.Value.ToString(),
                        REMARKS = txtUPSIremarks.Text,
                        UPSIAVAILABLE = txtUPSIUPSIavaailabe.Value.ToString(),
                    };

                    foreach (DataGridViewRow row in dataGridViewPhonemobile.Rows)
                    {
                        if (row.Cells["category"].Value.ToString() == "Designated Person")
                        {
                            if (PRO.IDOFDP == "" || PRO.IDOFDP == null)
                            {
                                PRO.IDOFDP += "Recipient:'" + row.Cells["IDrecp"].Value.ToString() + "'";
                            }
                            else
                            {
                                PRO.IDOFDP += "," + "'" + row.Cells["IDrecp"].Value.ToString() + "'";
                            }
                        }
                        else
                        {
                            if (PRO.IDOFIP == "" || PRO.IDOFIP == null)
                            {
                                PRO.IDOFIP += "Recipient:'" + row.Cells["IDrecp"].Value.ToString() + "'";
                            }
                            else
                            {
                                PRO.IDOFIP += "," + "'" + row.Cells["IDrecp"].Value.ToString() + "'";
                            }
                        }
                    }
                    PRO.IDOFIP += "|Informants:";
                    PRO.IDOFDP += "|Informants:";
                    string idofp = "";
                    string idofdp = "";
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["categoryin"].Value.ToString() == "Designated Person")
                        {
                            if (idofdp == "")
                            {
                                idofdp += "'" + row.Cells["IDinfo"].Value.ToString() + "'";
                            }
                            else
                            {
                                idofdp += "," + "'" + row.Cells["IDinfo"].Value.ToString() + "'";
                            }
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["categoryin"].Value.ToString() != "Designated Person")
                        {
                            if (idofp == "")
                            {
                                idofp += "'" + row.Cells["IDinfo"].Value.ToString() + "'";
                            }
                            else
                            {
                                idofp += "," + "'" + row.Cells["IDinfo"].Value.ToString() + "'";
                            }
                        }
                    }

                    PRO.IDOFIP += idofp;
                    PRO.IDOFDP += idofdp;

                    if (radioButtonNDAYES.Checked == true)
                    {
                        PRO.NDASIGNED = "YES";
                    }
                    else if (radioButtonNDANo.Checked == true)
                    {
                        PRO.NDASIGNED = "NO";
                    }

                    PRO.ID = "";
                    PRO.ENTEREDBY = SESSIONKEYS.UserID.ToString();

                    DataSet getval = new MasterClass().getDataSet("SELECT ID FROM T_INS_UPSI_LOG WHERE ACTIVE = 'Y' ORDER BY ENTEREDON DESC");

                    ds = new MasterClass().executeQueryForDB("UPDATE T_INS_UPSI SET UPSIID = '" + CryptographyHelper.Encrypt(PRO.UPSIID) + "',IDOFIP = '" + CryptographyHelper.Encrypt(PRO.IDOFIP) + "',IDOFDP = '" + CryptographyHelper.Encrypt(PRO.IDOFDP) + "',UPSINATURE = '" + CryptographyHelper.Encrypt(PRO.UPSINATURE) + "',SHARINGPURPOSE = '" + CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) + "',SHARINGDATE = '" + CryptographyHelper.Encrypt(PRO.SHARINGDATE) + "',EFFECTIVEUPTO = '" + CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) + "',REMARKS = '" + CryptographyHelper.Encrypt(PRO.REMARKS) + "',NDASIGNED = '" + CryptographyHelper.Encrypt(PRO.NDASIGNED) + "',UPSIAVAILABLE = '" + CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) + "',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' WHERE ID = '" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "' ; ").ToString();

                    string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_UPSI_LOG (TID,UPSIID,IDOFIP,IDOFDP,UPSINATURE,SHARINGPURPOSE,SHARINGDATE,EFFECTIVEUPTO,REMARKS,NDASIGNED,UPSIAVAILABLE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ((ComboboxItem)cmdINSCONSAVEID.SelectedItem).ID.ToString() + "','" + CryptographyHelper.Encrypt(PRO.UPSIID) + "','" + CryptographyHelper.Encrypt(PRO.IDOFIP) + "','" + CryptographyHelper.Encrypt(PRO.IDOFDP) + "','" + CryptographyHelper.Encrypt(PRO.UPSINATURE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGDATE) + "','" + CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) + "','" + CryptographyHelper.Encrypt(PRO.REMARKS) + "','" + CryptographyHelper.Encrypt(PRO.NDASIGNED) + "','" + CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N') ;").ToString();

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
                    button2click();
                }
                //});

                //SetLoading(false);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                //SetLoading(false);
                DialogResult dialog = MessageBox.Show("Data Not Updated. Please Check Your Internet Connection.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            button2click();
        }

        public void Clear()
        {
            //lblnNDS.Text = "";
            txtUPSIID.Text = "";
            txtUPSINAME.Text = "";
            txtUPSINatureUPSI.Text = "";
            txtUPSIpurposesharing.Text = "";
            txtUPSIDateofsharing.Text = "";
            txtUPSIEffctiveUpto.Text = "";
            txtUPSIremarks.Text = "";
            txtUPSIUPSIavaailabe.Text = "";
            radioButtonNDAYES.Checked = false;
            radioButtonNDANo.Checked = false;
            dataGridViewPhonemobile.Rows.Clear();
            dataGridViewPhonemobile.Refresh();

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
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
                                dataGridViewPhonemobile.Rows.Clear();
                                dataGridViewPhonemobile.Refresh();
                                dataGridView1.Rows.Clear();
                                dataGridView1.Refresh();
                                //txtUPSINAME.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RECIPIENTNAME"].ToString());
                                txtUPSINatureUPSI.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["UPSINATURE"].ToString());
                                txtUPSIpurposesharing.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["SHARINGPURPOSE"].ToString());
                                if (CryptographyHelper.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["SHARINGDATE"].ToString())).Trim() == "")
                                {
                                    //txtUPSIDateofsharing.Value = "";
                                }
                                else
                                {
                                    string a = CryptographyHelper.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["SHARINGDATE"].ToString()));
                                    //txtUPSIDateofsharing.Value = DateTime.ParseExact(CryptographyHelper.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["SHARINGDATE"].ToString())), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                    txtUPSIDateofsharing.Value = Convert.ToDateTime(a);
                                }

                                if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["EFFECTIVEUPTO"].ToString()).Trim() == "")
                                {
                                    //txtUPSIEffctiveUpto.CustomFormat = " ";
                                }
                                else
                                {
                                    txtUPSIEffctiveUpto.Value = Convert.ToDateTime(CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["EFFECTIVEUPTO"].ToString()));
                                }

                                txtUPSIremarks.Text = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["REMARKS"].ToString());

                                if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["UPSIAVAILABLE"].ToString()).Trim() == "")
                                {
                                    //txtUPSIUPSIavaailabe.CustomFormat = " ";
                                }
                                else
                                {
                                    txtUPSIUPSIavaailabe.Value = Convert.ToDateTime(CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["UPSIAVAILABLE"].ToString()));
                                }

                                string idip = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["IDOFIP"].ToString());
                                string iddp = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["IDOFDP"].ToString());
                                DataSet dsIPrec = new DataSet();
                                DataSet dsIPinf = new DataSet();
                                DataSet dsDPrec = new DataSet();
                                DataSet dsDPinf = new DataSet();
                                if (idip.Trim() != "" && idip.Trim() != null)
                                {
                                    string[] a = idip.Split('|');
                                    string[] rec = a[0].Split(':');
                                    if (rec[1] != null || rec[1] != "")
                                    {
                                        dsIPrec = new MasterClass().getDataSet("SELECT * FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                    }

                                    string[] recA = a[1].Split(':');
                                    if (recA[1].Trim() != null && recA[1].Trim() != "")
                                    {
                                        dsIPinf = new MasterClass().getDataSet("SELECT * FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                    }

                                }
                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                {
                                    string[] a = iddp.Split('|');
                                    if (a[0] != "" && a[0] != null)
                                    {
                                        string[] rec = a[0].Split(':');
                                        if (rec[1] != null && rec[1] != "")
                                        {
                                            dsDPrec = new MasterClass().getDataSet("SELECT * FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                        }
                                    }


                                    string[] recA = a[1].Split(':');
                                    if (recA[1] != null && recA[1] != "")
                                    {
                                        dsDPinf = new MasterClass().getDataSet("SELECT * FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                    }

                                }

                                if (dsIPrec.Tables.Count > 0)
                                {
                                    for (int j = 0; j < dsIPrec.Tables[0].Rows.Count; j++)
                                    {
                                        dataGridViewPhonemobile.Rows.Add(CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["NAMEINSIDER"].ToString()) + " - " + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["RECEPIENTID"].ToString()), "Recipient", dsIPrec.Tables[0].Rows[j]["ID"].ToString(), CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["CATEGORYRECEIPT"].ToString()));
                                    }
                                }
                                if (dsIPinf.Tables.Count > 0)
                                {
                                    for (int j = 0; j < dsIPinf.Tables[0].Rows.Count; j++)
                                    {
                                        dataGridView1.Rows.Add(CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["NAMEINSIDER"].ToString()) + " - " + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["RECEPIENTID"].ToString()), "Recipient", dsIPinf.Tables[0].Rows[j]["ID"].ToString(), CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["CATEGORYRECEIPT"].ToString()));
                                        //dataGridView1.Rows.Add(CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["EMPNAME"].ToString()) + " - " + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["CONNECTPERSONID"].ToString()), "Informants", dsIPinf.Tables[0].Rows[j]["ID"].ToString(), "Designated Person");
                                    }
                                }

                                if (dsDPrec.Tables.Count > 0)
                                {
                                    for (int j = 0; j < dsDPrec.Tables[0].Rows.Count; j++)
                                    {
                                        //dataGridViewPhonemobile.Rows.Add(CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["NAMEINSIDER"].ToString()) + " - " + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["RECEPIENTID"].ToString()), "Recipient", dsDPrec.Tables[0].Rows[j]["ID"].ToString(), CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["CATEGORYRECEIPT"].ToString()));
                                        dataGridViewPhonemobile.Rows.Add(CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["EMPNAME"].ToString()) + " - " + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["CONNECTPERSONID"].ToString()), "Informants", dsDPrec.Tables[0].Rows[j]["ID"].ToString(), "Designated Person");
                                    }
                                }
                                if (dsDPinf.Tables.Count > 0)
                                {
                                    for (int j = 0; j < dsDPinf.Tables[0].Rows.Count; j++)
                                    {
                                        dataGridView1.Rows.Add(CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[j]["EMPNAME"].ToString()) + " - " + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[j]["CONNECTPERSONID"].ToString()), "Informants", dsDPinf.Tables[0].Rows[j]["ID"].ToString(), "Designated Person");
                                    }
                                }

                                //if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RECIPIENTCAT"].ToString()).Contains("OTHERS"))
                                //{
                                //	string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RECIPIENTCAT"].ToString()).Split('|');
                                //	txtUPSIcategory.SelectedText = abc[0];
                                //	txtUPSIOthercategory.Text = abc[1];
                                //	label12.Visible = true;
                                //	txtUPSIOthercategory.Visible = true;
                                //}
                                //else
                                //{
                                //	txtUPSIcategory.SelectedText = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["RECIPIENTCAT"].ToString());
                                //}

                                string yes = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["NDASIGNED"].ToString());

                                if (yes == "YES")
                                {
                                    radioButtonNDAYES.Checked = true;
                                }
                                else if (yes == "NO")
                                {
                                    radioButtonNDANo.Checked = true;
                                }

                                //txtUPSIID.ReadOnly = true;
                                btnupdateINSCON.Visible = true;
                                btnaddINSCONdeelete.Visible = true;
                                btncacncelINSCON.Visible = true;
                                btnaddINSCON.Visible = false;
                                txtUPSINAME.Enabled = true;
                                txtUPSIUPSIavaailabe.Enabled = true;
                                btnINSCONaddnumber.Enabled = true;
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
                    button2click();
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnaddINSCON_Click(object sender, EventArgs e)
        {
            try
            {
                //SetLoading(true);

                //Thread.Sleep(2000);
                //Invoke((MethodInvoker)delegate
                //{
                int val = 0;
                int val2 = 0;
                if (txtUPSIEffctiveUpto.Value.ToString().Trim() != "")
                {
                    val = DateTime.Compare(txtUPSIDateofsharing.Value, txtUPSIEffctiveUpto.Value);
                }
                if (txtUPSIUPSIavaailabe.Value.ToString().Trim() != "")
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
                    Hide();
                }
                //if (txtUPSIID.Text == "")
                //{
                //	DialogResult dialog = MessageBox.Show("Enter UPSI ID.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                else if (dataGridViewPhonemobile.Rows.Count == 0)
                {
                    DialogResult dialog = MessageBox.Show("Enter Atleast 1 Recepient Name.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtUPSINatureUPSI.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Enter Nature of UPSI.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtUPSIpurposesharing.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Enter Purpose of Sharing.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtUPSIDateofsharing.Value.ToString() == "")
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
                    string ds = "";
                    string CPID = "UPSI" + new MasterClass().GETUPSIID();
                    T_INS_UPSI PRO = new T_INS_UPSI
                    {
                        UPSIID = txtUPSIID.Text,
                        UPSINATURE = txtUPSINatureUPSI.Text,
                        SHARINGPURPOSE = txtUPSIpurposesharing.Text,
                        SHARINGDATE = txtUPSIDateofsharing.Value.ToString(),
                        EFFECTIVEUPTO = txtUPSIEffctiveUpto.Value.ToString(),
                        REMARKS = txtUPSIremarks.Text,
                        UPSIAVAILABLE = txtUPSIUPSIavaailabe.Value.ToString(),
                    };

                    foreach (DataGridViewRow row in dataGridViewPhonemobile.Rows)
                    {
                        if (row.Cells["category"].Value.ToString() == "Designated Person")
                        {
                            if (PRO.IDOFDP == "" || PRO.IDOFDP == null)
                            {
                                PRO.IDOFDP += "Recipient:'" + row.Cells["IDrecp"].Value.ToString() + "'";
                            }
                            else
                            {
                                PRO.IDOFDP += "," + "'" + row.Cells["IDrecp"].Value.ToString() + "'";
                            }
                        }
                        else
                        {
                            if (PRO.IDOFIP == "" || PRO.IDOFIP == null)
                            {
                                PRO.IDOFIP += "Recipient:'" + row.Cells["IDrecp"].Value.ToString() + "'";
                            }
                            else
                            {
                                PRO.IDOFIP += "," + "'" + row.Cells["IDrecp"].Value.ToString() + "'";
                            }
                        }
                    }
                    PRO.IDOFIP += "|Informants:";
                    PRO.IDOFDP += "|Informants:";
                    string idofp = "";
                    string idofdp = "";
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["categoryin"].Value.ToString() == "Designated Person")
                        {
                            if (idofdp == "")
                            {
                                idofdp += "'" + row.Cells["IDinfo"].Value.ToString() + "'";
                            }
                            else
                            {
                                idofdp += "," + "'" + row.Cells["IDinfo"].Value.ToString() + "'";
                            }
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["categoryin"].Value.ToString() != "Designated Person")
                        {
                            if (idofp == "")
                            {
                                idofp += "'" + row.Cells["IDinfo"].Value.ToString() + "'";
                            }
                            else
                            {
                                idofp += "," + "'" + row.Cells["IDinfo"].Value.ToString() + "'";
                            }
                        }
                    }

                    PRO.IDOFIP += idofp;
                    PRO.IDOFDP += idofdp;

                    if (radioButtonNDAYES.Checked == true)
                    {
                        PRO.NDASIGNED = "YES";
                    }
                    else if (radioButtonNDANo.Checked == true)
                    {
                        PRO.NDASIGNED = "NO";
                    }

                    PRO.ID = "";
                    PRO.ENTEREDBY = SESSIONKEYS.UserID.ToString();

                    ds = new MasterClass().executeQuery("INSERT INTO T_INS_UPSI(UPSIID,IDOFIP,IDOFDP,UPSINATURE,SHARINGPURPOSE,SHARINGDATE,EFFECTIVEUPTO,REMARKS,NDASIGNED,UPSIAVAILABLE,ENTEREDBY,ENTEREDON,ACTIVE,LOCK) VALUES ('" + CryptographyHelper.Encrypt(CPID) + "','" + CryptographyHelper.Encrypt(PRO.IDOFIP) + "','" + CryptographyHelper.Encrypt(PRO.IDOFDP) + "','" + CryptographyHelper.Encrypt(PRO.UPSINATURE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGDATE) + "','" + CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) + "','" + CryptographyHelper.Encrypt(PRO.REMARKS) + "','" + CryptographyHelper.Encrypt(PRO.NDASIGNED) + "','" + CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N') ;").ToString();
                    string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_UPSI_LOG (TID,UPSIID,IDOFIP,IDOFDP,UPSINATURE,SHARINGPURPOSE,SHARINGDATE,EFFECTIVEUPTO,REMARKS,NDASIGNED,UPSIAVAILABLE,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ds + "','" + CryptographyHelper.Encrypt(CPID) + "','" + CryptographyHelper.Encrypt(PRO.IDOFIP) + "','" + CryptographyHelper.Encrypt(PRO.IDOFDP) + "','" + CryptographyHelper.Encrypt(PRO.UPSINATURE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGPURPOSE) + "','" + CryptographyHelper.Encrypt(PRO.SHARINGDATE) + "','" + CryptographyHelper.Encrypt(PRO.EFFECTIVEUPTO) + "','" + CryptographyHelper.Encrypt(PRO.REMARKS) + "','" + CryptographyHelper.Encrypt(PRO.NDASIGNED) + "','" + CryptographyHelper.Encrypt(PRO.UPSIAVAILABLE) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N') ;").ToString();

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
                        DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Clear();
                    FillConnectPersonID();
                    button2click();
                }
                //});
                //SetLoading(false);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                //SetLoading(false);
                DialogResult dialog = MessageBox.Show("Data Not Saved. Please Check Your Internet Connection.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public class ComboboxItem1
        {
            public string NAMEINSIDER { get; set; }
            public object UPSIID { get; set; }

            public override string ToString()
            {
                return NAMEINSIDER;
            }
        }

        public void FillConnectPersonID()
        {
            try
            {
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
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 15, 15);
            button2.Region = Region.FromHrgn(ptr);
        }

        private void button3_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, button3.Width, button3.Height, 15, 15);
            button3.Region = Region.FromHrgn(ptr);
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
    }
}
