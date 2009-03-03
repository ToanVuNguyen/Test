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
                BindNeverBillReasonDropDownList();
                BindNeverPayReasonDropDownList();
                int fc_id = int.Parse(Request.QueryString["CaseID"].ToString());
                BindAccounting(fc_id);
            }
   
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                btnSave.Enabled = false;
            }
        }
        protected void BindNeverBillReasonDropDownList()
        {
            RefCodeItemDTOCollection refCodeCol = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_NEVER_BILL_REASON_CODE);
            ddlNerverBillReason.DataSource = refCodeCol;
            ddlNerverBillReason.DataTextField = "CodeDesc";
            ddlNerverBillReason.DataValueField = "Code";
            ddlNerverBillReason.DataBind();
        }
        protected void BindNeverPayReasonDropDownList()
        {
            RefCodeItemDTOCollection refCodeCol = LookupDataBL.Instance.GetRefCode(Constant.REF_CODE_SET_NEVER_PAY_REASON_CODE);
            ddlNeverPayReason.DataSource = refCodeCol;
            ddlNeverPayReason.DataTextField = "CodeDesc";
            ddlNeverPayReason.DataValueField = "Code";
            ddlNeverPayReason.DataBind();
        }
        protected void BindAccounting(int fc_id)
        {
            try
            {
                AccountingDTO accountinginfo = AccountingBL.Instance.GetAccountingDetailInfo(fc_id);
                ddlNeverPayReason.SelectedValue = accountinginfo.NerverPayReason;
                ddlNerverBillReason.SelectedValue = accountinginfo.NeverBillReason;
                grvBillingInfo.DataSource = accountinginfo.BillingInfo;
                grvBillingInfo.DataBind();
                grvPaymentInfo.DataSource = accountinginfo.AgencyPayableCase;
                grvPaymentInfo.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                lblMessage.Text = ex.Message;
                throw;
            }
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateForclosureCase();
        }
        protected void UpdateForclosureCase()
        {
            try
            {
                ForeclosureCaseDTO foreclosureCase = new ForeclosureCaseDTO();
                foreclosureCase.FcId= int.Parse(Request.QueryString["CaseID"]);
                foreclosureCase.NeverBillReasonCd = ddlNerverBillReason.SelectedValue;
                foreclosureCase.NeverPayReasonCd = ddlNeverPayReason.SelectedValue;
                foreclosureCase.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                AccountingBL.Instance.UpdateForeclosureCase(foreclosureCase);
                lblMessage.Text = "Update Forclosurecase successfully";
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                throw;
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
    }
}