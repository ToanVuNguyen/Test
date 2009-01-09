using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.DataTransferObjects;
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

        public InvoiceSearchResultDTOCollection SearchInvoice(InvoiceSearchCriteriaDTO criterial)
        {
            return InvoiceDAO.CreateInstance().SearchInvoice(criterial);
        }

        public InvoiceDraftDTOCollection CreateInvoiceDraft(InvoiceSearchCriteriaDTO criteria)
        {
            throw new NotImplementedException();
        }
    }
}
