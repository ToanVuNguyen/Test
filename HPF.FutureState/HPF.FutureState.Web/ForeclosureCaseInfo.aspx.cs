using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common;

namespace HPF.FutureState.Web
{
    public partial class AppForeclosureCaseDetailPage : System.Web.UI.Page
    {

        string UCLOCATION = "ForeclosureCaseDetail\\";

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplySecurity();
            try
            {
                btnEmailSummary.Attributes.Add("onclick", "return ChangeData();");
                ClearErrorMessage();
                BindData();
                tabControl.TabClick += new HPF.FutureState.Web.HPFWebControls.TabControlEventHandler(tabControl_TabClick);
                if (!IsPostBack)
                {
                    tabControl.AddTab("caseDetail", "Case Detail");
                    tabControl.AddTab("caseLoan", "Case Loan(s)");
                    tabControl.AddTab("budget", "Budget(s)");
                    tabControl.AddTab("outcome", "Outcome(s)");
                    tabControl.AddTab("accounting", "Accounting");
                    tabControl.AddTab("activityLog", "Activity Log");
                    tabControl.AddTab("caseFollowUp", "Case Follow-Up");
                    tabControl.AddTab("audit", "Audit");
                    tabControl.SelectedTab = "caseDetail";
                    UserControlLoader.LoadUserControl(UCLOCATION + "CaseDetail.ascx", "ucCaseDetail");
                }
                if (emailSummary.Value == "EMAILSUMMARY")
                {
                    emailSummary.Value = string.Empty;
                    PopupEmail();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
                //Response.Redirect("SearchForeclosureCase.aspx");
            }
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_FORECLOSURE_CASE_DETAIL))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
        }
        private void ClearErrorMessage()
        {
            lblErrorMessage.Items.Clear();
        }
        private void AddErrorMessage(string errMes)
        {
            lblErrorMessage.Items.Add(new ListItem(errMes));
        }
        private void BindData()
        {
            ClearErrorMessage();
            if (Request.QueryString["CaseID"] == null)
                return;
            int caseid = int.Parse(Request.QueryString["CaseID"].ToString());
            var ForeclosureCase = GetForeclosureCase(caseid);
            //store in session to get info for summary email
            if (!IsPostBack)
                Session["foreclosureInfo"] = ForeclosureCase;
            //
            BindForeclosureCaseToUI(ForeclosureCase);
        }

        private void BindForeclosureCaseToUI(ForeclosureCaseDTO ForeclosureCase)
        {
            lblHpfID.Text = ForeclosureCase.FcId.ToString();
            lblBorrower.Text = ForeclosureCase.BorrowerFname + " " + ForeclosureCase.BorrowerMname + " " + ForeclosureCase.BorrowerLname;
            lblPropertyAddress.Text = ForeclosureCase.PropCity + ", " + ForeclosureCase.PropStateCd + ", " + ForeclosureCase.PropZip;
            lblLoanList.Text = ForeclosureCase.LoanList;
            lblCounselor.Text = ForeclosureCase.CounselorFname + " " + ForeclosureCase.CounselorLname;
            if(string.IsNullOrEmpty(ForeclosureCase.CounselorExt))
                lblPhone.Text = ForeclosureCase.CounselorPhone ;
                else lblPhone.Text = ForeclosureCase.CounselorPhone + " (ext:" + ForeclosureCase.CounselorExt+")";
            lblCounselorEmail.Text = ForeclosureCase.CounselorEmail;
            lblAgencyName.Text = GetAgencyName(ForeclosureCase.AgencyId);
        }

        private ForeclosureCaseDTO GetForeclosureCase(int caseid)
        {
            var ForeclosureCase = ForeclosureCaseBL.Instance.GetForeclosureCase(caseid);
            return ForeclosureCase;
        }
        protected String GetAgencyName(int? agencyID)
        {
            try
            {
                AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgency();
                AgencyDTO item = agencyCollection[0];
                agencyCollection.Remove(item);
                foreach (var i in agencyCollection)
                {
                    if (i.AgencyID == agencyID.ToString())
                        return i.AgencyName;
                }
                
            }
            catch (Exception ex)
            {
                AddErrorMessage(ex.Message);
                ExceptionProcessor.HandleException(ex);
            }
            return string.Empty;
        }
        //display message when you click out casedetail tab
        void tabControl_TabClick(object sender, HPF.FutureState.Web.HPFWebControls.TabControlEventArgs e)
        {
            try
            {
                switch (e.SelectedTabID)
                {
                    case "caseDetail":
                        UserControlLoader.LoadUserControl(UCLOCATION + "CaseDetail.ascx", "ucCaseDetail");
                        break;
                    case "caseLoan":
                        UserControlLoader.LoadUserControl(UCLOCATION + "CaseLoan.ascx", "ucCaseLoan");
                        break;
                    case "budget":
                        UserControlLoader.LoadUserControl(UCLOCATION + "Budget.ascx", "ucBudget");
                        break;
                    case "outcome":
                        UserControlLoader.LoadUserControl(UCLOCATION + "Outcome.ascx", "ucOutcome");
                        break;
                    case "accounting":
                        UserControlLoader.LoadUserControl(UCLOCATION + "Accounting.ascx", "ucAccounting");
                        break;
                    case "activityLog":
                        UserControlLoader.LoadUserControl(UCLOCATION + "ActivityLog.ascx", "ucActivityLog");
                        break;
                    case "caseFollowUp":
                        UserControlLoader.LoadUserControl(UCLOCATION + "CaseFollowUp.ascx", "ucCaseFollowUp");
                        break;
                    case "audit":
                        UserControlLoader.LoadUserControl(UCLOCATION + "Audit.ascx", "ucAudit");
                        break;
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage(ex.Message);
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
            
        }
        protected void btnEmailSummary_Click(object sender, EventArgs e)
        {
            PopupEmail();
        }

        private void PopupEmail()
        {
            try
            {
                if (Request.QueryString["CaseID"] == null)
                    return;
                int caseid = int.Parse(Request.QueryString["CaseID"].ToString());
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Email Summary", "<script language='javascript'>window.open('EmailSummary.aspx?CaseID=" + caseid + "','','menu=no,scrollbars=no,resizable=yes,top=0,left=0,width=1010px,height=500px')</script>");
            }
            catch (Exception ex)
            {
                AddErrorMessage(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        protected void btn_Print_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["CaseID"] == null)
                    return;
                int caseid = int.Parse(Request.QueryString["CaseID"].ToString());
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Print Summary", "<script language='javascript'>window.open('PrintSummary.aspx?CaseID=" + caseid + "','','menu=no,scrollbars=no,resizable=yes,top=0,left=0,width=1010px,height=900px')</script>");
            }
            catch (Exception ex)
            {
                AddErrorMessage(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        protected void btnResendServicer_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["CaseID"] == null)
                return;
            
            try
            {
                int caseid = int.Parse(Request.QueryString["CaseID"].ToString());
                var foreclosureCase = GetForeclosureCase(caseid);
                ForeclosureCaseBL.Instance.ResendToServicer(foreclosureCase);
            }
            catch (Exception ex)
            {
                AddErrorMessage(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
    }
}
