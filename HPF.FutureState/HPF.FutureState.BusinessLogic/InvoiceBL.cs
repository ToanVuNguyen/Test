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

        private bool ValidateCriteria(InvoiceSearchCriteriaDTO criteria,out string returnMessage)
        {
            returnMessage = "";
            if (criteria.PeriodStart == DateTime.MinValue)
                returnMessage += "Wrong input on Period Start; ";
            if (criteria.PeriodEnd == DateTime.MinValue)
                returnMessage += "Wrong input on Period End; ";
            if ((criteria.PeriodStart > criteria.PeriodEnd)&&returnMessage=="")
                returnMessage += "Period Start can not larger than Period End; ";
            return (returnMessage == "");
        }
        public InvoiceSearchResultDTOCollection SearchInvoice(InvoiceSearchCriteriaDTO criterial)
        {
            InvoiceSearchResultDTOCollection result = null;
            string returnMessage;
            if (ValidateCriteria(criterial, out returnMessage) == false)
                throw new DataValidationException(returnMessage);
            try
            {
                result = InvoiceDAO.CreateInstance().SearchInvoice(criterial);
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
