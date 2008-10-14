using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
/// <summary>
/// Summary description for LoginDAL
/// </summary>
public class LoginDAL
{
    public LoginDAL()
    {

    }

    public SqlDataReader GetHashedPwd(string username)
    {
        string SQL;//= "SELECT HASHED_PASSWORD  FROM CCRC_USER WHERE NT_USERID='" + username + "'";


        SQL = "SELECT U.CCRC_USER_SEQ_ID, ";
        SQL += "U.ACTIVE_IND USER_ACTIVE_IND, U.USER_ROLE_STRING,";
        SQL += "BE.BSN_ENTITY_SEQ_ID, BE.BSN_ENTITY_TYPE_CODE, BE.ACTIVE_IND BUSINESS_ENTITY_ACTIVE_IND,USER_PASSWORD";
        SQL += " FROM CCRC_USER U INNER JOIN BSN_ENTITY BE ON U.BSN_ENTITY_SEQ_ID = BE.BSN_ENTITY_SEQ_ID";
        SQL += " WHERE  U.NT_USERID = '" + username + "'";



        Sqlhelper sqlhelper = new Sqlhelper();
        return sqlhelper.GetReader(SQL);


    }


    public int AddSecureUser(int seqID, string sDomain, string username, string ACTIVE_IND,
        string USER_ROLE_STRING, string ChangeProgName, string createdUser, string lastName, string FirstName,
        string title, string email, string priPhone, string hashedpassword) //,int locationID
    {
        string SQL = "SELECT COUNT(NT_USERID) AS COUNT_NT_USERID  FROM CCRC_USER WHERE NT_USERID='" + username + "'";
        Sqlhelper sqlhelper = new Sqlhelper();
        string struserCount = "";
        int userCount = 100;
        sqlhelper.ExecuteScalar(SQL, out struserCount);
        Int32.TryParse(struserCount, out userCount);
        int LocationID = 0;
        if (userCount == 0)
        {



            SqlParameter[] l_SqlParameterArray_params = new SqlParameter[14];
            l_SqlParameterArray_params[0] = new SqlParameter("@BSN_ENTITY_LOCTN_SEQ_ID", SqlDbType.Int);
            l_SqlParameterArray_params[0].Direction = ParameterDirection.Output;
            l_SqlParameterArray_params[0].Value = 0;

            l_SqlParameterArray_params = PopulateParameter(seqID, sDomain, username, ACTIVE_IND,
             USER_ROLE_STRING, ChangeProgName, createdUser, lastName, FirstName,
             title, email, priPhone, hashedpassword, l_SqlParameterArray_params);


            sqlhelper.ExecuteNonQuery("ADD_User", out LocationID, l_SqlParameterArray_params);

        }
        else
        {
            throw new CCRCException(" The user   '" + username + "'  Already exists");
        }
        return LocationID;
    }

    public int UpdateSecureUser(int seqID, string sDomain, string username, string ACTIVE_IND,
    string USER_ROLE_STRING, string ChangeProgName, string createdUser, string lastName, string FirstName,
    string title, string email, string priPhone, string hashedpassword, string IsUpdatepwd,
    int iBSN_ENTITY_LOCTN_SEQ_ID, int iPROFILE_SEQ_ID)
    {
        Sqlhelper sqlhelper = new Sqlhelper();
        SqlParameter[] l_SqlParameterArray_params = new SqlParameter[17];
        int LocationID = 0;

        l_SqlParameterArray_params[0] = new SqlParameter("@BSN_ENTITY_LOCTN_SEQ_ID", SqlDbType.Int);
        l_SqlParameterArray_params[0].Direction = ParameterDirection.Output;
        l_SqlParameterArray_params[0].Value = 0;

        l_SqlParameterArray_params = PopulateParameter(seqID, sDomain, username, ACTIVE_IND,
         USER_ROLE_STRING, ChangeProgName, createdUser, lastName, FirstName,
         title, email, priPhone, hashedpassword, l_SqlParameterArray_params);

        l_SqlParameterArray_params[14] = new SqlParameter("@IsUpdatepwd", SqlDbType.NVarChar, 1);
        l_SqlParameterArray_params[14].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[14].Value = IsUpdatepwd;


        l_SqlParameterArray_params[15] = new SqlParameter("@OLD_BSN_ENTITY_LOCTN_SEQ_ID", SqlDbType.Int);
        l_SqlParameterArray_params[15].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[15].Value = iBSN_ENTITY_LOCTN_SEQ_ID;

        l_SqlParameterArray_params[16] = new SqlParameter("@OLD_PROFILE_SEQ_ID", SqlDbType.Int);
        l_SqlParameterArray_params[16].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[16].Value = iPROFILE_SEQ_ID;

        
        sqlhelper.ExecuteNonQuery("Update_User", out LocationID, l_SqlParameterArray_params);
        return LocationID;
    }


    private SqlParameter[] PopulateParameter(int seqID, string sDomain, string username, string ACTIVE_IND,
        string USER_ROLE_STRING, string ChangeProgName, string createdUser, string lastName, string FirstName,
        string title, string email, string priPhone, string hashedpassword, SqlParameter[] l_SqlParameterArray_params)
    {
        l_SqlParameterArray_params[1] = new SqlParameter("@BSN_ENTITY_SEQ_ID", SqlDbType.Int);
        l_SqlParameterArray_params[1].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[1].Value = seqID;

        l_SqlParameterArray_params[2] = new SqlParameter("@NT_DOMAIN", SqlDbType.NVarChar, 50);
        l_SqlParameterArray_params[2].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[2].Value = sDomain;


        l_SqlParameterArray_params[3] = new SqlParameter("@NT_USERID", SqlDbType.NVarChar, 50);
        l_SqlParameterArray_params[3].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[3].Value = username;

        l_SqlParameterArray_params[4] = new SqlParameter("@ACTIVE_IND", SqlDbType.Char, 1);
        l_SqlParameterArray_params[4].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[4].Value = ACTIVE_IND;

        l_SqlParameterArray_params[5] = new SqlParameter("@USER_ROLE_STRING", SqlDbType.NVarChar, 200);
        l_SqlParameterArray_params[5].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[5].Value = USER_ROLE_STRING;

        l_SqlParameterArray_params[6] = new SqlParameter("@CREATE_PROG_NAME", SqlDbType.NVarChar, 30);
        l_SqlParameterArray_params[6].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[6].Value = ChangeProgName;


        l_SqlParameterArray_params[7] = new SqlParameter("@CREATE_USER_ID", SqlDbType.NVarChar, 8);
        l_SqlParameterArray_params[7].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[7].Value = createdUser;

        l_SqlParameterArray_params[8] = new SqlParameter("@LAST_NAME", SqlDbType.NVarChar, 30);
        l_SqlParameterArray_params[8].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[8].Value = lastName;


        l_SqlParameterArray_params[9] = new SqlParameter("@FIRST_NAME", SqlDbType.NVarChar, 30);
        l_SqlParameterArray_params[9].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[9].Value = FirstName;

        l_SqlParameterArray_params[10] = new SqlParameter("@TITLE_TXT", SqlDbType.NVarChar, 100);
        l_SqlParameterArray_params[10].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[10].Value = title;



        l_SqlParameterArray_params[11] = new SqlParameter("@PRIMARY_EMAIL_ADDR", SqlDbType.NVarChar, 255);
        l_SqlParameterArray_params[11].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[11].Value = email;

        l_SqlParameterArray_params[12] = new SqlParameter("@PRIMARY_PHN", SqlDbType.NVarChar, 20);
        l_SqlParameterArray_params[12].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[12].Value = priPhone;


        l_SqlParameterArray_params[13] = new SqlParameter("@USER_PASSWORD", SqlDbType.NVarChar, 500);
        l_SqlParameterArray_params[13].Direction = ParameterDirection.Input;
        l_SqlParameterArray_params[13].Value = hashedpassword;

        return l_SqlParameterArray_params;

    }

    public void ChangePassword(string Uname, string sHashedPassword, string ChangeBy)
    {

        string SQL = "UPDATE CCRC_USER SET USER_PASSWORD='" + sHashedPassword;
        SQL += "',CHG_LST_PROG_NAME='CCRCWEB',CHG_LST_USER_ID='" + ChangeBy + "'";
        SQL += ",LAST_PASSWORD_CHG=GETDATE() WHERE NT_USERID='" + Uname + "'";

        Sqlhelper sqlhelper = new Sqlhelper();
        sqlhelper.ExecuteNonQuery(SQL);

    }

    public SqlDataReader GetUserInfo(string uName)
    {
        string SQL = "SELECT  BSN_ENTITY_SEQ_ID,  CU.NT_USERID, CU.ACTIVE_IND, USER_ROLE_STRING, CU.LAST_NAME, CU.FIRST_NAME,";
        SQL += " CU.TITLE_TXT, CU.PRIMARY_EMAIL_ADDR,CU.PRIMARY_PHN,LCU.BSN_ENTITY_LOCTN_SEQ_ID,PROFILE_SEQ_ID";
        SQL += " FROM     CCRC_USER  CU INNER JOIN BSN_ENTITY_LOCTN_CCRC_USER  LCU ON  CU.CCRC_USER_SEQ_ID =LCU.CCRC_USER_SEQ_ID";
        SQL += " INNER JOIN [PROFILE] PF ON PF.NT_USERID= CU.NT_USERID WHERE     CU.NT_USERID = '" + uName + "'";

        Sqlhelper sqlhelper = new Sqlhelper();
        return sqlhelper.GetReader(SQL);
    }

    public SqlDataReader GetBSN_ENTITY()
    {
        string SQL = "SELECT BSN_ENTITY_SEQ_ID,BSN_ENTITY_NAME FROM BSN_ENTITY WHERE ACTIVE_IND='Y'";
        Sqlhelper sqlhelper = new Sqlhelper();
        return sqlhelper.GetReader(SQL);

    }

    public SqlDataReader GetBSN_ENTITY_LOCTN()
    {
        string SQL = "SELECT BSN_ENTITY_LOCTN_SEQ_ID,LOCTN_NAME FROM BSN_ENTITY_LOCTN  WHERE ACTIVE_IND='Y'";
        Sqlhelper sqlhelper = new Sqlhelper();
        return sqlhelper.GetReader(SQL);

    }

    public void UpdateLastLogin(int userSeqID)
    {
        string SQL = "UPDATE CCRC_USER SET LAST_LOGIN= GETDATE() WHERE  CCRC_USER_SEQ_ID= " + userSeqID;
        Sqlhelper sqlhelper = new Sqlhelper();
        sqlhelper.ExecuteNonQuery(SQL);
    }

}
