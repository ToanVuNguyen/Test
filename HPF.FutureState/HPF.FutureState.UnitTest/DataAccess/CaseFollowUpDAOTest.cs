using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using System;
using System.Data.SqlClient;
using System.Configuration;

namespace HPF.FutureState.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for CaseFollowUpDAOTest and is intended
    ///to contain all CaseFollowUpDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CaseFollowUpDAOTest
    {


        private TestContext testContextInstance;
        static int fcId;
        static int outcomeTypeId;
        static string workingUserId = "Case Follow Up";
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
            fcId = GetFcId();
            outcomeTypeId = GetOutcomeTypeId();
            DeleteCaseFollowUp(fcId, outcomeTypeId);
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            DeleteCaseFollowUp(fcId, outcomeTypeId);
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
        ///A test for SaveCaseFollowUp
        ///</summary>
        [TestMethod()]
        public void InsertCaseFollowUpTest()
        {
            CaseFollowUpDAO_Accessor target = new CaseFollowUpDAO_Accessor(); // TODO: Initialize to an appropriate value
            CaseFollowUpDTO caseFollowUp = new CaseFollowUpDTO(); // TODO: Initialize to an appropriate value           
            caseFollowUp.FcId = fcId;
            caseFollowUp.OutcomeTypeId = outcomeTypeId;
            caseFollowUp.FollowUpDt = DateTime.Now;
            caseFollowUp.FollowUpComment = "Comment";
            caseFollowUp.FollowupSourceCd = "FUSC";
            caseFollowUp.LoanDelinqStatusCd = "LDSC";
            caseFollowUp.StillInHouseInd = "Y";
            caseFollowUp.CreditScore = "CRS";
            caseFollowUp.CreditBureauCd = "CRB";
            caseFollowUp.CreditReportDt = DateTime.Now;
            caseFollowUp.CreateUserId = workingUserId;
            caseFollowUp.CreateDate = DateTime.Now;
            caseFollowUp.CreateAppName = "HPF";
            caseFollowUp.ChangeLastUserId = workingUserId;
            caseFollowUp.ChangeLastDate = DateTime.Now;
            caseFollowUp.ChangeLastAppName = "HPF";
            bool isUpdated = false; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.SaveCaseFollowUp(caseFollowUp, isUpdated);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void UpdateCaseFollowUpTest()
        {
            CaseFollowUpDAO_Accessor target = new CaseFollowUpDAO_Accessor(); // TODO: Initialize to an appropriate value
            CaseFollowUpDTO caseFollowUp = new CaseFollowUpDTO(); // TODO: Initialize to an appropriate value           
            caseFollowUp.CasePostCounselingStatusId = GetFollowUpId(fcId, outcomeTypeId);
            caseFollowUp.FcId = fcId;
            caseFollowUp.OutcomeTypeId = outcomeTypeId;
            caseFollowUp.FollowUpDt = DateTime.Now;
            caseFollowUp.FollowUpComment = "Comment update";
            caseFollowUp.FollowupSourceCd = "FUSC";
            caseFollowUp.LoanDelinqStatusCd = "LDSC";
            caseFollowUp.StillInHouseInd = "Y";
            caseFollowUp.CreditScore = "CRS";
            caseFollowUp.CreditBureauCd = "CRB";
            caseFollowUp.CreditReportDt = DateTime.Now;
            caseFollowUp.CreateUserId = workingUserId;
            caseFollowUp.CreateDate = DateTime.Now;
            caseFollowUp.CreateAppName = "HPF";
            caseFollowUp.ChangeLastUserId = workingUserId;
            caseFollowUp.ChangeLastDate = DateTime.Now;
            caseFollowUp.ChangeLastAppName = "HPF";
            bool isUpdated = true; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.SaveCaseFollowUp(caseFollowUp, isUpdated);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetFollowUp
        ///</summary>
        [TestMethod()]
        public void GetFollowUpTest()
        {
            CaseFollowUpDAO_Accessor target = new CaseFollowUpDAO_Accessor(); // TODO: Initialize to an appropriate value            
            string expected = GetFollowUpDTO(fcId).FollowUpComment; // TODO: Initialize to an appropriate value
            string actual = null;
            CaseFollowUpDTOCollection temp = target.GetFollowUp(fcId);
            if (temp.Count != 0) 
                actual = target.GetFollowUp(fcId)[0].FollowUpComment;
            Assert.AreEqual(expected, actual);
        }

        #region Utility
        private static void DeleteCaseFollowUp(int fcId, int outcomeTypeId)
        {            
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string sql = "DELETE FROM case_post_counseling_status WHERE fc_Id = " + fcId + " AND outcome_type_id = " + outcomeTypeId + "";
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();            
        }
        private static int GetFcId()
        {
            int result = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string sql = "SELECT MAX(fc_id) as Fc_id FROM foreclosure_case";
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = int.Parse(reader["fc_id"].ToString());
                    break;
                }
            }
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
            return result;
        }

        private static int GetOutcomeTypeId()
        {
            int result = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string sql = "SELECT MAX(outcome_type_Id) as outcome_type_Id  FROM Outcome_type";
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = int.Parse(reader["outcome_type_Id"].ToString());
                    break;
                }
            }
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
            return result;
        }

        private int GetFollowUpId(int fcId, int outcomeTypeId)
        {
            int result = 0;
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string sql = "SELECT case_post_counseling_status_id  FROM case_post_counseling_status WHERE fc_Id = " + fcId + " AND outcome_type_id = " + outcomeTypeId + "";
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = int.Parse(reader["case_post_counseling_status_id"].ToString());
                    break;
                }
            }
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
            return result;
        }

        private CaseFollowUpDTO GetFollowUpDTO(int fcId)
        {
            CaseFollowUpDTO result = new CaseFollowUpDTO();
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string sql = "SELECT case_post_counseling_status_id, followup_comment  FROM case_post_counseling_status WHERE fc_Id = " + fcId + "";
            var command = new SqlCommand(sql, dbConnection);
            dbConnection.Open();
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.CasePostCounselingStatusId = int.Parse(reader["case_post_counseling_status_id"].ToString());
                    result.FollowUpComment = reader["followup_comment"].ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                dbConnection.Close();
            }
            dbConnection.Close();
            return result;
        }

        #endregion
    }
}
