using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace HPF.FutureState.UnitTest
{


    /// <summary>
    ///This is a test class for AccountingDAOTest and is intended
    ///to contain all AccountingDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AccountingDAOTest
    {

        static string first_name = "accounting test";
        static string acct_num = "test test";
        static string working_user_id = "accounting test user";

        static string agency_name = "accounting agency";

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
        [ClassInitialize()]
        public static void myclassinitialize(TestContext testcontext)
        {
            ClearTestData();
            InsertTestData();
        }

        private static void InsertTestData()
        {
            //agency, agency_payable_case, agency_payable
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();

            //insert AGENCY(agency_name:payable test---create_user_id:working_user_id)
            string strsql = @"INSERT INTO [Agency]([agency_name],[contact_fname],[contact_lname],[phone],[fax]
           ,[email],[active_ind],[hud_agency_num],[hud_agency_sub_grantee_num],[create_dt],[create_user_id]
           ,[create_app_name],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name])VALUES
           ('payable test','payable','test',99990000,00009999,'payable@yahoo.com','N','9999','9999','2222/1/1'
            ,'" + working_user_id + "','payable test','2222/1/1','aaaa','aaaa')";
            var command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //get Agency id to insert agency_payable
            strsql = @"select agency_id from agency where create_user_id='" + working_user_id + "'";
            command = new SqlCommand(strsql, dbConnection);
            int agency_id = Convert.ToInt32(command.ExecuteScalar());

            //insert program for test
            strsql = @"INSERT INTO [hpf].[dbo].[program]
           ([program_name],[program_comment],[create_dt],[create_user_id],[create_app_name]
           ,[chg_lst_dt] ,[chg_lst_user_id],[chg_lst_app_name])
            VALUES('accounting test','test','2/2/2222','" + working_user_id + "','test','2/2/2222','test' ,'test')";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //select program id test data
            strsql = @"select program_id from program where create_user_id='" + working_user_id + "'";
            command = new SqlCommand(strsql, dbConnection);
            int program_id = Convert.ToInt32(command.ExecuteScalar());

            //insert foreclosure_case test info
            strsql = "Insert into foreclosure_case "
                + " (agency_id, program_id, intake_dt"
               + ", borrower_fname, borrower_lname, primary_contact_no"
               + ", contact_addr1, contact_city, contact_state_cd, contact_zip"
               + ", funding_consent_ind, servicer_consent_ind, counselor_email"
               + ", counselor_phone, opt_out_newsletter_ind, opt_out_survey_ind"
               + ", do_not_call_ind, owner_occupied_ind, primary_residence_ind"
               + ", counselor_fname, counselor_lname, counselor_id_ref"
               + ", prop_zip, agency_case_num, borrower_last4_SSN"
               + ", chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name"
               + ", create_user_id,create_dt,never_pay_reason_cd,never_bill_reason_cd,prop_addr1,prop_city,prop_state_cd,duplicate_ind ) values "

               + " (" + agency_id + "," + program_id + ", '" + DateTime.Now + "'"
               + ",'" + first_name + "', 'accounting test', 'pcontactno'"
               + ", 'address1', 'cty', 'scod', 'czip'"
               + ", 'Y', 'Y', 'email'"
               + ", 'phone', 'Y', 'Y'"
               + ", 'Y', 'Y', 'Y'"
               + ", 'cfname', 'clname', 'cidref'"
               + ", '" + "9999" + "', '" + "abc" + "', '" + "1111" + "'"
               + ", 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', '" + working_user_id + "', '" + DateTime.Now + "','accounting pay','accounting bill','nguyen hong','hcm','bt','N' )";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //get fc_id to insert agency_payable
            strsql = @"select fc_id from foreclosure_case where create_user_id='" + working_user_id + "'";
            command = new SqlCommand(strsql, dbConnection);
            int fc_id = Convert.ToInt32(command.ExecuteScalar());

            //insert AGENCY_PAYABLE(agencyid:agencyID---status_cd:'payable test')
            strsql = @"INSERT INTO agency_payable([agency_id],[pmt_dt],[status_cd],[period_start_dt]
           ,[period_end_dt],[pmt_comment],[accounting_link_TBD],[create_dt],[create_user_id]
           ,[create_app_name],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name],[agency_payable_pmt_amt])
           values(" + agency_id + ",'2000/1/1','payable test','2222/1/1','2222/1/1','test comment','accounting_link_TBD_test','2000/1/1','" + working_user_id + "','tester','2000/1/1','tester','tester',0.0)";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //get  agency_payable_id
            strsql = @"select agency_payable_id from agency_payable where create_user_id='" + working_user_id + "'";
            command = new SqlCommand(strsql, dbConnection);
            int agency_payable_id = Convert.ToInt32(command.ExecuteScalar());

            //insert AGENCY_PAYABLE_CASE
            strsql = @"INSERT INTO [hpf].[dbo].[agency_payable_case]
           ([fc_id],[agency_payable_id],[pmt_dt],[create_dt],[create_user_id],[create_app_name]
           ,[chg_lst_dt],[chg_lst_user_id] ,[chg_lst_app_name],[NFMC_difference_eligible_ind]
           )
            VALUES
           (" + fc_id + "," + agency_payable_id + ",'2/2/2222','2/2/2222','" + working_user_id + "','test','2/2/2222','test','test','N')";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();
            //
            //insert test data for DisplayInvoiceCase
            //invoice ,funding_source, invoice_case, invoice_payment , case_loan 

            //insert test data to FUNDING_SOURCE
            command.CommandText = @"insert into [dbo].funding_source([funding_source_name],[funding_source_comment]
           ,[city],[state_cd],[create_dt],[create_user_id],[create_app_name],[chg_lst_dt],[chg_lst_user_id]
           ,[chg_lst_app_name],[funding_source_abbrev]) values ('fs accounting test','fs test','hcm test'
           ,'test','1/1/2222','" + working_user_id + "','fs test app','1/1/2222','Test data','CCRC','5')";
            command.ExecuteNonQuery();
            //-- get Funding Source Id of test data .
            command.CommandText = @"select funding_source_id from funding_source where create_user_id='" + working_user_id + "'";
            int fs_id = Convert.ToInt32(command.ExecuteScalar());

            //insert test data to INVOICE
            command.CommandText = "Insert Into Invoice(invoice_dt,funding_source_id,period_start_dt, period_end_dt, create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name,invoice_comment)" +
                                    " values ('2/2/2222'," + fs_id + ",'1/1/2107', '1/1/2108', '1/1/2208', '" + working_user_id + "', 'test', '1/1/2008', 'test', 'test','test invoice')";
            command.ExecuteNonQuery();
            //--get invoice_id
            command.CommandText = "select invoice_id from invoice where create_user_id='" + working_user_id + "'";
            int invoice_id = Convert.ToInt32(command.ExecuteScalar());

            //insert test data  INVOICE_CASE
            command.CommandText = "Insert Into invoice_case(invoice_case_pmt_amt,invoice_case_bill_amt,fc_id,invoice_id,create_dt, create_user_id, create_app_name, chg_lst_dt, chg_lst_user_id, chg_lst_app_name,pmt_reject_reason_cd)" +
                                   " values (99999,99999," + fc_id + "," + invoice_id + ",'1/1/2208', '" + working_user_id + "', 'InvoiceCase1', '1/1/2008', 'InvoiceCase1', 'test','no accounting')";
            command.ExecuteNonQuery();

            //insert INVOICE_PAYMENT
            command.CommandText = @"INSERT INTO [invoice_payment]([funding_source_id],[pmt_num] ,[pmt_dt],[pmt_cd]
            ,[create_dt] ,[create_user_id],[create_app_name],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name])
            VALUES (" + fs_id + ",'invoice thao test','2/2/2222','test test','1/1/2222','" + working_user_id + "','fs test app','1/1/2222','Test data','CCRC')";
            command.ExecuteNonQuery();

            //insert SERVICER to insert test data to CASE_LOAN
            command.CommandText = @"INSERT INTO [servicer]([servicer_name],[create_dt],[create_user_id]" +
           ",[create_app_name],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name])VALUES" +
           "('test servicer','2/2/2222','" + working_user_id + "','HPF','2/2/2222','test','test')";
            command.ExecuteNonQuery();
            //--get servicer_id
            command.CommandText = "select servicer_id from servicer where create_user_id='" + working_user_id + "'";
            int servicer_id = Convert.ToInt32(command.ExecuteScalar());
            //insert test data CASE_LOAN
            command.CommandText = @"INSERT INTO [dbo].[case_loan]" +
           "([fc_id],[servicer_id],[acct_num],[loan_1st_2nd_cd],[create_dt],[create_app_name]" +
           ",[create_user_id],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name])VALUES" +
           "(" + fc_id + "," + servicer_id + ",99999,'1st','2/2/2222','test','" + working_user_id + "','2/2/2222','test','test')";
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {

            ClearTestData();

        }

        private static void ClearTestData()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();

            //Delete data test AGENCY_PAYABLE_CASE
            string strsql = @"delete from agency_payable_case where create_user_id='" + working_user_id + "'";
            var command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //Delete data test AGENCY_PAYABLE
            strsql = @"delete from agency_payable where create_user_id='" + working_user_id + "'";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //Delete ACTIVITY_LOG
            //get fc_id
            strsql = @"select fc_id from foreclosure_case where create_user_id='" + working_user_id + "'";
            command = new SqlCommand(strsql, dbConnection);
            int fc_id = Convert.ToInt32(command.ExecuteScalar());
            strsql = @"delete activity_log where fc_id=" + fc_id;
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            
            //Delete data test AGENCY
            strsql = @"delete from agency where create_user_id='" + working_user_id + "'";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //Delete data test PROGRAM
            strsql = @"delete from program where create_user_id='" + working_user_id + "'";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();
            //
            //delete data test DisplayInvoiceCase
            //case_loan,servicer,invoice_case,invoice_payment,invoice,funding_source
            //delete CASE_LOAN
            command.CommandText = "delete from case_loan where create_user_id='" + working_user_id + "'";
            command.ExecuteNonQuery();
            //delete SERVICER
            command.CommandText = "delete from servicer where create_user_id='" + working_user_id + "'";
            command.ExecuteNonQuery();
            //delete INVOICE_CASE
            command.CommandText = "delete from invoice_case where create_user_id='" + working_user_id + "'";
            command.ExecuteNonQuery();
            //delete INVOICE_PAYMENT
            command.CommandText = "delete from invoice_payment where create_user_id='" + working_user_id + "'";
            command.ExecuteNonQuery();
            //delete INVOICE
            command.CommandText = "delete from invoice where create_user_id='" + working_user_id + "'";
            command.ExecuteNonQuery();
            //delete FUNDING_SOURCE
            command.CommandText = "delete from funding_source where create_user_id='" + working_user_id + "'";
            command.ExecuteNonQuery();

            //Delete data test FORECLOSURE 
            strsql = @"delete from  foreclosure_case where create_user_id='"+working_user_id+"'";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        // Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
        }

        #endregion


        /// <summary>
        ///A test for UpdateForclosureCase
        ///</summary>
        [TestMethod()]
        public void UpdateForclosureCaseTest()
        {
            AccountingDAO_Accessor target = new AccountingDAO_Accessor(); // TODO: Initialize to an appropriate value
            ForeclosureCaseDTO foreclosureCase = new ForeclosureCaseDTO();
            //
            string NeverBillReason = null;
            string NeverPayReason = null; // TODO: Initialize to an appropriate value
            //get current fc_id
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            string strsql = @"select fc_id from foreclosure_case where create_user_id='" + working_user_id + "'";
            var command = new SqlCommand(strsql, dbConnection);
            //set foreclosureCase data
            foreclosureCase.FcId = Convert.ToInt32(command.ExecuteScalar());
            foreclosureCase.NeverPayReasonCd = "pay accounting";
            foreclosureCase.NeverBillReasonCd = "bill accounting";
            foreclosureCase.ChangeLastDate = DateTime.Now;
            foreclosureCase.ChangeLastUserId = working_user_id;
            foreclosureCase.ChangeLastAppName = "HPF";
            target.UpdateForeclosureCase(foreclosureCase);
            //check update successful or not
            strsql = @"select * from foreclosure_case where create_user_id='" + working_user_id + "'";
            command = new SqlCommand(strsql, dbConnection);
            var dr = command.ExecuteReader();
            if (dr.HasRows)
                while (dr.Read())
                {
                    NeverPayReason = dr["never_pay_reason_cd"].ToString();
                    NeverBillReason = dr["never_bill_reason_cd"].ToString();
                }
            Assert.AreEqual(NeverPayReason, "pay accounting");
            Assert.AreEqual(NeverBillReason, "bill accounting");
            dbConnection.Close();
        }

        /// <summary>
        ///A test for DisplayInvoiceCase
        ///</summary>
        /// 
        [TestMethod()]
        public void DisplayInvoiceCaseTest()
        {
            AccountingDAO_Accessor target = new AccountingDAO_Accessor(); // TODO: Initialize to an appropriate value
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            //
            string strsql = @"select fc_id from foreclosure_case where create_user_id='" + working_user_id + "' ";
            var command = new SqlCommand(strsql, dbConnection);
            int CaseID = Convert.ToInt32(command.ExecuteScalar());
            BillingInfoDTOCollection actual = new BillingInfoDTOCollection();
            BillingInfoDTO expect = new BillingInfoDTO();
            //actual 
            actual = target.DisplayInvoiceCase(CaseID);
            //expect
            expect.InvoiceDate = Convert.ToDateTime("2/2/2222");
            expect.FundingSourceName = "fs accounting test";
            command.CommandText = "select invoice_id from invoice where create_user_id='" + working_user_id + "'";
            int invoice_id = Convert.ToInt32(command.ExecuteScalar());
            expect.InvoiceId = invoice_id;
            expect.Loan = "99999";
            expect.InDisputeIndicator = "1st";
            expect.InvoiceCaseBillAmount = 99999;
            expect.InvoiceCasePaymentAmount = 99999;
            expect.PaidDate = Convert.ToDateTime("2/2/2222");
            expect.PaymentRejectReasonCode = "no accounting";
            Assert.IsFalse(!CompareBillingInfoDTO(actual[0], expect));
            dbConnection.Close();
        }
        private bool CompareBillingInfoDTO(BillingInfoDTO actual, BillingInfoDTO expect)
        {

            if (expect.InvoiceDate != actual.InvoiceDate) return false;
            if (expect.FundingSourceName != actual.FundingSourceName) return false;
            if (expect.InvoiceId != actual.InvoiceId) return false;
            if (expect.Loan != actual.Loan) return false;
            if (expect.InDisputeIndicator != actual.InDisputeIndicator) return false;
            if (expect.InvoiceCaseBillAmount != actual.InvoiceCaseBillAmount) return false;
            if (expect.InvoiceCasePaymentAmount != actual.InvoiceCasePaymentAmount) return false;
            if (expect.PaidDate != actual.PaidDate) return false;
            if (expect.PaymentRejectReasonCode != actual.PaymentRejectReasonCode) return false;
            return true;
        }
        /// <summary>
        ///A test for DisplayAgencyPayable
        ///</summary>
        [TestMethod()]
        public void DisplayAgencyPayableTest()
        {
            AccountingDAO_Accessor target = new AccountingDAO_Accessor(); // TODO: Initialize to an appropriate value
            AgencyPayableCaseDTOCollection actual = new AgencyPayableCaseDTOCollection();

            // get Funding Source Id of test data.
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string strsql = @"select fc_id from agency_payable_case where create_user_id='" + working_user_id + "'";
            var command = new SqlCommand(strsql, dbConnection);
            dbConnection.Open();
            int fc_id = Convert.ToInt32(command.ExecuteScalar());
            //
            actual = target.DisplayAgencyPayable(fc_id);
            Assert.IsFalse(!CompareAgencyPayableCaseDTO(actual[0]));
            dbConnection.Close();
        }
        protected bool CompareAgencyPayableCaseDTO(AgencyPayableCaseDTO actual)
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            //get fc_id
            string strsql = @"select fc_id from agency_payable_case where create_user_id='" + working_user_id + "'";
            var command = new SqlCommand(strsql, dbConnection);
            int fc_id = Convert.ToInt32(command.ExecuteScalar());
            //get agencypayableid
            strsql = @"select agency_payable_id from agency_payable_case where create_user_id='" + working_user_id + "'";
            command = new SqlCommand(strsql, dbConnection);
            int agencypayableid = Convert.ToInt32(command.ExecuteScalar());
            dbConnection.Close();
            //compare expected values
            if (actual.AgencyName != "payable test") return false;
            if (actual.AgencyPayableId != agencypayableid) return false;
            return true;
        }
        /// <summary>
        ///A test for DisplayAccounting
        ///</summary>
        [TestMethod()]
        public void DisplayAccountingTest()
        {
            AccountingDAO_Accessor target = new AccountingDAO_Accessor(); // TODO: Initialize to an appropriate value
            AccountingDTO actual = new AccountingDTO(); // TODO: Initialize to an appropriate value
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            string strsql = @"select fc_id from foreclosure_case where create_user_id='" + working_user_id + "'";
            var command = new SqlCommand(strsql, dbConnection);
            int fc_id = Convert.ToInt32(command.ExecuteScalar());
            actual = target.DisplayAccounting(fc_id);
            Assert.AreEqual(actual.NerverPayReason, "accounting pay");
            Assert.AreEqual(actual.NeverBillReason, "accounting bill");
            dbConnection.Close();
        }
    }
}
