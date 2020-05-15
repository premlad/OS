using System;
using System.Collections;
using System.ComponentModel;

namespace OS
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
			base.Install(savedState);
			string licensekey = Context.Parameters["LicenseKey"];
			if (licensekey != "@9C5-395$-&B5A-281-528#-961")
			{
				throw new Exception("Invalid License Key");
			}
		}

		public override void Commit(IDictionary savedState)
		{

			base.Commit(savedState);
			string licensekey = Context.Parameters["LicenseKey"];
			if (licensekey != "@9C5-395$-&B5A-281-528#-961")
			{
				throw new Exception("Invalid License Key");
			}
		}

		public override void Rollback(IDictionary savedState)
		{

			base.Rollback(savedState);
			string licensekey = Context.Parameters["LicenseKey"];
			if (licensekey != "@9C5-395$-&B5A-281-528#-961")
			{
				throw new Exception("Invalid License Key");
			}
		}

		public override void Uninstall(IDictionary savedState)
		{

			base.Uninstall(savedState);
			string licensekey = Context.Parameters["LicenseKey"];
			if (licensekey != "@9C5-395$-&B5A-281-528#-961")
			{
				throw new Exception("Invalid License Key");
			}
		}
	}
}
