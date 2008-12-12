﻿using System.Data;
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
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);

            var command = new SqlCommand("insert into ws_user(login_username, login_password) values('utest_user2', 'utest_user2')", dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();
            command.CommandText = "insert into call_center(call_center_name) values('utest_callcenter1')";
            command.ExecuteNonQuery();
            command.CommandText = "insert into agency(agency_name) values('utest_agency1')";
            command.ExecuteNonQuery();
            command.CommandText = "insert into ws_user(login_username, login_password, agency_id) values('utest_user2', 'utest_user2', '" + getAgencyID("utest_agency1") + "')";
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
            var command = new SqlCommand("delete from ws_user where login_username = 'utest_user2'", dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();
            command.CommandText = "delete from agency where agency_name = 'utest_agency1'";
            command.ExecuteNonQuery();
            command.CommandText ="delete from call_center where call_center_name = 'utest_callcenter1'";
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
            string userName = "utest_user2";
            string password = "utest_user2";
            WSType wsType = WSType.Agency;

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
            string userName = "utest_user2";
            string password = "utest_user2";
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
            string userName = "utest_user2";
            string password = "utest_user";
            WSType wsType = WSType.Any;

            bool expected = false;
            bool actual;
            actual = target.WSUserLogin(userName, password, wsType);
            Assert.AreEqual(expected, actual);
        }


        #region Ultility

        static private int getCallCenterID(string centerName)
        {
            int result = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            var command = new SqlCommand("select call_center_id from call_center where call_center_name='" + centerName + "'", dbConnection);
            dbConnection.Open();
            var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                result = int.Parse(reader["call_center_id"].ToString());
                break;
            }
            dbConnection.Close();

            return result;
        }

        static private int getAgencyID(string agencyName)
        {            
            int result = 0;
            try
            {
                var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
                var command = new SqlCommand("select agency_id from agency where agency_name='" + agencyName + "'", dbConnection);
                dbConnection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result = int.Parse(reader["agency_id"].ToString());
                    break;
                }
                dbConnection.Close();
            }
            catch (System.Exception ex)
            {
                return 0;
            }
            return result;
        }
        #endregion
    }
}
