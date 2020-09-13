using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using The_PIT_Archive.Data_Access_Layer;
using The_PIT_Archive.Data_Entity;

namespace The_PIT_Archive.Forms
{
    public partial class MainDashboard : Form
    {
        public long valsize = 0;
        public long curentzie = 0;
        public long valFUllsize = 4187593114; //4294967296;//3.90GB
                                              //public long valFUllsize = 1048576;//2.00MB
        private AUDITLOG lg = new AUDITLOG();
        private static readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };
        public MainDashboard()
        {
            InitializeComponent();
        }

        private void MainDashboard_Load(object sender, EventArgs e)
        {
            string path = FormatSize(GetFileSize(new MasterClass().GETDBTEXTLOG()));//FormatSize(GetFileSize(Directory.GetCurrentDirectory() + "\\The-PIT-Archive.sdf"));
            LBLGETSIZE.Text = path.ToString(); //+ " / 4.00 GB Used.";
            decimal a = Convert.ToDecimal((curentzie / valFUllsize) * 100);
            //chart1.Titles.Add("Pie Chart");
            //chart1.Series["S1"].Points.AddXY("Used", a);
            //chart1.Series["S1"].Points.AddXY("Unused", "100");
            //bunifuCircleProgressbar1.Value = Convert.ToInt32((curentzie / valFUllsize) * 100);
            FIllData();
            if (GetMatching())
            {

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
                    Hide();
                }
            }
        }

        public void FIllData()
        {
            try
            {
                DataSet ds = new MasterClass().getDataSet("SELECT COUNT(ID) AS [COUNT] FROM T_INS_PER ");
                DataSet ds1 = new MasterClass().getDataSet("SELECT COUNT(ID) AS [COUNT] FROM T_INS_PRO ");
                DataSet ds2 = new MasterClass().getDataSet("SELECT COUNT(ID) AS [COUNT] FROM T_INS_UPSI WHERE ACTIVE = 'Y'");
                dpcount.Text = ds.Tables[0].Rows[0]["COUNT"].ToString();
                upsicount.Text = ds2.Tables[0].Rows[0]["COUNT"].ToString();
                ipcount.Text = ds1.Tables[0].Rows[0]["COUNT"].ToString();
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            curentzie = bytes;
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
    }
}
