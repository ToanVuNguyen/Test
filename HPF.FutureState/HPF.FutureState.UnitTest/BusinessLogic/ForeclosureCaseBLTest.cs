using HPF.FutureState.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

using System.Collections;
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
        ForeClosureCaseSearchCriteriaDTO searchCriteria;
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
            //searchCriteria.AgencyCaseNumber = criteria[0];
            //searchCriteria.FirstName = criteria[1];
            //searchCriteria.LastName = criteria[2];
            //searchCriteria.Last4_SSN = criteria[3];
            //searchCriteria.LoanNumber = criteria[4];
            //searchCriteria.PropertyZip = criteria[5];
            criterias = new string[][]{new string[] {null, null, null, null, null, "12345", "23"},
                                       new string[] {"644186", "MICHAEL", "GOINS", null, null, null, "23"},
                                       new string[] {"10458400", "TODD", "SEITZ", "", "15399", "", "123"}};            

        }
        
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for SearchForeClosureCase
        ///</summary>
        
        [TestMethod()]
        public void SearchForeClosureCaseSuccessTest_MatchAllCriteria()
        {
            ForeclosureCaseBL_Accessor target = new ForeclosureCaseBL_Accessor(); // TODO: Initialize to an appropriate value
            searchCriteria = new ForeClosureCaseSearchCriteriaDTO();

            string[] criteria = criterias[0];
            searchCriteria.AgencyCaseNumber = criteria[0];
            searchCriteria.FirstName = criteria[1];
            searchCriteria.LastName = criteria[2];
            searchCriteria.Last4_SSN = criteria[3];
            searchCriteria.LoanNumber = criteria[4];
            searchCriteria.PropertyZip = criteria[5];

            int expected = int.Parse(criteria[6]);  // expect an fc_id to be returned
            
            ForeClosureCaseWSDTO retObj = target.SearchForeClosureCase(searchCriteria)[0];
            int actual =retObj.FcId; // target.SearchForeClosureCase(searchCriteria).Count; 
            
            Assert.AreEqual(expected, actual);
            
        }
    }
}
