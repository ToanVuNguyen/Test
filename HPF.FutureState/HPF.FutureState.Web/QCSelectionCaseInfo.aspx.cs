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

namespace HPF.FutureState.Web
{
    public partial class QCSelectionCaseInfo : System.Web.UI.Page
    {
        string UCLOCATION = "QCSelectionCaseDetail\\";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMessage();
                BindData();
                tabControl2.TabClick += new HPF.FutureState.Web.HPFWebControls.TabControlEventHandler(tabControl_TabClick);
                if (!IsPostBack)
                {
                    tabControl2.AddTab("agencyAudit", "Agency Audit");
                    tabControl2.AddTab("hpfAudit", "HPF Audit");
                    tabControl2.AddTab("compareResult", "Compare Result");
                    tabControl2.AddTab("fileUploads", "File Upload");
                    tabControl2.SelectedTab = "agencyAudit";
                    UserControlLoader2.LoadUserControl(UCLOCATION + "AgencyAudit.ascx", "ucAgencyAudit");
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(ex.Message);
            }
        }

        private void BindData()
        {
            ClearErrorMessage();
            if (Request.QueryString["caseId"] == null)
                return;
            int caseId = int.Parse(Request.QueryString["caseId"].ToString());
            var selectionCase = GetSelectionCase(caseId);
            BindSelectionCaseToUI(selectionCase);

        }
        private CaseEvalSearchResultDTO GetSelectionCase(int caseId)
        {
            return CaseEvaluationBL.Instance.SearchCaseEvalByFcId(caseId);
        }
        private void BindSelectionCaseToUI(CaseEvalSearchResultDTO caseSelection)
        {
            lblHpfID.Text = caseSelection.FcId.ToString();
            lblAgencyName.Text = caseSelection.AgencyName;
            lblHomeOwner.Text = caseSelection.HomeowenerFirstName + " " + caseSelection.HomeowenerLastName;
            lblEvaluationStatus.Text = caseSelection.EvalStatus;
            lblLoanNumber.Text = caseSelection.LoanNumber;
            lblServicerName.Text = caseSelection.ServicerName;
            lblZipCode.Text = caseSelection.ZipCode;
            lblCallDate.Text = caseSelection.CallDate.ToString();
            lblCounselor.Text = caseSelection.CounselorName;
        }
        private void AddErrorMessage(string errMes)
        {
            lblErrorMessage.Items.Add(new ListItem(errMes));
        }
        private void ClearErrorMessage()
        {
            lblErrorMessage.Items.Clear();
        }
        //display message when you click out casedetail tab
        void tabControl_TabClick(object sender, HPF.FutureState.Web.HPFWebControls.TabControlEventArgs e)
        {
            try
            {
                switch (e.SelectedTabID)
                {
                    case "agencyAudit":
                        UserControlLoader2.LoadUserControl(UCLOCATION + "AgencyAudit.ascx", "ucAgencyAudit");
                        break;
                    case "hpfAudit":
                        UserControlLoader2.LoadUserControl(UCLOCATION + "HpfAudit.ascx", "ucHpfAudit");
                        break;
                    case "compareResult":
                        UserControlLoader2.LoadUserControl(UCLOCATION + "CompareResult.ascx", "ucCompareResult");
                        break;
                    case "fileUploads":
                        UserControlLoader2.LoadUserControl(UCLOCATION + "FileUploads.ascx", "ucFileUploads");
                        break;
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage(ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }

        }
    }
}
