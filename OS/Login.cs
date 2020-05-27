using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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

		private void SetLoading(bool displayLoader)
		{
			try
			{
				if (displayLoader)
				{
					if (Cursors.WaitCursor != Cursor)
					{
						//Invoke((MethodInvoker)delegate
						//{
						//picLoader.Visible = true;
						Cursor = Cursors.WaitCursor;
						//Thread.Sleep(4000);
						//});
					}
				}
				else
				{
					if (Cursors.Default != Cursor)
					{
						//Invoke((MethodInvoker)delegate
						//{
						//picLoader.Visible = false;
						Cursor = Cursors.Default;
						//});
					}

				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				//Login l = new Login();
				//l.Show();
				//Close();
			}
		}

		private void btnlogin_Click(object sender, EventArgs e)
		{
			try
			{
				SetLoading(true);

				//Thread.Sleep(2000);
				//Invoke((MethodInvoker)delegate
				//{
				if (txtusername.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Provide Values.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (txtpassword.Text == "")
				{
					DialogResult dialog = MessageBox.Show("Provide Values.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (MasterClass.GETISTII() == "TEMP")
				{
					DialogResult dialog = MessageBox.Show("Date & Time is Tempered.\nPlease Check your Date & Time Settings.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (new CheckDB().CreateDbForFirstInstance() == "EXISTS")
				{
					DataSet ds;
					if (txtusername.Text.ToUpper().Trim() == "COMPLIANCE_OFFICER")
					{
						//ds = new MasterClass().getDataSet("SELECT ID,PASSWORD,FULLNAME,ADMIN,DATEFROM,DATETO FROM T_LOGIN WHERE EMAIL = '" + txtusername.Text + "' AND ACTIVE = 'Y' AND CONVERT(DATETIME,DATEFROM) >= CONVERT(DATETIME,'" + SESSIONKEYS.datetimeog.ToString("yyyy-MM-dd") + "') AND CONVERT(DATETIME,DATETO) <= CONVERT(DATETIME,'" + SESSIONKEYS.datetimeog.ToString("yyyy-MM-dd") + "')");
						ds = new MasterClass().getDataSet("SELECT ID,PASSWORD,FULLNAME,ADMIN,DATEFROM,DATETO,SENDEMAIL FROM T_LOGIN WHERE EMAIL = '" + txtusername.Text + "' AND ACTIVE = 'Y' AND CONVERT(DATETIME,'" + SESSIONKEYS.datetimeog.ToString("yyyy-MM-dd") + "') BETWEEN CONVERT(DATETIME,DATEFROM) AND CONVERT(DATETIME,DATETO) ");
					}
					else
					{
						ds = new MasterClass().getDataSet("SELECT ID,PASSWORD,FULLNAME,ADMIN,DATEFROM,DATETO,SENDEMAIL FROM T_LOGIN WHERE EMAIL = '" + txtusername.Text + "' AND ACTIVE = 'Y'");
					}

					DataSet dss = new MasterClass().getDataSet("SELECT COMPANYNAME FROM T_INS_COMPANY WHERE ACTIVE = 'Y'");
					if (ds.Tables[0].Rows.Count > 0)
					{
						if (txtpassword.Text == CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["PASSWORD"].ToString()))
						{
							SESSIONKEYS.UserID = ds.Tables[0].Rows[0]["ID"].ToString();
							SESSIONKEYS.Role = ds.Tables[0].Rows[0]["ADMIN"].ToString();
							SESSIONKEYS.counteremail = Convert.ToInt32(ds.Tables[0].Rows[0]["SENDEMAIL"]);
							if (dss.Tables[0].Rows.Count > 0)
							{
								SESSIONKEYS.FullName = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["FULLNAME"].ToString());
								SESSIONKEYS.CompanyName = " of " + CryptographyHelper.Decrypt(dss.Tables[0].Rows[0]["COMPANYNAME"].ToString()) + ".";
							}
							else
							{
								SESSIONKEYS.FullName = CryptographyHelper.Decrypt(ds.Tables[0].Rows[0]["FULLNAME"].ToString());
								SESSIONKEYS.CompanyName = " of <<Company Name>>.";
							}
							lg.CURRVALUE = "LOG IN";
							lg.DESCRIPTION = "LOG IN SUCCESSFULLY";
							lg.TYPE = "SELECTED";
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
						DialogResult dialog = MessageBox.Show("Invalid Username or Password.\nEnter Correct Username or Password.\nPlease Contact System Administrator for More Details", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				//});
				SetLoading(false);
			}
			catch (Exception ex)
			{
				SetLoading(false);
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("Please Check Your Internet Connection.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Login_Load(object sender, EventArgs e)
		{
			//PrivateFontCollection pfc = new PrivateFontCollection();
			//pfc.AddFontFile(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "/Resources/" + "FontsFree-Net-SFProDisplay-Regular.ttf");
			////label2.Font = new Font(pfc.Families[0], 12, FontStyle.Regular);
			//List<Control> allControls = GetAllControls(this);
			//allControls.ForEach(k => k.Font = new Font(pfc.Families[0], 12, FontStyle.Regular));
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

		private class RoundedButton : Button
		{
			private GraphicsPath GetRoundPath(RectangleF Rect, int radius)
			{
				float r2 = radius / 2f;
				GraphicsPath GraphPath = new GraphicsPath();
				GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
				GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
				GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
				GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
				GraphPath.AddArc(Rect.X + Rect.Width - radius,
								 Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
				GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
				GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
				GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
				GraphPath.CloseFigure();
				return GraphPath;
			}

			protected override void OnPaint(PaintEventArgs e)
			{
				base.OnPaint(e);
				RectangleF Rect = new RectangleF(0, 0, Width, Height);
				using (GraphicsPath GraphPath = GetRoundPath(Rect, 50))
				{
					Region = new Region(GraphPath);
					using (Pen pen = new Pen(Color.CadetBlue, 1.75f))
					{
						pen.Alignment = PenAlignment.Inset;
						e.Graphics.DrawPath(pen, GraphPath);
					}
				}
			}
		}
	}
}
