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
using HPF.FutureState.Common;
using HPF.FutureState.BusinessLogic;
using System.Text;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Web.QCReport
{
    public partial class QCReportUC : System.Web.UI.UserControl
    {
        private const int MONTHS = 24;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindReportingName();
                BindAgencyDropDownList();
                BindEvalTypeDropDownList();
                BindMonthYearDropDownList();
            }
        }
        protected void BindAgencyDropDownList()
        {
            try
            {
                AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgencies();
                ddlAgency.DataValueField = "AgencyID";
                ddlAgency.DataTextField = "AgencyName";
                if (string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_HPF) == 0)
                {
                    ddlAgency.DataSource = agencyCollection;
                    ddlAgency.DataBind();
                    ddlAgency.Items.RemoveAt(ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue("-1")));
                    ddlAgency.Items.Insert(0, new ListItem("All Agencies", "-1"));
                    ddlAgency.Items.FindByText("All Agencies").Selected = true;
                }
                else if (string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_AGENCY) == 0)
                {
                    AgencyDTO agency = agencyCollection.FirstOrDefault(o => o.AgencyID == HPFWebSecurity.CurrentIdentity.AgencyId.ToString());
                    agencyCollection = new AgencyDTOCollection();
                    agencyCollection.Add(agency);
                    ddlAgency.DataSource = agencyCollection;
                    ddlAgency.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void BindReportingName()
        {
            ddlReportingName.Items.Add(new ListItem("Monthly Summary - Detail Result",Constant.QC_MONTHLY_SUMMARY_REPORT_TYPE));
            ddlReportingName.Items.Add(new ListItem("Monthly Calibration",Constant.QC_MONTHLY_CALIBRATION_SUMMARY_REPORT_TYPE));
            ddlReportingName.Items.Add(new ListItem("Annual Onsite Summary", Constant.QC_ANNUAL_ONSITE_SUMMARY_REPORT_TYPE));
        }
        private void BindEvalTypeDropDownList()
        {
            ddlEvaluationType.Items.Add(new ListItem("All", "All"));
            ddlEvaluationType.Items.Add(new ListItem(CaseEvaluationBL.EvaluationType.DESKTOP, CaseEvaluationBL.EvaluationType.DESKTOP));
            ddlEvaluationType.Items.Add(new ListItem(CaseEvaluationBL.EvaluationType.ONSITE, CaseEvaluationBL.EvaluationType.ONSITE));
        }
        /// <summary>
        /// Display MonthYear dropdownlist for From and To
        /// </summary>
        private void BindMonthYearDropDownList()
        {
            DateTime dt;
            StringBuilder text;
            StringBuilder value;
            for (int i = 0; i < MONTHS; i++)
            {
                text = new StringBuilder();
                value = new StringBuilder();
                dt = DateTime.Now.AddMonths(0 - i);
                text.AppendFormat("{0}-{1}", dt.ToString("MM"), dt.ToString("yyyy"));
                value.AppendFormat("{0}{1}", dt.ToString("yyyy"), dt.ToString("MM"));
                ddlYearMonthFrom.Items.Add(new ListItem(text.ToString(), value.ToString()));
                ddlYearMonthTo.Items.Add(new ListItem(text.ToString(), value.ToString()));
            }
            string prevMonth = DateTime.Now.AddMonths(-1).ToString("MM") + "-" + DateTime.Now.AddMonths(-1).ToString("yyyy");
            ddlYearMonthFrom.Items.FindByText(prevMonth).Selected = true;
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Print QC Report", "<script language='javascript'>window.open('PrintQCReport.aspx?ReportType=" + ddlReportingName.SelectedValue  
                +"&AgencyId="+ddlAgency.SelectedValue
                +"&EvalType="+ddlEvaluationType.SelectedValue
                +"&From="+ddlYearMonthFrom.SelectedValue
                +"&To="+ddlYearMonthTo.SelectedValue
                +"','','menu=no,scrollbars=no,resizable=yes,top=0,left=0,width=1010px,height=900px')</script>");
        }
    }
}