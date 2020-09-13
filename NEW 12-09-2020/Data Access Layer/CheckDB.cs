using The_PIT_Archive.Data_Entity;
using RSACryptography;
using System;
using System.Data.SqlServerCe;
using System.Globalization;
using System.IO;

namespace The_PIT_Archive.Data_Access_Layer
{
    public class CheckDB
    {
        private string qry = "";
        public string CreateDbForFirstInstance()
        {
            try
            {
                string path = SESSIONKEYS.CONNECTPATH; //new MasterClass().GETDBTEXTLOG();
                string _C = "Data Source=" + path + ";Password=DB@-MAS#TER-@1C4-473$-%B7P-860#-961; Max Database Size=4091";
                SqlCeEngine en = new SqlCeEngine(_C);
                if (File.Exists(path))
                {
                    string newpath = path.Replace("The-PIT-Archive.sdf", "");
                    string destFile = System.IO.Path.Combine(newpath, "The-PIT-Archive-" + DateTime.Now.ToString("dd-MM-yyyy-hh-MM-tt") + CultureInfo.InvariantCulture + ".sdf");
                    System.IO.File.Move(path, destFile);
                    //File.Delete(path);
                }
                en.CreateDatabase();
                qry = "CREATE TABLE [M_LOG_DATABASE](	ID int IDENTITY(1,1) PRIMARY KEY NOT NULL,	DBPATH ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON datetime NULL, ACTIVE nchar(10) NULL);CREATE TABLE [M_LOG_AUDIT](	ID int IDENTITY(1,1) PRIMARY KEY NOT NULL,	NAME ntext NULL,	OPERATION ntext NULL,	DESCRIPTION ntext NULL,	TID ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON datetime NULL); CREATE TABLE [T_LOGIN] (ID int IDENTITY(1,1) PRIMARY KEY NOT NULL,	FULLNAME ntext NULL,	MOBILENO ntext NULL,	EMAIL nchar(100) NULL,	PASSWORD ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON datetime NULL,	DATEFROM datetime NULL,	DATETO datetime NULL,	ADMIN nchar(10) NULL,	ACTIVE nchar(10) NULL,	LOCK nchar(10) NULL, SENDEMAIL nchar(10) NULL ); CREATE TABLE [T_INS_COMPANY](ID int IDENTITY(1,1) PRIMARY KEY NOT NULL,	COMPANYNAME ntext NULL,	REGOFFICE ntext NULL,	CORPORATEOFFICE ntext NULL,	MOBILENO ntext NULL,	LANDLINENO ntext NULL,	EMAILID ntext NULL,	CIN ntext NULL,	BSECODE ntext NULL,	NSECODE ntext NULL,	ISIN ntext NULL,	OFFICERNAME ntext NULL,	DESIGNATION ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON ntext NULL,	MODIFIEDBY ntext NULL,	MODIFIEDON ntext NULL,	ACTIVE nchar(10) NULL,	LOCK nchar(10) NULL); CREATE TABLE [T_INS_COMPANY_LOG](	ID int IDENTITY(1, 1) PRIMARY KEY NOT NULL,	TID int NULL,	COMPANYNAME ntext NULL,	REGOFFICE ntext NULL,	CORPORATEOFFICE ntext NULL,	MOBILENO ntext NULL,	LANDLINENO ntext NULL,	EMAILID ntext NULL,	CIN ntext NULL,	BSECODE ntext NULL,	NSECODE ntext NULL,	ISIN ntext NULL,	OFFICERNAME ntext NULL,	DESIGNATION ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON datetime NULL,	OPERATION ntext NULL,	ACTIVE nchar(10) NULL,	LOCK nchar(10) NULL); CREATE TABLE [T_INS_PER](	ID int IDENTITY(1, 1) PRIMARY KEY NOT NULL,	CONNECTPERSONID ntext NULL,	EMPNAME ntext NULL,	CURRDESIGNATION ntext NULL,	ADDRESS ntext NULL,	RESIADDRESS ntext NULL,	PANNO ntext NULL,	OTHERIDENTIFIER ntext NULL,	DEMATACNO ntext NULL,	MOBILENO ntext NULL,  EMAILID ntext NULL,	GRADUATIONINSTI ntext NULL,	PASTEMP ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON ntext NULL,	MODIFIEDBY ntext NULL,	MODIFIEDON ntext NULL,	ACTIVE nchar(10) NULL,	LOCK nchar(10) NULL); CREATE TABLE [T_INS_PER_DT](ID int IDENTITY(1, 1) PRIMARY KEY NOT NULL,	PERID int NULL,	NAME ntext NULL,	ADDRESS ntext NULL,	RELATIONSHIP ntext NULL,	MOBILENO ntext NULL,	PANNO ntext NULL,	DEMATACNO ntext NULL,	EMAILID ntext NULL,	TYPE ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON ntext NULL,	MODIFIEDBY ntext NULL,	MODIFIEDON ntext NULL,	ACTIVE nchar(10) NULL,	LOCK nchar(10) NULL); CREATE TABLE [T_INS_PER_DT_LOG] (	ID int IDENTITY(1, 1) PRIMARY KEY NOT NULL,	TID int NULL,	PERID int NULL,	NAME ntext NULL,	ADDRESS ntext NULL,	RELATIONSHIP ntext NULL,	MOBILENO ntext NULL,	PANNO ntext NULL,	DEMATACNO ntext NULL,	EMAILID ntext NULL,	TYPE ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON datetime NULL,	OPERATION ntext NULL,	ACTIVE nchar(10) NULL,	LOCK nchar(10) NULL);CREATE TABLE [T_INS_PER_LOG] (ID int IDENTITY(1, 1) PRIMARY KEY NOT NULL,	TID int NULL,	CONNECTPERSONID ntext NULL,	EMPNAME ntext NULL,	CURRDESIGNATION ntext NULL,	ADDRESS ntext NULL,	RESIADDRESS ntext NULL,	PANNO ntext NULL,	OTHERIDENTIFIER ntext NULL,	DEMATACNO ntext NULL,	MOBILENO ntext NULL,  EMAILID ntext NULL,	GRADUATIONINSTI ntext NULL,	PASTEMP ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON datetime NULL,	OPERATION ntext NULL,	ACTIVE nchar(10) NULL,	LOCK nchar(10) NULL); CREATE TABLE [T_INS_PRO](	ID int IDENTITY(1, 1) PRIMARY KEY NOT NULL,	RECEPIENTID ntext NULL,	NAMEINSIDER ntext NULL,	CATEGORYRECEIPT ntext NULL,	ADDRESS ntext NULL,	PANNO ntext NULL,	OTHERIDENTIFIER ntext NULL,	AADHARNO ntext NULL,	MOBILENO ntext NULL,	LANDLINENO ntext NULL,	EMAILID ntext NULL,	DEMATACNO ntext NULL,	PANNOAFFILIATES ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON ntext NULL,	MODIFIEDBY ntext NULL,	MODIFIEDON ntext NULL,	ACTIVE nchar(10) NULL,	LOCK nchar(10) NULL); CREATE TABLE [T_INS_PRO_LOG] (	ID int IDENTITY(1, 1) PRIMARY KEY NOT NULL,	TID int NULL,	RECEPIENTID ntext NULL,	NAMEINSIDER ntext NULL,	CATEGORYRECEIPT ntext NULL,	ADDRESS ntext NULL,	PANNO ntext NULL,	OTHERIDENTIFIER ntext NULL,	AADHARNO ntext NULL,	MOBILENO ntext NULL,	LANDLINENO ntext NULL,	EMAILID ntext NULL,	DEMATACNO ntext NULL,	PANNOAFFILIATES ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON datetime NULL,	OPERATION ntext NULL,	ACTIVE nchar(10) NULL,	LOCK nchar(10) NULL); CREATE TABLE [T_INS_UPSI] (	ID int IDENTITY(1, 1) PRIMARY KEY NOT NULL,	UPSIID ntext NULL,  IDOFIP ntext NULL,	IDOFDP ntext NULL,	UPSINATURE ntext NULL,	SHARINGPURPOSE ntext NULL,	SHARINGDATE ntext NULL,	EFFECTIVEUPTO ntext NULL,	REMARKS ntext NULL,	NDASIGNED ntext NULL,	UPSIAVAILABLE ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON ntext NULL,	MODIFIEDBY ntext NULL,	MODIFIEDON ntext NULL,	ACTIVE nchar(10) NULL,	LOCK nchar(10) NULL); CREATE TABLE [T_INS_UPSI_LOG](	ID int IDENTITY(1, 1) PRIMARY KEY NOT NULL,	TID int NULL,	UPSIID ntext NULL,  IDOFIP ntext NULL,	IDOFDP ntext NULL,	UPSINATURE ntext NULL,	SHARINGPURPOSE ntext NULL,	SHARINGDATE ntext NULL,	EFFECTIVEUPTO ntext NULL,	REMARKS ntext NULL,	NDASIGNED ntext NULL,	UPSIAVAILABLE ntext NULL,	ENTEREDBY nchar(100) NULL,	ENTEREDON datetime NULL,	OPERATION ntext NULL,	ACTIVE nchar(10) NULL,	LOCK nchar(10) NULL); INSERT INTO T_LOGIN (FULLNAME,MOBILENO,EMAIL,PASSWORD,ENTEREDBY,ENTEREDON,ADMIN ,ACTIVE ,LOCK, SENDEMAIL) VALUES('" + CryptographyHelper.Encrypt("Admin") + "','" + CryptographyHelper.Encrypt("9619626541") + "','ADMIN','" + CryptographyHelper.Encrypt("PiTArc@$961@") + "',NULL,NULL,'Y','Y','N','0')  ";

                string[] a = qry.Split(';');

                for (int i = 0; i < a.Length; i++)
                {
                    new MasterClass().executeQueryForDB(a[i]);
                }
                return "EXISTS";
                //}
            }
            catch (Exception ex)
            {
                new MasterClass().SAVETEXTLOG(ex);
                throw ex;
            }
        }

        public string GetDBFilePath()
        {
            try
            {
                string path = new MasterClass().GETDBTEXTLOG();
                string rtnvalue = "";
                if (path != "")
                {
                    if (File.Exists(path))
                    {
                        string _C = "Data Source=" + path + ";Password=DB@-MAS#TER-@1C4-473$-%B7P-860#-961; Max Database Size=4091";
                        SESSIONKEYS.CONNECT = CryptographyHelper.Encrypt(_C);
                        SESSIONKEYS.CONNECTPATH = path;
                        rtnvalue = "EXISTS";
                    }
                    else
                    {
                        rtnvalue = "NOEXISTS";
                    }
                }
                else
                {
                    rtnvalue = "NOEXISTS";
                }
                return rtnvalue;
            }
            catch (Exception)
            {
                return "NOEXISTS";
                throw;
            }
        }
    }
}
