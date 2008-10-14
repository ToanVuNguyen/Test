using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using CCRCEncryption;
/// <summary>
/// Summary description for LoginBuisness
/// </summary>
public class LoginBuisness
{
	public LoginBuisness()
	{
		
	}
    LoginDAL Login = new LoginDAL();

    public SqlDataReader GetHashedPwd(string UserName)
    {
        return Login.GetHashedPwd(UserName);
    }

    public int AddSecureUser(int seqID, string sDomain, string username, string ACTIVE_IND,
        string USER_ROLE_STRING, string ChangeProgName, string createdUser, string lastName, string FirstName,
        string title, string email, string priPhone, string hashedpassword) //,int LocationID
    {
        return Login.AddSecureUser(seqID,sDomain, username, ACTIVE_IND,
        USER_ROLE_STRING, ChangeProgName, createdUser, lastName, FirstName, title, email, priPhone, SaltedHash.CreateSaltedPasswordHash(hashedpassword)); //, LocationID
    }

    public int  UpdateSecureUser(int seqID, string sDomain, string username, string ACTIVE_IND,
        string USER_ROLE_STRING, string ChangeProgName, string createdUser, string lastName, string FirstName,
        string title, string email, string priPhone, string hashedpassword, int iBSN_ENTITY_LOCTN_SEQ_ID, int iPROFILE_SEQ_ID) 
    {
        string IsUpdatepwd = "";
        if(hashedpassword!="*******")
        {
            hashedpassword = SaltedHash.CreateSaltedPasswordHash(hashedpassword);
            IsUpdatepwd ="Y";
        }
        else
        {
            IsUpdatepwd ="N";
        }
        
        return     Login.UpdateSecureUser(seqID,sDomain, username, ACTIVE_IND,
                   USER_ROLE_STRING, ChangeProgName, createdUser, lastName, FirstName, title, email, priPhone, hashedpassword, IsUpdatepwd,
                   iBSN_ENTITY_LOCTN_SEQ_ID, iPROFILE_SEQ_ID); 
    }


    
    public void ChangePassword(string  Uname, string sPassword, string ChangeBy)
    {
        string newHashedPassword = "";
        newHashedPassword = SaltedHash.CreateSaltedPasswordHash(sPassword);
        Login.ChangePassword(Uname, newHashedPassword, ChangeBy);

    }
    
}
namespace CCRCEncryption
{
    /// <summary>
    /// Provides methods for creating salted password hashes and validating
    /// them against a user-entered password.
    /// </summary>
    public class SaltedHash
    {
        static public bool ValidatePassword(string password, string saltedHash)
        {
            // Extract hash and salt string
            // While storing we added a byte[16] salt , which is always 24 char in length.
            string saltString = saltedHash.Substring(saltedHash.Length - 24);
            string hash1 = saltedHash.Substring(0, saltedHash.Length - 24);

            // Append the salt string to the password
            string saltedPassword = password + saltString;

            // Hash the salted password
            string hash2 = FormsAuthentication.HashPasswordForStoringInConfigFile
                (saltedPassword, "SHA1");

            // Compare the hashes
            return (hash1.CompareTo(hash2) == 0);
        }

        static public string CreateSaltedPasswordHash(string password)
        {
            // Generate random salt string
            RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[16];
            csp.GetNonZeroBytes(saltBytes);
            string saltString = Convert.ToBase64String(saltBytes);

            // Append the salt string to the password
            string saltedPassword = password + saltString;

            // Hash the salted password
            string hash = FormsAuthentication.HashPasswordForStoringInConfigFile
                (saltedPassword, "SHA1");

            // Append the salt to the hash
            string saltedHash = hash + saltString;
            return saltedHash;
        }
    }
}

