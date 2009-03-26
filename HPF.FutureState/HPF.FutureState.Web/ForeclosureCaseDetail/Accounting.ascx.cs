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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common;

namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class Accounting : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplySecurity();
            if (IsPostBack)
            {
                if (Request.QueryString["CaseID"] == null)
                    return;
                BindNeverBillReasonDropDownList();
                BindNeverPayReasonDropDownList();
                int fc_id = int.Parse(Request.QueryString["CaseID"].ToString());
                ViewState["CaseID"] = fc_id;
                BindAccounting(fc_id);
            }
   
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                btnSave.Enabled = false;
                ddlNerverBillReason.Enabled = false;
                ddlNeverPayReason.Enabled = false;
            }
        }
        protected void BindNeverBillReasonDropDownList()
        {
            RefCodeItemDTOCollection refCodeCol = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_NEVER_BILL_REASON_CODE);
            ddlNerverBillReason.DataSource = refCodeCol;
            ddlNerverBillReason.DataTextField = "CodeDesc";
            ddlNerverBillReason.DataValueField = "Code";
            ddlNerverBillReason.DataBind();
            ddlNerverBillReason.Items.Insert(0, string.Empty);
        }
        protected void BindNeverPayReasonDropDownList()
        {
            RefCodeItemDTOCollection refCodeCol = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_NEVER_PAY_REASON_CODE);
            ddlNeverPayReason.DataSource = refCodeCol;
            ddlNeverPayReason.DataTextField = "CodeDesc";
            ddlNeverPayReason.DataValueField = "Code";
            ddlNeverPayReason.DataBind();
            ddlNeverPayReason.Items.Insert(0, new ListItem(string.Empty, null));
        }
        protected void BindAccounting(int fc_id)
        {
            try
            {
                AccountingDTO accountinginfo = AccountingBL.Instance.GetAccountingDetailInfo(fc_id);
                ddlNeverPayReason.SelectedValue = accountinginfo.NerverPayReason;
                ddlNerverBillReason.SelectedValue = accountinginfo.NeverBillReason;
                if (accountinginfo.BillingInfo.Count == 0)
                    panBillingInfo.Height= Unit.Parse("20px");
                else panBillingInfo.Height = Unit.Parse("250px");

                if (accountinginfo.AgencyPayableCase.Count == 0)
                    panPaymentInfo.Height = Unit.Parse("20px");
                else panPaymentInfo.Height = Unit.Parse("250px");
                grvBillingInfo.DataSource = accountinginfo.BillingInfo;
                grvBillingInfo.DataBind();
                grvPaymentInfo.DataSource = accountinginfo.AgencyPayableCase;
                grvPaymentInfo.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                bullblErrorMessage.Items.Add(ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateForclosureCase();
        }
        protected bool UpdateForclosureCase()
        {
            try
            {
                hidSaveIsYes.Value = "";
                ForeclosureCaseDTO foreclosureCase = new ForeclosureCaseDTO();
                foreclosureCase.FcId= int.Parse(ViewState["CaseID"].ToString());
                foreclosureCase.NeverBillReasonCd = ddlNerverBillReason.SelectedValue;
                foreclosureCase.NeverPayReasonCd = ddlNeverPayReason.SelectedValue;
                foreclosureCase.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                AccountingBL.Instance.UpdateForeclosureCase(foreclosureCase);
                bullblErrorMessage.Items.Add("Update Forclosurecase successfully");
                return true;
            }
            catch (Exception ex)
            {
                bullblErrorMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                return false;
            }
        }
        protected void grvBillingInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRejectReasonDesc = e.Row.FindControl("lblPaymentRejectReasonDesc") as Label;
                RefCodeItemDTOCollection PaymentRejectReasonDTOCol = LookupDataBL.Instance.GetRefCode("payment reject reason code");
                foreach (var PaymentRejectReasonDTO in PaymentRejectReasonDTOCol)
                {
                    if (lblRejectReasonDesc.Text == PaymentRejectReasonDTO.Code)
                        lblRejectReasonDesc.Text = PaymentRejectReasonDTO.CodeDesc;
                }
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            if (hidSaveIsYes.Value != string.Empty)
                UpdateForclosureCase();
        }

        public string msgWARN0450
        {
            get
            {
                return ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0450);
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            if (!UpdateForclosureCase())
                selTabCtrl.Value = string.Empty;
        }
    }
}