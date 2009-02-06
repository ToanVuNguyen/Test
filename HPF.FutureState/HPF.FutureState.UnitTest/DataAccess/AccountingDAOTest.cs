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
//        [classinitialize()]
//        public static void myclassinitialize(testcontext testcontext)
//        {//agency, agency_payable_case, agency_payable
//            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
//            dbConnection.Open();
//            //insert AGENCY(agency_name:payable test---create_user_id:1)
//            string strsql = @"INSERT INTO [Agency]([agency_name],[contact_fname],[contact_lname],[phone],[fax]
//           ,[email],[active_ind],[hud_agency_num],[hud_agency_sub_grantee_num],[create_dt],[create_user_id]
//           ,[create_app_name],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name])VALUES
//           ('payable test','payable','test',99990000,00009999,'payable@yahoo.com','N','9999','9999','2222/1/1'
//            ,1,'payable test','2222/1/1','aaaa','aaaa')";
//            var command = new SqlCommand(strsql, dbConnection);
//            command.ExecuteNonQuery();

//            //insert foreclosure_case test info
//            strsql = "Insert into foreclosure_case "
//               + " (agency_id, program_id, intake_dt"
//               + ", borrower_fname, borrower_lname, primary_contact_no"
//               + ", contact_addr1, contact_city, contact_state_cd, contact_zip"
//               + ", funding_consent_ind, servicer_consent_ind, counselor_email"
//               + ", counselor_phone, opt_out_newsletter_ind, opt_out_survey_ind"
//               + ", do_not_call_ind, owner_occupied_ind, primary_residence_ind"
//               + ", counselor_fname, counselor_lname, counselor_id_ref"
//               + ", prop_zip, agency_case_num, borrower_last4_SSN"
//               + ", chg_lst_app_name, chg_lst_user_id, chg_lst_dt ,create_app_name , create_user_id,create_dt ) values "
//               + " (" + "1" + ", 1, '" + DateTime.Now + "'"
//               + ", '" + "accounting test" + "', 'accounting test', 'pcontactno'"
//               + ", 'address1', 'cty', 'scod', 'czip'"
//               + ", 'Y', 'Y', 'email'"
//               + ", 'phone', 'Y', 'Y'"
//               + ", 'Y', 'Y', 'Y'"
//               + ", 'cfname', 'clname', 'cidref'"
//               + ", '" + "9999" + "', '" + "abc" + "', '" + "1111" + "'"
//               + ", 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', 'HPF', '" + DateTime.Now + "' )";
//            command = new SqlCommand(strsql,dbConnection);
//            command.ExecuteNonQuery();

//            //get Agency id to insert agency_payable
//            strsql = @"select fc_id from forclosure_case where borrower_fname='accounting test'";
//            command = new SqlCommand(strsql, dbConnection);
//            int  fc_Id = Convert.ToInt16(command.ExecuteScalar());

//            //get Agency id to insert agency_payable
//            strsql = @"select agency_id from agency where agency_name='payable test'";
//            command = new SqlCommand(strsql, dbConnection);
//            int agencyID = Convert.ToInt16(command.ExecuteScalar());

//            //insert AGENCY_PAYABLE(agencyid:agencyID---status_cd:'payable test')
//            strsql = @"INSERT INTO agency_payable([agency_id],[pmt_dt],[status_cd],[period_start_dt]
//           ,[period_end_dt],[pmt_comment],[accounting_link_TBD],[create_dt],[create_user_id]
//           ,[create_app_name],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name],[agency_payable_pmt_amt])
//           values(" + agencyID + ",'2000/1/1','payable test','2222/1/1','2222/1/1','test comment','accounting_link_TBD_test','2000/1/1',1,'tester','2000/1/1','tester','tester',0.0)";
//            command = new SqlCommand(strsql, dbConnection);
//            command.ExecuteNonQuery();

//            //insert AGENCY_PAYABLE_CASE
//            dbConnection.Close();

//        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //    //Delete data test AGENCY_PAYABLE(status_cd='accounting payable test')
        //    var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
        //    string strsql = @"delete from agency_payable where status_cd='payable test'";
        //    var command = new SqlCommand(strsql, dbConnection);
        //    dbConnection.Open();
        //    command.ExecuteNonQuery();

        //    //Delete data test AGENCY(agency_name='accounting payable test')
        //    strsql = @"delete from agency where agency_name='payable test'";
        //    command = new SqlCommand(strsql, dbConnection);
        //    command.ExecuteNonQuery();
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
        ///A test for UpdateForclosureCase
        ///</summary>
        //[TestMethod()]
        //public void UpdateForclosureCaseTest()
        //{
        //    AccountingDAO_Accessor target = new AccountingDAO_Accessor(); // TODO: Initialize to an appropriate value
        //    string NeverBillReason = string.Empty; // TODO: Initialize to an appropriate value
        //    string NeverPayReason = string.Empty; // TODO: Initialize to an appropriate value
        //    int Fc_ID = 0; // TODO: Initialize to an appropriate value
        //    target.UpdateForclosureCase(NeverBillReason, NeverPayReason, Fc_ID);
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}

        /// <summary>
        ///A test for DisplayInvoiceCase
        ///</summary>
        //[TestMethod()]
        //public void DisplayInvoiceCaseTest()
        //{
        //    AccountingDAO_Accessor target = new AccountingDAO_Accessor(); // TODO: Initialize to an appropriate value
        //    int CaseID = 0; // TODO: Initialize to an appropriate value
        //    BillingInfoDTOCollection expected = null; // TODO: Initialize to an appropriate value
        //    BillingInfoDTOCollection actual;
        //    actual = target.DisplayInvoiceCase(CaseID);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for DisplayAgencyPayable
        ///</summary>
        //[TestMethod()]
        //public void DisplayAgencyPayableTest()
        //{
        //    AccountingDAO_Accessor target = new AccountingDAO_Accessor(); // TODO: Initialize to an appropriate value
        //    int CaseID = 0; // TODO: Initialize to an appropriate value
        //    AgencyPayableCaseDTOCollection expected = null; // TODO: Initialize to an appropriate value
        //    AgencyPayableCaseDTOCollection actual;
        //    // get Funding Source Id of test data.
        //    var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
        //    string strsql = @"select funding_source_id from funding_source where funding_source_name='fs thao test'";
        //    var command = new SqlCommand(strsql, dbConnection);
        //    dbConnection.Open();
        //    int fsID = Convert.ToInt16(command.ExecuteScalar());

        //    //search criteria
        //    searchCriteria.FundingSourceId = fsID;
        //    searchCriteria.PeriodStart = Convert.ToDateTime("2222/1/1");
        //    searchCriteria.PeriodEnd = Convert.ToDateTime("2222/1/1");
        //    //search criteria  

        //    List<string> expectlist = new List<string> { fsID.ToString(), "fs thao test", "invoice thao test", "1/1/2222" };

        //    InvoicePaymentDTOCollection actual = target.InvoicePaymentSearch(searchCriteria);
        //    List<string> actuallist = new List<string> {actual[0].FundingSourceID.ToString(),actual[0].FundingSourceName,
        //    actual[0].PaymentNum,actual[0].PaymentDate.ToShortDateString()};
        //    CollectionAssert.AreEquivalent(expectlist, actuallist);
        //    dbConnection.Close();
        //}

        /// <summary>
        ///A test for DisplayAccounting
        ///</summary>
        //[TestMethod()]
        //public void DisplayAccountingTest()
        //{
        //    AccountingDAO_Accessor target = new AccountingDAO_Accessor(); // TODO: Initialize to an appropriate value
        //    int Fc_ID = 0; // TODO: Initialize to an appropriate value
        //    AccountingDTO expected = null; // TODO: Initialize to an appropriate value
        //    AccountingDTO actual;
        //    actual = target.DisplayAccounting(Fc_ID);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}
    }
}
