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
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;

namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class CaseFollowUp : System.Web.UI.UserControl
    {
        private bool _isUpdating = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            grdvCaseFollowUpBinding();

            ddlFollowUpSourceBinding();
            ddlCreditReportBureauBinding();
            ddlOutcomeBinding();
            ddlDelinquencyStatusBinding();
            ddlStillInHomeBinding();
        }

        private void grdvCaseFollowUpBinding()
        {
            int caseID = int.Parse(Request.QueryString["CaseID"].ToString());
            CaseFollowUpDTOCollection caseFollowUps = RetrieveCaseFollowUps(caseID);
            if (caseFollowUps.Count > 0)
            {
                grd_FollowUpList.DataSource = caseFollowUps;
                grd_FollowUpList.DataBind();
            }
            else
            {
                caseFollowUps.Add(new CaseFollowUpDTO());

                grd_FollowUpList.DataSource = caseFollowUps;
                grd_FollowUpList.DataBind();

                int TotalColumns = grd_FollowUpList.Rows[0].Cells.Count;
                grd_FollowUpList.Rows[0].Cells.Clear();
                grd_FollowUpList.Rows[0].Cells.Add(new TableCell());
                grd_FollowUpList.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grd_FollowUpList.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        //Bind data to follow-up source
        private void ddlFollowUpSourceBinding()
        {
            ddl_FollowUpSource.Items.Clear();
            RefCodeItemDTOCollection followUpTypeCodes = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_FOLLOW_UP_SOURCE_CODE);
            ddl_FollowUpSource.DataValueField = "Code";
            ddl_FollowUpSource.DataTextField = "CodeDesc";
            ddl_FollowUpSource.DataSource = followUpTypeCodes;
            ddl_FollowUpSource.DataBind();
            ddl_FollowUpSource.Items.Insert(0, new ListItem(string.Empty));
        }

        //Bind data to credir report bureau
        private void ddlCreditReportBureauBinding()
        {
            ddl_CreditReportBureau .Items.Clear();
            RefCodeItemDTOCollection followUpTypeCodes = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_CREDIR_BERREAU_CODE);
            ddl_CreditReportBureau.DataValueField = "Code";
            ddl_CreditReportBureau.DataTextField = "CodeDesc";
            ddl_CreditReportBureau.DataSource = followUpTypeCodes;
            ddl_CreditReportBureau.DataBind();
            ddl_CreditReportBureau.Items.Insert(0, new ListItem(string.Empty));
        }

        //Bind data to follow up outcome 
        private void ddlOutcomeBinding()
        {
            ddl_FollowUpOutcome.Items.Clear();
            OutcomeTypeDTOCollection followUpTypeCodes = CaseFollowUpBL.Instance.RetrieveOutcomeTypes();
            ddl_FollowUpOutcome.DataValueField = "OutcomeTypeID";
            ddl_FollowUpOutcome.DataTextField = "OutcomeTypeName";
            ddl_FollowUpOutcome.DataSource = followUpTypeCodes;
            ddl_FollowUpOutcome.DataBind();
            ddl_FollowUpOutcome.Items.Insert(0, new ListItem(string.Empty));
        }

        //Bind data to delinquncy status
        private void ddlDelinquencyStatusBinding()
        {
            ddl_DelinqencyStatus.Items.Clear();
            RefCodeItemDTOCollection followUpTypeCodes = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_CREDIR_BERREAU_CODE);
            ddl_DelinqencyStatus.DataValueField = "Code";
            ddl_DelinqencyStatus.DataTextField = "CodeDesc";
            ddl_DelinqencyStatus.DataSource = followUpTypeCodes;
            ddl_DelinqencyStatus.DataBind();
            ddl_DelinqencyStatus.Items.Insert(0, new ListItem(string.Empty));
        }

        //Bind data to still in home
        private void ddlStillInHomeBinding()
        {
            ddl_StillInHome.Items.Clear();
            ddl_StillInHome.Items.Add(new ListItem(string.Empty));
            ddl_StillInHome.Items.Add(new ListItem(Constant.INDICATOR_YES_FULL));
            ddl_StillInHome.Items.Add(new ListItem(Constant.INDICATOR_NO_FULL));
        }

        private CaseFollowUpDTOCollection RetrieveCaseFollowUps(int fcid)
        {
            return CaseFollowUpBL.Instance.RetrieveCaseFollowUps(fcid);
        }

        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                //btnDelete.Enabled = false;
                //btnReinstate.Enabled = false;
            }
            else
            {
                //btnDelete.Enabled = true;
                //btnReinstate.Enabled = true;
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            CaseFollowUpDTO caseFollowUp = new CaseFollowUpDTO();
            int caseID = int.Parse(Request.QueryString["CaseID"].ToString());
            caseFollowUp.FcId = caseID;
        }
    }
}