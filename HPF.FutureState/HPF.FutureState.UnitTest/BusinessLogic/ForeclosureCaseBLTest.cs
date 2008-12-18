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
                                    ", prop_zip = '12345'" +
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
                                       new string[] {null, null, null, null, null, "12345", "23"}, //3 valid Prop Zip
                                       new string[] {"abc*&*)&", null, null, null, null, "12345", "23"}, //4 invalid Agency Case Num
                                       new string[] {"644186", null, null, null, null, "12345", "23"}, //5 valid Agency Case Num
                                       new string[] {null, null, null, "ab12", null, "12345", "23"}, //6 invalid SSN1
                                       new string[] {null, null, null, "123", null, "12345", "23"}, //7 invalid SSN2
                                       new string[] {null, null, null, "1234", null, "12345", "23"}, //8 valid SSN
                                       new string[] {null, null, null, null, "abc-$^*", "12345", "23"}, //9 test LoanNumber
                                       new string[] {"644186", "MICHAEL", "GOINS", "1234", null, "12345", "23"}};//10 match all
            
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
            PerformTest(0);
        }

        [TestMethod()]
        public void Test_Invalid_PropZip1()
        {
            PerformTest(1);
            //TestContext.WriteLine(criterias[1][5]);
        }

        [TestMethod()]
        public void Test_Invalid_PropZip2()
        {
            PerformTest(2);
            //TestContext.WriteLine(criterias[2][5]);
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
            PerformTest(4);
            //TestContext.WriteLine(criterias[4][0]);
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
            PerformTest(6);
            //TestContext.WriteLine(criterias[6][4]);
        }
        [TestMethod()]
        public void Test_Invalid_SSN2()
        {
            PerformTest(7);
            //TestContext.WriteLine(criterias[7][4]);
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
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
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
            ForeclosureCaseSearchResult results = target.SearchForeclosureCase(searchCriteria);

            if (results != null)
            {
                if ((results.Messages != null) && (results.Messages.ExceptionMessages != null) && (results.Messages.ExceptionMessages.Count > 0))
                {
                    DisplayWarningMessage(results);
                }
                else
                {
                    if (results.Count == 0)
                    {
                        TestContext.WriteLine("There are no objects found");
                    }
                    else
                    {
                        ForeclosureCaseWSDTO retObj = target.SearchForeclosureCase(searchCriteria)[0];
                        actual = retObj.FcId; 
                        Assert.AreEqual(expected, actual);
                        TestContext.WriteLine("Foreclosurecase ID: {0}", retObj.FcId);
                    }
                }
            }
            else
            {
                TestContext.WriteLine("Object return is null");
            }
        }
        private void DisplayWarningMessage(ForeclosureCaseSearchResult results)
        {
            foreach (ExceptionMessage ex in results.Messages.ExceptionMessages)
            {
                TestContext.WriteLine(string.Format("Warning id: {0} - {1}", ex.ExceptionId, ex.Message));
            }

        }
        #endregion

        #region CheckValidFCIdForAgency

        [TestMethod()]
        public void CheckValidFCIdForAgencyTest_Success()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value            
            int fc_id = 23; // TODO: Initialize to an appropriate value
            int agency_id = 2;
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual = target.CheckValidFCIdForAgency(fc_id, agency_id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckValidFCIdForAgencyTest_Fail()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value            
            int fc_id = 23; // TODO: Initialize to an appropriate value
            int agency_id = 3;
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckValidFCIdForAgency(fc_id, agency_id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CheckValidFCIdForAgencyTest_NullCase()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value            
            int fc_id = -1; // TODO: Initialize to an appropriate value
            int agency_id = 3;
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual = target.CheckValidFCIdForAgency(fc_id, agency_id);
            Assert.AreEqual(expected, actual);
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
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
            DateTime completeDate = Convert.ToDateTime("12/01/2007");// TODO: Initialize to an appropriate value                        
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckInactiveCase(completeDate);
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
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
            DateTime completeDate = Convert.ToDateTime("12/12/2009");// TODO: Initialize to an appropriate value                        
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckInactiveCase(completeDate);
            Assert.AreEqual(expected, actual);
        }       

        /// <summary>
        ///A test for RequireFieldsForeclosureCase
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void RequireFieldsForeclosureCaseTestSuccess()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseDTO foreclosureCase = SetForeclosureCase(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RequireFieldsForeclosureCase(foreclosureCase);
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for RequireFieldsBudgetItem
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void RequireFieldsBudgetItemTestSuccess()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
            BudgetItemDTOCollection budgetItemDTOCollection = SetBudgetItemCollection(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RequireFieldsBudgetItem(budgetItemDTOCollection);
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for RequireFieldsOutcomeItem
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void RequireFieldsOutcomeItemTestSuccess()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
            OutcomeItemDTOCollection outcomeItemDTOCollection = SetOutcomeItemCollection(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RequireFieldsOutcomeItem(outcomeItemDTOCollection);
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for RequireFieldsValidation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void RequireFieldsValidationTestSuccess()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO(); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase = SetForeclosureCase();
            foreclosureCaseSet.BudgetItems = SetBudgetItemCollection();
            foreclosureCaseSet.Outcome = SetOutcomeItemCollection();
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RequireFieldsValidation(foreclosureCaseSet);
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for CheckCode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckCodeTest()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
            string codeValue = string.Empty; // TODO: Initialize to an appropriate value
            string codeName = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckCode(codeValue, codeName);
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for CheckValidCode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckValidCodeTestSuccess()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO(); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase = SetForeclosureCase();
            foreclosureCaseSet.CaseLoans = SetCaseLoanCollection();
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckValidCode(foreclosureCaseSet);
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test for CheckValidCode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckValidCodeTestFail()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseSetDTO foreclosureCaseSet = new ForeclosureCaseSetDTO(); // TODO: Initialize to an appropriate value
            foreclosureCaseSet.ForeclosureCase = SetForeclosureCase();
            foreclosureCaseSet.CaseLoans = SetCaseLoanCollection();
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckValidCode(foreclosureCaseSet);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CheckCodeCaseLoan
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HPF.FutureState.BusinessLogic.dll")]
        public void CheckCodeCaseLoanTest()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
            CaseLoanDTOCollection caseLoanCollection = SetCaseLoanCollection(); // TODO: Initialize to an appropriate value            
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckValidCodeForCaseLoan(caseLoanCollection);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }    

        private ForeclosureCaseDTO SetForeclosureCase()
        {            
            ForeclosureCaseDTO foreclosureCase = new ForeclosureCaseDTO();
            foreclosureCase.AgencyId = Convert.ToInt32("1");
            foreclosureCase.ProgramId = Convert.ToInt32("1");
            foreclosureCase.AgencyCaseNum = "Test";
            foreclosureCase.IntakeDt = DateTime.MinValue;
            foreclosureCase.CaseSourceCd = "";
            foreclosureCase.BorrowerFname = "Test";
            foreclosureCase.BorrowerLname = "Test";
            foreclosureCase.PrimaryContactNo = "Test";
            foreclosureCase.ContactAddr1 = "Test";
            foreclosureCase.ContactCity = "Test";
            foreclosureCase.ContactStateCd = "";
            foreclosureCase.ContactZip = "Test";
            foreclosureCase.PropAddr1 = "Test";
            foreclosureCase.PropCity = "Test";
            foreclosureCase.PropStateCd = "";
            foreclosureCase.PropZip = "Test";
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
            return foreclosureCase;
        }

        private BudgetItemDTOCollection SetBudgetItemCollection()
        {
            BudgetItemDTOCollection budgetItemCollection = new BudgetItemDTOCollection();            
            for (int i = 0; i < 5; i++)
            {
                BudgetItemDTO budgetItemDTO = new BudgetItemDTO();
                budgetItemDTO.BudgetSubcategoryId = Convert.ToInt32("1");
                budgetItemDTO.BudgetItemAmt = Convert.ToDecimal("1");
                budgetItemCollection.Add(budgetItemDTO);
            }
            return budgetItemCollection;
        }

        private OutcomeItemDTOCollection SetOutcomeItemCollection()
        {
            OutcomeItemDTOCollection outcomeItemCollection = new OutcomeItemDTOCollection();
            for (int i = 0; i < 5; i++)
            {
                OutcomeItemDTO outcomeItemDTO = new OutcomeItemDTO();
                outcomeItemDTO.OutcomeItemId = Convert.ToInt32("1");
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
                caseLoanCollection.Add(caseLoanDTO);
            }
            return caseLoanCollection;
        }
    }
}
