using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Collections.Generic;
using HPF.Webservice.CallCenter;

namespace HPF.FutureState.WebService.Test.Web
{
    public class CallCenterHelper
    {
        public static CallLogWSDTO ParseCallLogWSDTO(XDocument xdoc)
        {
            try
            {
                var objs = from obj in xdoc.Descendants("CallLogWSDTO")
                           select new CallLogWSDTO
                           {
                               AuthorizedInd = obj.Element("AuthorizedInd").Value,
                               CallCenter = obj.Element("CallCenter").Value,
                               //CallCenterID = Util.ConvertToInt(obj.Element("CallCenterID").Value),
                               CallSourceCd = obj.Element("CallSourceCd").Value,
                               CcCallKey = obj.Element("CcCallKey").Value,
                               CcAgentIdKey = obj.Element("CcAgentIdKey").Value,
                               DNIS = obj.Element("DNIS").Value,
                               EndDate = Util.ConvertToDateTime(obj.Element("EndDate").Value),
                               FinalDispoCd = obj.Element("FinalDispoCd").Value,
                               FirstName = obj.Element("FirstName").Value,
                               HomeownerInd = obj.Element("HomeownerInd").Value,
                               LastName = obj.Element("LastName").Value,
                               LoanAccountNumber = obj.Element("LoanAccountNumber").Value,
                               LoanDelinqStatusCd = obj.Element("LoanDelinqStatusCd").Value,
                               OtherServicerName = obj.Element("OtherServicerName").Value,
                               PowerOfAttorneyInd = obj.Element("PowerOfAttorneyInd").Value,
                               PrevAgencyId = Util.ConvertToInt(obj.Element("PrevAgencyId").Value),
                               PropZipFull9 = obj.Element("PropZipFull9").Value,
                               ReasonForCall = obj.Element("ReasonToCall").Value,
                               ScreenRout = obj.Element("ScreenRout").Value,
                               SelectedAgencyId = Util.ConvertToInt(obj.Element("SelectedAgencyId").Value),
                               SelectedCounselor = obj.Element("SelectedCounselor").Value,
                               ServicerId = Util.ConvertToInt(obj.Element("ServicerId").Value),
                               StartDate = Util.ConvertToDateTime(obj.Element("StartDate").Value),
                               TransNumber = obj.Element("TransNumber").Value,
                               State = obj.Element("State").Value,
                               City = obj.Element("City").Value,
                               NonprofitReferralKeyNum1 = obj.Element("NonprofitReferralKeyNum1").Value,
                               NonprofitReferralKeyNum2 = obj.Element("NonprofitReferralKeyNum2").Value,
                               NonprofitReferralKeyNum3 = obj.Element("NonprofitReferralKeyNum3").Value,

                               DelinqInd = obj.Element("DelinqInd").Value,
                               PropStreetAddress = obj.Element("PropStreetAddress").Value,
                               PrimaryResidenceInd = obj.Element("PrimaryResidenceInd").Value,
                               MaxLoanAmountInd = obj.Element("MaxLoanAmountInd").Value,
                               CustomerPhone = obj.Element("CustomerPhone").Value,
                               LoanLookupCd = obj.Element("LoanLookupCd").Value,
                               OriginatedPrior2009Ind = obj.Element("OriginatedPrior2009Ind").Value,
                               PaymentAmount = Util.ConvertToDouble(obj.Element("PaymentAmount").Value),
                               GrossIncomeAmount = Util.ConvertToDouble(obj.Element("GrossIncomeAmount").Value),
                               DTIInd = obj.Element("DTIInd").Value,
                               ServicerCANumber = Util.ConvertToInt(obj.Element("ServicerCANumber").Value),
                               ServicerCALastContactDate = Util.ConvertToDateTime(obj.Element("ServicerCALastContactDate").Value),
                               ServicerCAId = Util.ConvertToInt(obj.Element("ServicerCAId").Value),
                               ServicerCAOtherName = obj.Element("ServicerCAOtherName").Value,
                               MHAInfoShareInd = obj.Element("MHAInfoShareInd").Value,
                               ICTCallId = obj.Element("ICTCallId").Value,
                               ServicerComplaintCd = obj.Element("ServicerComplaintCd").Value,
                               MHAScriptStartedInd = obj.Element("MHAScriptStartedInd").Value                               
                           };
                return objs.ToList<CallLogWSDTO>()[0];
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
        }
    }
}
