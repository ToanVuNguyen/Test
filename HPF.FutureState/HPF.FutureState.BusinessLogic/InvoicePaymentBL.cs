using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common.Utils.DataValidator;
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
        public InvoicePaymentDTOCollection InvoicePaymentSearch(InvoiceSearchCriteriaDTO searchCriteria)
        {
            ExceptionMessageCollection exCol = new ExceptionMessageCollection();
            DataValidationException dataValidEx = new DataValidationException();
            ValidationResults valResult = HPFValidator.Validate<InvoiceSearchCriteriaDTO>(searchCriteria, Constant.RULESET_PAYMENTVALIDATION);
            if (!valResult.IsValid)
                foreach (var valMes in valResult)
                {
                    string errorCode = string.IsNullOrEmpty(valMes.Tag) ? "Error" : valMes.Tag;
                    string errorMes = string.IsNullOrEmpty(valMes.Tag) ? valMes.Message : ErrorMessages.GetExceptionMessage(valMes.Tag);
                    dataValidEx.ExceptionMessages.AddExceptionMessage(errorCode, errorMes);
                }
            if (dataValidEx.ExceptionMessages.Count > 0)
                throw dataValidEx;
            return (InvoicePaymentDAO.Instance.InvoicePaymentSearch(searchCriteria));
        }
        public InvoicePaymentDTO InvoicePaymentGet(int invoicePaymentId)
        { 
            return (InvoicePaymentDAO.Instance.InvoicePaymentGet(invoicePaymentId));
        }
       
    }
}