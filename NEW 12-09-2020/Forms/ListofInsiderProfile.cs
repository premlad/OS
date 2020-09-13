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
    public partial class ListofInsiderProfile : Form
    {
        AUDITLOG lg = new AUDITLOG();
        public ListofInsiderProfile()
        {
            InitializeComponent();
        }

        private void ListofInsiderProfile_Load(object sender, EventArgs e)
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
                DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_PRO P INNER JOIN T_LOGIN L ON L.ID = P.ENTEREDBY WHERE L.ACTIVE = 'Y'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string cat = "";

                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CATEGORYRECEIPT"].ToString()).Contains("|"))
                        {

                            string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CATEGORYRECEIPT"].ToString()).Split('|');
                            cat = abc[0] + " - " + abc[1];
                        }
                        else
                        {
                            cat = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CATEGORYRECEIPT"].ToString());
                        }

                        string mod = "";
                        if (ds.Tables[0].Rows[i]["MODIFIEDBY"].ToString().Trim() != "" || ds.Tables[0].Rows[i]["MODIFIEDBY"].ToString().Trim() != null)
                        {
                            if (ds.Tables[0].Rows[i]["MODIFIEDBY"].ToString().Trim() == "1")
                            {
                                mod = MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MODIFIEDON"].ToString())) + " - " + "ADMIN";
                            }
                            else if (ds.Tables[0].Rows[i]["MODIFIEDBY"].ToString().Trim() == "2")
                            {
                                mod = MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MODIFIEDON"].ToString())) + " - " + "COMPLIANCE_OFFICER";
                            }
                        }

                        if (ds.Tables[0].Rows[i]["ACTIVE"].ToString().Trim() == "Y")
                        {
                            string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAMEINSIDER"].ToString()), cat, CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["AADHARNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNOAFFILIATES"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["LANDLINENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO"].ToString()), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), mod, "IP" };
                            dataGridViewTable.Rows.Add(row);
                        }
                        else
                        {
                            string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAMEINSIDER"].ToString()), cat, CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["AADHARNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNOAFFILIATES"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["LANDLINENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO"].ToString()), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), mod, "NOMORE IP" };
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
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            txtInsiderID.Text = "";
            FillDataGrid();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInsiderID.Text != "")
                {
                    FillDataGrid(txtInsiderID.Text, "", "");
                    //int rowIndex = -1;
                    //foreach (DataGridViewRow row in dataGridViewTable.Rows)
                    //{
                    //	if (row.Cells[0].Value.ToString().Equals(txtInsiderID.Text))
                    //	{
                    //		rowIndex = row.Index;
                    //		//dataGridViewTable.Rows[rowIndex].Selected = true;
                    //	}
                    //	else
                    //	{
                    //		dataGridViewTable.Rows.RemoveAt(row.Index);
                    //	}
                    //}
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Following Value doesnt Match Any Records.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    lg.CURRVALUE = "LIST OF INSIDER PROFILE";
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
                        MessageBox.Show("Exported Data Successfully in PDF Sheet.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //SetLoading(true);

                //Thread.Sleep(2000);
                //Invoke((MethodInvoker)delegate
                //{
                if (dataGridViewTable.Rows.Count > 0)
                {
                    lg.CURRVALUE = "LIST OF INSIDER PROFILE";
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
                        MessageBox.Show("Exported Data Successfully in Excel Sheet.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
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

        public void FillDataGrid(string text, string From, string To)
        {

            try
            {
                //SetLoading(true);

                //Thread.Sleep(2000);
                //Invoke((MethodInvoker)delegate
                //{
                dataGridViewTable.Rows.Clear();
                dataGridViewTable.Refresh();
                DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_PRO P INNER JOIN T_LOGIN L ON L.ID = P.ENTEREDBY WHERE L.ACTIVE = 'Y'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString()) == text)
                        {
                            string cat = "";

                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CATEGORYRECEIPT"].ToString()).Contains("|"))
                            {

                                string[] abc = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CATEGORYRECEIPT"].ToString()).Split('|');
                                cat = abc[0] + " - " + abc[1];
                            }
                            else
                            {
                                cat = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CATEGORYRECEIPT"].ToString());
                            }

                            string mod = "";
                            if (ds.Tables[0].Rows[i]["MODIFIEDBY"].ToString().Trim() != "" || ds.Tables[0].Rows[i]["MODIFIEDBY"].ToString().Trim() != null)
                            {
                                if (ds.Tables[0].Rows[i]["MODIFIEDBY"].ToString().Trim() == "1")
                                {
                                    mod = MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MODIFIEDON"].ToString())) + " - " + "ADMIN";
                                }
                                else if (ds.Tables[0].Rows[i]["MODIFIEDBY"].ToString().Trim() == "2")
                                {
                                    mod = MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MODIFIEDON"].ToString())) + " - " + "COMPLIANCE_OFFICER";
                                }
                            }

                            if (ds.Tables[0].Rows[i]["ACTIVE"].ToString().Trim() == "Y")
                            {
                                string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAMEINSIDER"].ToString()), cat, CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["AADHARNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNOAFFILIATES"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["LANDLINENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO"].ToString()), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), mod, "IP" };
                                dataGridViewTable.Rows.Add(row);
                            }
                            else
                            {
                                string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAMEINSIDER"].ToString()), cat, CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["AADHARNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNOAFFILIATES"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["LANDLINENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO"].ToString()), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), mod, "NOMORE IP" };
                                dataGridViewTable.Rows.Add(row);
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
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
