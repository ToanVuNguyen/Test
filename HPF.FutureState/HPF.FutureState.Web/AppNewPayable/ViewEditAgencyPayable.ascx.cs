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
using System.Collections.Generic;
using System.Text;

namespace HPF.FutureState.Web.AppNewPayable
{
    public partial class ViewEditAgencyPayable : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplySecurity();
            bulErrorMessage.Items.Clear();
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
            RefCodeItemDTOCollection takebackReasonCol = LookupDataBL.Instance.GetRefCodes(Constant.REF_CODE_SET_TAKE_BACK_REASON_CODE);
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
                lblPayableTotal_ft.Text = agencyPayableSet.TotalPayable.ToString("C");
                lblTotalNFMCUpChangePaid_ft.Text = agencyPayableSet.TotalNFMCUpChargePaid.ToString();
                txtPayableComments.Text = agencyPayableSet.Payable.PaymentComment;
                //Bind Agency Payable Case, from agencypayableDTO.payablecase
                grvViewEditAgencyPayable.DataSource = agencyPayableSet.PayableCases;
                grvViewEditAgencyPayable.DataBind();
                hidIsSelected.Value = "";
                hidPayUnpayCheck.Value = "";
            }
        }
        
        protected void btnPayUnpayMarkCase_Click(object sender, EventArgs e)
        {
            bulErrorMessage.Items.Clear();
            PayUnPay();
        }

        private void PayUnPay()
        {
            List<List<string>> payableCaseIdCollection = GetPayUnpayList();
            AgencyPayableSetDTO agencyPayableSet = (AgencyPayableSetDTO)ViewState["agencyPayableSet"];
            List<string> payList = payableCaseIdCollection[0];
            List<string> unpayList = payableCaseIdCollection[1];
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
                agencyPayableSet.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                if(payList.Count>0)
                    AgencyPayableBL.Instance.PayUnPayMarkCase(agencyPayableSet, payList, 1);
                if(unpayList.Count>0)
                    AgencyPayableBL.Instance.PayUnPayMarkCase(agencyPayableSet, unpayList, 2);
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
                List<string> payableCaseIdCollection = GetSelectedRow();
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
        protected List<List<string>> GetPayUnpayList()
        {
            bulErrorMessage.Items.Clear();
            List<string> payListCollection = new List<string>();
            List<string> unpayListCollection = new List<string>();
            string  payList = "";
            string unpayList = "";
            AgencyPayableSetDTO agencyPayableSet = (AgencyPayableSetDTO)ViewState["agencyPayableSet"];

            foreach (GridViewRow row in grvViewEditAgencyPayable.Rows)
            {
                CheckBox chkSelected = (CheckBox)row.FindControl("chkSelected");
                if (chkSelected != null)
                    if (chkSelected.Checked == true)
                    {
                        AgencyPayableCaseDTO selectedPayableCase = agencyPayableSet.PayableCases[row.DataItemIndex];
                        if (selectedPayableCase.NFMCDifferencePaidAmt != null)
                        {
                            if (unpayList.Length + selectedPayableCase.AgencyPayableId.ToString().Length < HPFConfigurationSettings.CASE_ID_COLLECTION_MAX_LENGTH)
                                unpayList += selectedPayableCase.AgencyPayableId.ToString() + ",";
                            else
                            {
                                unpayList = unpayList.Remove(unpayList.LastIndexOf(","), 1);
                                unpayListCollection.Add(unpayList);
                                unpayList = selectedPayableCase.AgencyPayableId.ToString()+",";
                            }
                        }
                        else
                        {
                            if (payList.Length + selectedPayableCase.AgencyPayableId.ToString().Length < HPFConfigurationSettings.CASE_ID_COLLECTION_MAX_LENGTH)
                                payList += selectedPayableCase.AgencyPayableId.ToString() + ",";
                            else
                            {
                                payList = payList.Remove(payList.LastIndexOf(","), 1);
                                payListCollection.Add(payList);
                                payList = selectedPayableCase.AgencyPayableId.ToString() + ",";
                            }
                        }
                        if (agencyPayableSet.PayableCases[row.DataItemIndex].NFMCDifferenceEligibleInd == "N")
                        {
                            hidPayUnpayCheck.Value = "-1";
                        }
                    }
            }
            
            if (payList.Length==0&&unpayList.Length==0)
                return null;
            if (payList.Length > 0)
            {
                payList = payList.Remove(payList.LastIndexOf(","), 1);
                payListCollection.Add(payList);
            }
            if (unpayList.Length > 0)
            {
                unpayList = unpayList.Remove(unpayList.LastIndexOf(","), 1);
                unpayListCollection.Add(unpayList);
            }
            List<List<string>> result = new List<List<string>>();
            result.Add(payListCollection);
            result.Add(unpayListCollection);
            return result;
        }
        protected List<string> GetSelectedRow()
        {
            bulErrorMessage.Items.Clear();
            List<string> resultCollection = new List<string>();
            string result = "";
            AgencyPayableSetDTO agencyPayableSet = (AgencyPayableSetDTO)ViewState["agencyPayableSet"];
            if (agencyPayableSet == null)
                return null;
            foreach (GridViewRow row in grvViewEditAgencyPayable.Rows)
            {
                CheckBox chkSelected = (CheckBox)row.FindControl("chkSelected");
                if (chkSelected != null)
                    if (chkSelected.Checked == true)
                    {
                        if (result.Length + agencyPayableSet.PayableCases[row.DataItemIndex].AgencyPayableId.ToString().Length < HPFConfigurationSettings.CASE_ID_COLLECTION_MAX_LENGTH)
                            result += agencyPayableSet.PayableCases[row.DataItemIndex].AgencyPayableId.ToString() + ",";
                        else
                        {
                            result = result.Remove(result.LastIndexOf(","), 1);
                            resultCollection.Add(result);
                            result = agencyPayableSet.PayableCases[row.DataItemIndex].AgencyPayableId.ToString() + ","; 
                        }
                    }
            }
            if (result != "")
            {
                result = result.Remove(result.LastIndexOf(","), 1);
                resultCollection.Add(result);
            }
            if (resultCollection.Count == 0)
                return null;
            return resultCollection;
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