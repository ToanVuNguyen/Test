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
    ///This is a test class for ForeClosureCaseSetDAOTest and is intended
    ///to contain all ForeClosureCaseSetDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ForeClosureCaseSetDAOTest
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
                                    ", prop_zip = '12345'" +
                                    ", borrower_last4_SSN = '1234'" +
                                    " where fc_id in (23, 123, 181, 183, 185) ";
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
                                    " where fc_id in (23, 123, 181, 183, 185)";
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            criterias = new string[][]{                                       
                                       new string[] {null, null, null, null, null, "12346"},
                                       new string[] {null,null, null, null, null, "12345"}};//10 match all
            expected = new List<int>();
            expected.Add(185);
            expected.Add(183);
            expected.Add(181);
            expected.Add(123);
            expected.Add(23);
            //int[] results = new int[]{185, 183, 181, 123, 23};
            
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
        ///A test for SearchForeClosureCase
        ///</summary>
        [TestMethod()]
        public void SearchForeClosureCaseTest_Success()
        {
            List<int> actual = PerformTest(1);
            CollectionAssert.AreEquivalent(expected, actual);            
        }

        /// <summary>
        ///A test for SearchForeClosureCase
        ///</summary>
        [TestMethod()]
        public void SearchForeClosureCaseTest_Fail()
        {
           var actual = PerformTest(0);
           CollectionAssert.AreNotEqual(expected, actual);
            //TestContext.WriteLine(actual.Length.ToString());
        }

        private List<int> PerformTest(int index)
        {
            ForeclosureCaseSetDAO_Accessor target = new ForeclosureCaseSetDAO_Accessor();
            HPF.FutureState.Common.DataTransferObjects.ForeclosureCaseSearchCriteriaDTO searchCriteria = new HPF.FutureState.Common.DataTransferObjects.ForeclosureCaseSearchCriteriaDTO();

            string[] criteria = criterias[index];
            searchCriteria.AgencyCaseNumber = criteria[0];
            searchCriteria.FirstName = criteria[1];
            searchCriteria.LastName = criteria[2];
            searchCriteria.Last4_SSN = criteria[3];
            searchCriteria.LoanNumber = criteria[4];
            searchCriteria.PropertyZip = criteria[5];


            ForeclosureCaseSearchResult searchResults = target.SearchForeClosureCase(searchCriteria);

            List<int> actual;
            if (searchResults != null)
            {
                actual = new List<int>();
                foreach (HPF.FutureState.Common.DataTransferObjects.WebServices.ForeclosureCaseWSDTO result in searchResults)
                {
                   actual.Add(result.FcId);
                }
                return actual;
            }
            else
                return null;
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
    }
}
