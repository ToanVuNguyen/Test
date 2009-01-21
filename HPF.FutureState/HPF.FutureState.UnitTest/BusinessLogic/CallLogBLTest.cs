using HPF.FutureState.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using System.Text;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace HPF.FutureState.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for CallLogBLTest and is intended
    ///to contain all CallLogBLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CallLogBLTest
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

        #region retrieve call log
        /// <summary>
        ///A test for RetrieveCallLog
        ///</summary>
        [TestMethod()]
        public void RetrieveCallLogTestSuccess()
        {
            CallLogBL_Accessor target = new CallLogBL_Accessor(); // TODO: Initialize to an appropriate value
            int callLogId = 0; // TODO: Initialize to an appropriate value
            CallLogDTO expected = null; // TODO: Initialize to an appropriate value
            CallLogDTO actual;
            actual = target.RetrieveCallLog(callLogId);
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///A test fail for RetrieveCallLog        
        ///</summary>    
        [TestMethod()]
        public void RetrieveCallLogTestFail()
        {
            CallLogBL_Accessor target = new CallLogBL_Accessor(); // TODO: Initialize to an appropriate value
            int callLogId = 2; // TODO: Initialize to an appropriate value
            CallLogDTO expected = null; // TODO: Initialize to an appropriate value
            CallLogDTO actual;
            actual = target.RetrieveCallLog(callLogId);
            Assert.AreEqual(expected, actual);
        }
#endregion

        #region Insert CallLog
        [TestMethod]
        public void InsertCallLogTest_Pass()
        {
            CallLogBL_Accessor target = new CallLogBL_Accessor();
            CallLogDTO aCallLog = new CallLogDTO();
            try
            {
                SetCallLogTestData(aCallLog);
                int actual = target.InsertCallLog(aCallLog);
                int expected = GetCallLogId();
                Assert.AreEqual(expected, actual);

                TestContext.WriteLine(string.Format("Expected: {0} - Actual: {1}", expected, actual));
            }
            catch
            {
                ClearTestData(aCallLog);
            }

        }

        #region helper
        private void SetCallLogTestData(CallLogDTO aCallLog)
        {
            string sql = "Insert into Agency "
                + " (chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql);

            sql = "Insert into Call_Center "
                + " (chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql);

            sql = "Insert into Servicer "
                + " (chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql);
            

            aCallLog.StartDate = new DateTime(2008, 10, 10);
            aCallLog.EndDate = DateTime.Now;
            aCallLog.CcCallKey = "12345";
            aCallLog.FinalDispoCd = "test";
            aCallLog.PrevAgencyId = GetAgencyID();
            aCallLog.ServicerId = GetServicerID();
            aCallLog.CallCenterID = GetCallCenterID();

            aCallLog.SetInsertTrackingInformation("Unit test");

            
        }
        private void ClearTestData(CallLogDTO aCallLog)
        {
            string s = "Delete from Call where call_id = " + aCallLog.CallId;
            ExecuteSql(s);
            s = "Delete from Agency where agency_id = " + aCallLog.PrevAgencyId;
            ExecuteSql(s);
            s = "Delete from CallCenter where call_center_id = " + aCallLog.CallCenterID;
            ExecuteSql(s);
            s = "Delete from Servicer where servicer_id = " + aCallLog.ServicerId;
            ExecuteSql(s);
        }
        
        private int GetCallLogId()
        {
            int id = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand("SELECT Max(Call_Id) FROM Call", dbConnection);
            dbConnection.Open();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                    id = reader.GetInt32(0);
            }

            dbConnection.Close();
            return id;
        }
       
        private int GetAgencyID()
        {
            int id = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand("SELECT Max(Agency_ID) FROM Agency", dbConnection);
            dbConnection.Open();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                    id = reader.GetInt32(0);
            }

            dbConnection.Close();
            return id;
        }

        private int GetServicerID()
        {
            int id = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand("SELECT Max(Servicer_ID) FROM Servicer", dbConnection);
            dbConnection.Open();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                    id = reader.GetInt32(0);
            }

            dbConnection.Close();
            return id;
        }

        private int GetCallCenterID()
        {
            int id = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand("SELECT Max(Call_Center_ID) FROM Call_Center", dbConnection);
            dbConnection.Open();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                    id = reader.GetInt32(0);
            }

            dbConnection.Close();
            return id;
        }

        private void ExecuteSql(string sql)
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = sql;
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
        #endregion
        #endregion
    }
}
