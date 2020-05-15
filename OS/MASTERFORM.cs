using OS.Data_Access_Layer;
using OS.Data_Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OS
{
	public partial class MASTERFORM : Form
	{
		private AUDITLOG lg = new AUDITLOG();

		public MASTERFORM()
		{
			InitializeComponent();
		}

		private void listOfConnectedPersonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LIST_OF_CONNECTED_PERSON c = new LIST_OF_CONNECTED_PERSON();
			c.Show();
			Close();
		}

		private void MASTERFORM_Load(object sender, EventArgs e)
		{
			//WindowState = FormWindowState.Maximized;
		}

		private List<Control> GetAllControls(Control container, List<Control> list)
		{
			foreach (Control c in container.Controls)
			{
				if (c.Controls.Count > 0)
				{
					list = GetAllControls(c, list);
				}
				else
				{
					list.Add(c);
				}
			}
			return list;
		}
		private List<Control> GetAllControls(Control container)
		{
			return GetAllControls(container, new List<Control>());
		}

		private void rECORDINGINSIDERCONNECTEDPERSONToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MASTER_FOR_RECORDING_INSIDER_CONNECTED_PERSON mr = new MASTER_FOR_RECORDING_INSIDER_CONNECTED_PERSON();
			mr.Show();
			Close();
		}

		private void rECORDINGINSIDERPROFILEToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MASTER_FOR_RECORDING_INSIDER_PROFILE ir = new MASTER_FOR_RECORDING_INSIDER_PROFILE();
			ir.Show();
			Close();
		}

		private void rECORDINGOFSHARINGOFUPSIToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RECORDING_OF_SHARING_OF_UPSI rd = new RECORDING_OF_SHARING_OF_UPSI();
			rd.Show();
			Close();
		}

		private void lOGOUTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lg.CURRVALUE = "LOG OUT SUCCESSFULLY";
			lg.DESCRIPTION = "LOG OUT SUCCESSFULLY";
			lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
			lg.ID = SESSIONKEYS.UserID.ToString();
			string json = new MasterClass().SAVE_LOG(lg);
			Login l = new Login();
			SESSIONKEYS.UserID = null;
			l.Show();
			Close();
			timer1.Stop();
		}

		private const int CP_NOCLOSE_BUTTON = 0x200;

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams myCp = base.CreateParams;
				myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
				return myCp;
			}
		}

		private void lISTOFINSIDERToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LIST_OF_INSIDERS i = new LIST_OF_INSIDERS();
			i.Show();
			Close();
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			MASTER_DATA_OF_COMPANY c = new MASTER_DATA_OF_COMPANY();
			c.Show();
			Close();
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			HOMEPAGE h = new HOMEPAGE();
			h.Show();
			Close();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			SESSIONKEYS.datetimeog = SESSIONKEYS.datetimeog.AddSeconds(1);
			//label1.Text = DateTime.Now.ToString();
			label1.Text = SESSIONKEYS.datetimeog.ToString();
		}
	}
}
