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
    ///This is a test class for AgencyPayableDAOTest and is intended
    ///to contain all AgencyPayableDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AgencyPayableDAOTest
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            //create test data:SearchAgencyPayableTest

            //insert AGENCY_PAYABLE(agencyid:2---status_cd:'payable test')
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string strsql = @"INSERT INTO agency_payable([agency_id],[pmt_dt],[status_cd],[period_start_dt]
           ,[period_end_dt],[pmt_comment],[accounting_link_TBD],[create_dt],[create_user_id]
           ,[create_app_name],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name],[agency_payable_pmt_amt])
           values(2,'2000/1/1','payable test','2222/1/1','2222/1/1','test comment','accounting_link_TBD_test'
           ,'2000/1/1',1,'tester','2000/1/1','tester','tester',0.0)";
            dbConnection.Open();
            var command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //insert AGENCY(agency_name:payable test---create_user_id:1)
            strsql = @"INSERT INTO [Agency]([agency_name],[contact_fname],[contact_lname],[phone],[fax]
           ,[email],[active_ind],[hud_agency_num],[hud_agency_sub_grantee_num],[create_dt],[create_user_id]
           ,[create_app_name],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name])VALUES
           ('payable test','payable','test',99990000,00009999,'payable@yahoo.com','N','9999','9999','2222/1/1'
            ,1,'payable test','2222/1/1','aaaa','aaaa')";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            dbConnection.Close();

        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            //Delete data test AGENCY_PAYABLE(status_cd='payable test')
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string strsql = @"delete from agency_payable where [status_cd]='payable test'";
            var command = new SqlCommand(strsql, dbConnection);
            dbConnection.Open();
            command.ExecuteNonQuery();

            //Delete data test AGENCY(agency_name='payable_test')
            strsql = @"delete from agency where [agency_name]='payable test'";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //Delete test data insert to InsertAgencyPayableCaseTest
            //
            //get payable_id from AGENCY_PAYABLE data test status_cd='payable test'
            strsql = @"select agency_payable_id from agency_payable where status_cd='payable test'";
            command = new SqlCommand(strsql, dbConnection);
            dbConnection.Open();
            int agency_payable_id = Convert.ToInt16(command.ExecuteScalar());
            //
            strsql = @"delete from agency_payable_case where agency_payable_id="+agency_payable_id;
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();

            //Delete test data insert to InsertAgencyPayableTest
            strsql = @"delete from agency_payable where [status_cd] = 'insert agency'";
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
        ///A test for UpdateAgencyPayableCase
        ///</summary>
        //[TestMethod()]
        //public void UpdateAgencyPayableCaseTest()
        //{
        //    AgencyPayableDAO_Accessor target = new AgencyPayableDAO_Accessor(); // TODO: Initialize to an appropriate value
        //    AgencyPayableCaseDTO agencyPayableCase = null; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = target.UpdateAgencyPayableCase(agencyPayableCase);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for UpdateAgencyPayable
        ///</summary>
        //[TestMethod()]
        //public void UpdateAgencyPayableTest()
        //{
        //    AgencyPayableDAO_Accessor target = new AgencyPayableDAO_Accessor(); // TODO: Initialize to an appropriate value
        //    AgencyPayableDTO agencyPayable = null; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = target.UpdateAgencyPayable(agencyPayable);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for SearchAgencyPayable
        ///</summary>
        [TestMethod()]
        public void SearchAgencyPayableTest()
        {
            AgencyPayableDAO_Accessor target = new AgencyPayableDAO_Accessor(); // TODO: Initialize to an appropriate value
            AgencyPayableSearchCriteriaDTO agencyPayableCriteria = new AgencyPayableSearchCriteriaDTO(); // TODO: Initialize to an appropriate value

            // search criteria
            agencyPayableCriteria.AgencyId = 2;
            agencyPayableCriteria.PeriodStartDate = Convert.ToDateTime("2222/1/1");
            agencyPayableCriteria.PeriodEndDate = Convert.ToDateTime("2222/1/1");
            //search criteria  

            List<string> expectlist = new List<string> { Convert.ToDateTime("2222/1/1").ToShortDateString(), Convert.ToDateTime("2222/1/1").ToShortDateString(), "test comment", "payable test" };

            AgencyPayableDTOCollection actual = target.SearchAgencyPayable(agencyPayableCriteria);
            List<string> actuallist = new List<string> {actual[0].PeriodStartDate.ToShortDateString(),
            actual[0].PeriodEndDate.ToShortDateString(),actual[0].PaymentComment,actual[0].StatusCode };

            CollectionAssert.AreEquivalent(expectlist, actuallist);
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
            int agency_payable_id = Convert.ToInt16(command.ExecuteScalar());
            dbConnection.Close();

            //agencyPayableCase.
            agencyPayableCaseInsert.ForeclosureCaseId = 123;
            agencyPayableCaseInsert.AgencyPayableId = agency_payable_id;
            agencyPayableCaseInsert.PaymentDate = Convert.ToDateTime("2000/1/1");
            agencyPayableCaseInsert.PaymentAmount = 99999;
            agencyPayableCaseInsert.CreateDate = Convert.ToDateTime("2000/1/1");
            agencyPayableCaseInsert.CreateUserId = "HPF";
            agencyPayableCaseInsert.CreateAppName = "HPF";
            agencyPayableCaseInsert.ChangeLastDate = Convert.ToDateTime("2000/1/1");
            agencyPayableCaseInsert.ChangeLastUserId = "HPF";
            agencyPayableCaseInsert.ChangeLastAppName = "HPF";
            agencyPayableCaseInsert.NFMCDiffererencePaidInd = "N";
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
                    agencyPayableCaseSelect.AgencyPayableId = Convert.ToInt16(dr["agency_payable_id"]);
                    agencyPayableCaseSelect.PaymentDate = Convert.ToDateTime(dr["pmt_dt"]);
                    agencyPayableCaseSelect.PaymentAmount = Convert.ToDecimal(dr["pmt_amt"]);
                    agencyPayableCaseSelect.CreateDate = Convert.ToDateTime(dr["create_dt"]);
                    agencyPayableCaseSelect.CreateUserId = dr["create_user_id"].ToString();
                    agencyPayableCaseSelect.CreateAppName = dr["create_app_name"].ToString();
                    agencyPayableCaseSelect.ChangeLastDate = Convert.ToDateTime(dr["chg_lst_dt"]);
                    agencyPayableCaseSelect.ChangeLastUserId = Convert.ToString(dr["chg_lst_user_id"]);
                    agencyPayableCaseSelect.ChangeLastAppName = Convert.ToString(dr["chg_lst_app_name"]);
                    agencyPayableCaseSelect.NFMCDiffererencePaidInd = Convert.ToString(dr["NFMC_difference_paid_ind"]);
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
            if (agencyPayableCaseSelect.NFMCDiffererencePaidInd != agencyPayableCaseInsert.NFMCDiffererencePaidInd) return false;
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
            agencyPayableInsert.CreateUserId = "HPF";
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
                    agencyPayableSelect.AgencyId = Convert.ToInt16(dr["agency_id"]);
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
                    agencyPayableSelect.AgencyPayablePaymentAmount = Convert.ToDecimal(dr["agency_payable_pmt_amt"]);
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
            int agency_payable_id = Convert.ToInt16(command.ExecuteScalar());

            agencyPayable.PaymentComment = "Agency didn't complete";
            agencyPayable.StatusCode = "Cancelled";
            agencyPayable.AgencyPayableId = agency_payable_id;
            target.CancelAgencyPayable(agencyPayable);

            strsql = @"select * from agency_payable where agency_payable_id=" + agency_payable_id;
            command = new SqlCommand(strsql, dbConnection);
            var rd = command.ExecuteReader();
            List<string> actuallist = new List<string>();
            if (rd.HasRows)
                while (rd.Read())
                {
                    actuallist.Add(rd["pmt_comment"].ToString());
                    actuallist.Add(rd["status_cd"].ToString());
                }

            List<string> expectlist = new List<string> { "Agency didn't complete", "Cancelled" };
            CollectionAssert.AreEquivalent(actuallist, expectlist);

        }
        #region Test Data
        
        #endregion
    }
}
