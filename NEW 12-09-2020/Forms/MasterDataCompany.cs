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
    public partial class MasterDataCompany : Form
    {
        private AUDITLOG lg = new AUDITLOG();
        private string val = "";
        public MasterDataCompany()
        {
            InitializeComponent();
        }

        private void MasterDataCompany_Load(object sender, EventArgs e)
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
            FIllData();
        }

        public void FIllData()
        {
            try
            {
                DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_COMPANY WHERE ACTIVE = 'Y' AND LOCK = 'N'");
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
                    btnaddINSCON.Text = "UPDATE";
                    val = "UPDATE";
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Master Data of Company", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //SetLoading(false);
                    l.Show();
                    Hide();
                }
                else if (txtCompanyName.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Please Enter Company Name.", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!new MasterClass().IsValidEmail(txtEmailID.Text))
                {
                    DialogResult dialog = MessageBox.Show("Please Enter Email in Proper Format.", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string ds;
                    if (val == "UPDATE")
                    {
                        DataSet getval = new MasterClass().getDataSet("SELECT ID FROM T_INS_COMPANY_LOG WHERE ACTIVE = 'Y' ORDER BY ENTEREDON DESC");

                        ds = new MasterClass().executeQueryForDB("UPDATE T_INS_COMPANY SET COMPANYNAME = '" + CryptographyHelper.Encrypt(txtCompanyName.Text) + "',REGOFFICE = '" + CryptographyHelper.Encrypt(txtregisteredOffice.Text) + "',CORPORATEOFFICE = '" + CryptographyHelper.Encrypt(txtCorporateOffice.Text) + "',MOBILENO = '" + CryptographyHelper.Encrypt(txtMobileNo.Text) + "',LANDLINENO = '" + CryptographyHelper.Encrypt(txtLandLineNo.Text) + "',EMAILID = '" + CryptographyHelper.Encrypt(txtEmailID.Text) + "',CIN = '" + CryptographyHelper.Encrypt(txtCIN.Text) + "',BSECODE = '" + CryptographyHelper.Encrypt(txtBSE.Text) + "',NSECODE = '" + CryptographyHelper.Encrypt(txtNSE.Text) + "',ISIN = '" + CryptographyHelper.Encrypt(txtISIN.Text) + "',OFFICERNAME = '" + CryptographyHelper.Encrypt(txtOfficerName.Text) + "',DESIGNATION = '" + CryptographyHelper.Encrypt(txtDesignation.Text) + "',MODIFIEDBY = '" + SESSIONKEYS.UserID.ToString() + "',MODIFIEDON = '" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "' ;").ToString();
                        string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_COMPANY_LOG(TID,COMPANYNAME,REGOFFICE,CORPORATEOFFICE,MOBILENO,LANDLINENO,EMAILID,CIN,BSECODE,NSECODE,ISIN,OFFICERNAME,DESIGNATION,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ds + "','" + CryptographyHelper.Encrypt(txtCompanyName.Text) + "','" + CryptographyHelper.Encrypt(txtregisteredOffice.Text) + "','" + CryptographyHelper.Encrypt(txtCorporateOffice.Text) + "','" + CryptographyHelper.Encrypt(txtMobileNo.Text) + "','" + CryptographyHelper.Encrypt(txtLandLineNo.Text) + "','" + CryptographyHelper.Encrypt(txtEmailID.Text) + "','" + CryptographyHelper.Encrypt(txtCIN.Text) + "','" + CryptographyHelper.Encrypt(txtBSE.Text) + "','" + CryptographyHelper.Encrypt(txtNSE.Text) + "','" + CryptographyHelper.Encrypt(txtISIN.Text) + "','" + CryptographyHelper.Encrypt(txtOfficerName.Text) + "','" + CryptographyHelper.Encrypt(txtDesignation.Text) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("UPDATED") + "','Y','N') ;").ToString();



                        lg.CURRVALUE = "MASTER DATA OF COMPANY PROFILE TAB";
                        lg.TYPE = "UPDATED";
                        lg.ID = perlogid + "|" + getval.Tables[0].Rows[0]["ID"].ToString();
                        lg.DESCRIPTION = "UPDATED IN MASTER DATA OF COMPANY";
                        lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                        //lg.ID = SESSIONKEYS.UserID.ToString();
                        new MasterClass().SAVE_LOG(lg);

                        if (Convert.ToInt32(ds) > 0)
                        {
                            DialogResult dialog = MessageBox.Show("Updated Data Successfully.", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        ds = new MasterClass().executeQuery("INSERT INTO T_INS_COMPANY(COMPANYNAME,REGOFFICE,CORPORATEOFFICE,MOBILENO,LANDLINENO,EMAILID,CIN,BSECODE,NSECODE,ISIN,OFFICERNAME,DESIGNATION,ENTEREDBY,ENTEREDON,ACTIVE,LOCK) VALUES ('" + CryptographyHelper.Encrypt(txtCompanyName.Text) + "','" + CryptographyHelper.Encrypt(txtregisteredOffice.Text) + "','" + CryptographyHelper.Encrypt(txtCorporateOffice.Text) + "','" + CryptographyHelper.Encrypt(txtMobileNo.Text) + "','" + CryptographyHelper.Encrypt(txtLandLineNo.Text) + "','" + CryptographyHelper.Encrypt(txtEmailID.Text) + "','" + CryptographyHelper.Encrypt(txtCIN.Text) + "','" + CryptographyHelper.Encrypt(txtBSE.Text) + "','" + CryptographyHelper.Encrypt(txtNSE.Text) + "','" + CryptographyHelper.Encrypt(txtISIN.Text) + "','" + CryptographyHelper.Encrypt(txtOfficerName.Text) + "','" + CryptographyHelper.Encrypt(txtDesignation.Text) + "','" + SESSIONKEYS.UserID.ToString() + "','" + CryptographyHelper.Encrypt(MasterClass.GETIST()) + "','Y','N') ;").ToString();
                        string perlogid = new MasterClass().executeQuery("INSERT INTO T_INS_COMPANY_LOG(TID,COMPANYNAME,REGOFFICE,CORPORATEOFFICE,MOBILENO,LANDLINENO,EMAILID,CIN,BSECODE,NSECODE,ISIN,OFFICERNAME,DESIGNATION,ENTEREDBY,ENTEREDON,OPERATION,ACTIVE,LOCK) VALUES ('" + ds + "','" + CryptographyHelper.Encrypt(txtCompanyName.Text) + "','" + CryptographyHelper.Encrypt(txtregisteredOffice.Text) + "','" + CryptographyHelper.Encrypt(txtCorporateOffice.Text) + "','" + CryptographyHelper.Encrypt(txtMobileNo.Text) + "','" + CryptographyHelper.Encrypt(txtLandLineNo.Text) + "','" + CryptographyHelper.Encrypt(txtEmailID.Text) + "','" + CryptographyHelper.Encrypt(txtCIN.Text) + "','" + CryptographyHelper.Encrypt(txtBSE.Text) + "','" + CryptographyHelper.Encrypt(txtNSE.Text) + "','" + CryptographyHelper.Encrypt(txtISIN.Text) + "','" + CryptographyHelper.Encrypt(txtOfficerName.Text) + "','" + CryptographyHelper.Encrypt(txtDesignation.Text) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','" + CryptographyHelper.Encrypt("INSERTED") + "','Y','N') ;").ToString();

                        lg.CURRVALUE = "MASTER DATA OF COMPANY PROFILE TAB";
                        lg.TYPE = "INSERTED";
                        lg.ID = perlogid;
                        lg.DESCRIPTION = "INSERTED IN MASTER DATA OF COMPANY";
                        lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                        //lg.ID = SESSIONKEYS.UserID.ToString();
                        new MasterClass().SAVE_LOG(lg);

                        if (Convert.ToInt32(ds) > 0)
                        {
                            DialogResult dialog = MessageBox.Show("Save Data Successfully.", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            DialogResult dialog = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    FIllData();
                }
                //});

                //SetLoading(false);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                //SetLoading(false);
                DialogResult dialog = MessageBox.Show("Data Not Saved. Please Check Your Internet Connection.", "Company Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SESSIONKEYS.CompanyName = " of " + txtCompanyName.Text + ".";
            }
        }

        private void btnaddINSCON_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, btnaddINSCON.Width, btnaddINSCON.Height, 15, 15);
            btnaddINSCON.Region = Region.FromHrgn(ptr);
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
    }
}
