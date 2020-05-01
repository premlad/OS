using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Data;
using System.Windows.Forms;

namespace OS
{
	public partial class LIST_OF_CONNECTED_PERSON : MASTERFORM
	{
		private AUDITLOG lg = new AUDITLOG();
		public LIST_OF_CONNECTED_PERSON()
		{
			InitializeComponent();
		}

		private void LIST_OF_CONNECTED_PERSON_Load(object sender, EventArgs e)
		{
			Login l = new Login();
			//TopMost = true;
			WindowState = FormWindowState.Maximized;
			try
			{
				if (SESSIONKEYS.UserID.ToString() == "" || SESSIONKEYS.UserID.ToString() == null)
				{
					Hide();
					l.Show();
				}
			}
			catch (Exception)
			{
				Hide();
				l.Show();
			}
			txtFromDate.CustomFormat = "dd-MM-yyyy";
			txtToDate.CustomFormat = "dd-MM-yyyy";
			FillDataGrid();
		}

		private void FillDataGrid()
		{
			dataGridViewTable.Rows.Clear();
			dataGridViewTable.Refresh();
			//DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_PER P, T_INS_PER_DT D WHERE P.ID = D.PERID AND P.ACTIVE = 'Y' AND D.ACTIVE = 'Y' AND P.LOCK = 'N' AND D.LOCK = 'N'");			
			DataSet ds = new MasterClass().getDataSet("SELECT * FROM T_INS_PER P LEFT JOIN T_INS_PER_DT D ON P.ID = D.PERID AND D.ACTIVE = 'Y' AND D.LOCK = 'N' INNER JOIN T_LOGIN L ON L.ID = P.ENTEREDBY WHERE P.ACTIVE = 'Y'  AND P.LOCK = 'N' AND L.ACTIVE = 'Y'");

			if (ds.Tables[0].Rows.Count > 0)
			{
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CONNECTPERSONID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMPNAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CURRDESIGNATION"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["GRADUATIONINSTI"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PASTEMP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["TYPE"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAME"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RELATIONSHIP"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO1"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["DEMATACNO"].ToString()), (ds.Tables[0].Rows[i]["ENTEREDON"].ToString() + " - " + ds.Tables[0].Rows[i]["EMAIL"].ToString()).Trim() };
					dataGridViewTable.Rows.Add(row);
				}
			}
		}

		public void FillDataGrid(string text, string From, string To)
		{

			//dataGridViewTable.Rows.Clear();
			dataGridViewTable.Refresh();

			DataSet ds = new MasterClass().getDataSet("SELECT 'Connect Person Id' = P.CONNECTPERSONID,'Name of the Employee' = P.EMPNAME,'Current Designation' = P.CURRDESIGNATION,'Address' = P.ADDRESS,'PAN' = P.PANNO,'Demat A/c No' = P.DEMATACNO,'Phone No and Cell No' = P.MOBILENO,'Graduation Institutions' = P.GRADUATIONINSTI,'My Past Employee' = P.PASTEMP,'Type' = D.TYPE,'Name' = D.NAME,'Address' = D.ADDRESS,'Relationship' = D.RELATIONSHIP,'Phone No' = D.MOBILENO,'Pan No' = D.PANNO,'Demat Ac No' = D.DEMATACNO FROM T_INS_PER P INNER JOIN T_INS_PER_DT D ON P.ID = D.PERID WHERE P.ACTIVE = 'Y' AND D.ACTIVE = 'Y' AND P.LOCK = 'N' AND D.LOCK = 'N'");

			if (ds.Tables[0].Rows.Count > 0)
			{
				dataGridViewTable.DataSource = ds.Tables[0];
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{

				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			HOMEPAGE h = new HOMEPAGE();
			h.Show();
			Hide();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			FillDataGrid();
		}

		private void btnDownloadexcel_Click(object sender, EventArgs e)
		{
			lg.CURRVALUE = "CONNECTED PERSON TAB";
			lg.DESCRIPTION = "DOWNLOADED EXCEL FILE";
			lg.TYPE = "SELECTED";
			lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
			lg.ID = SESSIONKEYS.UserID.ToString();
			string json = new MasterClass().SAVE_LOG(lg);
			SaveFileDialog sfd = new SaveFileDialog
			{
				Filter = "Excel Documents (*.xls)|*.xls",
				FileName = "LIST OF CONNECTED PERSON.xls"
			};

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				new MasterClass().ToCsV(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
				MessageBox.Show("Exported Data Successfully in Excel Sheet.", "LIST OF CONNECTED PERSON", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void btnDownloadPDF_Click(object sender, EventArgs e)
		{
			lg.CURRVALUE = "CONNECTED PERSON TAB";
			lg.DESCRIPTION = "DOWNLOADED PDF FILE";
			lg.TYPE = "SELECTED";
			lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
			lg.ID = SESSIONKEYS.UserID.ToString();
			string json = new MasterClass().SAVE_LOG(lg);
			SaveFileDialog sfd = new SaveFileDialog
			{
				Filter = "PDF (*.pdf)|*.pdf",
				FileName = "LIST OF CONNECTED PERSON.pdf"
			};
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				new MasterClass().ToPDF(dataGridViewTable, sfd.FileName); // Here dvwACH is your grid view name
				MessageBox.Show("Exported Data Successfully in PDF Sheet.", "LIST OF CONNECTED PERSON", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void btnDownloadPrinter_Click(object sender, EventArgs e)
		{
			lg.CURRVALUE = "CONNECTED PERSON TAB";
			lg.DESCRIPTION = "PRINT FILE";
			lg.TYPE = "SELECTED";
			lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
			lg.ID = SESSIONKEYS.UserID.ToString();
			string json = new MasterClass().SAVE_LOG(lg);
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtInsiderID.Text != "")
				{
					int rowIndex = -1;
					foreach (DataGridViewRow row in dataGridViewTable.Rows)
					{
						if (row.Cells[0].Value.ToString().Equals(txtInsiderID.Text))
						{
							rowIndex = row.Index;
							//dataGridViewTable.Rows[rowIndex].Selected = true;
						}
						else
						{
							dataGridViewTable.Rows.RemoveAt(row.Index);
						}
					}
				}
				else
				{
					FillDataGrid(txtInsiderID.Text, txtFromDate.Value.ToString(), txtToDate.Value.ToString());
				}
			}
			catch (Exception)
			{
				DialogResult dialog = MessageBox.Show("Following Value doesnt Match Any Records.", "List of Insider", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
