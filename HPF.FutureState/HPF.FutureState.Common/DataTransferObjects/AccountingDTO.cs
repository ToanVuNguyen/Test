using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class AccountingDTO : BaseDTO
    {
        //Billing info
        public string NeverBillReason { get; set; }
        public BillingInfoDTOCollection BillingInfo { get; set; }
        //Payment info
        public string NerverPayReason { get; set; }
        public AgencyPayableCaseDTOCollection AgencyPayableCase { get; set; }
    }
}
