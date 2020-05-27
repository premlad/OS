using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace OS
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Process[] processName = Process.GetProcessesByName("The PIT Archive");
			if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
			{
				DialogResult dialog = MessageBox.Show("The PIT Archive is running already.", "The PIT Archive", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				Application.Run(new Login());
			}
		}
	}
}
