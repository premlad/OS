using OS.Data_Access_Layer;
using OS.Data_Entity;
using System;
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

		}

		private void MASTERFORM_Load(object sender, EventArgs e)
		{
			//if (SESSIONKEYS.Role.ToString() == "Y")
			//{
			//	toolStripMenuItem1.Enabled = true;
			//}
			WindowState = FormWindowState.Maximized;
		}

		private void rECORDINGINSIDERCONNECTEDPERSONToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MASTER_FOR_RECORDING_INSIDER_CONNECTED_PERSON mr = new MASTER_FOR_RECORDING_INSIDER_CONNECTED_PERSON();
			mr.Show();
			Hide();
		}

		private void rECORDINGINSIDERPROFILEToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MASTER_FOR_RECORDING_INSIDER_PROFILE ir = new MASTER_FOR_RECORDING_INSIDER_PROFILE();
			ir.Show();
			Hide();
		}

		private void rECORDINGOFSHARINGOFUPSIToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RECORDING_OF_SHARING_OF_UPSI rd = new RECORDING_OF_SHARING_OF_UPSI();
			rd.Show();
			Hide();
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
			Hide();
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
			Hide();
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			MASTER_DATA_OF_COMPANY c = new MASTER_DATA_OF_COMPANY();
			c.Show();
			Hide();
		}
	}
}
