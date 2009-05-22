using HPF.FutureState.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for RefCodeItemBLTest and is intended
    ///to contain all RefCodeItemBLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RefCodeItemBLTest
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
        ///A test for GetRefCodeItemsForAgency
        ///</summary>
        [TestMethod()]
        public void GetRefCodeItemsForAgencyTest()
        {
            RefCodeItemBL_Accessor target = new RefCodeItemBL_Accessor(); // TODO: Initialize to an appropriate value
            string refCodeName = null; // TODO: Initialize to an appropriate value            
            RefCodeItemDTOCollection actual;
            actual = target.GetRefCodeItemsForAgency(refCodeName);
            Assert.AreNotEqual(0, actual.Count);            
        }

        [TestMethod()]
        public void GetRefCodeItemsForAgencyCodeNameTest()
        {
            RefCodeItemBL_Accessor target = new RefCodeItemBL_Accessor(); // TODO: Initialize to an appropriate value
            string refCodeName = "activity code"; // TODO: Initialize to an appropriate value            
            RefCodeItemDTOCollection actual;
            actual = target.GetRefCodeItemsForAgency(refCodeName);
            Assert.AreNotEqual(0, actual.Count);
        }
    }
}
