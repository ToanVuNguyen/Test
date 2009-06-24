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
    public partial class ICTSearchCall : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }       

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchCall();
        }
               
        private void SearchCall()
        {
            CallLogSearchRequest request = new CallLogSearchRequest();
            AgencyWebService proxy = new AgencyWebService();
            
            AuthenticationInfo ai = new AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();
            proxy.AuthenticationInfoValue = ai;

            request.SearchCriteria = GetSearchCriteriaRequest();
            CallLogSearchResponse response = proxy.SearchCallLog(request);

            if (response.Status == ResponseStatus.Success)
            {
                grdvResult.DataSource = response.CallLogs;
                lblResult.Text = "Total rows found: " + response.CallLogs.Length.ToString();
            }
            else
            {
                grdvResult.DataSource = response.Messages;
            }
            grdvResult.DataBind();                       
        }

        private CallLogSearchCriteriaDTO GetSearchCriteriaRequest()
        {
            CallLogSearchCriteriaDTO searchCriteria = new CallLogSearchCriteriaDTO();

            searchCriteria.FirstName = txtFirstName.Text;
            searchCriteria.LastName = txtLastName.Text;
            searchCriteria.LoanNumber = txtLoanNumber.Text;

            return searchCriteria;
        }
    }
}
