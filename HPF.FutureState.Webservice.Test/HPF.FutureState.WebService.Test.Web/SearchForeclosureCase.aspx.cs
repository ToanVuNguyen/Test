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
namespace HPF.FutureState.WebService.Test.Web
{
    public partial class SearchForeclosureCase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }       

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            ForeclosureCaseSearchRequest request = new ForeclosureCaseSearchRequest();

            request.SearchCriteria = GetSearchCriteria();


            CallCenterService proxy = new CallCenterService();
            AuthenticationInfo ai = new AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();
            proxy.AuthenticationInfoValue = ai;

            ForeclosureCaseSearchResponse response = proxy.SearchForeclosureCase(request);

            if (response.Status == ResponseStatus.Success)
                grdvResult.DataSource = response.Results;
            else
                grdvResult.DataSource = response.Messages;
            grdvResult.DataBind();

            lblResult.Text = "Total rows found: " + response.SearchResultCount.ToString();
            
        }

        private ForeclosureCaseSearchCriteriaDTO GetSearchCriteria()
        {
            ForeclosureCaseSearchCriteriaDTO searchCriteria = new ForeclosureCaseSearchCriteriaDTO();

            searchCriteria.AgencyCaseNumber = (txtAgencyCaseNumber.Text.Trim() == string.Empty) ? null : txtAgencyCaseNumber.Text.Trim();
            searchCriteria.FirstName = (txtFirstName.Text.Trim() == string.Empty) ? null : txtFirstName.Text.Trim();
            searchCriteria.LastName = (txtLastName.Text.Trim() == string.Empty) ? null : txtLastName.Text.Trim();
            searchCriteria.Last4_SSN = (txtLast4SSN.Text.Trim() == string.Empty) ? null : txtLast4SSN.Text.Trim();
            searchCriteria.PropertyZip = (txtPropertyZip.Text.Trim() == string.Empty) ? null : txtPropertyZip.Text.Trim();
            searchCriteria.LoanNumber = (txtLoanNumber.Text.Trim() == string.Empty) ? null : txtLoanNumber.Text.Trim();

            return searchCriteria;
        }
    }
}
