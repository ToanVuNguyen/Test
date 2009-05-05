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

using HPF.Webservice.CallCenter;
using HPF.Webservice.Agency;

namespace HPF.FutureState.WebService.Test.Web
{
    public partial class ICTSearchForeclosureCase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }       

        protected void BtnSearch_Click(object sender, EventArgs e)
        {            
            CallCenter_SearchForeclosureCase();
        }
               
        private void CallCenter_SearchForeclosureCase()
        {
            HPF.Webservice.CallCenter.ICTForeclosureCaseSearchRequest request = CallCenter_GetSearchCriteriaRequest();

            CallCenterService proxy = new CallCenterService();
            
            HPF.Webservice.CallCenter.AuthenticationInfo ai = new HPF.Webservice.CallCenter.AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();
            proxy.AuthenticationInfoValue = ai;

            HPF.Webservice.CallCenter.ForeclosureCaseSearchResponse response = proxy.ICTSearchForeclosureCase(request);

            if (response.Status == HPF.Webservice.CallCenter.ResponseStatus.Success ||
                response.Status == HPF.Webservice.CallCenter.ResponseStatus.Warning)
            {
                grdvResult.DataSource = response.Results;
            }
            else
            {
                grdvResult.DataSource = response.Messages;
            }
            grdvResult.DataBind();

            if (response.Status == HPF.Webservice.CallCenter.ResponseStatus.Warning)
                lblResult.Text = string.Format("{0} -- {1}", response.Messages.First().ErrorCode, response.Messages.First().Message);
            else
                lblResult.Text = "Total rows found: " + response.SearchResultCount.ToString();

        }

        private HPF.Webservice.CallCenter.ICTForeclosureCaseSearchRequest CallCenter_GetSearchCriteriaRequest()
        {
            HPF.Webservice.CallCenter.ICTForeclosureCaseSearchRequest searchCriteria = new HPF.Webservice.CallCenter.ICTForeclosureCaseSearchRequest();
            
            searchCriteria.FirstName = (txtFirstName.Text.Trim() == string.Empty) ? null : txtFirstName.Text.Trim();
            searchCriteria.LastName = (txtLastName.Text.Trim() == string.Empty) ? null : txtLastName.Text.Trim();         
            searchCriteria.PropertyZip = (txtPropertyZip.Text.Trim() == string.Empty) ? null : txtPropertyZip.Text.Trim();
            searchCriteria.LoanNumber = (txtLoanNumber.Text.Trim() == string.Empty) ? null : txtLoanNumber.Text.Trim();

            return searchCriteria;
        }
    }
}
