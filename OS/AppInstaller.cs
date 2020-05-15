using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OS
{
	[RunInstaller(true)]
	public partial class AppInstaller : System.Configuration.Install.Installer
	{
		public AppInstaller()
		{
			InitializeComponent();
		}

		public override void Install(IDictionary stateSaver)
		{
			base.Install(stateSaver);
			string licensekey = Context.Parameters["LicenseKey"];
			if (licensekey != "PIT@321")
			{
				MessageBox.Show("Can't Install File, The License Key is Invalid!");
				base.Rollback(savedState);
				//throw new System.Exception("Can't Install File, The License Key is Invalid!");
			}
		}

		public override void Rollback(IDictionary savedState)
		{
			base.Rollback(savedState);
		}

		public override void Uninstall(IDictionary savedState)
		{
			base.Uninstall(savedState);
		}
	}
}
