using RSACryptography;
using System;
using System.Windows.Forms;

namespace OS
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			textBox2.Text = CryptographyHelper.Encrypt(textBox1.Text);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			textBox3.Text = CryptographyHelper.Decrypt(textBox2.Text);
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}
