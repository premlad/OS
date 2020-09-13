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
    public partial class AuditTrail : Form
    {
        AUDITLOG lg = new AUDITLOG();
        public AuditTrail()
        {
            InitializeComponent();
        }

        private void AuditTrail_Load(object sender, EventArgs e)
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
            //txtFromDate.CustomFormat = " ";
            //txtToDate.CustomFormat = " ";
            FillDataGrid();
        }

        public static void enableDoubleBuff(System.Windows.Forms.Control cont)
        {
            System.Reflection.PropertyInfo DemoProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            DemoProp.SetValue(cont, true, null);
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
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "PDF (*.pdf)|*.pdf",
                        FileName = "AUDIT LOG.pdf"
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        lg.CURRVALUE = "AUDIT LOG TAB";
                        lg.DESCRIPTION = "DOWNLOADED PDF FILE";
                        lg.TYPE = "SELECTED";
                        lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                        //lg.ID = SESSIONKEYS.UserID.ToString();
                        string json = new MasterClass().SAVE_LOG(lg);
                        DataGridView dataGrid = new DataGridView();
                        dataGrid = dataGridViewTable;
                        new MasterClass().ToPDF(dataGrid, sfd.FileName); // Here dvwACH is your grid view name
                        MessageBox.Show("Exported Data Successfully in PDF Sheet.", "Audit Report", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "Audit Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "Excel Documents (*.xls)|*.xls",
                        FileName = "AUDIT LOG.xls"
                    };

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        lg.CURRVALUE = "AUDIT LOG TAB";
                        lg.DESCRIPTION = "DOWNLOADED EXCEL FILE";
                        lg.TYPE = "SELECTED";
                        lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
                        //lg.ID = SESSIONKEYS.UserID.ToString();
                        string json = new MasterClass().SAVE_LOG(lg);

                        new MasterClass().ToCsV(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
                        MessageBox.Show("Exported Data Successfully in Excel Sheet.", "Audit Report", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                DialogResult dialog = MessageBox.Show("Name Already Exists in the Location or The File is Already Opened.", "Audit Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FillDataGrid(DateTime From, DateTime To)
        {

            try
            {
                //SetLoading(true);

                ////Thread.Sleep(2000);
                //Invoke((MethodInvoker)delegate
                //{


                dataGridViewTable.Rows.Clear();
                dataGridViewTable.Refresh();

                string qry = "SELECT * FROM M_LOG_AUDIT WHERE 1 = 1 ";
                if (From.ToString() != "" || From.ToString() != null)
                {
                    qry += " AND CONVERT(DATETIME,ENTEREDON) >= CONVERT(DATETIME,'" + From.ToString("yyyy-MM-dd 00:00:00") + "')";
                }

                if (To.ToString() != "" || To.ToString() != null)
                {
                    qry += " AND CONVERT(DATETIME,ENTEREDON) <= CONVERT(DATETIME,'" + To.ToString("yyyy-MM-dd 23:59:59") + "')";
                }

                qry += "ORDER BY ENTEREDON DESC";

                DataSet ds = new MasterClass().getDataSet(qry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataSet sgrow = new DataSet();
                        DataSet updtrow = new DataSet();
                        string output = "";
                        string updtvalue = "";
                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()).Trim() == "DESIGNATED PERSON TAB")
                        {
                            string[] val = ds.Tables[0].Rows[i]["TID"].ToString().Split('|');
                            sgrow = new MasterClass().getDataSet("SELECT TID,CONNECTPERSONID AS [Designated Person Id],EMPNAME AS [Name of the Employee],CURRDESIGNATION AS [Current Designation],ADDRESS AS [Permanent Address],RESIADDRESS AS [Correspondence Address], OTHERIDENTIFIER AS [Other Identifier], PANNO AS[PAN], DEMATACNO AS [Demat A/c No], MOBILENO AS [Mobile No], EMAILID AS [Email ID],GRADUATIONINSTI AS [Graduation Institution], PASTEMP AS [Past Employee] FROM T_INS_PER_LOG WHERE ID = '" + val[0] + "'");

                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()).Trim() == "UPDATED")
                            {
                                updtrow = new MasterClass().getDataSet("SELECT CONNECTPERSONID AS [Designated Person Id],EMPNAME AS [Name of the Employee],CURRDESIGNATION AS [Current Designation],ADDRESS AS [Permanent Address],RESIADDRESS AS [Correspondence Address], OTHERIDENTIFIER AS [Other Identifier], PANNO AS[PAN], DEMATACNO AS [Demat A/c No], MOBILENO AS [Mobile No], EMAILID AS [Email ID],GRADUATIONINSTI AS [Graduation Institution], PASTEMP AS [Past Employee] FROM T_INS_PER_LOG WHERE ID = '" + val[1] + "'");

                                string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                output += "Updated Value :- ";
                                for (int j = 1; j <= columnNames1.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                                output += "Previous Value :- ";


                                for (int j = 0; j < columnNames.Length; j++)
                                {
                                    if (j == 0)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }


                                for (int k = 0; k < columnNames.Length; k++)
                                {
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (columnNames1[j] == columnNames[k])
                                        {
                                            if (CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim() != CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][k].ToString()).Trim())
                                            {
                                                if (k == 1)
                                                {
                                                    updtvalue += columnNames[k];
                                                }
                                                else
                                                {
                                                    updtvalue += " | " + columnNames[k];
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                for (int j = 1; j <= columnNames.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                            }

                        }

                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()).Trim() == "DESIGNATED PERSON TAB RELATIVE RELATIONSHIP")
                        {
                            string[] val = ds.Tables[0].Rows[i]["TID"].ToString().Split('|');
                            sgrow = new MasterClass().getDataSet("SELECT TID,NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],EMAILID AS [Email ID],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat A/c No],TYPE as [TYPE] FROM T_INS_PER_DT_LOG WHERE ID = '" + val[0] + "'");
                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()).Trim() == "UPDATED")
                            {
                                if (val.Length < 2)
                                {
                                    string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                    output += "Inserted New Value :- ";
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (j == 1)
                                        {
                                            output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                        else
                                        {
                                            output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                    }
                                    updtvalue += "Inserted New Value";
                                }
                                else
                                {
                                    updtrow = new MasterClass().getDataSet("SELECT NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],EMAILID AS [Email ID],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat A/c No],TYPE as [TYPE] FROM T_INS_PER_DT_LOG WHERE ID = '" + val[1] + "'");

                                    string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                    string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                    output += "Updated Value :- ";
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (j == 1)
                                        {
                                            output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                        else
                                        {
                                            output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                    }
                                    output += "Previous Value :- ";

                                    for (int j = 0; j < columnNames.Length; j++)
                                    {
                                        if (j == 0)
                                        {
                                            output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                        else
                                        {
                                            output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                    }

                                    for (int k = 0; k < columnNames.Length; k++)
                                    {
                                        for (int j = 1; j <= columnNames1.Length - 1; j++)
                                        {
                                            if (columnNames1[j] == columnNames[k])
                                            {
                                                if (CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim() != CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][k].ToString()).Trim())
                                                {
                                                    if (k == 1)
                                                    {
                                                        updtvalue += columnNames[k];
                                                    }
                                                    else
                                                    {
                                                        updtvalue += " | " + columnNames[k];
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                for (int j = 1; j <= columnNames.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                            }

                        }

                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()).Trim() == "DESIGNATED PERSON TAB FINANCIAL RELATIONSHIP")
                        {
                            string[] val = ds.Tables[0].Rows[i]["TID"].ToString().Split('|');
                            sgrow = new MasterClass().getDataSet("SELECT TID,NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],EMAILID AS [Email ID],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat A/c No],TYPE as [TYPE] FROM T_INS_PER_DT_LOG WHERE ID = '" + val[0] + "'");
                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()).Trim() == "UPDATED")
                            {
                                if (val.Length < 2)
                                {
                                    string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                    output += "Inserted New Value :- ";
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (j == 1)
                                        {
                                            output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                        else
                                        {
                                            output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                    }

                                    updtvalue += "Inserted New Value";
                                }
                                else
                                {

                                    updtrow = new MasterClass().getDataSet("SELECT NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],EMAILID AS [Email ID],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat A/c No],TYPE as [TYPE] FROM T_INS_PER_DT_LOG WHERE ID = '" + val[1] + "'");

                                    string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                    string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                    output += "Updated Value :- ";
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (j == 1)
                                        {
                                            output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                        else
                                        {
                                            output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                    }
                                    output += "Previous Value :- ";

                                    for (int j = 0; j < columnNames.Length; j++)
                                    {
                                        if (j == 0)
                                        {
                                            output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                        else
                                        {
                                            output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                    }

                                    for (int k = 0; k < columnNames.Length; k++)
                                    {
                                        for (int j = 1; j <= columnNames1.Length - 1; j++)
                                        {
                                            if (columnNames1[j] == columnNames[k])
                                            {
                                                if (CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim() != CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][k].ToString()).Trim())
                                                {
                                                    if (k == 1)
                                                    {
                                                        updtvalue += columnNames[k];
                                                    }
                                                    else
                                                    {
                                                        updtvalue += " | " + columnNames[k];
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                for (int j = 1; j <= columnNames.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                            }

                        }

                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()).Trim() == "INSIDER PROFILE TAB")
                        {
                            string[] val = ds.Tables[0].Rows[i]["TID"].ToString().Split('|');
                            sgrow = new MasterClass().getDataSet("SELECT TID,RECEPIENTID AS [Recipient Id],NAMEINSIDER AS [Name of the Insider],CATEGORYRECEIPT AS [Category of Receipt], ADDRESS AS [Address], OTHERIDENTIFIER AS [Other Identifier], PANNO AS [PAN], AADHARNO AS [Aadhar No],MOBILENO AS [Mobile No],LANDLINENO AS[Landline No], EMAILID AS [Email Id], PANNOAFFILIATES AS [PAN No.of Affiliates] FROM T_INS_PRO_LOG WHERE ID = '" + val[0] + "'");
                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()).Trim() == "UPDATED")
                            {
                                updtrow = new MasterClass().getDataSet("SELECT RECEPIENTID AS [Recipient Id],NAMEINSIDER AS [Name of the Insider],CATEGORYRECEIPT AS [Category of Receipt], ADDRESS AS [Address], OTHERIDENTIFIER AS [Other Identifier], PANNO AS [PAN], AADHARNO AS [Aadhar No],MOBILENO AS [Mobile No],LANDLINENO AS[Landline No], EMAILID AS [Email Id], PANNOAFFILIATES AS [PAN No.of Affiliates] FROM T_INS_PRO_LOG WHERE ID = '" + val[1] + "'");


                                string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                output += "Updated Value :- ";
                                for (int j = 1; j <= columnNames1.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                                output += "Previous Value :- ";


                                for (int j = 0; j < columnNames.Length; j++)
                                {
                                    if (j == 0)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }


                                for (int k = 0; k < columnNames.Length; k++)
                                {
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (columnNames1[j] == columnNames[k])
                                        {
                                            if (CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim() != CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][k].ToString()).Trim())
                                            {
                                                if (k == 1)
                                                {
                                                    updtvalue += columnNames[k];
                                                }
                                                else
                                                {
                                                    updtvalue += " | " + columnNames[k];
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            else
                            {
                                string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                for (int j = 1; j <= columnNames.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                            }


                        }

                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()).Trim() == "SHARING OF UPSI PROFILE TAB")
                        {
                            string[] val = ds.Tables[0].Rows[i]["TID"].ToString().Split('|');
                            sgrow = new MasterClass().getDataSet("SELECT TID,UPSIID AS [UPSI ID],IDOFIP AS [ID OF IP],IDOFDP AS [ID OF DP],UPSINATURE AS [Nature of UPSI], SHARINGPURPOSE AS [Purpose of Sharing], SHARINGDATE AS [Date of Sharing],EFFECTIVEUPTO AS [Effective Upto],REMARKS AS [Remarks], NDASIGNED AS [Whether NDA has been signed and Notice of confidentiality has been given ?],UPSIAVAILABLE AS [Date when UPSI became publicly available] FROM T_INS_UPSI_LOG WHERE ID = '" + val[0] + "'");
                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()).Trim() == "UPDATED")
                            {
                                updtrow = new MasterClass().getDataSet("SELECT UPSIID AS [UPSI ID],IDOFIP AS [ID OF IP],IDOFDP AS [ID OF DP],UPSINATURE AS [Nature of UPSI], SHARINGPURPOSE AS [Purpose of Sharing], SHARINGDATE AS [Date of Sharing],EFFECTIVEUPTO AS [Effective Upto],REMARKS AS [Remarks], NDASIGNED AS [Whether NDA has been signed and Notice of confidentiality has been given ?],UPSIAVAILABLE AS [Date when UPSI became publicly available] FROM T_INS_UPSI_LOG WHERE ID = '" + val[1] + "'");

                                string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                output += "Updated Value :- ";

                                for (int j = 1; j <= columnNames1.Length - 1; j++)
                                {
                                    if (columnNames1[j] != "ID OF DP")
                                    {
                                        string IDofipdp = "";
                                        if (j == 1)
                                        {
                                            if (columnNames1[j] == "ID OF IP")
                                            {
                                                string idip = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
                                                string iddp = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j + 1].ToString());

                                                DataSet dsIPrec = new DataSet();
                                                DataSet dsIPinf = new DataSet();
                                                DataSet dsDPrec = new DataSet();
                                                DataSet dsDPinf = new DataSet();

                                                if (idip.Trim() != "" && idip.Trim() != null)
                                                {
                                                    //dsIP = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + idip + ")");
                                                    string[] aa = idip.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null || rec[1] != "")
                                                        {
                                                            dsIPrec = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }
                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1].Trim() != null && recA[1].Trim() != "")
                                                        {
                                                            dsIPinf = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }
                                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                                {
                                                    //dsDP = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + iddp + ")");
                                                    string[] aa = iddp.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null && rec[1] != "")
                                                        {
                                                            dsDPrec = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }

                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1] != null && recA[1] != "")
                                                        {
                                                            dsDPinf = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }

                                                if (dsIPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "Recepient ID's :" + " - " + IDofipdp;//columnNames[j] + " - " + IDofipdp;
                                                string IDofipdpa = "";
                                                if (dsIPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdpa == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "Informants ID's :" + " - " + IDofipdpa;//columnNames[j] + " - " + IDofipdp;
                                            }
                                            else
                                            {
                                                output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                            }
                                        }
                                        else
                                        {
                                            if (columnNames1[j] == "ID OF IP")
                                            {
                                                string idip = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
                                                string iddp = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j + 1].ToString());
                                                DataSet dsIPrec = new DataSet();
                                                DataSet dsIPinf = new DataSet();
                                                DataSet dsDPrec = new DataSet();
                                                DataSet dsDPinf = new DataSet();

                                                if (idip.Trim() != "" && idip.Trim() != null)
                                                {
                                                    //dsIP = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + idip + ")");
                                                    string[] aa = idip.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null || rec[1] != "")
                                                        {
                                                            dsIPrec = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }
                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1].Trim() != null && recA[1].Trim() != "")
                                                        {
                                                            dsIPinf = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }
                                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                                {
                                                    //dsDP = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + iddp + ")");
                                                    string[] aa = iddp.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null && rec[1] != "")
                                                        {
                                                            dsDPrec = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }

                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1] != null && recA[1] != "")
                                                        {
                                                            dsDPinf = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }

                                                if (dsIPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Recepient ID's :" + " - " + IDofipdp;//columnNames[j] + " - " + IDofipdp;
                                                string IDofipdpa = "";
                                                if (dsIPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdpa == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Informants ID's :" + " - " + IDofipdpa;//columnNames[j] + " - " + IDofipdp;
                                            }
                                            else
                                            {
                                                output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                            }
                                        }
                                    }
                                }
                                output += "Previous Value :- ";


                                for (int j = 0; j < columnNames.Length; j++)
                                {
                                    if (columnNames[j] != "ID OF DP")
                                    {
                                        string IDofipdp = "";
                                        if (j == 0)
                                        {
                                            if (columnNames[j] == "ID OF IP")
                                            {
                                                string idip = CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
                                                string iddp = CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j + 1].ToString());
                                                DataSet dsIPrec = new DataSet();
                                                DataSet dsIPinf = new DataSet();
                                                DataSet dsDPrec = new DataSet();
                                                DataSet dsDPinf = new DataSet();

                                                if (idip.Trim() != "" && idip.Trim() != null)
                                                {
                                                    //dsIP = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + idip + ")");
                                                    string[] aa = idip.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null || rec[1] != "")
                                                        {
                                                            dsIPrec = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }
                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1].Trim() != null && recA[1].Trim() != "")
                                                        {
                                                            dsIPinf = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }
                                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                                {
                                                    //dsDP = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + iddp + ")");
                                                    string[] aa = iddp.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null && rec[1] != "")
                                                        {
                                                            dsDPrec = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }

                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1] != null && recA[1] != "")
                                                        {
                                                            dsDPinf = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }

                                                if (dsIPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Recepient ID's :" + " - " + IDofipdp;//columnNames[j] + " - " + IDofipdp;
                                                string IDofipdpa = "";
                                                if (dsIPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdpa == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Informants ID's :" + " - " + IDofipdpa;//columnNames[j] + " - " + IDofipdp;
                                            }
                                            else
                                            {
                                                output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                            }
                                        }
                                        else
                                        {
                                            if (columnNames[j] == "ID OF IP")
                                            {
                                                string idip = CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
                                                string iddp = CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j + 1].ToString());
                                                DataSet dsIPrec = new DataSet();
                                                DataSet dsIPinf = new DataSet();
                                                DataSet dsDPrec = new DataSet();
                                                DataSet dsDPinf = new DataSet();

                                                if (idip.Trim() != "" && idip.Trim() != null)
                                                {
                                                    //dsIP = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + idip + ")");
                                                    string[] aa = idip.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null || rec[1] != "")
                                                        {
                                                            dsIPrec = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }
                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1].Trim() != null && recA[1].Trim() != "")
                                                        {
                                                            dsIPinf = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }
                                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                                {
                                                    //dsDP = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + iddp + ")");
                                                    string[] aa = iddp.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null && rec[1] != "")
                                                        {
                                                            dsDPrec = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }

                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1] != null && recA[1] != "")
                                                        {
                                                            dsDPinf = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }

                                                if (dsIPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Recepient ID's :" + " - " + IDofipdp;//columnNames[j] + " - " + IDofipdp;
                                                string IDofipdpa = "";
                                                if (dsIPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdpa == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Informants ID's :" + " - " + IDofipdpa;//columnNames[j] + " - " + IDofipdp;
                                            }
                                            else
                                            {
                                                output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                            }
                                        }
                                    }
                                }


                                for (int k = 0; k < columnNames.Length; k++)
                                {
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (columnNames1[j] == columnNames[k])
                                        {
                                            if (CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim() != CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][k].ToString()).Trim())
                                            {
                                                if (k == 1)
                                                {
                                                    updtvalue += columnNames[k];
                                                }
                                                else
                                                {
                                                    updtvalue += " | " + columnNames[k];
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
                                string IDofipdp = "";
                                for (int j = 1; j <= columnNames.Length - 1; j++)
                                {
                                    if (columnNames[j] != "ID OF DP")
                                    {
                                        if (j == 1)
                                        {
                                            if (columnNames[j] == "ID OF IP")
                                            {
                                                string idip = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
                                                string iddp = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j + 1].ToString());
                                                DataSet dsIPrec = new DataSet();
                                                DataSet dsIPinf = new DataSet();
                                                DataSet dsDPrec = new DataSet();
                                                DataSet dsDPinf = new DataSet();

                                                if (idip.Trim() != "" && idip.Trim() != null)
                                                {
                                                    //dsIP = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + idip + ")");
                                                    string[] aa = idip.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null || rec[1] != "")
                                                        {
                                                            dsIPrec = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }
                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1].Trim() != null && recA[1].Trim() != "")
                                                        {
                                                            dsIPinf = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }
                                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                                {
                                                    //dsDP = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + iddp + ")");
                                                    string[] aa = iddp.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null && rec[1] != "")
                                                        {
                                                            dsDPrec = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }

                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1] != null && recA[1] != "")
                                                        {
                                                            dsDPinf = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }

                                                if (dsIPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Recepient ID's :" + " - " + IDofipdp;//columnNames[j] + " - " + IDofipdp;
                                                string IDofipdpa = "";
                                                if (dsIPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdpa == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Informants ID's :" + " - " + IDofipdpa;//columnNames[j] + " - " + IDofipdp;
                                            }
                                            else
                                            {
                                                output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                            }
                                        }
                                        else
                                        {
                                            if (columnNames[j] == "ID OF IP")
                                            {
                                                string idip = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
                                                string iddp = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j + 1].ToString());
                                                DataSet dsIPrec = new DataSet();
                                                DataSet dsIPinf = new DataSet();
                                                DataSet dsDPrec = new DataSet();
                                                DataSet dsDPinf = new DataSet();

                                                if (idip.Trim() != "" && idip.Trim() != null)
                                                {
                                                    //dsIP = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + idip + ")");
                                                    string[] aa = idip.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null || rec[1] != "")
                                                        {
                                                            dsIPrec = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }
                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1].Trim() != null && recA[1].Trim() != "")
                                                        {
                                                            dsIPinf = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }
                                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                                {
                                                    //dsDP = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + iddp + ")");
                                                    string[] aa = iddp.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null && rec[1] != "")
                                                        {
                                                            dsDPrec = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }

                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1] != null && recA[1] != "")
                                                        {
                                                            dsDPinf = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }

                                                if (dsIPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Recepient ID's :" + " - " + IDofipdp;//columnNames[j] + " - " + IDofipdp;
                                                string IDofipdpa = "";
                                                if (dsIPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdpa == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Informants ID's :" + " - " + IDofipdpa;//columnNames[j] + " - " + IDofipdp;
                                            }
                                            else
                                            {
                                                output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()).Trim() == "MASTER DATA OF COMPANY PROFILE TAB")
                        {
                            string[] val = ds.Tables[0].Rows[i]["TID"].ToString().Split('|');
                            sgrow = new MasterClass().getDataSet("SELECT TID,COMPANYNAME AS [Company Name],REGOFFICE AS [Registered Office],CORPORATEOFFICE AS [Corporate Office],MOBILENO AS [Mobile No],LANDLINENO AS [Landline No],EMAILID AS [Email Id],CIN AS [CIN],BSECODE AS [BSE SCRIP CODE],NSECODE AS [NSE SCIRP CODE],ISIN AS [ISIN],OFFICERNAME AS [Compliance Officer Name],DESIGNATION AS [Desgination] FROM T_INS_COMPANY_LOG WHERE ID = '" + val[0] + "'");
                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()).Trim() == "UPDATED")
                            {
                                updtrow = new MasterClass().getDataSet("SELECT COMPANYNAME AS [Company Name],REGOFFICE AS [Registered Office],CORPORATEOFFICE AS [Corporate Office],MOBILENO AS [Mobile No],LANDLINENO AS [Landline No],EMAILID AS [Email Id],CIN AS [CIN],BSECODE AS [BSE SCRIP CODE],NSECODE AS [NSE SCIRP CODE],ISIN AS [ISIN],OFFICERNAME AS [Compliance Officer Name],DESIGNATION AS [Desgination] FROM T_INS_COMPANY_LOG WHERE ID = '" + val[1] + "'");

                                string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                output += "Updated Value :- ";
                                for (int j = 1; j <= columnNames1.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                                output += "Previous Value :- ";


                                for (int j = 0; j < columnNames.Length; j++)
                                {
                                    if (j == 0)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }


                                for (int k = 0; k < columnNames.Length; k++)
                                {
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (columnNames1[j] == columnNames[k])
                                        {
                                            if (CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim() != CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][k].ToString()).Trim())
                                            {
                                                if (k == 1)
                                                {
                                                    updtvalue += columnNames[k];
                                                }
                                                else
                                                {
                                                    updtvalue += " | " + columnNames[k];
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                for (int j = 1; j <= columnNames.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                            }

                        }

                        DataSet dsLogin = new MasterClass().getDataSet("SELECT EMAIL FROM T_LOGIN WHERE ID = '" + ds.Tables[0].Rows[i]["ENTEREDBY"].ToString() + "'");
                        string a = "";
                        if (dsLogin.Tables[0].Rows.Count > 0)
                        {
                            a = dsLogin.Tables[0].Rows[0]["EMAIL"].ToString();
                        }

                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
                        {
                            if (updtvalue != "")
                            {
                                string[] row = { a.Trim(), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DESCRIPTION"].ToString()), MasterClass.GETIST(ds.Tables[0].Rows[i]["ENTEREDON"].ToString()), updtvalue, output };
                                dataGridViewTable.Rows.Add(row);
                            }
                        }
                        else
                        {
                            string[] row = { a.Trim(), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DESCRIPTION"].ToString()), MasterClass.GETIST(ds.Tables[0].Rows[i]["ENTEREDON"].ToString()), updtvalue, output };
                            dataGridViewTable.Rows.Add(row);
                        }



                    }
                }

                dataGridViewTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                //});

                //SetLoading(false);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Audit Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGrid(txtFromDate.Value, txtToDate.Value);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                DialogResult dialog = MessageBox.Show("Following Value doesnt Match Any Records.", "Audit Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillDataGrid()
        {
            try
            {
                //SetLoading(true);

                ////Thread.Sleep(2000);
                //Invoke((MethodInvoker)delegate
                //{

                dataGridViewTable.Rows.Clear();
                dataGridViewTable.Refresh();

                DataSet ds = new MasterClass().getDataSet("SELECT TOP(30) * FROM M_LOG_AUDIT ORDER BY ID DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    int z = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataSet sgrow = new DataSet();
                        DataSet updtrow = new DataSet();
                        string output = "";
                        string updtvalue = "";
                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()).Trim() == "DESIGNATED PERSON TAB")
                        {
                            string[] val = ds.Tables[0].Rows[i]["TID"].ToString().Split('|');
                            sgrow = new MasterClass().getDataSet("SELECT TID,CONNECTPERSONID AS [Designated Person Id],EMPNAME AS [Name of the Employee],CURRDESIGNATION AS [Current Designation],ADDRESS AS [Permanent Address],RESIADDRESS AS [Correspondence Address], OTHERIDENTIFIER AS [Other Identifier], PANNO AS[PAN], DEMATACNO AS [Demat A/c No], MOBILENO AS [Mobile No], EMAILID AS [Email ID],GRADUATIONINSTI AS [Graduation Institution], PASTEMP AS [Past Employee] FROM T_INS_PER_LOG WHERE ID = '" + val[0] + "'");

                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()).Trim() == "UPDATED")
                            {
                                updtrow = new MasterClass().getDataSet("SELECT CONNECTPERSONID AS [Designated Person Id],EMPNAME AS [Name of the Employee],CURRDESIGNATION AS [Current Designation],ADDRESS AS [Permanent Address],RESIADDRESS AS [Correspondence Address], OTHERIDENTIFIER AS [Other Identifier], PANNO AS[PAN], DEMATACNO AS [Demat A/c No], MOBILENO AS [Mobile No], EMAILID AS [Email ID],GRADUATIONINSTI AS [Graduation Institution], PASTEMP AS [Past Employee] FROM T_INS_PER_LOG WHERE ID = '" + val[1] + "'");

                                string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                output += "Updated Value :- ";
                                for (int j = 1; j <= columnNames1.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                                output += "Previous Value :- ";


                                for (int j = 0; j < columnNames.Length; j++)
                                {
                                    if (j == 0)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }


                                for (int k = 0; k < columnNames.Length; k++)
                                {
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (columnNames1[j] == columnNames[k])
                                        {
                                            if (CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim() != CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][k].ToString()).Trim())
                                            {
                                                if (k == 1)
                                                {
                                                    updtvalue += columnNames[k];
                                                }
                                                else
                                                {
                                                    updtvalue += " | " + columnNames[k];
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                for (int j = 1; j <= columnNames.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                            }

                        }

                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()).Trim() == "DESIGNATED PERSON TAB RELATIVE RELATIONSHIP")
                        {
                            string[] val = ds.Tables[0].Rows[i]["TID"].ToString().Split('|');
                            sgrow = new MasterClass().getDataSet("SELECT TID,NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],EMAILID AS [Email ID],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat A/c No],TYPE as [TYPE] FROM T_INS_PER_DT_LOG WHERE ID = '" + val[0] + "'");
                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()).Trim() == "UPDATED")
                            {
                                if (val.Length < 2)
                                {
                                    string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                    output += "Inserted New Value :- ";
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (j == 1)
                                        {
                                            output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                        else
                                        {
                                            output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                    }
                                    updtvalue += "Inserted New Value";
                                }
                                else
                                {
                                    updtrow = new MasterClass().getDataSet("SELECT NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],EMAILID AS [Email ID],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat A/c No],TYPE as [TYPE] FROM T_INS_PER_DT_LOG WHERE ID = '" + val[1] + "'");

                                    string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                    string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                    output += "Updated Value :- ";
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (j == 1)
                                        {
                                            output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                        else
                                        {
                                            output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                    }
                                    output += "Previous Value :- ";

                                    for (int j = 0; j < columnNames.Length; j++)
                                    {
                                        if (j == 0)
                                        {
                                            output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                        else
                                        {
                                            output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                    }

                                    for (int k = 0; k < columnNames.Length; k++)
                                    {
                                        for (int j = 1; j <= columnNames1.Length - 1; j++)
                                        {
                                            if (columnNames1[j] == columnNames[k])
                                            {
                                                if (CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim() != CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][k].ToString()).Trim())
                                                {
                                                    if (k == 1)
                                                    {
                                                        updtvalue += columnNames[k];
                                                    }
                                                    else
                                                    {
                                                        updtvalue += " | " + columnNames[k];
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                for (int j = 1; j <= columnNames.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                            }

                        }

                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()).Trim() == "DESIGNATED PERSON TAB FINANCIAL RELATIONSHIP")
                        {
                            string[] val = ds.Tables[0].Rows[i]["TID"].ToString().Split('|');
                            sgrow = new MasterClass().getDataSet("SELECT TID,NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],EMAILID AS [Email ID],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat A/c No],TYPE as [TYPE] FROM T_INS_PER_DT_LOG WHERE ID = '" + val[0] + "'");
                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()).Trim() == "UPDATED")
                            {
                                if (val.Length < 2)
                                {
                                    string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                    output += "Inserted New Value :- ";
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (j == 1)
                                        {
                                            output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                        else
                                        {
                                            output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                    }

                                    updtvalue += "Inserted New Value";
                                }
                                else
                                {

                                    updtrow = new MasterClass().getDataSet("SELECT NAME AS [Name],ADDRESS AS [Address],RELATIONSHIP AS [Relationship],EMAILID AS [Email ID],[MOBILENO] AS [Mobile No],PANNO AS [Pan No],[DEMATACNO] AS [Demat A/c No],TYPE as [TYPE] FROM T_INS_PER_DT_LOG WHERE ID = '" + val[1] + "'");

                                    string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                    string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                    output += "Updated Value :- ";
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (j == 1)
                                        {
                                            output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                        else
                                        {
                                            output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                    }
                                    output += "Previous Value :- ";

                                    for (int j = 0; j < columnNames.Length; j++)
                                    {
                                        if (j == 0)
                                        {
                                            output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                        else
                                        {
                                            output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                        }
                                    }

                                    for (int k = 0; k < columnNames.Length; k++)
                                    {
                                        for (int j = 1; j <= columnNames1.Length - 1; j++)
                                        {
                                            if (columnNames1[j] == columnNames[k])
                                            {
                                                if (CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim() != CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][k].ToString()).Trim())
                                                {
                                                    if (k == 1)
                                                    {
                                                        updtvalue += columnNames[k];
                                                    }
                                                    else
                                                    {
                                                        updtvalue += " | " + columnNames[k];
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                for (int j = 1; j <= columnNames.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                            }

                        }

                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()).Trim() == "INSIDER PROFILE TAB")
                        {
                            string[] val = ds.Tables[0].Rows[i]["TID"].ToString().Split('|');
                            sgrow = new MasterClass().getDataSet("SELECT TID,RECEPIENTID AS [Recipient Id],NAMEINSIDER AS [Name of the Insider],CATEGORYRECEIPT AS [Category of Receipt], ADDRESS AS [Address], OTHERIDENTIFIER AS [Other Identifier], PANNO AS [PAN], AADHARNO AS [Aadhar No],MOBILENO AS [Mobile No],LANDLINENO AS[Landline No], EMAILID AS [Email Id], PANNOAFFILIATES AS [PAN No.of Affiliates] FROM T_INS_PRO_LOG WHERE ID = '" + val[0] + "'");
                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()).Trim() == "UPDATED")
                            {
                                updtrow = new MasterClass().getDataSet("SELECT RECEPIENTID AS [Recipient Id],NAMEINSIDER AS [Name of the Insider],CATEGORYRECEIPT AS [Category of Receipt], ADDRESS AS [Address], OTHERIDENTIFIER AS [Other Identifier], PANNO AS [PAN], AADHARNO AS [Aadhar No],MOBILENO AS [Mobile No],LANDLINENO AS[Landline No], EMAILID AS [Email Id], PANNOAFFILIATES AS [PAN No.of Affiliates] FROM T_INS_PRO_LOG WHERE ID = '" + val[1] + "'");


                                string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                output += "Updated Value :- ";
                                for (int j = 1; j <= columnNames1.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                                output += "Previous Value :- ";


                                for (int j = 0; j < columnNames.Length; j++)
                                {
                                    if (j == 0)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }


                                for (int k = 0; k < columnNames.Length; k++)
                                {
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (columnNames1[j] == columnNames[k])
                                        {
                                            if (CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim() != CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][k].ToString()).Trim())
                                            {
                                                if (k == 1)
                                                {
                                                    updtvalue += columnNames[k];
                                                }
                                                else
                                                {
                                                    updtvalue += " | " + columnNames[k];
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            else
                            {
                                string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                for (int j = 1; j <= columnNames.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                            }


                        }

                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()).Trim() == "SHARING OF UPSI PROFILE TAB")
                        {
                            string[] val = ds.Tables[0].Rows[i]["TID"].ToString().Split('|');
                            sgrow = new MasterClass().getDataSet("SELECT TID,UPSIID AS [UPSI ID],IDOFIP AS [ID OF IP],IDOFDP AS [ID OF DP],UPSINATURE AS [Nature of UPSI], SHARINGPURPOSE AS [Purpose of Sharing], SHARINGDATE AS [Date of Sharing],EFFECTIVEUPTO AS [Effective Upto],REMARKS AS [Remarks], NDASIGNED AS [Whether NDA has been signed and Notice of confidentiality has been given ?],UPSIAVAILABLE AS [Date when UPSI became publicly available] FROM T_INS_UPSI_LOG WHERE ID = '" + val[0] + "'");
                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()).Trim() == "UPDATED")
                            {
                                updtrow = new MasterClass().getDataSet("SELECT UPSIID AS [UPSI ID],IDOFIP AS [ID OF IP],IDOFDP AS [ID OF DP],UPSINATURE AS [Nature of UPSI], SHARINGPURPOSE AS [Purpose of Sharing], SHARINGDATE AS [Date of Sharing],EFFECTIVEUPTO AS [Effective Upto],REMARKS AS [Remarks], NDASIGNED AS [Whether NDA has been signed and Notice of confidentiality has been given ?],UPSIAVAILABLE AS [Date when UPSI became publicly available] FROM T_INS_UPSI_LOG WHERE ID = '" + val[1] + "'");

                                string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                output += "Updated Value :- ";

                                for (int j = 1; j <= columnNames1.Length - 1; j++)
                                {
                                    if (columnNames1[j] != "ID OF DP")
                                    {
                                        string IDofipdp = "";
                                        if (j == 1)
                                        {
                                            if (columnNames1[j] == "ID OF IP")
                                            {
                                                string idip = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
                                                string iddp = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j + 1].ToString());

                                                DataSet dsIPrec = new DataSet();
                                                DataSet dsIPinf = new DataSet();
                                                DataSet dsDPrec = new DataSet();
                                                DataSet dsDPinf = new DataSet();

                                                if (idip.Trim() != "" && idip.Trim() != null)
                                                {
                                                    //dsIP = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + idip + ")");
                                                    string[] aa = idip.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null || rec[1] != "")
                                                        {
                                                            dsIPrec = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }
                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1].Trim() != null && recA[1].Trim() != "")
                                                        {
                                                            dsIPinf = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }
                                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                                {
                                                    //dsDP = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + iddp + ")");
                                                    string[] aa = iddp.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null && rec[1] != "")
                                                        {
                                                            dsDPrec = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }

                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1] != null && recA[1] != "")
                                                        {
                                                            dsDPinf = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }

                                                if (dsIPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "Recepient ID's :" + " - " + IDofipdp;//columnNames[j] + " - " + IDofipdp;
                                                string IDofipdpa = "";
                                                if (dsIPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdpa == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "Informants ID's :" + " - " + IDofipdpa;//columnNames[j] + " - " + IDofipdp;
                                            }
                                            else
                                            {
                                                output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                            }
                                        }
                                        else
                                        {
                                            if (columnNames1[j] == "ID OF IP")
                                            {
                                                string idip = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
                                                string iddp = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j + 1].ToString());
                                                DataSet dsIPrec = new DataSet();
                                                DataSet dsIPinf = new DataSet();
                                                DataSet dsDPrec = new DataSet();
                                                DataSet dsDPinf = new DataSet();

                                                if (idip.Trim() != "" && idip.Trim() != null)
                                                {
                                                    //dsIP = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + idip + ")");
                                                    string[] aa = idip.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null || rec[1] != "")
                                                        {
                                                            dsIPrec = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }
                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1].Trim() != null && recA[1].Trim() != "")
                                                        {
                                                            dsIPinf = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }
                                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                                {
                                                    //dsDP = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + iddp + ")");
                                                    string[] aa = iddp.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null && rec[1] != "")
                                                        {
                                                            dsDPrec = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }

                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1] != null && recA[1] != "")
                                                        {
                                                            dsDPinf = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }

                                                if (dsIPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Recepient ID's :" + " - " + IDofipdp;//columnNames[j] + " - " + IDofipdp;
                                                string IDofipdpa = "";
                                                if (dsIPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdpa == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Informants ID's :" + " - " + IDofipdpa;//columnNames[j] + " - " + IDofipdp;
                                            }
                                            else
                                            {
                                                output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                            }
                                        }
                                    }
                                }
                                output += "Previous Value :- ";


                                for (int j = 0; j < columnNames.Length; j++)
                                {
                                    if (columnNames[j] != "ID OF DP")
                                    {
                                        string IDofipdp = "";
                                        if (j == 0)
                                        {
                                            if (columnNames[j] == "ID OF IP")
                                            {
                                                string idip = CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
                                                string iddp = CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j + 1].ToString());
                                                DataSet dsIPrec = new DataSet();
                                                DataSet dsIPinf = new DataSet();
                                                DataSet dsDPrec = new DataSet();
                                                DataSet dsDPinf = new DataSet();

                                                if (idip.Trim() != "" && idip.Trim() != null)
                                                {
                                                    //dsIP = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + idip + ")");
                                                    string[] aa = idip.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null || rec[1] != "")
                                                        {
                                                            dsIPrec = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }
                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1].Trim() != null && recA[1].Trim() != "")
                                                        {
                                                            dsIPinf = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }
                                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                                {
                                                    //dsDP = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + iddp + ")");
                                                    string[] aa = iddp.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null && rec[1] != "")
                                                        {
                                                            dsDPrec = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }

                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1] != null && recA[1] != "")
                                                        {
                                                            dsDPinf = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }

                                                if (dsIPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Recepient ID's :" + " - " + IDofipdp;//columnNames[j] + " - " + IDofipdp;
                                                string IDofipdpa = "";
                                                if (dsIPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdpa == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Informants ID's :" + " - " + IDofipdpa;//columnNames[j] + " - " + IDofipdp;
                                            }
                                            else
                                            {
                                                output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                            }
                                        }
                                        else
                                        {
                                            if (columnNames[j] == "ID OF IP")
                                            {
                                                string idip = CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString());
                                                string iddp = CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j + 1].ToString());
                                                DataSet dsIPrec = new DataSet();
                                                DataSet dsIPinf = new DataSet();
                                                DataSet dsDPrec = new DataSet();
                                                DataSet dsDPinf = new DataSet();

                                                if (idip.Trim() != "" && idip.Trim() != null)
                                                {
                                                    //dsIP = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + idip + ")");
                                                    string[] aa = idip.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null || rec[1] != "")
                                                        {
                                                            dsIPrec = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }
                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1].Trim() != null && recA[1].Trim() != "")
                                                        {
                                                            dsIPinf = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }
                                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                                {
                                                    //dsDP = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + iddp + ")");
                                                    string[] aa = iddp.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null && rec[1] != "")
                                                        {
                                                            dsDPrec = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }

                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1] != null && recA[1] != "")
                                                        {
                                                            dsDPinf = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }

                                                if (dsIPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Recepient ID's :" + " - " + IDofipdp;//columnNames[j] + " - " + IDofipdp;
                                                string IDofipdpa = "";
                                                if (dsIPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdpa == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Informants ID's :" + " - " + IDofipdpa;//columnNames[j] + " - " + IDofipdp;
                                            }
                                            else
                                            {
                                                output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                            }
                                        }
                                    }
                                }


                                for (int k = 0; k < columnNames.Length; k++)
                                {
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (columnNames1[j] == columnNames[k])
                                        {
                                            if (CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim() != CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][k].ToString()).Trim())
                                            {
                                                if (k == 1)
                                                {
                                                    updtvalue += columnNames[k];
                                                }
                                                else
                                                {
                                                    updtvalue += " | " + columnNames[k];
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
                                string IDofipdp = "";
                                for (int j = 1; j <= columnNames.Length - 1; j++)
                                {
                                    if (columnNames[j] != "ID OF DP")
                                    {
                                        if (j == 1)
                                        {
                                            if (columnNames[j] == "ID OF IP")
                                            {
                                                string idip = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
                                                string iddp = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j + 1].ToString());
                                                DataSet dsIPrec = new DataSet();
                                                DataSet dsIPinf = new DataSet();
                                                DataSet dsDPrec = new DataSet();
                                                DataSet dsDPinf = new DataSet();

                                                if (idip.Trim() != "" && idip.Trim() != null)
                                                {
                                                    //dsIP = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + idip + ")");
                                                    string[] aa = idip.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null || rec[1] != "")
                                                        {
                                                            dsIPrec = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }
                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1].Trim() != null && recA[1].Trim() != "")
                                                        {
                                                            dsIPinf = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }
                                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                                {
                                                    //dsDP = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + iddp + ")");
                                                    string[] aa = iddp.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null && rec[1] != "")
                                                        {
                                                            dsDPrec = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }

                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1] != null && recA[1] != "")
                                                        {
                                                            dsDPinf = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }

                                                if (dsIPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Recepient ID's :" + " - " + IDofipdp;//columnNames[j] + " - " + IDofipdp;
                                                string IDofipdpa = "";
                                                if (dsIPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdpa == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Informants ID's :" + " - " + IDofipdpa;//columnNames[j] + " - " + IDofipdp;
                                            }
                                            else
                                            {
                                                output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                            }
                                        }
                                        else
                                        {
                                            if (columnNames[j] == "ID OF IP")
                                            {
                                                string idip = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString());
                                                string iddp = CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j + 1].ToString());
                                                DataSet dsIPrec = new DataSet();
                                                DataSet dsIPinf = new DataSet();
                                                DataSet dsDPrec = new DataSet();
                                                DataSet dsDPinf = new DataSet();

                                                if (idip.Trim() != "" && idip.Trim() != null)
                                                {
                                                    //dsIP = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + idip + ")");
                                                    string[] aa = idip.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null || rec[1] != "")
                                                        {
                                                            dsIPrec = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }
                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1].Trim() != null && recA[1].Trim() != "")
                                                        {
                                                            dsIPinf = new MasterClass().getDataSet("SELECT RECEPIENTID FROM T_INS_PRO WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }
                                                if (iddp.Trim() != "" && iddp.Trim() != null)
                                                {
                                                    //dsDP = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + iddp + ")");
                                                    string[] aa = iddp.Split('|');
                                                    if (aa[0] != "" && aa[0] != null)
                                                    {
                                                        string[] rec = aa[0].Split(':');
                                                        if (rec[1] != null && rec[1] != "")
                                                        {
                                                            dsDPrec = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + rec[1] + ")");
                                                        }
                                                    }

                                                    if (aa[1] != "" && aa[1] != null)
                                                    {
                                                        string[] recA = aa[1].Split(':');
                                                        if (recA[1] != null && recA[1] != "")
                                                        {
                                                            dsDPinf = new MasterClass().getDataSet("SELECT CONNECTPERSONID FROM T_INS_PER WHERE ACTIVE = 'Y' AND LOCK = 'N'  AND ID IN (" + recA[1] + ")");
                                                        }
                                                    }
                                                }

                                                if (dsIPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsIPrec.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPrec.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPrec.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdp += CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdp += "-" + CryptographyHelper.Decrypt(dsDPrec.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Recepient ID's :" + " - " + IDofipdp;//columnNames[j] + " - " + IDofipdp;
                                                string IDofipdpa = "";
                                                if (dsIPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsIPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdpa == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsIPinf.Tables[0].Rows[k]["RECEPIENTID"].ToString());
                                                        }
                                                    }
                                                }

                                                if (dsDPinf.Tables.Count > 0)
                                                {
                                                    for (int k = 0; k < dsDPinf.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (IDofipdp == "")
                                                        {
                                                            IDofipdpa += CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                        else
                                                        {
                                                            IDofipdpa += "-" + CryptographyHelper.Decrypt(dsDPinf.Tables[0].Rows[k]["CONNECTPERSONID"].ToString());
                                                        }
                                                    }
                                                }

                                                output += "|" + "Informants ID's :" + " - " + IDofipdpa;//columnNames[j] + " - " + IDofipdp;
                                            }
                                            else
                                            {
                                                output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()).Trim() == "MASTER DATA OF COMPANY PROFILE TAB")
                        {
                            string[] val = ds.Tables[0].Rows[i]["TID"].ToString().Split('|');
                            sgrow = new MasterClass().getDataSet("SELECT TID,COMPANYNAME AS [Company Name],REGOFFICE AS [Registered Office],CORPORATEOFFICE AS [Corporate Office],MOBILENO AS [Mobile No],LANDLINENO AS [Landline No],EMAILID AS [Email Id],CIN AS [CIN],BSECODE AS [BSE SCRIP CODE],NSECODE AS [NSE SCIRP CODE],ISIN AS [ISIN],OFFICERNAME AS [Compliance Officer Name],DESIGNATION AS [Desgination] FROM T_INS_COMPANY_LOG WHERE ID = '" + val[0] + "'");
                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()).Trim() == "UPDATED")
                            {
                                updtrow = new MasterClass().getDataSet("SELECT COMPANYNAME AS [Company Name],REGOFFICE AS [Registered Office],CORPORATEOFFICE AS [Corporate Office],MOBILENO AS [Mobile No],LANDLINENO AS [Landline No],EMAILID AS [Email Id],CIN AS [CIN],BSECODE AS [BSE SCRIP CODE],NSECODE AS [NSE SCIRP CODE],ISIN AS [ISIN],OFFICERNAME AS [Compliance Officer Name],DESIGNATION AS [Desgination] FROM T_INS_COMPANY_LOG WHERE ID = '" + val[1] + "'");

                                string[] columnNames = updtrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                string[] columnNames1 = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                output += "Updated Value :- ";
                                for (int j = 1; j <= columnNames1.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames1[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                                output += "Previous Value :- ";


                                for (int j = 0; j < columnNames.Length; j++)
                                {
                                    if (j == 0)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }


                                for (int k = 0; k < columnNames.Length; k++)
                                {
                                    for (int j = 1; j <= columnNames1.Length - 1; j++)
                                    {
                                        if (columnNames1[j] == columnNames[k])
                                        {
                                            if (CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim() != CryptographyHelper.Decrypt(updtrow.Tables[0].Rows[0][k].ToString()).Trim())
                                            {
                                                if (k == 1)
                                                {
                                                    updtvalue += columnNames[k];
                                                }
                                                else
                                                {
                                                    updtvalue += " | " + columnNames[k];
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string[] columnNames = sgrow.Tables[0].Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                                for (int j = 1; j <= columnNames.Length - 1; j++)
                                {
                                    if (j == 1)
                                    {
                                        output += columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                    else
                                    {
                                        output += " | " + columnNames[j] + " - " + CryptographyHelper.Decrypt(sgrow.Tables[0].Rows[0][j].ToString()).Trim();
                                    }
                                }
                            }

                        }

                        DataSet dsLogin = new MasterClass().getDataSet("SELECT EMAIL FROM T_LOGIN WHERE ID = '" + ds.Tables[0].Rows[i]["ENTEREDBY"].ToString() + "'");
                        string a = "";
                        if (dsLogin.Tables[0].Rows.Count > 0)
                        {
                            a = dsLogin.Tables[0].Rows[0]["EMAIL"].ToString();
                        }

                        if (z < 10)
                        {
                            if (CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()) == "UPDATED")
                            {
                                if (updtvalue != "")
                                {
                                    string[] row = { a.Trim(), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DESCRIPTION"].ToString()), MasterClass.GETIST(ds.Tables[0].Rows[i]["ENTEREDON"].ToString()), updtvalue, output };
                                    dataGridViewTable.Rows.Add(row);
                                    z = z + 1;
                                }
                            }
                            else
                            {
                                string[] row = { a.Trim(), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["OPERATION"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DESCRIPTION"].ToString()), MasterClass.GETIST(ds.Tables[0].Rows[i]["ENTEREDON"].ToString()), updtvalue, output };
                                dataGridViewTable.Rows.Add(row);
                                z = z + 1;
                            }
                        }
                        else
                        {
                            break;
                        }

                    }
                }

                dataGridViewTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                //});

                //SetLoading(false);
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                //SetLoading(false);
                DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Audit Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
