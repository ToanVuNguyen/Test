using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using System.Collections.Generic;
using System.Configuration;
using System;
using System.Data.SqlClient;
using HPF.FutureState.Common.Utils.Exceptions;


namespace HPF.FutureState.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for InvoiceDAOTest and is intended
    ///to contain all InvoiceDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InvoiceDAOTest
    {

        public static int invoiceId;
        public static int fundingSourceId;
        public static int paymentId;
        private TestContext testContextInstance;
        static string working_user_id = "SinhUnitTest";

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
            //Insert Funding Source
            command.CommandText = @"insert into [dbo].funding_source([funding_source_name],[funding_source_comment]
           ,[city],[state_cd],[create_dt],[create_user_id],[create_app_name],[chg_lst_dt],[chg_lst_user_id]
           ,[chg_lst_app_name],[funding_source_abbrev]) values ('fs accounting test','fs test','hcm test'
           ,'test','1/1/2222','" + working_user_id + "','fs test app','1/1/2222','Test data','CCRC','5')";
            command.ExecuteNonQuery();
            //-- get Funding Source Id of test data .
            command.CommandText = @"select funding_source_id from funding_source where create_user_id='" + working_user_id + "'";
            fundingSourceId = Convert.ToInt32(command.ExecuteScalar());

            //Insert invoice payment
            command.CommandText = @"INSERT INTO [dbo].[invoice_payment]([funding_source_id],[pmt_num] ,[pmt_dt],[pmt_cd]
            ,[create_dt] ,[create_user_id],[create_app_name],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name])
            VALUES (" + fundingSourceId + ",'"+working_user_id+"','1/1/2222','test test','1/1/2222','HPF','fs test app','1/1/2222','Test data','CCRC')";
            command.ExecuteNonQuery();
            //get back
            command.CommandText = @"select invoice_payment_id from invoice_payment where pmt_num='" + working_user_id + "'";
            paymentId = Convert.ToInt32(command.ExecuteScalar());
            //Insert Invoice

            command.CommandText = "Insert Into Invoice(invoice_dt,funding_source_id,period_start_dt, period_end_dt, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name,invoice_comment)" +
                                    " values ('1/1/2107',"+fundingSourceId.ToString()+",'1/1/2107', '1/1/2108', '1/1/2208', 'test', 'test', '1/1/2008', 'test', 'test','"+working_user_id+"')";
            command.ExecuteNonQuery();

            //command.CommandText = "Insert Into Invoice(invoice_dt,funding_source_id,period_start_dt, period_end_dt, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name,invoice_comment)" +
            //                        " values ('1/1/2107'," + fundingSourceId.ToString() + ",'1/1/2107', '1/1/2108', '1/1/2208', 'test', 'test', '1/1/2008', 'test', 'test','"+working_user_id+"')";
            //command.ExecuteNonQuery();
            //Get invoiceId
            command.CommandText = "Select Invoice_id from Invoice where invoice_comment='"+working_user_id+"' ";
            var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                dbConnection.Close();
                reader.Close();
                Assert.Fail("Insert Invoice Not successful");
            }
            reader.Read();
            invoiceId = int.Parse(reader["Invoice_id"].ToString());
            reader.Close();
            //Insert invoice Case
            command.CommandText = "Insert Into invoice_case(invoice_case_pmt_amt,fc_id,invoice_id,create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)" +
                                    " values (100,234769," + invoiceId.ToString() + ",'1/1/2208', '"+working_user_id+"', 'InvoiceCase1', '1/1/2008', 'InvoiceCase1', 'test')";
            command.ExecuteNonQuery();
            command.CommandText = "Insert Into invoice_case(invoice_case_pmt_amt,fc_id,invoice_id,create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)" +
                                    " values (200,234769," + invoiceId.ToString() + ",'1/1/2208', '"+working_user_id+"', 'InvoiceCase2', '1/1/2008', 'InvoiceCase2', 'test')";
            command.ExecuteNonQuery();
            command.CommandText = "Insert Into invoice_case(invoice_case_pmt_amt,fc_id,invoice_id,create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name)" +
                                    " values (300,234769," + invoiceId.ToString() + ",'1/1/2208', '"+working_user_id+"', 'InvoiceCase3', '1/1/2008', 'InvoiceCase3', 'test')";
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
            

            command.CommandText = "Delete invoice_case where create_user_id='"+working_user_id+"'";
            command.ExecuteNonQuery();

            command.CommandText = "Delete Invoice where invoice_comment='"+working_user_id+"'";
            command.ExecuteNonQuery();
            command.CommandText = @"delete invoice_payment where pmt_num='" + working_user_id + "'";

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
            searchCriteria.FundingSourceId = fundingSourceId;
            searchCriteria.PeriodStart = new DateTime(2107, 01, 01);
            searchCriteria.PeriodEnd = new DateTime(2108, 02, 01);

            // Search Result's funding source ids 

            InvoiceDTOCollection results;
            results = target.SearchInvoice(searchCriteria);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(fundingSourceId, results[0].FundingSourceId.Value);
        }
        /// <summary>
        ///A test for InsertInvoice
        ///</summary>
        [TestMethod()]
        public void InsertInvoiceTest()
        {
            InvoiceDAO target = new InvoiceDAO(); // TODO: Initialize to an appropriate value

            InvoiceDTO invoice = new InvoiceDTO { FundingSourceId = fundingSourceId, InvoiceComment=working_user_id, PeriodStartDate=new DateTime(2107,1,1), PeriodEndDate= new DateTime(2108,1,1)};
            invoice.InvoiceDate = new DateTime(2108, 1, 1);
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
            Assert.AreEqual(fundingSourceId, int.Parse(reader["funding_source_id"].ToString()));
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
            InvoiceDTO invoice = new InvoiceDTO { FundingSourceId = fundingSourceId, PeriodStartDate = new DateTime(2107, 1, 1), PeriodEndDate = new DateTime(2108, 1, 1) };
            invoice.InvoiceDate = new DateTime(2108, 1, 1);
            invoice.InvoiceComment = working_user_id;
            invoice.SetInsertTrackingInformation("1");
            target.BeginTransaction();
            int invoiceID = -1;
            invoiceID= target.InsertInvoice(invoice);
            target.CommitTransaction();
            if (invoiceID == -1)
                Assert.Fail("Cannot Insert invoice.");


            InvoiceCaseDTO invoiceCase = new InvoiceCaseDTO { InvoiceId = invoiceID, ForeclosureCaseId = 123, InvoiceCasePaymentAmount = 9999 };
            invoiceCase.SetInsertTrackingInformation("1");
            invoiceCase.CreateUserId = working_user_id;

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
            dbConnection.Close();
        }
        [TestMethod()]
        public void InvoiceSetGetTest()
        {
            InvoiceDAO target = new InvoiceDAO(); // TODO: Initialize to an appropriate value
            InvoiceSetDTO actual= target.InvoiceSetGet(invoiceId);
            Assert.AreEqual(fundingSourceId, actual.Invoice.FundingSourceId);
            Assert.AreEqual(working_user_id, actual.Invoice.InvoiceComment);
            Assert.AreEqual(invoiceId, actual.Invoice.InvoiceId);
        }
        [TestMethod()]
        public void UpdateInvoiceCase_RejectCase()
        {
            InvoiceDAO target = new InvoiceDAO(); // TODO: Initialize to an appropriate value
            //Get invoice Set which was inserted in the init method
            InvoiceSetDTO actual = target.InvoiceSetGet(invoiceId);
            foreach(var i in actual.InvoiceCases)
                i.PaidDate = new DateTime(2007, 4, 4).ToShortDateString();
            actual.ChangeLastDate = new DateTime(2007, 4, 4);
            actual.ChangeLastAppName = "Unit TEst";
            actual.ChangeLastUserId = "1";
            string invoiceCaseIdCollection = actual.InvoiceCases[1].InvoiceCaseId.ToString()+","+actual.InvoiceCases[2].InvoiceCaseId.ToString();
            //set RejectReason
            actual.PaymentRejectReason = "REO";
            //reject invoiceCase 1 and invoiceCase2
            target.UpdateInvoiceCase(actual, invoiceCaseIdCollection, InvoiceCaseUpdateFlag.Reject);

            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "Select fc_id,invoice_id,create_user_id,create_app_name,invoice_case_pmt_amt,pmt_reject_reason_cd from invoice_case where invoice_case_id in (" + invoiceCaseIdCollection+")";
            var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                dbConnection.Close();
                reader.Close();
                Assert.Fail("Insert InvoiceCase fail.");
            }
            while(reader.Read())
            {
                Assert.AreEqual(234769, int.Parse(reader["fc_id"].ToString()));
                Assert.AreEqual(""+working_user_id+"", reader["create_user_id"].ToString());
                Assert.AreEqual("REO", reader["pmt_reject_reason_cd"].ToString());
                Assert.AreEqual(string.Empty, reader["invoice_case_pmt_amt"].ToString());
            }
        }

        [TestMethod()]
        public void UpdateInvoiceCase_UnPayCase()
        {
            InvoiceDAO target = new InvoiceDAO(); // TODO: Initialize to an appropriate value
            //Get invoice Set which was inserted in the init method
            InvoiceSetDTO actual = target.InvoiceSetGet(invoiceId);
            foreach (var i in actual.InvoiceCases)
                i.PaidDate = new DateTime(2007, 4, 4).ToShortDateString();
            actual.ChangeLastDate = new DateTime(2007, 4, 4);
            actual.ChangeLastAppName = "Unit TEst";
            actual.ChangeLastUserId = "1";
            string invoiceCaseIdCollection = actual.InvoiceCases[1].InvoiceCaseId.ToString() + "," + actual.InvoiceCases[2].InvoiceCaseId.ToString();
            //set RejectReason
            //reject invoiceCase 1 and invoiceCase2
            target.UpdateInvoiceCase(actual, invoiceCaseIdCollection, InvoiceCaseUpdateFlag.Unpay);

            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "Select fc_id,invoice_id,create_user_id,create_app_name,invoice_case_pmt_amt,pmt_reject_reason_cd from invoice_case where invoice_case_id in (" + invoiceCaseIdCollection + ")";
            var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                dbConnection.Close();
                reader.Close();
                Assert.Fail("Insert InvoiceCase fail.");
            }
            while (reader.Read())
            {
                Assert.AreEqual(234769, int.Parse(reader["fc_id"].ToString()));
                Assert.AreEqual(""+working_user_id+"", reader["create_user_id"].ToString());
                Assert.AreEqual(string.Empty, reader["pmt_reject_reason_cd"].ToString());
                Assert.AreEqual(string.Empty, reader["invoice_case_pmt_amt"].ToString());
            }
        }

        [TestMethod()]
        public void UpdateInvoiceCase_PayCase_Successful()
        {
            InvoiceDAO target = new InvoiceDAO(); // TODO: Initialize to an appropriate value
            //Get invoice Set which was inserted in the init method
            InvoiceSetDTO actual = target.InvoiceSetGet(invoiceId);
            foreach (var i in actual.InvoiceCases)
                i.PaidDate = new DateTime(2007, 4, 4).ToShortDateString();
            actual.ChangeLastDate = new DateTime(2007, 4, 4);
            actual.ChangeLastAppName = "Unit TEst";
            actual.ChangeLastUserId = "1";
            //set payment id
            actual.InvoicePaymentId = paymentId;
            string invoiceCaseIdCollection = "";
            foreach (var i in actual.InvoiceCases)
                invoiceCaseIdCollection = invoiceCaseIdCollection + i.InvoiceCaseId.ToString() + ",";
            invoiceCaseIdCollection = invoiceCaseIdCollection.Remove(invoiceCaseIdCollection.Length - 1, 1);
            //set RejectReason
            bool result = target.UpdateInvoiceCase(actual, invoiceCaseIdCollection, InvoiceCaseUpdateFlag.Pay);
            Assert.IsTrue(result);
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            var command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandText = "Select fc_id,invoice_id,create_user_id,create_app_name,invoice_case_pmt_amt,invoice_case_bill_amt,pmt_reject_reason_cd from invoice_case where invoice_case_id in (" + invoiceCaseIdCollection + ")";
            var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                dbConnection.Close();
                reader.Close();
                Assert.Fail("Insert InvoiceCase fail.");
            }
            while (reader.Read())
            {
                Assert.AreEqual(234769, int.Parse(reader["fc_id"].ToString()));
                Assert.AreEqual(""+working_user_id+"", reader["create_user_id"].ToString());
                Assert.AreEqual(string.Empty, reader["pmt_reject_reason_cd"].ToString());
                Assert.AreEqual(reader["invoice_case_bill_amt"].ToString(), reader["invoice_case_pmt_amt"].ToString());
            }
        }

        [TestMethod()]
        public void UpdateInvoiceCase_PayCase_Fail()
        {
            InvoiceDAO target = new InvoiceDAO(); // TODO: Initialize to an appropriate value
            //Get invoice Set which was inserted in the init method
            InvoiceSetDTO actual = target.InvoiceSetGet(invoiceId);
            foreach (var i in actual.InvoiceCases)
                i.PaidDate = new DateTime(2007, 4, 4).ToShortDateString();
            actual.ChangeLastDate = new DateTime(2007, 4, 4);
            actual.ChangeLastAppName = "Unit TEst";
            actual.ChangeLastUserId = "1";
            //set payment id
            actual.InvoicePaymentId = 13423425;
            string invoiceCaseIdCollection = actual.InvoiceCases[1].InvoiceCaseId.ToString() + "," + actual.InvoiceCases[2].InvoiceCaseId.ToString();
            //set RejectReason
            //reject invoiceCase 1 and invoiceCase2
            bool result = target.UpdateInvoiceCase(actual, invoiceCaseIdCollection, InvoiceCaseUpdateFlag.Pay);
            Assert.IsFalse(result);
        }
        //[ExpectedException (typeof(DataValidationException),"Unable to process the reconciliation file. The Invoice Case ID in row 0 is not valid for the funding source selected.")]
        //[TestMethod()]
        //public void BackEndPreProcessingTest_Fail()
        //{
        //    InvoiceDAO target = new InvoiceDAO(); // TODO: Initialize to an appropriate value
        //    //Get invoice Set which was inserted in the init method
        //    string xmlString = "<invoice_cases>"
        //                           + "<invoice_case row_index=\"0\" invoice_case_id=\"924353739\" invoice_case_pmt_amt=\"0\" reject_reason_code=\"BK\" />"
        //                      +"</invoice_cases>";
        //    target.BackEndPreProcessing(xmlString);
        //}
        

    }

}
