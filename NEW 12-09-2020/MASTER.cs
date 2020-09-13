using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using The_PIT_Archive.Forms;
using The_PIT_Archive.Data_Entity;
using The_PIT_Archive.Data_Access_Layer;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace The_PIT_Archive
{
    public partial class MASTER : Form
    {
        public long valsize = 0;
        public long valFUllsize = 4187593114; //4294967296;//3.90GB
                                              //public long valFUllsize = 1048576;//2.00MB
        private AUDITLOG lg = new AUDITLOG();
        private static readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };

        public MASTER()
        {
            InitializeComponent();
        }

        private void MASTER_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            string path = FormatSize(GetFileSize(new MasterClass().GETDBTEXTLOG()));//FormatSize(GetFileSize(Directory.GetCurrentDirectory() + "\\The-PIT-Archive.sdf"));
            //LBLGETSIZE.Text = path.ToString() + " / 4.00 GB Used.";
            LLBNAME.Text = "Welcome " + SESSIONKEYS.FullName.ToString() + SESSIONKEYS.CompanyName.ToString();

            MainDashboard objForm = new MainDashboard();
            toggle(sender, objForm);
            //btndashboard.selected = true;

            if (GetMatching())
            {
                if (SESSIONKEYS.Role.ToString().Trim() == "Y")
                {
                    btncreatelogin.Visible = true;
                    label4.Visible = true;
                }
                else
                {
                    btncreatelogin.Visible = false;
                    label4.Visible = false;
                }
            }
            else
            {
                DialogResult dialog = MessageBox.Show("The Following Database size Reached Upto 4.00 GB. Please Contact The Admin.", "HomePage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (SESSIONKEYS.Role.ToString().Trim() != "Y")
                {
                    Login l = new Login();
                    //lg.CURRVALUE = "LOG OUT";
                    //lg.DESCRIPTION = "LOG OUT SUCCESSFULLY WITH DB STORAGE FULL";
                    //lg.TYPE = "SELECTED";
                    //lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                    //lg.ID = SESSIONKEYS.UserID.ToString();
                    string json = new MasterClass().SAVE_LOG(lg);
                    SESSIONKEYS.UserID = "";
                    SESSIONKEYS.Role = "";
                    SESSIONKEYS.FullName = "";
                    l.Show();
                    Close();
                }
            }

        }

        public bool GetMatching()
        {
            if (valFUllsize >= valsize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string FormatSize(long bytes)
        {
            valsize = bytes;
            int counter = 0;
            decimal number = bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }
            return string.Format("{0:n1}{1} ", number, suffixes[counter]);
        }

        private long GetFileSize(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                return new FileInfo(FilePath).Length;
            }
            return 0;
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

        void toggle(object sender, Form objForm)
        {
            //btndashboard.selected = false;
            //btnmasterdat.selected = false;
            //btndp.selected = false;
            //btnip.selected = false;
            //btnupsi.selected = false;
            //btnbackup.selected = false;
            //btnrestoredata.selected = false;
            //btnchoosedb.selected = false;
            //btncreatelogin.selected = false;
            //((Bunifu.Framework.UI.BunifuFlatButton)sender).selected = true;

            objForm.TopLevel = false;
            mainpanel.Controls.Clear();
            mainpanel.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }


        private void btndashboard_Click(object sender, EventArgs e)
        {
            MainDashboard objForm = new MainDashboard();
            toggle(sender, objForm);
            //btndashboard.selected = true;
        }

        private void btnmasterdat_Click(object sender, EventArgs e)
        {
            MasterDataCompany objForm = new MasterDataCompany();
            toggle(sender, objForm);
            //btnmasterdat.selected = true;
        }

        private void btndp_Click(object sender, EventArgs e)
        {
            DesignatedPerson objForm = new DesignatedPerson();
            toggle(sender, objForm);
            //btnip.selected = true;
        }

        private void btnip_Click(object sender, EventArgs e)
        {
            InsiderProfile objForm = new InsiderProfile();
            toggle(sender, objForm);
            //btnip.selected = true;
        }

        private void btnupsi_Click(object sender, EventArgs e)
        {
            SharedUPSI objForm = new SharedUPSI();
            toggle(sender, objForm);
            //btnupsi.selected = true;
        }

        private void btnbackup_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Backup Data?", "Back Up Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DialogResult ofd = folderBrowserDialog1.ShowDialog();

                    if (ofd == DialogResult.OK)
                    {
                        lg.CURRVALUE = "BACK UP DATA";
                        lg.TYPE = "SELECTED";
                        //lg.ID = SESSIONKEYS.UserID.ToString();
                        lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                        lg.DESCRIPTION = "BACK UP DATA";
                        new MasterClass().SAVE_LOG(lg);

                        string backupPath = folderBrowserDialog1.SelectedPath;
                        //string fileName = "The-PIT-Archive.sdf";
                        string sourcePath = SESSIONKEYS.CONNECTPATH; //Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                        //string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                        string destFile = System.IO.Path.Combine(backupPath, "The-PIT-Archive-" + DateTime.Now.ToString("dd-MM-yyyy-hh-MM-tt") + CultureInfo.InvariantCulture + ".sdf");

                        System.IO.File.Copy(sourcePath, destFile, true);

                        DialogResult dialog = MessageBox.Show("Data Backup Successfully.", "Back Up Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Data Backup Failed.", "Back Up Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                string path = FormatSize(GetFileSize(new MasterClass().GETDBTEXTLOG()));//FormatSize(GetFileSize(Directory.GetCurrentDirectory() + "\\The-PIT-Archive.sdf"));
                //LBLGETSIZE.Text = path.ToString() + " / 4.00 GB Used.";
            }
        }

        private void btnrestoredata_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Restore Data?\nPlease note that all your earlier data will be wiped out\nRestoring Data on Location :- " + SESSIONKEYS.CONNECTPATH, "Restore Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    OpenFileDialog openFileDialog1 = new OpenFileDialog
                    {
                        InitialDirectory = @"D:\",
                        Title = "Browse .SDF Files",

                        CheckFileExists = true,
                        CheckPathExists = true,

                        DefaultExt = "sdf",
                        Filter = "sdf files (*.sdf)|*.sdf",
                        //FilterIndex = 2,
                        RestoreDirectory = true,

                        //ReadOnlyChecked = true,
                        //ShowReadOnly = true
                    };

                    DialogResult ofd = openFileDialog1.ShowDialog();

                    if (ofd == DialogResult.OK)
                    {
                        if (openFileDialog1.FileName.Contains("The-PIT-Archive"))
                        {
                            string name = openFileDialog1.FileName;
                            lg.CURRVALUE = "RESTORE DATA";
                            lg.TYPE = "SELECTED";
                            //lg.ID = SESSIONKEYS.UserID.ToString();
                            lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                            lg.DESCRIPTION = "RESTORE DATA";
                            new MasterClass().SAVE_LOG(lg);
                            string backupPath = folderBrowserDialog1.SelectedPath;
                            string fileName = name;
                            string sourcePath = backupPath;
                            string restorePath = SESSIONKEYS.CONNECTPATH;//Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                            //if (File.Exists("The-PIT-Archive.sdf"))
                            //{
                            //	System.IO.File.Move("The-PIT-Archive.sdf", "The-PIT-Archive" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + ".sdf");
                            //}

                            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                            //string destFile = System.IO.Path.Combine(restorePath, fileName);

                            System.IO.File.Copy(sourceFile, restorePath, true);

                            lg.CURRVALUE = "RESTORE DATA";
                            lg.TYPE = "SELECTED";
                            //lg.ID = SESSIONKEYS.UserID.ToString();
                            lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                            lg.DESCRIPTION = "RESTORE DATA";
                            new MasterClass().SAVE_LOG(lg);

                            DialogResult dialog = MessageBox.Show("Data Restore Successfully.", "Restore Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            DialogResult dialogs = MessageBox.Show("Selected File is not Proper. Please Select Correct Archive File.", "Create | Choose Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Data Restore Failed.", "Restore Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                string path = FormatSize(GetFileSize(new MasterClass().GETDBTEXTLOG()));//FormatSize(GetFileSize(Directory.GetCurrentDirectory() + "\\The-PIT-Archive.sdf"));
                //LBLGETSIZE.Text = path.ToString() + " / 4.00 GB Used.";
            }
        }

        private void btnchoosedb_Click(object sender, EventArgs e)
        {
            CreateDatabase objForm = new CreateDatabase();
            toggle(sender, objForm);
            //btnchoosedb.selected = true;
        }

        private void btncreatelogin_Click(object sender, EventArgs e)
        {
            CreateLogin objForm = new CreateLogin();
            toggle(sender, objForm);
            //btncreatelogin.selected = true;
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            try
            {
                Login l = new Login();
                lg.CURRVALUE = "LOG OUT";
                lg.DESCRIPTION = "LOG OUT SUCCESSFULLY";
                lg.TYPE = "SELECTED";
                lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                //lg.ID = SESSIONKEYS.UserID.ToString();
                string json = new MasterClass().SAVE_LOG(lg);

                SESSIONKEYS.UserID = "";
                SESSIONKEYS.Role = "";
                SESSIONKEYS.FullName = "";
                foreach (var process in Process.GetProcessesByName("The PIT Archive"))
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Homepage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnlistofIP_Click(object sender, EventArgs e)
        {
            ListofInsiderProfile objForm = new ListofInsiderProfile();
            toggle(sender, objForm);
            //btnlistofIP.selected = true;
        }

        private void listofpan_Click(object sender, EventArgs e)
        {
            ListofPAn objForm = new ListofPAn();
            toggle(sender, objForm);
            //listofpan.selected = true;
        }

        private void btnadutitrail_Click(object sender, EventArgs e)
        {
            AuditTrail objForm = new AuditTrail();
            toggle(sender, objForm);
            //btnadutitrail.selected = true;
        }

        private void btnlistofDP_Click(object sender, EventArgs e)
        {
            ListofDP objForm = new ListofDP();
            toggle(sender, objForm);
            //btnadutitrail.selected = true;
        }

        private void btnlistofUPSI_Click(object sender, EventArgs e)
        {
            ListofSharingUPSI objForm = new ListofSharingUPSI();
            toggle(sender, objForm);
            //btnlistofUPSI.selected = true;
        }
    }
}
