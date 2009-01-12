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

using HPF.Webservice.CallCenter;
namespace HPF.FutureState.WebService.Test.Web
{
    public partial class RetrieveCallLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            CallCenterService proxy = new CallCenterService();
            

            

            AuthenticationInfo ai = new AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();
            proxy.AuthenticationInfoValue = ai;

            CallLogRetrieveRequest callLogRetrieveRequest = new CallLogRetrieveRequest();
            callLogRetrieveRequest.callLogId = txt_CallLogId.Text;

            CallLogRetrieveResponse callLogRetrieveResponse = new CallLogRetrieveResponse();
            callLogRetrieveResponse = proxy.RetrieveCallLog(callLogRetrieveRequest);
                         
            CallLogWSReturnDTO callLogWSDTO = callLogRetrieveResponse.CallLog;           

            
            lbl_Status.Text = callLogRetrieveResponse.Status.ToString();
            if (lbl_Status.Text != "Success")
            {
                lbl_Message.Text = callLogRetrieveResponse.Messages[0].Message.ToString();
            }
            else
            {
                lbl_Message.Text = "Success";
            }
            
            if (callLogWSDTO != null)
            {
                ArrayList a = new ArrayList();
                a.Add(callLogWSDTO);
                gv_results.DataSource = a;
                gv_results.DataBind();
                gv_results.Visible = true;
            }
            else
            {
                gv_results.Visible = false;
            }
        }
    }
}
