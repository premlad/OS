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
    public partial class ListofSharingUPSI : Form
    {
        AUDITLOG lg = new AUDITLOG();
        public ListofSharingUPSI()
        {
            InitializeComponent();
        }

        private void ListofSharingUPSI_Load(object sender, EventArgs e)
        {
            Login l = new Login();
            //TopMost = true;
            //WindowState = FormWindowState.Maximized;
            try
            {
                if (SESSIONKEYS.UserID.ToString() == "" || SESSIONKEYS.UserID.ToString() == null)
                {
                    Close();
                    l.Show();
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                Close();
                l.Show();
            }
            //txtFromDate.CustomFormat = "dd-MM-yyyy";
            //txtToDate.CustomFormat = "dd-MM-yyyy";
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
                DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_UPSI P INNER JOIN T_LOGIN L ON L.ID = P.ENTEREDBY WHERE P.ACTIVE = 'Y' AND L.ACTIVE = 'Y'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string idip = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["IDOFIP"].ToString());
                        string iddp = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["IDOFDP"].ToString());
                        DataSet dsIPrec = new DataSet();
                        DataSet dsIPinf = new DataSet();
                        DataSet dsDPrec = new DataSet();
                        DataSet dsDPinf = new DataSet();

                        if (idip.Trim() != "" && idip.Trim() != null)
                        {
                            string[] a = idip.Split('|');
                            if (a[0] != "" && a[0] != null)
                            {
                                string[] rec = a[0].Split(':');
                                if (rec[1] != null || rec[1] != "")
                                {
                                    dsIPrec = new MasterClass().getDataSet("SELECT * FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                }
                            }
                            if (a[1] != "" && a[1] != null)
                            {
                                string[] recA = a[1].Split(':');
                                if (recA[1].Trim() != null && recA[1].Trim() != "")
                                {
                                    dsIPinf = new MasterClass().getDataSet("SELECT * FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                }
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

                            if (a[1] != "" && a[1] != null )
                            {
                                string[] recA = a[1].Split(':');
                                if (recA[1] != null && recA[1] != "")
                                {
                                    dsDPinf = new MasterClass().getDataSet("SELECT * FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                }
                            }
                        }

                        string mainid = "Recipient:", Name = "Recipient:", Category = "Recipient:", PANNO = "Recipient:", PANNOAffliates = "Recipient:", Address = "Recipient:";

                        if (dsIPrec.Tables.Count > 0)
                        {
                            for (int j = 0; j < dsIPrec.Tables[0].Rows.Count; j++)
                            {
                                mainid += "|" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["RECEPIENTID"].ToString());
                                Name += "|" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["NAMEINSIDER"].ToString());
                                Category += "|" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["CATEGORYRECEIPT"].ToString());
                                PANNO += "|" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["PANNO"].ToString());
                                PANNOAffliates += "|" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["PANNOAFFILIATES"].ToString());
                                Address += "|" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["ADDRESS"].ToString());
                            }
                        }

                        if (dsDPrec.Tables.Count > 0)
                        {
                            for (int j = 0; j < dsDPrec.Tables[0].Rows.Count; j++)
                            {
                                mainid += "|" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["CONNECTPERSONID"].ToString());
                                Name += "|" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["EMPNAME"].ToString());
                                Category += "|" + "Designated Person";
                                PANNO += "|" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["PANNO"].ToString());
                                PANNOAffliates += "|" + "";
                                Address += "|" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["ADDRESS"].ToString());
                            }
                        }
                        mainid += "Informants:";
                        Name += "Informants:";
                        Category += "Informants:";
                        PANNO += "Informants:";
                        PANNOAffliates += "Informants:";
                        Address += "Informants:";
                        if (dsIPinf.Tables.Count > 0)
                        {
                            for (int j = 0; j < dsIPinf.Tables[0].Rows.Count; j++)
                            {
                                mainid += "|" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["RECEPIENTID"].ToString());
                                Name += "|" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["NAMEINSIDER"].ToString());
                                Category += "|" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["CATEGORYRECEIPT"].ToString());
                                PANNO += "|" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["PANNO"].ToString());
                                PANNOAffliates += "|" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["PANNOAFFILIATES"].ToString());
                                Address += "|" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["ADDRESS"].ToString());
                            }
                        }

                        if (dsDPinf.Tables.Count > 0)
                        {
                            for (int j = 0; j < dsDPinf.Tables[0].Rows.Count; j++)
                            {
                                mainid += "|" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[j]["CONNECTPERSONID"].ToString());
                                Name += "|" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[j]["EMPNAME"].ToString());
                                Category += "|" + "Designated Person";
                                PANNO += "|" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[j]["PANNO"].ToString());
                                PANNOAffliates += "|" + "";
                                Address += "|" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[j]["ADDRESS"].ToString());
                            }
                        }
                        if (ds.Tables[0].Rows[i]["MODIFIEDON"].ToString() == "")
                        {
                            string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIID"].ToString()), mainid, Name, Category, PANNO, Address, PANNOAffliates, "Details of UPSI : " + CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSINATURE"].ToString()) + " Reason of Sharing : " + CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["SHARINGPURPOSE"].ToString()), MasterClass.GETISTFORUPSI(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["SHARINGDATE"].ToString())), MasterClass.GETISTFORUPSI(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EFFECTIVEUPTO"].ToString())), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["REMARKS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NDASIGNED"].ToString()), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), "", MasterClass.GETISTFORUPSI(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIAVAILABLE"].ToString())) };
                            dataGridViewTable.Rows.Add(row);
                        }
                        else
                        {
                            string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIID"].ToString()), mainid, Name, Category, PANNO, Address, PANNOAffliates, "Details of UPSI : " + CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSINATURE"].ToString()) + " Reason of Sharing : " + CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["SHARINGPURPOSE"].ToString()), MasterClass.GETISTFORUPSI(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["SHARINGDATE"].ToString())), MasterClass.GETISTFORUPSI(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EFFECTIVEUPTO"].ToString())), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["REMARKS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NDASIGNED"].ToString()), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MODIFIEDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), MasterClass.GETISTFORUPSI(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIAVAILABLE"].ToString())) };
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
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "List of Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInsiderID.Text != "")
                {
                    FillDataGrid(txtInsiderID.Text, "", "");
                }
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Following Value doesnt Match Any Records.", "List of Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FillDataGrid(string text, string From, string To)
        {

            try
            {
                //SetLoading(true);

                ////Thread.Sleep(2000);
                //Invoke((MethodInvoker)delegate
                //{
                dataGridViewTable.Rows.Clear();
                dataGridViewTable.Refresh();
                DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_UPSI P INNER JOIN T_LOGIN L ON L.ID = P.ENTEREDBY WHERE P.ACTIVE = 'Y' AND L.ACTIVE = 'Y'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIID"].ToString()) == text)
                        {
                            string idip = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["IDOFIP"].ToString());
                            string iddp = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["IDOFDP"].ToString());
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

                            string mainid = "Recipient:", Name = "Recipient:", Category = "Recipient:", PANNO = "Recipient:", PANNOAffliates = "Recipient:", Address = "Recipient:";

                            if (dsIPrec.Tables.Count > 0)
                            {
                                for (int j = 0; j < dsIPrec.Tables[0].Rows.Count; j++)
                                {
                                    mainid += "|" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["RECEPIENTID"].ToString());
                                    Name += "|" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["NAMEINSIDER"].ToString());
                                    Category += "|" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["CATEGORYRECEIPT"].ToString());
                                    PANNO += "|" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["PANNO"].ToString());
                                    PANNOAffliates += "|" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["PANNOAFFILIATES"].ToString());
                                    Address += "|" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[j]["ADDRESS"].ToString());
                                }
                            }

                            if (dsDPrec.Tables.Count > 0)
                            {
                                for (int j = 0; j < dsDPrec.Tables[0].Rows.Count; j++)
                                {
                                    mainid += "|" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["CONNECTPERSONID"].ToString());
                                    Name += "|" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["EMPNAME"].ToString());
                                    Category += "|" + "Designated Person";
                                    PANNO += "|" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["PANNO"].ToString());
                                    PANNOAffliates += "|" + "";
                                    Address += "|" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[j]["ADDRESS"].ToString());
                                }
                            }
                            mainid += "Informants:";
                            Name += "Informants:";
                            Category += "Informants:";
                            PANNO += "Informants:";
                            PANNOAffliates += "Informants:";
                            Address += "Informants:";
                            if (dsIPinf.Tables.Count > 0)
                            {
                                for (int j = 0; j < dsIPinf.Tables[0].Rows.Count; j++)
                                {
                                    mainid += "|" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["RECEPIENTID"].ToString());
                                    Name += "|" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["NAMEINSIDER"].ToString());
                                    Category += "|" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["CATEGORYRECEIPT"].ToString());
                                    PANNO += "|" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["PANNO"].ToString());
                                    PANNOAffliates += "|" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["PANNOAFFILIATES"].ToString());
                                    Address += "|" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[j]["ADDRESS"].ToString());
                                }
                            }

                            if (dsDPinf.Tables.Count > 0)
                            {
                                for (int j = 0; j < dsDPinf.Tables[0].Rows.Count; j++)
                                {
                                    mainid += "|" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[j]["CONNECTPERSONID"].ToString());
                                    Name += "|" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[j]["EMPNAME"].ToString());
                                    Category += "|" + "Designated Person";
                                    PANNO += "|" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[j]["PANNO"].ToString());
                                    PANNOAffliates += "|" + "";
                                    Address += "|" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[j]["ADDRESS"].ToString());
                                }
                            }
                            if (ds.Tables[0].Rows[i]["MODIFIEDON"].ToString() == "")
                            {
                                string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIID"].ToString()), mainid, Name, Category, PANNO, Address, PANNOAffliates, "Details of UPSI : " + CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSINATURE"].ToString()) + " Reason of Sharing : " + CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["SHARINGPURPOSE"].ToString()), MasterClass.GETISTFORUPSI(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["SHARINGDATE"].ToString())), MasterClass.GETISTFORUPSI(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EFFECTIVEUPTO"].ToString())), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["REMARKS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NDASIGNED"].ToString()), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), "", MasterClass.GETISTFORUPSI(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIAVAILABLE"].ToString())) };
                                dataGridViewTable.Rows.Add(row);
                            }
                            else
                            {
                                string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIID"].ToString()), mainid, Name, Category, PANNO, Address, PANNOAffliates, "Details of UPSI : " + CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSINATURE"].ToString()) + " Reason of Sharing : " + CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["SHARINGPURPOSE"].ToString()), MasterClass.GETISTFORUPSI(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["SHARINGDATE"].ToString())), MasterClass.GETISTFORUPSI(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EFFECTIVEUPTO"].ToString())), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["REMARKS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NDASIGNED"].ToString()), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ENTEREDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), (MasterClass.GETIST(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MODIFIEDON"].ToString())) + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim(), MasterClass.GETISTFORUPSI(CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIAVAILABLE"].ToString())) };
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
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "List of Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    lg.CURRVALUE = "REPORTS OF SHARING OF UPSI PROFILE";
                    lg.DESCRIPTION = "DOWNLOADED EXCEL FILE";
                    lg.TYPE = "SELECTED";
                    lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                    //lg.ID = SESSIONKEYS.UserID.ToString();
                    string json = new MasterClass().SAVE_LOG(lg);

                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "Excel Documents (*.xls)|*.xls",
                        FileName = "LIST OF SHARING OF UPSI.xls"
                    };

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        new MasterClass().ToCsV(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
                        MessageBox.Show("Exported Data Successfully in Excel Sheet.", "List of Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "List of Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            try
            {
                //SetLoading(true);

                //Thread.Sleep(2000);
                //Invoke((MethodInvoker)delegate
                //{
                if (dataGridViewTable.Rows.Count > 0)
                {
                    lg.CURRVALUE = "REPORTS OF SHARING OF UPSI PROFILE";
                    lg.DESCRIPTION = "DOWNLOADED PDF FILE";
                    lg.TYPE = "SELECTED";
                    lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                    //lg.ID = SESSIONKEYS.UserID.ToString();
                    string json = new MasterClass().SAVE_LOG(lg);

                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "PDF (*.pdf)|*.pdf",
                        FileName = "LIST OF SHARING OF UPSI.pdf"
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        new MasterClass().ToPDF(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
                        MessageBox.Show("Exported Data Successfully in PDF Sheet.", "List of Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "List of Sharing of UPSI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            txtInsiderID.Text = "";
            FillDataGrid();
        }
    }
}
