using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;
using System.Xml;
//using HPF.FutureState.Web.Security;

namespace HPF.FutureState.BusinessLogic
{
    public class InvoiceBL: BaseBusinessLogic
    {
        private static readonly InvoiceBL instance = new InvoiceBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static InvoiceBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected InvoiceBL()
        {            
        }
        private InvoiceDAO invoiceDAO;
        private void RollbackTransaction()
        {
            invoiceDAO.CancelTransaction();
        }

        private void CompleteTransaction()
        {
            invoiceDAO.CommitTransaction();
        }

        private void InitiateTransaction()
        {
            invoiceDAO = InvoiceDAO.CreateInstance();
            invoiceDAO.BeginTransaction();
        }
        /// <summary>
        /// InsertInvoice and Invoice-Case to the database
        /// </summary>
        /// <param name="invoiceDraft">InvoiceDraft contains Invoice and Invoice Case info</param>
        public void InsertInvoice(InvoiceDraftDTO invoiceDraft)
        {

            try
            {
                InitiateTransaction();
                InvoiceDTO invoice = new InvoiceDTO();
                //-----------
                invoice.PeriodStartDate = invoiceDraft.PeriodStartDate;
                invoice.PeriodEndDate = invoiceDraft.PeriodEndDate;
                invoice.InvoiceBillAmount = (double)invoiceDraft.TotalAmount;
                invoice.FundingSourceId = int.Parse(invoiceDraft.FundingSourceId);
                //Get Set Invoice status to Active
                invoice.StatusCode = LookupDataBL.Instance.GetRefCode("invoice status code")[0].Code;
                invoice.ChangeLastAppName = invoiceDraft.ChangeLastAppName;
                invoice.ChangeLastDate = invoiceDraft.ChangeLastDate;
                invoice.ChangeLastUserId = invoiceDraft.ChangeLastUserId;
                invoice.CreateAppName = invoiceDraft.CreateAppName;
                invoice.CreateDate = invoiceDraft.CreateDate;
                invoice.InvoiceDate = invoice.CreateDate;
                invoice.CreateUserId = invoiceDraft.CreateUserId;
                //Insert Invoice
                int invoiceId = -1;
                invoiceId = invoiceDAO.InsertInvoice(invoice);
                //Insert Invoice Case
                ForeclosureCaseDraftDTOCollection fCaseDrafColection = invoiceDraft.ForeclosureCaseDrafts;
                foreach (ForeclosureCaseDraftDTO fCaseDraf in fCaseDrafColection)
                {
                    InvoiceCaseDTO invoiceCase = new InvoiceCaseDTO();
                    invoiceCase.InvoiceId = invoiceId;
                    invoiceCase.InvoiceCaseBillAmount = (double)fCaseDraf.Amount;
                    invoiceCase.ForeclosureCaseId = fCaseDraf.ForeclosureCaseId;
                    invoiceCase.ChangeLastAppName = invoiceDraft.ChangeLastAppName;
                    invoiceCase.ChangeLastDate = invoiceDraft.ChangeLastDate;
                    invoiceCase.ChangeLastUserId = invoiceDraft.ChangeLastUserId;
                    invoiceCase.CreateAppName = invoiceDraft.CreateAppName;
                    invoiceCase.CreateDate = invoiceDraft.CreateDate;
                    invoiceCase.CreateUserId = invoiceDraft.CreateUserId;
                    invoiceDAO.InsertInvoiceCase(invoiceCase);
                }
                CompleteTransaction();
                
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw (ex) ;
            }
        }
        /// <summary>
        /// Update invoice Case
        /// </summary>
        /// <param name="invoiceSet">Invoice set containts Invoice and Invoice Cases info</param>
        /// <param name="invoiceCaseIdCollection">a string that collect all invoice Case Ids to REject</param>
        /// <param name="updateFlag">Let the store procedure know which method should be used</param>
        public bool UpdateInvoiceCase(InvoiceSetDTO invoiceSet, string invoiceCaseIdCollection, InvoiceCaseUpdateFlag updateFlag)
        {
            try
            {
                return InvoiceDAO.CreateInstance().UpdateInvoiceCase(invoiceSet,invoiceCaseIdCollection,updateFlag);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public void UpdateInvoice(InvoiceDTO invoice)
        {
            InitiateTransaction();
            try
            {
                invoiceDAO.UpdateInvoice(invoice);
                CompleteTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw (ex);
            }

        }
        /// <summary>
        /// Validate criteria for Invoice Search 
        /// </summary>
        /// <param name="criteria">searchCriteria</param>
        /// <param name="returnMessage">Error Message</param>
        /// <returns>True if ok and false if fail</returns>
        public bool ValidateInvoiceCriteria(InvoiceSearchCriteriaDTO criteria)
        {
            string returnMessage = "";
            if (criteria.PeriodStart == DateTime.MinValue)
                returnMessage += "Period Start:Wrong DateTime format; ";
            if (criteria.PeriodEnd == DateTime.MinValue)
                returnMessage += "Period End:Wrong DateTime format; ";
            if ((criteria.PeriodStart > criteria.PeriodEnd)&&returnMessage=="")
                returnMessage += "Period Start can not larger than Period End; ";
            if (returnMessage != "")
                throw new DataValidationException(returnMessage);
            return true;
        }
        /// <summary>
        /// Validate criteria for Invoice Search Case
        /// </summary>
        /// <param name="criteria">searchCriteria</param>
        /// <param name="returnMessage">Error Message</param>
        /// <returns>True if ok and false if fail</returns>
        public bool ValidateInvoiceCaseCriteria(InvoiceCaseSearchCriteriaDTO criteria)
        {
            string returnMessage = "";
            if(criteria.FundingSourceId=="-1")
                returnMessage += "FundingSource is required; ";
            if (criteria.PeriodStart == DateTime.MinValue)
                returnMessage += "Period Start:Wrong DateTime format; ";
            if (criteria.PeriodEnd == DateTime.MinValue)
                returnMessage += "Period End:Wrong DateTime format; ";
            if ((criteria.PeriodStart > criteria.PeriodEnd) && returnMessage == "")
                returnMessage += "Period Start can not larger than Period End; ";
            if(criteria.Age.Min>criteria.Age.Max&& criteria.Age.Max!=int.MinValue)
                returnMessage += "AgeMin can not larger than AgeMax; ";
            if(criteria.HouseholdGrossAnnualIncome.Min>criteria.HouseholdGrossAnnualIncome.Max&&criteria.HouseholdGrossAnnualIncome.Max!=double.MinValue)
                returnMessage += "GrossAnnualIncome Min can not larger than GrossAnnualIncome Max; ";
            if(returnMessage!="")
                throw new DataValidationException(returnMessage);
            return true;
        }
        public InvoiceDTOCollection InvoiceSearch(InvoiceSearchCriteriaDTO searchCriteria)
        {
            InvoiceDTOCollection result = null;
            //if (ValidateInvoiceCriteria(searchCriteria))
                result = InvoiceDAO.CreateInstance().SearchInvoice(searchCriteria);
            return result;
        }
        public ForeclosureCaseDraftDTOCollection InvoiceCaseSearch(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            ForeclosureCaseDraftDTOCollection result = null;
            if (ValidateInvoiceCaseCriteria(searchCriteria))
                result = InvoiceDAO.CreateInstance().InvoiceCaseSearch(searchCriteria);
            return result;
        }
        /// <summary>
        /// Create Invoice Draft 
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public InvoiceDraftDTO CreateInvoiceDraft(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            InvoiceDraftDTO invoiceDraft = new InvoiceDraftDTO();
            invoiceDraft.FundingSourceId = searchCriteria.FundingSourceId;
            invoiceDraft.PeriodEndDate = searchCriteria.PeriodEnd;
            invoiceDraft.PeriodStartDate = searchCriteria.PeriodStart;
            invoiceDraft.ForeclosureCaseDrafts = this.InvoiceCaseSearch(searchCriteria);
            return invoiceDraft;
        }
        /// <summary>
        /// Get invoice Set 
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public InvoiceSetDTO GetInvoiceSet(int invoiceId)
        {
            return InvoiceDAO.CreateInstance().InvoiceSetGet(invoiceId);
        }
        /// <summary>
        /// Create a xml file to validate all the invoice Cases
        /// </summary>
        /// <param name="reconciliationDTOCollection"></param>
        /// <returns></returns>
        string GetXmlString(ReconciliationDTOCollection reconciliationDTOCollection)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("invoice_cases");
            int rowIndex=0;
            foreach (var reconciliationDTO in reconciliationDTOCollection)
            {
                XmlElement item = doc.CreateElement("invoice_case");
                item.SetAttribute("row_index", rowIndex.ToString());
                item.SetAttribute("invoice_case_id", reconciliationDTO.InvoiceCaseId.ToString());
                item.SetAttribute("invoice_case_pmt_amt", reconciliationDTO.PaymentAmount.ToString());
                item.SetAttribute("reject_reason_code", reconciliationDTO.PaymentRejectReasonCode);
                root.AppendChild(item);
                rowIndex++;
            }
            doc.AppendChild(root);
            return doc.InnerXml;
        }
        public void BackEndPreProcessing(ReconciliationDTOCollection reconciliationDTOCollection)
        {
            string xml = GetXmlString(reconciliationDTOCollection);
            DataValidationException ex= InvoiceDAO.CreateInstance().BackEndPreProcessing(xml);
            if (ex != null)
                throw (ex);
        }
        /// <summary>
        /// push an xml file to the database to update invoice Case
        /// </summary>
        /// <param name="reconciliationDTOCollection">Data from excel file</param>
        /// <param name="changeLastDt">for update </param>
        /// <param name="changeLastApp">for update</param>
        /// <param name="changeLastUserId">for update</param>
        public int UpdateInvoicePayment(ReconciliationDTOCollection reconciliationDTOCollection, InvoicePaymentDTO invoicePayment)
        {
            //string payList = "";
            //RefCodeItemDTOCollection paymentRejectCodeCollection = LookupDataBL.Instance.GetRefCode("payment reject reason code");
            //var rejectString =new { RejectList="", RejectReason=""};
            //foreach (var item in reconciliationDTOCollection)
            //    if (item.PaymentRejectReasonCode == string.Empty)
            //        payList = payList+ item.InvoiceCaseId + ",";
            //List<string> rejectIds = new List<string>();
            //List<string> rejectCodes = new List<string>();
            //foreach (var item in paymentRejectCodeCollection)
            //{
            //    var rejectArray = from s in reconciliationDTOCollection
            //                      where (s.PaymentRejectReasonCode == item.Code)
            //                      select ( s.InvoiceCaseId);
            //    if(rejectArray.Count<int>() >0)
            //    {
            //        string temp = "";
            //        foreach (var i in rejectArray)
            //            temp = temp + i + ",";
            //        temp = temp.Remove(temp.LastIndexOf(','),1);
            //        rejectIds.Add(temp);
            //        rejectCodes.Add(item.Code);
            //    }
            //}
            ////remove last ','
            //payList = payList.Remove(payList.LastIndexOf(','), 1);
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("invoice_cases");
            foreach (var reconciliationDTO in reconciliationDTOCollection)
            {
                XmlElement item = doc.CreateElement("invoice_case");
                item.SetAttribute("fc_id", reconciliationDTO.ForeclosureCaseId.ToString());
                item.SetAttribute("invoice_case_id", reconciliationDTO.InvoiceCaseId.ToString());
                item.SetAttribute("acct_num", reconciliationDTO.LoanNumber);
                item.SetAttribute("invoice_case_pmt_amt", reconciliationDTO.PaymentAmount.ToString());
                item.SetAttribute("reject_reason_cd", reconciliationDTO.PaymentRejectReasonCode);
                item.SetAttribute("investor_loan_num", reconciliationDTO.FreddieMacLoanNumber);
                item.SetAttribute("investor_num", reconciliationDTO.InvestorNumber);
                item.SetAttribute("investor_name", reconciliationDTO.InvestorName);
                root.AppendChild(item);
            }
            doc.AppendChild(root);
            string xmlString = doc.InnerXml;
            return ExecuteUpdateInvoicePayment(xmlString,invoicePayment);
        }
        public int ExecuteUpdateInvoicePayment(string xmlString,InvoicePaymentDTO invoicePayment)
        {
            try
            {
                InitiateTransaction();
                invoiceDAO.InvoiceCaseUpdateForPayment(xmlString, invoicePayment.ChangeLastDate.Value,invoicePayment.ChangeLastUserId,invoicePayment.ChangeLastAppName);
                if (invoicePayment.InvoicePaymentID == -1)
                    invoiceDAO.InsertInvoicePayment(invoicePayment);
                else
                    invoiceDAO.UpdateInvoicePayment(invoicePayment);
                CompleteTransaction();
                return invoicePayment.InvoicePaymentID.Value;
            }
            catch(Exception ex)
            {
                RollbackTransaction();
                throw (ex);
            }
        }

    }
}
