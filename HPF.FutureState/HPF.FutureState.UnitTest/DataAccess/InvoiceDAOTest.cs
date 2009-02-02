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

            // Search Result's funding source ids 
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
        /// <summary>
        ///A test for InsertInvoice
        ///</summary>
        [TestMethod()]
        public void InsertInvoiceTest()
        {
            InvoiceDAO target = new InvoiceDAO(); // TODO: Initialize to an appropriate value

            InvoiceDTO invoice = new InvoiceDTO { FundingSourceId = 2, PeriodStartDate=new DateTime(2107,1,1), PeriodEndDate= new DateTime(2108,1,1)};
            invoice.SetInsertTrackingInformation("1");
            target.BeginTransaction();
            int invoiceID = target.InsertInvoice(invoice);
            target.CommitTransaction();

            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "Select funding_source_id from Invoice where Invoice_id="+ invoiceID.ToString();
            var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                dbConnection.Close();
                reader.Close();
                Assert.Fail("Insert Invoice Not successful");
            }
            reader.Read();
            Assert.AreEqual(2, int.Parse(reader["funding_source_id"].ToString()));
            reader.Close();
            dbConnection.Close();
        }

        /// <summary>
        /// Insert Invoice and then insert Invoice case which belongs to the inserted Invoice
        /// </summary>
        [TestMethod()]
        public void InsertInvoiceCaseTest()
        {
            InvoiceDAO target = new InvoiceDAO(); // TODO: Initialize to an appropriate value

            // insert Invoice 
            InvoiceDTO invoice = new InvoiceDTO { FundingSourceId = 2, PeriodStartDate = new DateTime(2107, 1, 1), PeriodEndDate = new DateTime(2108, 1, 1) };
            invoice.SetInsertTrackingInformation("1");
            target.BeginTransaction();
            int invoiceID = -1;
            invoiceID= target.InsertInvoice(invoice);
            target.CommitTransaction();
            if (invoiceID == -1)
                Assert.Fail("Cannot Insert invoice.");


            InvoiceCaseDTO invoiceCase = new InvoiceCaseDTO { InvoiceId = invoiceID, ForeclosureCaseId = 123, InvoiceCasePaymentAmount = 9999 };
            invoiceCase.SetInsertTrackingInformation("1");

            target.BeginTransaction();
            target.InsertInvoiceCase(invoiceCase);
            target.CommitTransaction();

            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "Select invoice_case_pmt_amt from invoice_case where Invoice_id="+invoiceID;
            var reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                dbConnection.Close();
                reader.Close();
                Assert.Fail("Insert InvoiceCase fail.");
            }
            reader.Read();
            Assert.AreEqual(9999,double.Parse(reader["invoice_case_pmt_amt"].ToString()));
            reader.Close();
            //Delete inserted invoiceCase
            command.CommandText = "Delete invoice_case where Invoice_id=" + invoiceID.ToString();
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

    }

}
