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

using HPF.Webservice.Agency;
namespace HPF.FutureState.WebService.Test.Web
{
    public partial class RetrieveCallLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            AgencyWebService proxy = new AgencyWebService();
            

            

            AuthenticationInfo ai = new AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();
            proxy.AuthenticationInfoValue = ai;

            CallLogRetrieveRequest callLogRetrieveRequest = new CallLogRetrieveRequest();
            callLogRetrieveRequest.ICTCallId = txt_CallLogId.Text;

            CallLogRetrieveResponse callLogRetrieveResponse = new CallLogRetrieveResponse();
            callLogRetrieveResponse = proxy.RetrieveCallLog(callLogRetrieveRequest);
                         
            CallLogWSReturnDTO[] callLogWSDTOs = callLogRetrieveResponse.CallLogs;           

            
            lbl_Status.Text = callLogRetrieveResponse.Status.ToString();
            if (callLogRetrieveResponse.Status != ResponseStatus.Success)
            {
                grdvMsg.Visible = true;
                lbl_Message.Text = "";
                grdvMsg.DataSource = callLogRetrieveResponse.Messages;
                grdvMsg.DataBind();

                gv_results.Visible = false;
            }
            else
            {
                //lbl_Message.Text = "Success";
                grdvMsg.Visible = false;

                gv_results.DataSource = callLogWSDTOs;
                gv_results.DataBind();
                gv_results.Visible = true;
            }                        
        }
    }
}
