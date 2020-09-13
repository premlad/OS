using RSACryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using The_PIT_Archive.Data_Access_Layer;
using The_PIT_Archive.Data_Entity;
using The_PIT_Archive.Forms;

namespace The_PIT_Archive
{
    public partial class Login : Form
    {
        private AUDITLOG lg = new AUDITLOG();
        public Login()
        {
            InitializeComponent();
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
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

        private void btnLOGIN_Click(object sender, EventArgs e)
        {
            try
            {
                int error = 0;
                DataSet ds = new DataSet();
                //SetLoading(true);

                //Thread.Sleep(2000);
                //Invoke((MethodInvoker)delegate
                //{
                if (txtusername.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Provide Values.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtpassword.Text == "")
                {
                    DialogResult dialog = MessageBox.Show("Provide Values.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (MasterClass.GETISTII() == "TEMP")
                {
                    DialogResult dialog = MessageBox.Show("Date & Time is Tempered.\nPlease Check your Date & Time Settings.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (new CheckDB().GetDBFilePath() == "EXISTS")
                {
                    if (txtusername.Text.ToUpper().Trim() == "COMPLIANCE_OFFICER")
                    {
                        if (new MasterClass().GETLOGINDATAOFFIUCER() == "Y")
                        {
                            ds = new MasterClass().getDataSet("SELECT ID,PASSWORD,FULLNAME,ADMIN,DATEFROM,DATETO,SENDEMAIL FROM T_LOGIN WHERE EMAIL = '" + txtusername.Text + "' AND ACTIVE = 'Y' AND CONVERT(DATETIME,'" + SESSIONKEYS.datetimeog.ToString("yyyy-MM-dd 00:00:00") + "') BETWEEN CONVERT(DATETIME,DATEFROM) AND CONVERT(DATETIME,DATETO) ");

                        }
                        else
                        {
                            error++;
                            DialogResult dialog = MessageBox.Show("COMPLIANCE_OFFICER Session is Still On. Please Log Out on the Respective System OR Ask Admin to Force Logout.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        ds = new MasterClass().getDataSet("SELECT ID,PASSWORD,FULLNAME,ADMIN,DATEFROM,DATETO,SENDEMAIL FROM T_LOGIN WHERE EMAIL = '" + txtusername.Text + "' AND ACTIVE = 'Y'");
                    }

                    if (error == 0)
                    {
                        DataSet dss = new MasterClass().getDataSet("SELECT COMPANYNAME FROM T_INS_COMPANY WHERE ACTIVE = 'Y'");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (txtpassword.Text == CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PASSWORD"].ToString()))
                            {
                                SESSIONKEYS.UserID = ds.Tables[0].Rows[0]["ID"].ToString();
                                SESSIONKEYS.Role = ds.Tables[0].Rows[0]["ADMIN"].ToString();
                                SESSIONKEYS.counteremail = Convert.ToInt32(ds.Tables[0].Rows[0]["SENDEMAIL"]);
                                if (dss.Tables[0].Rows.Count > 0)
                                {
                                    SESSIONKEYS.FullName = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["FULLNAME"].ToString());
                                    SESSIONKEYS.CompanyName = " of " + CryptographyHelper.Decrypt(dss.Tables[0].Rows[0]["COMPANYNAME"].ToString()) + ".";
                                }
                                else
                                {
                                    SESSIONKEYS.FullName = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["FULLNAME"].ToString());
                                    SESSIONKEYS.CompanyName = " of <<Company Name>>.";
                                }
                                lg.CURRVALUE = "LOG IN";
                                lg.DESCRIPTION = "LOG IN SUCCESSFULLY";
                                lg.TYPE = "SELECTED";
                                lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                                lg.ID = SESSIONKEYS.UserID.ToString();
                                string json = new MasterClass().SAVE_LOG(lg);
                                DialogResult dialog = MessageBox.Show("Login Successfully.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MASTER h = new MASTER();
                                h.Show();
                                Hide();
                            }
                            else
                            {
                                DialogResult dialog = MessageBox.Show("Invalid Username or Password.\nEnter Correct Username or Password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            DialogResult dialog = MessageBox.Show("Invalid Username or Password.\nEnter Correct Username or Password.\nPlease Contact System Administrator for More Details", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    //SetLoading(false);
                    DialogResult dialog = MessageBox.Show("Database Connection Not Found.\nPlease Select the Desired Database", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SESSIONKEYS.UserID = "";
                    SESSIONKEYS.Role = "";
                    SESSIONKEYS.FullName = "";
                    CHOOSEDATABASE l = new CHOOSEDATABASE();
                    l.Show();
                    Hide();
                }
                //});
            }
            catch (Exception ex)
            {
                //SetLoading(false);
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Please Check Your Internet Connection or Older Database File is Choosen. Please Select/Create the Desired File", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHOOSEDATABASE l = new CHOOSEDATABASE();
                l.Show();
                Hide();
            }
        }

        private void bunifuImageButton2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("The PIT Archive"))
            {
                process.Kill();
            }
            this.Close();
        }

        private void btnlogin_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, btnlogin.Width, btnlogin.Height, 15, 15);
            btnlogin.Region = Region.FromHrgn(ptr);
        }
    }
}
