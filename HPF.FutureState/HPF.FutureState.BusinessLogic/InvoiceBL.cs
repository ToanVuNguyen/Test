using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common;
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
        public InvoiceDTO InsertInvoice(InvoiceDraftDTO invoiceDraft)
        {

            try
            {
                InitiateTransaction();
                InvoiceDTO invoice = new InvoiceDTO();
                //-----------
                invoice.PeriodStartDate = invoiceDraft.PeriodStartDate;
                invoice.PeriodEndDate = invoiceDraft.PeriodEndDate;
                invoice.InvoiceBillAmount = (double)invoiceDraft.TotalAmount;
                invoice.InvoicePaymentAmount = 0;
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
                invoice.InvoiceComment = invoiceDraft.InvoiceComment;
                //Insert Invoice
                int invoiceId = -1;
                invoiceId = invoiceDAO.InsertInvoice(invoice);
                //Insert Invoice Case
                ForeclosureCaseDraftDTOCollection fCaseDrafColection = invoiceDraft.ForeclosureCaseDrafts;
                foreach (ForeclosureCaseDraftDTO fCaseDraf in fCaseDrafColection)
                {
                    InvoiceCaseDTO invoiceCase = new InvoiceCaseDTO();
                    invoiceCase.InvoiceId = invoiceId;
                    invoiceCase.InvoiceCaseBillAmount = fCaseDraf.Amount;
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
                return invoice;
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
        
        public InvoiceDTOCollection InvoiceSearch(InvoiceSearchCriteriaDTO searchCriteria)
        {
            ExceptionMessageCollection exCol = new ExceptionMessageCollection();
            DataValidationException dataValidEx = new DataValidationException();
            ValidationResults valResult = HPFValidator.Validate<InvoiceSearchCriteriaDTO>(searchCriteria,Constant.RULESET_FUNDINGSOURCEVALIDATION);
            if (!valResult.IsValid)
                foreach (var valMes in valResult)
                {
                    string errorCode = string.IsNullOrEmpty(valMes.Tag) ? "Error" : valMes.Tag;
                    string errorMes = string.IsNullOrEmpty(valMes.Tag) ? valMes.Message:ErrorMessages.GetExceptionMessage(valMes.Tag);
                    dataValidEx.ExceptionMessages.AddExceptionMessage(errorCode, errorMes);
                }
            if (dataValidEx.ExceptionMessages.Count > 0)
                throw dataValidEx;
            return InvoiceDAO.CreateInstance().SearchInvoice(searchCriteria);
        }
        public ForeclosureCaseDraftDTOCollection InvoiceCaseSearch(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            return InvoiceDAO.CreateInstance().InvoiceCaseSearch(searchCriteria);
        }
        /// <summary>
        /// Create Invoice Draft 
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public InvoiceDraftDTO CreateInvoiceDraft(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            InvoiceDraftDTO invoiceDraft = new InvoiceDraftDTO();
            invoiceDraft.FundingSourceId = searchCriteria.FundingSourceId.ToString();
            invoiceDraft.PeriodEndDate = searchCriteria.PeriodEnd;
            invoiceDraft.PeriodStartDate = searchCriteria.PeriodStart;
            invoiceDraft.ForeclosureCaseDrafts = this.InvoiceCaseSearch(searchCriteria);
            return invoiceDraft;
        }
        bool CheckOneNonServicerFundingSourceOption(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            return (searchCriteria.ServicerRejected || searchCriteria.ServicerRejectedFreddie || searchCriteria.NeighborworkRejected || searchCriteria.SelectAllServicer || searchCriteria.SelectUnfunded);
        }
        public void ValidateInvoiceCaseSearchCriteria(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            ExceptionMessageCollection exCol = new ExceptionMessageCollection();
            DataValidationException dataValidEx = new DataValidationException();
            ValidationResults valResult = HPFValidator.Validate<InvoiceCaseSearchCriteriaDTO>(searchCriteria, Constant.RULESET_INVOICE_CASE_VALIDATION);
            if (!valResult.IsValid)
                foreach (var valMes in valResult)
                {
                    string errorCode = string.IsNullOrEmpty(valMes.Tag) ? "Error" : valMes.Tag;
                    string errorMes = string.IsNullOrEmpty(valMes.Tag) ? valMes.Message : ErrorMessages.GetExceptionMessage(valMes.Tag);
                    dataValidEx.ExceptionMessages.AddExceptionMessage(errorCode, errorMes);
                }
            if (searchCriteria.ServicerConsentQty == 0 && !CheckOneNonServicerFundingSourceOption(searchCriteria))
                dataValidEx.ExceptionMessages.AddExceptionMessage(ErrorMessages.ERR0564, ErrorMessages.GetExceptionMessage(ErrorMessages.ERR0564));
            if (dataValidEx.ExceptionMessages.Count > 0)
                throw dataValidEx;
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
                item.SetAttribute("fc_id", reconciliationDTO.ForeclosureCaseId==-1?"":reconciliationDTO.ForeclosureCaseId.ToString());
                item.SetAttribute("invoice_case_id", reconciliationDTO.InvoiceCaseId==-1?"":reconciliationDTO.InvoiceCaseId.ToString());
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
            try
            {
                InvoiceDAO.CreateInstance().BackEndPreProcessing(xml,reconciliationDTOCollection.FundingSourceId);
            }
            catch (DataValidationException ex)
            {
                throw ex;
            }
            
            
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
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("invoice_cases");
            foreach (var reconciliationDTO in reconciliationDTOCollection)
            {
                XmlElement item = doc.CreateElement("invoice_case");
                item.SetAttribute("fc_id", reconciliationDTO.ForeclosureCaseId.ToString());
                item.SetAttribute("invoice_case_id", reconciliationDTO.InvoiceCaseId.ToString());
                item.SetAttribute("invoice_case_pmt_amt", reconciliationDTO.PaymentAmount.ToString());
                item.SetAttribute("reject_reason_cd", reconciliationDTO.PaymentRejectReasonCode);
                item.SetAttribute("investor_loan_num", reconciliationDTO.FreddieMacLoanNumber);
                item.SetAttribute("investor_num", reconciliationDTO.InvestorNumber);
                item.SetAttribute("investor_name", reconciliationDTO.InvestorName);
                item.SetAttribute("Freddie_servicer_num", reconciliationDTO.FreddieMacServicerNumber.ToString());
                root.AppendChild(item);
            }
            doc.AppendChild(root);
            string xmlString = doc.InnerXml;
            return ExecuteUpdateInvoicePayment(xmlString,invoicePayment);
        }
        /// <summary>
        /// Update Invoice Cases in the excel file 
        /// </summary>
        /// <param name="xmlString">xml string containts all the information in the excel file</param>
        /// <param name="invoicePayment">Invoice Payment info to update or insert</param>
        /// <returns>New Invoice Payment ID</returns>
        public int ExecuteUpdateInvoicePayment(string xmlString,InvoicePaymentDTO invoicePayment)
        {
            try
            {
                InitiateTransaction();
                
                if (invoicePayment.InvoicePaymentID == -1)
                    invoicePayment.InvoicePaymentID= invoiceDAO.InsertInvoicePayment(invoicePayment);
                else
                    //UPdate with file
                    invoiceDAO.UpdateInvoicePayment(invoicePayment,true);
                invoiceDAO.InvoiceCaseUpdateForPayment(xmlString,invoicePayment.InvoicePaymentID.Value,invoicePayment.ChangeLastDate.Value, invoicePayment.ChangeLastUserId, invoicePayment.ChangeLastAppName);
                CompleteTransaction();
                return invoicePayment.InvoicePaymentID.Value;
            }
            catch(Exception ex)
            {
                RollbackTransaction();
                throw (ex);
            }
        }
        /// <summary>
        /// In case user not provide excel file, so we only update or insert new invoice payment.
        /// </summary>
        /// <param name="invoicePayment"></param>
        /// <returns>new invoicePaymentId</returns>
        public int UpdateInvoicePaymentOnly(InvoicePaymentDTO invoicePayment)
        {
            try
            {
                InitiateTransaction();
                if (invoicePayment.InvoicePaymentID == -1)
                    invoiceDAO.InsertInvoicePayment(invoicePayment);
                else
                    //update without file
                    invoiceDAO.UpdateInvoicePayment(invoicePayment,false);
                CompleteTransaction();
                return invoicePayment.InvoicePaymentID.Value;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw (ex);
            }
                
        }

    }
}
