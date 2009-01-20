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

namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class Accounting : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindNeverBillReasonDropDownList();
                BindNeverPayReasonDropDownList();
                int fc_id = int.Parse(Request.QueryString["CaseID"].ToString());
                BindAccounting(fc_id);
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
            AccountingDTO accountinginfo = AccountingBL.Instance.GetAccountingDetailInfo(fc_id);
            ddlNeverPayReason.SelectedValue = accountinginfo.NerverPayReason;
            ddlNerverBillReason.SelectedValue = accountinginfo.NeverBillReason;
            grvBillingInfo.DataSource = accountinginfo.BillingInfo;
            grvBillingInfo.DataBind();
            grvPaymentInfo.DataSource = accountinginfo.AgencyPayableCase;
            grvPaymentInfo.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateForclosureCase();
        }
        protected void UpdateForclosureCase()
        {
            try
            {
                int fc_id = int.Parse(Request.QueryString["CaseID"]);
                string neverbillreason = ddlNerverBillReason.SelectedValue;
                string neverpayreason = ddlNeverPayReason.SelectedValue;
                AccountingBL.Instance.UpdateForclosureCase(neverbillreason, neverpayreason, fc_id);
                
                lblMessage.Text = "Update Forclosurecase successfull";
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}