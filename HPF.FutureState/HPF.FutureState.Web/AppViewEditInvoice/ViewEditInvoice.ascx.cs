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

namespace HPF.FutureState.Web.AppViewEditInvoice
{
    public partial class ViewEditInvoice : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            BindDataToRejectReason();
        }
        private void BindDataToRejectReason()
        {
            try
            {
                RefCodeItemDTOCollection paymentRejectCode = LookupDataBL.Instance.GetRefCode("payment reject reason code");
                dropRejectReason.DataValueField = "Code";
                dropRejectReason.DataTextField = "CodeDesc";
                dropRejectReason.DataSource = paymentRejectCode;
                dropRejectReason.DataBind();
                dropRejectReason.Items.Insert(0, new ListItem(" ", "-1"));
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.Visible = true;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.DisplayName);
            }
        }
        protected void chkCheckAllCheck(object sender, EventArgs e)
        {
            CheckBox headerCheckbox = (CheckBox)sender;
            foreach (GridViewRow row in grvViewEditInvoice.Rows)
            {
                CheckBox chkSelected = (CheckBox)row.FindControl("chkSelected");
                if (chkSelected != null)
                    chkSelected.Checked = headerCheckbox.Checked;
            }
        }
    }
}