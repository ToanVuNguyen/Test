using HPF.FutureState.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using System.Text;
using System;
using System.Configuration;
using System.Data.SqlClient;

using HPF.FutureState.Common.Utils.Exceptions;

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
        private CallLogDTO aCallLog;
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
            aCallLog = new CallLogDTO();
            SetCallLogTestData(aCallLog);
           
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            ClearTestData(aCallLog);   
        }
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
            int actual = target.InsertCallLog(aCallLog);
            int expected = GetCallLogId();
            Assert.AreEqual(expected, actual);

            TestContext.WriteLine(string.Format("Expected: {0} - Actual: {1}", expected, actual));
           
        }

        [TestMethod]
        public void InsertCallLogTest_MissingRequiredField()
        {
            CallLogBL_Accessor target = new CallLogBL_Accessor();
            try
            {
                aCallLog.CcCallKey = null;
                target.InsertCallLog(aCallLog);                                               
            }
            catch (DataValidationException actual)
            {
                var expected = new DataValidationException();
                Assert.AreEqual(expected.GetType(), actual.GetType());
                ShowException(actual);
            }
        }

        [TestMethod]
        public void InsertCallLogTest_InvalidForeignKey()
        {
            CallLogBL_Accessor target = new CallLogBL_Accessor();
            try
            {
                aCallLog.PrevAgencyId = 9999999;
                aCallLog.ServicerId = 999999;
                aCallLog.CallCenterID = 9999999;
                target.InsertCallLog(aCallLog);
            }
            catch (Exception actual)
            {
                var expected = new DataValidationException();
                Assert.AreEqual(expected.GetType(), actual.GetType());
                ShowException((DataValidationException)actual);
            }
        }

        [TestMethod]
        public void InsertCallLogTest_InvalidCode()
        {
            CallLogBL_Accessor target = new CallLogBL_Accessor();
            try
            {
                aCallLog.FinalDispoCd = "InvalidCd";
                target.InsertCallLog(aCallLog);
            }
            catch (DataValidationException actual)
            {
                var expected = new DataValidationException();
                Assert.AreEqual(expected.GetType(), actual.GetType());
                ShowException(actual);
            }
        }

        [TestMethod]
        public void InsertCallLogTest_InvalidIndicator()
        {
            
            CallLogBL_Accessor target = new CallLogBL_Accessor();
            try
            {
                aCallLog.HomeownerInd = "s";
                target.InsertCallLog(aCallLog);
            }
            catch (DataValidationException actual)
            {
                var expected = new DataValidationException();
                Assert.AreEqual(expected.GetType(), actual.GetType());
                ShowException(actual);
            }
        }
        [TestMethod]
        public void InsertCallLogTest_InvalidDataLength()
        {
            //12982
            CallLogBL_Accessor target = new CallLogBL_Accessor();
            try
            {
                aCallLog.CcCallKey = "Invalid CcCallKey will be longer than 18 characters";
                target.InsertCallLog(aCallLog);
            }
            catch (DataValidationException actual)
            {
                var expected = new DataValidationException();
                Assert.AreEqual(expected.GetType(), actual.GetType());
                ShowException(actual);
            }
        }

        
        //[TestMethod]
        public void InsertCallLogTest_DependingCallCenter()
        {
            //12982 - servicer other
            CallLogBL_Accessor target = new CallLogBL_Accessor();
            int? oldCallCenterID = aCallLog.CallCenterID;
            try
            {                
                aCallLog.CallCenterID = 76;
                target.InsertCallLog(aCallLog);
                
            }
            catch (Exception actual)
            {
                aCallLog.CallCenterID = oldCallCenterID;
                var expected = new DataValidationException();
                Assert.AreEqual(expected.GetType(), actual.GetType());
                ShowException((DataValidationException)actual);
            }
        }

        #region helper

        #region property
        string agency_name = "agency_name_68";
        string servicer_name = "servicer_name_68";
        string call_center_name = "call_center_68";
        string working_user_id = "wui_686868";
        #endregion

        private void SetCallLogTestData(CallLogDTO aCallLog)
        {

            string sql = "Insert into Agency "
               + " (agency_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
               + " ('" + agency_name + "', 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql);

            sql = "Insert into Call_Center "
                + " (call_center_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('" + call_center_name + "', 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql);

            sql = "Insert into Servicer "
                + " (servicer_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('" + servicer_name + "', 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
            ExecuteSql(sql);

            aCallLog.StartDate = new DateTime(2008, 10, 10);
            aCallLog.EndDate = DateTime.Now;
            aCallLog.CcAgentIdKey = working_user_id;
            aCallLog.CcCallKey = "12345";
            aCallLog.LoanDelinqStatusCd = "120+";
            aCallLog.CallSourceCd = "Billboard";
            aCallLog.FinalDispoCd = "COUNSELORTRANS";
            aCallLog.PrevAgencyId = GetAgencyID();
            aCallLog.ServicerId = GetServicerID();
            aCallLog.CallCenterID = GetCallCenterID();

            aCallLog.SetInsertTrackingInformation(working_user_id);
        }

        private void ClearTestData(CallLogDTO aCallLog)
        {
            string s = "Delete from Call where call_id = " + aCallLog.CallId;
            ExecuteSql(s);
            s = "Delete from Agency where agency_id = " + aCallLog.PrevAgencyId;
            ExecuteSql(s);
            s = "Delete from Call_Center where call_center_id = " + aCallLog.CallCenterID;
            ExecuteSql(s);
            s = "Delete from Servicer where servicer_id = " + aCallLog.ServicerId;
            ExecuteSql(s);  
        }
        
        private int GetCallLogId()
        {
            int id = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string sql = string.Format("SELECT call_Id FROM Call where create_user_id = '{0}' and chg_lst_user_id = '{1}'", working_user_id, working_user_id);
            var command = new SqlCommand(sql, dbConnection);
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
            string sql = "Select agency_id from Agency where agency_name = '" + agency_name + "'";
            var command = new SqlCommand(sql, dbConnection);
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
            string sql = "Select servicer_id from Servicer where servicer_name = '" + servicer_name + "'";
            var command = new SqlCommand(sql, dbConnection);
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
            var command = new SqlCommand("SELECT call_center_id FROM Call_Center where call_center_name = '" + call_center_name + "'", dbConnection);
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
        private void ShowException(DataValidationException ex)
        {
            foreach (ExceptionMessage em in ex.ExceptionMessages)
                TestContext.WriteLine(em.Message);
        }
        #endregion
        #endregion
    }
}
