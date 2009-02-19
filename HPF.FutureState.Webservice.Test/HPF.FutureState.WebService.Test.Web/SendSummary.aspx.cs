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
    public partial class SendSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            int fcid = 0;
            if (!int.TryParse(txtFcId.Text.Trim(), out fcid))
                fcid = int.MinValue;
            SendSummaryRequest request = new SendSummaryRequest()
            {
                FCId = fcid,
                EmailBody = txtBody.Text.Trim(),
                EmailSubject = txtSubject.Text.Trim(),
                EmailToAddress = txtTo.Text.Trim(),
            };

            AuthenticationInfo ai = new AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();

            AgencyWebService proxy = new AgencyWebService();
            proxy.AuthenticationInfoValue = ai;

            SendSummaryResponse response = proxy.SendSummary(request);
            if (response.Status != ResponseStatus.Success)
            {
                if (response.Status == ResponseStatus.Warning)
                    lblMessage.Text = "Warning";
                else
                    lblMessage.Text = "Fail";
                grdvMessages.Visible = true;
                grdvMessages.DataSource = response.Messages;
                grdvMessages.DataBind();
            }
            else
            {
                grdvMessages.Visible = false;
                lblMessage.Text = "Success";
            }
        }       
    }
}
