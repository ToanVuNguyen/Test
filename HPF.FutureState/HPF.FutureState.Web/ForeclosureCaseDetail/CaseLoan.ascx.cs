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
using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class CaseLoan : System.Web.UI.UserControl
    {
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                int caseid = int.Parse(Request.QueryString["CaseID"].ToString());
                CaseLoanDTOCollection caseLoanCollection = GetCaseLoan(caseid);
                if (caseLoanCollection != null)
                {
                    lblMessage.Visible = false;
                    dtlCaseLoan.DataSource = caseLoanCollection;
                    dtlCaseLoan.DataBind();
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "No data found";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex);
            }

        }        

        private CaseLoanDTOCollection GetCaseLoan(int fcId)
        {
            CaseLoanDTOCollection caseLoanCollection = null;
            caseLoanCollection = CaseLoanBL.Instance.RetrieveCaseLoan(fcId);
            if (caseLoanCollection != null)
            {
                foreach (CaseLoanDTO item in caseLoanCollection)
                {
                    item.ArmResetInd = DisplayInd(item.ArmResetInd);
                    item.Loan1st2nd = DisplayMortgage(item.Loan1st2nd);
                }
            }
            return caseLoanCollection;
        }

        /// <summary>
        /// help to return Y: Yes, N: No and Null: ""
        /// </summary>
        /// <param name="Ind"></param>
        /// <returns></returns>
        private string DisplayInd(string ind)
        {
            if (ind == "Y")
                return "Yes";
            if (ind == "N")
                return "No";
            return "";
        }

        private string DisplayMortgage(string mortgage)
        {
            if (mortgage == null || mortgage == string.Empty)
                return string.Empty;
            string s1 = string.Empty;
            string s2 = string.Empty;
            try
            {
                s1 = mortgage.Substring(0, 1);
                s2 = mortgage.Substring(1, 2);
                s2 = "<sup>" + s2 + "</sup>" + " Mortgage";
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex);
            }
            return s1 + s2;
        }
    }
}