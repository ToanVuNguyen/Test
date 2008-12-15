using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

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

        /// <summary>
        ///A test fail for ReadCallLog
        ///This function will be passed when 
        /// 01. connection string wrong, or
        /// 02. store procedure doesn't exist        
        ///</summary>                
        //[TestMethod()]
        //[ExpectedException(typeof(DataAccessException))]
        //public void ReadCallLogTestFailException()
        //{
        //    CallLogDAO_Accessor target = new CallLogDAO_Accessor(); // TODO: Initialize to an appropriate value
        //    var callLogId = int.MinValue; // TODO: Initialize to an appropriate value            
        //    CallLogDTO actual = target.ReadCallLog(callLogId);
        //    Assert.AreEqual(null, actual);
        //}                
    }
}
