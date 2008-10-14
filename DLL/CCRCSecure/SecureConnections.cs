using System;
using System.Configuration;
using System.Text;
using Microsoft.Win32;
namespace CCRCEncryption 
{
	/// <summary>
	/// Provides method to retrieve connection string from web.config
	/// and decyrpt the string using DataProtector class. 
	/// </summary>
	public class SecureConnection
	{


        static public string GetConnectionStringFromRegistry()
        {

            RegistryKey myCo;
            string rootKey;
            string conStr="";
            rootKey = "SOFTWARE\\MYCompany";
            myCo = Registry.LocalMachine.OpenSubKey(rootKey);
            if (myCo != null)
            {
                conStr = (string)myCo.GetValue("DOTNET_CONNECTION_STRING", "") ;
                myCo.Close();
            }
            return conStr;
        }
        
        
        
        static public string GetCnxString(string configKey)
		{
            //string strCnx;
            //    // Grab encrypted connection string from web.config
            //    string strEncryptedCnx = ConfigurationSettings.AppSettings[configKey];
            
            //    // Decrypt the connection string
            //    DataProtector dp = new DataProtector(DataProtector.Store.USE_MACHINE_STORE);
            //    byte[] dataToDecrypt = Convert.FromBase64String(strEncryptedCnx);
            //    // Optional entropy parameter is null.
            //    // If entropy was used within the Encrypt method, the same entropy parameter
            //    // must be supplied here
            //    strCnx = Encoding.UTF8.GetString(dp.Decrypt(dataToDecrypt,null));

            //return strCnx;
            return "Dummy Secure Connections -  GetCnxString";
		}
	}
}
