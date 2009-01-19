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
        /// <summary>
        /// InsertInvoice and Invoice-Case to the database
        /// </summary>
        /// <param name="invoiceDraft">InvoiceDraft contains Invoice and Invoice Case info</param>
        public void InsertInvoice(InvoiceDraftDTO invoiceDraft)
        {
            InvoiceDAO invoiceDAO = InvoiceDAO.CreateInstance();
            try
            {
                InvoiceDTO invoice = new InvoiceDTO();
                //-----------
                invoice.PeriodStartDate = invoiceDraft.PeriodStartDate;
                invoice.PeriodEndDate = invoiceDraft.PeriodEndDate;
                invoice.InvoiceBillAmount = (double)invoiceDraft.TotalAmount;
                invoice.FundingSourceId = int.Parse(invoiceDraft.FundingSourceId);
                invoice.StatusCode = "ACTIVE";
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
                
            }
            catch (Exception ex)
            {
                throw (ex) ;
            }
        }
        /// <summary>
        /// Validate criteria for Invoice Search 
        /// </summary>
        /// <param name="criteria">searchCriteria</param>
        /// <param name="returnMessage">Error Message</param>
        /// <returns>True if ok and false if fail</returns>
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
        /// <summary>
        /// Validate criteria for Invoice Search Case
        /// </summary>
        /// <param name="criteria">searchCriteria</param>
        /// <param name="returnMessage">Error Message</param>
        /// <returns>True if ok and false if fail</returns>
        public bool ValidateInvoiceCaseCriteria(InvoiceCaseSearchCriteriaDTO criteria, out string returnMessage)
        {
            returnMessage = "";
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
            return (returnMessage == "");
        }
        public InvoiceSearchResultDTOCollection InvoiceSearch(InvoiceSearchCriteriaDTO searchCriteria)
        {
            InvoiceSearchResultDTOCollection result = null;
            string returnMessage;
            if (ValidateInvoiceCriteria(searchCriteria, out returnMessage) == false)
                throw new DataValidationException(returnMessage);
            result = InvoiceDAO.CreateInstance().SearchInvoice(searchCriteria);
            return result;
        }
        public ForeclosureCaseDraftDTOCollection InvoiceCaseSearch(InvoiceCaseSearchCriteriaDTO searchCriteria)
        {
            ForeclosureCaseDraftDTOCollection result = null;
            string returnMessage;
            if (ValidateInvoiceCaseCriteria(searchCriteria, out returnMessage) == false)
                throw new DataValidationException(returnMessage);
            result = InvoiceDAO.CreateInstance().InvoiceCaseSearch(searchCriteria);
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
