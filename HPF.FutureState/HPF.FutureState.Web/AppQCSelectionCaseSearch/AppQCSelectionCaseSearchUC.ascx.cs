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
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Web.Security;
using System.Text;
using HPF.FutureState.Common;

namespace HPF.FutureState.Web.ApphQCSelectionCaseSearch
{
    public partial class AppQCSelectionCaseSearchUC : System.Web.UI.UserControl
    {
        private const int MONTHS = 24;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearErrorMessages();
            if (!IsPostBack)
            {
                BindMonthYearDropDownList();
                BindAgencyDropDownList();
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
                dt = DateTime.Now.AddMonths(0-i);
                text.AppendFormat("{0}-{1}",dt.ToString("MM"),dt.ToString("yyyy"));
                value.AppendFormat("{0}{1}",dt.ToString("yyyy"),dt.ToString("MM"));
                ddlYearMonthFrom.Items.Add(new ListItem(text.ToString(),value.ToString()));
                ddlYearMonthTo.Items.Add(new ListItem(text.ToString(), value.ToString()));
            }
            string prevMonth = DateTime.Now.AddMonths(-1).ToString("MM") + "-" + DateTime.Now.AddMonths(-1).ToString("yyyy");
            ddlYearMonthFrom.Items.FindByText(prevMonth).Selected = true;
        }
        private int ConvertToInt(object obj)
        {
            int value;
            if (int.TryParse(obj.ToString().Trim(), out value))
                return value;
            return int.MinValue;
        }
        private void ClearErrorMessages()
        {
            lblErrorMessage.Items.Clear();
        }
        private CaseEvalSearchCriteriaDTO GetSearchCriteria()
        {
            CaseEvalSearchCriteriaDTO caseEvalCriteria = new CaseEvalSearchCriteriaDTO();
            caseEvalCriteria.AgencyId = ConvertToInt(ddlAgency.SelectedValue);
            caseEvalCriteria.YearMonthFrom = ddlYearMonthFrom.SelectedValue;
            caseEvalCriteria.YearMonthTo = ddlYearMonthTo.SelectedValue;
            return caseEvalCriteria;
        }
        private void CaseEvalSearch(CaseEvalSearchCriteriaDTO searchCriteria)
        {
            CaseEvalSearchResultDTOCollection searchResult = CaseEvaluationBL.Instance.SearchCaseEval(searchCriteria);
            grvCaseEvalList.DataSource = searchResult;
            grvCaseEvalList.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMessages();
                CaseEvalSearchCriteriaDTO searchCriteria = GetSearchCriteria();
                CaseEvalSearch(searchCriteria);
            }
            catch (DataValidationException ex)
            {
                lblErrorMessage.DataSource = ex.ExceptionMessages;
                lblErrorMessage.DataBind();
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                grvCaseEvalList.DataSource = null;
                grvCaseEvalList.DataBind();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Items.Add(new ListItem(ex.Message));
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                grvCaseEvalList.DataSource = null;
                grvCaseEvalList.DataBind();
            }
        }

        protected void btnEditCase_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            if (grvCaseEvalList.SelectedValue == null)
            {
                //lblErrorMessage.Items.Add(new ListItem(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0568)));
                return;
            }
            int fcId = (int)grvCaseEvalList.SelectedValue;
            Response.Redirect("QCSelectionCaseInfo.aspx?caseId=" + fcId.ToString());
        }

        
    }
}