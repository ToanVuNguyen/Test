using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using System;
using System.Data.SqlClient;
using System.Configuration;

namespace HPF.FutureState.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for BudgetDAOTest and is intended
    ///to contain all BudgetDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BudgetDAOTest
    {


        private TestContext testContextInstance;
        public static int fc_id = -1;

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
            string sql = "Insert into foreclosure_case "
                + " (agency_id, program_id, intake_dt"
                + ", borrower_fname, borrower_lname, primary_contact_no"
                + ", contact_addr1, contact_city, contact_state_cd, contact_zip"
                + ", funding_consent_ind, servicer_consent_ind, counselor_email"
                + ", counselor_phone, opt_out_newsletter_ind, opt_out_survey_ind"
                + ", do_not_call_ind, owner_occupied_ind, primary_residence_ind"
                + ", counselor_fname, counselor_lname, counselor_id_ref"
                + ", prop_zip, agency_case_num, borrower_last4_SSN"
                + ", chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " (" + "1" + ", 1, '" + DateTime.Now + "'"
                + ", '" + "Sinh-Test" + "', 'Sinh-Test', 'pcontactno'"
                + ", 'address1', 'cty', 'scod', 'czip'"
                + ", 'Y', 'Y', 'email'"
                + ", 'phone', 'Y', 'Y'"
                + ", 'Y', 'Y', 'Y'"
                + ", 'cfname', 'clname', 'cidref'"
                + ", '" + "9999" + "', '" + "abc" + "', '" + "1111" + "'"
                + ", 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = sql;
            command.ExecuteNonQuery();


            command.CommandText = "Select fc_id from foreclosure_case where borrower_fname='Sinh-Test' and borrower_lname='Sinh-Test'";
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                fc_id = int.Parse(reader["fc_id"].ToString());
            }
            reader.Close();

            command.CommandText = "insert into budget_set (fc_id,budget_set_dt,chg_lst_app_name,chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt)  values (" + fc_id.ToString() + ",'" + DateTime.Now + "','HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "')";
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;

            command.CommandText = "delete from budget_set where fc_id=" + fc_id.ToString();
            command.ExecuteNonQuery();

            command.CommandText = "Delete foreclosure_case where borrower_fname='Sinh-Test' and borrower_lname='Sinh-Test' ";
            command.ExecuteNonQuery();

            
            dbConnection.Close();
        }
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for GetBudgetSet
        ///</summary>
        [TestMethod()]
        public void GetBudgetSetTest()
        {
            BudgetDAO_Accessor target = new BudgetDAO_Accessor(); // TODO: Initialize to an appropriate value
            BudgetSetDTOCollection actual;
            actual = target.GetBudgetSet(fc_id);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(fc_id, actual[0].FcId);
        }

    }
}
