using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

using System;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;


namespace HPF.FutureState.UnitTest
{
    /// <summary>
    ///This is a test class for CallLogDAOTest and is intended
    ///to contain all CallLogDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CallLogDAOTest
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
        /// Create data test
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize]
        public static void SetupTest(TestContext testContext)
        {
           
        }

        /// <summary>
        /// Delete data test
        /// </summary>
        [ClassCleanup()]
        public static void CleanupTest()
        {
           
        }
        /// <summary>
        ///A test sussess for ReadCallLog
        ///</summary>      
        [TestMethod()]
        public void ReadCallLogTestSuccess()
        {
            CallLogDAO_Accessor target = new CallLogDAO_Accessor(); // TODO: Initialize to an appropriate value
            var callLogId = 2;//TODO: Initialize to an appropriate value
            CallLogDTO expected = CallLogDAO.Instance.ReadCallLog(callLogId); // TODO: Initialize to an appropriate value                        
            CallLogDTO actual = target.ReadCallLog(callLogId);
            Assert.AreEqual(expected.CallId, actual.CallId);
        }

        /// <summary>
        ///A test fail for ReadCallLog
        ///</summary>                
        [TestMethod()]
        public void ReadCallLogTestFail()
        {
            CallLogDAO_Accessor target = new CallLogDAO_Accessor(); // TODO: Initialize to an appropriate value
            var callLogId1 = 2; // TODO: Initialize to an appropriate value
            var callLogId2 = 3;
            CallLogDTO expected = CallLogDAO.Instance.ReadCallLog(callLogId1); // TODO: Initialize to an appropriate value           
            CallLogDTO actual = target.ReadCallLog(callLogId2);
            Assert.AreNotEqual(expected.CallId, actual.CallId);
        }

        #region InsertCallLog_Test

        private void InsertCallLogTest_Pre()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;

            command.CommandText = "Insert into call_center(call_center_name, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name) values ('call_center_name_1', '" + DateTime.Now + "', 'test', 'test', '" + DateTime.Now + "', 'test', 'test')";
            command.ExecuteNonQuery();
            
            dbConnection.Close();

        }

        private int GetCallCenterID()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;

            command.CommandText = "Select Max(call_center_id) from call_center";
            var reader = command.ExecuteReader();
            int id = 0;
            if (reader.HasRows)
            {
                reader.Read();
                id = reader.GetInt32(0);
            }
            reader.Close();
            dbConnection.Close();
            return id;           
        }

        private int GetCallLogID()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;

            command.CommandText = "Select Max(call_id) from call";
            var reader = command.ExecuteReader();
            int id = 0;
            if (reader.HasRows)
            {
                reader.Read();
                id = reader.GetInt32(0);
            }
            reader.Close();
            dbConnection.Close();
            return id;
        }

        int callLogid = 0;
        [TestMethod()]
        public void InsertCallLogTest_Success()
        {
            InsertCallLogTest_Pre();
            CallLogDAO_Accessor target = new CallLogDAO_Accessor(); // TODO: Initialize to an appropriate value           
            
            CallLogDTO aCallLog = new CallLogDTO();
            aCallLog.CallCenterID = GetCallCenterID();
            aCallLog.CreateDate = DateTime.Now;
            aCallLog.CreateUserId = "test";
            aCallLog.CreateAppName = "test";
            aCallLog.ChangeLastDate = DateTime.Now;
            aCallLog.ChangeLastAppName = "test";
            aCallLog.ChangeLastUserId = "test";
            int actual = target.InsertCallLog(aCallLog);
            int expected = GetCallLogID();
            callLogid = actual;
            Assert.AreEqual(expected, actual);
            TestContext.WriteLine("New Call Log ID: " + actual);

            InsertCallLogTest_Post();
        }

        private void InsertCallLogTest_Post()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;

            command.CommandText = "Delete from Call where call_id = " + callLogid;
            command.ExecuteNonQuery();


            command.CommandText = "Delete from Call_Center where call_center_ID = " + GetCallCenterID() ;// 'call_center_name_1'";
            command.ExecuteNonQuery();


            dbConnection.Close();
        }

        #endregion

    }
}
