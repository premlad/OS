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

namespace The_PIT_Archive.Forms
{
    public partial class CHOOSEDATABASE : Form
    {
        private AUDITLOG lg = new AUDITLOG();
        string setvlaue = "";
        public CHOOSEDATABASE()
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

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("The PIT Archive"))
            {
                process.Kill();
            }
            this.Close();
        }

        private void CHOOSEDATABASE_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private void btnLOGIN_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, btnLOGIN.Width, btnLOGIN.Height, 15, 15);
            btnLOGIN.Region = Region.FromHrgn(ptr);
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

        private void bunifuFlatButton3_Paint(object sender, PaintEventArgs e)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, bunifuFlatButton3.Width, bunifuFlatButton3.Height, 15, 15);
            bunifuFlatButton3.Region = Region.FromHrgn(ptr);
        }

        private void btnLOGIN_Click(object sender, EventArgs e)
        {
            try
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

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.FileName.Contains("The-PIT-Archive"))
                    {
                        setvlaue = "CHOOSE";
                        txtdbpath.Text = openFileDialog1.FileName;
                        bunifuFlatButton2.Enabled = true;
                    }
                    else
                    {
                        DialogResult dialogs = MessageBox.Show("Selected File is not Proper. Please Select Correct Archive File.", "Create | Choose Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                bunifuFlatButton2.Enabled = false;
                throw;

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Create a New Database?", "Create/Choose Database", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DialogResult ofd = folderBrowserDialog1.ShowDialog();

                    if (ofd == DialogResult.OK)
                    {
                        setvlaue = "CREATE";
                        string backupPath = folderBrowserDialog1.SelectedPath;
                        string fileName = "The-PIT-Archive.sdf";
                        string sourceFile = System.IO.Path.Combine(backupPath, fileName);
                        txtdbpath.Text = sourceFile;
                        bunifuFlatButton2.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                bunifuFlatButton2.Enabled = false;
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Create/Choose Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Select/Create The Following Database\nOnce, Created/Selected make Sure you should not change the Path Further or Rename the Existing File?", "Create/Choose Database", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (MasterClass.GETISTII() == "TEMP")
                    {
                        DialogResult dialogs = MessageBox.Show("Date & Time is Tempered.\nPlease Check your Date & Time Settings.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        SESSIONKEYS.CONNECT = CryptographyHelper.Encrypt(@"Data Source=" + txtdbpath.Text + ";Password=DB@-MAS#TER-@1C4-473$-%B7P-860#-961; Max Database Size=4091");
                        new MasterClass().SAVEDBTEXTLOG(txtdbpath.Text);
                        SESSIONKEYS.CONNECTPATH = txtdbpath.Text;
                        string ds = "";
                        if (setvlaue == "CREATE")
                        {
                            new CheckDB().CreateDbForFirstInstance();
                        }

                        new MasterClass().executeQuery("UPDATE M_LOG_DATABASE SET ACTIVE = 'N'").ToString();
                        ds = new MasterClass().executeQuery("INSERT INTO M_LOG_DATABASE (DBPATH,ENTEREDBY,ENTEREDON,ACTIVE) VALUES('" + CryptographyHelper.Encrypt(txtdbpath.Text) + "','" + SESSIONKEYS.UserID.ToString() + "','" + MasterClass.GETIST() + "','Y');").ToString();

                        if (Convert.ToInt32(ds) > 0)
                        {
                            DialogResult dialogs = MessageBox.Show("Data Saved Successfully. Please Login", "Create/Choose Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SESSIONKEYS.UserID = "";
                            SESSIONKEYS.Role = "";
                            SESSIONKEYS.FullName = "";
                            Login l = new Login();
                            l.Show();
                            Hide();
                        }
                        else
                        {
                            DialogResult dialogs = MessageBox.Show("Something Went Wrong. Data Not Saved.", "Create/Choose Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                bunifuFlatButton2.Enabled = false;
                DialogResult dialog = MessageBox.Show("Something Went Wrong.\nYou will have to Create a New Database", "Create/Choose Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
