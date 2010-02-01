using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace HPF.FutureState.WebService.Test.Web
{
    public class SessionVariables
    {
        public static string BUDGET_ASSET_COLLECTION = "BudgetAssetCollection";
        public static string CASE_LOAN_COLLECTION = "CaseLoanCollection";
        public static string BUDGET_ITEM_COLLECTION = "BudgetItemCollection";
        public static string PROPOSED_BUDGET_ITEM_COLLECTION = "ProposedBudgetItemCollection";
        //public static string ACTIVITY_LOG_COLLECTION = "ActivityLogCollection";
        public static string OUTCOME_ITEM_COLLECTION = "OutcomeItemCollection";
        public static string FORECLOSURE_CASE = "ForeclosureCase";
        public static string CALLLOG_WS = "CallLogWS";

    }
}
