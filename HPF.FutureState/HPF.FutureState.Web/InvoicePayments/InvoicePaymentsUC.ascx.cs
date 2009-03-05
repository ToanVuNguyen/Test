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
            if(grvInvoicePaymentList.SelectedValue ==null)
                SelectedRowIndex.Value = "";
            try
            {
                if (!IsPostBack)
                {
                    BindFundingSourceDropDownList();
                    SetDefaultPeriodStartEnd();
                    DefaultSearch();
                    btnViewEditPayable.Attributes.Add("onclick", " return ViewEditClientClick();");
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionMessage exMes = GetExceptionMessageWithoutCode(ErrorMessages.ERR0996);
            }

        }

        private void DefaultSearch()
        {
            InvoiceSearchCriteriaDTO searchCriteria = new InvoiceSearchCriteriaDTO();
            searchCriteria.FundingSourceId = -1;
            searchCriteria.PeriodStart = DateTime.Today.AddMonths(-6);
            searchCriteria.PeriodEnd = DateTime.Today;
            InvoicePaymentDTOCollection invoicePayment = InvoicePaymentSearch(searchCriteria);
            grvInvoicePaymentList.DataSource = invoicePayment;
            grvInvoicePaymentList.DataBind();
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
        /// <summary>
        /// Bind search data into gridview
        /// </summary>
        protected void BindGrvInvoicePaymentList()
        {
            try
            {
                InvoicePaymentDTOCollection invoicePayment = GetInvoicePaymentInfo();
                //bind search data to gridview
                grvInvoicePaymentList.DataSource = invoicePayment;
                ViewState["invoicePayment"] = invoicePayment;
                grvInvoicePaymentList.DataBind();
                
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.DisplayName);
            }
        }

        private ExceptionMessage GetExceptionMessage(string errorCode)
        {
            var exMes = new ExceptionMessage();
            exMes.ErrorCode = errorCode;
            exMes.Message = ErrorMessages.GetExceptionMessageCombined(errorCode);
            return exMes;
        }
        private ExceptionMessage GetExceptionMessageWithoutCode(string errorCode)
        {
            var exMes = new ExceptionMessage();
            exMes.ErrorCode = errorCode;
            exMes.Message = ErrorMessages.GetExceptionMessage(errorCode);
            return exMes;
        }
        /// <summary>
        /// Get or throw exception if there's any datavalidation error.
        /// </summary>
        /// <returns></returns>
        private InvoiceSearchCriteriaDTO GetInvoicePaymentSearchCriterial()
        {
            DataValidationException ex = new DataValidationException();
            InvoiceSearchCriteriaDTO searchCriteria = new InvoiceSearchCriteriaDTO();
            searchCriteria.FundingSourceId = int.Parse(ddlFundingSource.SelectedValue);
            if (txtPeriodStart.Text == string.Empty)
            {
                ExceptionMessage exMes = GetExceptionMessage(ErrorMessages.ERR0675);
                ex.ExceptionMessages.Add(exMes);
            }
            else
            {
                try
                {
                    searchCriteria.PeriodStart = DateTime.Parse(txtPeriodStart.Text);
                    if (searchCriteria.PeriodStart.Year < 1753)
                        throw (new Exception());
                }
                catch
                {
                    ExceptionMessage exMes = GetExceptionMessageWithoutCode(ErrorMessages.ERR0996);
                    ex.ExceptionMessages.Add(exMes);
                }
            }
            if (txtPeriodEnd.Text == string.Empty)
            {
                ExceptionMessage exMes = GetExceptionMessage(ErrorMessages.ERR0676);
                ex.ExceptionMessages.Add(exMes);
            }
            else
            {
                try
                {
                    searchCriteria.PeriodEnd = DateTime.Parse(txtPeriodEnd.Text);
                    if (searchCriteria.PeriodEnd.Year < 1753)
                        throw (new Exception());
                }
                catch
                {
                    ExceptionMessage exMes = GetExceptionMessageWithoutCode(ErrorMessages.ERR0997);
                    ex.ExceptionMessages.Add(exMes);
                }
            }
            if (ex.ExceptionMessages.Count > 0)
                throw (ex);
            return searchCriteria;

        }
        /// <summary>
        /// Get the searchCriteria and then perform a search
        /// </summary>
        /// <returns></returns>
        protected InvoicePaymentDTOCollection GetInvoicePaymentInfo()
        {
            try
            {
                InvoiceSearchCriteriaDTO searchCriteria = GetInvoicePaymentSearchCriterial();
                InvoicePaymentDTOCollection invoicePayment = InvoicePaymentSearch(searchCriteria);
                return invoicePayment;
            }
            catch (DataValidationException ex)
            {
                foreach (var mes in ex.ExceptionMessages)
                    lblErrorMessage.Items.Add(new ListItem(mes.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }
            return null;
            
        }

        private static InvoicePaymentDTOCollection InvoicePaymentSearch(InvoiceSearchCriteriaDTO searchCriteria)
        {
            InvoicePaymentDTOCollection invoicePayment = new InvoicePaymentDTOCollection();
            //get search criteria to AgencyPayableSearchCriteriaDTO
            invoicePayment = InvoicePaymentBL.Instance.InvoicePaymentSearch(searchCriteria);
            if (invoicePayment == null)
            {
                Exception ex = new Exception(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0684));
                throw (ex);
            }
            return invoicePayment;
        }
        /// <summary>
        /// get default period start:1st\prior month\year
        /// get default period end: lastday\prior month\year
        /// </summary>
        protected void SetDefaultPeriodStartEnd()
        {
            txtPeriodStart.Text = DateTime.Today.AddMonths(-6).ToShortDateString() ;
            txtPeriodEnd.Text = DateTime.Today.ToShortDateString();
        }

        private void ClearErrorMessages()
        {
            lblErrorMessage.Items.Clear();
        }
        protected void btnRefreshList_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            BindGrvInvoicePaymentList();
        }
       

        protected void btnViewPayable_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            if (grvInvoicePaymentList.SelectedValue!=null)
                Response.Redirect("InvoicePaymentInfo.aspx?id=" + grvInvoicePaymentList.SelectedValue.ToString());

        }

        protected void btnNewPayable_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoicePaymentInfo.aspx");
        }

        protected void grvInvoicePaymentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grvInvoicePaymentList.SelectedValue != null)
                SelectedRowIndex.Value = grvInvoicePaymentList.SelectedValue.ToString();
        }
    }
}