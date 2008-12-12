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

using HPF.FutureState.WebService.Test.Web.HPFCallCenterService;

namespace HPF.FutureState.WebService.Test.Web
{
    public partial class SearchForeclosureCase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }       

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            ForeClosureCaseSearchRequest request = new ForeClosureCaseSearchRequest();

            request.SearchCriteria = new ForeClosureCaseSearchCriteriaDTO();

            
            request.SearchCriteria.AgencyCaseNumber = (txtAgencyCaseNumber.Text.Trim() == string.Empty)? null : txtAgencyCaseNumber.Text.Trim() ;
            request.SearchCriteria.FirstName = (txtFirstName.Text.Trim() == string.Empty) ? null : txtFirstName.Text.Trim();
            request.SearchCriteria.LastName = (txtLastName.Text.Trim() == string.Empty) ? null : txtLastName.Text.Trim();
            request.SearchCriteria.Last4_SSN = (txtLast4SSN.Text.Trim() == string.Empty) ? null : txtLast4SSN.Text.Trim();
            request.SearchCriteria.PropertyZip = (txtPropertyZip.Text.Trim() == string.Empty) ? null : txtPropertyZip.Text.Trim();
            request.SearchCriteria.LoanNumber = (txtLoanNumber.Text.Trim() == string.Empty) ? null : txtLoanNumber.Text.Trim();

            CallCenterService proxy = new CallCenterService();
            
            ForeClosureCaseSearchResponse response = proxy.SearchForeClosureCase(request);

            if (response.Status == ResponseStatus.Success)
                grdvResult.DataSource = response.Results;
            else
                grdvResult.DataSource = response.Messages;
            grdvResult.DataBind();

            lblResult.Text = "Total rows found: " + response.SearchResultCount.ToString();
            
        }
    }
}
