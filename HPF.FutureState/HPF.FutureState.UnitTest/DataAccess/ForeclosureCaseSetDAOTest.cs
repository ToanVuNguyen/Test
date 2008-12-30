using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Data.SqlClient;
using System.Configuration;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;


using System.Collections.Generic;

namespace HPF.FutureState.UnitTest.DataAccess
{
    
    
    /// <summary>
    ///This is a test class for ForeclosureCaseSetDAOTest and is intended
    ///to contain all ForeclosureCaseSetDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ForeclosureCaseSetDAOTest
    {

        string[][] criterias;
        List<int> expected;
        //t[] results;
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "update foreclosure_case" +
                                    " set loan_list = 'abc123, abc124, def123, def1234'" +
                                    ", prop_zip = '66666'" +
                                    ", borrower_last4_SSN = '1234'" +
                                    " where fc_id in (23, 181, 183, 185) ";
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand();
            dbConnection.Open();
            command.Connection = dbConnection;
            command.CommandText = "update foreclosure_case" +
                                    " set loan_list = null" +
                                    ", prop_zip = null" +
                                    ", borrower_last4_SSN = null" +
                                    " where fc_id in (23, 181, 183, 185)";
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {            
    
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        #region SearchForeclosureCase - Test

        /// <summary>
        ///A test for SearchForeclosureCase
        ///</summary>
        [TestMethod()]
        public void SearchForeclosureCaseTest()
        {
            ForeclosureCaseDAO_Accessor target = new ForeclosureCaseDAO_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO() { PropertyZip = "66666" };

            List<int> expected = new List<int>();            
            expected.Add(23);
            expected.Add(181);
            expected.Add(183);
            expected.Add(185);

            List<int> actual = new List<int>();
            ForeclosureCaseSearchResult actualResult = target.SearchForeclosureCase(searchCriteria, 50);
            foreach (ForeclosureCaseWSDTO wscase in actualResult)
            {
                actual.Add(wscase.FcId);
            }
            CollectionAssert.AreEquivalent(expected, actual);
            
        }

        private static List<int> GetListFCID(ForeclosureCaseSearchCriteriaDTO searchCriteria)
        {
            List<int> expected = new List<int>();
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand();
            dbConnection.Open();
            command.Connection = dbConnection;
            command.CommandText = " Select fc_id from foreclosure_case where prop_zip = '" + searchCriteria.PropertyZip + "'";
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    expected.Add(reader.GetInt32(0));
                }
            }
            reader.Close();
            dbConnection.Close();
            return expected;
        }
        
        #endregion


        #region GetForeclosureCase - Test
        /// <summary>
        ///A test for GetForeclosureCase
        ///</summary>
        [TestMethod()]
        public void GetForeclosureCaseTest_Success()
        {
            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value
            int fc_id = 23; // TODO: Initialize to an appropriate value
            int expected = 23; // TODO: Initialize to an appropriate value
            int actual = target.GetForeclosureCase(fc_id).FcId;            
            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod()]
        public void GetForeclosureCaseTest_Fail()
        {
            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value
            int fc_id = 23; // TODO: Initialize to an appropriate value
            int expected = 24; // TODO: Initialize to an appropriate value
            int actual = target.GetForeclosureCase(fc_id).FcId;
            Assert.AreNotEqual(expected, actual);

        }

        [TestMethod()]
        public void GetForeclosureCaseTest_Null()
        {
            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value
            int fc_id = -1; // TODO: Initialize to an appropriate value
            ForeclosureCaseDTO expected = null; // TODO: Initialize to an appropriate value
            ForeclosureCaseDTO actual = target.GetForeclosureCase(fc_id);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region CheckExistingAgencyIdAndCaseNumber
        [TestMethod()]
        public void CheckExistingAgencyIdAndCaseNumberTest_Exist()
        {
            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value
            int agency_id = 2; // TODO: Initialize to an appropriate value
            string agency_case_number = "644186";
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual = target.CheckExistingAgencyIdAndCaseNumber(agency_id, agency_case_number);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckExistingAgencyIdAndCaseNumberTest_NonExist()
        {
            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value
            int agency_id = 2; // TODO: Initialize to an appropriate value
            string agency_case_number = "644187";
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckExistingAgencyIdAndCaseNumber(agency_id, agency_case_number);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckExistingAgencyIdAndCaseNumberTest_InvalidAgencyID()
        {
            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value
            int agency_id = -1; // TODO: Initialize to an appropriate value
            string agency_case_number = "644187";
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckExistingAgencyIdAndCaseNumber(agency_id, agency_case_number);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckExistingAgencyIdAndCaseNumberTest_InvalidCaseNumber()
        {
            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value
            int agency_id = -1; // TODO: Initialize to an appropriate value
            string agency_case_number = "644#$87";
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckExistingAgencyIdAndCaseNumber(agency_id, agency_case_number);
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region CheckDuplicate
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

        [TestMethod()]
        public void CheckDuplicateTest_Exist_From_FCID()
        {
            CheckDuplicate_PreTest();
            
            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value
            int fc_id = 23; // TODO: Initialize to an appropriate value              
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual = target.CheckDuplicate(fc_id);//, agency_id, agency_case_number);
            Assert.AreEqual(expected, actual);
            
            CheckDuplicate_PostTest();
        }

        [TestMethod()]
        public void CheckDuplicateTest_NonExist_FromFCID()
        {
            CheckDuplicate_PreTest();

            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value
            int fc_id = 181; // TODO: Initialize to an appropriate value              
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckDuplicate(fc_id);//, agency_id, agency_case_number);
            Assert.AreEqual(expected, actual);

            CheckDuplicate_PostTest();
        }

        [TestMethod()]
        public void CheckDuplicateTest_InvalidFCID()
        {
            CheckDuplicate_PreTest();

            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value
            int fc_id = -1; // TODO: Initialize to an appropriate value              
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckDuplicate(fc_id);//, agency_id, agency_case_number);
            Assert.AreEqual(expected, actual);

            CheckDuplicate_PostTest();
        }
        [TestMethod()]
        public void CheckDuplicateTest_Exist_FromAgencyIDandAgency_Case_Number()
        {
            CheckDuplicate_PreTest();

            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value
            
            int agency_id = 2;
            string agency_case_number = "644186";
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual = target.CheckDuplicate(agency_id, agency_case_number);
            Assert.AreEqual(expected, actual);

            CheckDuplicate_PostTest();
        }
        [TestMethod()]
        public void CheckDuplicateTest_NonExist_FromAgencyIDandAgency_Case_Number()
        {
            CheckDuplicate_PreTest();

            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value

            int agency_id = 2;
            string agency_case_number = "644404";
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckDuplicate(agency_id, agency_case_number);
            Assert.AreEqual(expected, actual);

            CheckDuplicate_PostTest();
        }

        [TestMethod()]
        public void CheckDuplicateTest_NonExist_InvalidAgencyID()
        {
            CheckDuplicate_PreTest();

            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value

            int agency_id = -2;
            string agency_case_number = "644404";
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckDuplicate(agency_id, agency_case_number);
            Assert.AreEqual(expected, actual);

            CheckDuplicate_PostTest();
        }

        [TestMethod()]
        public void CheckDuplicateTest_NonExist_InvalidAgencyCaseNumber()
        {
            CheckDuplicate_PreTest();

            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor(); // TODO: Initialize to an appropriate value

            int agency_id = 2;
            string agency_case_number = "64asd04";
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckDuplicate(agency_id, agency_case_number);
            Assert.AreEqual(expected, actual);

            CheckDuplicate_PostTest();
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

        #endregion

        #region AppSearchForeclosureCase - Test
        [TestMethod()]
        public void AppSearchForeclosureCaseTest()
        {
            ForeclosureCaseDAO_Accessor target = new ForeclosureCaseDAO_Accessor();
            AppForeclosureCaseSearchCriteriaDTO criteria = new AppForeclosureCaseSearchCriteriaDTO { PropertyZip = "66666", Last4SSN = "1234", Agency=-1, ForeclosureCaseID=-1, Program=-1};
            var result = target.AppSearchForeclosureCase(criteria);
            Assert.AreEqual(result.Count, 5);
            var expected = new List<string> {"23", "123", "181", "183", "185"};
            foreach (var actual in result)
            {
                Assert.AreEqual(actual.LoanList, "abc123, abc124, def123, def1234");
                Assert.AreEqual(actual.PropertyZip, "66666");
                Assert.AreEqual(actual.Last4SSN, "1234");
                Assert.IsTrue(expected.IndexOf(actual.CaseID)!=-1);
            }
        }
        /// <summary>
        /// Search case which has id = 23
        /// </summary>
        [TestMethod()]
        public void AppSearchForeclosureCasebyID()
        {
            ForeclosureCaseDAO_Accessor target = new ForeclosureCaseDAO_Accessor();
            AppForeclosureCaseSearchCriteriaDTO criteria = new AppForeclosureCaseSearchCriteriaDTO {  Agency = -1, ForeclosureCaseID = 23, Program = -1 };
            AppForeclosureCaseSearchResultDTOCollection searchResult = target.AppSearchForeclosureCase(criteria);
            Assert.AreEqual(searchResult.Count, 1);
            AppForeclosureCaseSearchResultDTO actual = searchResult[0];
            Assert.AreEqual(actual.LoanList, "abc123, abc124, def123, def1234");
            Assert.AreEqual(actual.PropertyZip, "66666");
            Assert.AreEqual(actual.Last4SSN, "1234");
            Assert.AreEqual(actual.CaseID, "23");
        }
        #endregion
    }
}
