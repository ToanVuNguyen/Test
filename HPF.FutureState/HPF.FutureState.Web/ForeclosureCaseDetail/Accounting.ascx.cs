﻿using System;
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
            RefCodeItemDTOCollection refCodeCol = LookupDataBL.Instance.GetRefCode("never bill reason code");
            ddlNerverBillReason.DataSource = refCodeCol;
            ddlNerverBillReason.DataTextField = "CodeDesc";
            ddlNerverBillReason.DataValueField = "Code";
            ddlNerverBillReason.DataBind();
        }
        protected void BindNeverPayReasonDropDownList()
        {
            RefCodeItemDTOCollection refCodeCol = LookupDataBL.Instance.GetRefCode("never pay reason code");
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
                lblMessage.Text = "Update Forclosurecase successfull";
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                throw;
            }
            
        }
    }
}