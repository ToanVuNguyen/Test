using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class InvoicePaymentDTO : BaseDTO
    {

        public int? InvoicePaymentID { get; set; }
        
        public string FundingSourceName { get; set; }
        [NullableOrInRangeNumberValidator(false, "1", "100000000", Ruleset = Constant.RULESET_INVOICE_PAYMENT_VALIDATION, Tag = ErrorMessages.ERR0650)]      
        public int? FundingSourceID { get; set; }
        [NullableOrStringLengthValidator(false, 30, "PaymentNum", Ruleset = Constant.RULESET_INVOICE_PAYMENT_VALIDATION, Tag = ErrorMessages.ERR0686)]
        public string PaymentNum { get; set; }
        [NullableOrInRangeNumberValidator(false, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_INVOICE_PAYMENT_VALIDATION, Tag = ErrorMessages.ERR0652)]
        public DateTime? PaymentDate { get; set; }
        [NullableOrStringLengthValidator(false, 50, "PaymentType", Ruleset = Constant.RULESET_INVOICE_PAYMENT_VALIDATION, Tag = ErrorMessages.ERR0653)]
        public string PaymentType { get; set; }
        public string PaymentTypeDesc { get; set; }
        [NullableOrInRangeNumberValidator(false, "0", "9999999999999.99", Ruleset = Constant.RULESET_INVOICE_PAYMENT_VALIDATION, Tag = ErrorMessages.ERR0654)]      
        public double? PaymentAmount { get; set; }
        [NullableOrStringLengthValidator(true, 300, "Comments", Ruleset = Constant.RULESET_INVOICE_PAYMENT_VALIDATION, Tag = ErrorMessages.ERR0688)]
        public string Comments { get; set; }
        [NullableOrStringLengthValidator(true, 300, "PaymentFile", Ruleset = Constant.RULESET_INVOICE_PAYMENT_VALIDATION, Tag = ErrorMessages.ERR0687)]
        public string PaymentFile { get; set; }
    }
}
