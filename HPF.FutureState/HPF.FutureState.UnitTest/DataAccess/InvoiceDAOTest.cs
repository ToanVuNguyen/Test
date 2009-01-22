using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using System.Collections.Generic;
using System.Configuration;
using System;
using System.Data.SqlClient;

namespace HPF.FutureState.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for InvoiceDAOTest and is intended
    ///to contain all InvoiceDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InvoiceDAOTest
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "Insert Into Invoice(funding_source_id,period_start_dt, period_end_dt, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)" +
                                    " values (1,'1/1/2107', '1/1/2108', '1/1/2208', 'test', 'test', '1/1/2008', 'test', 'test')";
            command.ExecuteNonQuery();

            command.CommandText = "Insert Into Invoice(funding_source_id,period_start_dt, period_end_dt, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)" +
                                    " values (2,'1/1/2107', '1/1/2108', '1/1/2208', 'test', 'test', '1/1/2008', 'test', 'test')";
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "Delete Invoice where funding_source_id = 1 and period_start_dt='1/1/2107' and period_end_dt='1/1/2108'";
            command.ExecuteNonQuery();

            command.CommandText = command.CommandText = "Delete Invoice where funding_source_id = 2 and period_start_dt='1/1/2107' and period_end_dt='1/1/2108'";
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
        //
        #endregion


        /// <summary>
        ///A test for SearchInvoice
        ///</summary>
        [TestMethod()]
        public void SearchInvoiceTest()
        {
            InvoiceDAO target = new InvoiceDAO(); // TODO: Initialize to an appropriate value
            InvoiceSearchCriteriaDTO searchCriteria = new InvoiceSearchCriteriaDTO(); // TODO: Initialize to an appropriate value
            searchCriteria.FundingSourceId = -1;
            searchCriteria.PeriodStart = new DateTime(2107, 01, 01);
            searchCriteria.PeriodEnd = new DateTime(2108, 02, 01);

            List<int> expected = new List<int>();
            expected.Add(1);
            expected.Add(2);

            InvoiceDTOCollection results;
            results = target.SearchInvoice(searchCriteria);
            List<int> actual = new List<int>();
            actual.Add(results[0].FundingSourceId);
            actual.Add(results[1].FundingSourceId);
            for (int i = 0; i < 2; i++)
                Assert.AreEqual(expected[i], actual[i]);
            
        }
    }
}
