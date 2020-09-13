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
    public partial class CreateLogin : Form
    {
        string val = "";
        AUDITLOG lg = new AUDITLOG();
        public CreateLogin()
        {
            InitializeComponent();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        private void bunifuFlatButton2_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, btnaddINSCON.Width, btnaddINSCON.Height, 15, 15);
            btnaddINSCON.Region = Region.FromHrgn(ptr);
        }

        private void bunifuFlatButton1_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, bunifuFlatButton1.Width, bunifuFlatButton1.Height, 15, 15);
            bunifuFlatButton1.Region = Region.FromHrgn(ptr);
        }

        private void CreateLogin_Load(object sender, EventArgs e)
        {
            Login l = new Login();
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
            txtFromDate.CustomFormat = " ";
            txtToDate.CustomFormat = " ";
            FIllData();
        }


        public void FIllData()
        {
            try
            {
                DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_LOGIN WHERE ACTIVE = 'Y' AND ADMIN = 'N'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtUsername.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString().Trim();
                    btnaddINSCON.Text = "UPDATE";
                    txtUsername.Enabled = false;
                    val = "UPDATE";
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //if (new MasterClass().GETLOCKDB() == "Y")
                //{
                //	DialogResult dialog = MessageBox.Show("Database is Locked.", "Locked Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //else
                int value = DateTime.Compare(txtFromDate.Value, txtToDate.Value);

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
                    DialogResult dialog = MessageBox.Show("Provide Values.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtPassword.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Provide Values.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtFromDate.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Provide Values.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtToDate.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Provide Values.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (value > 0)
                {
                    DialogResult dialog = MessageBox.Show("To Date cant be less than From Date.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string ds;
                    if (val == "UPDATE")
                    {
                        ds = new MasterClass().executeQueryForDB("UPDATE T_LOGIN SET DATEFROM = '" + txtFromDate.Value.ToString("yyyy-MM-dd 00:00:00") + "',DATETO = '" + txtToDate.Value.ToString("yyyy-MM-dd 00:00:00") + "', PASSWORD = '" + CryptographyHelper.Encrypt(txtPassword.Text) + "' WHERE ADMIN = 'N'").ToString();

                        lg.CURRVALUE = "LOGIN CREATION";
                        lg.TYPE = "UPDATED";
                        lg.ID = ds;
                        lg.DESCRIPTION = "LOGIN CREATION";
                        lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                        //lg.ID = SESSIONKEYS.UserID.ToString();
                        new MasterClass().SAVE_LOG(lg);

                        if (Convert.ToInt32(ds) > 0)
                        {
                            DialogResult dialog = MessageBox.Show("Updated Data Successfully.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        ds = new MasterClass().executeQuery("INSERT INTO T_LOGIN (FULLNAME,MOBILENO,EMAIL,PASSWORD,ENTEREDBY,ENTEREDON,DATEFROM,DATETO,ADMIN ,ACTIVE ,LOCK,SENDEMAIL) VALUES('','','" + txtUsername.Text + "','" + CryptographyHelper.Encrypt(txtPassword.Text) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + txtFromDate.Value.ToString("yyyy-MM-dd 00:00:00") + "','" + txtToDate.Value.ToString("yyyy-MM-dd 00:00:00") + "','N','Y','N','0');").ToString();

                        lg.CURRVALUE = "LOGIN CREATION";
                        lg.TYPE = "INSERTED";
                        lg.ID = ds;
                        lg.DESCRIPTION = "LOGIN CREATION";
                        lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                        //lg.ID = SESSIONKEYS.UserID.ToString();
                        new MasterClass().SAVE_LOG(lg);

                        if (Convert.ToInt32(ds) > 0)
                        {
                            DialogResult dialog = MessageBox.Show("Save Data Successfully.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    txtPassword.Text = "";
                    FIllData();
                }
                //});

                //SetLoading(false);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                //SetLoading(false);
                DialogResult dialog = MessageBox.Show("Data Not Saved. Please Check Your Internet Connection.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                lg.CURRVALUE = "LOG OUT";
                lg.DESCRIPTION = "LOG OUT SUCCESSFULLY";
                lg.TYPE = "SELECTED";
                lg.ENTEREDBY = "2";
                string json = new MasterClass().SAVE_LOG(lg);
                DialogResult dialog = MessageBox.Show("Data Saved Successfully.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            txtFromDate.CustomFormat = "dd-MM-yyyy";
        }

        private void txtToDate_ValueChanged(object sender, EventArgs e)
        {
            txtToDate.CustomFormat = "dd-MM-yyyy";
        }
    }
}
