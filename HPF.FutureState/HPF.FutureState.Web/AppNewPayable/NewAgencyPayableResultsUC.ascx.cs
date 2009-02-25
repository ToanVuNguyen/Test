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
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;
using System.Text;
namespace HPF.FutureState.Web.AppNewPayable
{
    public partial class NewAgencyPayableResultsUC : System.Web.UI.UserControl
    {
        //store AgencyPayableDraftDTO
        AgencyPayableDraftDTO agencyPayableDraft
        {
            get
            {
                return (AgencyPayableDraftDTO)ViewState["agencyPayableDraft"];
            }
            set
            {
                ViewState["agencyPayableDraft"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //get search criteria.
            ApplySecurity();
            AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria = new AgencyPayableSearchCriteriaDTO();
            agencyPayableSearchCriteria = GetCriteria();
            if (!IsPostBack)
            {
                //display all info match criteria to gridview
                DisplayNewAgencyPayableResult(agencyPayableSearchCriteria);
            }
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanEdit(Constant.MENU_ITEM_TARGET_AGENCY_ACCOUNT_PAYABLE))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR999");
            }
        }
        /// <summary>
        /// get all criteria from view state and put them into AgencyPayableSearchCriteriaDTO
        /// </summary>
        /// <returns>AgencyPayableSearchCriteriaDTO</returns>
        protected AgencyPayableSearchCriteriaDTO GetCriteria()
        {
            AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria = new AgencyPayableSearchCriteriaDTO();
            int agencyid = int.Parse(Request.QueryString["agencyid"].ToString());
            string casecomplete = Request.QueryString["casecomplete"].ToString();
            DateTime periodenddate = Convert.ToDateTime(Request.QueryString["periodenddate"].ToString());
            DateTime periodstartdate = Convert.ToDateTime(Request.QueryString["periodstartdate"].ToString());
            int indicator = Convert.ToInt16(Request.QueryString["indicator"]);
            agencyPayableSearchCriteria.AgencyId = agencyid;
            agencyPayableSearchCriteria.CaseComplete = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), casecomplete);
            agencyPayableSearchCriteria.PeriodStartDate = periodstartdate.AddMonths(-6);
            agencyPayableSearchCriteria.PeriodEndDate = periodenddate;
            agencyPayableSearchCriteria.Indicator = indicator;
            return agencyPayableSearchCriteria;
        }
        /// <summary>
        /// bind search data match search criteria into gridview
        /// </summary>
        protected void BindGridView()
        {
            grvInvoiceItems.DataSource = this.agencyPayableDraft.ForclosureCaseDrafts;
            grvInvoiceItems.DataBind();
            double total = 0;
            //calculate the total amount of ForeclosureCaseDraftDTOCollection
            foreach (var item in this.agencyPayableDraft.ForclosureCaseDrafts)
            {
                total += item.Amount == null ? 0 : item.Amount.Value;
            }
            //add the values you just calculate to lable in UI
            lblInvoiceTotalFooter.Text = String.Format("{0:c}", total);
            lblTotalCasesFooter.Text = this.agencyPayableDraft.TotalCases.ToString();
            lblTotalAmount.Text = String.Format("{0:C}", total);
            lblTotalCases.Text = this.agencyPayableDraft.TotalCases.ToString();
            this.agencyPayableDraft.TotalAmount = total;
            if (this.agencyPayableDraft.ForclosureCaseDrafts.Count > 0)
                btnGeneratePayable.Enabled = true;
            else btnGeneratePayable.Enabled = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agencyPayableSearchCriteria"></param>
        protected void DisplayNewAgencyPayableResult(AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria)
        {
            try
            {
                AgencyPayableDraftDTO agencyPayableDraftDTO = new AgencyPayableDraftDTO();
                agencyPayableDraftDTO = AgencyPayableBL.Instance.CreateDraftAgencyPayable(agencyPayableSearchCriteria);
                if (agencyPayableDraftDTO.ForclosureCaseDrafts != null)
                {
                    grvInvoiceItems.DataSource = agencyPayableDraftDTO.ForclosureCaseDrafts;
                    this.agencyPayableDraft = agencyPayableDraftDTO;
                    grvInvoiceItems.DataBind();
                    AgencyDTOCollection agencyCol = LookupDataBL.Instance.GetAgency();
                    lblAgency.Text = agencyCol.GetAgencyName(agencyPayableSearchCriteria.AgencyId);
                    lblPeriodStart.Text = agencyPayableSearchCriteria.PeriodStartDate.ToShortDateString();
                    lblPeriodEnd.Text = agencyPayableSearchCriteria.PeriodEndDate.ToShortDateString();
                    lblTotalAmount.Text = String.Format("{0:C}", agencyPayableDraftDTO.TotalAmount);
                    lblTotalCases.Text = agencyPayableDraftDTO.TotalCases.ToString();
                    lblTotalCasesFooter.Text = agencyPayableDraftDTO.ForclosureCaseDrafts.Count.ToString();
                    double total = 0;
                    //calculate total amount of cases - search data match  search criteria.
                    foreach (var item in agencyPayableDraftDTO.ForclosureCaseDrafts)
                    {
                        total += item.Amount == null ? 0 : item.Amount.Value;
                    }
                    lblInvoiceTotalFooter.Text = total.ToString();

                    lblTotalAmount.Text = String.Format("{0:C}", total);

                }
                else
                {
                    lblMessage.Text = "no data";

                }
                if (agencyPayableDraftDTO.ForclosureCaseDrafts.Count == 0)
                {
                    btnGeneratePayable.Enabled = false; 
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        /// <summary>
        /// manage all check boxes in gridview row when check change in checkbox header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkHeaderCaseIDCheck(object sender, EventArgs e)
        {
            CheckBox chkdelall = (CheckBox)grvInvoiceItems.HeaderRow.FindControl("chkHeaderCaseID");
            foreach (GridViewRow row in grvInvoiceItems.Rows)
            {
                CheckBox chkdel = (CheckBox)row.FindControl("chkCaseID");
                chkdel.Checked = chkdelall.Checked;
            }
        }
        /// <summary>
        /// save data in gridview to suitable tables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGeneratePayable_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.agencyPayableDraft.ForclosureCaseDrafts.Count > 0)
                {
                    AgencyPayableDraftDTO agencyPayableDraftDTO = new AgencyPayableDraftDTO();
                    AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria = new AgencyPayableSearchCriteriaDTO();
                    agencyPayableSearchCriteria = GetCriteria();
                    agencyPayableDraftDTO = AgencyPayableBL.Instance.CreateDraftAgencyPayable(agencyPayableSearchCriteria);
                    for (int i = 0; i < this.agencyPayableDraft.ForclosureCaseDrafts.Count; i++)
                    {
                        this.agencyPayableDraft.ForclosureCaseDrafts[i].SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                    }
                    agencyPayableDraftDTO.ForclosureCaseDrafts = this.agencyPayableDraft.ForclosureCaseDrafts;
                    //agencyPayableDraftDTO.TotalAmount = double.Parse(lblTotalAmount.Text.ToString());
                    agencyPayableDraftDTO.TotalAmount = this.agencyPayableDraft.TotalAmount;
                    agencyPayableDraftDTO.TotalCases = this.agencyPayableDraft.ForclosureCaseDrafts.Count;
                    agencyPayableDraftDTO.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.UserId.ToString());
                    AgencyPayableBL.Instance.InsertAgencyPayable(agencyPayableDraftDTO);
                    Response.Redirect("AgencyPayable.aspx");
                }
                else lblMessage.Text = "Please choose a record.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        /// <summary>
        /// occur when you click on RemoveMarkedCases button
        /// clear check row from gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRemoveMarkedCases_Click(object sender, EventArgs e)
        {
            for (int i = grvInvoiceItems.Rows.Count - 1; i >= 0; i--)
            {
                CheckBox chkdel = (CheckBox)grvInvoiceItems.Rows[i].FindControl("chkCaseID");
                if (chkdel.Checked == true)
                {
                    ForeclosureCaseDraftDTO agency = this.agencyPayableDraft.ForclosureCaseDrafts[i];
                    this.agencyPayableDraft.ForclosureCaseDrafts.Remove(agency);
                }
            }
            BindGridView();

        }
        /// <summary>
        /// return NewPayableCriteria page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelPayable_Click(object sender, EventArgs e)
        {
            AgencyPayableSearchCriteriaDTO criteria = GetCriteria();
            string query = GetQueryString(criteria);
            Response.Redirect("CreateNewPayable.aspx" + query);
        }
        private string GetQueryString(AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria)
        {
            StringBuilder query = new StringBuilder();
            query.Append("?agencyid=");
            query.Append(agencyPayableSearchCriteria.AgencyId);
            query.Append("&casecomplete=");
            query.Append(agencyPayableSearchCriteria.CaseComplete);
            query.Append("&periodenddate=");
            query.Append(agencyPayableSearchCriteria.PeriodEndDate.ToShortDateString());
            query.Append("&periodstartdate=");
            query.Append(agencyPayableSearchCriteria.PeriodStartDate.ToShortDateString());
            query.Append("&indicator=");
            query.Append(agencyPayableSearchCriteria.Indicator);
            return query.ToString();
        }
    }
}