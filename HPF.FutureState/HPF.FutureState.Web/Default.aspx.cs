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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Web.HPFWebControls;



namespace HPF.FutureState.Web
{
    public partial class Default : System.Web.UI.Page
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!IsPostBack)
            //    UserControlLoader1.LoadUC("AppForeclosureCaseSearch//AppForeClosureCaseSearch.ascx", "abc");            
        }

        

        protected void Button1_Click1(object sender, EventArgs e)
        {
            //AppForeclosureCaseSearchCriteriaDTO searchCritera = new AppForeclosureCaseSearchCriteriaDTO { Agency=-1, ForeclosureCaseID=-1, Program=-1 };
            //AppForeclosureCaseSearchResultDTOCollection result = ForeclosureCaseSetBL.Instance.AppSearchforeClosureCase(searchCritera);
            
        }              
    }   
}
