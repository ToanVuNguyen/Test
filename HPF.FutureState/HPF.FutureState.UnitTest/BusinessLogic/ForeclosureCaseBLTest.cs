using HPF.FutureState.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Configuration;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.DataAccess;
using System.Collections;
using HPF.FutureState.Common.Utils.Exceptions;
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.ObjectModel;
using System.Data;
using System.Collections.Generic;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils.DataValidator;
namespace HPF.FutureState.UnitTest.BusinessLogic
{
    
    
    /// <summary>
    ///This is a test class for ForeclosureCaseBLTest and is intended
    ///to contain all ForeclosureCaseBLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ForeclosureCaseBLTest
    {


        private TestContext testContextInstance;

        static string prop_zip = "68686";
        static string ssn = "6868";
        static string agency_case_number = "686868686868";
        static string first_name = "Test data";
        static int agency_id = 2;
        static string acct_num = "acct_num6868";
        static string working_user_id = "utest_FC_test_12345";

        static string agency_name = "agency_name_68";
        static string servicer_name = "servicer_name_68";
        static int fc_id = 0;
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
            SearchFcCase_ClearTestData();
            SearchFcCase_GenerateTestData();            
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            SearchFcCase_ClearTestData();
        }
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            
        }
        
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            
        }
        //
        #endregion


        /// <summary>
        ///A test for SearchForeclosureCase
        ///</summary>

        #region SearchForeclosureCase
        #region Prop_Zip
        [TestMethod()]
        public void SearchFcCase_PropZip_Pass()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            
            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            searchCriteria.PropertyZip = prop_zip;

            //int expected = SearchFcCase_GetFcID();
            ForeclosureCaseSearchResult actual = target.SearchForeclosureCase(searchCriteria, 50);

            //SearchFcCase_ClearTestData();
            Assert.AreEqual(1, actual.Count);  
            Assert.AreEqual(fc_id, actual[0].FcId);  
            //TestContext.WriteLine(string.Format("Expected: {0} - Actual: {1} ",expected, actual));
        }

        
        
        [TestMethod()]
        public void SearchFcCase_PropZip_Fail()
        {
            //SearchFcCase_GenerateTestData();

            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            
            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            searchCriteria.PropertyZip = prop_zip;

            int expected = 2; //number of cases returned
            int actual = target.SearchForeclosureCase(searchCriteria, 50).Count;

            //SearchFcCase_ClearTestData();

            Assert.AreNotEqual(expected, actual);
            TestContext.WriteLine(string.Format("Expected: {0} rows found - Actual: {1} rows found",expected, actual));
        }
        
        [TestMethod()]
        public void SearchFcCase_PropZip_Null()
        {
            //SearchFcCase_GenerateTestData();

            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            searchCriteria.PropertyZip = null;

            DataValidationException expected = new DataValidationException();
            try
            {
                target.SearchForeclosureCase(searchCriteria, 50);
            }
            catch (DataValidationException actual)
            {
                Assert.AreEqual(expected.GetType(), actual.GetType());
                TestContext.WriteLine(string.Format("Expected: {0} - Actual: {1} ", expected.GetType(), actual.GetType()));
            }
            
            
            
        }
        
        [TestMethod()]
        public void SearchFcCase_PropZip_Invalid()
        {
            //SearchFcCase_GenerateTestData();

            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            searchCriteria.PropertyZip = "@#$%";

            DataValidationException expected = new DataValidationException(); //number of cases returned
            try
            {
                target.SearchForeclosureCase(searchCriteria, 50);
            }
            catch (DataValidationException actual)
            {
                Assert.AreEqual(expected.GetType(), actual.GetType());
                TestContext.WriteLine(string.Format("Expected: {0} - Actual: {1} ", expected, actual));

            }
            
        }
        #endregion
        
        #region SSN
        [TestMethod()]
        public void SearchFcCase_SSN_Pass()
        {
                       
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value

            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            searchCriteria.PropertyZip = prop_zip;
            searchCriteria.Last4_SSN = ssn;

            int expected = SearchFcCase_GetFcID();
            ForeclosureCaseSearchResult actual = target.SearchForeclosureCase(searchCriteria, 50);

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(expected, actual[0].FcId);  
            
            TestContext.WriteLine(string.Format("Expected: {0} rows found - Actual: {1} rows found", expected, actual));
        }
        
        [TestMethod()]
        public void SearchFcCase_SSN_Fail()
        {

            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            searchCriteria.PropertyZip = prop_zip;
            searchCriteria.Last4_SSN = "1235";

            int expected = 1; //number of cases returned
            int actual = target.SearchForeclosureCase(searchCriteria, 50).Count;


            Assert.AreNotEqual(expected, actual);
            TestContext.WriteLine(string.Format("Expected: {0} rows found - Actual: {1} rows found", expected, actual));
        }
        
        [TestMethod()]
        public void SearchFcCase_SSN_Invalid()
        {

            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            
            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            searchCriteria.Last4_SSN = "@#$%";
            searchCriteria.PropertyZip = prop_zip;

            DataValidationException expected = new DataValidationException(); //number of cases returned
            try
            {
                target.SearchForeclosureCase(searchCriteria, 50);
            }
            catch (Exception actual)
            {
                Assert.AreEqual(expected.GetType(), actual.GetType());
                TestContext.WriteLine(string.Format("Expected: {0} - Actual: {1} ", expected.GetType(), actual.GetType()));
            }
           
        }

        
        #endregion

        #region FirstName
        [TestMethod()]
        public void SearchFcCase_FirstName_Pass()
        {

            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            searchCriteria.PropertyZip = prop_zip;
            searchCriteria.FirstName = first_name;

            int expected = SearchFcCase_GetFcID();
            ForeclosureCaseSearchResult actual = target.SearchForeclosureCase(searchCriteria, 50);

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(expected, actual[0].FcId);  


            TestContext.WriteLine(string.Format("Expected: {0} rows found - Actual: {1} rows found", expected, actual));
        }
        [TestMethod()]
        public void SearchFcCase_FirstName_Fail()
        {

            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            searchCriteria.PropertyZip = prop_zip;
            searchCriteria.FirstName = "1235afdasdf";            

            int expected = 1; //number of cases returned
            int actual = target.SearchForeclosureCase(searchCriteria, 50).Count;

            Assert.AreNotEqual(expected, actual);
            TestContext.WriteLine(string.Format("Expected: {0} rows found - Actual: {1} rows found", expected, actual));
        }
        [TestMethod()]
        public void SearchFcCase_FirstName_Invalid()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value

            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            searchCriteria.FirstName = "@#$%";
            searchCriteria.PropertyZip = prop_zip;

            DataValidationException expected = new DataValidationException(); //number of cases returned
            try
            {
                target.SearchForeclosureCase(searchCriteria, 50);
            }
            catch (DataValidationException actual)
            {
                Assert.AreEqual(expected.GetType(), actual.GetType());
                TestContext.WriteLine(string.Format("Expected: {0} - Actual: {1} ", expected.GetType(), actual.GetType()));

            }
        }
        #endregion

        #region AgencyCaseNumber
        [TestMethod()]
        public void SearchFcCase_AgencyCaseNumber_Pass()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            
            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            searchCriteria.PropertyZip = prop_zip;
            searchCriteria.AgencyCaseNumber = agency_case_number;

            int expected = SearchFcCase_GetFcID();
            ForeclosureCaseSearchResult actual = target.SearchForeclosureCase(searchCriteria, 50);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(expected, actual[0].FcId);  

            TestContext.WriteLine(string.Format("Expected: {0} found - Actual: {1} found", expected, actual));
        }

        [TestMethod()]
        public void SearchFcCase_AgencyCaseNumber_Fail()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            searchCriteria.PropertyZip = prop_zip;
            searchCriteria.AgencyCaseNumber = "123421";

            int expected = 1; //number of cases returned
            int actual = target.SearchForeclosureCase(searchCriteria, 50).Count;

            Assert.AreNotEqual(expected, actual);
            TestContext.WriteLine(string.Format("Expected: {0} rows found - Actual: {1} rows found", expected, actual));
        }

        [TestMethod()]        
        public void SearchFcCase_AgencyCaseNumber_Invalid()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value

            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();
            searchCriteria.PropertyZip = prop_zip;
            searchCriteria.AgencyCaseNumber = "asdfgds";

            DataValidationException expected = new DataValidationException(); //number of cases returned
            try
            {
                target.SearchForeclosureCase(searchCriteria, 50);
            }
            catch (Exception actual)
            {
                TestContext.WriteLine(string.Format("Expected: {0} - Actual: {1} ", expected.GetType(), actual.GetType()));
                Assert.AreEqual(expected.GetType(), actual.GetType());              
            }           
        }
        #endregion

        #region helper

        static private void SearchFcCase_GenerateTestData()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            
            string sql = "Insert into Agency "
                + " (agency_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('" + agency_name + "', 'HPF' ,'" + working_user_id+ "' ,'" + DateTime.Now + "', 'HPF', '"+working_user_id+"', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);
            agency_id = SearchFcCase_GetAgencyID();
            
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
                + ", 'HPF' ,'" + working_user_id +"' ,'" + DateTime.Now + "', 'HPF', '" + working_user_id +"', '" + DateTime.Now + "' )";
             ExecuteSql(sql, dbConnection);
            #endregion

            #region servicer, case_loan
            
            sql = "Insert into Servicer "
                + " (servicer_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('" + servicer_name + "', 'HPF' ,'" + working_user_id + "' ,'" + DateTime.Now + "', 'HPF', '" + working_user_id + "', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);

            fc_id = SearchFcCase_GetFcID();
            int servicer_id = SearchFcCase_GetServicerID();
            sql = "Insert into Case_Loan "
                + " (fc_id, servicer_id, acct_num, loan_1st_2nd_cd, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " (" + fc_id + ", " + servicer_id + ", '" + acct_num + "', '1st', 'HPF' ,'"+working_user_id+"' ,'" + DateTime.Now + "', 'HPF', '" + working_user_id+"', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);
            #endregion

            dbConnection.Close();
        }

        static private void SearchFcCase_ClearTestData()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();

            //int fc_id = SearchFcCase_GetFcID();
            //int bsId = GetBudgetSetId(fc_id);

            string sql = "Delete from Case_Loan Where create_user_id = '" + working_user_id + "'";
            ExecuteSql(sql, dbConnection);

            sql = "DELETE FROM Budget_Asset WHERE create_user_id ='" + working_user_id + "'";
            ExecuteSql(sql, dbConnection);

            sql = "DELETE FROM Budget_Item WHERE create_user_id ='" +working_user_id + "'";
            ExecuteSql(sql, dbConnection);

            sql = "DELETE FROM Budget_Set WHERE create_user_id ='" +working_user_id + "'";
            ExecuteSql(sql, dbConnection);

            sql = "DELETE FROM Outcome_Item WHERE create_user_id ='" + working_user_id + "'";
            ExecuteSql(sql, dbConnection);

            sql = "Delete from activity_log where create_user_id = '" + working_user_id + "'"; ;
            ExecuteSql(sql, dbConnection);

            sql = "Delete from Foreclosure_case where create_user_id = '" + working_user_id + "'";
            ExecuteSql(sql, dbConnection);

            sql = "Delete from Agency where create_user_id = '" +working_user_id + "'";
            ExecuteSql(sql, dbConnection);

            sql = "Delete from Servicer where create_user_id = '"  +working_user_id + "'";
            ExecuteSql(sql, dbConnection);

            dbConnection.Close();
        }

        static private int SearchFcCase_GetFcID()
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
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
            return result;
        }

        static private int SearchFcCase_GetAgencyID()
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

        static private int SearchFcCase_GetServicerID()
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

        static private void ExecuteSql(string sql, SqlConnection dbConnection)
        {            
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = sql;
            command.ExecuteNonQuery();            
        }

        #endregion

        #endregion
        [Ignore]
        [TestMethod()]
        public void SaveForeclosureCase_MissingRequiredFields()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseDTO fcCase = SetForeclosureCase("True");
            fcCase.ProgramId = 0;
            ForeclosureCaseSetDTO fcCaseSet = new ForeclosureCaseSetDTO();
            fcCaseSet.ForeclosureCase = fcCase;

            var expected = new DataValidationException();
            try
            {
                target.SaveForeclosureCaseSet(fcCaseSet);
            }
            catch (DataValidationException actual)
            {
                expected = actual;
                Assert.AreEqual(expected.GetType(), actual.GetType()); 
            }

            if (expected.ExceptionMessages != null && expected.ExceptionMessages.Count > 0)
                foreach (ExceptionMessage em in expected.ExceptionMessages)
                    testContextInstance.WriteLine(em.Message);
            else
                testContextInstance.WriteLine(expected.Message);
        }

        [Ignore]
        [TestMethod()]
        public void SaveForeclosureCase_BadCode()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseDTO fcCase = SetForeclosureCase("True");
            fcCase.IncomeEarnersCd = "-123124";
            ForeclosureCaseSetDTO fcCaseSet = new ForeclosureCaseSetDTO();
            fcCaseSet.ForeclosureCase = fcCase;

            var expected = new DataValidationException();
            try
            {
                target.SaveForeclosureCaseSet(fcCaseSet);
            }
            catch (DataValidationException actual)
            {
                expected = actual;
                Assert.AreEqual(expected.GetType(), actual.GetType());
            }

            if (expected.ExceptionMessages != null && expected.ExceptionMessages.Count > 0)
                foreach (ExceptionMessage em in expected.ExceptionMessages)
                    testContextInstance.WriteLine(em.Message);
            else
                testContextInstance.WriteLine(expected.Message);
        }

        [Ignore]
        [TestMethod()]
        public void SaveForeclosureCase_NotValidFcIdForAgencyId()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseDTO fcCase = SetForeclosureCase("True");

            GeoCodeRefDTOCollection cols = GeoCodeRefDAO.Instance.GetGeoCodeRef();
            fcCase.ContactZip = cols[0].ZipCode;
            fcCase.ContactStateCd = cols[0].StateAbbr;

            fcCase.PropStateCd = cols[0].StateAbbr;
            fcCase.PropZip = cols[0].ZipCode;

            

            fcCase.FcId = 123124;
            fcCase.AgencyId = 12345;
            ForeclosureCaseSetDTO fcCaseSet = new ForeclosureCaseSetDTO();
            fcCaseSet.ForeclosureCase = fcCase;

            var expected = new DataValidationException();
            try
            {
                target.SaveForeclosureCaseSet(fcCaseSet);
            }
            catch (DataValidationException actual)
            {
                expected = actual;
                Assert.AreEqual(expected.GetType(), actual.GetType());
            }

            if (expected.ExceptionMessages != null && expected.ExceptionMessages.Count > 0)
                foreach (ExceptionMessage em in expected.ExceptionMessages)
                    testContextInstance.WriteLine(em.Message);
            else
                testContextInstance.WriteLine(expected.Message);
        }

        private GeoCodeRefDTOCollection GetGeoCodeRefDTO()
        {
            return GeoCodeRefDAO.Instance.GetGeoCodeRef();
            
        }

       

        

        #region ProcessInsertUpdateWithoutForeclosureCaseId
        [Ignore]
        [TestMethod]
        public void ProcessInsertUpdateWithoutForeclosureCaseId_Success_Null_FC_CaseSet()
        {
            TestContext.WriteLine("This test will throw an DataValidationException when ForeclosureCaseSetDTO is null");
            
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = null; // TODO: Initialize to an appropriate value
            var expected = (new DataValidationException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithoutForeclosureCaseId(foreclosureCaseSet);
            }
            catch (DataValidationException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }

        [Ignore]
        [TestMethod]
        public void ProcessInsertUpdateWithoutForeclosureCaseId_Success_Null_AgencyID_CaseNumber()
        {
            TestContext.WriteLine("This test will throw an DataValidationException when AgencyID or CaseNumber is null");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO();
            foreclosureCaseSet.ForeclosureCase = new ForeclosureCaseDTO();
            foreclosureCaseSet.ForeclosureCase.AgencyId = 0;
            foreclosureCaseSet.ForeclosureCase.AgencyCaseNum = null;
            var expected = (new DataValidationException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithoutForeclosureCaseId(foreclosureCaseSet);
            }
            catch (DataValidationException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }

        [Ignore]
        [TestMethod]
        public void ProcessInsertUpdateWithoutForeclosureCaseId_Success_Existing_AgencyID_CaseNumber()
        {
            TestContext.WriteLine("This test will throw an DataValidationException when AgencyID and CaseNumber existed");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO();
            foreclosureCaseSet.ForeclosureCase = new ForeclosureCaseDTO();
            foreclosureCaseSet.ForeclosureCase.AgencyId = 2;
            foreclosureCaseSet.ForeclosureCase.AgencyCaseNum = "644186";
            var expected = (new DataValidationException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithoutForeclosureCaseId(foreclosureCaseSet);
            }
            catch (DataValidationException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }

        [TestMethod]
        public void ProcessInsertUpdateWithoutForeclosureCaseId_Success()
        {
            TestContext.WriteLine("This test is not implemented yet");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            target.InitiateTransaction();
            target._workingUserID = "HPF";
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO();
            foreclosureCaseSet.ForeclosureCase = new ForeclosureCaseDTO();
            foreclosureCaseSet.ForeclosureCase.AgencyId = 2;
            foreclosureCaseSet.ForeclosureCase.AgencyCaseNum = "644186";
            var expected = (new DataValidationException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithoutForeclosureCaseId(foreclosureCaseSet);
            }
            catch (DataValidationException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
            target.CompleteTransaction();
        }
        #endregion

        #region ProcessInsertForeclosureCaseSet
        [Ignore]
        [TestMethod]
        public void ProcessInsertForeclosureCaseSet_Success_Null_FC_CaseSet()
        {
            TestContext.WriteLine("This test will throw an DataValidationException when ForeclosureCaseSetDTO is null");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = null; // TODO: Initialize to an appropriate value
            var expected = (new DataValidationException()).GetType();
            try
            {
                target.ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (DataValidationException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }            
        }

        private void CheckDuplicate_PreTest()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;

            command.CommandText = "Update foreclosure_case Set Completed_dt = null, Duplicate_IND = 'N' Where fc_id = 23";
            command.ExecuteNonQuery();

            command.CommandText = "Update foreclosure_case Set Completed_dt = '2008-02-17' , Duplicate_IND = 'N' Where fc_id = 4341";
            command.ExecuteNonQuery();

            command.CommandText = "Update foreclosure_case Set Completed_dt = '2008-02-17', Duplicate_IND = 'N' Where fc_id = 123";
            command.ExecuteNonQuery();

            command.CommandText = "Update case_loan set acct_num = '4650801', servicer_id = 5 where fc_id = 123";
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
        private void CheckDuplicate_PostTest()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "Update foreclosure_case Set Completed_dt = '2003-05-15', Duplicate_IND = null Where fc_id = 23 or fc_id = 4341";
            command.ExecuteNonQuery();

            command.CommandText = "Update foreclosure_case Set Completed_dt = '2003-05-05', Duplicate_IND = null Where fc_id = 123";
            command.ExecuteNonQuery();

            command.CommandText = "Update case_loan set acct_num = '1994489', servicer_id = 2 where fc_id = 123";
            command.ExecuteNonQuery();

            dbConnection.Close();
        }
        [Ignore]
        [TestMethod]
        public void ProcessInsertForeclosureCaseSet_Success_Duplicated_FC_Case()
        {
            TestContext.WriteLine("This test will throw an DataValidationException when there is a dupe case");
            CheckDuplicate_PreTest();
            ForeclosureCaseDTO fcCase = new ForeclosureCaseDTO();
            fcCase.FcId = 123;
            
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO(); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase = fcCase;
            var expected = (new DataValidationException()).GetType();
            try
            {
                target.ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (DataValidationException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
            CheckDuplicate_PostTest();
        }

        [Ignore]
        [TestMethod]
        public void ProcessInsertForeclosureCaseSet_Success_MiscException()
        {
            TestContext.WriteLine("This test is not implemented yet");
            CheckDuplicate_PreTest();
            ForeclosureCaseDTO fcCase = new ForeclosureCaseDTO();
            fcCase.FcId = 123;

            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO(); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase = fcCase;
            var expected = (new DataValidationException()).GetType();
            try
            {
                target.ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (DataValidationException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
            CheckDuplicate_PostTest();
        }
        [Ignore]
        [TestMethod]
        public void ProcessInsertForeclosureCaseSet_Success()
        {
            TestContext.WriteLine("This test is not implemented yet");
            CheckDuplicate_PreTest();
            ForeclosureCaseDTO fcCase = new ForeclosureCaseDTO();
            fcCase.FcId = 123;

            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO(); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase = fcCase;
            var expected = (new DataValidationException()).GetType();
            try
            {
                target.ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (DataValidationException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
            CheckDuplicate_PostTest();
        }
        #endregion

        #region ProcessUpdateForeclosureCaseSet
        [Ignore]
        [TestMethod]
        public void ProcessUpdateForeclosureCaseSet_Success_Null_FC_CaseSet()
        {
            TestContext.WriteLine("This test will throw an DataValidationException when ForeclosureCaseSetDTO is null");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = null; // TODO: Initialize to an appropriate value
            var expected = (new DataValidationException()).GetType();
            try
            {
                target.ProcessUpdateForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (DataValidationException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }
        [Ignore]
        [TestMethod]
        public void ProcessUpdateForeclosureCaseSet_Success_MiscException()
        {
            TestContext.WriteLine("This test is not implemented yet");
            CheckDuplicate_PreTest();
            ForeclosureCaseDTO fcCase = new ForeclosureCaseDTO();
            fcCase.FcId = 123;

            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO(); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase = fcCase;
            var expected = (new DataValidationException()).GetType();
            try
            {
                target.ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (DataValidationException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
            CheckDuplicate_PostTest();
        }

        [Ignore]
        [TestMethod]
        public void ProcessUpdateForeclosureCaseSet_Success()
        {
            TestContext.WriteLine("This test is not implemented yet");
            CheckDuplicate_PreTest();
            ForeclosureCaseDTO fcCase = new ForeclosureCaseDTO();
            fcCase.FcId = 123;

            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO(); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase = fcCase;
            var expected = (new DataValidationException()).GetType();
            try
            {
                target.ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (DataValidationException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
            CheckDuplicate_PostTest();
        }
        #endregion

        #region CheckInactiveCase
        /// <summary>
        ///A test for CheckInactiveCase With FcId and CompleteDate
        ///Case True
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckInactiveCaseWithFcIDAndCompleteDateTrue()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value                                               
            ForeclosureCaseDTO fCase = new ForeclosureCaseDTO();
            int fcId = SearchFcCase_GetFcID();
            fCase.FcId = fcId;
            fCase.CompletedDt = Convert.ToDateTime("12/12/2007");
            UpdateForeclosureCase(fCase);
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual = target.CheckInactiveCase(fcId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CheckInactiveCase With FcId and CompleteDate
        ///Case False
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckInactiveCaseWithFcIDAndCompleteDateFalse()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value                                               
            ForeclosureCaseDTO fCase = new ForeclosureCaseDTO();
            //int fcId = SearchFcCase_GetFcID();
            fCase.FcId = fc_id;
            fCase.CompletedDt = Convert.ToDateTime("12/12/2008");
            UpdateForeclosureCase(fCase);
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckInactiveCase(fc_id);            
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CheckInactiveCase with FcId without CompleteDate
        ///Case True
        ///</summary>        
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckInactiveCaseWithFcIDWithoutCompleteDate()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value                                                           
            //int fcId = SearchFcCase_GetFcID();            
            bool expected = false; 
            bool actual = target.CheckInactiveCase(fc_id);
            Assert.AreEqual(expected, actual);         
        }

        /// <summary>
        ///A test for CheckInactiveCase with FcId without CompleteDate
        ///Case True
        ///</summary>
        [TestMethod()]        
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckAgencyId()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseDTO foreclosureCase = SetForeclosureCase("FALSE");
            foreclosureCase.AgencyId = SearchFcCase_GetAgencyID();
            ExceptionMessageCollection actual;            
            actual = target.CheckValidAgencyId(foreclosureCase);            
            ExceptionMessageCollection expected = null; // TODO: Initialize to an appropriate value
            Assert.AreEqual(expected, actual);
        }            
        #endregion

        /// <summary>
        ///A test for RequireFieldsValidation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckBorrowerDOB()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value                        
            DateTime borrowerDob = Convert.ToDateTime("1900/04/02");            
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual = true;
            actual = target.CheckDateOfBirth(borrowerDob);            
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RequireFieldsValidation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckCallId()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value                        
            string callId = "HPF1    ";                        
            bool expected = true;
            bool actual = target.CheckCallID(callId);            
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RequireFieldsValidation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CompleteValidation()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = SetForeclosureCaseSet("FALSE"); // TODO: Initialize to an appropriate value            
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckComplete(foreclosureCaseSet);            
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RequireFieldsValidation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckMaxLengthTest()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = SetForeclosureCaseSet("TRUE"); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase.ChangeUserID = working_user_id;
            foreclosureCaseSet.ForeclosureCase.CallId = "";
            foreclosureCaseSet.ForeclosureCase.BorrowerFname = "123";
            foreclosureCaseSet.ForeclosureCase.PrimResEstMktValue = Convert.ToDouble("999999999999.99");                        
            ExceptionMessageCollection actual;
            actual = target.CheckInvalidFormatData(foreclosureCaseSet);            
            Assert.AreEqual(0, actual.Count);
        }

        
        #region CheckRequireField
        /// <summary>
        ///A test for RequireFieldsValidation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void RequireFieldsValidationPass()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = SetForeclosureCaseSet("TRUE"); // TODO: Initialize to an appropriate value                        
            foreclosureCaseSet.ForeclosureCase.ChangeUserID = working_user_id;
            foreclosureCaseSet.ForeclosureCase.SummarySentOtherCd = "HPF";            
            ExceptionMessageCollection actual;
            actual = target.CheckRequireForPartial(foreclosureCaseSet);            
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for RequireFieldsValidation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void RequireFieldsValidationNotPass()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = SetForeclosureCaseSet("FALSE"); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase.AgencyCaseNum = "";            
            ExceptionMessageCollection expected = null; // TODO: Initialize to an appropriate value
            ExceptionMessageCollection actual;
            actual = target.CheckRequireForPartial(foreclosureCaseSet);            
            Assert.AreNotEqual(expected, actual);
        }
        #endregion

        #region CheckValidCode
        /// <summary>
        ///A test for CheckValidCode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckValidCodeAllPass()
        {            
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = SetForeclosureCaseSet("TRUE");
            foreclosureCaseSet.ForeclosureCase = SetForeclosureCaseCodeTrue();            
            ExceptionMessageCollection actual;
            InsertGeoCodeRef();            
            actual = target.CheckValidCode(foreclosureCaseSet);            
            DeleteGeoCodeRef(GetGeoCodeRefId());
            Assert.AreEqual(0, actual.Count);            
        }

        /// <summary>
        ///A test for CheckValidCode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckValidCodeForeclosureCaseNotPass()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = SetForeclosureCaseSet("TRUE");
            foreclosureCaseSet.ForeclosureCase = SetForeclosureCaseCodeFalse();
            ExceptionMessageCollection expected = null; // TODO: Initialize to an appropriate value
            ExceptionMessageCollection actual;            
            actual = target.CheckValidCode(foreclosureCaseSet);            
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for CheckValidCode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckValidCodeCaseLoanNotPass()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = SetForeclosureCaseSet("TRUE");
            foreclosureCaseSet.CaseLoans = SetCaseLoanCodeFalse();
            ExceptionMessageCollection expected = null; // TODO: Initialize to an appropriate value
            ExceptionMessageCollection actual;            
            actual = target.CheckValidCode(foreclosureCaseSet);            
            Assert.AreNotEqual(expected, actual);
        }
        #endregion
        
        /// <summary>
        ///A test for InsertForeclosureCaseSet
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void InsertForeclosureCaseSetTest()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = SetForeclosureCaseSet("TRUE"); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase.AgencyCaseNum = "Acency Case Num Test";            
            target._workingUserID = working_user_id;
            target.InsertForeclosureCaseSet(foreclosureCaseSet);            
            int fcId = GetForeclosureCaseId();
            string expected = "Acency Case Num Test";
            ForeclosureCaseDTO fcCase = GetForeclosureCase(fcId);
            string actual = fcCase.AgencyCaseNum;
            //DeleteForeclosureCase(fcId);
            Assert.AreEqual(expected, actual);
        }

        [Ignore]
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void ValidationComplete()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseDTO foreclosureCase = SetForeclosureCase("TRUE"); // TODO: Initialize to an appropriate value
            ExceptionMessageCollection actual = null;
            try
            {
                actual = HPFValidator.ValidateToGetExceptionMessage<ForeclosureCaseDTO>(foreclosureCase, Constant.RULESET_LENGTH);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            ExceptionMessageCollection expected = null;
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        ///A test for UpdateForeclosureCaseSet
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void UpdateForeclosureCaseSetTest()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = SetForeclosureCaseSet("TRUE"); // TODO: Initialize to an appropriate value
            target.InitiateTransaction();
            target._workingUserID = "HPF";
            target.InsertForeclosureCaseSet(foreclosureCaseSet);
            target.CompleteTransaction();
            int fcId = GetForeclosureCaseId();
            foreclosureCaseSet.ForeclosureCase.FcId = fcId;
            foreclosureCaseSet.ForeclosureCase.AgencyCaseNum = "Acency Case Num Test";
            #region CaseLoan
            CaseLoanDTOCollection caseLoanCollection = new CaseLoanDTOCollection();
            CaseLoanDTO caseLoanDTO = new CaseLoanDTO();
            caseLoanDTO.FcId = fcId;
            caseLoanDTO.ServicerId = Convert.ToInt32("5");
            caseLoanDTO.AcctNum = "CSC";
            caseLoanDTO.Loan1st2nd = "2st";
            caseLoanDTO.MortgageTypeCd = "11";
            caseLoanDTO.TermLengthCd = "";
            caseLoanDTO.LoanDelinqStatusCd = "30-59";
            caseLoanDTO.InterestRate = 10;
            caseLoanDTO.CreateDate = DateTime.Now;
            caseLoanDTO.CreateUserId = "CSC";
            caseLoanDTO.CreateAppName = "CSC";
            caseLoanDTO.ChangeLastDate = DateTime.Now;
            caseLoanDTO.ChangeLastAppName = "CSC";
            caseLoanDTO.ChangeLastUserId = "CSC";
            caseLoanCollection.Add(caseLoanDTO);
            //2
            caseLoanDTO = new CaseLoanDTO();
            caseLoanDTO.FcId = fcId;
            caseLoanDTO.ServicerId = Convert.ToInt32("5");
            caseLoanDTO.AcctNum = "CSC";
            caseLoanDTO.Loan1st2nd = "1st";
            caseLoanDTO.MortgageTypeCd = "11";
            caseLoanDTO.TermLengthCd = "";
            caseLoanDTO.LoanDelinqStatusCd = "30-59";
            caseLoanDTO.InterestRate = 10;
            caseLoanDTO.CreateDate = DateTime.Now;
            caseLoanDTO.CreateUserId = "CSC";
            caseLoanDTO.CreateAppName = "CSC";
            caseLoanDTO.ChangeLastDate = DateTime.Now;
            caseLoanDTO.ChangeLastAppName = "CSC";
            caseLoanDTO.ChangeLastUserId = "CSC";
            caseLoanCollection.Add(caseLoanDTO);
            foreclosureCaseSet.CaseLoans = caseLoanCollection;
            #endregion
            #region Outcome
            OutcomeItemDTOCollection outcomeItemCollection = new OutcomeItemDTOCollection();
            OutcomeItemDTO outcomeItemDTO = new OutcomeItemDTO();
            outcomeItemDTO.OutcomeTypeId = Convert.ToInt32("1");
            outcomeItemDTO.NonprofitreferralKeyNum = "CSC";
            outcomeItemDTO.ExtRefOtherName = "CSC";
            outcomeItemDTO.OutcomeDt = DateTime.Now;
            outcomeItemDTO.CreateDate = DateTime.Now;
            outcomeItemDTO.CreateUserId = "HPF";
            outcomeItemDTO.CreateAppName = "HPF";
            outcomeItemDTO.ChangeLastDate = DateTime.Now;
            outcomeItemDTO.ChangeLastAppName = "HPF";
            outcomeItemDTO.ChangeLastUserId = "HPF";
            outcomeItemCollection.Add(outcomeItemDTO);
            //2
            outcomeItemDTO = new OutcomeItemDTO();
            outcomeItemDTO.OutcomeTypeId = Convert.ToInt32("1");
            outcomeItemDTO.NonprofitreferralKeyNum = "HPF";
            outcomeItemDTO.ExtRefOtherName = "HPF";
            outcomeItemDTO.OutcomeDt = DateTime.Now;
            outcomeItemDTO.CreateDate = DateTime.Now;
            outcomeItemDTO.CreateUserId = "HPF";
            outcomeItemDTO.CreateAppName = "HPF";
            outcomeItemDTO.ChangeLastDate = DateTime.Now;
            outcomeItemDTO.ChangeLastAppName = "HPF";
            outcomeItemDTO.ChangeLastUserId = "HPF";
            outcomeItemCollection.Add(outcomeItemDTO);
            foreclosureCaseSet.Outcome = outcomeItemCollection;
            #endregion
            #region BudgetItem
            BudgetItemDTOCollection budgetItemCollection = new BudgetItemDTOCollection();
            BudgetItemDTO budgetItemDTO = new BudgetItemDTO();            
            budgetItemDTO.BudgetSubcategoryId = Convert.ToInt32("1");
            budgetItemDTO.BudgetItemAmt = Convert.ToDouble("1000");
            budgetItemDTO.BudgetNote = null;
            budgetItemDTO.CreateDate = DateTime.Now;
            budgetItemDTO.CreateUserId = "CSC";
            budgetItemDTO.CreateAppName = "HPF";
            budgetItemDTO.ChangeLastDate = DateTime.Now;
            budgetItemDTO.ChangeLastAppName = "HPF";
            budgetItemDTO.ChangeLastUserId = "HPF";
            budgetItemCollection.Add(budgetItemDTO);
            //
            budgetItemDTO = new BudgetItemDTO();            
            budgetItemDTO.BudgetSubcategoryId = Convert.ToInt32("8");
            budgetItemDTO.BudgetItemAmt = Convert.ToDouble("300");
            budgetItemDTO.BudgetNote = null;
            budgetItemDTO.CreateDate = DateTime.Now;
            budgetItemDTO.CreateUserId = "HPF";
            budgetItemDTO.CreateAppName = "HPF";
            budgetItemDTO.ChangeLastDate = DateTime.Now;
            budgetItemDTO.ChangeLastAppName = "HPF";
            budgetItemDTO.ChangeLastUserId = "HPF";
            budgetItemCollection.Add(budgetItemDTO);
            foreclosureCaseSet.BudgetItems = budgetItemCollection;
            #endregion
            #region BudgetAsset
            BudgetAssetDTOCollection budgetAssetCollection = new BudgetAssetDTOCollection();
            BudgetAssetDTO budgetAsset = new BudgetAssetDTO();
            budgetAsset.BudgetSetId = Convert.ToInt32("900");
            budgetAsset.AssetName = "CSC";
            budgetAsset.AssetValue = Convert.ToDouble("506");
            budgetAsset.CreateDate = DateTime.Now;
            budgetAsset.CreateUserId = "HPF";
            budgetAsset.CreateAppName = "HPF";
            budgetAsset.ChangeLastDate = DateTime.Now;
            budgetAsset.ChangeLastAppName = "HPF";
            budgetAsset.ChangeLastUserId = "HPF";
            budgetAssetCollection.Add(budgetAsset);
            //2
            budgetAsset = new BudgetAssetDTO();
            budgetAsset.BudgetSetId = Convert.ToInt32("299");
            budgetAsset.AssetName = "CSC";
            budgetAsset.AssetValue = Convert.ToDouble("350");
            budgetAsset.CreateDate = DateTime.Now;
            budgetAsset.CreateUserId = "HPF";
            budgetAsset.CreateAppName = "HPF";
            budgetAsset.ChangeLastDate = DateTime.Now;
            budgetAsset.ChangeLastAppName = "HPF";
            budgetAsset.ChangeLastUserId = "HPF";
            budgetAssetCollection.Add(budgetAsset);
            #endregion
            target.InitiateTransaction();
            target.UpdateForeclosureCaseSet(foreclosureCaseSet);
            target.CompleteTransaction();
            string expected = "Acency Case Num Test";
            ForeclosureCaseDTO fcCase = GetForeclosureCase(fcId);
            string actual = fcCase.AgencyCaseNum;
            DeleteForeclosureCase(fcId);
            Assert.AreEqual(expected, actual);
        }        

        /// <summary>
        ///A test for MiscErrorException
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void MiscErrorExceptionTest()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = SetForeclosureCaseSet("TRUE"); // TODO: Initialize to an appropriate value
            ExceptionMessageCollection expected = null; // TODO: Initialize to an appropriate value
            ExceptionMessageCollection actual;
            target.WarningMessage = new ExceptionMessageCollection();            
            actual = target.MiscErrorException(foreclosureCaseSet);            
            Assert.AreEqual(0, actual.Count);
        }

        #region Data test ForeclosureCaseSet
        private ForeclosureCaseDTO SetForeclosureCase(string status)
        {            
            ForeclosureCaseDTO foreclosureCase = new ForeclosureCaseDTO();
            foreclosureCase.AgencyId = Convert.ToInt32("2");
            foreclosureCase.ProgramId = Convert.ToInt32("1");
            foreclosureCase.AgencyCaseNum = "Test";
            foreclosureCase.IntakeDt = Convert.ToDateTime("12/12/2007");
            foreclosureCase.CaseSourceCd = "HPF";
            foreclosureCase.BorrowerFname = "Test";
            foreclosureCase.BorrowerLname = "Test";
            foreclosureCase.PrimaryContactNo = "Y";
            foreclosureCase.ContactAddr1 = "Test";
            foreclosureCase.ContactCity = "Test";            
            foreclosureCase.ContactZip = "12345";
            foreclosureCase.PropAddr1 = "Test";
            foreclosureCase.PropCity = "Test";            
            foreclosureCase.PropZip = "12345";
            foreclosureCase.OwnerOccupiedInd = "Y";
            foreclosureCase.FundingConsentInd = "Y";
            foreclosureCase.ServicerConsentInd = "Y";
            foreclosureCase.AssignedCounselorIdRef = "Test";
            foreclosureCase.CounselorFname = "Test";
            foreclosureCase.CounselorLname = "Test";
            foreclosureCase.CounselorEmail = "Test";
            foreclosureCase.CounselorPhone = "Test";
            foreclosureCase.OptOutNewsletterInd = "N";
            foreclosureCase.DoNotCallInd = "N";
            foreclosureCase.PrimaryResidenceInd = "Y";
            foreclosureCase.CreateDate = DateTime.Now;
            foreclosureCase.CreateUserId = "HPF";
            foreclosureCase.CreateAppName = "HPF";
            foreclosureCase.ChangeLastDate = DateTime.Now;
            foreclosureCase.ChangeLastAppName = "HPF";
            foreclosureCase.ChangeLastUserId = "HPF";
            foreclosureCase.AgencyClientNum = "HPF";
            foreclosureCase.ContactStateCd = "HPF";
            foreclosureCase.PropStateCd = "HPF";
            foreclosureCase.GenderCd = "ABC";
            if (status == "TRUE")
            {
                //foreclosureCase.CompletedDt = Convert.ToDateTime("12/12/2007");
            }
            else
            {
                foreclosureCase.BankruptcyInd = "Y";
            }
            return foreclosureCase;
        }

        private BudgetAssetDTOCollection SetBudgetAssetCollection(string status)
        {
            BudgetAssetDTOCollection budgetAssetCollection = new BudgetAssetDTOCollection();
            if (status == "TRUE")
            {
                for (int i = 0; i < 4; i++)
                {
                    BudgetAssetDTO budgetAsset = new BudgetAssetDTO();
                    budgetAsset.BudgetSetId = Convert.ToInt32("76156");
                    budgetAsset.AssetName = "01234567890123456789012345678901234567890123456789";
                    budgetAsset.AssetValue = Convert.ToDouble("30.65");
                    budgetAsset.CreateDate = DateTime.Now;
                    budgetAsset.CreateUserId = "HPF";
                    budgetAsset.CreateAppName = "HPF";
                    budgetAsset.ChangeLastDate = DateTime.Now;
                    budgetAsset.ChangeLastAppName = "HPF";
                    budgetAsset.ChangeLastUserId = "HPF";
                    budgetAssetCollection.Add(budgetAsset);
                }
            }
            else
            {
                BudgetAssetDTO budgetAsset = new BudgetAssetDTO();
                budgetAsset.BudgetSetId = Convert.ToInt32("76156");
                budgetAsset.AssetName = "Test";
                budgetAsset.AssetValue = Convert.ToDouble("30.65");
                budgetAsset.CreateDate = DateTime.Now;
                budgetAsset.CreateUserId = "HPF";
                budgetAsset.CreateAppName = "HPF";
                budgetAsset.ChangeLastDate = DateTime.Now;
                budgetAsset.ChangeLastAppName = "HPF";
                budgetAsset.ChangeLastUserId = "HPF";
                budgetAssetCollection.Add(budgetAsset);
            }
            return budgetAssetCollection;
        }

        private BudgetItemDTOCollection SetBudgetItemCollection(string status)
        {
            BudgetItemDTOCollection budgetItemCollection = new BudgetItemDTOCollection();
            if (status == "TRUE")
            {
                BudgetItemDTO budgetItemDTO = new BudgetItemDTO();
                //budgetItemDTO.BudgetSetId = Convert.ToInt32("76156");
                budgetItemDTO.BudgetSubcategoryId = Convert.ToInt32("1");
                budgetItemDTO.BudgetItemAmt = Convert.ToDouble("900.08");
                budgetItemDTO.BudgetNote = null;
                budgetItemDTO.CreateDate = DateTime.Now;
                budgetItemDTO.CreateUserId = "HPF";
                budgetItemDTO.CreateAppName = "HPF";
                budgetItemDTO.ChangeLastDate = DateTime.Now;
                budgetItemDTO.ChangeLastAppName = "HPF";
                budgetItemDTO.ChangeLastUserId = "HPF";
                budgetItemCollection.Add(budgetItemDTO);
                //
                budgetItemDTO = new BudgetItemDTO();
                //budgetItemDTO.BudgetSetId = Convert.ToInt32("76156");
                budgetItemDTO.BudgetSubcategoryId = Convert.ToInt32("8");
                budgetItemDTO.BudgetItemAmt = Convert.ToDouble("300.05");
                budgetItemDTO.BudgetNote = null;
                budgetItemDTO.CreateDate = DateTime.Now;
                budgetItemDTO.CreateUserId = "HPF";
                budgetItemDTO.CreateAppName = "HPF";
                budgetItemDTO.ChangeLastDate = DateTime.Now;
                budgetItemDTO.ChangeLastAppName = "HPF";
                budgetItemDTO.ChangeLastUserId = "HPF";
                budgetItemCollection.Add(budgetItemDTO);
            }
            else
            {
                BudgetItemDTO budgetItemDTO = new BudgetItemDTO();
                budgetItemDTO.BudgetSetId = Convert.ToInt32("76156");
                budgetItemDTO.BudgetSubcategoryId = Convert.ToInt32("1");
                budgetItemDTO.BudgetItemAmt = 900.08;
                budgetItemDTO.BudgetNote = null;
                budgetItemDTO.CreateDate = DateTime.Now;
                budgetItemDTO.CreateUserId = "HPF";
                budgetItemDTO.CreateAppName = "HPF";
                budgetItemDTO.ChangeLastDate = DateTime.Now;
                budgetItemDTO.ChangeLastAppName = "HPF";
                budgetItemDTO.ChangeLastUserId = "HPF";
                budgetItemCollection.Add(budgetItemDTO);
                //
                budgetItemDTO = new BudgetItemDTO();
                budgetItemDTO.BudgetSetId = Convert.ToInt32("76156");
                //budgetItemDTO.BudgetSubcategoryId = Convert.ToInt32("8");
                budgetItemDTO.BudgetItemAmt = 300.05;
                budgetItemDTO.BudgetNote = null;
                budgetItemDTO.CreateDate = DateTime.Now;
                budgetItemDTO.CreateUserId = "HPF";
                budgetItemDTO.CreateAppName = "HPF";
                budgetItemDTO.ChangeLastDate = DateTime.Now;
                budgetItemDTO.ChangeLastAppName = "HPF";
                budgetItemDTO.ChangeLastUserId = "HPF";
                budgetItemCollection.Add(budgetItemDTO);
            }
            return budgetItemCollection;
        }

        private OutcomeItemDTOCollection SetOutcomeItemCollection(string status)
        {
            OutcomeItemDTOCollection outcomeItemCollection = new OutcomeItemDTOCollection();
            if (status == "TRUE")
            {
                for (int i = 0; i < 1; i++)
                {
                    OutcomeItemDTO outcomeItemDTO = new OutcomeItemDTO();
                    outcomeItemDTO.OutcomeTypeId = Convert.ToInt32("1");
                    outcomeItemDTO.NonprofitreferralKeyNum = "Test";
                    outcomeItemDTO.ExtRefOtherName = "Test";
                    outcomeItemDTO.OutcomeDt = DateTime.Now;
                    outcomeItemDTO.CreateDate = DateTime.Now;
                    outcomeItemDTO.CreateUserId = "HPF";
                    outcomeItemDTO.CreateAppName = "HPF";
                    outcomeItemDTO.ChangeLastDate = DateTime.Now;
                    outcomeItemDTO.ChangeLastAppName = "HPF";
                    outcomeItemDTO.ChangeLastUserId = "HPF";
                    outcomeItemCollection.Add(outcomeItemDTO);
                }
            }
            else
            {
                for (int i = 0; i < 1; i++)
                {
                    OutcomeItemDTO outcomeItemDTO = new OutcomeItemDTO();                                        
                    outcomeItemDTO.NonprofitreferralKeyNum = "Test";
                    outcomeItemDTO.OutcomeTypeId = Convert.ToInt32("1");
                    outcomeItemDTO.ExtRefOtherName = "Test";
                    outcomeItemDTO.OutcomeDt = DateTime.Now;
                    outcomeItemDTO.CreateDate = DateTime.Now;
                    outcomeItemDTO.CreateUserId = "HPF";
                    outcomeItemDTO.CreateAppName = "HPF";
                    outcomeItemDTO.ChangeLastDate = DateTime.Now;
                    outcomeItemDTO.ChangeLastAppName = "HPF";
                    outcomeItemDTO.ChangeLastUserId = "HPF";
                    outcomeItemCollection.Add(outcomeItemDTO);
                }
            }
            return outcomeItemCollection;
        }

        private CaseLoanDTOCollection SetCaseLoanCollection(string status)
        {
            CaseLoanDTOCollection caseLoanCollection = new CaseLoanDTOCollection();
            if (status == "TRUE")
            {
                for (int i = 0; i < 1; i++)
                {
                    CaseLoanDTO caseLoanDTO = new CaseLoanDTO();
                    caseLoanDTO.ServicerId = Convert.ToInt32("5");
                    caseLoanDTO.AcctNum = "HPF";
                    caseLoanDTO.Loan1st2nd = "1st";
                    caseLoanDTO.MortgageTypeCd = "";                    
                    caseLoanDTO.TermLengthCd = "";
                    caseLoanDTO.LoanDelinqStatusCd = "30-59";
                    caseLoanDTO.InterestRate = 10;
                    caseLoanDTO.CreateDate = DateTime.Now;
                    caseLoanDTO.CreateUserId = "HPF";
                    caseLoanDTO.CreateAppName = "HPF";
                    caseLoanDTO.ChangeLastDate = DateTime.Now;
                    caseLoanDTO.ChangeLastAppName = "HPF";
                    caseLoanDTO.ChangeLastUserId = "HPF";
                    caseLoanCollection.Add(caseLoanDTO);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    CaseLoanDTO caseLoanDTO = new CaseLoanDTO();
                    caseLoanDTO.ServicerId = Convert.ToInt32("5");
                    caseLoanDTO.AcctNum = "Test";
                    //caseLoanDTO.Loan1st2nd = "1st";
                    caseLoanDTO.MortgageTypeCd = "HPF";                    
                    caseLoanDTO.TermLengthCd = "HPF";
                    caseLoanDTO.LoanDelinqStatusCd = "30-59";
                    caseLoanDTO.InterestRate = 1;
                    caseLoanDTO.CreateDate = DateTime.Now;
                    caseLoanDTO.CreateUserId = "HPF";
                    caseLoanDTO.CreateAppName = "HPF";
                    caseLoanDTO.ChangeLastDate = DateTime.Now;
                    caseLoanDTO.ChangeLastAppName = "HPF";
                    caseLoanDTO.ChangeLastUserId = "HPF";
                    caseLoanCollection.Add(caseLoanDTO);
                }
            }
            return caseLoanCollection;
        }

        private BudgetSetDTO SetBudgetSet(string status)
        {
            BudgetSetDTO budgetSetDTO = new BudgetSetDTO();
            if (status == "TRUE")
            {
                budgetSetDTO.BudgetSetDt = Convert.ToDateTime("12/12/2009");
                budgetSetDTO.CreateDate = DateTime.Now;
                budgetSetDTO.CreateUserId = "HPF";
                budgetSetDTO.CreateAppName = "HPF";
                budgetSetDTO.ChangeLastDate = DateTime.Now;
                budgetSetDTO.ChangeLastAppName = "HPF";
                budgetSetDTO.ChangeLastUserId = "HPF";
            }
            else 
            {
                budgetSetDTO.BudgetSetDt = Convert.ToDateTime("12/12/2009");
                budgetSetDTO.CreateDate = DateTime.Now;
                budgetSetDTO.CreateUserId = "HPF";
                budgetSetDTO.CreateAppName = "HPF";
                budgetSetDTO.ChangeLastDate = DateTime.Now;
                budgetSetDTO.ChangeLastAppName = "HPF";
                budgetSetDTO.ChangeLastUserId = "HPF";
            }
            return budgetSetDTO;
        }

        private ActivityLogDTOCollection SetActivityLogCollection(string status)
        {
            ActivityLogDTOCollection activityLogCollection = new ActivityLogDTOCollection();
            if (status == "TRUE")
            {
                for (int i = 0; i < 2; i++)
                {
                    ActivityLogDTO activityLog = new ActivityLogDTO();
                    activityLog.ActivityCd = "Test";
                    activityLog.ActivityDt = DateTime.Now;
                    activityLog.CreateDate = DateTime.Now;
                    activityLog.CreateUserId = "HPF";
                    activityLog.CreateAppName = "HPF";
                    activityLog.ChangeLastDate = DateTime.Now;
                    activityLog.ChangeLastAppName = "HPF";
                    activityLog.ChangeLastUserId = "HPF";
                    activityLogCollection.Add(activityLog);
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    ActivityLogDTO activityLog = new ActivityLogDTO();                   
                    activityLog.CreateDate = DateTime.Now;
                    activityLog.CreateUserId = "HPF";
                    activityLog.CreateAppName = "HPF";
                    activityLog.ChangeLastDate = DateTime.Now;
                    activityLog.ChangeLastAppName = "HPF";
                    activityLog.ChangeLastUserId = "HPF";
                    activityLogCollection.Add(activityLog);
                }
            }
            return activityLogCollection;
        }

        private ForeclosureCaseSetDTO SetForeclosureCaseSet(string status)
        {            
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO();
            //foreclosureCaseSet.ChangeUserID = "test working uID";
            foreclosureCaseSet.ForeclosureCase = SetForeclosureCase(status);
            foreclosureCaseSet.CaseLoans = SetCaseLoanCollection(status);
            foreclosureCaseSet.Outcome = SetOutcomeItemCollection(status);
            foreclosureCaseSet.BudgetSet = SetBudgetSet(status);
            foreclosureCaseSet.BudgetItems = SetBudgetItemCollection(status);
            foreclosureCaseSet.BudgetAssets = SetBudgetAssetCollection(status);
            foreclosureCaseSet.ActivityLog = SetActivityLogCollection(status);
            //foreclosureCaseSet.ChangeUserID = "HPF";
            return foreclosureCaseSet;
        }

        private ForeclosureCaseDTO SetForeclosureCaseCodeTrue()
        {
            ForeclosureCaseDTO foreclosureCase = new ForeclosureCaseDTO();
            foreclosureCase.IncomeEarnersCd = "1";
            foreclosureCase.CaseSourceCd = "Faith Community";
            foreclosureCase.RaceCd = "2";
            foreclosureCase.HouseholdCd = "SINGL";            
            foreclosureCase.DfltReason1stCd = "17";
            foreclosureCase.DfltReason2ndCd = "18";
            foreclosureCase.HudTerminationReasonCd = "1";
            foreclosureCase.HudOutcomeCd = "3";
            foreclosureCase.CounselingDurationCd = "<30 minutes";            
            foreclosureCase.SummarySentOtherCd = "Phone";            
            foreclosureCase.MilitaryServiceCd = "ACTV";
            foreclosureCase.ContactZip = "12345";
            foreclosureCase.PropZip = "12345";
            foreclosureCase.ContactStateCd = "AL";
            foreclosureCase.PropStateCd = "AL";
            foreclosureCase.ProgramId = 101;
            foreclosureCase.AgencyId = 2;
            return foreclosureCase;
        }

        private ForeclosureCaseDTO SetForeclosureCaseCodeFalse()
        {
            ForeclosureCaseDTO foreclosureCase = new ForeclosureCaseDTO();
            foreclosureCase.IncomeEarnersCd = "ABC";
            foreclosureCase.CaseSourceCd = "Faith Community";
            foreclosureCase.RaceCd = "2";
            foreclosureCase.HouseholdCd = "123";
            foreclosureCase.DfltReason1stCd = "17";
            foreclosureCase.DfltReason2ndCd = "18";
            foreclosureCase.HudTerminationReasonCd = "1";
            foreclosureCase.HudOutcomeCd = "3";
            foreclosureCase.CounselingDurationCd = "HPF";
            foreclosureCase.SummarySentOtherCd = "Phone";
            foreclosureCase.MilitaryServiceCd = "ACTV";
            foreclosureCase.ContactZip = "1145";
            foreclosureCase.PropZip ="1145";
            return foreclosureCase;
        }

        private CaseLoanDTOCollection SetCaseLoanCodeFalse()
        {
            CaseLoanDTOCollection caseLoanCollection = new CaseLoanDTOCollection();                        
            CaseLoanDTO caseLoanDTO = new CaseLoanDTO();
            caseLoanDTO.MortgageTypeCd = "ARM";                
            caseLoanDTO.TermLengthCd = "10";
            caseLoanDTO.LoanDelinqStatusCd = "30-59";                
            caseLoanCollection.Add(caseLoanDTO);
            caseLoanDTO = new CaseLoanDTO();
            caseLoanDTO.MortgageTypeCd = "FIXED";
            caseLoanDTO.TermLengthCd = "5";
            caseLoanDTO.LoanDelinqStatusCd = "ABC";
            caseLoanCollection.Add(caseLoanDTO);    
            return caseLoanCollection;
        }
        #endregion

        #region Utility



        static private int GetForeclosureCaseId()
        {
            int result = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand("SELECT MAX(Fc_id) as fc_id FROM foreclosure_case", dbConnection);
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
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
            return result;
        }

        static private string GetAgencyCaseNum(int fcId)
        {
            string result = string.Empty;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand("SELECT agency_case_num FROM foreclosure_case WHERE fc_id =" + fcId + "", dbConnection);
            dbConnection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    result = reader["agency_case_num"].ToString();
                }
                catch (Exception ex)
                {
                    dbConnection.Close();
                }
                break;
            }
            dbConnection.Close();

            return result;
        }
               
        static private int GetBudgetSetId(int fcId)
        {
            int result = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand("SELECT MAX(budget_set_id) as budget_set_id FROM budget_set WHERE fc_id =" + fcId + "", dbConnection);
            dbConnection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    result = int.Parse(reader["budget_set_id"].ToString());
                }
                catch (Exception ex)
                {
                    dbConnection.Close();
                }
                break;
            }
            dbConnection.Close();

            return result;
        }

        static private void UpdateForeclosureCase(ForeclosureCaseDTO fCase)
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            int? fcId = fCase.FcId;
            DateTime? completeDt = fCase.CompletedDt;
            dbConnection.Open();
            try
            {
                var command = new SqlCommand("UPDATE foreclosure_case SET completed_dt = '" + completeDt + "' WHERE fc_id = " + fcId + "", dbConnection);
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
        }

        static private void UpdateForeclosureCaseDateMin(ForeclosureCaseDTO fCase)
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            int? fcId = fCase.FcId;            
            dbConnection.Open();
            try
            {
                var command = new SqlCommand("UPDATE foreclosure_case SET completed_dt = 1/1/0001  WHERE fc_id = " + fcId + "", dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
        }

        static private void InsertGeoCodeRef()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);          
            dbConnection.Open();
            try
            {
                var command = new SqlCommand("INSERT INTO geocode_ref(zip_code, state_abbr, zip_type) VALUES ('12345', 'AL', 'Y')", dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
        }

        static private int GetGeoCodeRefId()
        {
            int result = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand("SELECT geocode_ref_id FROM geocode_ref WHERE zipcode = '12345' AND state_abbr = 'TE' AND zip_type = 'Y'", dbConnection);
            dbConnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = int.Parse(reader["budget_set_id"].ToString());
                }                
            }
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
            return result;
        }

        static private int DeleteGeoCodeRef(int refId)
        {
            int result = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand("DELETE FROM geocode_ref WHERE geocode_ref_id = " + refId + "", dbConnection);
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
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
            return result;
        }
        
        static private void DeleteForeclosureCase(int fcId)
        {            
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            int bsId = GetBudgetSetId(fcId);
            dbConnection.Open();
            try
            {
                var command = new SqlCommand("DELETE FROM Budget_Asset WHERE budget_set_id =" + bsId + "", dbConnection);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM Budget_Item WHERE budget_set_id =" + bsId + "", dbConnection);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM Budget_Set WHERE fc_id =" + fcId + "", dbConnection);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM Case_Loan WHERE fc_id =" + fcId + "", dbConnection);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM Outcome_Item WHERE fc_id =" + fcId + "", dbConnection);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM Activity_log WHERE fc_id =" + fcId + "", dbConnection);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM foreclosure_case WHERE fc_id =" + fcId + "", dbConnection);
                command.ExecuteNonQuery();
            }
            catch 
            {
                dbConnection.Close();            
            }
            dbConnection.Close();
        }

        public ForeclosureCaseDTO GetForeclosureCase(int fcId)
        {
            ForeclosureCaseDTO returnObject = new ForeclosureCaseDTO();
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            try
            {
                var command = new SqlCommand("SELECT * FROM foreclosure_case WHERE fc_id =" + fcId + "", dbConnection);                

                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        #region set ForeclosureCase value
                        returnObject.FcId = int.Parse(reader["fc_id"].ToString());
                        returnObject.ActionItemsNotes = (reader["action_items_notes"].ToString());
                        returnObject.AgencyCaseNum = (reader["agency_case_num"].ToString());
                        returnObject.AgencyClientNum = (reader["agency_client_num"].ToString());
                        returnObject.AgencyId = int.Parse(reader["agency_id"].ToString());
                        returnObject.AgencyMediaInterestInd = (reader["agency_media_interest_ind"].ToString());
                        returnObject.AgencySuccessStoryInd = (reader["agency_success_story_ind"].ToString());
                        returnObject.AmiPercentage = int.Parse(reader["AMI_percentage"].ToString());
                        returnObject.AssignedCounselorIdRef = (reader["counselor_id_ref"].ToString());
                        returnObject.BankruptcyAttorney = (reader["bankruptcy_attorney"].ToString());
                        returnObject.BankruptcyInd = (reader["bankruptcy_ind"].ToString());
                        returnObject.BankruptcyPmtCurrentInd = (reader["bankruptcy_pmt_current_ind"].ToString());
                        returnObject.BorrowerDisabledInd = (reader["borrower_disabled_ind"].ToString());                        
                        returnObject.BorrowerEducLevelCompletedCd = (reader["borrower_educ_level_completed_cd"].ToString());
                        returnObject.BorrowerFname = (reader["borrower_fname"].ToString());
                        returnObject.BorrowerLname = (reader["borrower_lname"].ToString());
                        returnObject.BorrowerMaritalStatusCd = (reader["borrower_marital_status_cd"].ToString());
                        returnObject.BorrowerLast4Ssn = (reader["borrower_last4_SSN"].ToString());
                        returnObject.BorrowerMname = (reader["borrower_mname"].ToString());
                        returnObject.BorrowerOccupationCd = (reader["borrower_occupation"].ToString());
                        returnObject.BorrowerPreferredLangCd = (reader["borrower_preferred_lang_cd"].ToString());
                        //returnObject.BorrowerSsn = (reader["borrower_ssn"].ToString());
                        returnObject.CallId = (reader["call_id"].ToString());                        
                        returnObject.ChangeLastAppName = (reader["chg_lst_app_name"].ToString());                        
                        returnObject.ChangeLastUserId = (reader["chg_lst_user_id"].ToString());
                        returnObject.CoBorrowerDisabledInd = (reader["co_borrower_disabled_ind"].ToString());                        
                        returnObject.CoBorrowerFname = (reader["co_borrower_fname"].ToString());
                        returnObject.CoBorrowerLname = (reader["co_borrower_lname"].ToString());
                        returnObject.CoBorrowerLast4Ssn = (reader["co_borrower_last4_SSN"].ToString());
                        returnObject.CoBorrowerMname = (reader["co_borrower_mname"].ToString());
                        returnObject.CoBorrowerOccupationCd = (reader["co_borrower_occupation"].ToString());
                        //returnObject.CoBorrowerSsn = (reader["co_borrower_ssn"].ToString());                        
                        returnObject.ContactAddr1 = (reader["contact_addr1"].ToString());
                        returnObject.ContactAddr2 = (reader["contact_addr2"].ToString());
                        returnObject.ContactCity = (reader["contact_city"].ToString());
                        returnObject.ContactedSrvcrRecentlyInd = (reader["contacted_srvcr_recently_ind"].ToString());
                        returnObject.ContactStateCd = (reader["contact_state_cd"].ToString());
                        returnObject.ContactZip = (reader["contact_zip"].ToString());
                        returnObject.ContactZipPlus4 = (reader["contact_zip_plus4"].ToString());
                        returnObject.CounselingDurationCd = (reader["counseling_duration_cd"].ToString());
                        returnObject.CounselorFname = (reader["counselor_fname"].ToString());
                        returnObject.CounselorLname = (reader["counselor_lname"].ToString());
                        returnObject.CounselorPhone = (reader["counselor_phone"].ToString());
                        returnObject.CounselorExt = (reader["counselor_ext"].ToString());
                        returnObject.CounselorEmail = (reader["counselor_email"].ToString());
                        returnObject.CreateAppName = (reader["create_app_name"].ToString());                        
                        returnObject.CreateUserId = (reader["create_user_id"].ToString());
                        returnObject.DfltReason1stCd = (reader["dflt_reason_1st_cd"].ToString());
                        returnObject.DfltReason2ndCd = (reader["dflt_reason_2nd_cd"].ToString());
                        returnObject.DiscussedSolutionWithSrvcrInd = (reader["discussed_solution_with_srvcr_ind"].ToString());
                        returnObject.DoNotCallInd = (reader["do_not_call_ind"].ToString());
                        returnObject.DuplicateInd = (reader["duplicate_ind"].ToString());
                        returnObject.Email1 = (reader["email_1"].ToString());
                        returnObject.Email2 = (reader["email_2"].ToString());
                        
                        returnObject.FcNoticeReceiveInd = (reader["fc_notice_received_ind"].ToString());
                        returnObject.FollowupNotes = (reader["followup_notes"].ToString());
                        returnObject.ForSaleInd = (reader["for_sale_ind"].ToString());
                        returnObject.FundingConsentInd = (reader["funding_consent_ind"].ToString());
                        returnObject.GenderCd = (reader["gender_cd"].ToString());
                        returnObject.HasWorkoutPlanInd = (reader["has_workout_plan_ind"].ToString());
                        returnObject.HispanicInd = (reader["hispanic_ind"].ToString());
                        if (!string.IsNullOrEmpty(reader["home_current_market_value"].ToString()))
                            returnObject.HomeCurrentMarketValue = double.Parse(reader["home_current_market_value"].ToString());
                        if (!string.IsNullOrEmpty(reader["home_purchase_price"].ToString()))
                            returnObject.HomePurchasePrice = double.Parse(reader["home_purchase_price"].ToString());
                        if (!string.IsNullOrEmpty(reader["home_purchase_year"].ToString()))
                            returnObject.HomePurchaseYear = int.Parse(reader["home_purchase_year"].ToString());
                        if (!string.IsNullOrEmpty(reader["home_sale_price"].ToString()) )
                            returnObject.HomeSalePrice = double.Parse(reader["home_sale_price"].ToString());
                        returnObject.HouseholdCd = (reader["household_cd"].ToString());
                        returnObject.HpfMediaCandidateInd = (reader["hpf_media_candidate_ind"].ToString());                        
                        returnObject.HpfSuccessStoryInd = (reader["hpf_success_story_ind"].ToString());
                        returnObject.HudOutcomeCd = (reader["hud_outcome_cd"].ToString());                        
                        returnObject.HudTerminationReasonCd = (reader["hud_termination_reason_cd"].ToString());
                        returnObject.IncomeEarnersCd = (reader["income_earners_cd"].ToString());                        
                        returnObject.LoanDfltReasonNotes = (reader["loan_dflt_reason_notes"].ToString());
                        returnObject.MilitaryServiceCd = (reader["military_service_cd"].ToString());
                        returnObject.MotherMaidenLname = (reader["mother_maiden_lname"].ToString());
                        returnObject.NeverBillReasonCd = (reader["never_bill_reason_cd"].ToString());
                        returnObject.NeverPayReasonCd = (reader["never_pay_reason_cd"].ToString());
                        if (!string.IsNullOrEmpty(reader["occupant_num"].ToString())) 
                            returnObject.OccupantNum = byte.Parse(reader["occupant_num"].ToString());
                        returnObject.OptOutNewsletterInd = (reader["opt_out_newsletter_ind"].ToString());
                        returnObject.OptOutSurveyInd = (reader["opt_out_survey_ind"].ToString());
                        returnObject.OwnerOccupiedInd = (reader["owner_occupied_ind"].ToString());
                        returnObject.PrimaryContactNo = (reader["primary_contact_no"].ToString());
                        returnObject.PrimaryResidenceInd = (reader["primary_residence_ind"].ToString());
                        if (!string.IsNullOrEmpty(reader["prim_res_est_mkt_value"].ToString()) )
                            returnObject.PrimResEstMktValue = double.Parse(reader["prim_res_est_mkt_value"].ToString());
                        if (!string.IsNullOrEmpty(reader["program_id"].ToString()))
                            returnObject.ProgramId = int.Parse(reader["program_id"].ToString());
                        returnObject.PropAddr1 = (reader["prop_addr1"].ToString());
                        returnObject.PropAddr2 = (reader["prop_addr2"].ToString());
                        returnObject.PropCity = (reader["prop_city"].ToString());
                        returnObject.PropStateCd = (reader["prop_state_cd"].ToString());
                        returnObject.PropZip = (reader["prop_zip"].ToString());
                        returnObject.PropertyCd = (reader["property_cd"].ToString());
                        returnObject.PropZipPlus4 = (reader["prop_zip_plus_4"].ToString());
                        returnObject.RaceCd = (reader["race_cd"].ToString());
                        returnObject.RealtyCompany = (reader["realty_company"].ToString());
                        returnObject.SecondContactNo = (reader["second_contact_no"].ToString());
                        returnObject.ServicerConsentInd = (reader["servicer_consent_ind"].ToString());
                        returnObject.SrvcrWorkoutPlanCurrentInd = (reader["srvcr_workout_plan_current_ind"].ToString());                        
                        returnObject.SummarySentOtherCd = (reader["summary_sent_other_cd"].ToString());                        
                        returnObject.WorkedWithAnotherAgencyInd = (reader["worked_with_another_agency_ind"].ToString());
                        #endregion
                    }
                    reader.Close();
                }
                else
                    returnObject = null;

            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return returnObject;
        }
        #endregion
        
        #region AppForeclosureCaseSearch Test
        /// <summary>
        ///A test for Empty Search Criteria provided by User
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        [ExpectedException(typeof(DataValidationException))]
        public void EmptySearchCriteriaProvided()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
            //empty Search criteria, default value of interger is -1 and string is null
            AppForeclosureCaseSearchCriteriaDTO criteria = new AppForeclosureCaseSearchCriteriaDTO { Agency = -1, ForeclosureCaseID = -1, Program = -1 };
            target.AppSearchforeClosureCase(criteria);
        }
        #endregion
    }
}
