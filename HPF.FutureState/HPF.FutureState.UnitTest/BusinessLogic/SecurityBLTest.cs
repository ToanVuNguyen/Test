using System.Data;
using System.Data.SqlClient;
using System.Configuration;

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
        private static SqlConnection dbConnection;

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

        /// <summary>
        /// Setup Test environment
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize]
        public static void SetupTest(TestContext testContext)
        {
            //try
            //{
            dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);

            var command = new SqlCommand("insert into ws_user(login_username, login_password) values('utest_user1', 'utest_user1')", dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{ 
            //}
        }

        /// <summary>
        /// Clearn All Data test
        /// </summary>
        [ClassCleanup()]
        public static void CleanupTest()
        {
            var command = new SqlCommand("delete from ws_user where login_username like 'utest_user%'", dbConnection);
            command.ExecuteNonQuery();

            dbConnection.Close();
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
        /// Test login in case successfull case
        /// </summary>
        [TestMethod()]
        public void WSUserLoginSuccessTest()
        {
            SecurityBL_Accessor target = new SecurityBL_Accessor();
            string userName = "utest_user1";
            string password = "utest_user1";
            WSType wsType = WSType.Any;

            bool expected = true;
            bool actual;
            actual = target.WSUserLogin(userName, password, wsType);
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        /// test login in case invalid user type
        /// </summary>
        [TestMethod()]
        public void WSUserLoginInvalidUserTypeTest()
        {
            SecurityBL_Accessor target = new SecurityBL_Accessor();
            string userName = "utest_user1";
            string password = "utest_user1";
            WSType wsType = WSType.CallCenter;

            bool expected = false;
            bool actual;
            actual = target.WSUserLogin(userName, password, wsType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// test login in case invalid password
        /// </summary>        
        [TestMethod()]
        public void WSUserLoginInvalidPasswordTest()
        {
            SecurityBL_Accessor target = new SecurityBL_Accessor();
            string userName = "utest_user1";
            string password = "utest_user2";
            WSType wsType = WSType.Any;

            bool expected = false;
            bool actual;
            actual = target.WSUserLogin(userName, password, wsType);
            Assert.AreEqual(expected, actual);
        }
    }
}
