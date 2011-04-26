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
using HPF.FutureState.Web.Security;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Web.MarkRebillables
{
    public partial class MarkRebillables : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplySecurity();
        }
        private void ApplySecurity()
        {
            if (!HPFWebSecurity.CurrentIdentity.CanView(Constant.MENU_ITEM_TARGET_APP_MARKREBILLABLE_INVOCE_CASES))
            {
                Response.Redirect("ErrorPage.aspx?CODE=ERR0999");
            }
        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                lstErrorMessage.Items.Clear();
                InvoiceBL workingInstance = InvoiceBL.Instance;
                int processCount = workingInstance.MarkRebillableInvoceCases(fileUpload.FileContent, HPFWebSecurity.CurrentIdentity.LoginName);

                string message = string.Format("{0} invoice cases have been marked rebillable as per uploaded excel file", processCount);
                if (workingInstance.WarningMessage.Count > 0)
                {
                    lstErrorMessage.DataSource = workingInstance.WarningMessage;
                    lstErrorMessage.DataBind();
                }
                lstErrorMessage.Items.Add(message);
            }
            catch (DataValidationException dataEx)
            {
                lstErrorMessage.DataSource = dataEx.ExceptionMessages;
                lstErrorMessage.DataBind();
                ExceptionProcessor.HandleException(dataEx, HPFWebSecurity.CurrentIdentity.LoginName);
            }
            catch (Exception ex)
            {
                lstErrorMessage.Items.Add("" + ex.Message);
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
    }
}