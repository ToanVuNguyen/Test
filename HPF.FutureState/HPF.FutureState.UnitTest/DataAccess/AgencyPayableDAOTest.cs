using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Globalization;
using System.Collections.Generic;
using HPF.FutureState.Web.Security;
namespace HPF.FutureState.UnitTest
{


    /// <summary>
    ///This is a test class for AgencyPayableDAOTest and is intended
    ///to contain all AgencyPayableDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AgencyPayableDAOTest
    {
        static string working_user_id = "agencypayable test user";
        static string agency_name = "payable test";
        static string status_cd = "payable test";

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
        public static void MyClassInitialize(TestContext testContext)
        {
            ClearTestData();
            InsertTestData();

        }

        private static void InsertTestData()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            //create test data:SearchAgencyPayableTest
            //insert AGENCY
            string strsql = @"INSERT INTO [Agency]([agency_name],[contact_fname],[contact_lname],[phone],[fax]
           ,[email],[active_ind],[hud_agency_num],[hud_agency_sub_grantee_num],[create_dt],[create_user_id]
           ,[create_app_name],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name])VALUES
           ('payable test','payable','test',99990000,00009999,'payable@yahoo.com','N','9999','9999','2222/1/1'
            ,'"+working_user_id+"','payable test','2222/1/1','aaaa','aaaa')";
            var command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //get agencyid test data
            strsql = @"select agency_id from agency where create_user_id='"+working_user_id+"'";
            command = new SqlCommand(strsql,dbConnection);
            int agency_id = Convert.ToInt32(command.ExecuteScalar());

            //insert AGENCY_PAYABLE
            strsql = @"INSERT INTO agency_payable([agency_id],[pmt_dt],[status_cd],[period_start_dt]
           ,[period_end_dt],[pmt_comment],[accounting_link_TBD],[create_dt],[create_user_id]
           ,[create_app_name],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name],[agency_payable_pmt_amt])
           values("+agency_id+",'2000/1/1','payable test','2222/1/1','2222/1/1','test comment','accounting_link_TBD_test','2000/1/1','"+working_user_id+"','tester','2000/1/1','tester','tester',0.0)";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

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

            //insert FORECLOSURE_CASE
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
              + ", create_user_id,create_dt,never_pay_reason_cd,never_bill_reason_cd,prop_addr1,prop_city,prop_state_cd,duplicate_ind) values "

              + " (" + agency_id + "," + program_id + ", '" + DateTime.Now + "'"
              + ",'thao test', 'accounting test', 'pcontactno'"
              + ", 'address1', 'cty', 'scod', 'czip'"
              + ", 'Y', 'Y', 'email'"
              + ", 'phone', 'Y', 'Y'"
              + ", 'Y', 'Y', 'Y'"
              + ", 'cfname', 'clname', 'cidref'"
              + ", '" + "9999" + "', '" + "abc" + "', '" + "1111" + "'"
              + ", 'HPF' ,'HPF' ,'" + DateTime.Now + "', 'HPF', '" + working_user_id + "', '" + DateTime.Now + "','accounting pay','accounting bill','nguyen hong','hcm','bt','N' )";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //close connection
            dbConnection.Close();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            ClearTestData();

        }

        private static void ClearTestData()
        {
            //Delete data test AGENCY_PAYABLE(status_cd='payable test')
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            //
            //get payable_id from AGENCY_PAYABLE data test status_cd='payable test'
            string strsql = @"select agency_payable_id from agency_payable where create_user_id='" + working_user_id + "'";
            var command = new SqlCommand(strsql, dbConnection);
            int agency_payable_id = Convert.ToInt32(command.ExecuteScalar());
            //
            strsql = @"delete from agency_payable_case where agency_payable_id=" + agency_payable_id;
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            strsql = @"delete from agency_payable where create_user_id='" + working_user_id + "'";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //Delete test data insert to InsertAgencyPayableCaseTest
            //
           
            //Delete test data insert to InsertAgencyPayableTest
            strsql = @"delete from agency_payable where [status_cd] = 'insert agency'";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //Delete data test FORECLOSURE 
            strsql = @"delete from  foreclosure_case where create_user_id='" + working_user_id + "'";
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

            dbConnection.Close();
        }
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
        ///A test for SearchAgencyPayable
        ///</summary>
        [TestMethod()]
        public void SearchAgencyPayableTest()
        {
            //Connection
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            //
            AgencyPayableDAO_Accessor target = new AgencyPayableDAO_Accessor(); // TODO: Initialize to an appropriate value
            AgencyPayableSearchCriteriaDTO agencyPayableCriteria = new AgencyPayableSearchCriteriaDTO(); // TODO: Initialize to an appropriate value
            //get agencyid test data
            string strsql = @"select agency_id from agency where create_user_id='" + working_user_id + "'";
            var command = new SqlCommand(strsql, dbConnection);
            int agency_id = Convert.ToInt32(command.ExecuteScalar());
            // search criteria
            agencyPayableCriteria.AgencyId = agency_id;
            agencyPayableCriteria.PeriodStartDate = Convert.ToDateTime("2222/1/1");
            agencyPayableCriteria.PeriodEndDate = Convert.ToDateTime("2222/1/1");
            
            //actual search data  
            AgencyPayableDTOCollection actual = target.SearchAgencyPayable(agencyPayableCriteria);
            //compare actual and expect
            Assert.AreEqual(actual[0].PeriodStartDate.Value.ToShortDateString(), "1/1/2222");
            Assert.AreEqual(actual[0].PeriodEndDate.Value.ToShortDateString(), "1/1/2222");
            Assert.AreEqual(actual[0].PaymentComment, "test comment");
            Assert.AreEqual(actual[0].StatusCode, "payable test");
            dbConnection.Close();
        }
        /// ng StatusCode {g<summary>
        ///ATime PaymentDate  test for InsertAgencyPayableCase
        ///</summary>
        [TestMethod()]
        public void InsertAgencyPayableCaseTest()
        {
            AgencyPayableDAO_Accessor target = new AgencyPayableDAO_Accessor(); // TODO: Initialize to an appropriate value
            AgencyPayableCaseDTO agencyPayableCaseInsert = new AgencyPayableCaseDTO(); // TODO: Initialize to an appropriate value
            AgencyPayableCaseDTO agencyPayableCaseSelect = new AgencyPayableCaseDTO();

            //get payable_id from AGENCY_PAYABLE data test status_cd='payable test'
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string strsql = @"select agency_payable_id from agency_payable where status_cd='payable test'";
            var command = new SqlCommand(strsql, dbConnection);
            dbConnection.Open();
            int agency_payable_id = Convert.ToInt32(command.ExecuteScalar());
            //get fc_id
            strsql = @"select fc_id from foreclosure_case where create_user_id='"+working_user_id+"'";
            command = new SqlCommand(strsql, dbConnection);
            int fc_id =Convert.ToInt32(command.ExecuteScalar());
            dbConnection.Close();

            //agencyPayableCase.
            agencyPayableCaseInsert.ForeclosureCaseId = fc_id;
            agencyPayableCaseInsert.AgencyPayableId = agency_payable_id;
            agencyPayableCaseInsert.PaymentDate = Convert.ToDateTime("2000/1/1");
            agencyPayableCaseInsert.PaymentAmount = 99999;
            agencyPayableCaseInsert.CreateDate = Convert.ToDateTime("2000/1/1");
            agencyPayableCaseInsert.CreateUserId = working_user_id;
            agencyPayableCaseInsert.CreateAppName = "HPF";
            agencyPayableCaseInsert.ChangeLastDate = Convert.ToDateTime("2000/1/1");
            agencyPayableCaseInsert.ChangeLastUserId = "HPF";
            agencyPayableCaseInsert.ChangeLastAppName = "HPF";
            agencyPayableCaseInsert.NFMCDifferenceEligibleInd = "N";
            target.BeginTran();
            target.InsertAgencyPayableCase(agencyPayableCaseInsert);
            target.CommitTran();
            // select data from AgencyPayableCase
            dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            dbConnection.Open();
            strsql = "select * from agency_payable_case where agency_payable_id=" + agency_payable_id;
            command = new SqlCommand(strsql, dbConnection);

            var dr = command.ExecuteReader();
            if (dr.HasRows)
                while (dr.Read())
                {
                    agencyPayableCaseSelect.ForeclosureCaseId = Convert.ToInt32(dr["fc_id"]);
                    agencyPayableCaseSelect.AgencyPayableId = Convert.ToInt32(dr["agency_payable_id"]);
                    agencyPayableCaseSelect.PaymentDate = Convert.ToDateTime(dr["pmt_dt"]);
                    agencyPayableCaseSelect.PaymentAmount = Convert.ToDouble(dr["pmt_amt"]);
                    agencyPayableCaseSelect.CreateDate = Convert.ToDateTime(dr["create_dt"]);
                    agencyPayableCaseSelect.CreateUserId = dr["create_user_id"].ToString();
                    agencyPayableCaseSelect.CreateAppName = dr["create_app_name"].ToString();
                    agencyPayableCaseSelect.ChangeLastDate = Convert.ToDateTime(dr["chg_lst_dt"]);
                    agencyPayableCaseSelect.ChangeLastUserId = Convert.ToString(dr["chg_lst_user_id"]);
                    agencyPayableCaseSelect.ChangeLastAppName = Convert.ToString(dr["chg_lst_app_name"]);
                    agencyPayableCaseSelect.NFMCDifferenceEligibleInd = Convert.ToString(dr["NFMC_difference_eligible_ind"]);
                }
            Assert.IsFalse(!CompareAgencyPayableCaseDTO(agencyPayableCaseInsert, agencyPayableCaseSelect));
            dbConnection.Close();
            dr.Close();
        }
        private bool CompareAgencyPayableCaseDTO(AgencyPayableCaseDTO agencyPayableCaseInsert, AgencyPayableCaseDTO agencyPayableCaseSelect)
        {
            if (agencyPayableCaseSelect.ForeclosureCaseId != agencyPayableCaseInsert.ForeclosureCaseId) return false;
            if (agencyPayableCaseSelect.AgencyPayableId != agencyPayableCaseInsert.AgencyPayableId) return false;
            if (agencyPayableCaseSelect.PaymentAmount != agencyPayableCaseInsert.PaymentAmount) return false;
            if (agencyPayableCaseSelect.CreateDate != agencyPayableCaseInsert.CreateDate) return false;
            if (agencyPayableCaseSelect.CreateUserId != agencyPayableCaseInsert.CreateUserId) return false;
            if (agencyPayableCaseSelect.CreateAppName != agencyPayableCaseInsert.CreateAppName) return false;
            if (agencyPayableCaseSelect.ChangeLastDate != agencyPayableCaseInsert.ChangeLastDate) return false;
            if (agencyPayableCaseSelect.ChangeLastAppName != agencyPayableCaseInsert.ChangeLastAppName) return false;
            if (agencyPayableCaseSelect.ChangeLastUserId != agencyPayableCaseInsert.ChangeLastUserId) return false;
            //if (agencyPayableCaseSelect.NFMCDiffererencePaidInd != agencyPayableCaseInsert.NFMCDiffererencePaidInd) return false;
            if (agencyPayableCaseSelect.NFMCDifferenceEligibleInd != agencyPayableCaseInsert.NFMCDifferenceEligibleInd) return false;
            return true;
        }
        /// <summary>                                                                                       
        ///A test for InsertAgencyPayable
        ///</summary>
        [TestMethod()]
        public void InsertAgencyPayableTest()
        {
            AgencyPayableDAO_Accessor target = new AgencyPayableDAO_Accessor(); // TODO: Initialize to an appropriate value
            AgencyPayableDTO agencyPayableInsert = new AgencyPayableDTO(); // TODO: Initialize to an appropriate value
            AgencyPayableDTO agencyPayableSelect = new AgencyPayableDTO();
            agencyPayableInsert.AgencyId = 2;
            agencyPayableInsert.PaymentDate = Convert.ToDateTime("2000/1/1");
            agencyPayableInsert.StatusCode = "insert agency";
            agencyPayableInsert.PeriodStartDate = Convert.ToDateTime("2000/1/1");
            agencyPayableInsert.PeriodEndDate = Convert.ToDateTime("2222/1/1"); ;
            agencyPayableInsert.PaymentComment = "insert agency comment";
            agencyPayableInsert.AccountLinkTBD = "insert a";
            agencyPayableInsert.CreateDate = Convert.ToDateTime("2000/1/1");
            agencyPayableInsert.CreateUserId = working_user_id;
            agencyPayableInsert.CreateAppName = "HPF";
            agencyPayableInsert.ChangeLastDate = Convert.ToDateTime("2000/1/1");
            agencyPayableInsert.ChangeLastUserId = "HPF";
            agencyPayableInsert.ChangeLastAppName = "HPF";
            agencyPayableInsert.AgencyPayablePaymentAmount = 99999;
            target.BeginTran();
            target.InsertAgencyPayable(agencyPayableInsert);
            target.CommitTran();
            //select data from AgencyPayableCase
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string strsql = "select * from agency_payable where status_cd='insert agency'";
            var command = new SqlCommand(strsql, dbConnection);
            dbConnection.Open();
            var dr = command.ExecuteReader();
            if (dr.HasRows)
                while (dr.Read())
                {
                    agencyPayableSelect.AgencyId = Convert.ToInt32(dr["agency_id"]);
                    agencyPayableSelect.PaymentDate = Convert.ToDateTime(dr["pmt_dt"]);
                    agencyPayableSelect.StatusCode = dr["status_cd"].ToString();
                    agencyPayableSelect.PeriodStartDate = Convert.ToDateTime(dr["period_start_dt"]);
                    agencyPayableSelect.PeriodEndDate = Convert.ToDateTime(dr["period_end_dt"]);
                    agencyPayableSelect.PaymentComment = dr["pmt_comment"].ToString();
                    agencyPayableSelect.AccountLinkTBD = dr["accounting_link_TBD"].ToString();
                    agencyPayableSelect.CreateDate = Convert.ToDateTime(dr["create_dt"]);
                    agencyPayableSelect.CreateUserId = dr["create_user_id"].ToString();
                    agencyPayableSelect.CreateAppName = dr["create_app_name"].ToString();
                    agencyPayableSelect.ChangeLastDate = Convert.ToDateTime(dr["chg_lst_dt"]);
                    agencyPayableSelect.ChangeLastUserId = dr["chg_lst_user_id"].ToString();
                    agencyPayableSelect.ChangeLastAppName = dr["chg_lst_app_name"].ToString();
                    agencyPayableSelect.AgencyPayablePaymentAmount = Convert.ToDouble(dr["agency_payable_pmt_amt"]);
                }
            Assert.IsFalse(!CompareAgencyPayableDTO(agencyPayableInsert, agencyPayableSelect));
        }
        protected bool CompareAgencyPayableDTO(AgencyPayableDTO agencyPayableInsert, AgencyPayableDTO agencyPayableSelect)
        {
            if (agencyPayableSelect.AgencyId != agencyPayableInsert.AgencyId) return false;
            if (agencyPayableSelect.PaymentDate != agencyPayableInsert.PaymentDate) return false;
            if (agencyPayableSelect.StatusCode != agencyPayableInsert.StatusCode) return false;
            if (agencyPayableSelect.PeriodStartDate != agencyPayableInsert.PeriodStartDate) return false;
            if (agencyPayableSelect.PeriodEndDate != agencyPayableInsert.PeriodEndDate) return false;
            if (agencyPayableSelect.PaymentComment != agencyPayableInsert.PaymentComment) return false;
            if (agencyPayableSelect.AccountLinkTBD != agencyPayableInsert.AccountLinkTBD) return false;
            if (agencyPayableSelect.CreateDate != agencyPayableInsert.CreateDate) return false;
            if(agencyPayableSelect.CreateUserId != agencyPayableInsert.CreateUserId)return false;
            if (agencyPayableSelect.CreateAppName != agencyPayableInsert.CreateAppName) return false;
            if(agencyPayableSelect.ChangeLastDate != agencyPayableInsert.ChangeLastDate)return false;
            if (agencyPayableSelect.ChangeLastUserId != agencyPayableInsert.ChangeLastUserId) return false;
            if(agencyPayableSelect.ChangeLastAppName != agencyPayableInsert.ChangeLastAppName)return false;
            if (agencyPayableSelect.AgencyPayablePaymentAmount != agencyPayableInsert.AgencyPayablePaymentAmount) return false;
            return true;
        }

        /// <summary>
        ///A test for CancelAgencyPayable
        ///</summary>
        [TestMethod()]
        public void CancelAgencyPayableTest()
        {
            AgencyPayableDAO_Accessor target = new AgencyPayableDAO_Accessor(); // TODO: Initialize to an appropriate value
            AgencyPayableDTO agencyPayable = new AgencyPayableDTO(); // TODO: Initialize to an appropriate value
            //get payable_id from AGENCY_PAYABLE data test status_cd='payable test'
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string strsql = @"select agency_payable_id from agency_payable where status_cd='payable test'";
            var command = new SqlCommand(strsql, dbConnection);
            dbConnection.Open();
            int agency_payable_id = Convert.ToInt32(command.ExecuteScalar());

            //agencyPayable.PaymentComment = "Agency didn't complete";
            agencyPayable.StatusCode = "CANCELLED";
            agencyPayable.AgencyPayableId = agency_payable_id;
            agencyPayable.ChangeLastDate = DateTime.Now;
            agencyPayable.ChangeLastUserId = working_user_id;
            agencyPayable.ChangeLastAppName = "HPF";
            target.CancelAgencyPayable(agencyPayable);
            strsql = @"select * from agency_payable where agency_payable_id=" + agency_payable_id;
            command = new SqlCommand(strsql, dbConnection);
            var rd = command.ExecuteReader();
            //actual data
            string PaymentComment = null;
            string StatusCd = null;
            //List<string> actuallist = new List<string>();
            if (rd.HasRows)
                while (rd.Read())
                {
                    PaymentComment=rd["pmt_comment"].ToString();
                    StatusCd=rd["status_cd"].ToString();
                }
            //Assert.AreEqual(PaymentComment,"Agency didn't complete");
            Assert.AreEqual(StatusCd,"CANCELLED");
        }
        /// <summary>
        ///A test for TakeBackMarkedCase
        ///</summary>
        [TestMethod()]
        public void TakeBackMarkedCaseTest()
        {
            AgencyPayableDAO_Accessor target = new AgencyPayableDAO_Accessor(); // TODO: Initialize to an appropriate value
            AgencyPayableCaseDTO agencyPayableCase = new AgencyPayableCaseDTO(); // TODO: Initialize to an appropriate value

            //get payable_id from AGENCY_PAYABLE data test status_cd='payable test'
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string strsql = @"select agency_payable_id from agency_payable where status_cd='" + status_cd + "'";
            dbConnection.Open();
            var command = new SqlCommand(strsql, dbConnection);
            int agency_payable_id = Convert.ToInt32(command.ExecuteScalar());
            //
            strsql = @"select agency_payable_case_id from agency_payable_case where agency_payable_id=" + agency_payable_id;
            command = new SqlCommand(strsql, dbConnection);
            int agency_payable_case_id = Convert.ToInt32(command.ExecuteScalar());

            //update criteria.
            AgencyPayableSetDTO agencyPayableSet = new AgencyPayableSetDTO();
            agencyPayableSet.ChangeLastAppName = "test";
            agencyPayableSet.ChangeLastDate = Convert.ToDateTime("2/2/2222");
            agencyPayableSet.ChangeLastUserId = working_user_id;
            string takebackReason = "";
            string agencyPayableIDCol = agency_payable_case_id.ToString();
            //
            target.TakebackMarkCase(agencyPayableSet, takebackReason, agencyPayableIDCol);
            //
            strsql = @"select * from agency_payable_case where agency_payable_id=" + agency_payable_id;
            command = new SqlCommand(strsql, dbConnection);
            var rd = command.ExecuteReader();
            string takebackReason_actual = "";
            if (rd.HasRows)
                while (rd.Read())
                {
                    takebackReason_actual = rd["takeback_pmt_reason_cd"].ToString();
                }
            Assert.AreEqual(takebackReason, takebackReason_actual);
            dbConnection.Close();
        }

        /// <summary>
        ///A test for PayUnpayMarkedCase
        ///</summary>
        //[TestMethod()]
        //public void PayUnpayMarkedCaseTest()
        //{
        //    AgencyPayableDAO_Accessor target = new AgencyPayableDAO_Accessor(); // TODO: Initialize to an appropriate value
        //    AgencyPayableDTO agencyPayable = null; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = target.UpdateAgencyPayable(agencyPayable);
        //    Assert.AreEqual(expected, actual);
        //}

        #region Test Data
        
        #endregion
    }
}
