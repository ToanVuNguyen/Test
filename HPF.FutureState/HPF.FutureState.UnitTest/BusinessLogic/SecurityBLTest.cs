using HPF.FutureState.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.UnitTest.BusinessLogic
{
    
    
    /// <summary>
    ///This is a test class for SecurityBLTest and is intended
    ///to contain all SecurityBLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SecurityBLTest
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


        
        [DeploymentItem("SecurityBL_UserLogin.xls"),
        DataSource("System.Data.Odbc", "Dsn=Excel Files;dbq=|DataDirectory|\\SecurityBL_UserLogin.xls;defaultdir=\\HPF.FutureState.UnitTest\\BusinessLogic;driverid=790;maxbuffersize=2048;pagetimeout=5", "Success$", DataAccessMethod.Sequential),
        TestMethod()]
        public void WSUserLoginSuccessTest()
        {
            SecurityBL_Accessor target = new SecurityBL_Accessor(); 
            string userName = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            WSType wsType = (WSType)System.Enum.Parse(typeof(WSType), TestContext.DataRow["usertype"].ToString(), true);

            bool expected = bool.Parse(TestContext.DataRow["result"].ToString());
            bool actual;
            actual = target.WSUserLogin(userName, password, wsType);
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        /// test login in case invalid user type
        /// </summary>
        [DeploymentItem("SecurityBL_UserLogin.xls"),
        DataSource("System.Data.Odbc", "Dsn=Excel Files;dbq=|DataDirectory|\\SecurityBL_UserLogin.xls;defaultdir=\\HPF.FutureState.UnitTest\\BusinessLogic;driverid=790;maxbuffersize=2048;pagetimeout=5", "InvalidUserType$", DataAccessMethod.Sequential),
        TestMethod()]
        public void WSUserLoginInvalidUserTypeTest()
        {
            SecurityBL_Accessor target = new SecurityBL_Accessor();
            string userName = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            WSType wsType = (WSType)System.Enum.Parse(typeof(WSType), TestContext.DataRow["usertype"].ToString(), true);

            bool expected = bool.Parse(TestContext.DataRow["result"].ToString());
            bool actual;
            actual = target.WSUserLogin(userName, password, wsType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// test login in case invalid user type
        /// </summary>
        [DeploymentItem("SecurityBL_UserLogin.xls"),
        DataSource("System.Data.Odbc", "Dsn=Excel Files;dbq=|DataDirectory|\\SecurityBL_UserLogin.xls;defaultdir=\\HPF.FutureState.UnitTest\\BusinessLogic;driverid=790;maxbuffersize=2048;pagetimeout=5", "InvalidPassword$", DataAccessMethod.Sequential),
        TestMethod()]
        public void WSUserLoginInvalidPasswordTest()
        {
            SecurityBL_Accessor target = new SecurityBL_Accessor();
            string userName = TestContext.DataRow["username"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            WSType wsType = (WSType)System.Enum.Parse(typeof(WSType), TestContext.DataRow["usertype"].ToString(), true);

            bool expected = bool.Parse(TestContext.DataRow["result"].ToString());
            bool actual;
            actual = target.WSUserLogin(userName, password, wsType);
            Assert.AreEqual(expected, actual);
        }
    }
}
