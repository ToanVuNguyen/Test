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
using System.IO;
using System.Data;
using HPF.FutureState.Common.Utils;
//using HPF.FutureState.Web.Security;

namespace HPF.FutureState.BusinessLogic
{
    public class InvoiceBL: BaseBusinessLogic
    {
        private List<string> COLUMN_NAME = new List<string>();
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

        public void ValidateInvoicePayment(InvoicePaymentDTO invoicePayment)
        {
            ExceptionMessageCollection exCol = new ExceptionMessageCollection();
            DataValidationException dataValidEx = new DataValidationException();
            ValidationResults valResult = HPFValidator.Validate<InvoicePaymentDTO>(invoicePayment, Constant.RULESET_INVOICE_PAYMENT_VALIDATION);
            if (!valResult.IsValid)
                foreach (var valMes in valResult)
                {
                    string errorCode = string.IsNullOrEmpty(valMes.Tag) ? "Error" : valMes.Tag;
                    string errorMes = string.IsNullOrEmpty(valMes.Tag) ? valMes.Message : ErrorMessages.GetExceptionMessage(valMes.Tag);
                    dataValidEx.ExceptionMessages.AddExceptionMessage(errorCode, errorMes);
                }
            double testValue = (double)Math.Round(invoicePayment.PaymentAmount.Value, 2);
            if (testValue != invoicePayment.PaymentAmount && dataValidEx.ExceptionMessages.GetExceptionMessage(ErrorMessages.ERR0654) == null)
                dataValidEx.ExceptionMessages.AddExceptionMessage(ErrorMessages.ERR0654, ErrorMessages.GetExceptionMessage(ErrorMessages.ERR0654));
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
        private void UcInit()
        {
            COLUMN_NAME.Clear();
            COLUMN_NAME.Add("HPF Internal Case ID");
            COLUMN_NAME.Add("HPF Invoice Case ID");
            COLUMN_NAME.Add("Payment Amount");
            COLUMN_NAME.Add("Payment Reject Reason Code");
            COLUMN_NAME.Add("Freddie Mac Servicer Number");
            COLUMN_NAME.Add("Freddie Mac Loan Number");
            COLUMN_NAME.Add("Investor Number");
            COLUMN_NAME.Add("Investor Name");
            COLUMN_NAME.Add("Servicer Name");
            COLUMN_NAME.Add("Loan Number");
        }
        public int ExcelProcessing(Stream fileContents, InvoicePaymentDTO invoicePayment)
        {
            if (fileContents == null)
            {
                DataValidationException ex = new DataValidationException();
                ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0655));
                throw ex;
            }
            UcInit();
            DataSet dataSet = null;
            try
            {
                dataSet = ExcelFileReader.Read(fileContents, Constant.EXCEL_FILE_TAB_NAME);
            }
            catch (ExcelFileReaderException ex)
            {
                if (ex.ErrorCode == -1)
                {
                    DataValidationException dataEx = new DataValidationException();
                    dataEx.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0673));
                    throw dataEx;
                }
                else
                {
                    DataValidationException dataEx = new DataValidationException();
                    dataEx.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0656));
                    throw dataEx;
                }

            }
            catch (Exception ex)
            {
                DataValidationException ex1 = new DataValidationException();
                ex1.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0655));
                throw ex1;
            }
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                DataValidationException ex = new DataValidationException();
                ex.ExceptionMessages.Add(GetExceptionMessage(ErrorMessages.ERR0673));
                throw ex;
            }
            //FrontEndPreProcessing on the Presentation Layer
            ReconciliationDTOCollection reconciliationCollection = FrontEndPreProcessing(dataSet,invoicePayment.PaymentAmount,invoicePayment.FundingSourceID.Value);
            //BackEndPreProcessing on Business Layer
            InvoiceBL.Instance.BackEndPreProcessing(reconciliationCollection);
            //Update Invoice CAses
            int paymentId = InvoiceBL.Instance.UpdateInvoicePayment(reconciliationCollection, invoicePayment);
            return paymentId;
            //if(!isCancel)
            //    Response.Redirect("InvoicePaymentInfo.aspx?id="+paymentId.ToString());  
            
        }
        private ExceptionMessage GetExceptionMessage(string errorCode, int rowIndex)
        {
            var exMes = new ExceptionMessage();
            exMes.ErrorCode = errorCode;
            exMes.Message = ErrorMessages.GetExceptionMessage(errorCode, rowIndex);
            return exMes;
        }

        private ExceptionMessage GetExceptionMessageWithoutCode(string errorCode)
        {
            var exMes = new ExceptionMessage();
            exMes.ErrorCode = errorCode;
            exMes.Message = ErrorMessages.GetExceptionMessage(errorCode);
            return exMes;
        }
        private ExceptionMessage GetExceptionMessage(string errorCode)
        {
            var exMes = new ExceptionMessage();
            exMes.ErrorCode = errorCode;
            exMes.Message = ErrorMessages.GetExceptionMessage(errorCode);
            return exMes;
        }
        #region FrontEnd PreProcessing
        /// <summary>
        /// Vaidate the excel file format
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        ReconciliationDTOCollection FrontEndPreProcessing(DataSet dataSet,double? paymentAmount,int fundingSourceId)
        {
            DataTable fileContent = dataSet.Tables[0];
            foreach (DataColumn col in fileContent.Columns)
                col.ColumnName = col.ColumnName.Trim().ToLower();
            ColumnsValidate(fileContent);
            RowsValidate(fileContent, paymentAmount.Value);
            return ConvertToObjectReconciliationDTO(fileContent,fundingSourceId);
        }
        ReconciliationDTOCollection ConvertToObjectReconciliationDTO(DataTable fileContent, int fundingSourceId)
        {
            ReconciliationDTOCollection result = new ReconciliationDTOCollection();
            foreach (DataRow row in fileContent.Rows)
            {
                ReconciliationDTO item = new ReconciliationDTO();
                if (row[COLUMN_NAME[0].ToLower()].ToString() != string.Empty)
                    item.ForeclosureCaseId = int.Parse(row[COLUMN_NAME[0]].ToString());
                else
                    item.ForeclosureCaseId = -1;
                if (row[COLUMN_NAME[1].ToLower()].ToString() != string.Empty)
                    item.InvoiceCaseId = int.Parse(row[COLUMN_NAME[1].ToLower()].ToString());
                else
                    item.InvoiceCaseId = -1;
                if (row[COLUMN_NAME[2].ToLower()].ToString() != string.Empty)
                    item.PaymentAmount = double.Parse(row[COLUMN_NAME[2].ToLower()].ToString());
                else
                    item.PaymentAmount = int.MinValue;
                item.PaymentRejectReasonCode = row[COLUMN_NAME[3].ToLower()].ToString();
                item.FreddieMacServicerNumber = row[COLUMN_NAME[4].ToLower()].ToString();
                item.FreddieMacLoanNumber = row[COLUMN_NAME[5].ToLower()].ToString();
                item.InvestorNumber = row[COLUMN_NAME[6].ToLower()].ToString();
                item.InvestorName = row[COLUMN_NAME[7].ToLower()].ToString();
                //item.LoanNumber = row[COLUMN_NAME[9]].ToString();
                result.Add(item);
            }
            result.FundingSourceId = fundingSourceId.ToString();
            return result;
        }
        private List<string> GetRejectReasonCode()
        {
            List<string> result = new List<string>();
            var rejectReason = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_PAYMENT_REJECT_REASON_CODE);
            foreach (var i in rejectReason)
                result.Add(i.Code);
            return result;
        }
        private void RowsValidate(DataTable fileContent, double paymentAmount)
        {
            DataValidationException ex = new DataValidationException();
            int rowIndex = 0;
            List<string> paymentRejectReasonCollection = GetRejectReasonCode();
            double sumOfPaymentAmount = 0;
            foreach (DataRow row in fileContent.Rows)
            {
                rowIndex++;
                //Internal Case IDs
                try
                {
                    if (row[COLUMN_NAME[0]].ToString() != string.Empty)
                        int.Parse(row[COLUMN_NAME[0]].ToString());
                }
                catch
                {
                    var exMes = GetExceptionMessage(ErrorMessages.ERR0658, rowIndex);
                    ex.ExceptionMessages.Add(exMes);
                }

                //Invoice Case ID
                try
                {
                    if (row[COLUMN_NAME[1]].ToString() != string.Empty)
                        int.Parse(row[COLUMN_NAME[1]].ToString());
                }
                catch
                {
                    var exMes = GetExceptionMessage(ErrorMessages.ERR0662, rowIndex);
                    ex.ExceptionMessages.Add(exMes);
                }
                //Internal Case ID and Invoice Case ID = null
                if (row[COLUMN_NAME[0]].ToString() == string.Empty && row[COLUMN_NAME[1]].ToString() == string.Empty)
                {
                    var exMes = GetExceptionMessage(ErrorMessages.ERR0678, rowIndex);
                    ex.ExceptionMessages.Add(exMes);
                }
                //Neither a payment amount nor payment reject reason code exist for the record
                if (row[COLUMN_NAME[3]].ToString() == string.Empty && row[COLUMN_NAME[2]].ToString() == string.Empty)
                {
                    var exMes = GetExceptionMessage(ErrorMessages.ERR0679, rowIndex);
                    ex.ExceptionMessages.Add(exMes);
                    continue;
                }

                //Reject Reaon Code
                if (row[COLUMN_NAME[3]].ToString() != string.Empty)
                    if (paymentRejectReasonCollection.IndexOf(row[COLUMN_NAME[3]].ToString()) == -1)
                    {
                        var exMes = GetExceptionMessage(ErrorMessages.ERR0660, rowIndex);
                        ex.ExceptionMessages.Add(exMes);
                        continue;
                    }


                //Payment Amounts
                try
                {
                    if (row[COLUMN_NAME[3]].ToString() == string.Empty)
                    {
                        string temp = row[COLUMN_NAME[2]].ToString();
                        double pmtAmt = 0;
                        if (temp != string.Empty)
                            pmtAmt = double.Parse(temp);
                        if (pmtAmt <= 0)
                            throw (new Exception());
                        sumOfPaymentAmount += pmtAmt;
                        //if the row doesnt have a payment reject reason code, add it to the payment amount total for the file.
                    }
                }
                catch
                {
                    var exMes = GetExceptionMessage(ErrorMessages.ERR0659, rowIndex);
                    ex.ExceptionMessages.Add(exMes);
                }
            }
            //sumOfpaymentAmount must equal total paymentAmount
            if (sumOfPaymentAmount != paymentAmount)
            {
                var exMes = new ExceptionMessage();
                exMes.ErrorCode = ErrorMessages.ERR0661;
                exMes.Message = ErrorMessages.GetExceptionMessage(ErrorMessages.ERR0661, rowIndex, sumOfPaymentAmount.ToString("C"), paymentAmount.ToString("C"));
                ex.ExceptionMessages.Add(exMes);
            }
            if (ex.ExceptionMessages.Count > 0)
                throw ex;
        }
        private void ColumnsValidate(DataTable fileContent)
        {
            DataValidationException ex = new DataValidationException();
            List<string> columnName = new List<string>();
            foreach (string name in COLUMN_NAME)
                columnName.Add(name.ToLower());
            int columnIndex = 0;
            foreach (DataColumn col in fileContent.Columns)
            {
                int index = columnName.IndexOf(col.ColumnName.ToLower().Trim());
                if (index == -1)
                {
                    ExceptionMessage exMes = GetColumnExceptionMessage(columnIndex);
                    if (exMes != null)
                        ex.ExceptionMessages.Add(exMes);
                }
                else
                    columnName.RemoveAt(index);
                columnIndex++;
            }
            if (ex.ExceptionMessages.Count > 0)
                throw ex;
        }
        private ExceptionMessage GetColumnExceptionMessage(int columnIndex)
        {
            if (columnIndex == 0)
                return GetExceptionMessage(ErrorMessages.ERR0662);
            if (columnIndex == 1)
                return GetExceptionMessage(ErrorMessages.ERR0663);
            if (columnIndex == 2)
                return GetExceptionMessage(ErrorMessages.ERR0664);
            if (columnIndex == 3)
                return GetExceptionMessage(ErrorMessages.ERR0665);
            if (columnIndex == 4)
                return GetExceptionMessage(ErrorMessages.ERR0666);
            if (columnIndex == 5)
                return GetExceptionMessage(ErrorMessages.ERR0667);
            if (columnIndex == 6)
                return GetExceptionMessage(ErrorMessages.ERR0668);
            if (columnIndex == 7)
                return GetExceptionMessage(ErrorMessages.ERR0669);
            return null;
        }
        #endregion

    }
}
