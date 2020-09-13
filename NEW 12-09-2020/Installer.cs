using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Windows.Forms;

namespace The_PIT_Archive
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary savedState)
        {
            try
            {

                base.Install(savedState);
                string licensekey = Context.Parameters["LicenseKey"];
                if (licensekey != "@395$-528#-&56B-@2SM-835$-&961-@012")
                {
                    DialogResult dialog = MessageBox.Show("Invalid License Key", "The PIT Archive", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //throw new Exception("Invalid License Key");
                }
            }
            catch (Exception)
            {
                DialogResult dialog = MessageBox.Show("Something Went Wrong While Installation. Problem with Windows Version", "The PIT Archive", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
