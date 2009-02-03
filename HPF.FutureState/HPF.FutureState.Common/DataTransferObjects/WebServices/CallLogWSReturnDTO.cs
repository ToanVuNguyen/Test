using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace HPF.FutureState.Common.DataTransferObjects.WebServices
{
    public class CallLogWSReturnDTO
    {
        #region property
        public string CallId { get; set; }                

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }                

        public string CallSourceCd { get; set; }

        public string ReasonToCall { get; set; }

        public string LoanAccountNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? ServicerId { get; set; }

        public string OtherServicerName { get; set; }

        public string PropZipFull9 { get; set; }

        public int? PrevAgencyId { get; set; }

        public string SelectedAgencyId { get; set; }        

        public string FinalDispoCd { get; set; }                

        public string LoanDelinqStatusCd { get; set; }

        public string SelectedCounselor { get; set; }

        public string HomeownerInd { get; set; }

        public string PowerOfAttorneyInd { get; set; }

        public string AuthorizedInd { get; set; }

        #endregion           
    }
}
