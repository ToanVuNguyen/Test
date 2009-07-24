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

using HPF.FutureState.Web.Security;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.Web.MarkDuplicateCases
{
    public partial class MarkDuplicateCases : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                lstErrorMessage.Items.Clear();
                int processCount = ForeclosureCaseBL.Instance.MarkDuplicateCases(fileUpload.FileContent, HPFWebSecurity.CurrentIdentity.LoginName);

                string message = string.Format("{0} cases have been marked duplicate as per uploaded excel file", processCount);
                lstErrorMessage.Items.Add(message);
            }
            catch (DataValidationException dataEx)
            {
                lstErrorMessage.DataSource = dataEx.ExceptionMessages;
                lstErrorMessage.DataBind();
            }
            catch (Exception ex)
            {
                lstErrorMessage.Items.Add(ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
    }
}