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
using HPF.FutureState.Web.Security;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;
namespace HPF.FutureState.Web
{
    public partial class ChagePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_chagepassword_Click(object sender, EventArgs e)
        {
            //verify old password
            lbl_status.Visible = false;
            if (txt_newpassword.Value != txt_confirmnewpassword.Value)
            {
                lbl_status.Text = "Password does not match.";
                lbl_status.Visible = true;
                return;
            }
            try
            {
                if (SecurityBL.Instance.WebUserChangePassword(HPFWebSecurity.CurrentIdentity.LoginName, txt_oldpassword.Value, txt_newpassword.Value))
                {
                    lbl_status.Visible = true;
                    lbl_status.Text = "Change password successfully";
                }
            }
            catch (Exception ex)
            {
                lbl_status.Text = ex.Message;
                lbl_status.Visible = true;
                ExceptionProcessor.HandleException(ex);
            }
        }
    }
}
