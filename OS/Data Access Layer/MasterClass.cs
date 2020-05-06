using iTextSharp.text;
using iTextSharp.text.pdf;
using OS.Data_Entity;
using RSACryptography;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OS.Data_Access_Layer
{
	internal class MasterClass
	{
		public SqlCeConnection con = new SqlCeConnection(ConfigurationManager.ConnectionStrings["CONNECT"].ToString());
		//Data Source=(localdb)\MSSqlCeLocalDB;AttachDbFilename=C:\Users\MAULI\source\repos\OS\OS\OS.mdf;Initial Catalog=OS;Integrated Security=True
		//public SqlCeConnection con = new SqlCeConnection(@"Data Source=(localdb)\MSSqlCeLocalDB;AttachDbFilename=|DataDirectory|\OS.mdf;Integrated Security=True;Connect Timeout=10000;User Instance=True");
		public SqlCeTransaction tran;
		public DataTable dt = new DataTable();
		public DataSet ds = new DataSet();
		private static readonly TimeZoneInfo India_Standard_Time = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

		public MasterClass()
		{

		}

		public string SAVE_LOG(AUDITLOG obj)
		{

			return new MasterClass().executeQuery("INSERT INTO M_LOG_AUDIT(NAME,OPERATION,DESCRIPTION,TID,ENTEREDBY,ENTEREDON) VALUES ('" + CryptographyHelper.Encrypt(obj.CURRVALUE) + "', '" + CryptographyHelper.Encrypt(obj.TYPE) + "', '" + CryptographyHelper.Encrypt(obj.DESCRIPTION) + "', '" + obj.ID + "', '" + obj.ENTEREDBY + "', '" + GETIST() + "'); ").ToString();
		}

		public string GETLOCKDB()
		{
			DataSet ds = new MasterClass().getDataSet("SELECT LOCK FROM T_LOGIN WHERE ACTIVE = 'Y'");
			List<string> termsList = new List<string>();
			if (ds.Tables[0].Rows.Count > 0)
			{
				string a = ds.Tables[0].Rows[0]["LOCK"].ToString();
				if (a.Trim() == "Y")
				{
					return "Y";
				}
				else
				{
					return "N";
				}
			}
			else
			{
				return "N";
			}
		}

		public string GETCPID()
		{
			DataSet ds = new MasterClass().getDataSet("select CONNECTPERSONID from T_INS_PER WHERE ACTIVE = 'Y'");
			List<string> termsList = new List<string>();
			if (ds.Tables[0].Rows.Count > 0)
			{
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					string a = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["CONNECTPERSONID"].ToString());
					termsList.Add(a.Substring(2));
				}
				string[] b = termsList.ToArray();
				int val = Convert.ToInt32(b.Max()) + Convert.ToInt32(1);
				return val.ToString();
			}
			else
			{
				return "1";
			}
		}

		public string GETIPID()
		{
			DataSet ds = new MasterClass().getDataSet("select RECEPIENTID from T_INS_PRO WHERE ACTIVE = 'Y'");
			List<string> termsList = new List<string>();
			if (ds.Tables[0].Rows.Count > 0)
			{
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					string a = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["RECEPIENTID"].ToString());
					termsList.Add(a.Substring(2));
				}
				string[] b = termsList.ToArray();
				int val = Convert.ToInt32(b.Max()) + Convert.ToInt32(1);
				return val.ToString();
			}
			else
			{
				return "1";
			}
		}

		public string GETUPSIID()
		{
			DataSet ds = new MasterClass().getDataSet("select UPSIID from T_INS_UPSI WHERE ACTIVE = 'Y'");
			List<string> termsList = new List<string>();
			if (ds.Tables[0].Rows.Count > 0)
			{
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					string a = CryptographyHelper.Decrypt(ds.Tables[0].Rows[i]["UPSIID"].ToString());
					termsList.Add(a.Substring(4));
				}
				string[] b = termsList.ToArray();
				int val = Convert.ToInt32(b.Max()) + Convert.ToInt32(1);
				return val.ToString();
			}
			else
			{
				return "1";
			}
		}

		public string Encrypt(string toEncrypt, bool useHashing)
		{
			byte[] keyArray;
			byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

			System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
			// Get the key from config file
			string key = (string)settingsReader.GetValue("SecurityKey", typeof(string));
			//System.Windows.Forms.MessageBox.Show(key);
			if (useHashing)
			{
				MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
				keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
				hashmd5.Clear();
			}
			else
			{
				keyArray = UTF8Encoding.UTF8.GetBytes(key);
			}

			TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
			{
				Key = keyArray,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			};

			ICryptoTransform cTransform = tdes.CreateEncryptor();
			byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
			tdes.Clear();
			return Convert.ToBase64String(resultArray, 0, resultArray.Length);
		}

		public string Decrypt(string cipherString, bool useHashing)
		{
			byte[] keyArray;
			byte[] toEncryptArray = Convert.FromBase64String(cipherString);

			System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
			//Get your key from config file to open the lock!
			string key = (string)settingsReader.GetValue("SecurityKey", typeof(string));

			if (useHashing)
			{
				MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
				keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
				hashmd5.Clear();
			}
			else
			{
				keyArray = UTF8Encoding.UTF8.GetBytes(key);
			}

			TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
			{
				Key = keyArray,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			};

			ICryptoTransform cTransform = tdes.CreateDecryptor();
			byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

			tdes.Clear();
			return UTF8Encoding.UTF8.GetString(resultArray);
		}

		public static DateTime GETIST()
		{
			DateTime dateTime_Indian = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, India_Standard_Time);
			return dateTime_Indian;
		}

		public static string GETISTI()
		{
			DateTime dateTime = DateTime.MinValue;
			System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://www.microsoft.com");
			request.Method = "GET";
			request.Accept = "text/html, application/xhtml+xml, */*";
			request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
			request.ContentType = "application/x-www-form-urlencoded";
			request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
			request.Timeout = 1000000000;
			System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				string todaysDates = response.Headers["date"];

				dateTime = DateTime.ParseExact(todaysDates, "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
					System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat, System.Globalization.DateTimeStyles.AssumeUniversal);
			}

			DateTime currentDateTime = DateTime.Now;
			DateTime dt = dateTime.AddMinutes(-dateTime.Minute).AddSeconds(-dateTime.Second);
			DateTime dtt = currentDateTime.AddMinutes(-currentDateTime.Minute).AddSeconds(-currentDateTime.Second);
			if (dt.ToString("dd-MM-yyyy hh") != dtt.ToString("dd-MM-yyyy hh"))
			{
				return "TEMP";
			}
			else
			{
				return "ALLOW";
			}
			//DateTime dateTime_Indian = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, India_Standard_Time);
			//return dateTime_Indian;
		}

		public static string GETISTII()
		{
			DateTime dateTime = DateTime.MinValue;
			System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://www.microsoft.com");
			request.Method = "GET";
			request.Accept = "text/html, application/xhtml+xml, */*";
			request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
			request.ContentType = "application/x-www-form-urlencoded";
			request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
			request.Timeout = 1000000000;
			System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				string todaysDates = response.Headers["date"];

				dateTime = DateTime.ParseExact(todaysDates, "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
					System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat, System.Globalization.DateTimeStyles.AssumeUniversal);
			}

			DateTime currentDateTime = DateTime.Now;
			DateTime dt = dateTime.AddMinutes(-dateTime.Minute).AddSeconds(-dateTime.Second);
			DateTime dtt = currentDateTime.AddMinutes(-currentDateTime.Minute).AddSeconds(-currentDateTime.Second);
			if (dt.ToString("dd-MM-yyyy hh") != dtt.ToString("dd-MM-yyyy hh"))
			{
				return "TEMP";
			}
			else
			{
				return "ALLOW";
			}
			//DateTime dateTime_Indian = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, India_Standard_Time);
			//return dateTime_Indian;
		}

		public DataSet executeDatable_SP(string sp_name, System.Collections.Hashtable hash)
		{
			try
			{
				using (SqlCeCommand cmd = new SqlCeCommand(sp_name))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Connection = con;
					con.Open();
					tran = con.BeginTransaction(IsolationLevel.ReadCommitted);
					cmd.Transaction = tran;
					System.Collections.IDictionaryEnumerator en = hash.GetEnumerator();

					while (en.MoveNext())
					{
						SqlCeParameter p = cmd.CreateParameter();
						p.ParameterName = en.Key.ToString();
						p.Value = en.Value;
						cmd.Parameters.Add(p);
					}
					SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
					cmd.ExecuteNonQuery();
					da.Fill(ds);
					tran.Commit();
					con.Close();
					return ds;
				}
			}
			catch (Exception ex)
			{
				tran.Rollback();
				con.Close();
				throw ex;
			}
		}

		public string executeScalar_SP(string sp_name, System.Collections.Hashtable hash)
		{
			try
			{
				using (SqlCeCommand cmd = new SqlCeCommand(sp_name))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Connection = con;
					con.Open();
					tran = con.BeginTransaction(IsolationLevel.ReadCommitted);
					cmd.Transaction = tran;
					System.Collections.IDictionaryEnumerator en = hash.GetEnumerator();

					while (en.MoveNext())
					{
						SqlCeParameter p = cmd.CreateParameter();
						p.ParameterName = en.Key.ToString();
						p.Value = en.Value;
						cmd.Parameters.Add(p);
					}
					string obj = cmd.ExecuteScalar().ToString();
					tran.Commit();
					con.Close();
					return obj;
				}
			}
			catch (Exception ex)
			{
				tran.Rollback();
				con.Close();
				throw ex;
			}
		}

		public DataTable getDataTable(string query)
		{
			try
			{
				con.Open();
				SqlCeDataAdapter da = new SqlCeDataAdapter(query, con);
				DataTable dt = new DataTable();
				da.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{
				con.Close();
				throw ex;
			}
			finally
			{
				con.Close();
			}

		}

		public DataSet getDataSet(string query)
		{
			try
			{
				con.Open();
				SqlCeDataAdapter da = new SqlCeDataAdapter(query, con);
				DataSet dt = new DataSet();
				da.Fill(dt);
				return dt;
			}
			catch (Exception ex)
			{
				con.Close();
				throw ex;
			}
			finally
			{
				con.Close();
			}
		}

		public string executeQuery(string query)
		{
			try
			{
				con.Open();
				SqlCeCommand cmd = new SqlCeCommand(query, con);
				int i = cmd.ExecuteNonQuery();
				cmd.CommandText = "SELECT @@IDENTITY";
				object id = cmd.ExecuteScalar();
				con.Close();
				return id.ToString();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public int executeQueryForDB(string query)
		{
			try
			{
				con.Open();
				SqlCeCommand cmd = new SqlCeCommand(query, con);
				int i = cmd.ExecuteNonQuery();
				//int i = (int)cmd.ExecuteScalar();
				con.Close();
				return i;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public string ToCsV(DataGridView dGV, string filename)
		{

			string stOutput = "";

			// Export titles:

			string sHeaders = "";

			for (int j = 0; j < dGV.Columns.Count; j++)
			{
				sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
			}

			stOutput += sHeaders + "\r\n";

			// Export data.

			for (int i = 0; i < dGV.RowCount; i++)
			{

				string stLine = "";

				for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
				{
					stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
				}

				stOutput += stLine + "\r\n";

			}

			Encoding utf16 = Encoding.GetEncoding(1254);

			byte[] output = utf16.GetBytes(stOutput);

			FileStream fs = new FileStream(filename, FileMode.Create);

			BinaryWriter bw = new BinaryWriter(fs);

			bw.Write(output, 0, output.Length); //write the encoded file

			bw.Flush();

			bw.Close();

			fs.Close();
			return "";

		}

		public void ToPDF(DataGridView dataGridView1, string filename, bool fileError = false)
		{
			if (!fileError)
			{
				try
				{
					PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
					pdfTable.DefaultCell.Padding = 3;
					pdfTable.WidthPercentage = 100;
					pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

					foreach (DataGridViewColumn column in dataGridView1.Columns)
					{
						PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
						pdfTable.AddCell(cell);
					}

					foreach (DataGridViewRow row in dataGridView1.Rows)
					{
						foreach (DataGridViewCell cell in row.Cells)
						{
							pdfTable.AddCell(cell.Value.ToString());
						}
					}

					using (FileStream stream = new FileStream(filename, FileMode.Create))
					{
						Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 20f, 20f, 10f);
						PdfWriter.GetInstance(pdfDoc, stream);
						pdfDoc.Open();
						pdfDoc.Add(pdfTable);
						pdfDoc.Close();
						stream.Close();
					}
				}
				catch (Exception)
				{
					throw;
				}
			}
		}

		public bool IsValidEmail(string email)
		{
			Regex rx = new Regex(
			@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
			return rx.IsMatch(email);
		}

		public bool IsValidPanno(string panno)
		{
			if (panno.Length == 10)
			{
				Regex rx = new Regex(@"[A-Z]{5}\d{4}[A-Z]{1}");
				return rx.IsMatch(panno);
			}
			else
			{
				return false;
			}
		}

		public bool IsValidDematAcno(string DematAcno)
		{
			if (DematAcno.Length == 16)
			{
				Regex rx = new Regex(@"[A-Z]{2}\d{14}");
				return rx.IsMatch(DematAcno);
			}
			else
			{
				return false;
			}

		}

		public static string getdate(string date)
		{
			string[] dt = date.Split('/');
			if (dt.Length == 3)
			{
				return dt[1] + "/" + dt[0] + "/" + dt[2];
			}
			else
			{
				return "";
			}
		}

		public static string[] getTime(string inputtime)
		{

			try
			{

				string hh, mm, ss, ampm;
				string[] _time1 = inputtime.Split(' ');
				string[] _time2 = _time1[0].Split(':');
				ampm = _time1[1];
				if (_time2.Length == 3)
				{
					hh = _time2[0];
					mm = _time2[1];
					ss = _time2[2];
				}
				else
				{
					hh = _time2[0];
					mm = _time2[1];
					ss = "00";
				}
				string[] outputtime = { hh, mm, ss, ampm };
				return outputtime;
			}
			catch
			{
				string[] outputtime = null;
				return outputtime;
			}
		}

		//Send Email To Client...!!!
		public static bool SendEmail(string mailbody, string email, string subject)
		{
			string to = email; //To address    
			string from = "premlad961@gmail.com"; //From address    
			MailMessage message = new MailMessage(from, to)
			{
				Subject = subject,
				Body = mailbody,
				BodyEncoding = Encoding.UTF8,
				IsBodyHtml = true
			};
			SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
			System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("premlad961@gmail.com", "Premlad961@#");
			//System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("dreal.software.solutions@gmail.com", "DReal@@123");
			client.EnableSsl = true;
			client.UseDefaultCredentials = true;
			client.Credentials = basicCredential1;
			try
			{
				client.Send(message);
				return true;
			}

			catch (Exception ex)
			{
				throw ex;
			}


		}

		//Get GEo LOcation of the Client in Latitude and Longitude
		public static string GetGeoLocation()
		{
			try
			{
				GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

				// Do not suppress prompt, and wait 1000 milliseconds to start.
				watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));

				GeoCoordinate coord = watcher.Position.Location;

				if (coord.IsUnknown != true)
				{
					return "Lat:" + coord.Latitude + "|Long:" + coord.Longitude;
					//Console.WriteLine("Lat: {0}, Long: {1}",
					//	coord.Latitude,
					//	coord.Longitude);
				}
				else
				{
					return "Unknown latitude and longitude.";
					//Console.WriteLine("Unknown latitude and longitude.");
				}
				return "Unknown latitude and longitude.";
			}
			catch (Exception e)
			{
				return "Unknown latitude and longitude with Exception:" + e.ToString();
			}
		}

		//Get IP Address of the Client
		public static string GetIPAdd()
		{
			try
			{


				string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
				Console.WriteLine(hostName);
				// Get the IP  
				string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
				return myIP;
				//Console.WriteLine("My IP Address is :" + myIP);
				//Console.ReadKey();
			}
			catch (Exception e)
			{
				return "Unknown IP with Exception:" + e.ToString();
			}
		}

		//Encode Value to Base64
		public static string Base64Encode(string plainText)
		{
			byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}

		//Decode Value to Base64
		public static string Base64Decode(string base64EncodedData)
		{
			byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}

		//Send Email To Client...!!!
		public static bool SendEmailOfException(string mailbody, List<string> email, string subject)
		{
			List<string> to = email; //To address   
			string From = "premlad961@gmail.com";
			MailMessage message = new MailMessage();
			foreach (string mutiemail in to)
			{
				message.To.Add(new MailAddress(mutiemail));
			}
			message.From = new MailAddress(From);
			message.Subject = subject;
			message.Body = mailbody;
			message.BodyEncoding = Encoding.UTF8;
			message.IsBodyHtml = true;
			SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
			System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("premlad961@gmail.com", "Premlad961@#");
			//System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("dreal.software.solutions@gmail.com", "DReal@@123");
			client.EnableSsl = true;
			client.UseDefaultCredentials = true;
			client.Credentials = basicCredential1;
			try
			{
				client.Send(message);
				return true;
			}

			catch (Exception ex)
			{
				throw ex;
			}


		}
	}
}
