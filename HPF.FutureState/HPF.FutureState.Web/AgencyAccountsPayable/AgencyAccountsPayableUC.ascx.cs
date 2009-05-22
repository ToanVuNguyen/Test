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
using HPF.FutureState.Common.Utils.DataValidator;
using System.Globalization;
namespace HPF.FutureState.Web.AgencyAccountsPayable
{
    public partial class AgencyAccountsPayableUC : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //apply security
            ApplySecurity();
            lblPortal.NavigateUrl = HPFConfigurationSettings.HPF_PAYABLE_PORTAL_URL;
            // display grv in the first time
            if (!IsPostBack)
            {
                if (grvInvoiceList.SelectedIndex == -1)
                    hidSelectedRowIndex.Value = "";
                btnCancelPayable.Attributes.Add("onclick", "return CancelClientClick();");
                BindAgencyDropDownList();
                SetUpDefaultValue();
            }
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_AGENCY_ACCOUNT_PAYABLE))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_AGENCY_ACCOUNT_PAYABLE))
            {
                btnCancelPayable.Enabled = false;
                btnNewPayable.Enabled = false;
            }
        }
        /// <summary>
        ///Display data in dropdownlist 
        /// </summary>
        protected void BindAgencyDropDownList()
        {
            try
            {
                AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgencies();
                ddlAgency.DataValueField = "AgencyID";
                ddlAgency.DataTextField = "AgencyName";
                ddlAgency.DataSource = agencyCollection;
                ddlAgency.DataBind();
                ddlAgency.Items.RemoveAt(ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue("-1")));
                ddlAgency.Items.Insert(0, new ListItem("ALL", "-1"));
                ddlAgency.Items.FindByText("ALL").Selected = true;
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        /// <summary>
        /// Bind search data into gridview
        /// </summary>
        protected void PayableSearch(AgencyPayableSearchCriteriaDTO searchCriteria)
        {
            bulMessage.Items.Clear();
            AgencyPayableDTOCollection searchResult = AgencyPayableBL.Instance.SearchAgencyPayable(searchCriteria); 
            Session["PayableSearchResult"] = searchResult;
            Session["PayableSearchCriteria"] = searchCriteria;
            grvInvoiceList.DataSource = searchResult;
            grvInvoiceList.DataBind();
            if (searchResult.Count == 0)
                bulMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.WARN0583));
        }
        /// <summary>
        /// get default period start:1st\prior month\year
        /// get default period end: lastday\prior month\year
        /// </summary>
        protected void SetUpDefaultValue()
        {
            AgencyPayableSearchCriteriaDTO defaultSearchCriteria = new AgencyPayableSearchCriteriaDTO();
            defaultSearchCriteria.AgencyId = -1;
            defaultSearchCriteria.PeriodStartDate = DateTime.Today.AddMonths(-6);
            defaultSearchCriteria.PeriodEndDate = DateTime.Today.AddDays(1).AddSeconds(-1);
            txtPeriodStart.Text = defaultSearchCriteria.PeriodStartDate.ToShortDateString();
            txtPeriodEnd.Text = defaultSearchCriteria.PeriodEndDate.ToShortDateString();
            PayableSearch(defaultSearchCriteria);
        }
        private ExceptionMessage GetExceptionMessage(string exCode)
        {
            ExceptionMessage exMess = new ExceptionMessage();
            exMess.ErrorCode = exCode;
            exMess.Message = ErrorMessages.GetExceptionMessageCombined(exCode);
            return exMess;
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
        private AgencyPayableSearchCriteriaDTO GetSearchCriteria()
        {
            AgencyPayableSearchCriteriaDTO searchCriteria = new AgencyPayableSearchCriteriaDTO();
            searchCriteria.AgencyId = ConvertToInt(ddlAgency.SelectedValue);
            searchCriteria.PeriodStartDate = ConvertToDateTime(txtPeriodStart.Text);
            searchCriteria.PeriodEndDate = ConvertToDateTime(txtPeriodEnd.Text);
            if (searchCriteria.PeriodEndDate != DateTime.MinValue)
                searchCriteria.PeriodEndDate = searchCriteria.PeriodEndDate.AddDays(1).AddSeconds(-1);
            return searchCriteria;
        }
        protected void btnRefreshList_Click(object sender, EventArgs e)
        {

            bulMessage.Items.Clear();
            try
            {
                AgencyPayableSearchCriteriaDTO searchCriterial = GetSearchCriteria();
                PayableSearch(searchCriterial);
            }
            catch (DataValidationException ex)
            {
                bulMessage.DataSource = ex.ExceptionMessages;
                bulMessage.DataBind();
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                grvInvoiceList.DataSource = null;
                grvInvoiceList.DataBind();
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                grvInvoiceList.DataSource = null;
                grvInvoiceList.DataBind();
            }
        }
        /// <summary>
        /// go to New payable criteria.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewPayable_Click(object sender, EventArgs e)
        {
            bulMessage.Items.Clear();
            string query = "?agency=" + ddlAgency.SelectedValue;
            Session["Comment"] = null;
            Response.Redirect("CreateNewPayable.aspx" + query);
        }

        /// <summary>
        /// update statuscode, payment comment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelPayable_Click(object sender, EventArgs e)
        {
            //clear the error message
            bulMessage.Items.Clear();
            //
            CancelPayable();
        }
        private void CancelPayable()
        {
            AgencyPayableDTOCollection agency = new AgencyPayableDTOCollection();
            try
            {
                if (grvInvoiceList.SelectedIndex != -1)
                {
                    agency = (AgencyPayableDTOCollection)Session["PayableSearchResult"];
                    //
                    int selectedrow = grvInvoiceList.SelectedIndex;
                    RefCodeItemDTO agencystatus = LookupDataBL.Instance.GetRefCodes(Constant.REF_CODE_SET_AGENCY_PAYABLE_STATUS_CODE)[1];
                    agency[selectedrow].StatusCode = agencystatus.CodeValue;
                    agency[selectedrow].SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                    AgencyPayableBL.Instance.CancelAgencyPayable(agency[selectedrow]);
                    //
                    AgencyPayableSearchCriteriaDTO searchCriterial = Session["PayableSearchCriteria"] as AgencyPayableSearchCriteriaDTO;
                    PayableSearch(searchCriterial);
                }
                else
                {
                    bulMessage.Items.Add(ErrorMessages.GetExceptionMessageCombined("ERR0584"));//(new ListItem("ERR0584--An agency account payable must be selected in order to cancel it."));
                }
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        protected void btnViewPayable_Click(object sender, EventArgs e)
        {
            try
            {
                bulMessage.Items.Clear();
                ViewPayable();
            }
            catch (DataValidationException ex)
            {
                foreach (var mes in ex.ExceptionMessages)
                    bulMessage.Items.Add(new ListItem(mes.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                return;
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                return;
            }
        }

        private void ViewPayable()
        {
            DataValidationException ex = new DataValidationException();
            try
            {
                AgencyPayableDTOCollection agencyPayableCol = (AgencyPayableDTOCollection)Session["PayableSearchResult"];
                AgencyPayableDTO agencyPayable = agencyPayableCol[grvInvoiceList.SelectedIndex];
                if (agencyPayable != null)
                {
                    Session["agencyPayable"] = agencyPayable;
                    Response.Redirect("AgencyPayableInfo.aspx");
                }
                else throw ex;
            }
            catch
            {
                ExceptionMessage exMessage = GetExceptionMessage(ErrorMessages.ERR0585);
                ex.ExceptionMessages.Add(exMessage);
                throw ex;
            }
        }
        protected void grvInvoiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grvInvoiceList.SelectedIndex != -1)
                hidSelectedRowIndex.Value = grvInvoiceList.SelectedValue.ToString();

        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            CancelPayable();
        }
    }
}