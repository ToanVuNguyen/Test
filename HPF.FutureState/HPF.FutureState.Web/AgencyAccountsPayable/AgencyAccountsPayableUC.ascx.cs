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
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.Web.AgencyAccountsPayable
{
    public partial class AgencyAccountsPayableUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDllAgency();
            }
        }
        protected void BindDllAgency()
        { 
         AgencyDTOCollection agencyCollection= LookupDataBL.Instance.GetAgency();
         ddlAgency.DataSource = agencyCollection;
         ddlAgency.DataTextField = "AgencyName";
         ddlAgency.DataValueField = "AgencyID";
         ddlAgency.DataBind();
        }
    }
}