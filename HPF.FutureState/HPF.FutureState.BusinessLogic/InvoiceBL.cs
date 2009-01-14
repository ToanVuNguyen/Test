using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;

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

        public bool InsertInvoice(InvoiceDraftDTO invoiceDraft)
        {
            throw new NotImplementedException();
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

        public InvoiceDraftDTOCollection CreateInvoiceDraft(InvoiceSearchCriteriaDTO criteria)
        {
            throw new NotImplementedException();
        }
    }
}
