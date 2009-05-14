using HPF.FutureState.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for ServicerBLTest and is intended
    ///to contain all ServicerBLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ServicerBLTest
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
        ///A test for GetServicers
        ///</summary>
        [TestMethod()]
        public void GetServicersTest()
        {
            ServicerBL target = new ServicerBL(); // TODO: Initialize to an appropriate value            
            ServicerDTOCollection actual;
            actual = target.GetServicers();            
            Assert.AreNotEqual(0, actual.Count);            
        }

        /// <summary>
        ///A test for GetServicer
        ///</summary>
        [TestMethod()]
        public void GetServicerTest()
        {
            ServicerBL target = new ServicerBL(); // TODO: Initialize to an appropriate value
            int servicerId = 110; // TODO: Initialize to an appropriate value            
            ServicerDTO actual;
            actual = target.GetServicer(servicerId);
            Assert.AreNotEqual(null, actual);
            Assert.AreEqual(servicerId, actual.ServicerID);            
        }
    }
}
