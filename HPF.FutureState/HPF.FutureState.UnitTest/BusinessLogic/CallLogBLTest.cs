﻿using HPF.FutureState.BusinessLogic;
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
        static private CallLogDTO aCallLog;
        private static SqlConnection dbConnection;
        private static SqlCommand command;
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

        //Use TestInitialize to run code before running each test
        [ClassInitialize()]
        public static void MyTestInitialize(TestContext testContext)
        {
            MyTestCleanup();
            aCallLog = new CallLogDTO();
            SetCallLogTestData(aCallLog);
           
        }
        //
        //Use TestCleanup to run code after each test has run
        [ClassCleanup()]
        public static void MyTestCleanup()
        {
            ClearTestData();            
        }
        //
        #endregion



        #region Insert CallLog
        //[Ignore]
        [TestMethod]
        public void InsertCallLogTest_Pass()
        {
            CallLogBL_Accessor target = new CallLogBL_Accessor();
            
            CallLogDTO aCallLog = new CallLogDTO();
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

            int actual = target.InsertCallLog(aCallLog);
            int expected = GetCallLogId();
            Assert.AreEqual(expected, actual);
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

        #region retrieve call log
        /// <summary>
        ///A test for RetrieveCallLog
        ///</summary>
        [TestMethod()]
        public void RetrieveCallLogTestSuccess()
        {
            CallLogBL_Accessor target = new CallLogBL_Accessor(); // TODO: Initialize to an appropriate value                                                
            int callLogId = GetCallId(); // TODO: Initialize to an appropriate value
            CallLogDTO actual = target.RetrieveCallLog(callLogId);
            Assert.AreEqual("cc_call_key_test_1", actual.CcCallKey);
        }

        /// <summary>
        ///A test fail for RetrieveCallLog        
        ///</summary>    
        [TestMethod()]
        public void RetrieveCallLogTestFail()
        {
            CallLogBL_Accessor target = new CallLogBL_Accessor(); // TODO: Initialize to an appropriate value                                                
            int callLogId = GetCallId(); // TODO: Initialize to an appropriate value
            CallLogDTO actual = target.RetrieveCallLog(callLogId);
            Assert.AreNotEqual("cc_call_key_test_2", actual.CcCallKey);
        }
        #endregion
        #region helper

        #region property
        static string agency_name = "utest_agency_name_68";
        static string servicer_name = "utest_servicer_name_68";
        static string call_center_name = "utest_call_center_68";
        static string working_user_id = "utest_wui_686868";
        #endregion

        static private void SetCallLogTestData(CallLogDTO aCallLog)
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            string sql = "Insert into Agency "
               + " (agency_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
               + " ('" + agency_name + "', 'HPF' ,'" + working_user_id + "' ,'" + DateTime.Now + "', 'HPF', '" + working_user_id + "', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);

            sql = "Insert into Call_Center "
                + " (call_center_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('" + call_center_name + "', 'HPF' ,'" + working_user_id + "' ,'" + DateTime.Now + "', 'HPF', '" + working_user_id + "', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);

            sql = "Insert into Servicer "
                + " (servicer_name, chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
                + " ('" + servicer_name + "', 'HPF' ,'" + working_user_id + "' ,'" + DateTime.Now + "', 'HPF', '" + working_user_id + "', '" + DateTime.Now + "' )";
            ExecuteSql(sql, dbConnection);

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

            sql = "Insert into call(call_center_id, start_dt, end_dt, cc_call_key, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name) values ( " + GetCallCenterID() + ", '" + DateTime.Now + "', '" + DateTime.Now + "', 'cc_call_key_test_1',  '" + DateTime.Now + "', '" + working_user_id + "', 'test', '" + DateTime.Now + "', 'test', 'test')";
            ExecuteSql(sql, dbConnection);
            dbConnection.Close();
        }

        static private void ClearTestData()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();

            string s = "Delete from Call where create_user_id = '" + working_user_id + "'";
            ExecuteSql(s, dbConnection);
            s = "Delete from Agency where create_user_id ='" + working_user_id + "'";
            ExecuteSql(s, dbConnection);
            s = "Delete from Call_Center where create_user_id = '" + working_user_id + "'";
            ExecuteSql(s, dbConnection);
            s = "Delete from Servicer where create_user_id ='" + working_user_id + "'";
            ExecuteSql(s, dbConnection);

            dbConnection.Close();
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

        private int GetCallId()
        {
            int id = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string sql = string.Format("SELECT call_Id FROM Call where cc_call_key = 'cc_call_key_test_1'");
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

        static private int GetAgencyID()
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

        static private int GetServicerID()
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

        static private int GetCallCenterID()
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

        static private void ExecuteSql(string sql, SqlConnection dbConnection)
        {            
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = sql;
            command.ExecuteNonQuery();            
        }
        
        private void ShowException(DataValidationException ex)
        {
            foreach (ExceptionMessage em in ex.ExceptionMessages)
                TestContext.WriteLine(em.Message);
        }
        #endregion

        #region Sript to manually work with db
        //select top(2) * from Call order by Call_Id desc 
        //select top(2) * from Call_Center where call_center_name = 'utest_call_center_68'
        //select top(2) * from Servicer where servicer_name = 'utest_servicer_name_68'
        //Select top(2) * from Agency where agency_name = 'utest_agency_name_68'

        //Delete from Call where cc_agent_id_key = 'utest_wui_686868'
        //Delete from Call_Center where call_center_name = 'utest_call_center_68'
        //Delete from Servicer where servicer_name = 'utest_servicer_name_68'
        //Delete from Agency where agency_name = 'utest_agency_name_68'
        #endregion
        #endregion
    }
}
