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

namespace HPF.FutureState.Web.AppManageEvalSection
{
    public partial class AppManageEvalSectionUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindDropDownList();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName); 
            }
        }
        private void BindDropDownList()
        {
            EvalSectionCollectionDTO evalSectionCollection = EvalTemplateBL.Instance.RetriveAllEvalSection();
            ddlSection.DataValueField = "EvalSectionId";
            ddlSection.DataTextField = "SectionName";
            ddlSection.DataSource = evalSectionCollection;
            ddlSection.DataBind();
            ddlSection.Items.RemoveAt(ddlSection.Items.IndexOf(ddlSection.Items.FindByValue("-1")));
            ddlSection.Items.Insert(0, new ListItem("Select Section", "-1"));
            ddlSection.Items.FindByText("Select Section").Selected = true;
        }
    }
}