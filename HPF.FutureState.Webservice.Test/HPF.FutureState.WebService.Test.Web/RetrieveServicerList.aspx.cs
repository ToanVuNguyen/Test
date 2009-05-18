using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using HPF.Webservice.Agency;

namespace HPF.FutureState.WebService.Test.Web
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AgencyWebService proxy = new AgencyWebService();
            HPF.Webservice.Agency.AuthenticationInfo ai = new HPF.Webservice.Agency.AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();
            proxy.AuthenticationInfoValue = ai;

            lblStatus.Text = "Status: Success";
            grdMessage.Visible = false;
            grdServicers.Visible = false;
            
            HPF.Webservice.Agency.ServicerListRetrieveResponse response = proxy.RetrieveServicerList();
            if (response.Status != ResponseStatus.Success)
            {
                lblStatus.Text = "Status: " + response.Status.ToString();
                lblMessage.Text = "Message:";
                grdMessage.Visible = true;
                grdMessage.DataSource = response.Messages;
                grdMessage.DataBind();
            }
            else
            {
                grdServicers.Visible = true;
                grdServicers.DataSource = response.Servicers;
                grdServicers.DataBind();
            }

        }        
    }
}
