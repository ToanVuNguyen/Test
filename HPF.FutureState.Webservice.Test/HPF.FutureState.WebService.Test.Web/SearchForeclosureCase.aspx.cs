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
    public partial class SearchForeclosureCase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }       

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (ddlUserType.SelectedValue.Equals("Agency"))
                Agency_SearchForeclosureCase();
            else
                CallCenter_SearchForeclosureCase();
        }

        private void Agency_SearchForeclosureCase()
        {
            HPF.Webservice.Agency.ForeclosureCaseSearchRequest request = new HPF.Webservice.Agency.ForeclosureCaseSearchRequest();

            request.SearchCriteria = Agency_GetSearchCriteria();

            //CallCenterService proxy = new CallCenterService();
            AgencyWebService proxy = new AgencyWebService();
            HPF.Webservice.Agency.AuthenticationInfo ai = new HPF.Webservice.Agency.AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();
            proxy.AuthenticationInfoValue = ai;

            HPF.Webservice.Agency.ForeclosureCaseSearchResponse response = proxy.SearchForeclosureCase(request);

            if (response.Status == HPF.Webservice.Agency.ResponseStatus.Success)
                grdvResult.DataSource = response.Results;
            else
                grdvResult.DataSource = response.Messages;
            grdvResult.DataBind();

            lblResult.Text = "Total rows found: " + response.SearchResultCount.ToString();

        }

        private void CallCenter_SearchForeclosureCase()
        {
            HPF.Webservice.CallCenter.ForeclosureCaseSearchRequest request = new HPF.Webservice.CallCenter.ForeclosureCaseSearchRequest();

            request.SearchCriteria = CallCenter_GetSearchCriteria();

            CallCenterService proxy = new CallCenterService();
            
            HPF.Webservice.CallCenter.AuthenticationInfo ai = new HPF.Webservice.CallCenter.AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();
            proxy.AuthenticationInfoValue = ai;

            HPF.Webservice.CallCenter.ForeclosureCaseSearchResponse response = proxy.SearchForeclosureCase(request);

            if (response.Status == HPF.Webservice.CallCenter.ResponseStatus.Success)
                grdvResult.DataSource = response.Results;
            else
                grdvResult.DataSource = response.Messages;
            grdvResult.DataBind();

            lblResult.Text = "Total rows found: " + response.SearchResultCount.ToString();


        }

        private HPF.Webservice.Agency.ForeclosureCaseSearchCriteriaDTO Agency_GetSearchCriteria()
        {
            HPF.Webservice.Agency.ForeclosureCaseSearchCriteriaDTO searchCriteria = new HPF.Webservice.Agency.ForeclosureCaseSearchCriteriaDTO();

            searchCriteria.AgencyCaseNumber = (txtAgencyCaseNumber.Text.Trim() == string.Empty) ? null : txtAgencyCaseNumber.Text.Trim();
            searchCriteria.FirstName = (txtFirstName.Text.Trim() == string.Empty) ? null : txtFirstName.Text.Trim();
            searchCriteria.LastName = (txtLastName.Text.Trim() == string.Empty) ? null : txtLastName.Text.Trim();
            searchCriteria.Last4_SSN = (txtLast4SSN.Text.Trim() == string.Empty) ? null : txtLast4SSN.Text.Trim();
            searchCriteria.PropertyZip = (txtPropertyZip.Text.Trim() == string.Empty) ? null : txtPropertyZip.Text.Trim();
            searchCriteria.LoanNumber = (txtLoanNumber.Text.Trim() == string.Empty) ? null : txtLoanNumber.Text.Trim();

            return searchCriteria;
        }

        private HPF.Webservice.CallCenter.ForeclosureCaseSearchCriteriaDTO CallCenter_GetSearchCriteria()
        {
            HPF.Webservice.CallCenter.ForeclosureCaseSearchCriteriaDTO searchCriteria = new HPF.Webservice.CallCenter.ForeclosureCaseSearchCriteriaDTO();

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
