using HPF.FutureState.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System;

namespace HPF.FutureState.UnitTest
{


    /// <summary>
    ///This is a test class for InvoicePaymentDAOTest and is intended
    ///to contain all InvoicePaymentDAOTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InvoicePaymentDAOTest
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
            //insert FUNDING_SOURCE (funding_source_name='fs thao test', funding_source_comment='fs test')
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string strsql = @"insert into [dbo].funding_source([funding_source_name],[funding_source_comment]
           ,[city],[state_cd],[create_dt],[create_user_id],[create_app_name],[chg_lst_dt],[chg_lst_user_id]
           ,[chg_lst_app_name],[funding_source_abbrev]) values ('fs thao test','fs test','hcm test'
           ,'test','1/1/2222','HPF','fs test app','1/1/2222','Test data','CCRC','5')";
            dbConnection.Open();
            var command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();
            
            // get Funding Source Id of test data .
            strsql = @"select funding_source_id from funding_source where funding_source_name='fs thao test'";
            command = new SqlCommand(strsql, dbConnection);
            int fsID = Convert.ToInt16(command.ExecuteScalar());
            
            //insert INVOICE_PAYMENT(pmt_num='invoice thao test', pmt_dt='1/1/2222')
            strsql = @"INSERT INTO [dbo].[invoice_payment]([funding_source_id],[pmt_num] ,[pmt_dt],[pmt_cd]
            ,[create_dt] ,[create_user_id],[create_app_name],[chg_lst_dt],[chg_lst_user_id],[chg_lst_app_name])
            VALUES (" + fsID + ",'invoice thao test','1/1/2222','test test','1/1/2222','HPF','fs test app','1/1/2222','Test data','CCRC')";
            command = new SqlCommand(strsql, dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            //delete insert INVOICE_PAYMENT(pmt_num='invoice thao test')
             var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
             string strsql = @"delete invoice_payment where pmt_num='invoice thao test'";
             var command = new SqlCommand(strsql, dbConnection);
             dbConnection.Open();
             command.ExecuteNonQuery();
            
             //delete FUNDING_SOURCE (funding_source_name='fs thao test', funding_source_comment='fs test') 
             strsql = @"delete funding_source where funding_source_name='fs thao test'";
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
        ///A test for InvoicePaymentSearch
        ///</summary>
        [TestMethod()]
        public void InvoicePaymentSearchTest()
        {
            InvoicePaymentDAO_Accessor target = new InvoicePaymentDAO_Accessor(); // TODO: Initialize to an appropriate value
            InvoiceSearchCriteriaDTO searchCriteria = new InvoiceSearchCriteriaDTO(); // TODO: Initialize to an appropriate value
            
            // get Funding Source Id of test data.
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HPFConnectionString"].ConnectionString);
            string strsql = @"select funding_source_id from funding_source where funding_source_name='fs thao test'";
            var command = new SqlCommand(strsql, dbConnection);
            dbConnection.Open();
            int fsID = Convert.ToInt16(command.ExecuteScalar());
            
            //search criteria
            searchCriteria.FundingSourceId = fsID;
            searchCriteria.PeriodStart = Convert.ToDateTime("2222/1/1");
            searchCriteria.PeriodEnd = Convert.ToDateTime("2222/1/1");
            //search criteria  

            List<string> expectlist = new List<string> { fsID.ToString(), "fs thao test","invoice thao test","1/1/2222" };

            InvoicePaymentDTOCollection actual = target.InvoicePaymentSearch(searchCriteria);
            List<string> actuallist = new List<string> {actual[0].FundingSourceID.ToString(),actual[0].FundingSourceName,
            actual[0].PaymentNum,actual[0].PaymentDate==null?"":actual[0].PaymentDate.Value.ToShortDateString()};
            CollectionAssert.AreEquivalent(expectlist, actuallist);
            dbConnection.Close();
        }
                                                                
                                                                
        /// <summary>                                           
        ///A test for PaymentTypeGet                            
        ///</summary>                                           





        
    }
}
