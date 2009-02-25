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
using HPF.FutureState.Common;
using HPF.FutureState.Web.Security;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Web.AppNewPayable
{
    public partial class ViewEditAgencyPayable : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplySecurity();
            if (!IsPostBack)
            {
                BindTakebackReasonDropDownList();
                BindViewEditPayable();
            }
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_AGENCY_ACCOUNT_PAYABLE))
            {
                btnPayUnpayMarkCase.Enabled = false;
                btnTakeBackMarkCase.Enabled = false;
            }
        }
        protected void BindTakebackReasonDropDownList()
        {
            RefCodeItemDTOCollection takebackReasonCol = LookupDataBL.Instance.GetRefCode("takeback reason code");
            ddlTakebackReason.DataSource = takebackReasonCol;
            ddlTakebackReason.DataTextField = "code";
            ddlTakebackReason.DataValueField = "code";
            ddlTakebackReason.DataBind();
            //add blank to first item in ddl
            ddlTakebackReason.Items.Insert(0, new ListItem("","-1"));
            ddlTakebackReason.Items.FindByText("").Selected = true;
            //ddlTakebackReason.sel
        }
        protected void BindViewEditPayable()
        {
            if (Session["agencyPayable"] != null)
            {
                AgencyPayableDTO agencyPayable = (AgencyPayableDTO)Session["agencyPayable"];
                int? agencypayableid = agencyPayable.AgencyPayableId;
                AgencyPayableSetDTO agencyPayableSet = AgencyPayableBL.Instance.AgencyPayableSetGet(agencypayableid);
                ViewState["agencyPayableSet"] = agencyPayableSet;
                //Bind Agency Payable, from agencypaybleDTO.payable
                lblAgency.Text = agencyPayableSet.Payable.AgencyName;
                lblPeriodStart.Text = agencyPayableSet.Payable.PeriodStartDate.Value.ToShortDateString();
                lblPeriodEnd.Text = agencyPayableSet.Payable.PeriodEndDate.Value.ToShortDateString();
                lblPayableNumber.Text = agencyPayableSet.Payable.PayableNum.ToString();
                
                //bind from agencypayableDTO
                lblTotalCases.Text = agencyPayableSet.TotalCases.ToString();
                lblTotalPayable.Text =String.Format("{0:C}",agencyPayableSet.TotalPayable);
                lblUnpaidNFMCEligibleCase.Text = agencyPayableSet.UnpaidNFMCEligibleCases.ToString();
                lblTotalChargePaid.Text = String.Format("{0:C}",agencyPayableSet.TotalNFMCUpChargePaid);
                lblGrandTotalPaid.Text = String.Format("{0:C}",(agencyPayableSet.TotalNFMCUpChargePaid + agencyPayableSet.TotalPayable));
                //footer
                lblTotalCase_ft.Text = agencyPayableSet.TotalCases.ToString();
                lblPayableTotal_ft.Text = agencyPayableSet.TotalPayable.ToString();
                lblTotalNFMCUpChangePaid_ft.Text = agencyPayableSet.TotalNFMCUpChargePaid.ToString();
                txtPayableComments.Text = agencyPayableSet.Payable.PaymentComment;
                //Bind Agency Payable Case, from agencypayableDTO.payablecase
                grvViewEditAgencyPayable.DataSource = agencyPayableSet.PayableCases;
                grvViewEditAgencyPayable.DataBind();
            }
           
        }
       
        protected void chkCheckAllCheck(object sender, EventArgs e)
        {
            CheckBox headerCheckbox = (CheckBox)sender;
            foreach (GridViewRow row in grvViewEditAgencyPayable.Rows)
            {
                CheckBox chkSelected = (CheckBox)row.FindControl("chkSelected");
                if (chkSelected != null)
                    chkSelected.Checked = headerCheckbox.Checked;
            }
        }
       
        protected void btnPayUnpayMarkCase_Click(object sender, EventArgs e)
        {
            bulErrorMessage.Items.Clear();
            string payableCaseIdCollection = GetSelectedRow();

            AgencyPayableSetDTO agencyPayableSet = (AgencyPayableSetDTO)ViewState["agencyPayableSet"];
            if (payableCaseIdCollection == null)
            {
                bulErrorMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined("ERR0575"));
                return;
            }
            try
            {
                agencyPayableSet.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                AgencyPayableBL.Instance.PayUnPayMarkCase(agencyPayableSet, payableCaseIdCollection);
                BindViewEditPayable();
            }
            catch (Exception ex)
            {
                bulErrorMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }
        }
        protected void btnTakeBackMarkCase_Click(object sender, EventArgs e)
        {

            bulErrorMessage.Items.Clear();
            string payableCaseIdCollection = GetSelectedRow();
            string takebackReason = ddlTakebackReason.SelectedItem.Text;

            AgencyPayableSetDTO agencyPayableSet = (AgencyPayableSetDTO)ViewState["agencyPayableSet"];
            if (payableCaseIdCollection == null)
            {
                bulErrorMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined("ERR0575"));
                return;
            }
            try
            {
                agencyPayableSet.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                AgencyPayableBL.Instance.TakebackMarkCase(agencyPayableSet,takebackReason,payableCaseIdCollection);
                BindViewEditPayable();
            }
            catch (Exception ex)
            {
                bulErrorMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }
        }
        protected string GetSelectedRow()
        {
            bulErrorMessage.Items.Clear();
            string result = "";
            AgencyPayableSetDTO agencyPayableSet = (AgencyPayableSetDTO)ViewState["agencyPayableSet"];
           
            foreach (GridViewRow row in grvViewEditAgencyPayable.Rows)
            {
                CheckBox chkSelected = (CheckBox)row.FindControl("chkSelected");
                if (chkSelected != null)
                    if (chkSelected.Checked == true)
                    {
                        result += agencyPayableSet.PayableCases[row.DataItemIndex].AgencyPayableId.ToString() + ",";
                        if (agencyPayableSet.PayableCases[row.DataItemIndex].NFMCDifferenceEligibleInd == "N")
                        { 
                        bulErrorMessage.Items.Add(new ListItem(ErrorMessages.GetExceptionMessageCombined("ERR0582")));
                        return null;
                        }
                    }
            }
            if (result == "")
                return null;
            result = result.Remove(result.LastIndexOf(","), 1);
            return result;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            //if (Session["agencyPayable"] != null)
            {
                Response.Redirect("AgencyPayable.aspx");
            }
        }

        
    }
}