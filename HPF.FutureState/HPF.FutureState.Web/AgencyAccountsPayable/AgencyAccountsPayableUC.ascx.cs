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

namespace HPF.FutureState.Web.AgencyAccountsPayable
{
    public partial class AgencyAccountsPayableUC : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //apply security
            ApplySecurity();

            // display grv in the first time
            if (!IsPostBack)
            {
                AgencyPayableSearchCriteriaDTO searchCriteria = new AgencyPayableSearchCriteriaDTO();
                BindAgencyDropDownList();
                if (Session["agencyPayableSearchCriteria"] != null)
                {
                    searchCriteria = (AgencyPayableSearchCriteriaDTO)Session["agencyPayableSearchCriteria"];
                    txtPeriodStart.Text = searchCriteria.PeriodStartDate.ToShortDateString();
                    txtPeriodEnd.Text = searchCriteria.PeriodEndDate.ToShortDateString();
                    ddlAgency.SelectedValue = searchCriteria.AgencyId.ToString();
                    BindGrvInvoiceList(searchCriteria.PeriodStartDate.ToShortDateString(), searchCriteria.PeriodEndDate.ToShortDateString());
                }
                else
                {
                    SetDefaultPeriodStartEnd();
                    //default display all agencyaccount within 6 month.
                    BindGrvInvoiceList(DateTime.Now.AddMonths(-6).ToShortDateString(), DateTime.Now.ToShortDateString());
                }
            }
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_AGENCY_ACCOUNT_PAYABLE))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR999");
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
            AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgency();
            ddlAgency.DataValueField = "AgencyID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = agencyCollection;
            ddlAgency.DataBind();
            ddlAgency.Items.RemoveAt(ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue("-1")));
            ddlAgency.Items.Insert(0, new ListItem("ALL", "-1"));
            ddlAgency.Items.FindByText("ALL").Selected = true;
        }
        /// <summary>
        /// Bind search data into gridview
        /// </summary>
        protected void BindGrvInvoiceList(string periodStart, string periodEnd)
        {
            AgencyPayableSearchCriteriaDTO searchCriteria = new AgencyPayableSearchCriteriaDTO();
            AgencyPayableDTOCollection agencycol = new AgencyPayableDTOCollection();
            try
            {
                //get search criteria to AgencyPayableSearchCriteriaDTO
                searchCriteria = GetSearchCriteria(periodStart, periodEnd);
                //get search data match that search collection
                agencycol = AgencyPayableBL.Instance.SearchAgencyPayable(searchCriteria);
                //
                ViewState["agencycol"] = agencycol;
                //bind search data to gridview
                grvInvoiceList.DataSource = agencycol;
                grvInvoiceList.DataBind();
                if (agencycol.Count == 0)
                {
                    grvInvoiceList.SelectedIndex = -1;
                }
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
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                throw ex;
            }
        }
        /// <summary>
        /// get default period start:1st\prior month\year
        /// get default period end: lastday\prior month\year
        /// </summary>
        protected void SetDefaultPeriodStartEnd()
        {
            txtPeriodStart.Text = DateTime.Now.AddMonths(-6).ToShortDateString();
            txtPeriodEnd.Text = DateTime.Now.ToShortDateString();
        }
        private ExceptionMessage GetExceptionMessage(string exCode)
        {
            ExceptionMessage exMess = new ExceptionMessage();
            exMess.ErrorCode = exCode;
            exMess.Message = ErrorMessages.GetExceptionMessageCombined(exCode);
            return exMess;
        }
        private AgencyPayableSearchCriteriaDTO GetSearchCriteria(string periodStart, string periodEnd)
        {
            AgencyPayableSearchCriteriaDTO searchCriteria = new AgencyPayableSearchCriteriaDTO();
            DataValidationException ex = new DataValidationException();
            searchCriteria.AgencyId = int.Parse(ddlAgency.SelectedValue);

            try
            {
                searchCriteria.PeriodStartDate = DateTime.Parse(periodStart);
            }
            catch
            {
                ExceptionMessage exMessage = GetExceptionMessage(ErrorMessages.ERR0580);//error code
                ex.ExceptionMessages.Add(exMessage);
            }
            try
            {
                searchCriteria.PeriodEndDate = DateTime.Parse(periodEnd);
            }
            catch
            {
                ExceptionMessage exMessage = GetExceptionMessage(ErrorMessages.ERR0581);
                ex.ExceptionMessages.Add(exMessage);
            }

            if ((searchCriteria.PeriodStartDate.CompareTo(Convert.ToDateTime("1/1/1753")) < 0) || (searchCriteria.PeriodStartDate.CompareTo(Convert.ToDateTime("12/31/9999")) > 0))
            {
                ExceptionMessage exMessage = GetExceptionMessage(ErrorMessages.ERR0580);
                ex.ExceptionMessages.Add(exMessage);
            }
            if ((searchCriteria.PeriodEndDate.CompareTo(Convert.ToDateTime("1/1/1753")) < 0) || (searchCriteria.PeriodEndDate.CompareTo(Convert.ToDateTime("12/31/9999")) > 0))
            {
                ExceptionMessage exMessage = GetExceptionMessage(ErrorMessages.ERR0581);
                ex.ExceptionMessages.Add(exMessage);
            }
            if (ex.ExceptionMessages.Count > 0)
                throw ex;
            Session["agencyPayableSearchCriteria"] = searchCriteria;
            return searchCriteria;
        }
        protected void btnRefreshList_Click(object sender, EventArgs e)
        {

            bulMessage.Items.Clear();
            try
            {

                BindGrvInvoiceList(txtPeriodStart.Text, txtPeriodEnd.Text);
                grvInvoiceList.SelectedIndex = -1;
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
        /// <summary>
        /// go to New payable criteria.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewPayable_Click(object sender, EventArgs e)
        {
            string query = "?agency=" + ddlAgency.SelectedValue;
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
            AgencyPayableDTOCollection agency = new AgencyPayableDTOCollection();
            try
            {
                if (grvInvoiceList.SelectedIndex != -1)
                {
                    agency = (AgencyPayableDTOCollection)ViewState["agencycol"];
                    //
                    int selectedrow = grvInvoiceList.SelectedIndex;
                    RefCodeItemDTO agencystatus = LookupDataBL.Instance.GetRefCode("agency payable status code")[1];
                    agency[selectedrow].StatusCode = agencystatus.Code;
                    agency[selectedrow].SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                    AgencyPayableBL.Instance.CancelAgencyPayable(agency[selectedrow]);
                    //
                    grvInvoiceList.DataSource = agency;
                    grvInvoiceList.DataBind();
                }
                else
                {
                    bulMessage.Items.Add(new ListItem("Please choose record."));
                }
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        protected void grvInvoiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCancelPayable.Attributes.Add("onclick", "return confirm('Do you really want to cancel payable?')");
        }

        protected void btnViewPayable_Click(object sender, EventArgs e)
        {

            try
            {
                AgencyPayableDTOCollection agencyPayableCol = (AgencyPayableDTOCollection)ViewState["agencycol"];
                AgencyPayableDTO agencyPayable = agencyPayableCol[grvInvoiceList.SelectedIndex];
                if (agencyPayable != null)
                {
                    Session["agencyPayable"] = agencyPayable;
                    Response.Redirect("AgencyPayableInfo.aspx");
                }
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(new ListItem("Please choose record."));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
    }
}