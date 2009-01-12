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

        #region Setup and Cleanup
        /// <summary>
        /// Setup Test environment
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize]
        public static void SetupTest(TestContext testContext)
        {
            //try
            //{
                var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
                string now = "1/1/2009";
                var command = new SqlCommand("insert into ws_user(login_username, login_password, create_dt,create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)" + 
                                            "values('utest_user1', 'utest_user1','" + now + "',1,'utest','" + now + "',1, 'utest'"+ ")", dbConnection);
                dbConnection.Open();
                command.ExecuteNonQuery();
                dbConnection.Close();
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
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand("delete from ws_user where login_username = 'utest_user1'", dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();
            dbConnection.Close();            
        }
        #endregion

        /// <summary>
        ///A test for GetWSUser Successfully case
        ///</summary>
        [TestMethod()]        
        public void GetWSUserSuccessCaseTest()
        {
            SecurityDAO_Accessor target = new SecurityDAO_Accessor();
            //string userName = "utest_user1";
            //string password = "utest_user1";
            string userName = "agency";
            string password = "agency";
            int userID = getUserID("utest_user1");
            
            WSUserDTO actual = target.GetWSUser(userName, password);
            Assert.AreEqual(userID, actual.WsUserId);            
            Assert.AreEqual(new System.DateTime(2009, 1, 1), actual.CreateDt);
            Assert.AreEqual("1", actual.CreateUserId);
            Assert.AreEqual(new System.DateTime(2009, 1, 1), actual.ChgLstDt);
            Assert.AreEqual("1", actual.ChgLstUserId);
            Assert.AreEqual("utest", actual.CreateLstAppName);
        }
        
        /// <summary>
        ///A test for GetWSUser Fail case: invaliad user/password
        ///</summary>
        [TestMethod()]        
        public void GetWSUserFailCaseTest()
        {
            SecurityDAO_Accessor target = new SecurityDAO_Accessor(); 
            string userName = "utest_user1";
            string password = "utest_user2";
            
            WSUserDTO actual = target.GetWSUser(userName, password);
            Assert.AreEqual(null, actual);
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
            
            WSUserDTO actual = target.GetWSUser(userName, password);
            Assert.AreEqual(null, actual);
        }

        #region Ultility  
      
        private int getUserID(string username)
        {
            int result = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand("select ws_user_id from ws_user where login_username='" + username + "'", dbConnection);
            dbConnection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result = int.Parse(reader["ws_user_id"].ToString());
                break;
            }
            dbConnection.Close();

            return result;
        }
        #endregion
    }
}
