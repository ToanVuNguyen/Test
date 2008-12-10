using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

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
        /// <summary>
        ///A test for GetWSUser Successfully case
        ///</summary>
        [TestMethod()]        
        public void GetWSUserSuccessCaseTest()
        {
            SecurityDAO_Accessor target = new SecurityDAO_Accessor();
            string userName = "utest_user1";
            string password = "utest_user1";
            WSUserDTO user = SecurityDAO.Instance.GetWSUser(userName, password);
            Assert.AreNotEqual(user, null);
            WSUserDTO actual;
            actual = target.GetWSUser(userName, password);
            Assert.AreEqual(user.WsUserId, actual.WsUserId);            
        }
        
        /// <summary>
        ///A test for GetWSUser Fail case
        ///</summary>
        [TestMethod()]        
        public void GetWSUserFailCaseTest()
        {
            SecurityDAO_Accessor target = new SecurityDAO_Accessor(); 
            string userName = "utest_user1";
            string password = "utest_user2";
            //int userid = (int)(double)TestContext.DataRow["userid"];
            WSUserDTO actual;
            actual = target.GetWSUser(userName, password);
            Assert.AreEqual(null, actual);
        }

        /// <summary>
        ///A test for GetWSUser
        ///</summary>
        [TestMethod()]  
        [Ignore]
        public void GetWSUserDataCaseTest()
        {
            SecurityDAO_Accessor target = new SecurityDAO_Accessor();
            //WSUserDTO user = GetWSUser(TestContext);
            //WSUserDTO actual;
            //actual = target.GetWSUser(user.LoginUsername, user.LoginPassword);
            //AssertCheckEquals(user, actual);
        }

        /// <summary>
        /// exception case test
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(DataAccessException))]
        public void GetWSUserExceptionCaseTest()
        {
            SecurityDAO_Accessor target = new SecurityDAO_Accessor();
            string userName = null;
            string password = null;
            
            WSUserDTO actual;
            actual = target.GetWSUser(userName, password);
            Assert.AreEqual(null, actual);
        }

        #region Ultility        

        #endregion
    }
}
