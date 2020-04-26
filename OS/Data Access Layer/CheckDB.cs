using System.Configuration;
using System.IO;

namespace OS.Data_Access_Layer
{
	public class CheckDB
	{
		//public SqlCeCommand cmd;
		//public SqlCeConnection con = new SqlCeConnection(ConfigurationManager.ConnectionStrings["CONNECT"].ToString());
		private readonly string _C = ConfigurationManager.ConnectionStrings["CONNECT"].ToString();
		public string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
		public string TXTPATH = Directory.GetCurrentDirectory() + "\\DBSCRIPTS";
		private readonly string qry = "";
		public string CreateDbForFirstInstance()
		{
			//SqlCeEngine en = new SqlCeEngine(_C);
			//if (File.Exists("OS.sdf"))
			//{
			//	File.Delete("OS.sdf");
			//	//return "EXISTS";
			//}
			////else
			////{
			//en.CreateDatabase();
			//DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory());//Assuming Test is your Folder
			//FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
			//string str = "";
			//foreach (FileInfo files in Files)
			//{
			//	qry = "";
			//	str = str + ", " + files.Name;
			//	using (StreamReader file = new StreamReader(files.Name))
			//	{
			//		int counter = 0;
			//		string ln;

			//		while ((ln = file.ReadLine()) != null)
			//		{
			//			qry += ln;
			//			counter++;
			//		}
			//		file.Close();
			//	}
			//	new MasterClass().executeQuery(qry);
			//}

			// Read file using StreamReader. Reads file line by line  

			return "EXISTS";
			//}
		}
	}
}
