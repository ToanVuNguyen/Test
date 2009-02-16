using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public enum AgencyPayableCaseUpdateFlag { TakebackMarkCase = 0, PayUnpayMarkCase = 1 }
    [Serializable]
    public class AgencyPayableSetDTO : BaseDTO
    {
        public AgencyPayableDTO Payable { get; set; }
        public AgencyPayableCaseDTOCollection PayableCases { get; set; }
        //use for Update InvoiceCase
        public string PaymentRejectReason { get; set; }
        public int InvoicePaymentId { get; set; }

        public AgencyPayableSetDTO()
        {
            PayableCases = new AgencyPayableCaseDTOCollection();
        }
        public int TotalCases
        {
            get { return PayableCases.Count; }
        }
        public double TotalPayable
        {
            get
            {
                if (TotalCases == 0)
                    return 0;
                double sum = 0;
                foreach (var payableCase in PayableCases)
                    sum += payableCase.PaymentAmount == null ? 0 : payableCase.PaymentAmount.Value;
                return sum;
            }
        }
        public double TotalNFMCUpChargePaid
        {
            get
            {
                if (TotalCases == 0)
                    return 0;
                double sum = 0;
                foreach (var payableCase in PayableCases)
                    sum += payableCase.NFMCDifferencePaidAmt == null ? 0 : payableCase.NFMCDifferencePaidAmt.Value;
                return sum;
            }
        }
        public double UnpaidNFMCEligibleCases
        {
            get
            {
                if (TotalCases == 0)
                    return 0;
                double sum = 0;
                foreach (var payableCase in PayableCases)
                    if (payableCase.NFMCDifferenceEligibleInd == "Y" && payableCase.NFMCDifferencePaidAmt == null)
                        sum++;
                return sum;
            }
        }
    }
}
