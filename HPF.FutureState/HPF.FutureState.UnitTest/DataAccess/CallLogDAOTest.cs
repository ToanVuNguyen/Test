using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for CallLogDAOTest and is intended
    ///to contain all CallLogDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CallLogDAOTest
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
        /// Create data test
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize]
        public static void SetupTest(TestContext testContext)
        {
           
        }

        /// <summary>
        /// Delete data test
        /// </summary>
        [ClassCleanup()]
        public static void CleanupTest()
        {
           
        }
        /// <summary>
        ///A test sussess for ReadCallLog
        ///</summary>      
        [TestMethod()]
        public void ReadCallLogTestSuccess()
        {
            CallLogDAO_Accessor target = new CallLogDAO_Accessor(); // TODO: Initialize to an appropriate value
            var callLogId = 2;//TODO: Initialize to an appropriate value
            CallLogDTO expected = CallLogDAO.Instance.ReadCallLog(callLogId); // TODO: Initialize to an appropriate value                        
            CallLogDTO actual = target.ReadCallLog(callLogId);            
            CompareCallLogDTO(expected, actual);            
        }

        /// <summary>
        ///A test fail for ReadCallLog
        ///</summary>                
        [TestMethod()]
        public void ReadCallLogTestFail()
        {
            CallLogDAO_Accessor target = new CallLogDAO_Accessor(); // TODO: Initialize to an appropriate value
            var callLogId1 = 2; // TODO: Initialize to an appropriate value
            var callLogId2 = 3;
            CallLogDTO expected = CallLogDAO.Instance.ReadCallLog(callLogId1); // TODO: Initialize to an appropriate value           
            CallLogDTO actual = target.ReadCallLog(callLogId2);
            CompareCallLogDTO(expected, actual);
        }

        private void CompareCallLogDTO(CallLogDTO expected, CallLogDTO actual)
        {
            Assert.AreEqual(expected.CallId, actual.CallId);
            Assert.AreEqual(expected.ExtCallNumber, actual.ExtCallNumber);
            Assert.AreEqual(expected.AgencyId, actual.AgencyId);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
            Assert.AreEqual(expected.DNIS, actual.DNIS);
            Assert.AreEqual(expected.CallCenter, actual.CallCenter);
            Assert.AreEqual(expected.CallCenterCD, actual.CallCenterCD);
            Assert.AreEqual(expected.CallResource, actual.CallResource);
            Assert.AreEqual(expected.ReasonToCall, actual.ReasonToCall);
            Assert.AreEqual(expected.AccountNumber, actual.AccountNumber);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.CounselPastYRInd, actual.CounselPastYRInd);
            Assert.AreEqual(expected.MtgProbInd, actual.MtgProbInd);
            Assert.AreEqual(expected.PastDueInd, actual.PastDueInd);
            Assert.AreEqual(expected.PastDueSoonInd, actual.PastDueSoonInd);
            Assert.AreEqual(expected.PastDueMonths, actual.PastDueMonths);
            Assert.AreEqual(expected.ServicerId, actual.ServicerId);
            Assert.AreEqual(expected.OtherServicerName, actual.OtherServicerName);
            Assert.AreEqual(expected.PropZip, actual.PropZip);
            Assert.AreEqual(expected.PrevCounselorId, actual.PrevCounselorId);
            Assert.AreEqual(expected.PrevAgencyId, actual.PrevAgencyId);
            Assert.AreEqual(expected.SelectedAgencyId, actual.SelectedAgencyId);
            Assert.AreEqual(expected.ScreenRout, actual.ScreenRout);
            Assert.AreEqual(expected.FinalDispo, actual.FinalDispo);
            Assert.AreEqual(expected.TransNumber, actual.TransNumber);
            Assert.AreEqual(expected.OutOfNetworkReferralTBD, actual.OutOfNetworkReferralTBD);
            Assert.AreEqual(expected.CreateDate, actual.CreateDate);
            Assert.AreEqual(expected.CreateUserId, actual.CreateUserId);
            Assert.AreEqual(expected.CreateAppName, actual.CreateAppName);
            Assert.AreEqual(expected.ChangeLastDate, actual.ChangeLastDate);
            Assert.AreEqual(expected.ChangeLastUserId, actual.ChangeLastUserId);
            Assert.AreEqual(expected.ChangeLastAppName, actual.ChangeLastAppName);
        }

        private void CreateCallLogDTO(CallLogDTO expected)
        {
            if (TestContext.DataRow["call_id"].ToString() != string.Empty)
            {
                expected.CallId = (int)((double)TestContext.DataRow["call_id"]);
            }
           

            if (TestContext.DataRow["ext_call_num"].ToString() != string.Empty)
            {
                expected.ExtCallNumber = (string)TestContext.DataRow["ext_call_num"];
            }
            else
            {
                expected.ExtCallNumber = string.Empty;
            }

            if (TestContext.DataRow["agency_id"].ToString() != string.Empty)
            {
                expected.AgencyId = (string)TestContext.DataRow["agency_id"];
            }
            else
            {
                expected.AgencyId = string.Empty;
            }

            if (TestContext.DataRow["start_dt"].ToString() != string.Empty)
            {
                expected.StartDate = (System.DateTime)TestContext.DataRow["start_dt"];
            }

            if (TestContext.DataRow["end_dt"].ToString() != string.Empty)
            {
                expected.EndDate = (System.DateTime)TestContext.DataRow["end_dt"];
            }

            if (TestContext.DataRow["dnis"].ToString() != string.Empty)
            {
                expected.DNIS = (string)TestContext.DataRow["dnis"];
            }
            else
            {
                expected.DNIS = string.Empty;
            }

            if (TestContext.DataRow["call_center"].ToString() != string.Empty)
            {
                expected.CallCenter = (string)TestContext.DataRow["call_center"];
            }
            else
            {
                expected.CallCenter = string.Empty;
            }

            if (TestContext.DataRow["call_center_cd"].ToString() != string.Empty)
            {
                expected.CallCenterCD = (string)TestContext.DataRow["call_center_cd"];
            }
            else
            {
                expected.CallCenterCD = string.Empty;
            }

            if (TestContext.DataRow["call_resource"].ToString() != string.Empty)
            {
                expected.CallResource = (string)TestContext.DataRow["call_resource"];
            }
            else
            {
                expected.CallResource = string.Empty;
            }

            if (TestContext.DataRow["reason_for_call"].ToString() != string.Empty)
            {
                expected.ReasonToCall = (string)TestContext.DataRow["reason_for_call"];
            }
            else
            {
                expected.ReasonToCall = string.Empty;
            }

            if (TestContext.DataRow["acct_num"].ToString() != string.Empty)
            {
                expected.AccountNumber = (string)TestContext.DataRow["acct_num"];
            }
            else
            {
                expected.AccountNumber = string.Empty;
            }

            if (TestContext.DataRow["fname"].ToString() != string.Empty)
            {
                expected.FirstName = (string)TestContext.DataRow["fname"];
            }
            else
            {
                expected.FirstName = string.Empty;
            }

            if (TestContext.DataRow["lname"].ToString() != string.Empty)
            {
                expected.LastName = (string)TestContext.DataRow["lname"];
            }
            else
            {
                expected.LastName = string.Empty;
            }

            if (TestContext.DataRow["counsel_past_YR_ind"].ToString() != string.Empty)
            {
                expected.CounselPastYRInd = (string)TestContext.DataRow["counsel_past_YR_ind"];
            }
            else
            {
                expected.CounselPastYRInd = string.Empty;
            }

            if (TestContext.DataRow["mtg_prob_ind"].ToString() != string.Empty)
            {
                expected.MtgProbInd = (string)TestContext.DataRow["mtg_prob_ind"];
            }
            else
            {
                expected.MtgProbInd = string.Empty;
            }

            if (TestContext.DataRow["past_due_ind"].ToString() != string.Empty)
            {
                expected.PastDueInd = (string)TestContext.DataRow["past_due_ind"];
            }
            else
            {
                expected.PastDueInd = string.Empty;
            }

            if (TestContext.DataRow["past_due_soon_ind"].ToString() != string.Empty)
            {
                expected.PastDueSoonInd = (string)TestContext.DataRow["past_due_soon_ind"];
            }
            else
            {
                expected.PastDueSoonInd = string.Empty;
            }

            if (TestContext.DataRow["past_due_months"].ToString() != string.Empty)
            {
                expected.PastDueMonths = (int)((double)TestContext.DataRow["past_due_months"]);
            }

            if (TestContext.DataRow["servicer_id"].ToString() != string.Empty)
            {
                expected.ServicerId = (int)((double)TestContext.DataRow["servicer_id"]);
            }

            if (TestContext.DataRow["other_servicer_name"].ToString() != string.Empty)
            {
                expected.OtherServicerName = (string)TestContext.DataRow["other_servicer_name"];
            }
            else
            {
                expected.OtherServicerName = string.Empty;
            }

            if (TestContext.DataRow["prop_zip"].ToString() != string.Empty)
            {
                expected.PropZip = (string)TestContext.DataRow["prop_zip"];
            }
            else
            {
                expected.PropZip = string.Empty;
            }

            if (TestContext.DataRow["prev_counselor_id"].ToString() != string.Empty)
            {
                expected.PrevCounselorId = (int)((double)TestContext.DataRow["prev_counselor_id"]);
            }

            if (TestContext.DataRow["prev_agency_id"].ToString() != string.Empty)
            {
                expected.PrevAgencyId = (int)((double)TestContext.DataRow["prev_agency_id"]);
            }

            if (TestContext.DataRow["selected_agency_id"].ToString() != string.Empty)
            {
                expected.SelectedAgencyId = (string)TestContext.DataRow["selected_agency_id"];
            }
            else
            {
                expected.SelectedAgencyId = string.Empty;
            }

            if (TestContext.DataRow["screen_rout"].ToString() != string.Empty)
            {
                expected.ScreenRout = (string)TestContext.DataRow["screen_rout"];
            }
            else
            {
                expected.ScreenRout = string.Empty;
            }

            if (TestContext.DataRow["final_dispo"].ToString() != string.Empty)
            {
                expected.FinalDispo = (int)((double)TestContext.DataRow["final_dispo"]);
            }

            if (TestContext.DataRow["trans_num"].ToString() != string.Empty)
            {
                expected.TransNumber = (string)TestContext.DataRow["trans_num"];
            }
            else
            {
                expected.TransNumber = string.Empty;
            }

            if (TestContext.DataRow["out_of_network_referral_TBD"].ToString() != string.Empty)
            {
                expected.OutOfNetworkReferralTBD = (string)TestContext.DataRow["out_of_network_referral_TBD"];
            }
            else
            {
                expected.OutOfNetworkReferralTBD = string.Empty;
            }

            if (TestContext.DataRow["create_dt"].ToString() != string.Empty)
            {
                expected.CreateDate = (System.DateTime)TestContext.DataRow["create_dt"];
            }

            if (TestContext.DataRow["create_user_id"].ToString() != string.Empty)
            {
                expected.CreateUserId = (string)TestContext.DataRow["create_user_id"];
            }
            else
            {
                expected.CreateUserId = string.Empty;
            }

            if (TestContext.DataRow["create_app_name"].ToString() != string.Empty)
            {
                expected.CreateAppName = (string)TestContext.DataRow["create_app_name"];
            }
            else
            {
                expected.CreateAppName = string.Empty;
            }

            if (TestContext.DataRow["chg_lst_dt"].ToString() != string.Empty)
            {
                expected.ChangeLastDate = (System.DateTime)TestContext.DataRow["chg_lst_dt"];
            }

            if (TestContext.DataRow["chg_lst_user_id"].ToString() != string.Empty)
            {
                expected.ChangeLastUserId = (string)TestContext.DataRow["chg_lst_user_id"];
            }
            else
            {
                expected.ChangeLastUserId = string.Empty;
            }

            if (TestContext.DataRow["chg_lst_app_name"].ToString() != string.Empty)
            {
                expected.ChangeLastAppName = (string)TestContext.DataRow["chg_lst_app_name"];
            }
            else
            {
                expected.ChangeLastAppName = string.Empty;
            }
        }        
    }
}
