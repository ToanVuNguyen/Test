using HPF.FutureState.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Configuration;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data.SqlTypes;
namespace HPF.FutureState.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for OutcomeItemBLTest and is intended
    ///to contain all OutcomeItemBLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OutcomeItemBLTest
    {
        

        private TestContext testContextInstance;

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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [ClassInitialize()]
        static public void MyTestInitialize(TestContext testContext)
        {
            ClearTestData();
            GenerateTestData();
        }
        //
        //Use TestCleanup to run code after each test has run
        [ClassCleanup()]
        static public void MyTestCleanup()
        {
            ClearTestData();
        }
        //
        #endregion


        /// <summary>
        ///A test for InstateOutcomeItem
        ///</summary>
        [Ignore]
        [TestMethod()]
        public void InstateOutcomeItemTest()
        {
            OutcomeItemBL_Accessor target = new OutcomeItemBL_Accessor(); // TODO: Initialize to an appropriate value

            string workingUserId = "test data";
            int fc_id = GetFcID();
            OutcomeItemDTO outcomeItem = GetOutcomeItem(fc_id, null);
            int? outcomeItemId = outcomeItem.OutcomeItemId;
            DeleteOutcomeItem(outcomeItemId);
            DateTime? expected = null;

            target.InstateOutcomeItem(outcomeItemId, workingUserId);
            outcomeItem = GetOutcomeItem(null, outcomeItemId);
            DateTime? actual = outcomeItem.OutcomeDeletedDt;

            Assert.AreEqual(expected, actual);
        }

        [Ignore]
        [TestMethod()]
        public void InstateOutcomeItemTest_Fail()
        {
            OutcomeItemBL_Accessor target = new OutcomeItemBL_Accessor(); // TODO: Initialize to an appropriate value

            string workingUserId = "test data";
            int fc_id = GetFcID();
            OutcomeItemDTO outcomeItem = GetOutcomeItem(fc_id, null);
            int? outcomeItemId = outcomeItem.OutcomeItemId;
            DeleteOutcomeItem(outcomeItemId);
            DateTime? expected = DateTime.Now.Date;

            target.InstateOutcomeItem(outcomeItemId, workingUserId);
            outcomeItem = GetOutcomeItem(null, outcomeItemId);
            DateTime? actual = outcomeItem.OutcomeDeletedDt;

            Assert.AreNotEqual(expected, actual);
        }
        
        /// <summary>
        ///A test for DeleteOutcomeItem
        ///</summary>
        [Ignore]
        [TestMethod()]
        public void DeleteOutcomeItemTest()
        {
            OutcomeItemBL_Accessor target = new OutcomeItemBL_Accessor(); // TODO: Initialize to an appropriate value

            string workingUserId = "test data";
            int fc_id = GetFcID();
            OutcomeItemDTO outcomeItem = GetOutcomeItem(fc_id, null);
            int? outcomeItemId = outcomeItem.OutcomeItemId;
            DateTime? expected = DateTime.Now.Date;

            target.DeleteOutcomeItem(outcomeItemId, workingUserId);
            outcomeItem = GetOutcomeItem(null, outcomeItemId);
            DateTime? actual = outcomeItem.OutcomeDeletedDt.Value.Date;

            Assert.AreEqual(expected, actual);
        }

        [Ignore]
        [TestMethod()]
        public void DeleteOutcomeItemTest_Fail()
        {
            OutcomeItemBL_Accessor target = new OutcomeItemBL_Accessor(); // TODO: Initialize to an appropriate value

            string workingUserId = "test data";
            int fc_id = GetFcID();
            OutcomeItemDTO outcomeItem = GetOutcomeItem(fc_id, null);
            int? outcomeItemId = outcomeItem.OutcomeItemId;
            DateTime? expected = null;

            target.DeleteOutcomeItem(outcomeItemId, workingUserId);
            outcomeItem = GetOutcomeItem(null, outcomeItemId);
            DateTime? actual = outcomeItem.OutcomeDeletedDt.Value.Date;

            Assert.AreNotEqual(expected, actual);
        }

        #region helper
        #region property
        static string prop_zip = "58686";
        static string ssn = "5868";
        static string agency_case_number = "586868686868";
        static string first_name = "Test data";
        static int agency_id = 2;
        static string acct_num = "acct_num5868";

        static string agency_name = "agency_name_58";
        static string servicer_name = "servicer_name_58";

        static string outcome_type_name = "otn_586868";
        #endregion
        
        static private void GenerateTestData()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            string sql = "Insert into Agency "
                + " (agency_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('" + agency_name + "', 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);
            agency_id = GetAgencyID();

            #region fc_case
            sql = "Insert into foreclosure_case "
               + " (agency_id, program_id, intake_dt"
               + ", borrower_fname, borrower_lname, primary_contact_no"
               + ", contact_addr1, contact_city, contact_state_cd, contact_zip"
               + ", funding_consent_ind, servicer_consent_ind, counselor_email"
               + ", counselor_phone, opt_out_newsletter_ind, opt_out_survey_ind"
               + ", do_not_call_ind, owner_occupied_ind, primary_residence_ind"
               + ", counselor_fname, counselor_lname, counselor_id_ref"
               + ", prop_zip, agency_case_num, borrower_last4_SSN"
               + ", chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
               + " (" + agency_id + ", 1, '" + DateTime.Now + "'"
               + ", '" + first_name + "', 'Lastname', 'pcontactno'"
               + ", 'address1', 'cty', 'scod', 'czip'"
               + ", 'Y', 'Y', 'email'"
               + ", 'phone', 'Y', 'Y'"
               + ", 'Y', 'Y', 'Y'"
               + ", 'cfname', 'clname', 'cidref'"
               + ", '" + prop_zip + "', '" + agency_case_number + "', '" + ssn + "'"
               + ", 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);
            #endregion

            #region outcome


            sql = "Insert into Outcome_Type "
                + " (outcome_type_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('" + outcome_type_name + "', 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);

            int outcome_type_id = GetOutcomeTypeId();
            int fc_id = GetFcID();
            sql = "Insert into Outcome_Item "
                + " (fc_id, outcome_type_id, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " (" + fc_id + ", " + outcome_type_id + ", 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);
            #endregion

        }

        static private void ClearTestData()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            int fc_id = GetFcID();
            string sql = "Delete from Case_Loan where fc_id = " + fc_id;
            ExecuteSql(sql, dbConnection);

            sql = "Delete from activity_log where fc_id = " + fc_id; ;
            ExecuteSql(sql, dbConnection);

            sql = "Delete from Outcome_Item where fc_id = " + fc_id;
            ExecuteSql(sql, dbConnection);

            sql = "Delete from Outcome_Type where outcome_type_name = '" + outcome_type_name + "'";
            ExecuteSql(sql, dbConnection);

            sql = "Delete from Foreclosure_case where fc_id = " + fc_id;
            ExecuteSql(sql, dbConnection);

            sql = "Delete from Agency where Agency_Name = '" + agency_name + "'";
            ExecuteSql(sql, dbConnection);

            dbConnection.Close();
        }

        static private int GetFcID()
        {
            int result = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string sql = "SELECT fc_id FROM foreclosure_case " +
                            " WHERE prop_zip = '" + prop_zip + "'"
                            + " and agency_case_num = '" + agency_case_number + "'";
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = int.Parse(reader["fc_id"].ToString());
                    break;
                }
            }
            catch 
            {
                dbConnection.Close();
            }
            dbConnection.Close();
            return result;
        }

        static private int GetAgencyID()
        {
            int id = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string sql = "Select agency_id from Agency where agency_name = '" + agency_name + "'";
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                    id = reader.GetInt32(0);
            }

            dbConnection.Close();
            return id;
        }

        static private int GetServicerID()
        {
            int id = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string sql = "Select servicer_id from Servicer where servicer_name = '" + servicer_name + "'";
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                    id = reader.GetInt32(0);
            }

            dbConnection.Close();
            return id;
        }

        static private int GetOutcomeTypeId()
        {
            int id = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string sql = "Select outcome_type_id from Outcome_Type where outcome_type_name = '" + outcome_type_name + "'";
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                    id = reader.GetInt32(0);
            }

            dbConnection.Close();
            return id;
        }

        static private OutcomeItemDTO GetOutcomeItem(int? fc_id, int? outcomeItemId)
        {
            OutcomeItemDTO outcomeItem = new OutcomeItemDTO();
            
            
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string sql = "";
            if (outcomeItemId == null)
                sql = "select outcome_item_id, outcome_deleted_dt from outcome_item where fc_id = " + fc_id;
            else if (fc_id == null)
                sql = "select outcome_item_id, outcome_deleted_dt from outcome_item where outcome_item_id = " + outcomeItemId;
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    outcomeItem.OutcomeItemId = reader.GetInt32(0);
                    if (reader.GetSqlDateTime(1).IsNull)
                        outcomeItem.OutcomeDeletedDt = null;
                    else
                        outcomeItem.OutcomeDeletedDt = (DateTime?)reader.GetSqlDateTime(1);
                }
            }

            dbConnection.Close();
            return outcomeItem;
        }

        static private void DeleteOutcomeItem(int? outcomeItemId)
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            string sql = "Update Outcome_Item set outcome_deleted_dt = '" + DateTime.Now + "' where outcome_item_id = " + outcomeItemId;
            ExecuteSql(sql, dbConnection);
            dbConnection.Close();
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
