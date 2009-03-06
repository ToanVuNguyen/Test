using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class CaseLoanDTOCollection : BaseDTOCollection<CaseLoanDTO>
    {
        public CaseLoanDTO GetCaseLoanByServicer(int? servicerId)
        {
            return this.SingleOrDefault(i => i.ServicerId == servicerId);
        }

        public CaseLoanDTO GetCaseLoan1st()
        {
            return this.SingleOrDefault(i => i.Loan1st2nd == Constant.LOAN_1ST);
        }
    }
}
