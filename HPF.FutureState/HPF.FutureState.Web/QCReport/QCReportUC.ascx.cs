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
                BindEvalTypeDropDownList();
                BindMonthYearDropDownList();
            }
        }
        private void BindReportingName()
        {
            ddlReportingName.Items.Add(new ListItem("Monthly Summary - Detail Result",Constant.QC_MONTHLY_SUMMARY_REPORT_TYPE));
            ddlReportingName.Items.Add(new ListItem("Monthly Calibration",Constant.QC_MONTHLY_CALIBRATION_SUMMARY_REPORT_TYPE));
        }
        private void BindEvalTypeDropDownList()
        {
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
                +"&EvalType="+ddlEvaluationType.SelectedValue
                +"&From"+ddlYearMonthFrom.SelectedValue
                +"&To"+ddlYearMonthTo.SelectedValue
                +"','','menu=no,scrollbars=no,resizable=yes,top=0,left=0,width=1010px,height=900px')</script>");
        }
    }
}