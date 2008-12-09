using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.UnitTest.DataAccess
{
    
    
    /// <summary>
    ///This is a test class for SecurityDAOTest and is intended
    ///to contain all SecurityDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SecurityDAOTest
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
        ///A test for GetWSUser Successfully case
        ///</summary>
        [DeploymentItem("SecurityDAO_GetWSUser.xls"), 
        DataSource("System.Data.Odbc","Dsn=Excel Files;dbq=|DataDirectory|\\SecurityDAO_GetWSUser.xls;defaultdir=HPF.FutureState.UnitTest\\DataAccess;driverid=790;maxbuffersize=2048;pagetimeout=5", "Success$", DataAccessMethod.Sequential),
        TestMethod()]        
        public void GetWSUserSuccessCaseTest()
        {
            SecurityDAO_Accessor target = new SecurityDAO_Accessor(); // TODO: Initialize to an appropriate value
            string userName = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            int userid = (int)(double)TestContext.DataRow["userid"];
            WSUserDTO actual;
            actual = target.GetWSUser(userName, password);
            Assert.AreEqual(userid, actual.WsUserId);            
        }

        /// <summary>
        ///A test for GetWSUser Fail case
        ///</summary>
        [DeploymentItem("SecurityDAO_GetWSUser.xls"),
        DataSource("System.Data.Odbc", "Dsn=Excel Files;dbq=|DataDirectory|\\SecurityDAO_GetWSUser.xls;defaultdir=HPF.FutureState.UnitTest\\DataAccess;driverid=790;maxbuffersize=2048;pagetimeout=5", "Fail$", DataAccessMethod.Sequential),
        TestMethod()]
        public void GetWSUserFailCaseTest()
        {
            SecurityDAO_Accessor target = new SecurityDAO_Accessor(); // TODO: Initialize to an appropriate value
            string userName = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            //int userid = (int)(double)TestContext.DataRow["userid"];
            WSUserDTO actual;
            actual = target.GetWSUser(userName, password);
            Assert.AreEqual(null, actual);            
        }
    }
}
