using OS.Data_Access_Layer;
using OS.Data_Entity;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace OS
{
	public partial class HOMEPAGE : MASTERFORM
	{
		public long valsize = 0;
		public long valFUllsize = 4187593114; //4294967296;//3.90GB
											  //public long valFUllsize = 1048576;//2.00MB
		private AUDITLOG lg = new AUDITLOG();
		private static readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };
		public HOMEPAGE()
		{
			InitializeComponent();
		}

		private void HOMEPAGE_Load(object sender, EventArgs e)
		{
			string path = FormatSize(GetFileSize(Directory.GetCurrentDirectory() + "\\OS.sdf"));
			LBLGETSIZE.Text = path.ToString() + " / 4.00 GB Used.";
			LLBNAME.Text = "Welcome " + SESSIONKEYS.FullName.ToString() + SESSIONKEYS.CompanyName.ToString();

			if (GetMatching())
			{
				if (SESSIONKEYS.Role.ToString().Trim() == "Y")
				{
					groupBox2.Visible = true;
				}
				else
				{
					groupBox2.Visible = false;
				}
			}
			else
			{
				DialogResult dialog = MessageBox.Show("The Following Database size Reached Upto 4.00 GB. Please Contact The Admin.", "HomePage", MessageBoxButtons.OK, MessageBoxIcon.Information);
				if (SESSIONKEYS.Role.ToString().Trim() != "Y")
				{
					Login l = new Login();
					//lg.CURRVALUE = "LOG OUT";
					//lg.DESCRIPTION = "LOG OUT SUCCESSFULLY WITH DB STORAGE FULL";
					//lg.TYPE = "SELECTED";
					//lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
					//lg.ID = SESSIONKEYS.UserID.ToString();
					string json = new MasterClass().SAVE_LOG(lg);
					SESSIONKEYS.UserID = "";
					SESSIONKEYS.Role = "";
					SESSIONKEYS.FullName = "";
					l.Show();
					Close();
				}
			}
		}

		public bool GetMatching()
		{
			if (valFUllsize >= valsize)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public string FormatSize(long bytes)
		{
			valsize = bytes;
			int counter = 0;
			decimal number = bytes;
			while (Math.Round(number / 1024) >= 1)
			{
				number = number / 1024;
				counter++;
			}
			return string.Format("{0:n1}{1} ", number, suffixes[counter]);
		}

		private long GetFileSize(string FilePath)
		{
			if (File.Exists(FilePath))
			{
				return new FileInfo(FilePath).Length;
			}
			return 0;
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

		private void btnaddINSCON_Click(object sender, EventArgs e)
		{
			MASTER_FOR_RECORDING_INSIDER_CONNECTED_PERSON md = new MASTER_FOR_RECORDING_INSIDER_CONNECTED_PERSON();
			md.Show();
			Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MASTER_FOR_RECORDING_INSIDER_PROFILE ir = new MASTER_FOR_RECORDING_INSIDER_PROFILE();
			ir.Show();
			Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			RECORDING_OF_SHARING_OF_UPSI up = new RECORDING_OF_SHARING_OF_UPSI();
			up.Show();
			Close();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			LIST_OF_CONNECTED_PERSON cp = new LIST_OF_CONNECTED_PERSON();
			cp.Show();
			Close();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			LIST_OF_INSIDERS i = new LIST_OF_INSIDERS();
			i.Show();
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			REPORTS_OF_SHARING_OF_UPSI up = new REPORTS_OF_SHARING_OF_UPSI();
			up.Show();
			Close();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			try
			{
				Login l = new Login();
				lg.CURRVALUE = "LOG OUT";
				lg.DESCRIPTION = "LOG OUT SUCCESSFULLY";
				lg.TYPE = "SELECTED";
				lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
				lg.ID = SESSIONKEYS.UserID.ToString();
				string json = new MasterClass().SAVE_LOG(lg);

				SESSIONKEYS.UserID = "";
				SESSIONKEYS.Role = "";
				SESSIONKEYS.FullName = "";
				l.Show();
				Close();
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("Something Went Wrong.", "Connected Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{
			MASTER_DATA_OF_COMPANY d = new MASTER_DATA_OF_COMPANY();
			d.Show();
			Close();
		}

		private void button8_Click(object sender, EventArgs e)
		{
			AUDIT_TRAIL a = new AUDIT_TRAIL();
			a.Show();
			Close();
		}

		private void button9_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Backup Data?", "Back Up Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					DialogResult ofd = folderBrowserDialog1.ShowDialog();

					if (ofd == DialogResult.OK)
					{
						lg.CURRVALUE = "BACK UP DATA";
						lg.TYPE = "SELECTED";
						lg.ID = SESSIONKEYS.UserID.ToString();
						lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
						lg.DESCRIPTION = "BACK UP DATA";
						new MasterClass().SAVE_LOG(lg);

						string backupPath = folderBrowserDialog1.SelectedPath;
						string fileName = "OS.sdf";
						string sourcePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

						string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
						string destFile = System.IO.Path.Combine(backupPath, fileName);

						System.IO.File.Copy(sourceFile, destFile, true);

						DialogResult dialog = MessageBox.Show("Data Backup Successfully.", "Back Up Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}

				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("Data Backup Failed.", "Back Up Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				string path = FormatSize(GetFileSize(Directory.GetCurrentDirectory() + "\\OS.sdf"));
				LBLGETSIZE.Text = path.ToString() + " / 4.00 GB Used.";
			}
		}

		private void button10_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Restore Data?\nPlease note that all your earlier data will be wiped out", "Restore Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					DialogResult ofd = folderBrowserDialog1.ShowDialog();

					if (ofd == DialogResult.OK)
					{
						lg.CURRVALUE = "RESTORE DATA";
						lg.TYPE = "SELECTED";
						lg.ID = SESSIONKEYS.UserID.ToString();
						lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
						lg.DESCRIPTION = "RESTORE DATA";
						new MasterClass().SAVE_LOG(lg);

						string backupPath = folderBrowserDialog1.SelectedPath;
						string fileName = "OS.sdf";
						string sourcePath = backupPath;
						string restorePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

						//if (File.Exists("OS.sdf"))
						//{
						//	System.IO.File.Move("OS.sdf", "OS" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + ".sdf");
						//}

						string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
						string destFile = System.IO.Path.Combine(restorePath, fileName);

						System.IO.File.Copy(sourceFile, destFile, true);

						lg.CURRVALUE = "RESTORE DATA";
						lg.TYPE = "SELECTED";
						lg.ID = SESSIONKEYS.UserID.ToString();
						lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
						lg.DESCRIPTION = "RESTORE DATA";
						new MasterClass().SAVE_LOG(lg);

						DialogResult dialog = MessageBox.Show("Data Restore Successfully.", "Restore Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
			catch (Exception ex)
			{
				new MasterClass().SAVETEXTLOG(ex);
				DialogResult dialog = MessageBox.Show("Data Restore Failed.", "Restore Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				string path = FormatSize(GetFileSize(Directory.GetCurrentDirectory() + "\\OS.sdf"));
				LBLGETSIZE.Text = path.ToString() + " / 4.00 GB Used.";
			}
		}

		private void button11_Click(object sender, EventArgs e)
		{
			CREATELOGIN l = new CREATELOGIN();
			l.Show();
			Close();
		}

		private void button12_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Lock the Database\nNote:After Locking you will not be able to Insert/Update/Delete Records.?\nSave the Database to appropraite Location.", "Lock Database", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				DialogResult ofd = folderBrowserDialog1.ShowDialog();

				if (ofd == DialogResult.OK)
				{
					string ds = new MasterClass().executeQueryForDB("UPDATE T_LOGIN SET LOCK = 'Y' ;").ToString();
					lg.CURRVALUE = "DATABASE LOCKED";
					lg.TYPE = "LOCK";
					lg.ID = ds;
					lg.DESCRIPTION = "DATABASE LOCKED";
					lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
					lg.ID = SESSIONKEYS.UserID.ToString();
					new MasterClass().SAVE_LOG(lg);

					string backupPath = folderBrowserDialog1.SelectedPath;
					string fileName = "OS.sdf";
					string sourcePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

					string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
					string destFile = System.IO.Path.Combine(backupPath, fileName);

					System.IO.File.Copy(sourceFile, destFile, true);
					lg.CURRVALUE = "BACK UP DATA";
					lg.TYPE = "SELECTED";
					lg.ID = SESSIONKEYS.UserID.ToString();
					lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
					lg.DESCRIPTION = "BACK UP DATA";
					new MasterClass().SAVE_LOG(lg);

					if (File.Exists("OS.sdf"))
					{
						File.Delete("OS.sdf");
					}

					DialogResult dialog = MessageBox.Show("Locked the Database Successfully.", "Lock Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
					Login l = new Login();
					l.Show();
					Close();

				}

			}
		}

		private void button13_Click(object sender, EventArgs e)
		{
			List_of_PAN_s_Registered_under_Database pn = new List_of_PAN_s_Registered_under_Database();
			pn.Show();
			Close();
		}
	}
}

