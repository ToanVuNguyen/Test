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
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Web.AppNewPayable
{
    public partial class NewAgencyPayableResultsUC : System.Web.UI.UserControl
    {

        AgencyPayableDraftDTO agencyPayableDraftDTO = new AgencyPayableDraftDTO();
        //ForeclosureCaseDraftDTOCollection fcDraftCol;
        AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria = new AgencyPayableSearchCriteriaDTO();
        ForeclosureCaseDraftDTOCollection FCDraftCol
        {
            get
            { return (ForeclosureCaseDraftDTOCollection)ViewState["fcDraftCol"]; }
            set
            {
                ViewState["fcDraftCol"] = value;  
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            
            agencyPayableSearchCriteria = GetCriteria();
            if (!IsPostBack)
            {
                DisplayNewAgencyPayableResult(agencyPayableSearchCriteria);
            }
           

        }
        protected AgencyPayableSearchCriteriaDTO GetCriteria()
        {
            AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria = new AgencyPayableSearchCriteriaDTO();
            int agencyid = int.Parse(Request.QueryString["agencyid"].ToString());
            string casecomplete = Request.QueryString["casecomplete"].ToString();

            DateTime periodenddate = Convert.ToDateTime(Request.QueryString["periodenddate"].ToString());

            string servicerconsent = Request.QueryString["servicerconsent"].ToString();

            DateTime periodstartdate = Convert.ToDateTime(Request.QueryString["periodstartdate"].ToString());

            string fundingconsent = Request.QueryString["fundingconsent"].ToString();
            int maxnumbercase = int.Parse(Request.QueryString["maxnumbercase"].ToString());
            string indicator = Request.QueryString["indicator"].ToString();

            agencyPayableSearchCriteria.AgencyId = agencyid;
            agencyPayableSearchCriteria.CaseComplete = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), casecomplete);
            //if (casecomplete == "Y")
            //    agencyPayableSearchCriteria.CaseComplete = CustomBoolean.Y;
            //if (casecomplete == "N")
            //    agencyPayableSearchCriteria.CaseComplete = CustomBoolean.N;
            //else agencyPayableSearchCriteria.CaseComplete = CustomBoolean.None;

            agencyPayableSearchCriteria.PeriodStartDate = periodstartdate;
            agencyPayableSearchCriteria.ServicerConsent = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), servicerconsent);
           
            agencyPayableSearchCriteria.PeriodEndDate = periodenddate;

            agencyPayableSearchCriteria.FundingConsent = (CustomBoolean)Enum.Parse(typeof(CustomBoolean), fundingconsent);
           
            agencyPayableSearchCriteria.MaxNumberOfCase = maxnumbercase;
            //if (indicator == "") indicator = null;
            agencyPayableSearchCriteria.LoanIndicator = indicator;
            
            return agencyPayableSearchCriteria;
        }
        protected void BindGridView()
        {
            grvInvoiceItems.DataSource = this.FCDraftCol;
            grvInvoiceItems.DataBind();
            decimal total = 0;
            foreach (var item in this.FCDraftCol)
            {
                total += item.Amount;
            }
            
            lblInvoiceTotalFooter.Text = total.ToString();
            lblTotalCasesFooter.Text = this.FCDraftCol.Count.ToString();
            lblTotalAmount.Text = total.ToString();
            lblTotalCases.Text = this.FCDraftCol.Count.ToString();
            
            
        }
        protected void DisplayNewAgencyPayableResult(AgencyPayableSearchCriteriaDTO agencyPayableSearchCriteria)
        {
            try
            {
                agencyPayableDraftDTO = AgencyPayableBL.Instance.CreateDraftAgencyPayable(agencyPayableSearchCriteria);
                if (agencyPayableDraftDTO.ForclosureCaseDrafts != null)
                {
                    grvInvoiceItems.DataSource = agencyPayableDraftDTO.ForclosureCaseDrafts;
                    this.FCDraftCol = agencyPayableDraftDTO.ForclosureCaseDrafts;
                    grvInvoiceItems.DataBind();

                    lblAgency.Text = agencyPayableSearchCriteria.AgencyId.ToString();
                    lblPeriodStart.Text = agencyPayableSearchCriteria.PeriodStartDate.ToShortDateString();
                    lblPeriodEnd.Text = agencyPayableSearchCriteria.PeriodEndDate.ToShortDateString();
                    lblTotalAmount.Text = agencyPayableDraftDTO.TotalAmount.ToString();
                    lblTotalCases.Text = agencyPayableDraftDTO.TotalCases.ToString();
                    lblTotalCasesFooter.Text = agencyPayableDraftDTO.ForclosureCaseDrafts.Count.ToString();
                    decimal total = 0;
                    foreach (var item in agencyPayableDraftDTO.ForclosureCaseDrafts)
                    {
                        //test
                        item.Amount = 10;
                        total +=item.Amount;
                    }
                    lblInvoiceTotalFooter.Text = total.ToString();
                    lblTotalAmount.Text = total.ToString();
                }
                else lblMessage.Text = "no data";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                //ExceptionProcessor.HandleException(ex);
            }
        }

        protected void chkHeaderCaseIDCheck(object sender, EventArgs e)
        {
            CheckBox chkdelall = (CheckBox)grvInvoiceItems.HeaderRow.FindControl("chkHeaderCaseID");
            foreach (GridViewRow row in grvInvoiceItems.Rows)
            {
                CheckBox chkdel = (CheckBox)row.FindControl("chkCaseID");
                chkdel.Checked = chkdelall.Checked;
            }
        }
        protected void btnGeneratePayable_Click(object sender, EventArgs e)
        {
            try
            {
                agencyPayableSearchCriteria = GetCriteria();
                agencyPayableDraftDTO = AgencyPayableBL.Instance.CreateDraftAgencyPayable(agencyPayableSearchCriteria);
                agencyPayableDraftDTO.ForclosureCaseDrafts = this.FCDraftCol;
                agencyPayableDraftDTO.TotalAmount = decimal.Parse(lblTotalAmount.Text.ToString());
                agencyPayableDraftDTO.TotalCases = this.FCDraftCol.Count;
                AgencyPayableBL.Instance.InsertAgencyPayable(agencyPayableDraftDTO);
                lblMessage.Text = "Generate succesfull.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                //ExceptionProcessor.HandleException(ex);
                
            }
        }
        protected void btnRemoveMarkedCases_Click(object sender, EventArgs e)
        {
            for(int i=grvInvoiceItems.Rows.Count-1;i>=0;i--)
            {
                CheckBox chkdel = (CheckBox)grvInvoiceItems.Rows[i].FindControl("chkCaseID");
                if (chkdel.Checked == true)
                {
                    ForeclosureCaseDraftDTO agency = this.FCDraftCol[i];
                    this.FCDraftCol.Remove(agency);
                }
            }
            BindGridView();
}

        protected void btnCancelPayable_Click(object sender, EventArgs e)
        {
            string query = "?agencyid="+lblAgency.Text.ToString();
            Response.Redirect("NewPayableCriteria.aspx"+query);
        }


    }
}