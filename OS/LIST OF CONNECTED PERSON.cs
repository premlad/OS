using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OS
{
	public partial class LIST_OF_CONNECTED_PERSON : MASTERFORM
	{
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
			//dataGridViewTable.Rows.Clear();
			dataGridViewTable.Refresh();
			Hashtable hstmst = new Hashtable
				{
					{ "@ACTION", "6" }
				};
			DataSet ds = new MasterClass().executeDatable_SP("STP_INS_PER", hstmst);
			if (ds.Tables[0].Rows.Count > 0)
			{
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					//ds.Tables[0].Rows[i]["CATEGORYRECEIPT"] = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CATEGORYRECEIPT"].ToString());
				}
				DataRelation Datatablerelation = new DataRelation("Following are my immediate relatives & Following are the person with whom, I share material financial relationship", ds.Tables[0].Columns[0], ds.Tables[1].Columns[0], true);
				ds.Relations.Add(Datatablerelation);
				dataGridViewTable.DataSource = ds.Tables[0];

			}
		}

		public void FillDataGrid(string text, string From, string To)
		{

			//dataGridViewTable.Rows.Clear();
			dataGridViewTable.Refresh();
			Hashtable hstmst = new Hashtable
				{
					{ "@FROM", From},
					{ "@TO", To },
					{ "@ACTION", "6" }
				};
			DataSet ds = new MasterClass().executeDatable_SP("STP_INS_PRO", hstmst);
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

					//string[] row = { CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["NAMEINSIDER"].ToString()), cat, CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["ADDRESS"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["AADHARNO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["PANNOAFFILIATES"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["MOBILENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["LANDLINENO"].ToString()), CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["EMAILID"].ToString()), ds.Tables[0].Rows[i]["ENTEREDON"].ToString() };
					//dataGridViewTable.Rows.Add(row);
				}
			}
		}

	}
}
