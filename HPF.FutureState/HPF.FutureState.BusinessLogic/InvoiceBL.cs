using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.DataTransferObjects;

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

        public bool InsertInvoice(InvoiceDTO invoice)
        {
            throw new NotImplementedException();
        }

        public bool InsertInvoiceCase(InvoiceCaseDTO invoiceCase)
        {
            throw new NotImplementedException();
        }

        public InvoiceCaseDTOCollection SearchInvoice(InvoiceSearchCriterialDTO criterial)
        {
            throw new NotImplementedException();
        }

        public InvoiceDraftDTOCollection CreateInvoiceDraft(InvoiceSearchCriterialDTO criteria)
        {
            throw new NotImplementedException();
        }
    }
}
