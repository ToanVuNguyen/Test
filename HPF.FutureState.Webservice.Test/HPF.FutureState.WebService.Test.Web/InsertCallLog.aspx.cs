using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using HPF.FutureState.WebService.Test.Web.HPFCallCenterService;

namespace HPF.FutureState.WebService.Test.Web
{
    public partial class InsertCallLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CallLogInsertRequest request = new CallLogInsertRequest();
            request.CallLog = new CallLogWSDTO();
            request.CallLog.EndDate = DateTime.Now;
            request.CallLog.StartDate = DateTime.Now;
            request.CallLog.CreateDate = DateTime.Now;
            request.CallLog.ChangeLastDate = DateTime.Now;
            request.CallLog.AccountNumber = txtAccountNumber.Text.Trim();
            request.CallLog.AgencyId = txtAgencyId.Text.Trim();
            request.CallLog.CallCenter = txtCallCenter.Text.Trim();
            request.CallLog.CallCenterCD = txtCallCenterCD.Text.Trim();
            //request.CallLog.CallId = txt
            request.CallLog.CallResource = txtCallResource.Text.Trim();
            request.CallLog.ChangeLastAppName = null;
            request.CallLog.ChangeLastUserId = null;
            request.CallLog.CounselPastYRInd = txtCounselPastYRInd.Text.Trim();
            request.CallLog.CreateAppName = null;
            request.CallLog.CreateUserId = null;
            request.CallLog.DNIS = txtDNIS.Text.Trim();
            request.CallLog.ExtCallNumber = txtExtCallNumber.Text.Trim();

            int temp = 0;
            int.TryParse(txtFinalDispo.Text.Trim(), out temp);
            request.CallLog.FinalDispo = temp;
            request.CallLog.FirstName = txtFirstName.Text.Trim();
            request.CallLog.LastName = txtLastName.Text.Trim();
            request.CallLog.MtgProbInd = txtMtgProbInd.Text.Trim();
            request.CallLog.OtherServicerName = txtOtherServicerName.Text.Trim();
            request.CallLog.OutOfNetworkReferralTBD = txtOutOfNetworkReferralTBD.Text.Trim();
            request.CallLog.PastDueInd = txtPastDueInd.Text.Trim();
            
            
            int.TryParse(txtPastDueMonths.Text.Trim(), out temp);
            request.CallLog.PastDueMonths = temp;
            request.CallLog.PastDueSoonInd = txtPastDueSoonInd.Text.Trim();

            int.TryParse(txtPrevAgencyId.Text.Trim(), out temp);
            request.CallLog.PrevAgencyId = temp;

            int.TryParse(txtPrevCounselorId.Text.Trim(), out temp);
            request.CallLog.PrevCounselorId = temp;
            request.CallLog.PropZip = txtPrevCounselorId.Text.Trim();
            request.CallLog.ReasonToCall = txtReasonToCall.Text.Trim();
            request.CallLog.ScreenRout = txtScreenRout.Text.Trim();
            request.CallLog.SelectedAgencyId = txtSelectedAgencyId.Text.Trim();

            int.TryParse(txtServicerId.Text.Trim(), out temp);
            request.CallLog.ServicerId = temp;
            request.CallLog.TransNumber = null;
            

            
            CallCenterService proxy = new CallCenterService();

            CallLogInsertResponse response = proxy.SaveCallLog(request);

            if (response.Status == ResponseStatus.Success)
                lblResult.Text = response.CallLogID;
            else
            {
                grdvResult.DataSource = response.Messages;
                grdvResult.DataBind();
            }         

        }
    }
}
