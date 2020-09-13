using RSACryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using The_PIT_Archive.Data_Access_Layer;
using The_PIT_Archive.Data_Entity;

namespace The_PIT_Archive.Forms
{
    public partial class ListofPAn : Form
    {
        AUDITLOG lg = new AUDITLOG();
        public ListofPAn()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //SetLoading(true);

                ////Thread.Sleep(2000);
                //Invoke((MethodInvoker)delegate
                //{
                if (dataGridViewTable.Rows.Count > 0)
                {
                    lg.CURRVALUE = "LIST OF PAN'S REGISTERED IN DB TAB";
                    lg.DESCRIPTION = "DOWNLOADED EXCEL FILE";
                    lg.TYPE = "SELECTED";
                    lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                    //lg.ID = SESSIONKEYS.UserID.ToString();
                    string json = new MasterClass().SAVE_LOG(lg);

                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "Excel Documents (*.xls)|*.xls",
                        FileName = "LIST OF INSIDERS.xls"
                    };

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        new MasterClass().ToCsV(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
                        MessageBox.Show("Exported Data Successfully in Excel Sheet.", "List of PAN’s Registered under Database", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                //});

                //SetLoading(false);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                //SetLoading(false);
                DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "List of PAN’s Registered under Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            try
            {
                //SetLoading(true);

                ////Thread.Sleep(2000);
                //Invoke((MethodInvoker)delegate
                //{
                if (dataGridViewTable.Rows.Count > 0)
                {
                    lg.CURRVALUE = "LIST OF PAN'S REGISTERED IN DB TAB";
                    lg.DESCRIPTION = "DOWNLOADED PDF FILE";
                    lg.TYPE = "SELECTED";
                    lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                    //lg.ID = SESSIONKEYS.UserID.ToString();
                    string json = new MasterClass().SAVE_LOG(lg);


                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "PDF (*.pdf)|*.pdf",
                        FileName = "LIST OF INSIDERS.pdf"
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        new MasterClass().ToPDF(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
                        MessageBox.Show("Exported Data Successfully in PDF Sheet.", "List of PAN’s Registered under Database", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    MessageBox.Show("No Record To Export !!!", "Info");
                }
                //});

                //SetLoading(false);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                //SetLoading(false);
                DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            try
            {
                //SetLoading(true);

                //Thread.Sleep(2000);
                //Invoke((MethodInvoker)delegate
                //{
                dataGridViewTable.Rows.Clear();
                dataGridViewTable.Refresh();
                DataSet ds = new MasterClass().getDataSet("SELECT NAMEINSIDER AS [NAME],PANNO,DEMATACNO AS [DEMATAC],OTHERIDENTIFIER,PANNOAFFILIATES,EMAILID,MOBILENO FROM T_INS_PRO");
                DataSet ds1 = new MasterClass().getDataSet("SELECT EMPNAME AS [NAME],PANNO,DEMATACNO AS [DEMATAC],OTHERIDENTIFIER,EMAILID,MOBILENO FROM T_INS_PER");
                DataSet ds2 = new MasterClass().getDataSet("SELECT NAME AS [NAME],PANNO,DEMATACNO AS [DEMATAC],EMAILID,MOBILENO FROM T_INS_PER_DT");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string abc = "";
                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()) == "")
                        {
                            abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString());
                        }
                        else
                        {
                            abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString());
                        }

                        string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), abc, CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATAC"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()) };
                        dataGridViewTable.Rows.Add(row);

                        string Pannoaffiates = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNOAFFILIATES"].ToString());
                        if (Pannoaffiates != "" && Pannoaffiates != null)
                        {
                            string[] xyz = Pannoaffiates.Split('|');
                            for (int k = 0; k < xyz.Length; k++)
                            {
                                string[] def = xyz[k].Split('-');
                                string[] row1 = { def[0], def[1], "", "", "" };
                                dataGridViewTable.Rows.Add(row1);
                            }
                        }
                    }
                }

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        string a = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["DEMATAC"].ToString());
                        if (a.Contains("|"))
                        {
                            string[] xyz = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["DEMATAC"].ToString()).Split('|');
                            for (int k = 0; k < xyz.Length; k++)
                            {
                                string abc = "";
                                if (CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["PANNO"].ToString()) == "")
                                {
                                    abc = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString());
                                }
                                else
                                {
                                    abc = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["PANNO"].ToString());
                                }
                                string[] row = { CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["NAME"].ToString()), abc, xyz[k], CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["MOBILENO"].ToString()) };
                                dataGridViewTable.Rows.Add(row);
                            }
                        }
                        else
                        {
                            string abc = "";
                            if (CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["PANNO"].ToString()) == "")
                            {
                                abc = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString());
                            }
                            else
                            {
                                abc = CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["PANNO"].ToString());
                            }
                            string[] row = { CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["NAME"].ToString()), abc, CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["DEMATAC"].ToString()), CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds1.Tables[0].Rows[i]["MOBILENO"].ToString()) };
                            dataGridViewTable.Rows.Add(row);
                        }

                    }
                }

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        string pan = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["PANNO"].ToString());
                        string demat = CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["DEMATAC"].ToString());
                        if (pan != "" || demat != "") {
                            string[] row = { CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["NAME"].ToString()), pan, demat, CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds2.Tables[0].Rows[i]["MOBILENO"].ToString()) };
                            dataGridViewTable.Rows.Add(row);
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
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "List of PAN’s Registered under Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListofPAn_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            enableDoubleBuff(panel1);
            Login l = new Login();
            //TopMost = true;
            //WindowState = FormWindowState.Maximized;
            try
            {
                if (SESSIONKEYS.UserID.ToString() == "" || SESSIONKEYS.UserID.ToString() == null)
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
            FillDataGrid();
        }

        public static void enableDoubleBuff(System.Windows.Forms.Control cont)
        {
            System.Reflection.PropertyInfo DemoProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            DemoProp.SetValue(cont, true, null);
        }
    }
}
