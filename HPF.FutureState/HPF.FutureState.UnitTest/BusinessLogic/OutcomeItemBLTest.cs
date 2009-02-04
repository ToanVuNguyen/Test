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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            GenerateTestData();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            ClearTestData();
        }
        //
        #endregion


        /// <summary>
        ///A test for InstateOutcomeItem
        ///</summary>
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
        string prop_zip = "68686";
        string ssn = "6868";
        string agency_case_number = "686868686868";
        string first_name = "Test data";
        int agency_id = 2;
        string acct_num = "acct_num6868";

        string agency_name = "agency_name_68";
        string servicer_name = "servicer_name_68";

        string outcome_type_name = "otn_686868";
        #endregion
        
        private void GenerateTestData()
        {
            string sql = "Insert into Agency "
                + " (agency_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('" + agency_name + "', 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql);
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
            ExecuteSql(sql);
            #endregion

            #region outcome


            sql = "Insert into Outcome_Type "
                + " (outcome_type_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('" + outcome_type_name + "', 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql);

            int outcome_type_id = GetOutcomeTypeId();
            int fc_id = GetFcID();
            sql = "Insert into Outcome_Item "
                + " (fc_id, outcome_type_id, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " (" + fc_id + ", " + outcome_type_id + ", 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql);
            #endregion

        }

        private void ClearTestData()
        {
            int fc_id = GetFcID();
            string sql = "Delete from Case_Loan where fc_id = " + fc_id;
            ExecuteSql(sql);

            sql = "Delete from activity_log where fc_id = " + fc_id; ;
            ExecuteSql(sql);

            sql = "Delete from Outcome_Item where fc_id = " + fc_id;
            ExecuteSql(sql);

            sql = "Delete from Outcome_Type where outcome_type_name = '" + outcome_type_name + "'";
            ExecuteSql(sql);

            sql = "Delete from Foreclosure_case where fc_id = " + fc_id;
            ExecuteSql(sql);

            sql = "Delete from Agency where Agency_Name = '" + agency_name + "'";
            ExecuteSql(sql);

            
        }

        private int GetFcID()
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

        private int GetAgencyID()
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

        private int GetServicerID()
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

        private int GetOutcomeTypeId()
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

        private OutcomeItemDTO GetOutcomeItem(int? fc_id, int? outcomeItemId)
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

        private void DeleteOutcomeItem(int? outcomeItemId)
        {
            string sql = "Update Outcome_Item set outcome_deleted_dt = '" + DateTime.Now + "' where outcome_item_id = " + outcomeItemId;
            ExecuteSql(sql);
        }

        private void ExecuteSql(string sql)
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = sql;
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        #endregion
    }
}
