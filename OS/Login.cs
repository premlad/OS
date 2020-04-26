using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OS
{
	public partial class Login : Form
	{
		private AUDITLOG lg = new AUDITLOG();
		public Login()
		{
			InitializeComponent();
		}

		private void btnlogin_Click(object sender, EventArgs e)
		{
			try
			{
				if (new CheckDB().CreateDbForFirstInstance() == "EXISTS")
				{
					LOGIN a = new LOGIN
					{
						EMAIL = txtusername.Text,
						PASSWORD = txtpassword.Text,
						ACTIVE = "Y"
					};

					Hashtable hstmst = new Hashtable
					{
						{ "@EMAIL", a.EMAIL },
						{ "@PASSWORD", a.PASSWORD },
						{ "@ACTIVE", a.ACTIVE },
						{ "@ACTION", "1" }
					};
					DataSet ds = new MasterClass().executeDatable_SP("STP_LOGIN", hstmst);
					if (ds.Tables[0].Rows.Count > 0)
					{
						if (a.PASSWORD == CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PASSWORD"].ToString()))
						{
							SESSIONKEYS.UserID = ds.Tables[0].Rows[0]["ID"].ToString();
							SESSIONKEYS.Role = ds.Tables[0].Rows[0]["ADMIN"].ToString();
							SESSIONKEYS.FullName = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
							lg.CURRVALUE = "LOG IN SUCCESSFULLY";
							lg.DESCRIPTION = "LOG IN SUCCESSFULLY";
							lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
							lg.ID = SESSIONKEYS.UserID.ToString();
							string json = new MasterClass().SAVE_LOG(lg);
							DialogResult dialog = MessageBox.Show("Login Successfully.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
							HOMEPAGE h = new HOMEPAGE();
							h.Show();
							Hide();
						}
						else
						{
							DialogResult dialog = MessageBox.Show("Invalid Username or Password.\nEnter Correct Username or Password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}

					}
					else
					{
						DialogResult dialog = MessageBox.Show("Invalid Username or Password.\nEnter Correct Username or Password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			catch (Exception en)
			{
				DialogResult dialog = MessageBox.Show(en.Message, "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Login_Load(object sender, EventArgs e)
		{

		}
		//private const int CP_NOCLOSE_BUTTON = 0x200;
		//protected override CreateParams CreateParams
		//{
		//	get
		//	{
		//		CreateParams myCp = base.CreateParams;
		//		myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
		//		return myCp;
		//	}
		//}
	}
}
