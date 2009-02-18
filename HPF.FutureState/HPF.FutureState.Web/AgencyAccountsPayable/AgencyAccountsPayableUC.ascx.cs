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
            ApplySecurity();
           
            if (IsPostBack)
            {
               
                if (grvInvoiceList.Rows.Count == 0)
                    grvInvoiceList.SelectedIndex = -1;
                if (grvInvoiceList.SelectedIndex != -1)
                {
                    btnCancelPayable.Attributes.Clear();
                    btnCancelPayable.Attributes.Add("onclick", "return confirm('Do you really want to cancel payable?')");
                }
                else
                {
                    btnCancelPayable.Attributes.Clear();
                    btnCancelPayable.Attributes.Add("onclick", "alert('You have to choose a row in gridview!')");
                }
            }
            //for (int i = 0; i < grvInvoiceList.Rows.Count; i++)
            //{
            //    grvInvoiceList.Rows[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grvInvoiceList, "Select$" + i));
            //}
            if (!IsPostBack)
            {
                BindAgencyDropDownList();
                SetDefaultPeriodStartEnd();
                BindGrvInvoiceList(DateTime.Now.AddMonths(-6).ToShortDateString(), DateTime.Now.ToShortDateString());
               
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
        /// 
        /// </summary>

        protected void BindAgencyDropDownList()
        {
            AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgency();
            ddlAgency.DataSource = agencyCollection;
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataValueField = "AgencyID";
            ddlAgency.DataBind();
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
                searchCriteria=GetSearchCriteria(periodStart,periodEnd);
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
                    btnCancelPayable.Attributes.Add("onclick", "alert('You have to choose a row in gridview!')");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }
        }
        /// <summary>
        /// get default period start:1st\prior month\year
        /// get default period end: lastday\prior month\year
        /// </summary>
        protected void SetDefaultPeriodStartEnd()
        {
            DateTime today = DateTime.Today;
            int priormonth = today.AddMonths(-1).Month;
            int year = today.AddMonths(-1).Year;
            txtPeriodStart.Text = priormonth + "/" + 1 + "/" + year;
            int daysinmonth = DateTime.DaysInMonth(year, priormonth);
            txtPeriodEnd.Text = priormonth + "/" + daysinmonth + "/" + year;
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
                ExceptionMessage exMessage = GetExceptionMessage(ErrorMessages.ERR0001);//error code
                ex.ExceptionMessages.Add(exMessage);
            }
            try
            {
                searchCriteria.PeriodEndDate = DateTime.Parse(periodEnd);
            }
            catch
            {
                ExceptionMessage exMessage = GetExceptionMessage(ErrorMessages.ERR0001);
                ex.ExceptionMessages.Add(exMessage);
            }
            return searchCriteria;
        }
        protected void btnRefreshList_Click(object sender, EventArgs e)
        {

            try
            {
                BindGrvInvoiceList(txtPeriodStart.Text, txtPeriodEnd.Text);
                grvInvoiceList.SelectedIndex = -1;
            }
            catch (DataValidationException ex)
            {
                foreach (var mes in ex.ExceptionMessages)
                    bulMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                return;
            }
            catch (Exception ex)
            {
                bulMessage.Items.Add(new ListItem(ex.Message));
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
            //AgencyPayableSearchCriteriaDTO searchCriteria = new AgencyPayableSearchCriteriaDTO();
            AgencyPayableDTOCollection agency = new AgencyPayableDTOCollection();
            try
            {
                if (grvInvoiceList.SelectedIndex != -1)
                {
                    //
                    btnCancelPayable.Attributes.Clear();
                    btnCancelPayable.Attributes.Add("onclick", "return confirm('Do you really want to cancel payable?')");
                    //
                    //searchCriteria.AgencyId = int.Parse(ddlAgency.SelectedValue);
                    //searchCriteria.PeriodEndDate = DateTime.Parse(txtPeriodEnd.Text);
                    //searchCriteria.PeriodStartDate = DateTime.Parse(txtPeriodStart.Text);
                    //agency = AgencyPayableBL.Instance.SearchAgencyPayable(searchCriteria);
                    agency = (AgencyPayableDTOCollection)ViewState["agencycol"];
                    //
                    int selectedrow = grvInvoiceList.SelectedIndex;
                    agency[selectedrow].StatusCode = "Cancel";
                    agency[selectedrow].PaymentComment = "Agency didn't complete";
                    AgencyPayableBL.Instance.CancelAgencyPayable(agency[selectedrow]);
                    //
                    grvInvoiceList.DataSource = agency;
                    grvInvoiceList.DataBind();
                }
                else
                {
                    btnCancelPayable.Attributes.Clear();
                    btnCancelPayable.Attributes.Add("onclick", "alert('You have to choose a row in gridview!')");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        protected void grvInvoiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCancelPayable.Attributes.Clear();
            btnCancelPayable.Attributes.Add("onclick", "return confirm('Do you really want to cancel payable?')");
        }

        protected void btnViewPayable_Click(object sender, EventArgs e)
        {
            AgencyPayableDTOCollection agencyPayableCol=(AgencyPayableDTOCollection)ViewState["agencycol"];
            AgencyPayableDTO agencyPayable = agencyPayableCol[grvInvoiceList.SelectedIndex];
            Session["agencyPayable"] = agencyPayable;
            Response.Redirect("AgencyPayableInfo.aspx");
        }
    }
}