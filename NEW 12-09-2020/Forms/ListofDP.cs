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
    public partial class ListofDP : Form
    {
        private AUDITLOG lg = new AUDITLOG();
        public ListofDP()
        {
            InitializeComponent();
        }

        private void ListofDP_Load(object sender, EventArgs e)
        {
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
                //DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_PER P, T_INS_PER_DT D WHERE P.ID = D.PERID AND P.ACTIVE = 'Y' AND D.ACTIVE = 'Y' AND P.LOCK = 'N' AND D.LOCK = 'N'");			
                DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_PER P LEFT JOIN T_INS_PER_DT D ON P.ID = D.PERID AND D.LOCK = 'N' INNER JOIN T_LOGIN L ON L.ID = P.ENTEREDBY WHERE P.LOCK = 'N'");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
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
                        //Emailidofrelfin
                        if (ds.Tables[0].Rows[i]["ACTIVE"].ToString().Trim() == "Y")
                        {
                            string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CONNECTPERSONID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMPNAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CURRDESIGNATION"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RESIADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["GRADUATIONINSTI"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PASTEMP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["TYPE"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RELATIONSHIP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO1"].ToString()), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), (mod), "DP" };
                            dataGridViewTable.Rows.Add(row);
                        }
                        else
                        {
                            string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CONNECTPERSONID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMPNAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CURRDESIGNATION"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RESIADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["GRADUATIONINSTI"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PASTEMP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["TYPE"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RELATIONSHIP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO1"].ToString()), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MODIFIEDON"].ToString())) + " - " + mod), "NOMORE DP" };
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
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "List of Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_PER P, T_INS_PER_DT D WHERE P.ID = D.PERID AND P.ACTIVE = 'Y' AND D.ACTIVE = 'Y' AND P.LOCK = 'N' AND D.LOCK = 'N'");			
                DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_PER P LEFT JOIN T_INS_PER_DT D ON P.ID = D.PERID AND D.LOCK = 'N' INNER JOIN T_LOGIN L ON L.ID = P.ENTEREDBY WHERE P.LOCK = 'N' AND L.ACTIVE = 'Y'");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CONNECTPERSONID"].ToString()) == text)
                        {
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
                                string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CONNECTPERSONID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMPNAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CURRDESIGNATION"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RESIADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["GRADUATIONINSTI"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PASTEMP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["TYPE"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RELATIONSHIP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO1"].ToString()), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MODIFIEDON"].ToString())) + " - " + mod), "DP" };
                                dataGridViewTable.Rows.Add(row);
                            }
                            else
                            {
                                string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CONNECTPERSONID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMPNAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CURRDESIGNATION"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RESIADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OTHERIDENTIFIER"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["GRADUATIONINSTI"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PASTEMP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["TYPE"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RELATIONSHIP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO1"].ToString()), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MODIFIEDON"].ToString())) + " - " + mod), "NOMORE DP" };
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
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "List of Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    lg.CURRVALUE = "LIST OF DESIGNATED PERSON";
                    lg.DESCRIPTION = "DOWNLOADED EXCEL FILE";
                    lg.TYPE = "SELECTED";
                    lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                    //lg.ID = SESSIONKEYS.UserID.ToString();
                    string json = new MasterClass().SAVE_LOG(lg);
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "Excel Documents (*.xls)|*.xls",
                        FileName = "LIST OF DESIGNATED PERSON.xls"
                    };

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        new MasterClass().ToCsV(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
                        MessageBox.Show("Exported Data Successfully in Excel Sheet.", "List of Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "List of Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    lg.CURRVALUE = "LIST OF DESIGNATED PERSON";
                    lg.DESCRIPTION = "DOWNLOADED PDF FILE";
                    lg.TYPE = "SELECTED";
                    lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                    //lg.ID = SESSIONKEYS.UserID.ToString();
                    string json = new MasterClass().SAVE_LOG(lg);
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "PDF (*.pdf)|*.pdf",
                        FileName = "LIST OF DESIGNATED PERSON.pdf"
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        new MasterClass().ToPDF(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
                        MessageBox.Show("Exported Data Successfully in PDF Sheet.", "List of Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "List of Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInsiderID.Text != "")
                {
                    FillDataGrid(txtInsiderID.Text, "","");
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Enter Value to be Searched.", "List of Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Following Value doesnt Match Any Records.", "List of Designated Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            FillDataGrid();
        }
    }
}
