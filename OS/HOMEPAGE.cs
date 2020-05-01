using OS.Data_Access_Layer;
using OS.Data_Entity;
using RSACryptography;
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
		private AUDITLOG lg = new AUDITLOG();

		public HOMEPAGE()
		{
			InitializeComponent();
		}

		private void HOMEPAGE_Load(object sender, EventArgs e)
		{
			LLBNAME.Text = "Welcome " + CryptographyHelper.Decrypt(SESSIONKEYS.FullName.ToString());
			if (SESSIONKEYS.Role.ToString().Trim() == "Y")
			{
				groupBox2.Visible = true;
			}
			else
			{
				groupBox2.Visible = false;
			}
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
			Hide();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MASTER_FOR_RECORDING_INSIDER_PROFILE ir = new MASTER_FOR_RECORDING_INSIDER_PROFILE();
			ir.Show();
			Hide();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			RECORDING_OF_SHARING_OF_UPSI up = new RECORDING_OF_SHARING_OF_UPSI();
			up.Show();
			Hide();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			LIST_OF_CONNECTED_PERSON cp = new LIST_OF_CONNECTED_PERSON();
			cp.Show();
			Hide();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			LIST_OF_INSIDERS i = new LIST_OF_INSIDERS();
			i.Show();
			Hide();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			REPORTS_OF_SHARING_OF_UPSI up = new REPORTS_OF_SHARING_OF_UPSI();
			up.Show();
			Hide();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			Login l = new Login();
			SESSIONKEYS.UserID = "";
			SESSIONKEYS.Role = "";
			SESSIONKEYS.FullName = "";
			lg.CURRVALUE = "LOG OUT";
			lg.DESCRIPTION = "LOG OUT SUCCESSFULLY";
			lg.TYPE = "SELECTED";
			lg.ENTEREDBY = SESSIONKEYS.UserID.ToString();
			lg.ID = SESSIONKEYS.UserID.ToString();
			string json = new MasterClass().SAVE_LOG(lg);
			l.Show();
			Hide();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			MASTER_DATA_OF_COMPANY d = new MASTER_DATA_OF_COMPANY();
			d.Show();
			Hide();
		}

		private void button8_Click(object sender, EventArgs e)
		{
			AUDIT_TRAIL a = new AUDIT_TRAIL();
			a.Show();
			Hide();
		}

		private void button9_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Backup Data?", "Back Up Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				DialogResult ofd = folderBrowserDialog1.ShowDialog();

				if (ofd == DialogResult.OK)
				{
					string backupPath = folderBrowserDialog1.SelectedPath;
					string fileName = "OS.sdf";
					string sourcePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

					string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
					string destFile = System.IO.Path.Combine(backupPath, fileName);

					System.IO.File.Copy(sourceFile, destFile, true);
					lg.CURRVALUE = "BACK UP DATA";
					lg.TYPE = "SELECTED";
					lg.ID = SESSIONKEYS.UserID.ToString();
					lg.DESCRIPTION = "BACK UP DATA";
					new MasterClass().SAVE_LOG(lg);
					DialogResult dialog = MessageBox.Show("Data Backup Successfully.", "Back Up Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

			}
		}

		private void button10_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are You Sure You Want to Restore Data?\nNote:-Please Back Up the Existing Data", "Restore Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				DialogResult ofd = folderBrowserDialog1.ShowDialog();

				if (ofd == DialogResult.OK)
				{
					string backupPath = folderBrowserDialog1.SelectedPath;
					string fileName = "OS.sdf";
					string sourcePath = backupPath;
					string restorePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

					string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
					string destFile = System.IO.Path.Combine(restorePath, fileName);

					System.IO.File.Copy(sourceFile, destFile, true);
					lg.CURRVALUE = "RESTORE DATA";
					lg.TYPE = "SELECTED";
					lg.ID = SESSIONKEYS.UserID.ToString();
					lg.DESCRIPTION = "RESTORE DATA";
					new MasterClass().SAVE_LOG(lg);
					DialogResult dialog = MessageBox.Show("Data Restore Successfully.", "Restore Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}

		}
	}
}

