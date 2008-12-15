using HPF.FutureState.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;

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
            Assert.AreNotEqual(expected, actual);            
        }
    }
}
