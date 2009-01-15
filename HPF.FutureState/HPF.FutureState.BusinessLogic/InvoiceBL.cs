using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;
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

        public void InsertInvoice(InvoiceDraftDTO invoiceDraft)
        {
            InvoiceDAO invoiceDAO = InvoiceDAO.CreateInstance();
            try
            {
                invoiceDAO.Begin();
                InvoiceDTO invoice = new InvoiceDTO();
                //-----------
                invoice.PeriodStartDate = invoiceDraft.PeriodStartDate;
                invoice.PeriodEndDate = invoiceDraft.PeriodEndDate;
                invoice.InvoicePaymentAmount = (double)invoiceDraft.TotalAmount;
                invoice.FundingSourceId = int.Parse(invoiceDraft.FundingSourceId);
                invoice.StatusCode = "ACTIVE";
                invoice.ChangeLastAppName = invoiceDraft.ChangeLastAppName;
                invoice.ChangeLastDate = invoiceDraft.ChangeLastDate;
                invoice.ChangeLastUserId = invoiceDraft.ChangeLastUserId;
                invoice.CreateAppName = invoiceDraft.CreateAppName;
                invoice.CreateDate = invoiceDraft.CreateDate;
                invoice.CreateUserId = invoiceDraft.CreateUserId;
                //Insert Invoice
                int invoiceId = -1;
                invoiceId = invoiceDAO.InserInvoice(invoice);
                //Insert Invoice Case
                ForeclosureCaseDraftDTOCollection fCaseDrafColection = invoiceDraft.ForeclosureCaseDrafts;
                foreach (ForeclosureCaseDraftDTO fCaseDraf in fCaseDrafColection)
                {
                    InvoiceCaseDTO invoiceCase = new InvoiceCaseDTO();
                    invoiceCase.InvoiceId = invoiceId;
                    invoiceCase.InvoiceCasePaymentAmount = (double)fCaseDraf.Amount;
                    invoiceCase.ForeclosureCaseId = fCaseDraf.ForeclosureCaseId;
                    invoiceCase.ChangeLastAppName = invoiceDraft.ChangeLastAppName;
                    invoiceCase.ChangeLastDate = invoiceDraft.ChangeLastDate;
                    invoiceCase.ChangeLastUserId = invoiceDraft.ChangeLastUserId;
                    invoiceCase.CreateAppName = invoiceDraft.CreateAppName;
                    invoiceCase.CreateDate = invoiceDraft.CreateDate;
                    invoiceCase.CreateUserId = invoiceDraft.CreateUserId;
                    invoiceDAO.InsertInvoiceCase(invoiceCase);
                }
                invoiceDAO.Commit();
            }
            catch (Exception ex)
            {
                invoiceDAO.Cancel();
                throw (ex) ;
            }
        }

        public bool ValidateInvoiceCriteria(InvoiceSearchCriteriaDTO criteria,out string returnMessage)
        {
            returnMessage = "";
            if (criteria.PeriodStart == DateTime.MinValue)
                returnMessage += "Period Start:Wrong DateTime format; ";
            if (criteria.PeriodEnd == DateTime.MinValue)
                returnMessage += "Period End:Wrong DateTime format; ";
            if ((criteria.PeriodStart > criteria.PeriodEnd)&&returnMessage=="")
                returnMessage += "Period Start can not larger than Period End; ";
            return (returnMessage == "");
        }
        public bool ValidateInvoiceCaseCriteria(InvoiceCaseSearchCriteriaDTO criteria, out string returnMessage)
        {
            returnMessage = "";
            if (criteria.PeriodStart == DateTime.MinValue)
                returnMessage += "Period Start:Wrong DateTime format; ";
            if (criteria.PeriodEnd == DateTime.MinValue)
                returnMessage += "Period End:Wrong DateTime format; ";
            if ((criteria.PeriodStart > criteria.PeriodEnd) && returnMessage == "")
                returnMessage += "Period Start can not larger than Period End; ";
            return (returnMessage == "");
        }
        public InvoiceSearchResultDTOCollection InvoiceSearch(InvoiceSearchCriteriaDTO searchCriteria)
        {
            InvoiceSearchResultDTOCollection result = null;
            string returnMessage;
            if (ValidateInvoiceCriteria(searchCriteria, out returnMessage) == false)
                throw new DataValidationException(returnMessage);
            try
            {
                result = InvoiceDAO.CreateInstance().SearchInvoice(searchCriteria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public ForeclosureCaseDraftDTOCollection InvoiceCaseSearch(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            ForeclosureCaseDraftDTOCollection result = null;
            string returnMessage;
            if (ValidateInvoiceCaseCriteria(searchCriteria, out returnMessage) == false)
                throw new DataValidationException(returnMessage);
            try
            {
                result = InvoiceDAO.CreateInstance().InvoiceCaseSearch(searchCriteria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public InvoiceDraftDTO CreateInvoiceDraft(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            InvoiceDraftDTO invoiceDraft = new InvoiceDraftDTO();
            invoiceDraft.FundingSourceId = searchCriteria.FundingSourceId;
            invoiceDraft.PeriodEndDate = searchCriteria.PeriodEnd;
            invoiceDraft.PeriodStartDate = searchCriteria.PeriodStart;
            invoiceDraft.ForeclosureCaseDrafts = this.InvoiceCaseSearch(searchCriteria);
            return invoiceDraft;
        }
    }
}
