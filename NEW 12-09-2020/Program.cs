using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using The_PIT_Archive.Forms;

namespace The_PIT_Archive
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Process[] processName = Process.GetProcessesByName("The PIT Archive");
                if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
                {
                    DialogResult dialog = MessageBox.Show("The PIT Archive is running already. Do you want to Close the Previous Window", "The PIT Archive", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Application.Run(new Login());
                }
            }
            catch (Exception)
            {
                DialogResult dialog = MessageBox.Show("Something Went Wrong. Please Check The Connection", "The PIT Archive", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
