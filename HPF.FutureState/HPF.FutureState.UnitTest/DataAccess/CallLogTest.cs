using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HPF.FutureState.UnitTest.DataAccess
{
    /// <summary>
    /// Summary description for CallLogTest
    /// </summary>
    [TestClass]
    public class CallLogTest
    {
        public CallLogTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestCleanup()]
        public void TestCallLogCleanup()
        {
            
        }

        [TestMethod]        
        public void TestInsertCallLog()
        {
            var callLog = InsertACallLog();
            var dbCallLog = ReadCallLogFromDatabase(callLog);  
            Assert.AreEqual(callLog, dbCallLog,"Fail");            
        }

        private CallLogDTO ReadCallLogFromDatabase(CallLogDTO callLog)
        {
            return CallLogDAO.Instance.ReadCallLog(callLog.CallId);
        }

        private CallLogDTO InsertACallLog()
        {
            var callLog = GetTestCallLog();
            CallLogDAO.Instance.InsertCallLog(callLog);
            return callLog;
        }

        private CallLogDTO GetTestCallLog()
        {
            return new CallLogDTO();
        }
    }
}
