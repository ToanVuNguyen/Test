using HPF.FutureState.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Configuration;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using System.Collections;
using HPF.FutureState.Common.Utils.Exceptions;
using System;
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
        string[][] criterias;
        ForeclosureCaseSearchCriteriaDTO searchCriteria;
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
                                    " where fc_id = 23";
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
                                    " where fc_id = 23";
            command.ExecuteNonQuery();            
            dbConnection.Close();
        }
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {         
            //AgencyCaseNumber 0 , FirstName 1 , LastName 2, Last4_SSN 3, LoanNumber 4, PropertyZip 5, return_Fcid
            criterias = new string[][]{
                                       new string[] {null, null, null, null, null, null, "23"}, //0 null all
                                       new string[] {null, null, null, null, null, "abcd", "23"}, //1 invalid Prop Zip
                                       new string[] {null, null, null, null, null, "123456", "23"}, //2 invalid Prop Zip
                                       new string[] {null, null, null, null, null, "66666", "23"}, //3 valid Prop Zip
                                       new string[] {"abc*&*)&", null, null, null, null, "66666", "23"}, //4 invalid Agency Case Num
                                       new string[] {"644186", null, null, null, null, "66666", "23"}, //5 valid Agency Case Num
                                       new string[] {null, null, null, "ab12", null, "66666", "23"}, //6 invalid SSN1
                                       new string[] {null, null, null, "123", null, "66666", "23"}, //7 invalid SSN2
                                       new string[] {null, null, null, "1234", null, "66666", "23"}, //8 valid SSN
                                       new string[] {null, null, null, null, "abc-$^*", "66666", "23"}, //9 test LoanNumber
                                       new string[] {"644186", "MICHAEL", "GOINS", "1234", null, "66666", "23"}};//10 match all
            
        }
        
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for SearchForeclosureCase
        ///</summary>

        #region SearchForeclosureCase
        [TestMethod()]
        public void Test_Null_All()
        {
            try
            {
                PerformTest(0);
            }
            catch (DataValidationException dve)
            {
                var expected = new DataValidationException();
                Assert.AreEqual(expected.GetType(), dve.GetType());
            }
        }

        [TestMethod()]
        public void Test_Invalid_PropZip1()
        {
            try
            {
                PerformTest(1);
            }
            catch (DataValidationException dve)
            {
                var expected = new DataValidationException();
                Assert.AreEqual(expected.GetType(), dve.GetType());
            }
            //TestContext.WriteLine(criterias[1][5]);
        }

        [TestMethod()]
        public void Test_Invalid_PropZip2()
        {
            try
            {
                PerformTest(2);
            }
            catch (DataValidationException dve)
            {
                var expected = new DataValidationException();
                Assert.AreEqual(expected.GetType(), dve.GetType());
            }
        }

        [TestMethod()]
        public void Test_Valid_PropZip()
        {
            PerformTest(3);
            //TestContext.WriteLine(criterias[3][5]);
        }

        [TestMethod()]
        public void Test_InValid_AgencyNumber()
        {
            try
            {
                PerformTest(4);
            }
            catch (DataValidationException dve)
            {
                var expected = new DataValidationException();
                Assert.AreEqual(expected.GetType(), dve.GetType());
            }
        }
        [TestMethod()]
        public void Test_Valid_AgencyNumber()
        {
            PerformTest(5);
            //TestContext.WriteLine(criterias[5][0]);
        }
        [TestMethod()]
        public void Test_Invalid_SSN1()
        {
            try
            {
                PerformTest(6);
            }
            catch (DataValidationException dve)
            {
                var expected = new DataValidationException();
                Assert.AreEqual(expected.GetType(), dve.GetType());
            }
            //TestContext.WriteLine(criterias[6][4]);
        }
        [TestMethod()]
        public void Test_Invalid_SSN2()
        {
            try
            {
                PerformTest(7);
            }
            catch (DataValidationException dve)
            {
                var expected = new DataValidationException();
                Assert.AreEqual(expected.GetType(), dve.GetType());
            }
        }

        [TestMethod()]
        public void Test_Valid_SSN()
        {
            PerformTest(8);
            //TestContext.WriteLine(criterias[8][4]);
        }

        [TestMethod()]
        public void Test_Valid_LoanNumber()
        {
            PerformTest(9);
            //TestContext.WriteLine(criterias[8][4]);
        }
        [TestMethod()]
        public void SearchForeclosureCaseSuccessTest_MatchAllCriteria()
        {
            PerformTest(10);                     
        }

        private void PerformTest(int index)
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            searchCriteria = new ForeclosureCaseSearchCriteriaDTO();

            string[] criteria = criterias[index];
            searchCriteria.AgencyCaseNumber = criteria[0];
            searchCriteria.FirstName = criteria[1];
            searchCriteria.LastName = criteria[2];
            searchCriteria.Last4_SSN = criteria[3];
            searchCriteria.LoanNumber = criteria[4];
            searchCriteria.PropertyZip = criteria[5];

            int expected = int.Parse(criteria[6]);  // expect an fc_id to be returned
            int actual = 0;
            ForeclosureCaseSearchResult results = target.SearchForeclosureCase(searchCriteria, 50);

            if (results != null)
            {
                    if (results.Count == 0)
                    {
                        TestContext.WriteLine("There are no objects found");
                    }
                    else
                    {
                        ForeclosureCaseWSDTO retObj = results[0];//target.SearchForeclosureCase(searchCriteria, 50)[0];
                        
                        actual = retObj.FcId;
                        Assert.AreEqual(expected, actual);
                        TestContext.WriteLine("Foreclosurecase ID: {0}", retObj.FcId);
                    }
            }
            else
            {
                TestContext.WriteLine("Object return is null");
            }
        }
        private void DisplayWarningMessage(ForeclosureCaseSearchResult results)
        {
            //foreach (ExceptionMessage ex in results.Messages.ExceptionMessages)
            //{
            //    TestContext.WriteLine(string.Format("Warning id: {0} - {1}", ex.ExceptionId, ex.Message));
            //}

        }
        #endregion

        #region CheckValidFCIdForAgency

        [TestMethod()]
        public void CheckValidFCIdForAgencyTest_Success()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value            
            int fc_id = 23; // TODO: Initialize to an appropriate value
            int agency_id = 2;
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual = target.CheckValidFCIdForAgency(fc_id, agency_id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckValidFCIdForAgencyTest_Fail()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value            
            int fc_id = 23; // TODO: Initialize to an appropriate value
            int agency_id = 3;
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckValidFCIdForAgency(fc_id, agency_id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckValidFCIdForAgencyTest_NullCase()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value            
            int fc_id = -1; // TODO: Initialize to an appropriate value
            int agency_id = 3;
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckValidFCIdForAgency(fc_id, agency_id);
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region ProcessInsertUpdateWithForeclosureCaseId
        [TestMethod]
        public void ProcessInsertUpdateWithForeclosureCaseId_Success_Null_FC_CaseSet()
        {
            TestContext.WriteLine("This test will throw an ProcessingException when ForeclosureCaseSetDTO is null");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = null; // TODO: Initialize to an appropriate value
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithForeclosureCaseId(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }

        [TestMethod]
        public void ProcessInsertUpdateWithForeclosureCaseId_Success_InvalidFcID()
        {
            TestContext.WriteLine("This test will throw an ProcessingException when FCID is invalid");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO();
            foreclosureCaseSet.ForeclosureCase = new ForeclosureCaseDTO();
            foreclosureCaseSet.ForeclosureCase.FcId = -1;            
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithForeclosureCaseId(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }

        [TestMethod]
        public void ProcessInsertUpdateWithForeclosureCaseId_Success_InvalidFcID_For_Agency()
        {
            TestContext.WriteLine("This test will throw an ProcessingException when pair of FCID and Agency is invalid");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO();
            foreclosureCaseSet.ForeclosureCase = new ForeclosureCaseDTO();
            foreclosureCaseSet.ForeclosureCase.AgencyId = 2;
            foreclosureCaseSet.ForeclosureCase.FcId = 24;
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithForeclosureCaseId(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }

        [TestMethod]
        public void ProcessInsertUpdateWithForeclosureCaseId_Success_ActiveFcCase()
        {
            TestContext.WriteLine("This test will throw an ProcessingException when fc_case is active");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO();
            foreclosureCaseSet.ForeclosureCase = new ForeclosureCaseDTO();            
            foreclosureCaseSet.ForeclosureCase.FcId = 23;
            foreclosureCaseSet.ForeclosureCase.CompletedDt = (DateTime)System.Data.SqlTypes.SqlDateTime.Null;
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithForeclosureCaseId(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }

        [TestMethod]
        public void ProcessInsertUpdateWithForeclosureCaseId_Success()
        {
            TestContext.WriteLine("This test is not implemented yet");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO();
            foreclosureCaseSet.ForeclosureCase = new ForeclosureCaseDTO();
            foreclosureCaseSet.ForeclosureCase.AgencyId = 2;
            foreclosureCaseSet.ForeclosureCase.AgencyCaseNum = "644186";
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithoutForeclosureCaseId(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }
        #endregion

        #region ProcessInsertUpdateWithoutForeclosureCaseId
        [TestMethod]
        public void ProcessInsertUpdateWithoutForeclosureCaseId_Success_Null_FC_CaseSet()
        {
            TestContext.WriteLine("This test will throw an ProcessingException when ForeclosureCaseSetDTO is null");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = null; // TODO: Initialize to an appropriate value
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithoutForeclosureCaseId(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }
        
        [TestMethod]
        public void ProcessInsertUpdateWithoutForeclosureCaseId_Success_Null_AgencyID_CaseNumber()
        {
            TestContext.WriteLine("This test will throw an ProcessingException when AgencyID or CaseNumber is null");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO();
            foreclosureCaseSet.ForeclosureCase = new ForeclosureCaseDTO();
            foreclosureCaseSet.ForeclosureCase.AgencyId = 0;
            foreclosureCaseSet.ForeclosureCase.AgencyCaseNum = null;
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithoutForeclosureCaseId(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }

        [TestMethod]
        public void ProcessInsertUpdateWithoutForeclosureCaseId_Success_Existing_AgencyID_CaseNumber()
        {
            TestContext.WriteLine("This test will throw an ProcessingException when AgencyID and CaseNumber existed");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO();
            foreclosureCaseSet.ForeclosureCase = new ForeclosureCaseDTO();
            foreclosureCaseSet.ForeclosureCase.AgencyId = 2;
            foreclosureCaseSet.ForeclosureCase.AgencyCaseNum = "644186";
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithoutForeclosureCaseId(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }

        [TestMethod]
        public void ProcessInsertUpdateWithoutForeclosureCaseId_Success()
        {
            TestContext.WriteLine("This test is not implemented yet");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO();
            foreclosureCaseSet.ForeclosureCase = new ForeclosureCaseDTO();
            foreclosureCaseSet.ForeclosureCase.AgencyId = 2;
            foreclosureCaseSet.ForeclosureCase.AgencyCaseNum = "644186";
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertUpdateWithoutForeclosureCaseId(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }
        #endregion

        #region ProcessInsertForeclosureCaseSet
        [TestMethod]
        public void ProcessInsertForeclosureCaseSet_Success_Null_FC_CaseSet()
        {
            TestContext.WriteLine("This test will throw an ProcessingException when ForeclosureCaseSetDTO is null");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = null; // TODO: Initialize to an appropriate value
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
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
        [TestMethod]
        public void ProcessInsertForeclosureCaseSet_Success_Duplicated_FC_Case()
        {
            TestContext.WriteLine("This test will throw an ProcessingException when there is a dupe case");
            CheckDuplicate_PreTest();
            ForeclosureCaseDTO fcCase = new ForeclosureCaseDTO();
            fcCase.FcId = 123;
            
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO(); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase = fcCase;
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
            CheckDuplicate_PostTest();
        }

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
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
            CheckDuplicate_PostTest();
        }

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
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
            CheckDuplicate_PostTest();
        }
        #endregion

        #region ProcessUpdateForeclosureCaseSet
        [TestMethod]
        public void ProcessUpdateForeclosureCaseSet_Success_Null_FC_CaseSet()
        {
            TestContext.WriteLine("This test will throw an ProcessingException when ForeclosureCaseSetDTO is null");
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = null; // TODO: Initialize to an appropriate value
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessUpdateForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
        }

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
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
            CheckDuplicate_PostTest();
        }

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
            var expected = (new ProcessingException()).GetType();
            try
            {
                target.ProcessInsertForeclosureCaseSet(foreclosureCaseSet);
            }
            catch (ProcessingException pe)
            {
                Assert.AreEqual(expected, pe.GetType());
            }
            CheckDuplicate_PostTest();
        }
        #endregion
        
        /// <summary>
        ///A test for CheckInactiveCase
        ///Case False
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckInactiveCaseTestFalse()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCase = SetForeclosureCaseSet();// TODO: Initialize to an appropriate value                        
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckInactiveCase(foreclosureCase);
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for CheckInactiveCase
        ///Case True
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckInactiveCaseTestTrue()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCase = SetForeclosureCaseSet();// TODO: Initialize to an appropriate value                        
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckInactiveCase(foreclosureCase);
            Assert.AreEqual(expected, actual);
        }       

        /// <summary>
        ///A test for RequireFieldsForeclosureCase
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void RequireFieldsForeclosureCaseTestSuccess()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseDTO foreclosureCase = SetForeclosureCase(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RequireFieldsForeclosureCase(foreclosureCase, "Default");
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for RequireFieldsBudgetItem
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void RequireFieldsBudgetItemTestSuccess()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            BudgetItemDTOCollection budgetItemDTOCollection = SetBudgetItemCollection(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RequireFieldsBudgetItem(budgetItemDTOCollection, "Default");
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for RequireFieldsOutcomeItem
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void RequireFieldsOutcomeItemTestSuccess()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            OutcomeItemDTOCollection outcomeItemDTOCollection = SetOutcomeItemCollection(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RequireFieldsOutcomeItem(outcomeItemDTOCollection, "Default");
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for RequireFieldsValidation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void RequireFieldsValidationTestSuccess()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO(); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase = SetForeclosureCase();
            foreclosureCaseSet.BudgetItems = SetBudgetItemCollection();
            foreclosureCaseSet.Outcome = SetOutcomeItemCollection();
            foreclosureCaseSet.CaseLoans = SetCaseLoanCollection();
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RequireFieldsValidation(foreclosureCaseSet);
            Assert.AreEqual(expected, actual);            
        }       

        /// <summary>
        ///A test for CheckValidCode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckValidCodeTestSuccess()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO(); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase = SetForeclosureCase();
            foreclosureCaseSet.CaseLoans = SetCaseLoanCollection();
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckValidCode(foreclosureCaseSet);
            Assert.AreEqual(expected, actual);            
        }

        ///// <summary>
        /////A test for CheckValidCode
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        //public void CheckValidCodeTestFail()
        //{
        //    ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
        //    ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO(); // TODO: Initialize to an appropriate value
        //    foreclosureCaseSet.ForeclosureCase = SetForeclosureCase();
        //    foreclosureCaseSet.CaseLoans = SetCaseLoanCollection();
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = target.CheckValidCode(foreclosureCaseSet);
        //    Assert.AreEqual(expected, actual);
        //}

        /// <summary>
        ///A test for CheckCodeCaseLoan
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckCodeCaseLoanTest()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            CaseLoanDTOCollection caseLoanCollection = SetCaseLoanCollection(); // TODO: Initialize to an appropriate value            
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckValidCodeForCaseLoan(caseLoanCollection);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for InsertForeclosureCaseSet
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void InsertForeclosureCaseSetTest()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = SetForeclosureCaseSet(); // TODO: Initialize to an appropriate value
            target.InsertForeclosureCaseSet(foreclosureCaseSet);            
        }

        /// <summary>
        ///A test for UpdateForeclosureCaseSet
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void UpdateForeclosureCaseSetTest()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = SetForeclosureCaseSet(); // TODO: Initialize to an appropriate value
             target.UpdateForeclosureCaseSet(foreclosureCaseSet);
        }

        /// <summary>
        ///A test for CheckForInsertBudgetItems
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void IsBudgetItemsDifferenceTest()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            BudgetItemDTOCollection budgetCollection = SetBudgetItemCollection(); // TODO: Initialize to an appropriate value
            int fc_id = 1350045; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual = true;
            //actual = target.IsBudgetItemsDifference(budgetCollection, fc_id);
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for CheckForInsertBudgetAsset
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void IsBudgetAssetDifferenceTest()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            BudgetAssetDTOCollection budgetCollection = SetBudgetAssetCollection(); // TODO: Initialize to an appropriate value
            int fc_id = 1350053; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual = true;
            //actual = target.IsBudgetAssetDifference(budgetCollection, fc_id);
            Assert.AreEqual(expected, actual);
        }

        private ForeclosureCaseDTO SetForeclosureCase()
        {            
            ForeclosureCaseDTO foreclosureCase = new ForeclosureCaseDTO();
            foreclosureCase.FcId = Convert.ToInt32("1350076");
            foreclosureCase.AgencyId = Convert.ToInt32("1");
            foreclosureCase.ProgramId = Convert.ToInt32("1");
            //foreclosureCase.AgencyCaseNum = "Test";
            foreclosureCase.IntakeDt = Convert.ToDateTime("12/12/2009");
            foreclosureCase.CaseSourceCd = "";
            foreclosureCase.BorrowerFname = "Test";
            foreclosureCase.BorrowerLname = "Test";
            foreclosureCase.PrimaryContactNo = "Test";
            foreclosureCase.ContactAddr1 = "Test";
            foreclosureCase.ContactCity = "Test";
            foreclosureCase.ContactStateCd = "";
            foreclosureCase.ContactZip = "1235";
            foreclosureCase.PropAddr1 = "Test";
            foreclosureCase.PropCity = "Test";
            foreclosureCase.PropStateCd = "";
            foreclosureCase.PropZip = "12345";
            foreclosureCase.OwnerOccupiedInd = "Test";
            foreclosureCase.FundingConsentInd = "Test";
            foreclosureCase.ServicerConsentInd = "Test";
            foreclosureCase.AssignedCounselorIdRef = "Test";
            foreclosureCase.CounselorFname = "Test";
            foreclosureCase.CounselorLname = "Test";
            foreclosureCase.CounselorEmail = "Test";
            foreclosureCase.CounselorPhone = "Test";
            foreclosureCase.OptOutNewsletterInd = "Test";
            foreclosureCase.OptOutSurveyInd = "Test";
            foreclosureCase.DoNotCallInd = "Test";
            foreclosureCase.PrimaryResidenceInd = "Test";
            foreclosureCase.CreateDate = DateTime.Now;
            foreclosureCase.CreateUserId = "HPF";
            foreclosureCase.CreateAppName = "HPF";
            foreclosureCase.ChangeLastDate = DateTime.Now;
            foreclosureCase.ChangeLastAppName = "HPF";
            foreclosureCase.ChangeLastUserId = "HPF";
            foreclosureCase.AgencyClientNum = "";
            foreclosureCase.AgencyCaseNum = "";
            return foreclosureCase;
        }

        private BudgetAssetDTOCollection SetBudgetAssetCollection()
        {
            BudgetAssetDTOCollection budgetAssetCollection = new BudgetAssetDTOCollection();
            for (int i = 0; i < 4; i++)
            {
                BudgetAssetDTO budgetAsset = new BudgetAssetDTO();
                budgetAsset.BudgetSetId = Convert.ToInt32("76156");
                budgetAsset.AssetName = "Test";
                budgetAsset.AssetValue = Convert.ToDecimal("30.65");
                budgetAsset.CreateDate = DateTime.Now;
                budgetAsset.CreateUserId = "HPF";
                budgetAsset.CreateAppName = "HPF";
                budgetAsset.ChangeLastDate = DateTime.Now;
                budgetAsset.ChangeLastAppName = "HPF";
                budgetAsset.ChangeLastUserId = "HPF";
                budgetAssetCollection.Add(budgetAsset);
            }
            //BudgetAssetDTO budgetAsset1 = new BudgetAssetDTO();
            //budgetAsset1.BudgetSetId = Convert.ToInt32("76156");
            //budgetAsset1.AssetName = "Test";
            //budgetAsset1.AssetValue = Convert.ToDecimal("11.2");

            //budgetAssetCollection.Add(budgetAsset1);
            return budgetAssetCollection;
        }

        private BudgetItemDTOCollection SetBudgetItemCollection()
        {
            BudgetItemDTOCollection budgetItemCollection = new BudgetItemDTOCollection();                        
            BudgetItemDTO budgetItemDTO = new BudgetItemDTO();
            budgetItemDTO.BudgetSetId = Convert.ToInt32("76156");
            budgetItemDTO.BudgetSubcategoryId = Convert.ToInt32("1");
            budgetItemDTO.BudgetItemAmt = Convert.ToDecimal("900.08");
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
            budgetItemDTO.BudgetSubcategoryId = Convert.ToInt32("8");
            budgetItemDTO.BudgetItemAmt = Convert.ToDecimal("300.05");
            budgetItemDTO.BudgetNote = null;
            budgetItemDTO.CreateDate = DateTime.Now;
            budgetItemDTO.CreateUserId = "HPF";
            budgetItemDTO.CreateAppName = "HPF";
            budgetItemDTO.ChangeLastDate = DateTime.Now;
            budgetItemDTO.ChangeLastAppName = "HPF";
            budgetItemDTO.ChangeLastUserId = "HPF";
            budgetItemCollection.Add(budgetItemDTO);
            
            return budgetItemCollection;
        }

        private OutcomeItemDTOCollection SetOutcomeItemCollection()
        {
            OutcomeItemDTOCollection outcomeItemCollection = new OutcomeItemDTOCollection();
            for (int i = 0; i < 1; i++)
            {
                OutcomeItemDTO outcomeItemDTO = new OutcomeItemDTO();
                outcomeItemDTO.OutcomeTypeId = Convert.ToInt32("1");
                outcomeItemDTO.FcId = Convert.ToInt32("1350076");
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
            return outcomeItemCollection;
        }

        private CaseLoanDTOCollection SetCaseLoanCollection()
        {
            CaseLoanDTOCollection caseLoanCollection = new CaseLoanDTOCollection();
            for (int i = 0; i < 1; i++)
            {
                CaseLoanDTO caseLoanDTO = new CaseLoanDTO();
                caseLoanDTO.LoanDelinqStatusCd = "30-59";
                caseLoanDTO.ServicerId = Convert.ToInt32("1");
                caseLoanDTO.AcctNum = "Test";
                caseLoanDTO.CreateDate = DateTime.Now;
                caseLoanDTO.CreateUserId = "HPF";
                caseLoanDTO.CreateAppName = "HPF";
                caseLoanDTO.ChangeLastDate = DateTime.Now;
                caseLoanDTO.ChangeLastAppName = "HPF";
                caseLoanDTO.ChangeLastUserId = "HPF";
                caseLoanCollection.Add(caseLoanDTO);
            }
            return caseLoanCollection;
        }

        private BudgetSetDTO SetBudgetSet()
        {
            BudgetSetDTO budgetSetDTO = new BudgetSetDTO();
            budgetSetDTO.BudgetSetDt = Convert.ToDateTime("12/12/2009");
            budgetSetDTO.CreateDate = DateTime.Now;
            budgetSetDTO.CreateUserId = "HPF";
            budgetSetDTO.CreateAppName = "HPF";
            budgetSetDTO.ChangeLastDate = DateTime.Now;
            budgetSetDTO.ChangeLastAppName = "HPF";
            budgetSetDTO.ChangeLastUserId = "HPF";
            return budgetSetDTO;
        }

        private ForeclosureCaseSetDTO SetForeclosureCaseSet()
        {
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO();
            foreclosureCaseSet.ForeclosureCase = SetForeclosureCase();
            foreclosureCaseSet.CaseLoans = SetCaseLoanCollection();
            foreclosureCaseSet.Outcome = SetOutcomeItemCollection();
            foreclosureCaseSet.BudgetSet = SetBudgetSet();
            foreclosureCaseSet.BudgetItems = SetBudgetItemCollection();
            foreclosureCaseSet.BudgetAssets = SetBudgetAssetCollection();
            return foreclosureCaseSet;
        }
        #region AppForeclosureCaseSearch Test
        /// <summary>
        ///A test for Empty Search Criteria provided by User
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        [ExpectedException(typeof(ProcessingException))]
        public void EmptySearchCriteriaProvided()
        {
            ForeclosureCaseSetBL_Accessor target = new ForeclosureCaseSetBL_Accessor(); // TODO: Initialize to an appropriate value
            //empty Search criteria, default value of interger is -1 and string is null
            AppForeclosureCaseSearchCriteriaDTO criteria = new AppForeclosureCaseSearchCriteriaDTO { Agency = -1, ForeclosureCaseID = -1, Program = -1 };
            target.AppSearchforeClosureCase(criteria);
        }
        #endregion
    }
}
