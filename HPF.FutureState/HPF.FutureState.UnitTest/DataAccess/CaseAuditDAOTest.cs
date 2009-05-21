using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace HPF.FutureState.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for CaseAuditDAOTest and is intended
    ///to contain all CaseAuditDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CaseAuditDAOTest
    {


        private TestContext testContextInstance;
        private static string working_user_id = "wuid_CaseAuditDAOTest_6868";
        private static int fc_id = 0;
        private static int agency_id = 0;
        private static int case_audit_id = 0;
        private static string audit_comment = "audit_comment_6868686868686868";
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {

            ClearTestData();
            GenerateTestData();
            
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            ClearTestData();
        }
        #endregion            
        /// <summary>
        ///A test for SaveCaseAudit
        ///</summary>
        [TestMethod()]
        public void InsertCaseAuditTest_Pass()
        {
            CaseAuditDAO_Accessor target = new CaseAuditDAO_Accessor(); // TODO: Initialize to an appropriate value
            CaseAuditDTO caseAudit = new CaseAuditDTO()
            {
                FcId = fc_id,
                AuditComments = audit_comment,
            };
            caseAudit.SetInsertTrackingInformation(working_user_id);
            
            var actual = target.SaveCaseAudit(caseAudit, false);
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void UpdateCaseAuditTest_Pass()
        {
            CaseAuditDAO_Accessor target = new CaseAuditDAO_Accessor(); // TODO: Initialize to an appropriate value
            CaseAuditDTO caseAudit = new CaseAuditDTO()
            {
                FcId = fc_id,
                CaseAuditId = case_audit_id,
                AuditComments = audit_comment
            };
            caseAudit.SetUpdateTrackingInformation(working_user_id);

            var actual = target.SaveCaseAudit(caseAudit, true);
            Assert.AreEqual(true, actual);

            caseAudit = target.GetCaseAudits(fc_id)[0];
            Assert.AreEqual(audit_comment, caseAudit.AuditComments);
        }

        #region helper
        static private void GenerateTestData()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();

            string sql = "Insert into Agency "
                + " (agency_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('agency_name', 'HPF' ,'" + working_user_id + "' ,'" + DateTime.Now + "', 'HPF', '" + working_user_id + "', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);
            agency_id = GetAgencyId();

            #region fc_case
            sql = "Insert into foreclosure_case "
              + " (duplicate_ind, prop_addr1, prop_city, prop_state_cd "
              + ", agency_id, program_id, intake_dt"
              + ", borrower_fname, borrower_lname, primary_contact_no"
              + ", contact_addr1, contact_city, contact_state_cd, contact_zip"
              + ", funding_consent_ind, servicer_consent_ind, counselor_email"
              + ", counselor_phone, opt_out_newsletter_ind, opt_out_survey_ind"
              + ", do_not_call_ind, owner_occupied_ind, primary_residence_ind"
              + ", counselor_fname, counselor_lname, counselor_id_ref"
              + ", prop_zip, agency_case_num, borrower_last4_SSN"
              + ", chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
              + " ('N', 'prop_addr1', 'prop_city', 'cd', " + agency_id + ", 1, '" + DateTime.Now + "'"
              + ", 'Firstname', 'Lastname', 'pcontactno'"
              + ", 'address1', 'cty', 'scod', 'czip'"
              + ", 'Y', 'Y', 'email'"
              + ", 'phone', 'Y', 'Y'"
              + ", 'Y', 'Y', 'Y'"
              + ", 'cfname', 'clname', 'cidref'"
              + ", '12345', 'acn121314', '1234'"
              + ", 'HPF' ,'" + working_user_id + "' ,'" + DateTime.Now + "', 'HPF', '" + working_user_id + "', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);   

            #endregion

            fc_id = GetFcID();
            sql = "Insert into Case_Audit"
               + " (fc_id, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
               + " (" + fc_id + ", 'HPF' ,'" + working_user_id + "' ,'" + DateTime.Now + "', 'HPF', '" + working_user_id + "', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);
            case_audit_id = GetCaseAuditId();
            dbConnection.Close();
        }

        static private void ClearTestData()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();


            string sql = "Delete from case_audit where create_user_id = '" + working_user_id + "'";
            ExecuteSql(sql, dbConnection);

            sql = "Delete from foreclosure_case where create_user_id = '" + working_user_id + "'";
            ExecuteSql(sql, dbConnection);

             sql = "Delete from Agency where create_user_id = '" + working_user_id + "'";
            ExecuteSql(sql, dbConnection);

            dbConnection.Close();

        }
        
        static private int GetFcID()
        {
            int result = 0;            
            string sql = "SELECT fc_id FROM foreclosure_case " +
                            " WHERE create_user_id = '" + working_user_id + "'";

            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                if (reader.Read())
                    result = int.Parse(reader["fc_id"].ToString());
            }
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
            return result;
        }

        static private int GetAgencyId()
        {
            int result = 0;
            string sql = "SELECT agency_id FROM agency " +
                            " WHERE create_user_id = '" + working_user_id + "'";

            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                if (reader.Read())
                    result = int.Parse(reader["agency_id"].ToString());
            }
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
            return result;
        }

        static private int GetCaseAuditId()
        {
            int result = 0;
            string sql = "SELECT case_audit_id FROM case_audit " +
                            " WHERE create_user_id = '" + working_user_id + "'";
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                if (reader.Read())
                    result = int.Parse(reader["case_audit_id"].ToString());
            }
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
            return result;
        }
        
        static private void ExecuteSql(string sql, SqlConnection dbConnection)
        {
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }
        #endregion
    }
}
