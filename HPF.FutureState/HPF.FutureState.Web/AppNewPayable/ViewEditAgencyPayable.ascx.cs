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
            bulErrorMessage.Items.Clear();
            hidIsSelected.Value = GetSelectedRow();
            btnTakeBackMarkCase.Attributes.Add("onclick", "return TakeBackReason();");
            btnPayUnpayMarkCase.Attributes.Add("onclick", "return PayUnpay();");
           
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
            RefCodeItemDTOCollection takebackReasonCol = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_TAKE_BACK_REASON_CODE);
            ddlTakebackReason.DataSource = takebackReasonCol;
            ddlTakebackReason.DataTextField = "CodeDesc";
            ddlTakebackReason.DataValueField = "Code";
            ddlTakebackReason.DataBind();
            //add blank to first item in ddl
            ddlTakebackReason.Items.Insert(0, new ListItem("", "-1"));
            ddlTakebackReason.Items.FindByText("").Selected = true;
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
                lblTotalPayable.Text = String.Format("{0:C}", agencyPayableSet.TotalPayable);
                lblUnpaidNFMCEligibleCase.Text = agencyPayableSet.UnpaidNFMCEligibleCases.ToString();
                lblTotalChargePaid.Text = String.Format("{0:C}", agencyPayableSet.TotalNFMCUpChargePaid);
                lblGrandTotalPaid.Text = String.Format("{0:C}", (agencyPayableSet.TotalNFMCUpChargePaid + agencyPayableSet.TotalPayable));
                //footer
                lblTotalCase_ft.Text = agencyPayableSet.TotalCases.ToString();
                lblPayableTotal_ft.Text = agencyPayableSet.TotalPayable.ToString();
                lblTotalNFMCUpChangePaid_ft.Text = agencyPayableSet.TotalNFMCUpChargePaid.ToString();
                txtPayableComments.Text = agencyPayableSet.Payable.PaymentComment;
                //Bind Agency Payable Case, from agencypayableDTO.payablecase
                grvViewEditAgencyPayable.DataSource = agencyPayableSet.PayableCases;
                grvViewEditAgencyPayable.DataBind();
                hidIsSelected.Value = "";
                hidPayUnpayCheck.Value = "";
            }
        }
        
        protected void chkSelected(object sender, EventArgs e)
        {
          
        }
        protected void btnPayUnpayMarkCase_Click(object sender, EventArgs e)
        {
            bulErrorMessage.Items.Clear();
            PayUnPay();
        }

        private void PayUnPay()
        {
            string payableCaseIdCollection = GetSelectedRow();
            AgencyPayableSetDTO agencyPayableSet = (AgencyPayableSetDTO)ViewState["agencyPayableSet"];

            if (payableCaseIdCollection == null)
            {
                bulErrorMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined("ERR0577"));
                return;
            }
            if (hidPayUnpayCheck.Value == "-1")
            {
                bulErrorMessage.Items.Add(new ListItem(ErrorMessages.GetExceptionMessageCombined("ERR0582")));
                hidPayUnpayCheck.Value = "";
                return;
            }
            try
            {
                agencyPayableSet.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                int i;
                for (i = 0; i < agencyPayableSet.PayableCases.Count; i++)
                //foreach (var agencyPayableCase in agencyPayableSet.PayableCases)
                {
                    if (payableCaseIdCollection.Contains(agencyPayableSet.PayableCases[i].AgencyPayableId.ToString()))
                    {
                        if (agencyPayableSet.PayableCases[i].NFMCDifferencePaidAmt == null)
                            AgencyPayableBL.Instance.PayUnPayMarkCase(agencyPayableSet, payableCaseIdCollection, 1);
                        else AgencyPayableBL.Instance.PayUnPayMarkCase(agencyPayableSet, payableCaseIdCollection, 2);
                        payableCaseIdCollection = payableCaseIdCollection.Replace(agencyPayableSet.PayableCases[i].AgencyPayableId.ToString() + ",", "");
                        payableCaseIdCollection = payableCaseIdCollection.Replace(agencyPayableSet.PayableCases[i].AgencyPayableId.ToString(), "");
                    }
                }
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
            TakeBackReason();
        }

        private void TakeBackReason()
        {
            
            try
            {
                string payableCaseIdCollection = GetSelectedRow();
                string takebackReason = ddlTakebackReason.SelectedItem.Value;
                if (takebackReason == "-1")
                    takebackReason = null;

                AgencyPayableSetDTO agencyPayableSet = (AgencyPayableSetDTO)ViewState["agencyPayableSet"];
                if (payableCaseIdCollection == null)
                {
                    bulErrorMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined("ERR0575"));
                    return;
                }
                agencyPayableSet.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                AgencyPayableBL.Instance.TakebackMarkCase(agencyPayableSet, takebackReason, payableCaseIdCollection);
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
                            hidPayUnpayCheck.Value = "-1";
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
            Response.Redirect("AgencyPayable.aspx");
        }
        protected void btnReprintPayable_Click(object sender, EventArgs e)
        {
            AgencyPayableDTO agencyPayable = (AgencyPayableDTO)Session["AgencyPayable"];
            string agencyPayableId = agencyPayable.AgencyPayableId.ToString();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Print Payable", "window.open('ReprintPayable.aspx?agencyPayableId=" + agencyPayableId + "','','menu=no,scrollbars=no,resizable=yes,top=0,left=0,width=1010px,height=900px');", true);
        }
        protected void grvViewEditAgencyPayable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //when datetime=mindate -> display blank
                Label lblComleteDate = (Label)e.Row.FindControl("lblCompleteDate");
                Label lblTakeBackDate = (Label)e.Row.FindControl("lblTakeBackDate");
                DateTime mindate = DateTime.MinValue;
                if (mindate.CompareTo(Convert.ToDateTime(lblComleteDate.Text)) == 0)
                {
                    lblComleteDate.Text = "";
                }
                if (mindate.CompareTo(Convert.ToDateTime(lblTakeBackDate.Text)) == 0)
                {
                    lblTakeBackDate.Text = "";
                }
                
                //add attribute to check isselected
                CheckBox chkSelected = e.Row.FindControl("chkSelected") as CheckBox;
                if (chkSelected != null)
                    chkSelected.AutoPostBack = true;
            }
        }
        protected void btnYesTakeBackReason_Click(object sender, EventArgs e)
        {
            TakeBackReason();
        }
        protected void btnYesPayUnpay_Click(object sender, EventArgs e)
        {
            PayUnPay();
        }
    }
}