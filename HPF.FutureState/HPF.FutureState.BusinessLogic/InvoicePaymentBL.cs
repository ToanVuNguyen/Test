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
    public class InvoicePaymentBL : BaseBusinessLogic
    {
        private static readonly InvoicePaymentBL instance = new InvoicePaymentBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static InvoicePaymentBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected InvoicePaymentBL()
        {
        }
        public InvoicePaymentDTOCollection InvoicePaymentSearch(InvoiceSearchCriteriaDTO paymentSearchCriteria)
        {
            return (InvoicePaymentDAO.Instance.InvoicePaymentSearch(paymentSearchCriteria));
        }
       
    }
}