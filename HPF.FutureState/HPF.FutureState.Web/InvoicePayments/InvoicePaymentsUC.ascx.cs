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
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Web.Security;

namespace HPF.FutureState.Web.InvoicePayments
{
    public partial class InvoicePaymentsUC : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearErrorMessages();
            ApplySecurity();
            try
            {
                if (!IsPostBack)
                {
                    BindFundingSourceDropDownList();
                    DefaultSearch();
                }
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.DataSource = ex.ExceptionMessages;
                lblErrorMessage.DataBind();
                BindNullData();
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                BindNullData();
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }

        }

        private void DefaultSearch()
        {
            InvoiceSearchCriteriaDTO searchCriteria = new InvoiceSearchCriteriaDTO();
            searchCriteria.FundingSourceId = -1;
            searchCriteria.PeriodStart = DateTime.Today.AddMonths(-6);
            searchCriteria.PeriodEnd = DateTime.Today;
            txtPeriodStart.Text = searchCriteria.PeriodStart.ToShortDateString();
            txtPeriodEnd.Text = searchCriteria.PeriodEnd.ToShortDateString();
            InvoicePaymentSearch(searchCriteria);
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_INVOICE_PAYMENT))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_APP_INVOICE_PAYMENT))
            {
                btnNewPayable.Enabled = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>

        protected void BindFundingSourceDropDownList()
        {
            FundingSourceDTOCollection fundingSourceCol = LookupDataBL.Instance.GetFundingSource();
            ddlFundingSource.DataSource = fundingSourceCol;
            ddlFundingSource.DataTextField = "FundingSourceName";
            ddlFundingSource.DataValueField = "FundingSourceID";
            ddlFundingSource.DataBind();
        }
        private void BindNullData()
        {
            grvInvoicePaymentList.DataSource = null;
            grvInvoicePaymentList.DataBind();
        }
        private InvoiceSearchCriteriaDTO GetInvoiceSearchCriterial()
        {
            InvoiceSearchCriteriaDTO searchCriteria = new InvoiceSearchCriteriaDTO();
            searchCriteria.FundingSourceId = ConvertToInt(ddlFundingSource.SelectedValue);
            searchCriteria.PeriodStart = ConvertToDateTime(txtPeriodStart.Text);
            if (searchCriteria.PeriodStart != DateTime.MinValue)
                searchCriteria.PeriodStart = SetToStartDay(searchCriteria.PeriodStart);
            searchCriteria.PeriodEnd = ConvertToDateTime(txtPeriodEnd.Text);
            if (searchCriteria.PeriodEnd != DateTime.MinValue)
                searchCriteria.PeriodEnd = SetToEndDay(searchCriteria.PeriodEnd);
            return searchCriteria;

        }
        
        private void InvoicePaymentSearch(InvoiceSearchCriteriaDTO searchCriteria)
        {
            InvoicePaymentDTOCollection searchResult = InvoicePaymentBL.Instance.InvoicePaymentSearch(searchCriteria);
            ViewState["invoicePayment"] = searchResult;
            grvInvoicePaymentList.DataSource = searchResult;
            grvInvoicePaymentList.DataBind();
            if (searchResult == null)
            {
                lblErrorMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0684));
            }
        }
        DateTime SetToStartDay(DateTime t)
        {
            t = t.AddHours(-t.Hour);
            t = t.AddMinutes(-t.Minute);
            t = t.AddSeconds(-t.Second);
            t = t.AddMilliseconds(-t.Millisecond);
            return t;
        }
        DateTime SetToEndDay(DateTime t)
        {
            t = SetToStartDay(t);
            t = t.AddDays(1);
            t = t.AddSeconds(-1);
            return t;
        }
        
        private void ClearErrorMessages()
        {
            lblErrorMessage.Items.Clear();
        }
        protected void btnRefreshList_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            InvoiceSearchCriteriaDTO searchCriteria = null;
            try
            {
                searchCriteria = GetInvoiceSearchCriterial();
                InvoicePaymentSearch(searchCriteria);
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.DataSource = ex.ExceptionMessages;
                lblErrorMessage.DataBind();
                BindNullData();
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
                BindNullData();
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        protected void btnViewPayable_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            if (grvInvoicePaymentList.SelectedValue == null)
            {
                lblErrorMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0683));
                return;
            }
            else
                Response.Redirect("InvoicePaymentInfo.aspx?id=" + grvInvoicePaymentList.SelectedValue.ToString());

        }

        protected void btnNewPayable_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoicePaymentInfo.aspx");
        }

        protected void grvInvoicePaymentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearErrorMessages();
            if (grvInvoicePaymentList.SelectedValue != null)
                SelectedRowIndex.Value = grvInvoicePaymentList.SelectedValue.ToString();
        }
        private DateTime ConvertToDateTime(object obj)
        {
            DateTime dt;
            if (DateTime.TryParse(obj.ToString().Trim(), out dt))
                return dt;
            return DateTime.MinValue;
        }
        private int ConvertToInt(object obj)
        {
            int value;
            if (int.TryParse(obj.ToString().Trim(), out value))
                return value;
            return int.MinValue;
        }
    }
}