using HPF.FutureState.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HPF.FutureState.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for OutcomeItemBLTest and is intended
    ///to contain all OutcomeItemBLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OutcomeItemBLTest
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
        ///A test for InstateOutcomeItem
        ///</summary>
        [TestMethod()]
        public void InstateOutcomeItemTest()
        {
            OutcomeItemBL_Accessor target = new OutcomeItemBL_Accessor(); // TODO: Initialize to an appropriate value
            int outcomeItemId = 0; // TODO: Initialize to an appropriate value
            string workingUserId = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.InstateOutcomeItem(outcomeItemId, workingUserId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DeleteOutcomeItem
        ///</summary>
        [TestMethod()]
        public void DeleteOutcomeItemTest()
        {
            OutcomeItemBL_Accessor target = new OutcomeItemBL_Accessor(); // TODO: Initialize to an appropriate value
            int outcomeItemId = 0; // TODO: Initialize to an appropriate value
            string workingUserId = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.DeleteOutcomeItem(outcomeItemId, workingUserId);
            Assert.AreEqual(expected, actual);
        }

        #region helper

        #endregion
    }
}
